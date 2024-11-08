namespace TkwEF.UI.Sprav
{
    partial class f_SpravUserClub
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlMain = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grdUsers = new TkwEF.Controls.DataGridViewXF(this.components);
            this.ColumnId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnFIO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnAgeUser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCurPoyasName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Begin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.End = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bsUsers = new TkwEF.Core.Controls.BindingSourceX(this.components);
            this.labelGrdZagolovok1 = new TkwEF.Controls.TextBoxGrdZagolovok(this.components);
            this.tsUser = new System.Windows.Forms.ToolStrip();
            this.tsbAddUserClub = new System.Windows.Forms.ToolStripButton();
            this.tsbEditUserClub = new System.Windows.Forms.ToolStripButton();
            this.tsbSelect = new System.Windows.Forms.ToolStripButton();
            this.tsbChangePoyas = new System.Windows.Forms.ToolStripButton();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.cbxClub = new TkwEF.Controls.ClubsCbx(this.components);
            this.btnAddClub = new System.Windows.Forms.Button();
            this.pnlMain.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsUsers)).BeginInit();
            this.tsUser.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.ColumnCount = 1;
            this.pnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlMain.Controls.Add(this.panel1, 0, 2);
            this.pnlMain.Controls.Add(this.tsUser, 0, 1);
            this.pnlMain.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.RowCount = 3;
            this.pnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.pnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.pnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlMain.Size = new System.Drawing.Size(551, 372);
            this.pnlMain.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.grdUsers);
            this.panel1.Controls.Add(this.labelGrdZagolovok1);
            this.panel1.Location = new System.Drawing.Point(3, 73);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(545, 296);
            this.panel1.TabIndex = 5;
            // 
            // grdUsers
            // 
            this.grdUsers.AllowUserToAddRows = false;
            this.grdUsers.AllowUserToDeleteRows = false;
            this.grdUsers.AllowUserToOrderColumns = true;
            this.grdUsers.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grdUsers.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.grdUsers.AutoGenerateColumns = false;
            this.grdUsers.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Sienna;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdUsers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.grdUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdUsers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnId,
            this.ColumnFIO,
            this.ColumnAgeUser,
            this.ColumnCurPoyasName,
            this.Begin,
            this.End});
            this.grdUsers.DataSource = this.bsUsers;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.Sienna;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdUsers.DefaultCellStyle = dataGridViewCellStyle7;
            this.grdUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdUsers.EnableHeadersVisualStyles = false;
            this.grdUsers.Location = new System.Drawing.Point(0, 22);
            this.grdUsers.Name = "grdUsers";
            this.grdUsers.ReadOnly = true;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.Sienna;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.Chocolate;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdUsers.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.grdUsers.RowHeadersWidth = 34;
            this.grdUsers.ShowRowErrors = false;
            this.grdUsers.Size = new System.Drawing.Size(545, 274);
            this.grdUsers.TabIndex = 5;
            this.grdUsers.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grdUsers_CellMouseDoubleClick);
            this.grdUsers.CellMouseMove += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grdUsers_CellMouseMove);
            // 
            // ColumnId
            // 
            this.ColumnId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnId.DataPropertyName = "Id";
            this.ColumnId.HeaderText = "Код";
            this.ColumnId.MinimumWidth = 25;
            this.ColumnId.Name = "ColumnId";
            this.ColumnId.ReadOnly = true;
            this.ColumnId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnId.Width = 60;
            // 
            // ColumnFIO
            // 
            this.ColumnFIO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnFIO.DataPropertyName = "FIO";
            this.ColumnFIO.HeaderText = "ФИО";
            this.ColumnFIO.MinimumWidth = 50;
            this.ColumnFIO.Name = "ColumnFIO";
            this.ColumnFIO.ReadOnly = true;
            this.ColumnFIO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnFIO.ToolTipText = "Член клуба";
            this.ColumnFIO.Width = 150;
            // 
            // ColumnAgeUser
            // 
            this.ColumnAgeUser.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnAgeUser.DataPropertyName = "AgeUser";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ColumnAgeUser.DefaultCellStyle = dataGridViewCellStyle3;
            this.ColumnAgeUser.HeaderText = "Возраст";
            this.ColumnAgeUser.Name = "ColumnAgeUser";
            this.ColumnAgeUser.ReadOnly = true;
            this.ColumnAgeUser.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnAgeUser.Width = 60;
            // 
            // ColumnCurPoyasName
            // 
            this.ColumnCurPoyasName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnCurPoyasName.DataPropertyName = "CurPoyasName";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ColumnCurPoyasName.DefaultCellStyle = dataGridViewCellStyle4;
            this.ColumnCurPoyasName.HeaderText = "Пояс";
            this.ColumnCurPoyasName.MinimumWidth = 50;
            this.ColumnCurPoyasName.Name = "ColumnCurPoyasName";
            this.ColumnCurPoyasName.ReadOnly = true;
            this.ColumnCurPoyasName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Begin
            // 
            this.Begin.DataPropertyName = "Begin";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Format = "d";
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Begin.DefaultCellStyle = dataGridViewCellStyle5;
            this.Begin.HeaderText = "Вступил";
            this.Begin.Name = "Begin";
            this.Begin.ReadOnly = true;
            this.Begin.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // End
            // 
            this.End.DataPropertyName = "End";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Format = "d";
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.End.DefaultCellStyle = dataGridViewCellStyle6;
            this.End.HeaderText = "Вышел";
            this.End.Name = "End";
            this.End.ReadOnly = true;
            this.End.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // bsUsers
            // 
            this.bsUsers.DataMember = "UserClubs";
            // 
            // labelGrdZagolovok1
            // 
            this.labelGrdZagolovok1.BackColor = System.Drawing.Color.Sienna;
            this.labelGrdZagolovok1.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelGrdZagolovok1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelGrdZagolovok1.ForeColor = System.Drawing.Color.LemonChiffon;
            this.labelGrdZagolovok1.Location = new System.Drawing.Point(0, 0);
            this.labelGrdZagolovok1.Name = "labelGrdZagolovok1";
            this.labelGrdZagolovok1.Size = new System.Drawing.Size(545, 22);
            this.labelGrdZagolovok1.TabIndex = 7;
            this.labelGrdZagolovok1.Text = "Члены клуба";
            this.labelGrdZagolovok1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tsUser
            // 
            this.tsUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tsUser.Dock = System.Windows.Forms.DockStyle.None;
            this.tsUser.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAddUserClub,
            this.tsbEditUserClub,
            this.tsbSelect,
            this.tsbChangePoyas});
            this.tsUser.Location = new System.Drawing.Point(0, 45);
            this.tsUser.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.tsUser.Name = "tsUser";
            this.tsUser.Size = new System.Drawing.Size(551, 25);
            this.tsUser.TabIndex = 4;
            this.tsUser.Text = "toolStrip2";
            // 
            // tsbAddUserClub
            // 
            this.tsbAddUserClub.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAddUserClub.Image = global::TkwEF.Properties.Resources.плюс_2_50;
            this.tsbAddUserClub.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAddUserClub.Name = "tsbAddUserClub";
            this.tsbAddUserClub.Size = new System.Drawing.Size(23, 22);
            this.tsbAddUserClub.Text = "Добавить в члены клуба";
            this.tsbAddUserClub.Click += new System.EventHandler(this.tsbAddUserClub_Click);
            // 
            // tsbEditUserClub
            // 
            this.tsbEditUserClub.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbEditUserClub.Image = global::TkwEF.Properties.Resources.pencil_16;
            this.tsbEditUserClub.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEditUserClub.Name = "tsbEditUserClub";
            this.tsbEditUserClub.Size = new System.Drawing.Size(23, 22);
            this.tsbEditUserClub.Text = "Редактирование члена клуба";
            this.tsbEditUserClub.Click += new System.EventHandler(this.tsbEditUserClub_Click);
            // 
            // tsbSelect
            // 
            this.tsbSelect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSelect.Image = global::TkwEF.Properties.Resources.tick_16;
            this.tsbSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSelect.Name = "tsbSelect";
            this.tsbSelect.Size = new System.Drawing.Size(23, 22);
            this.tsbSelect.Text = "Выбрать";
            this.tsbSelect.Click += new System.EventHandler(this.tsbSelect_Click);
            // 
            // tsbChangePoyas
            // 
            this.tsbChangePoyas.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbChangePoyas.Image = global::TkwEF.Properties.Resources.icons8_swimmer_back_view_48px;
            this.tsbChangePoyas.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbChangePoyas.Name = "tsbChangePoyas";
            this.tsbChangePoyas.Size = new System.Drawing.Size(23, 22);
            this.tsbChangePoyas.Text = "Выбрать";
            this.tsbChangePoyas.ToolTipText = "Изменить пояс";
            this.tsbChangePoyas.Click += new System.EventHandler(this.tsbChangePoyas_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.cbxClub);
            this.flowLayoutPanel1.Controls.Add(this.btnAddClub);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(545, 29);
            this.flowLayoutPanel1.TabIndex = 5;
            // 
            // cbxClub
            // 
            this.cbxClub.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbxClub.DisplayMember = "Name";
            this.cbxClub.FirstRowText = "- Выберите клуб -";
            this.cbxClub.FormattingEnabled = true;
            this.cbxClub.Location = new System.Drawing.Point(3, 4);
            this.cbxClub.MaxDropDownItems = 30;
            this.cbxClub.Name = "cbxClub";
            this.cbxClub.NextControl = null;
            this.cbxClub.SelectedItem = null;
            this.cbxClub.Size = new System.Drawing.Size(225, 21);
            this.cbxClub.TabIndex = 0;
            this.cbxClub.ValueMember = "Id";
            this.cbxClub.Enter += new System.EventHandler(this.cbxClub_Enter);
            // 
            // btnAddClub
            // 
            this.btnAddClub.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnAddClub.AutoSize = true;
            this.btnAddClub.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnAddClub.Image = global::TkwEF.Properties.Resources.plus_circle_frame_16;
            this.btnAddClub.Location = new System.Drawing.Point(234, 3);
            this.btnAddClub.Name = "btnAddClub";
            this.btnAddClub.Size = new System.Drawing.Size(109, 23);
            this.btnAddClub.TabIndex = 1;
            this.btnAddClub.Text = "Добавить клуб";
            this.btnAddClub.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddClub.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAddClub.UseVisualStyleBackColor = true;
            this.btnAddClub.Click += new System.EventHandler(this.btnAddClub_Click);
            // 
            // f_SpravUserClub
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(551, 372);
            this.Controls.Add(this.pnlMain);
            this.Name = "f_SpravUserClub";
            this.Text = "Члены клуба";
            this.Load += new System.EventHandler(this.f_SpravUserClub_Load);
            this.Shown += new System.EventHandler(this.f_SpravUserClub_Shown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.f_SpravUserClub_MouseMove);
            this.Controls.SetChildIndex(this.pnlMain, 0);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsUsers)).EndInit();
            this.tsUser.ResumeLayout(false);
            this.tsUser.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel pnlMain;
        private Controls.ClubsCbx cbxClub;
        private System.Windows.Forms.Button btnAddClub;
        private System.Windows.Forms.ToolStrip tsUser;
        private System.Windows.Forms.ToolStripButton tsbAddUserClub;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private TkwEF.Controls.DataGridViewXF grdUsers;
        private Core.Controls.BindingSourceX bsUsers;
        private System.Windows.Forms.ToolStripButton tsbSelect;
        private System.Windows.Forms.ToolStripButton tsbEditUserClub;
        private System.Windows.Forms.Panel panel1;
        private Controls.TextBoxGrdZagolovok labelGrdZagolovok1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnFIO;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnAgeUser;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCurPoyasName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Begin;
        private System.Windows.Forms.DataGridViewTextBoxColumn End;
        private System.Windows.Forms.ToolStripButton tsbChangePoyas;
    }
}
