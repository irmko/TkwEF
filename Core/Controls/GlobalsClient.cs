using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing;
using System.Diagnostics;

namespace TkwEF.Core.UI
{
    /// <summary>
    /// GlobalsClient
    /// </summary>
    public abstract class GlobalClient
    {
        #region Properties

        /// <summary>
        /// Стока подключения
        /// </summary>
        public static string ConnectionString { get; set; }

        /// <summary>
        /// Соединение с БД
        /// </summary>
        public static SqlConnection Connection { get; set; }

        /// <summary>
        /// Путь сервера
        /// </summary>
        public static string ServerPath
        {
            get { return @"\\appserv\base_supply$\"; }
        }

        /// <summary>
        /// Название файла
        /// </summary>
        public static string FileName
        {
            get { return Path.GetFileName(Application.ExecutablePath); }
        }

        /// <summary>
        /// Текущая сборка
        /// </summary>
        public static AssemblyName AssemblyNameCurrent
        {
            get { return AssemblyName.GetAssemblyName(Application.ExecutablePath); }
        }

        private static ClientProperties _properties = null;
        /// <summary>
        /// Свойства клиента
        /// </summary>
        public static ClientProperties Properties
        {
            get
            {
                if (_properties == null)
                    _properties = ClientProperties.Instance;
                return _properties;
            }
        }

        #endregion

        #region ctor

        protected GlobalClient() { }

        #endregion
    }

    public class ClientProperties
    {
        #region ctor

        private static ClientProperties _instance = null;
        public static ClientProperties Instance
        {
            get
            {
                if (_instance == null)
                    _instance = Activator.CreateInstance<ClientProperties>();
                return _instance;
            }
        }

        #endregion

        public readonly Color MenuBackColor = Color.Sienna;
        public readonly Color MenuForeColor = Color.WhiteSmoke;

        public Color CaptionColor = Color.Sienna;
        public Color GridSelectionBackColorCell = Color.Sienna;
        public Color GridSelectionForeColorCell = SystemColors.Info;
        public Color GridSelectionBackColorRow = Color.Sienna;

        public Color GridCellBackColorReadOnly = SystemColors.Info;
        public Color GridCellBackColorNotReadOnly = Color.FromArgb(255, 220, 250, 220);
        /// <summary>
        /// Возвращает цвет текста ячейки
        /// </summary>
        public Color GridCellForeColor = SystemColors.ControlText;
        public Color GridCellForeColorNotReadOnly = Color.Green;

        public Color GridCellBackColorRowClose = Color.LightSlateGray;
        public Color GridCellForeColorRowClose = Color.LightYellow;
        public Color GridCellSelectionBackColorRowClose = Color.SlateGray;
        public Color GridCellSelectionForeColorRowClose = Color.LightYellow;

        public Color GridSelectedCellBackColorNotReadOnly = Color.LightSeaGreen;
        /// <summary>
        /// Удаленная запись
        /// </summary>
        public Color GridCellDeleted = Color.FromArgb(255, 230, 230, 230);
        /// <summary>
        /// Выделенная удаленная запись
        /// </summary>
        public Color GridSelectedCellDeleted = Color.FromArgb(255, 240, 240, 230);
        /// <summary>
        /// Возвращает цвет шрифта для ячейки с ошибкой
        /// </summary>
        public Color GridCellErrorForeColor = Color.Red;
        /// <summary>
        /// Возвращает цвет фона для ячейки с ошибкой
        /// </summary>
        public Color GridCellErrorBackColor = Color.LightGray;

        public Color GridHeaderBackColor = Color.Sienna;
        public Color GridHeaderForeColor = SystemColors.Info;
        public Color GridHeaderSelectionBackColor = SystemColors.Highlight;
        public Color GridHeaderSelectionForeColor = SystemColors.HighlightText;

        public Color ButtonBackColor = Color.Sienna;
        public Color ButtonForeColor = SystemColors.Info;

        public Color TextBoxBackColorReadOnly = SystemColors.Info;
        public Color TextBoxBackColorReadOnlyNot = SystemColors.Window;
    }
    
    #region WaitClass

    /// <summary>
    /// Класс для временного изменения курсора на часики для заданного окошка и всех его потомков.
    /// <example>
    ///    using (new WaitCursor(this))
    ///    {
    ///        document.Save(document.FilePath);
    ///    }
    /// </example>
    /// </summary>
    internal class WaitCursor : IDisposable
    {
        public WaitCursor(Control ctl)
        {
            _ctl = ctl;
            _useWaitCursor = ctl.UseWaitCursor;

            // Если свойство UseWaitCursor уже выставлено в true, ничего не трогаем
            if (!_useWaitCursor)
            {
                ctl.UseWaitCursor = true;
                Cursor.Current = Cursors.WaitCursor;
            }
        }

        public void Dispose()
        {
            // Восстанавливаем, если нужно
            if (!_useWaitCursor)
                _ctl.UseWaitCursor = false;

            GC.SuppressFinalize(this);
        }

        private Control _ctl;
        private bool _useWaitCursor;

