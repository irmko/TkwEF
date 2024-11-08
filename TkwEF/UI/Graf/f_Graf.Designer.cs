namespace TkwEF.UI.Graf
{
    partial class f_Graf
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(f_Graf));
            this.grdGrafs = new TkwEF.Controls.DataGridViewXF(this.components);
            this.ColumnId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnEtap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnNomer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnRedUserFIO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnBlueUserFIO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnBegin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnEnd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnStatusName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPobeditelFIO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnVesToString = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnRedPoyasName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnBluePoyasName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bsGrafs = new TkwEF.Core.Controls.BindingSourceX(this.components);
            this.tsBtn = new System.Windows.Forms.ToolStrip();
            this.tsbAddUser = new System.Windows.Forms.ToolStripButton();
            this.tsbSaveGraf = new System.Windows.Forms.ToolStripButton();
            this.tsbSformirGraf = new System.Windows.Forms.ToolStripButton();
            this.tsbCancelGrafTmp = new System.Windows.Forms.ToolStripButton();
            this.tscType = new System.Windows.Forms.ToolStripComboBox();
            this.labelGrdZagolovok2 = new TkwEF.Controls.TextBoxGrdZagolovok(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.grdGrafs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsGrafs)).BeginInit();
            this.tsBtn.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdGrafs
            // 
            this.grdGrafs.AllowDrop = true;
            this.grdGrafs.AllowUserToAddRows = false;
            this.grdGrafs.AllowUserToDeleteRows = false;
            this.grdGrafs.AllowUserToOrderColumns = true;
            this.grdGrafs.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdGrafs.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.grdGrafs.AutoGenerateColumns = false;
            this.grdGrafs.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.grdGrafs.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Sienna;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdGrafs.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.grdGrafs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdGrafs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnId,
            this.ColumnEtap,
            this.ColumnNomer,
            this.ColumnRedUserFIO,
            this.ColumnBlueUserFIO,
            this.ColumnBegin,
            this.ColumnEnd,
            this.ColumnStatusName,
            this.ColumnPobeditelFIO,
            this.ColumnVesToString,
            this.ColumnRedPoyasName,
            this.ColumnBluePoyasName});
            this.grdGrafs.DataSource = this.bsGrafs;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.Sienna;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdGrafs.DefaultCellStyle = dataGridViewCellStyle12;
            this.grdGrafs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdGrafs.EnableHeadersVisualStyles = false;
            this.grdGrafs.Location = new System.Drawing.Point(0, 47);
            this.grdGrafs.Name = "grdGrafs";
            this.grdGrafs.ReadOnly = true;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.Sienna;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.Color.Chocolate;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdGrafs.RowHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.grdGrafs.RowHeadersWidth = 34;
            this.grdGrafs.ShowRowErrors = false;
            this.grdGrafs.Size = new System.Drawing.Size(857, 265);
            this.grdGrafs.TabIndex = 2;
            this.grdGrafs.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grdGrafs_CellMouseDoubleClick);
            this.grdGrafs.CellMouseMove += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grdGrafs_CellMouseMove);
            this.grdGrafs.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.grdGrafs_CellPainting);
            this.grdGrafs.DragDrop += new System.Windows.Forms.DragEventHandler(this.grdGrafs_DragDrop);
            this.grdGrafs.DragEnter += new System.Windows.Forms.DragEventHandler(this.grdGrafs_DragEnter);
            this.grdGrafs.DragOver += new System.Windows.Forms.DragEventHandler(this.grdGrafs_DragOver);
            this.grdGrafs.MouseDown += new System.Windows.Forms.MouseEventHandler(this.grdGrafs_MouseDown);
            // 
            // ColumnId
            // 
            this.ColumnId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnId.DataPropertyName = "Id";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ColumnId.DefaultCellStyle = dataGridViewCellStyle3;
            this.ColumnId.HeaderText = "ID";
            this.ColumnId.Name = "ColumnId";
            this.ColumnId.ReadOnly = true;
            this.ColumnId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnId.ToolTipText = "Код";
            this.ColumnId.Width = 50;
            // 
            // ColumnEtap
            // 
            this.ColumnEtap.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnEtap.DataPropertyName = "Etap";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ColumnEtap.DefaultCellStyle = dataGridViewCellStyle4;
            this.ColumnEtap.HeaderText = "Этап";
            this.ColumnEtap.MinimumWidth = 20;
            this.ColumnEtap.Name = "ColumnEtap";
            this.ColumnEtap.ReadOnly = true;
            this.ColumnEtap.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnEtap.Width = 50;
            // 
            // ColumnNomer
            // 
            this.ColumnNomer.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnNomer.DataPropertyName = "Nomer";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ColumnNomer.DefaultCellStyle = dataGridViewCellStyle5;
            this.ColumnNomer.HeaderText = "№";
            this.ColumnNomer.MinimumWidth = 50;
            this.ColumnNomer.Name = "ColumnNomer";
            this.ColumnNomer.ReadOnly = true;
            this.ColumnNomer.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnNomer.ToolTipText = "Порядковый номер на этапе";
            this.ColumnNomer.Width = 60;
            // 
            // ColumnRedUserFIO
            // 
            this.ColumnRedUserFIO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnRedUserFIO.DataPropertyName = "RedUserFIO";
            this.ColumnRedUserFIO.HeaderText = "Красный";
            this.ColumnRedUserFIO.Name = "ColumnRedUserFIO";
            this.ColumnRedUserFIO.ReadOnly = true;
            this.ColumnRedUserFIO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ColumnBlueUserFIO
            // 
            this.ColumnBlueUserFIO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnBlueUserFIO.DataPropertyName = "BlueUserFIO";
            this.ColumnBlueUserFIO.HeaderText = "Синий";
            this.ColumnBlueUserFIO.Name = "ColumnBlueUserFIO";
            this.ColumnBlueUserFIO.ReadOnly = true;
            this.ColumnBlueUserFIO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ColumnBegin
            // 
            this.ColumnBegin.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnBegin.DataPropertyName = "Begin";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Format = "d";
            dataGridViewCellStyle6.NullValue = null;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ColumnBegin.DefaultCellStyle = dataGridViewCellStyle6;
            this.ColumnBegin.HeaderText = "Начало";
            this.ColumnBegin.MinimumWidth = 50;
            this.ColumnBegin.Name = "ColumnBegin";
            this.ColumnBegin.ReadOnly = true;
            this.ColumnBegin.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnBegin.Width = 80;
            // 
            // ColumnEnd
            // 
            this.ColumnEnd.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnEnd.DataPropertyName = "End";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.Format = "d";
            dataGridViewCellStyle7.NullValue = null;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ColumnEnd.DefaultCellStyle = dataGridViewCellStyle7;
            this.ColumnEnd.HeaderText = "Окончание";
            this.ColumnEnd.MinimumWidth = 50;
            this.ColumnEnd.Name = "ColumnEnd";
            this.ColumnEnd.ReadOnly = true;
            this.ColumnEnd.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnEnd.Width = 80;
            // 
            // ColumnStatusName
            // 
            this.ColumnStatusName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnStatusName.DataPropertyName = "StatusName";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ColumnStatusName.DefaultCellStyle = dataGridViewCellStyle8;
            this.ColumnStatusName.HeaderText = "Статус";
            this.ColumnStatusName.MinimumWidth = 50;
            this.ColumnStatusName.Name = "ColumnStatusName";
            this.ColumnStatusName.ReadOnly = true;
            this.ColumnStatusName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnStatusName.Width = 70;
            // 
            // ColumnPobeditelFIO
            // 
            this.ColumnPobeditelFIO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnPobeditelFIO.DataPropertyName = "PobeditelFIO";
            this.ColumnPobeditelFIO.HeaderText = "Победитель";
            this.ColumnPobeditelFIO.MinimumWidth = 50;
            this.ColumnPobeditelFIO.Name = "ColumnPobeditelFIO";
            this.ColumnPobeditelFIO.ReadOnly = true;
            this.ColumnPobeditelFIO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ColumnVesToString
            // 
            this.ColumnVesToString.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnVesToString.DataPropertyName = "VesToString";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ColumnVesToString.DefaultCellStyle = dataGridViewCellStyle9;
            this.ColumnVesToString.HeaderText = "Вес";
            this.ColumnVesToString.Name = "ColumnVesToString";
            this.ColumnVesToString.ReadOnly = true;
            this.ColumnVesToString.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ColumnRedPoyasName
            // 
            this.ColumnRedPoyasName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnRedPoyasName.DataPropertyName = "RedPoyasName";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ColumnRedPoyasName.DefaultCellStyle = dataGridViewCellStyle10;
            this.ColumnRedPoyasName.HeaderText = "Пояс (кр)";
            this.ColumnRedPoyasName.MinimumWidth = 50;
            this.ColumnRedPoyasName.Name = "ColumnRedPoyasName";
            this.ColumnRedPoyasName.ReadOnly = true;
            this.ColumnRedPoyasName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnRedPoyasName.ToolTipText = "Пояс красного участника";
            this.ColumnRedPoyasName.Width = 80;
            // 
            // ColumnBluePoyasName
            // 
            this.ColumnBluePoyasName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnBluePoyasName.DataPropertyName = "BluePoyasName";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ColumnBluePoyasName.DefaultCellStyle = dataGridViewCellStyle11;
            this.ColumnBluePoyasName.HeaderText = "Пояс (с)";
            this.ColumnBluePoyasName.MinimumWidth = 50;
            this.ColumnBluePoyasName.Name = "ColumnBluePoyasName";
            this.ColumnBluePoyasName.ReadOnly = true;
            this.ColumnBluePoyasName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnBluePoyasName.ToolTipText = "Пояс синего участника";
            this.ColumnBluePoyasName.Width = 80;
            // 
            // bsGrafs
            // 
            this.bsGrafs.DataMember = "CompGrafs";
            // 
            // tsBtn
            // 
            this.tsBtn.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAddUser,
            this.tsbSaveGraf,
            this.tsbSformirGraf,
            this.tsbCancelGrafTmp,
            this.tscType});
            this.tsBtn.Location = new System.Drawing.Point(0, 0);
            this.tsBtn.Name = "tsBtn";
            this.tsBtn.Size = new System.Drawing.Size(857, 25);
            this.tsBtn.TabIndex = 4;
            this.tsBtn.Text = "toolStrip2";
            // 
            // tsbAddUser
            // 
            this.tsbAddUser.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAddUser.Image = ((System.Drawing.Image)(resources.GetObject("tsbAddUser.Image")));
            this.tsbAddUser.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAddUser.Name = "tsbAddUser";
            this.tsbAddUser.Size = new System.Drawing.Size(23, 22);
            this.tsbAddUser.Text = "Добавить участника";
            this.tsbAddUser.Click += new System.EventHandler(this.tsbAddUser_Click);
            // 
            // tsbSaveGraf
            // 
            this.tsbSaveGraf.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSaveGraf.Image = ((System.Drawing.Image)(resources.GetObject("tsbSaveGraf.Image")));
            this.tsbSaveGraf.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSaveGraf.Name = "tsbSaveGraf";
            this.tsbSaveGraf.Size = new System.Drawing.Size(23, 22);
            this.tsbSaveGraf.Text = "Сохранить график соревнования";
            this.tsbSaveGraf.Click += new System.EventHandler(this.tsbSaveGraf_Click);
            // 
            // tsbSformirGraf
            // 
            this.tsbSformirGraf.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSformirGraf.Image = ((System.Drawing.Image)(resources.GetObject("tsbSformirGraf.Image")));
            this.tsbSformirGraf.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSformirGraf.Name = "tsbSformirGraf";
            this.tsbSformirGraf.Size = new System.Drawing.Size(23, 22);
            this.tsbSformirGraf.Text = "Сформировать график соревнования";
            this.tsbSformirGraf.Click += new System.EventHandler(this.tsbSformirGraf_Click);
            this.tsbSformirGraf.VisibleChanged += new System.EventHandler(this.tsbSformirGraf_VisibleChanged);
            // 
            // tsbCancelGrafTmp
            // 
            this.tsbCancelGrafTmp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCancelGrafTmp.Image = ((System.Drawing.Image)(resources.GetObject("tsbCancelGrafTmp.Image")));
            this.tsbCancelGrafTmp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCancelGrafTmp.Name = "tsbCancelGrafTmp";
            this.tsbCancelGrafTmp.Size = new System.Drawing.Size(23, 22);
            this.tsbCancelGrafTmp.Text = "Отменить автоматически сформированный этап графика";
            this.tsbCancelGrafTmp.Visible = false;
            this.tsbCancelGrafTmp.Click += new System.EventHandler(this.tsbCancelGrafTmp_Click);
            // 
            // tscType
            // 
            this.tscType.Items.AddRange(new object[] {
            "Олимпийская",
            "Турнирная"});
            this.tscType.Name = "tscType";
            this.tscType.Size = new System.Drawing.Size(121, 25);
            this.tscType.ToolTipText = "Система расчета графика";
            // 
            // labelGrdZagolovok2
            // 
            this.labelGrdZagolovok2.BackColor = System.Drawing.Color.Sienna;
            this.labelGrdZagolovok2.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelGrdZagolovok2.Enabled = false;
            this.labelGrdZagolovok2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelGrdZagolovok2.ForeColor = System.Drawing.Color.LemonChiffon;
            this.labelGrdZagolovok2.Location = new System.Drawing.Point(0, 25);
            this.labelGrdZagolovok2.Name = "labelGrdZagolovok2";
            this.labelGrdZagolovok2.Size = new System.Drawing.Size(857, 22);
            this.labelGrdZagolovok2.TabIndex = 8;
            this.labelGrdZagolovok2.TabStop = false;
            this.labelGrdZagolovok2.Text = "График соревнования";
            this.labelGrdZagolovok2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // f_Graf
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(857, 312);
            this.Controls.Add(this.grdGrafs);
            this.Controls.Add(this.labelGrdZagolovok2);
            this.Controls.Add(this.tsBtn);
            this.Name = "f_Graf";
            this.Text = "График";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.f_Graf_FormClosing);
            this.Load += new System.EventHandler(this.f_Graf_Load);
            this.Controls.SetChildIndex(this.tsBtn, 0);
            this.Controls.SetChildIndex(this.labelGrdZagolovok2, 0);
            this.Controls.SetChildIndex(this.grdGrafs, 0);
            ((System.ComponentModel.ISupportInitialize)(this.grdGrafs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsGrafs)).EndInit();
            this.tsBtn.ResumeLayout(false);
            this.tsBtn.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Controls.DataGridViewXF grdGrafs;
        private Core.Controls.BindingSourceX bsGrafs;
        private System.Windows.Forms.ToolStrip tsBtn;
        private System.Windows.Forms.ToolStripButton tsbSaveGraf;
        private System.Windows.Forms.ToolStripComboBox tscType;
        private System.Windows.Forms.ToolStripButton tsbAddUser;
        private Controls.TextBoxGrdZagolovok labelGrdZagolovok2;
        private System.Windows.Forms.ToolStripButton tsbSformirGraf;
        private System.Windows.Forms.ToolStripButton tsbCancelGrafTmp;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnEtap;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNomer;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnRedUserFIO;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnBlueUserFIO;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnBegin;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnEnd;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStatusName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPobeditelFIO;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnVesToString;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnRedPoyasName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnBluePoyasName;
    }
}
