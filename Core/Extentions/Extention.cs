using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;
using System.Drawing;
using System.Globalization;
using System.Data;
using System.Linq;
using System.Threading;

namespace TkwEF.Core.Extentions
{
    #region ComboBox

    public static class ComboBoxExtention
    {
        /// <summary>
        /// Уставливаем ширину выпадающего списка по максимально длинной строке
        /// </summary>
        /// <param name="cbx"></param>
        /// <param name="isTrue">Надо ли устанавливать ширину выпадающего списка по максимально длинной строке</param>
        /// <param name="maxWidth">Если значение больше 0, то выпадающий список будет не больше введенного значения</param>
        static public void SetDropDownWidth(this ComboBox cbx, bool isTrue, int maxWidth = 0, int left = 0)
        {
            try
            {
                if (isTrue)
                {
                    //using (Graphics gr = Graphics.FromHwnd(cbx.Handle))
                    using (Graphics gr = cbx.CreateGraphics())
                    {
                        float maxW = 0f;
                        foreach (object o in cbx.Items)
                        {
                            string val;
                            if (cbx.DisplayMember != string.Empty)
                            {
                                PropertyDescriptorCollection prp = TypeDescriptor.GetProperties(o);
                                val = prp[cbx.DisplayMember].GetValue(o) == null ?
                                    "" :
                                    prp[cbx.DisplayMember].GetValue(o).ToString();
                            }
                            else
                            {
                                val = o.ToString();
                            }
                            float w = gr.MeasureString(val, cbx.Font).Width;
                            if (w > maxW)
                                maxW = w;
                            if (maxWidth > 0 && maxW >= maxWidth)
                                break;
                        }
                        maxW += 50;
                        if (cbx.Width > maxW)
                            maxW = cbx.Width;
                        // Проверяем чтобы ширина списка не выходила за пределы экрана
                        int clientSize =
                            Screen.PrimaryScreen.WorkingArea.Width - cbx.PointToScreen(new Point(left, 0)).X;
                        if (maxW > clientSize)
                            maxW = clientSize;
                        cbx.DropDownWidth = (int)maxW;
                    }
                }
                else
                {
                    cbx.DropDownWidth = cbx.Width;
                }
            }
            catch (Exception ex)
            {
                new Exception("Ошибка при открытии выпадающего списка", ex);
            }
        }
    }

    #endregion

    #region List

    static class ListExtention
    {
        static public int GetSum(this List<int> amounts)
        {
            int sum = 0;
            foreach (int amount in amounts)
            {
                sum += amount;
            }
            return sum;
        }

        static public int GetSumAndMessage(this List<int> amounts, string message)
        {
            int sum = 0;
            foreach (int amount in amounts)
            {
                sum += amount;
            }
            MessageBox.Show(string.Format("{0} = {1}", message, sum));
            return sum;
        }
    }

    #endregion

    #region Form

    public static class FormExtention
    {
        /// <summary>
        /// Возвращает true, если есть ошибка на форме
        /// </summary>
        /// <param name="frm"></param>
        /// <param name="errProvider"></param>
        /// <returns></returns>
        public static bool HasError(this Form frm, ErrorProvider errProvider)
        {
            foreach (Control ctr in frm.Controls)
            {
                if (HasErrorInControl(ctr, errProvider))
                    return true;
            }
            return false;
        }

