namespace TkwEF.UI.Sprav
{
    partial class f_SpravVesKat_edit
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(f_SpravVesKat_edit));
            this.numBegin = new System.Windows.Forms.NumericUpDown();
            this.numEnd = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.numBegin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEnd)).BeginInit();
            this.SuspendLayout();
            // 
            // numBegin
            // 
            this.numBegin.Location = new System.Drawing.Point(58, 26);
            this.numBegin.Maximum = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.numBegin.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numBegin.Name = "numBegin";
            this.numBegin.Size = new System.Drawing.Size(51, 20);
            this.numBegin.TabIndex = 1;
            this.numBegin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numBegin.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numBegin.ValueChanged += new System.EventHandler(this.numBegin_ValueChanged);
            // 
            // numEnd
            // 
            this.numEnd.Location = new System.Drawing.Point(155, 26);
            this.numEnd.Maximum = new decimal(new int[] {
            151,
            0,
            0,
            0});
            this.numEnd.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numEnd.Name = "numEnd";
            this.numEnd.Size = new System.Drawing.Size(51, 20);
            this.numEnd.TabIndex = 2;
            this.numEnd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numEnd.Value = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.numEnd.ValueChanged += new System.EventHandler(this.numEnd_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(32, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "от";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(128, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "до";
            // 
            // btnOk
            // 
            this.btnOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnOk.ImageList = this.imgList;
            this.btnOk.Location = new System.Drawing.Point(68, 83);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(100, 30);
            this.btnOk.TabIndex = 5;
            this.btnOk.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // imgList
            // 
            this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList.Images.SetKeyName(0, "плюс-2-50.png");
            this.imgList.Images.SetKeyName(1, "edit-26.png");
            // 
            // f_SpravVesKat_edit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(243, 122);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numEnd);
            this.Controls.Add(this.numBegin);
            this.Name = "f_SpravVesKat_edit";
            this.Text = "Весовые категории";
            this.Load += new System.EventHandler(this.f_SpravVesKat_edit_Load);
            this.Shown += new System.EventHandler(this.f_SpravVesKat_edit_Shown);
            this.Controls.SetChildIndex(this.numBegin, 0);
            this.Controls.SetChildIndex(this.numEnd, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.btnOk, 0);
            ((System.ComponentModel.ISupportInitialize)(this.numBegin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEnd)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numBegin;
        private System.Windows.Forms.NumericUpDown numEnd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ImageList imgList;
    }
}
