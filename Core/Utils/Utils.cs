using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Reflection;
using TkwEF.Core.Extentions;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Globalization;
using System.Management;
using System.Collections;
using System.Linq;
using System.Diagnostics;

namespace TkwEF.Core
{
    public static partial class UtilityCore
    {
        #region Language

        public class SystemLang
        {
            #region Импорт функций

            [DllImport("user32.dll")]
            private static extern long LoadKeyboardLayout(
                string pwszKLID, // передаём идентификатор местных специфик
                uint Flags // и его настройки
                );
            [DllImport("user32.dll")]
            //[out] строка, получающая имя локального идентификатора
            private static extern long GetKeyboardLayoutName(System.Text.StringBuilder pwszKLID);

            #endregion

            #region Const

            const uint KLF_ACTIVATE = 1; //активизируем раскладку
            const int KL_NAMELENGTH = 9; // длина буфера клавиатуры
            const string LANG_EN_US = "00000409";
            const string LANG_HE_IL = "0000040d";

            #endregion
            /// <summary>
            /// Установить английский язык
            /// </summary>
            public static void SetEnglish()
            {
                //загружаем и активизируем раскладку для текущей нити
                LoadKeyboardLayout(LANG_EN_US, KLF_ACTIVATE);
            }
            /// <summary>
            /// Возвращает выбранную раскладку клавиатуры
            /// </summary>
            /// <returns></returns>
            public static string GetSystemLang()
            {
                System.Text.StringBuilder name = new System.Text.StringBuilder(KL_NAMELENGTH);
                GetKeyboardLayoutName(name);
                switch (name.ToString())
                {
                    case LANG_EN_US:
                        return "En";
                    default:
                        return "##";
                }
            }

            public static void SetRussia()
            {
                InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new CultureInfo("ru-RU"));
            }

            public static string GetSysLang()
            {
                return InputLanguage.CurrentInputLanguage.Culture.TwoLetterISOLanguageName;
            }
        }

        #endregion

        #region Proccess

        public static class ProcessEx
        {
            /// <summary>
            /// Возвращает количество запущенных процессов
            /// </summary>
            /// <returns></returns>
            public static int GetThreadCount()
            {
                using (var currentProcess = System.Diagnostics.Process.GetCurrentProcess())
                {
                    return currentProcess.Threads
                        .OfType<System.Diagnostics.ProcessThread>()
                        .Where(t => t.ThreadState == System.Diagnostics.ThreadState.Running)
                        .Count();
                }
            }
        }

        #endregion

        #region Common

        /// <summary>
        /// Клонирование через сериализацию
        /// </summary>
        /// <param name="something"></param>
        /// <returns></returns>
        public static object Clone(object something)
        {
            XmlSerializer serializer = new XmlSerializer(something.GetType());
            using (MemoryStream tempStream = new MemoryStream(1024))
            {
                serializer.Serialize(tempStream, something);
                tempStream.Seek(0, SeekOrigin.Begin);
                return serializer.Deserialize(tempStream);
            }
        }
        #region Пример использования Clone
        //public override object Clone()
        //{
        //    MyType res = (MyType)Cloner.Clone(this);
        //    //set values of non-persistent fields
        //    res.SetContext(myContext);
        //    return res;
        //}
        #endregion