        private static bool HasErrorInControl(Control ctr, ErrorProvider errProvider)
        {
            //if (!ctr.Visible)
            //    return false;
            if (errProvider.GetError(ctr) != string.Empty)
                return true;
            foreach (Control c in ctr.Controls)
            {
                if (!string.IsNullOrEmpty(errProvider.GetError(c)))
                    return true;
                if (c.HasChildren)
                    if (HasErrorInControl(c, errProvider))
                        return true;
            }
            return false;
        }
        /// <summary>
        /// Включает / отключает песочные часы
        /// </summary>
        /// <param name="frm">Вызывающая форма</param>
        /// <param name="isWait">true - включить; false - отключить</param>
        /// <param name="setEnabled">true - при установке часов, блокировать форму</param>
        public static void WaitForm(this Form frm, bool isWait, bool setEnabled = false)
        {
            if (isWait)
            {
                //Application.DoEvents();
                //GlobalsClient.MainForm.Cursor = Cursors.WaitCursor;
                //GlobalsClient.MainForm.SuspendLayout();
                // 20150518
                //if (!frm.UseWaitCursor)
                //{
                if (setEnabled)
                    frm.Enabled = false;
                frm.UseWaitCursor = true;
                frm.Cursor = Cursors.WaitCursor;
                frm.Refresh();
                //}
            }
            else
            {
                //GlobalsClient.MainForm.Cursor = Cursors.Default;
                if (frm.UseWaitCursor)
                    frm.UseWaitCursor = false;
                frm.Cursor = Cursors.Default;
                _setCursorDef(frm.Controls);
                if (setEnabled)
                    frm.Enabled = true;
                frm.Refresh();
            }
        }
        private static void _setCursorDef(Control.ControlCollection controls)
        {
            foreach (Control ctrl in controls)
            {
                ctrl.UseWaitCursor = false;
                ctrl.Cursor = Cursors.Default;
                _setCursorDef(ctrl.Controls);
            }
        }
        public static Point GetPosition(this Form frm, Control byControl)
        {
            if (byControl == null)
                return new Point();
            Point parentScreenPoint = byControl.PointToScreen(new Point(frm.Location.X, frm.Location.Y));
            if ((parentScreenPoint.Y + frm.Height) > Screen.FromControl(frm).WorkingArea.Height)
                parentScreenPoint.Y = Screen.FromControl(frm).WorkingArea.Height - frm.Height;
            return parentScreenPoint;
        }
    }
    #endregion

    #region Enum

    public static class EnumExtention
    {
        public static bool HasFlagEx(this Enum flag, params Enum[] flags)
        {
            return Array.IndexOf(flags, flag) != -1;
        }
        /// <summary>
        /// Текстовое значение из дескриптора
        /// </summary>
        /// <param name="enumerate"></param>
        /// <returns></returns>
        public static string ToStringX(this Enum enumerate)
        {
            var type = enumerate.GetType();
            var fieldInfo = type.GetField(enumerate.ToString());
            var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return (attributes.Length > 0) ? attributes[0].Description : enumerate.ToString();
            //MyEnum.CompositeValue.ToStringX()  // выдаст: Составное значение
        }
        /// <summary>
        /// Список значений дескрипторов для перечисления
        /// </summary>
        /// <param name="enumerate"></param>
        /// <returns></returns>
        public static List<string> Descriptions(this Enum enumerate)
        {
            var values = Enum.GetValues(enumerate.GetType());
            var descriptions = new List<string>(values.Length);

            foreach (var enumVal in values)
            {
                descriptions.Add(((Enum)enumVal).ToStringX());
            }

            return descriptions;
            //var descriptions = MyEnum.Single.Descriptions();
        }
        /// <summary>
        /// Вспомогательный метод, который сообщит вам, что перечисляемое значение определено в данном перечислении
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsDefined(this Enum value)
        {
            return Enum.IsDefined(value.GetType(), value);
        }
        /// <summary>
        /// Метод установки флага
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static Enum SetFlag<T>(this Enum flags, params Enum[] values)
        {
            if ((values?.Count() ?? 0) == 0)
                return flags;
            int cnt = values?.Count() ?? 0;
            for (int i = 0; i < cnt; i++)
            {
                if (!values[i].GetType().IsEquivalentTo(typeof(T)))
                {
                    throw new ArgumentException("Типы значения Enum и флагов не совпадают.");  //Enum value and flags types don't match.
                }
                // да, это уродливо, но, к сожалению, мы должны использовать промежуточное приведение
                flags = (Enum)Enum.ToObject(typeof(T), Convert.ToUInt64(values[i]) | Convert.ToUInt64(flags));
            }
            return flags;
        }
    }

    #endregion

    #region TextBox

