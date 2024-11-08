using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Collections;

namespace TkwEF.Core.Extentions
{
    public static class GridViewExtention
    {
        /// <summary>
        /// Поиск указанного значения для свойства с указанным именем.
        /// </summary>
        /// <param name="setPosition">Нужно ли производить позиционирование</param>
        /// <param name="nomerPosition">Позиционироваться на позицию, если значение не найдено</param>
        /// <returns>Позиция найденного значения. Если найти не удалось, то -1.</returns>
        public static int FindX(this BindingSource bs, string PropertyName, object PropertyValue, bool setPosition = true, int? nomerPosition = null)
        {
            if ((bs.DataSource == null) || (PropertyValue == null)) return -1;
            //if (bs.DataSource is IBindingList) return bs.Find(PropertyName, PropertyValue);
            //else
            //{
            for (int pos = 0; pos < bs.Count; ++pos)
            {
                PropertyDescriptorCollection props = TypeDescriptor.GetProperties(bs.List[pos]);
                //object c = bs.List[Pos].PropertyGet(PropertyName);
                object c = props[PropertyName].GetValue(bs.List[pos]);
                props = null;
                //object m = bs.List[Pos].GetType().GetProperty(PropertyName);
                if ((c != null) && (c.ToString() == PropertyValue.ToString()))
                {
                    if (setPosition)
                        return bs.SetPosition(pos);
                    else
                        return pos;
                }
            }
            //}
            if(setPosition && nomerPosition.HasValue)
                return bs.SetPosition(nomerPosition.Value);
            return -1;
        }
        /// <summary>
        /// Рисование номеров строк
        /// </summary>
        /// <param name="grd"></param>
        /// <param name="e"></param>
        public static void DrawNumberRows(this DataGridView grd, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(grd.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString("g", System.Globalization.CultureInfo.CurrentUICulture),
                    e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
            //grd.Rows[e.RowIndex].HeaderCell.Style.SelectionBackColor = Color.Sienna;
            //grd.Rows[e.RowIndex].HeaderCell.Style.SelectionForeColor = SystemColors.Info;
        }
        /// <summary>
        /// Устанавливает курсор на текущую позицию
        /// </summary>
        /// <param name="bs"></param>
        /// <param name="pos"></param>
        public static int SetPosition(this BindingSource bs, int pos)
        {
            if (bs.DataSource == null || bs.Count == 0) return -1;
            try
            {
                int res = bs.Count > pos ? pos : bs.Count - 1;
                bs.Position = res;
                return res;
            }
            catch { return -1; }
        }
        /// <summary>
        /// Проверяет на наличие ошибки в таблице по колонке
        /// </summary>
        /// <param name="grd"></param>
        /// <param name="column">Колонка по которой проводится проверка</param>
        /// <param name="selected">Нужно ли выделить ячейку при обнаружении ошибки</param>
        /// <returns></returns>
        public static bool HasError(this DataGridView grd, int column, bool selected)
        {
            for (int i = 0; i < grd.Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(grd.Rows[i].Cells[column].ErrorText))
                {
                    if(selected)
                        grd.Rows[i].Cells[column].Selected = true;
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Проверяет на наличие ошибки в таблице по строке
        /// </summary>
        /// <param name="grd"></param>
        /// <param name="row">В строке</param>
        /// <param name="column">В колонке</param>
        /// <param name="selected">Нужно ли выделить ячейку при обнаружении ошибки</param>
        /// <returns></returns>
        public static bool HasError(this DataGridView grd, int row, int? column, bool selected)
        {
            bool hasColumn = (column.HasValue && column.Value >= 0);
            for (int i = (hasColumn ? column.Value : 0); i < (hasColumn ? column.Value + 1 : grd.Columns.Count); i++)
            {
                if (!string.IsNullOrEmpty(grd.Rows[row].Cells[i].ErrorText))
                {
                    if (selected)
                        grd.Rows[row].Cells[i].Selected = true;
                    return true;
                }

            }
            return false;
        }
        /// <summary>
        /// Проверяет на наличие ошибки в таблице по всем колонкам
        /// </summary>
        /// <param name="grd"></param>
        /// <param name="selected">Нужно ли выделить ячейку при обнаружении ошибки</param>
        /// <returns></returns>
        public static bool HasError(this DataGridView grd, bool selected)
        {
            for (int i = 0; i < grd.Rows.Count; i++)
            {
                for (int y = 0; y < grd.Columns.Count; y++)
                {
                    if (!string.IsNullOrEmpty(grd.Rows[i].Cells[y].ErrorText))
                    {
                        if(selected)
                            grd.Rows[i].Cells[y].Selected = true;
                        return true;
                    }

                }
            }
            return false;
        }
        /// <summary>
        /// Возвращает первую выделенную строку
        /// </summary>
        /// <param name="grd"></param>
        /// <returns></returns>
        public static DataGridViewRow GetFirstSelectionRow(this DataGridView grd)
        {
            if (grd.DataSource == null) return null;
            else if(grd.DataSource is BindingSource)
            {
                BindingSource bs = (BindingSource)grd.DataSource;
                if (bs.Count == 0 || bs.Position < 0)
                    return null;
            }
            if (grd.SelectedRows.Count != 0)
                return grd.SelectedRows[0];
            else if (grd.SelectedCells.Count != 0)
                return grd.Rows[grd.SelectedCells[0].RowIndex];
            else return null;
        }
        /// <summary>
        /// Возвращает строку переданной таблицы
        /// </summary>
        /// <param name="grd"></param>
        /// <returns></returns>
        public static DataGridViewRow GetFirstSelectionRow(this DataGridView grd, DataGridViewCellEventArgs e)
        {
            if (e == null || e.RowIndex < 0) return null;
            return grd.Rows[e.RowIndex];
        }
        /// <summary>
        /// Возвращает строку переданной таблицы
        /// </summary>
        /// <param name="grd"></param>
        /// <returns></returns>
        public static DataGridViewRow GetFirstSelectionRow(this DataGridView grd, DataGridViewCellMouseEventArgs e)
        {
            if (e == null || e.RowIndex < 0) return null;
            return grd.Rows[e.RowIndex];
        }

        public static void SelectDataGridViewCell(this DataGridView grd, int rowIndex, int cellIndex)
        {
            DataGridViewCell cell = grd.Rows[rowIndex].Cells[cellIndex];
            grd.CurrentCell = cell;
            grd.CurrentCell.Selected = true;
        }

        public static void SelectDataGridViewRow(this DataGridView grd, int rowNum)
        {
            SelectDataGridViewCell(grd, rowNum, 0);
        }
        /// <summary>
        /// Возвращает координаты ячейки на экране ( с учетом или без ширины и высоты ячейки )
        /// </summary>
        /// <param name="cell">Ячейки</param>
        /// <param name="withWidth">Учитывать ширину ячейки</param>
        /// <param name="withHeigth">Учитывать высоту ячейки</param>
        /// <returns></returns>
        public static Point GetPointCellByScreen(this DataGridViewCell cell, bool withWidth = false, bool withHeigth = false)
        {
            Point pcell = cell.DataGridView.GetCellDisplayRectangle(cell.ColumnIndex, cell.RowIndex, true).Location;
            Point pgrid = cell.DataGridView.RectangleToScreen(cell.DataGridView.DisplayRectangle).Location;
            int widthCell = withWidth ? cell.OwningColumn.Width : 0;
            int heigthCell = withHeigth ? cell.OwningRow.Height : 0;
            return new Point(pcell.X + pgrid.X + widthCell, pcell.Y + pgrid.Y + heigthCell);
        }
    }

    public static class GridViewExtention<T> where T : class
    {
        /// <summary>
        /// Возвращает первую выделенную строку
        /// </summary>
        /// <param name="grd"></param>
        /// <returns></returns>
        public static T GetFirstSelectionRow(DataGridView grd)
        {
            if (grd.DataSource == null) return null;
            else if (grd.DataSource is BindingSource)
            {
                BindingSource bs = (BindingSource)grd.DataSource;
                if (bs.Count == 0 || bs.Position < 0 || bs.Current == null)
                    return null;
                return bs.Current as T;
            }
            else
            {
                if (grd.SelectedRows.Count != 0)
                    return grd.SelectedRows[0] as T;
                else if (grd.SelectedCells.Count != 0)
                    return grd.Rows[grd.SelectedCells[0].RowIndex] as T;
                else return null;
            }
        }
    }
}
