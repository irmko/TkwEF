namespace TkwEF.UI
{
    partial class f_Main
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.smiFile = new System.Windows.Forms.ToolStripMenuItem();
            this.smiClose = new System.Windows.Forms.ToolStripMenuItem();
            this.smiSprav = new System.Windows.Forms.ToolStripMenuItem();
            this.msiSpravFizLitas = new System.Windows.Forms.ToolStripMenuItem();
            this.msiSpravPoyas = new System.Windows.Forms.ToolStripMenuItem();
            this.msiVesKat = new System.Windows.Forms.ToolStripMenuItem();
            this.msiSpravClub = new System.Windows.Forms.ToolStripMenuItem();
            this.msiClubUsers = new System.Windows.Forms.ToolStripMenuItem();
            this.grdComp = new TkwEF.Controls.DataGridViewXF(this.components);
            this.ColumnId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDateBegin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDateEnd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bsComp = new TkwEF.Core.Controls.BindingSourceX(this.components);
            this.tsComp = new System.Windows.Forms.ToolStrip();
            this.tsbAddComp = new System.Windows.Forms.ToolStripButton();
            this.tsbEditComp = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.grdCompUsers = new TkwEF.Controls.DataGridViewXF(this.components);
            this.bsCompUsers = new TkwEF.Core.Controls.BindingSourceX(this.components);
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.ColumnIdCu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnFIO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnAgeUser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnVesToString = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnClubName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdComp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsComp)).BeginInit();
            this.tsComp.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdCompUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsCompUsers)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smiFile,
            this.smiSprav});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(551, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // smiFile
            // 
            this.smiFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smiClose});
            this.smiFile.Name = "smiFile";
            this.smiFile.Size = new System.Drawing.Size(48, 20);
            this.smiFile.Text = "Файл";
            // 
            // smiClose
            // 
            this.smiClose.Image = global::TkwEF.Properties.Resources.закрыть_окно_48;
            this.smiClose.Name = "smiClose";
            this.smiClose.Size = new System.Drawing.Size(120, 22);
            this.smiClose.Text = "Закрыть";
            this.smiClose.Click += new System.EventHandler(this.smiClose_Click);
            // 
            // smiSprav
            // 
            this.smiSprav.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msiSpravFizLitas,
            this.msiSpravPoyas,
            this.msiVesKat,
            this.msiSpravClub,
            this.msiClubUsers});
            this.smiSprav.Name = "smiSprav";
            this.smiSprav.Size = new System.Drawing.Size(94, 20);
            this.smiSprav.Text = "Справочники";
            // 
            // msiSpravFizLitas
            // 
            this.msiSpravFizLitas.Name = "msiSpravFizLitas";
            this.msiSpravFizLitas.Size = new System.Drawing.Size(180, 22);
            this.msiSpravFizLitas.Text = "Физ. лица";
            this.msiSpravFizLitas.Click += new System.EventHandler(this.msiSpravFizLitas_Click);
            // 
            // msiSpravPoyas
            // 
            this.msiSpravPoyas.Name = "msiSpravPoyas";
            this.msiSpravPoyas.Size = new System.Drawing.Size(180, 22);
            this.msiSpravPoyas.Text = "Пояса";
            this.msiSpravPoyas.Click += new System.EventHandler(this.msiSpravPoyas_Click);
            // 
            // msiVesKat
            // 
            this.msiVesKat.Name = "msiVesKat";
            this.msiVesKat.Size = new System.Drawing.Size(180, 22);
            this.msiVesKat.Text = "Веcовые категории";
            this.msiVesKat.Click += new System.EventHandler(this.msiVesKat_Click);
            // 
            // msiSpravClub
            // 
            this.msiSpravClub.Name = "msiSpravClub";
            this.msiSpravClub.Size = new System.Drawing.Size(180, 22);
            this.msiSpravClub.Text = "Клубы";
            this.msiSpravClub.Click += new System.EventHandler(this.msiSpravClub_Click);
            // 
            // msiClubUsers
            // 
            this.msiClubUsers.Name = "msiClubUsers";
            this.msiClubUsers.Size = new System.Drawing.Size(180, 22);
            this.msiClubUsers.Text = "Члены клуба";
            this.msiClubUsers.Click += new System.EventHandler(this.msiClubUsers_Click);
            // 
            // grdComp
            // 
            this.grdComp.AllowUserToAddRows = false;
            this.grdComp.AllowUserToDeleteRows = false;
            this.grdComp.AllowUserToOrderColumns = true;
            this.grdComp.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdComp.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.grdComp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdComp.AutoGenerateColumns = false;
            this.grdComp.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Sienna;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdComp.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.grdComp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdComp.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnId,
            this.ColumnName,
            this.ColumnDateBegin,
            this.ColumnDateEnd});
            this.grdComp.DataSource = this.bsComp;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Sienna;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdComp.DefaultCellStyle = dataGridViewCellStyle6;
            this.grdComp.EnableHeadersVisualStyles = false;
            this.grdComp.Location = new System.Drawing.Point(3, 3);
            this.grdComp.Name = "grdComp";
            this.grdComp.ReadOnly = true;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.Sienna;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.Chocolate;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdComp.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.grdComp.RowHeadersWidth = 34;
            this.grdComp.ShowRowErrors = false;
            this.grdComp.Size = new System.Drawing.Size(265, 467);
            this.grdComp.TabIndex = 3;
            this.toolTip1.SetToolTip(this.grdComp, "Соревнования");
            // 
            // ColumnId
            // 
            this.ColumnId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnId.DataPropertyName = "Id";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ColumnId.DefaultCellStyle = dataGridViewCellStyle3;
            this.ColumnId.HeaderText = "Код";
            this.ColumnId.Name = "ColumnId";
            this.ColumnId.ReadOnly = true;
            this.ColumnId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnId.Width = 60;
            // 
            // ColumnName
            // 
            this.ColumnName.DataPropertyName = "Name";
            this.ColumnName.HeaderText = "Название";
            this.ColumnName.MinimumWidth = 50;
            this.ColumnName.Name = "ColumnName";
            this.ColumnName.ReadOnly = true;
            this.ColumnName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnName.Width = 150;
            // 
            // ColumnDateBegin
            // 
            this.ColumnDateBegin.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.ColumnDateBegin.DataPropertyName = "DateBegin";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Format = "d";
            dataGridViewCellStyle4.NullValue = null;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ColumnDateBegin.DefaultCellStyle = dataGridViewCellStyle4;
            this.ColumnDateBegin.HeaderText = "Начало";
            this.ColumnDateBegin.Name = "ColumnDateBegin";
            this.ColumnDateBegin.ReadOnly = true;
            this.ColumnDateBegin.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnDateBegin.ToolTipText = "Начало соревнования";
            this.ColumnDateBegin.Width = 70;
            // 
            // ColumnDateEnd
            // 
            this.ColumnDateEnd.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.ColumnDateEnd.DataPropertyName = "DateEnd";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Format = "d";
            dataGridViewCellStyle5.NullValue = null;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ColumnDateEnd.DefaultCellStyle = dataGridViewCellStyle5;
            this.ColumnDateEnd.HeaderText = "Окончание";
            this.ColumnDateEnd.Name = "ColumnDateEnd";
            this.ColumnDateEnd.ReadOnly = true;
            this.ColumnDateEnd.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnDateEnd.ToolTipText = "Окончание соревнования";
            this.ColumnDateEnd.Width = 88;
            // 
            // bsComp
            // 
            this.bsComp.DataMember = "Competitions";
            // 
            // tsComp
            // 
            this.tsComp.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAddComp,
            this.tsbEditComp});
            this.tsComp.Location = new System.Drawing.Point(0, 24);
            this.tsComp.Name = "tsComp";
            this.tsComp.Size = new System.Drawing.Size(551, 25);
            this.tsComp.TabIndex = 4;
            this.tsComp.Text = "toolStrip1";
            // 
            // tsbAddComp
            // 
            this.tsbAddComp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAddComp.Image = global::TkwEF.Properties.Resources.плюс_2_50;
            this.tsbAddComp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAddComp.Name = "tsbAddComp";
            this.tsbAddComp.Size = new System.Drawing.Size(23, 22);
            this.tsbAddComp.Text = "Добавить";
            this.tsbAddComp.Click += new System.EventHandler(this.tsbAddComp_Click);
            // 
            // tsbEditComp
            // 
            this.tsbEditComp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbEditComp.Image = global::TkwEF.Properties.Resources.редактировать_64;
            this.tsbEditComp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEditComp.Name = "tsbEditComp";
            this.tsbEditComp.Size = new System.Drawing.Size(23, 22);
            this.tsbEditComp.Text = "Редактировать соревнование";
            this.tsbEditComp.Click += new System.EventHandler(this.tsbEditComp_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.grdCompUsers, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.grdComp, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.splitter1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 49);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(551, 473);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // grdCompUsers
            // 
            this.grdCompUsers.AllowUserToAddRows = false;
            this.grdCompUsers.AllowUserToDeleteRows = false;
            this.grdCompUsers.AllowUserToOrderColumns = true;
            this.grdCompUsers.AllowUserToResizeRows = false;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdCompUsers.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle8;
            this.grdCompUsers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdCompUsers.AutoGenerateColumns = false;
            this.grdCompUsers.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.Sienna;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdCompUsers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.grdCompUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdCompUsers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnIdCu,
            this.ColumnFIO,
            this.ColumnAgeUser,
            this.ColumnVesToString,
            this.ColumnClubName});
            this.grdCompUsers.DataSource = this.bsCompUsers;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.Color.Sienna;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdCompUsers.DefaultCellStyle = dataGridViewCellStyle13;
            this.grdCompUsers.EnableHeadersVisualStyles = false;
            this.grdCompUsers.Location = new System.Drawing.Point(283, 3);
            this.grdCompUsers.Name = "grdCompUsers";
            this.grdCompUsers.ReadOnly = true;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.Sienna;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.Color.Chocolate;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdCompUsers.RowHeadersDefaultCellStyle = dataGridViewCellStyle14;
            this.grdCompUsers.RowHeadersWidth = 34;
            this.grdCompUsers.ShowRowErrors = false;
            this.grdCompUsers.Size = new System.Drawing.Size(265, 467);
            this.grdCompUsers.TabIndex = 6;
            this.toolTip1.SetToolTip(this.grdCompUsers, "Участники соревнования");
            // 
            // bsCompUsers
            // 
            this.bsCompUsers.DataMember = "CompUsers";
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(274, 3);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 467);
            this.splitter1.TabIndex = 4;
            this.splitter1.TabStop = false;
            // 
            // ColumnIdCu
            // 
            this.ColumnIdCu.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnIdCu.DataPropertyName = "Id";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ColumnIdCu.DefaultCellStyle = dataGridViewCellStyle10;
            this.ColumnIdCu.HeaderText = "Код";
            this.ColumnIdCu.Name = "ColumnIdCu";
            this.ColumnIdCu.ReadOnly = true;
            this.ColumnIdCu.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnIdCu.ToolTipText = "Код участника соревнования";
            this.ColumnIdCu.Width = 50;
            // 
            // ColumnFIO
            // 
            this.ColumnFIO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnFIO.DataPropertyName = "FIO";
            this.ColumnFIO.HeaderText = "Участник";
            this.ColumnFIO.MinimumWidth = 50;
            this.ColumnFIO.Name = "ColumnFIO";
            this.ColumnFIO.ReadOnly = true;
            this.ColumnFIO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnFIO.ToolTipText = "Участник соревнования";
            this.ColumnFIO.Width = 150;
            // 
            // ColumnAgeUser
            // 
            this.ColumnAgeUser.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnAgeUser.DataPropertyName = "AgeUser";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ColumnAgeUser.DefaultCellStyle = dataGridViewCellStyle11;
            this.ColumnAgeUser.HeaderText = "Возраст";
            this.ColumnAgeUser.Name = "ColumnAgeUser";
            this.ColumnAgeUser.ReadOnly = true;
            this.ColumnAgeUser.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnAgeUser.Width = 60;
            // 
            // ColumnVesToString
            // 
            this.ColumnVesToString.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnVesToString.DataPropertyName = "VesToString";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.Format = "N1";
            dataGridViewCellStyle12.NullValue = null;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ColumnVesToString.DefaultCellStyle = dataGridViewCellStyle12;
            this.ColumnVesToString.HeaderText = "Вес";
            this.ColumnVesToString.MinimumWidth = 25;
            this.ColumnVesToString.Name = "ColumnVesToString";
            this.ColumnVesToString.ReadOnly = true;
            this.ColumnVesToString.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnVesToString.ToolTipText = "Вес на соревновании";
            this.ColumnVesToString.Width = 65;
            // 
            // ColumnClubName
            // 
            this.ColumnClubName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnClubName.DataPropertyName = "ClubName";
            this.ColumnClubName.HeaderText = "Клуб";
            this.ColumnClubName.Name = "ColumnClubName";
            this.ColumnClubName.ReadOnly = true;
            this.ColumnClubName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // f_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 522);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.tsComp);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "f_Main";
            this.Text = "Черный леопард";
            this.Shown += new System.EventHandler(this.f_Main_Shown);
            this.Controls.SetChildIndex(this.menuStrip1, 0);
            this.Controls.SetChildIndex(this.tsComp, 0);
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdComp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsComp)).EndInit();
            this.tsComp.ResumeLayout(false);
            this.tsComp.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdCompUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsCompUsers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem smiFile;
        private System.Windows.Forms.ToolStripMenuItem smiClose;
        private System.Windows.Forms.ToolStripMenuItem smiSprav;
        private System.Windows.Forms.ToolStripMenuItem msiSpravFizLitas;
        private System.Windows.Forms.ToolStripMenuItem msiSpravPoyas;
        private System.Windows.Forms.ToolStripMenuItem msiVesKat;
        private System.Windows.Forms.ToolStripMenuItem msiSpravClub;
        private TkwEF.Controls.DataGridViewXF grdComp;
        private Core.Controls.BindingSourceX bsComp;
        private System.Windows.Forms.ToolStrip tsComp;
        protected internal System.Windows.Forms.ToolStripButton tsbAddComp;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private TkwEF.Controls.DataGridViewXF grdCompUsers;
        private Core.Controls.BindingSourceX bsCompUsers;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ToolStripButton tsbEditComp;
        private System.Windows.Forms.ToolStripMenuItem msiClubUsers;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDateBegin;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDateEnd;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnIdCu;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnFIO;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnAgeUser;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnVesToString;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnClubName;
    }
}