        ~WaitCursor()
        {
            Debug.Assert(false, "You forgot to dispose WaitCursor");
        }
    }

    /// <summary>
    /// Класс для временного изменения курсора на часики для всего приложения.
    /// <example>
    ///    using (new AppWaitCursor())
    ///    {
    ///        document.Save(document.FilePath);
    ///    }
    /// </example>
    /// </summary>
    internal class AppWaitCursor : IDisposable
    {
        public AppWaitCursor()
        {
            _useWaitCursor = Application.UseWaitCursor;

            // Если свойство UseWaitCursor уже выставлено в true, ничего не трогаем
            if (!_useWaitCursor)
            {
                Application.UseWaitCursor = true;
                Cursor.Current = Cursors.WaitCursor;
            }
        }

        public void Dispose()
        {
            // Восстанавливаем, если нужно
            if (!_useWaitCursor)
                Application.UseWaitCursor = false;

            GC.SuppressFinalize(this);
        }

        private bool _useWaitCursor;

        ~AppWaitCursor()
        {
            Debug.Assert(false, "You forgot to dispose AppWaitCursor");
        }
    }
    #endregion

    public class ColorsMenu : ProfessionalColorTable
    {
        public override Color MenuItemSelected
        {
            // 51, 153, 255 - устанавливаем голубой цвет выбранного элемента
            // (или задаете свой)
            //get { return Color.FromArgb(51, 153, 255); }
            get { return Color.SaddleBrown; }
        }

        public override Color MenuStripGradientBegin => Color.Sienna;
        public override Color MenuStripGradientEnd => Color.Sienna;

        public override Color ToolStripDropDownBackground
        {
            get { return Color.Sienna; }
        }

        public override Color ImageMarginGradientBegin
        {
            get { return Color.Sienna; }
        }

        public override Color ImageMarginGradientEnd
        {
            get { return Color.Sienna; }
        }

        public override Color ImageMarginGradientMiddle
        {
            get { return Color.Sienna; }
        }

        public override Color MenuItemSelectedGradientBegin
        {
            //get { return Color.FromArgb(51, 153, 255); }
            get { return Color.SaddleBrown; }
        }
        public override Color MenuItemSelectedGradientEnd
        {
            //get { return Color.FromArgb(51, 153, 255); }
            get { return Color.SaddleBrown; }
        }

        public override Color MenuItemPressedGradientBegin
        {
            //get { return Color.FromArgb(51, 153, 255); }
            get { return Color.SaddleBrown; }
        }

        public override Color MenuItemPressedGradientMiddle
        {
            //get { return Color.FromArgb(51, 153, 255); }
            get { return Color.SaddleBrown; }
        }

        public override Color MenuItemPressedGradientEnd
        {
            //get { return Color.FromArgb(51, 153, 255); }
            get { return Color.SaddleBrown; }
        }

        public override Color MenuItemBorder
        {
            get { return Color.LightYellow; }
        }

        public override Color MenuBorder => Color.LightYellow;

        public override Color StatusStripGradientBegin => Color.LightYellow;
        public override Color StatusStripGradientEnd => Color.LightYellow;

        //public override Color ToolStripBorder => Color.Sienna; // ????????
        //public override Color ToolStripContentPanelGradientBegin => Color.Blue; // ????????
        //public override Color ToolStripContentPanelGradientEnd => Color.Blue; // ????????

        //public override Color ToolStripGradientBegin => Color.Green; // ????????
        //public override Color ToolStripGradientEnd => Color.Green; // ????????

        //public override Color RaftingContainerGradientBegin => Color.Green; // ????????
        //public override Color RaftingContainerGradientEnd => Color.Green; // ????????

        //public override Color OverflowButtonGradientBegin => Color.Green; // ????????
        //public override Color OverflowButtonGradientMiddle => Color.Green; // ????????
        //public override Color OverflowButtonGradientEnd => Color.Green; // ????????

        //public override Color GripDark => Color.Green; // ????????
        //public override Color GripLight => Color.Green; // ????????

        //public override Color ToolStripPanelGradientBegin => Color.Green; // ????????
        //public override Color ToolStripGradientMiddle => Color.Green; // ????????
        //public override Color ToolStripPanelGradientEnd => Color.Green; // ????????

        //public override Color ButtonCheckedGradientBegin => Color.Green; // ????????
        //public override Color ButtonCheckedGradientMiddle => Color.Green; // ????????
        //public override Color ButtonCheckedGradientEnd => Color.Green; // ????????

        //public override Color ButtonCheckedHighlight => Color.Green; // ????????
        //public override Color ButtonCheckedHighlightBorder => Color.Green; // ????????

        //public override Color ButtonPressedHighlight => Color.Green; // ????????
        //public override Color ButtonPressedHighlightBorder => Color.Green; // ????????

        //public override Color ButtonPressedBorder => Color.Green; // ????????

        //public override Color ButtonPressedGradientBegin => Color.Green; // ????????
        //public override Color ButtonPressedGradientMiddle => Color.Green; // ????????
        //public override Color ButtonPressedGradientEnd => Color.Green; // ????????
    }
}
