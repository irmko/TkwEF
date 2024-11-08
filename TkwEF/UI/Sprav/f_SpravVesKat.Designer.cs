namespace TkwEF.UI.Sprav
{
    partial class f_SpravVesKat
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbAdd = new System.Windows.Forms.ToolStripButton();
            this.grdVesKat = new TkwEF.Core.Controls.DataGridViewXF_Base(this.components);
            this.ColumnId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnBeginVes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnEndVes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bsVesKat = new TkwEF.Core.Controls.BindingSourceX(this.components);
            this.tsbSelect = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdVesKat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsVesKat)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAdd,
            this.tsbSelect});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(284, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbAdd
            // 
            this.tsbAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAdd.Image = global::TkwEF.Properties.Resources.плюс_2_50;
            this.tsbAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAdd.Name = "tsbAdd";
            this.tsbAdd.Size = new System.Drawing.Size(23, 22);
            this.tsbAdd.Text = "Добавить";
            this.tsbAdd.Click += new System.EventHandler(this.tsbAdd_Click);
            // 
            // grdVesKat
            // 
            this.grdVesKat.AllowUserToAddRows = false;
            this.grdVesKat.AllowUserToDeleteRows = false;
            this.grdVesKat.AllowUserToOrderColumns = true;
            this.grdVesKat.AllowUserToResizeRows = false;
            this.grdVesKat.AutoGenerateColumns = false;
            this.grdVesKat.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.Sienna;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdVesKat.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.grdVesKat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdVesKat.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnId,
            this.ColumnBeginVes,
            this.ColumnEndVes});
            this.grdVesKat.DataSource = this.bsVesKat;
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.Color.Sienna;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdVesKat.DefaultCellStyle = dataGridViewCellStyle17;
            this.grdVesKat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdVesKat.EnableHeadersVisualStyles = false;
            this.grdVesKat.Location = new System.Drawing.Point(0, 25);
            this.grdVesKat.Name = "grdVesKat";
            this.grdVesKat.ReadOnly = true;
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle18.BackColor = System.Drawing.Color.Sienna;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.Color.Chocolate;
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdVesKat.RowHeadersDefaultCellStyle = dataGridViewCellStyle18;
            this.grdVesKat.RowHeadersWidth = 34;
            this.grdVesKat.ShowRowErrors = false;
            this.grdVesKat.Size = new System.Drawing.Size(284, 237);
            this.grdVesKat.TabIndex = 3;
            // 
            // ColumnId
            // 
            this.ColumnId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnId.DataPropertyName = "Id";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ColumnId.DefaultCellStyle = dataGridViewCellStyle14;
            this.ColumnId.HeaderText = "Код";
            this.ColumnId.Name = "ColumnId";
            this.ColumnId.ReadOnly = true;
            this.ColumnId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnId.ToolTipText = "Код физического лица";
            this.ColumnId.Width = 60;
            // 
            // ColumnBeginVes
            // 
            this.ColumnBeginVes.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnBeginVes.DataPropertyName = "BeginVes";
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle15.Format = "d";
            dataGridViewCellStyle15.NullValue = null;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ColumnBeginVes.DefaultCellStyle = dataGridViewCellStyle15;
            this.ColumnBeginVes.HeaderText = "От";
            this.ColumnBeginVes.Name = "ColumnBeginVes";
            this.ColumnBeginVes.ReadOnly = true;
            this.ColumnBeginVes.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnBeginVes.ToolTipText = "Вес от";
            this.ColumnBeginVes.Width = 65;
            // 
            // ColumnEndVes
            // 
            this.ColumnEndVes.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnEndVes.DataPropertyName = "EndVes";
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle16.Format = "d";
            dataGridViewCellStyle16.NullValue = null;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ColumnEndVes.DefaultCellStyle = dataGridViewCellStyle16;
            this.ColumnEndVes.HeaderText = "До";
            this.ColumnEndVes.Name = "ColumnEndVes";
            this.ColumnEndVes.ReadOnly = true;
            this.ColumnEndVes.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnEndVes.ToolTipText = "Вес до";
            this.ColumnEndVes.Width = 65;
            // 
            // tsbSelect
            // 
            this.tsbSelect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSelect.Image = global::TkwEF.Properties.Resources.tick_16;
            this.tsbSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSelect.Name = "tsbSelect";
            this.tsbSelect.Size = new System.Drawing.Size(23, 22);
            this.tsbSelect.Text = "toolStripButton1";
            this.tsbSelect.Click += new System.EventHandler(this.tsbSelect_Click);
            // 
            // f_SpravVesKat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.grdVesKat);
            this.Controls.Add(this.toolStrip1);
            this.Name = "f_SpravVesKat";
            this.Text = "Весовые категории";
            this.Shown += new System.EventHandler(this.f_SpravVesKat_Shown);
            this.Controls.SetChildIndex(this.toolStrip1, 0);
            this.Controls.SetChildIndex(this.grdVesKat, 0);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdVesKat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsVesKat)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbAdd;
        private Core.Controls.DataGridViewXF_Base grdVesKat;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnBeginVes;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnEndVes;
        private Core.Controls.BindingSourceX bsVesKat;
        private System.Windows.Forms.ToolStripButton tsbSelect;
    }
}