        /// <summary>
        /// Клонирование через рефлексию
        /// </summary>
        /// <param name="something"></param>
        /// <returns></returns>
        public static object CloneReflection(object something)
        {
            object newObject = Activator.CreateInstance(something.GetType());
            PropertyInfo[] propertyInfos = something.GetType().GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                if (propertyInfo.PropertyType.IsGenericType)
                {
                    if (propertyInfo.PropertyType.GetInterface("IList", true) != null)
                    {
                        IList oldList = propertyInfo.GetValue(something, null) as IList;
                        if (oldList != null && oldList.Count > 0 &&
                    oldList[0].GetType().GetInterface("ICloneable", true) != null)
                        {
                            IList newList = (IList)propertyInfo.GetValue(newObject, null);
                            foreach (object obj in oldList)
                            {
                                ICloneable clone = (ICloneable)obj;
                                newList.Add(clone.Clone());
                            }
                        }
                        else
                        {
                            propertyInfo.SetValue(newObject, oldList, null);
                        }
                    }
                    else if (propertyInfo.PropertyType.GetInterface("IDictionary", true) != null)
                    {
                        IDictionary oldDic =
                          propertyInfo.GetValue(something, null) as IDictionary;
                        if (oldDic != null && oldDic.Count > 0 &&
                       oldDic[0].GetType().GetInterface("ICloneable", true) != null)
                        {
                            IDictionary newDic =
                             (IDictionary)propertyInfo.GetValue(newObject, null);
                            foreach (DictionaryEntry entry in oldDic)
                            {
                                ICloneable clone = (ICloneable)entry.Value;
                                newDic[entry.Key] = clone.Clone();
                            }
                        }
                        else
                        {
                            propertyInfo.SetValue(newObject, oldDic, null);
                        }
                    }
                    //Clone IClonable object
                    else if (propertyInfo.GetType().GetInterface("ICloneable", true) != null)
                    {
                        ICloneable clone = (ICloneable)propertyInfo.GetValue(something, null);
                        propertyInfo.SetValue(newObject, clone.Clone(), null);
                    }
                    else
                    {
                        if (propertyInfo.CanWrite)
                            propertyInfo.SetValue(
                             newObject, propertyInfo.GetValue(something, null), null);
                    }
                }
                else
                {
                    //Clone IClonable object
                    if (propertyInfo.GetType().GetInterface("ICloneable", true) != null)
                    {
                        ICloneable clone = (ICloneable)propertyInfo.GetValue(something, null);
                        propertyInfo.SetValue(newObject, clone.Clone(), null);
                    }
                    else
                    {
                        if (propertyInfo.CanWrite)
                            propertyInfo.SetValue(
                             newObject, propertyInfo.GetValue(something, null), null);
                    }
                }
            }
            return newObject;
        }

        #endregion

        public static class ByEnum
        {
            /// <summary>
            /// Входит ли в подмножество
            /// </summary>
            /// <param name="flag"></param>
            /// <param name="flags"></param>
            /// <returns></returns>
            public static bool InEnum(Enum flag, params Enum[] flags)
            {
                return Array.IndexOf(flags, flag) != -1;
            }
            //public static bool InEnum(Enum flag, Enum flags)
            //{
            //    return flags.HasFlag(flag);
            //}

            //public static string GetStringXByValue(Enum flag, int value)
            //{
            //    string Type=flag.GetType().ToString();
            //    return ((Type.GetType(Type))Enum.Parse(Type.GetType(Type), "2")).ToStringX();
            //    return (
            //        (Type.GetType(Type))
            //        Enum.Parse(
            //        Type.GetType(Type), "2"
            //        )
            //        ).ToStringX();
            //}
        }

        /// <summary>
        /// Статический класс для форм
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static class ByForm<T> where T : Form, new()
        {
            /// <summary>
            /// Если форма открыта, возвращает ссылку на экземпляр, иначе null
            /// </summary>
            /// <returns></returns>
            public static T GetOpenForm()
            {
                foreach (Form f in Application.OpenForms)
                {
                    if (f.GetType() == typeof(T))
                    {
                        return (T)f;
                    }
                    else if (f.ParentForm != null ? f.ParentForm.GetType() == typeof(T) : false)
                    {
                        return (T)f;
                    }
                }
                return null;
            }

            /// <summary>
            /// Открывает дочернюю форму и возвращает true, если форма не была открыта ранее, иначе вернет false
            /// </summary>
            /// <param name="mdiParent"></param>
            /// <returns></returns>
            public static T OpenForm(Form mdiParent, bool isModal = false)
            {
                try
                {
                    mdiParent.WaitForm(true);
                    T frm = GetOpenForm();
                    if (frm == null)
                    {
                        frm = new T();
                        if (isModal)
                        {
                            if (mdiParent == null)
                                frm.ShowDialog();
                            else
                                frm.ShowDialog(mdiParent);
                        }
                        else
                        {
                            if (mdiParent != null)
                                frm.MdiParent = mdiParent;
                            frm.Show();
                        }
                        return frm;
                    }
                    else
                    {
                        if (frm.Visible)
                        {
                            frm.Select();
                        }
                    }
                }
                catch (Exception) { throw; }
                finally
                {
                    mdiParent.WaitForm(false);
                }
                return null;
            }

            /// <summary>
            /// Открывает форму и возвращает true, если форма не была открыта ранее, иначе вернет false
            /// </summary>
            /// <returns></returns>
            public static bool IsOpenForm()
            {
                foreach (Form frm in Application.OpenForms)
                {
                    if (typeof(T).Equals(frm.GetType()))
                        return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Не доделан
        /// </summary>
        /// <returns></returns>
        //public static class Menegements
        //{
        //    private static ManagementObjectCollection _managementObj = null;
        //    private static ManagementObjectCollection managementObjCollection
        //    {
        //        get
        //        {
        //            if (_managementObj == null)
        //                _managementObj = _getManagement();
        //            return _managementObj;
        //        }
        //    }

        //    public static class NetWorkObject : ManagementObject
        //    {
        //        #region Fields

        //        public string Caption
        //        {
        //            get
        //            {
        //                return (string)this["Caption"];
        //            }
        //        }
        //        public string Comment
        //        {
        //            get
        //            {
        //                return (string)this["Comment"];
        //            }
        //        }
        //        public string ConnectionState
        //        {
        //            get
        //            {
        //                return (string)this["ConnectionState"];
        //            }
        //        }
        //        public string ConnectionType
        //        {
        //            get
        //            {
        //                return (string)this["ConnectionType"];
        //            }
        //        }
        //        public string Description
        //        {
        //            get
        //            {
        //                return (string)this["Description"];
        //            }
        //        }
        //        public string DisplayType
        //        {
        //            get
        //            {
        //                return (string)this["DisplayType"];
        //            }
        //        }
        //        public DateTime InstallDate
        //        {
        //            get
        //            {
        //                return (DateTime)this["InstallDate"];
        //            }
        //        }
        //        public string LocalName
        //        {
        //            get
        //            {
        //                return (string)this["LocalName"];
        //            }
        //        }
        //        public string Name
        //        {
        //            get
        //            {
        //                return (string)this["Name"];
        //            }
        //        }
        //        public string Persistent
        //        {
        //            get
        //            {
        //                return (string)this["Persistent"];
        //            }
        //        }
        //        public string ProviderName
        //        {
        //            get
        //            {
        //                return (string)this["ProviderName"];
        //            }
        //        }
        //        public string RemoteName
        //        {
        //            get
        //            {
        //                return (string)this["RemoteName"];
        //            }
        //        }
        //        public string RemotePath
        //        {
        //            get
        //            {
        //                return (string)this["RemotePath"];
        //            }
        //        }
        //        public string ResourceType
        //        {
        //            get
        //            {
        //                return (string)this["ResourceType"];
        //            }
        //        }
        //        public string Status
        //        {
        //            get
        //            {
        //                return (string)this["Status"];
        //            }
        //        }
        //        public string UserName
        //        {
        //            get
        //            {
        //                return (string)this["UserName"];
        //            }
        //        }
        //        #endregion
        //    }
        //    private static ManagementObjectCollection _getManagement()
        //    {
        //        ManagementClass managementClass = new ManagementClass("Win32_NetworkConnection");
        //        ManagementObjectCollection managementObj = managementClass.GetInstances();
        //        for (int i = 0; i < managementObj.Count; i++)
        //        {

        //        }
        //        return managementObj;

        //        //foreach (ManagementObject mo in managementObj)
        //        //{
        //        //    Console.WriteLine("AccessMask:\t{0}", mo["AccessMask"]);
        //        //    Console.WriteLine("Caption:\t{0}", mo["Caption"]);
        //        //    Console.WriteLine("Comment:\t{0}", mo["Comment"]);
        //        //    Console.WriteLine("ConnectionState:\t{0}", mo["ConnectionState"]);
        //        //    Console.WriteLine("ConnectionType:\t{0}", mo["ConnectionType"]);
        //        //    Console.WriteLine("Description:\t{0}", mo["Description"]);
        //        //    Console.WriteLine("DisplayType:\t{0}", mo["DisplayType"]);
        //        //    Console.WriteLine("InstallDate:\t{0}", mo["InstallDate"]);
        //        //    Console.WriteLine("LocalName:\t{0}", mo["LocalName"]);
        //        //    Console.WriteLine("Name:\t{0}", mo["Name"]);
        //        //    Console.WriteLine("Persistent:\t{0}", mo["Persistent"]);
        //        //    Console.WriteLine("ProviderName:\t{0}", mo["ProviderName"]);
        //        //    Console.WriteLine("RemoteName:\t{0}", mo["RemoteName"]);
        //        //    Console.WriteLine("RemotePath:\t{0}", mo["RemotePath"]);
        //        //    Console.WriteLine("ResourceType:\t{0}", mo["ResourceType"]);
        //        //    Console.WriteLine("Status:\t{0}", mo["Status"]);
        //        //    Console.WriteLine("UserName:\t{0}", mo["UserName"]);
        //        //    Console.WriteLine("-----------------------------------------");
        //        //}
        //        //Console.ReadLine();
        //    }
        //}

        #region By Internet

        public sealed class ByArray
        {
            static T? Max<T>(IEnumerable<T> values) where T : struct, IComparable<T>
            {
                return MinMaxImpl(values, delegate (T left, T right)
                { return right.CompareTo(left); });
            }

            static T? Min<T>(IEnumerable<T> values) where T : struct, IComparable<T>
            {
                return MinMaxImpl(values, delegate (T left, T right)
                { return left.CompareTo(right); });
            }

            static T? MinMaxImpl<T>(IEnumerable<T> values, Comparison<T> comparison) where T : struct, IComparable<T>
            {
                T? v = null;

                foreach (T value in values)
                    if (v == null || comparison(v.Value, value) > 0)
                        v = value;

                return v;
            }
        }

        /// generic Singleton<T> (потокобезопасный с использованием generic-класса и с отложенной инициализацией)

        /// <typeparam name="T">Singleton class</typeparam>
        public class Singleton<T> where T : class
        {
            /// Закрытый конструктор по умолчанию необходим для того, чтобы
            /// предотвратить создание экземпляра класса Singleton
            private Singleton() { }

            /// Фабрика используется для отложенной инициализации экземпляра класса
            private sealed class SingletonCreator<S> where S : class
            {
                //Используется Reflection для создания экземпляра класса без публичного конструктора
                private static readonly S instance = (S)typeof(S).GetConstructor(
                            BindingFlags.Instance | BindingFlags.NonPublic,
                            null,
                            new Type[0],
                            new ParameterModifier[0]).Invoke(null);

                public static S CreatorInstance
                {
                    get { return instance; }
                }
            }

            public static T Instance
            {
                get { return SingletonCreator<T>.CreatorInstance; }
            }

        }

        /// Использование Singleton
        //public class TestClass : Singleton<TestClass>
        //{
        //    private TestClass() { }

        //    public string TestProc()
        //    {
        //        return "Hello World";
        //    }
        //}

        #endregion

        public static class ByGrid<T> where T : BLL.BIZ_Base
        {
            /// <summary>
            /// Возвращает объект из таблицы
            /// </summary>
            /// <param name="grd"></param>
            /// <returns></returns>
            public static T GetFirstGridRow(DataGridView grd)
            {
                T rr = null;
                try
                {
                    if (grd.DataSource == null || ((grd.DataSource is BindingSource) && (grd.DataSource as BindingSource).Count == 0)) return null;
                    DataGridViewRow row = grd.GetFirstSelectionRow();
                    if (row != null && row.Index >= 0 && (row.DataBoundItem as T) != null)
                        rr = row.DataBoundItem as T;
                }
                catch (IndexOutOfRangeException) { }
                catch (Exception) { throw; }
                return rr;
            }
        }

        public static class ByBsSrc<T> where T : BLL.BIZ_Base
        {
            /// <summary>
            /// Возвращает признак наличия данных
            /// </summary>
            /// <param name="bs"></param>
            /// <returns></returns>
            public static bool IsNull(BindingSource bs)
            {
                return bs.DataSource == null || !(bs.DataSource is BLL.BindingListView<T>) || bs.Count == 0;
            }
        }

        public static class BizEx<T> where T : BLL.BIZ_Base
        {
            /// <summary>
            /// Есть ли измененные поля
            /// </summary>
            /// <param name="biz"></param>
            /// <param name="obj"></param>
            /// <returns></returns>
            public static bool IsDirty(T biz, T obj)
            {
                if (biz.Id != obj.Id) throw new Exception("Невозможно сравнить разные объекты");
                foreach (PropertyInfo p in typeof(T).GetProperties())
                {
                    if (!p.GetValue(biz, null).Equals(p.GetValue(obj, null)))
                        return true;
                }
                return false;
            }
            /// <summary>
            /// Возвращает список контролов, находящимся в контроле
            /// </summary>
            /// <param name="ctrl"></param>
            /// <returns></returns>
            public static List<T> GetControls(Control ctrl)
            {
                List<T> res = new List<T>();
                if (!ctrl.HasChildren)
                    return res;
                foreach (Control c in ctrl.Controls)
                {
                    if (c.HasChildren)
                        res.AddRange(GetControls(c));
                    else
                    {
                        if (c is T)
                            res.Add(c as T);
                    }
                }
                return res;
            }
        }
        /// <summary>
        /// Возвращает признак наличия значения в списке
        /// </summary>
        /// <param name="src"></param>
        /// <param name="findVal"></param>
        /// <param name="fieldVal"></param>
        public static bool HasValue(IEnumerable<object> src, string findVal, out string fieldVal)
        {
            fieldVal = string.Empty;
            try
            {
                foreach (var item in src)
                {
                    var fields = item.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
                    int cnt = fields.Count();
                    for (int i = 0; i < cnt; i++)
                    {
                        if ((fields[i].GetValue(item, null) ?? "").ToString().IndexOf(findVal) >= 0)
                        {
                            //Debug.Assert(false, fields[i].Name);
                            string idVal = fields.FirstOrDefault(w => w.Name == "Id")?.GetValue(item, null)?.ToString() ?? @"Н/Д";
                            fieldVal = string.Format("Id={0}; {1}={2};", idVal, fields[i].Name, (fields[i].GetValue(item, null) ?? "").ToString());
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception)
            {
                //Trace.Fail(ex.Message, ex.GetAllMessages());
                return false;
            }
        }

    }
}
