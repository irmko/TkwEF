using System;
using System.Data;
using System.Windows.Forms;
using System.Linq;
using System.Data.Entity;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace TkwEF.UI.Sprav
{
    public partial class f_SpravVesKat_edit : TkwEF.UI.f_BDUI
    {
        #region enum

        private enum Mode { Add, Edit }

        #endregion

        #region Fields

        private readonly Mode _mode;
        private readonly int? _idVesKat = null;
        /// <summary>
        /// Возвращаемое значение
        /// </summary>
        public int? RetVal { get; private set; }

        #endregion

        #region ctor

        public f_SpravVesKat_edit()
        {
            InitializeComponent();
            _mode = Mode.Add;
        }
        public f_SpravVesKat_edit(int id) : this()
        {
            _mode = Mode.Edit;
            _idVesKat = id;
        }

        #endregion

        #region handled

        private void f_SpravVesKat_edit_Load(object sender, EventArgs e)
        {
            btnOk.Text = _mode == Mode.Add ? "Добавить" : "Изменить";
            btnOk.ImageIndex = _mode == Mode.Add ? 0 : 1;
        }
        private void f_SpravVesKat_edit_Shown(object sender, EventArgs e)
        {
            _getBeginEndValue();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.None;
            btnOk.Enabled = false;
            DialogResult = DialogResult.Abort;
            try
            {
                int begin = (int)numBegin.Value;
                int end = (int)numEnd.Value;
                if (_mode == Mode.Add)
                {
                    using (BLL.TkwContext context = new BLL.TkwContext())
                    {
                        BLL.VesKat vesKat = new BLL.VesKat { BeginVes = begin, EndVes = end };
                        bool allow = context.VesKates.Where(w => w.BeginVes == vesKat.BeginVes && w.EndVes == vesKat.EndVes).FirstOrDefault() == null;
                        if (!allow)
                            throw new UiException("Такая категория уже есть");

                        context.VesKates.Add(vesKat);
                        context.SaveChanges();
                        RetVal = vesKat.Id;
                    }
                }
                else
                {
                    using (BLL.TkwContext context = new BLL.TkwContext())
                    {
                    }
                }
                DialogResult = DialogResult.OK;
            }
            catch (UiException ex) { ex.ShowMessage(this); }
            catch (Exception ex)
            {
                new UiException(string.Format("Ошибка при {0} весовой категории", (_mode == Mode.Add ? "создании" : "изменении")), ex, sendMail: true);
            }
        }

        private void numBegin_ValueChanged(object sender, EventArgs e)
        {
            if (numBegin.Value > numEnd.Value)
                numEnd.Value = numBegin.Value;
        }
        private void numEnd_ValueChanged(object sender, EventArgs e)
        {
            if (numEnd.Value < numBegin.Value)
                numBegin.Value = numEnd.Value;
        }

        #endregion

        #region Private function

        private async void _getBeginEndValue()
        {
            int begin = 0, end = 0;
            ObservableCollection<BLL.VesKat> src;
            using (BLL.TkwContext context = new BLL.TkwContext())
            {
                context.VesKates.Load();
                src = context.VesKates.Local;
            }
            var lst = await Task.Run(() => src.OrderBy(o => o.BeginVes).ThenBy(t => t.EndVes));
            begin = await Task.Run(() => src.Count == 0 ? 0 : src.Max(m => m.BeginVes));
            end = await Task.Run(() => src.Count == 0 ? 0 : src.Max(m => m.EndVes));

            if (begin == 0)
            {
                begin = 10;
            }
            else if (begin > numBegin.Maximum)
            {
                begin = (int)numBegin.Maximum;
            }
            else
                begin = end;
            end = begin + 5;
            numBegin.Value = begin;
            numEnd.Value = end;
        }

        #endregion
    }
}
