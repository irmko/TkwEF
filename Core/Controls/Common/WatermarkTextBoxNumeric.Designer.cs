namespace TkwEF.Core.Controls
{
    partial class WatermarkTextBoxNumeric
    {

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // WatermarkTextBoxNumeric
            // 
            this.MaxLength = 50;
            this.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Enter += new System.EventHandler(this.WatermarkTextBox_Enter);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.WatermarkTextBox_KeyPress);
            this.Leave += new System.EventHandler(this.WatermarkTextBox_Leave);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