    public static class TextBoxExtention
    {
        /// <summary>
        /// Проверяет на допустимость ввода символа для соответствия цифре
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="e"></param>
        /// <param name="allowTochka">Разрешен знак десятичной точки</param>
        /// <param name="scale">Количество знаков после запятой ( при allowTochka = true )</param>
        /// <param name="allowNegative">Разрешен ли знак минус ( отрицательные значения )</param>
        public static void IsCifiriInText(this TextBox txt, KeyPressEventArgs e, bool allowTochka = true, int scale = 6, bool allowNegative = false)
        {
            if (allowTochka && scale <= 0)
                allowTochka = false;
            //string expression = allowTochka ? @"[0,1,2,3,4,5,6,7,8,9,\b,',','.']" : @"[0,1,2,3,4,5,6,7,8,9,\b]";
            string expression = string.Format(@"[0123456789\b{0}{1}]", (allowTochka ? @",." : ""), (allowNegative ? @"-" : ""));
            string txtText = _getTextNotSelection(txt);
            if (allowNegative && System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), @"['\-']"))
            {
                e.Handled = txtText.Length != 0;
            }
            if (!e.Handled && e.KeyChar.ToString() == ".") e.KeyChar = char.Parse(",");
            if (!e.Handled && allowTochka && scale > 0)
            {
                e.Handled =
                    System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), @"[',','.']") &&
                    System.Text.RegularExpressions.Regex.Matches(txtText, @"[',','.']").Count > 0;
                // Если это не точка и точка уже есть, проверяем допустимость количества знаков после точки
                if (!e.Handled &&
                    !System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), @"[',','.','\b','-']") &&
                    System.Text.RegularExpressions.Regex.Matches(txtText, @"[',','.']").Count > 0)
                {
                    int pos = txtText.Trim().IndexOf(',');
                    if (pos < txtText.Trim().Length)
                    {
                        string s = txtText.Trim().Substring(pos + 1);
                        e.Handled = s.Trim().Length >= scale;
                    }
                }
            }
            if (!e.Handled)
                e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), expression);
        }
        /// <summary>
        /// Возвращает не выделенный текст из TextBox
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        private static string _getTextNotSelection(TextBox txt, int? startPos = null, int? endPos = null)
        {
            if (!startPos.HasValue) startPos = 0;
            if (!endPos.HasValue) endPos = txt.Text.Length;
            //int? beginSelectPos = null, finishSelectPos = null;
            if (startPos >= endPos) return string.Empty;
            //beginSelectPos = txt.SelectionStart > startPos || (txt.SelectionStart + txt.SelectionLength) < startPos ? startPos : (txt.SelectionStart + txt.SelectionLength);
            startPos = txt.SelectionStart > startPos || (txt.SelectionStart + txt.SelectionLength) < startPos ? startPos : (txt.SelectionStart + txt.SelectionLength);
            endPos = startPos < txt.SelectionStart ? txt.SelectionStart : txt.Text.Length;
            string res = string.Empty;
            res += txt.Text.Substring(startPos.Value, endPos.Value - startPos.Value) + _getTextNotSelection(txt, endPos.Value, txt.Text.Length);
            return res;
        }
        /// <summary>
        /// При нажатии Enter переводит фокус на следующий контрол
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="e"></param>
        public static void IsReturnInText(this TextBox txt, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                //txt.SelectNextControl(txt, true, true, false, true);
            }
        }
    }

    #endregion

    #region Control

    public static class ControlExtention
    {
        /// <summary>
        /// Изменение свойства DoubleBuffered контрола.
        /// </summary>
        /// <param name="c">Ссылка на контрол.</param>
        /// <param name="value">Значение, которое надо установить DoubleBuffered.</param>
        public static void SetDoubleBuffered(this Control c, bool value)
        {
            PropertyInfo pi = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic);
            if (pi != null)
            {
                pi.SetValue(c, value, null);

                MethodInfo mi = typeof(Control).GetMethod("SetStyle", BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.NonPublic);
                if (mi != null)
                {
                    mi.Invoke(c, new object[] { ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.DoubleBuffer, true });
                }

                mi = typeof(Control).GetMethod("UpdateStyles", BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.NonPublic);
                if (mi != null)
                {
                    mi.Invoke(c, null);
                }
            }
        }
        /// <summary>
        /// Включает / отключает песочные часы
        /// </summary>
        /// <param name="ctrl">Контрол</param>
        /// <param name="isWait">true - включить; false - отключить</param>
        public static void Wait(this Control ctrl, bool isWait)
        {
            if (isWait)
            {
                //Application.DoEvents();
                if (!ctrl.UseWaitCursor)
                {
                    ctrl.UseWaitCursor = true;
                    ctrl.Cursor = Cursors.WaitCursor;
                    ctrl.SuspendLayout();
                }
            }
            else
            {
                ctrl.UseWaitCursor = false;
                ctrl.Cursor = Cursors.Default;
                //Application.DoEvents();
                ctrl.ResumeLayout();
            }
        }
        public static Point GetPointRelativeToForm(this Control c)
        {
            //// Method 1: walk up the control tree
            //Point controlLocationRelativeToForm1 = new Point();
            //Control currentControl = c;
            //while (currentControl.Parent != null)
            //{
            //    controlLocationRelativeToForm1.Offset(currentControl.Left, currentControl.Top);
            //    currentControl = currentControl.Parent;
            //}

            //// Method 2: determine absolute position on screen of control and form, and calculate difference
            //Point controlScreenPoint = c.PointToScreen(Point.Empty);
            //Point formScreenPoint = PointToScreen(Point.Empty);
            //Point controlLocationRelativeToForm2 = controlScreenPoint - new Size(formScreenPoint);

            // Method 3: combine PointToScreen() and PointToClient()
            Point locationOnForm = c.FindForm().PointToClient(c.Parent.PointToScreen(c.Location));

            //// Theoretically they should be the same
            //Debug.Assert(controlLocationRelativeToForm1 == controlLocationRelativeToForm2);
            //Debug.Assert(locationOnForm == controlLocationRelativeToForm1);
            //Debug.Assert(locationOnForm == controlLocationRelativeToForm2);

            //return controlLocationRelativeToForm1;
            return locationOnForm;
        }
    }

    #endregion

    #region Exception

    public static class ExceptionExtention
    {
        /// <summary>
        /// Возвращает сообщения из вложенных исключений
        /// </summary>
        /// <param name="innerLevel"></param>
        /// <returns></returns>
        public static string GetAllInnerMessage(this Exception ex, bool trace = false)
        {
            if (ex.InnerException == null)
                return ex.Message;
            string result = string.Format("{0}\n", ex.Message);
            InnerEx(ref result, ex.InnerException, trace);
            return result;
        }
        private static void InnerEx(ref string msg, Exception ex, bool trace = false)
        {
            if (ex == null)
                return;
            string srcMsg = trace ? string.Format("StackTrace: {0}{1}", ex.StackTrace, Environment.NewLine) : string.Empty;
            msg += string.Format("{0}{1}{2}", ex.Message, Environment.NewLine, srcMsg);
            InnerEx(ref msg, ex.InnerException, trace);
        }
    }

    #endregion

    #region TabPages

    public static class TabPagesExtention
    {
        /// <summary>
        /// Возвращает массив TabPage
        /// </summary>
        /// <param name="tpc"></param>
        /// <returns></returns>
        public static TabPage[] GetTabPages(this TabControl tpc)
        {
            TabPage[] tbs = new TabPage[tpc.TabCount];
            for (int i = 0; i < tpc.TabCount; i++)
            {
                tbs[i] = tpc.TabPages[i];
            }
            return tbs;
        }
        /// <summary>
        /// Удаляет вкладку. Перед удалением нужно сохранить набор вкладок при помощи GetTabPages()
        /// </summary>
        /// <param name="tbc"></param>
        /// <param name="page"></param>
        public static void HidePage(this TabControl tbc, TabPage page)
        {
            tbc.TabPages.Remove(page);
        }
        /// <summary>
        /// Отображение вкладки
        /// </summary>
        /// <param name="tbc"></param>
        /// <param name="index"></param>
        /// <param name="page"></param>
        public static void ShowPage(this TabControl tbc, int index, TabPage page)
        {
            if (index < 0)
                index = 0;
            int maxIndex = tbc.TabCount;

            // Проверка на отсутствие вкладки в массиве
            for (int i = 0; i < tbc.TabPages.Count; i++)
            {
                if (tbc.TabPages[i].Equals(page))
                {
                    tbc.SelectedTab = page;
                    return;
                }
            }

            if (index >= maxIndex)
                tbc.TabPages.Add(page);
            else
            {
                TabPage[] tmpTabs = tbc.GetTabPages();
                tbc.TabPages.Clear();
                for (int i = 0; i < maxIndex; i++)
                {
                    if (i == maxIndex)
                    {
                        tbc.TabPages.Add(page);
                        tbc.TabPages[i].Focus();
                    }
                    else if (i > maxIndex)
                        tbc.TabPages.Add(tmpTabs[i - 1]);
                    else
                        tbc.TabPages.Add(tmpTabs[i]);
                }
            }
            tbc.SelectedIndex = index;
        }
    }

    #endregion

    #region Common

    public static class CommonExtention
    {
        /// <summary>
        /// Является ли значение значением по умолчанию
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public static bool IsPropertyHasDefaultValue(this object obj, string property)
        {
            if (obj == null || string.IsNullOrEmpty(property))
                return false;

            // проверим наличие нужного свойства
            PropertyInfo pi = obj.GetType().GetProperty(property);
            if (pi == null)
                return false;

            // получим экземпляр DefaultValueAttribute для объектов этого типа
            AttributeCollection attributes =
               TypeDescriptor.GetProperties(obj)[property].Attributes;
            DefaultValueAttribute myAttribute =
               (DefaultValueAttribute)attributes[typeof(DefaultValueAttribute)];

            if (myAttribute == null)
                return false;

            // сравним значения
            object v1 = myAttribute.Value;
            object v2 = pi.GetValue(obj, null);
            if (v1.ToString() != v2.ToString())
                return false;

            return true;
        }
        /// <summary>
        /// Выполняет "глубокое" копирование объекта. Последний должен быть сериализуем!
        /// </summary>
        /// <param name="value">Оригинал</param>
        /// <returns>Копия объекта</returns>
        public static T DeepCopy<T>(T value) where T : class
        {
            var f = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

            using (var stream = new System.IO.MemoryStream())
            {
                f.Serialize(stream, value);
                stream.Position = 0;
                return (T)f.Deserialize(stream);
            }
        }
        /// <summary>
        /// Проверяет вхождение значений
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool IN(this int val, params int[] values)
        {
            for (int i = 0; i < values.GetLength(0); i++)
                if (values[i] == val)
                    return true;
            return false;
        }
        /// <summary>
        /// Проверяет вхождение значений
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool IN(this short val, params short[] values)
        {
            for (int i = 0; i < values.GetLength(0); i++)
                if (values[i] == val)
                    return true;
            return false;
        }
        /// <summary>
        /// Проверяет вхождение значений
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool IN(this byte val, params byte[] values)
        {
            for (int i = 0; i < values.GetLength(0); i++)
                if (values[i] == val)
                    return true;
            return false;
        }
        /// <summary>
        /// Проверяет вхождение значений
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool IN(this decimal val, params decimal[] values)
        {
            for (int i = 0; i < values.GetLength(0); i++)
                if (values[i] == val)
                    return true;
            return false;
        }
        /// <summary>
        /// Проверяет вхождение значений
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool IN(this string val, params string[] values)
        {
            for (int i = 0; i < values.GetLength(0); i++)
                if (values[i].Equals(val))
                    return true;
            return false;
        }

        public static bool IN<T> (this T val, params T[] values)
        {
            for (int i = 0; i < values.GetLength(0); i++)
                if (values[i].Equals(val))
                    return true;
            return false;
        }
    }

    #endregion

    #region ErrorProvider

    public static class ErrorProviderExtention
    {
        /// <summary>
        /// Если true, то проверка не пройдена
        /// </summary>
        /// <param name="errProv"></param>
        /// <param name="sender"></param>
        /// <returns></returns>
        public static bool Control_Validating(this ErrorProvider errProv, object sender)
        {
            Control c = (Control)sender;
            if ((c is TextBox || c is MaskedTextBox) && string.IsNullOrWhiteSpace(c.Text))
            {
                errProv.SetError(c, "Введите значение");
                return true;
            }
            else if (c is ComboBox)
            {
                ComboBox cbx = c as ComboBox;
                if (((cbx.DataSource == null) && (cbx.Items.Count != 0) && ((cbx.SelectedItem == null) || ((cbx.SelectedItem is string) && string.IsNullOrWhiteSpace(Convert.ToString(cbx.SelectedItem))))) ||
                    ((cbx.DataSource != null) && (cbx.SelectedValue is int || cbx.SelectedValue is short || cbx.SelectedValue is byte || cbx.SelectedValue == null) && Convert.ToInt32(cbx.SelectedValue) <= 0) ||
                    (cbx.DataSource == null) && (cbx.Items.Count == 0))
                {
                    errProv.SetError(c, "Выберите значение из списка");
                    return true;
                }
                else
                    errProv.SetError(c, "");
            }
            else if ((c is Label) && string.IsNullOrWhiteSpace(((Label)c).Text))
            {
                errProv.SetError(c, "Введите значение");
                return true;
            }
            else
                errProv.SetError(c, "");
            return false;
        }
    }

    #endregion

    #region Decimal

    public static class DecimalEx
    {
        public static decimal ParseEx(this string val)
        {
            decimal result;
            val = val.Replace(',', '.');
            bool res = decimal.TryParse(val, NumberStyles.Any, CultureInfo.InvariantCulture, out result);
            if (res)
                return result;
            throw new Exception("Ошибка преобразавания строки в число");
        }
    }

    #endregion

    #region GroupBox

    public static class GroupBoxEx
    {
        /// <summary>
        /// Наименование выбранного радиокнопки в группе
        /// </summary>
        /// <param name="grp"></param>
        /// <returns></returns>
        public static string GetCheckedRadioButtonName(this GroupBox grp)
        {
            //IEnumerable<RadioButton> selRb=from ctrl in grp.Controls
            //                               where ctrl is RadioButton && ((RadioButton)ctrl).Checked
            //                               select (RadioButton) ctrl;
            foreach (RadioButton r in grp.Controls)
            {
                if (r.Checked)
                    return r.Name;
            }
            return string.Empty;
        }
    }

    #endregion

    #region Reader

    public static class ReaderEx
    {
        public static bool HasColumn(this IDataReader reader, string column)
        {
            //for (int i = 0; i < reader.FieldCount; i++)
            //{
            //    if (reader[i].Equals(column))
            //        return true;
            //}
            //return false;
            try
            {
                int i = reader[column].GetHashCode();
                return true;
            }
            catch { return false; }
        }
    }

    #endregion

    #region ErrProv

    public static class DataErrorEx
    {
        /// <summary>
        /// Наличие ошибок в интерфейсе IDataErrorInfo
        /// </summary>
        /// <param name="biz"></param>
        /// <returns></returns>
        public static bool HasError(this IDataErrorInfo biz)
        {
            //return !string.IsNullOrEmpty(((IDataErrorInfo)biz).Error);
            return biz.GetOneError() != "";
        }
        /// <summary>
        /// Наличие ошибок в интерфейсе IDataErrorInfo в передаваемом свойстве
        /// </summary>
        /// <param name="biz"></param>
        /// <param name="propName">Свойство подлежащее проверке на наличие ошибки</param>
        /// <returns></returns>
        public static bool HasError(this IDataErrorInfo biz, string propName)
        {
            return !string.IsNullOrEmpty(((IDataErrorInfo)biz)[propName]);
        }
        /// <summary>
        /// Возвращает первую попавшуюся ошибку
        /// </summary>
        /// <param name="mpw"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        public static string GetOneError(this IDataErrorInfo biz)
        {
            if (biz != null)
            {
                if (biz.Error.Equals(string.Empty))
                {
                    foreach (PropertyInfo p in biz.GetType().GetProperties())
                    {
                        if (biz.HasError(p.Name))
                            return biz[p.Name];
                    }
                }
                else
                    return biz.Error;
            }
            return string.Empty;
        }
    }

    #endregion

    #region Thread

    public static class ThreadUtil
    {
        public static bool IsBlocking(Thread thread)
        {
            bool blocked = (thread.ThreadState & ThreadState.WaitSleepJoin) != 0;
            return blocked;
        }
    }

    #endregion
}
