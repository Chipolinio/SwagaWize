namespace FitnessCenterApp.Forms
{
    partial class CreateTableForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnDeleteTable = new System.Windows.Forms.Button();
            this.btnViewStructure = new System.Windows.Forms.Button();
            this.dgvExistingTables = new System.Windows.Forms.DataGridView();
            this.lstStructure = new System.Windows.Forms.ListBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.chkForeignKey = new System.Windows.Forms.CheckBox();
            this.cmbRefColumn = new System.Windows.Forms.ComboBox();
            this.cmbRefTable = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCreateTable = new System.Windows.Forms.Button();
            this.lstColumns = new System.Windows.Forms.ListBox();
            this.btnRemoveColumn = new System.Windows.Forms.Button();
            this.btnAddColumn = new System.Windows.Forms.Button();
            this.cmbColType = new System.Windows.Forms.ComboBox();
            this.txtColName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNewTableName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExistingTables)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(760, 438);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnDeleteTable);
            this.tabPage1.Controls.Add(this.btnViewStructure);
            this.tabPage1.Controls.Add(this.dgvExistingTables);
            this.tabPage1.Controls.Add(this.lstStructure);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(752, 412);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Существующие таблицы";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnDeleteTable
            // 
            this.btnDeleteTable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDeleteTable.BackColor = System.Drawing.Color.IndianRed;
            this.btnDeleteTable.ForeColor = System.Drawing.Color.White;
            this.btnDeleteTable.Location = new System.Drawing.Point(168, 377);
            this.btnDeleteTable.Name = "btnDeleteTable";
            this.btnDeleteTable.Size = new System.Drawing.Size(120, 23);
            this.btnDeleteTable.TabIndex = 3;
            this.btnDeleteTable.Text = "Удалить таблицу";
            this.btnDeleteTable.UseVisualStyleBackColor = false;
            this.btnDeleteTable.Click += new System.EventHandler(this.btnDeleteTable_Click);
            // 
            // btnViewStructure
            // 
            this.btnViewStructure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnViewStructure.Location = new System.Drawing.Point(8, 377);
            this.btnViewStructure.Name = "btnViewStructure";
            this.btnViewStructure.Size = new System.Drawing.Size(120, 23);
            this.btnViewStructure.TabIndex = 2;
            this.btnViewStructure.Text = "Структура";
            this.btnViewStructure.UseVisualStyleBackColor = true;
            this.btnViewStructure.Click += new System.EventHandler(this.btnViewStructure_Click);
            // 
            // dgvExistingTables
            // 
            this.dgvExistingTables.AllowUserToAddRows = false;
            this.dgvExistingTables.AllowUserToDeleteRows = false;
            this.dgvExistingTables.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvExistingTables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExistingTables.Location = new System.Drawing.Point(8, 6);
            this.dgvExistingTables.Name = "dgvExistingTables";
            this.dgvExistingTables.ReadOnly = true;
            this.dgvExistingTables.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvExistingTables.Size = new System.Drawing.Size(350, 365);
            this.dgvExistingTables.TabIndex = 0;
            // 
            // lstStructure
            // 
            this.lstStructure.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstStructure.FormattingEnabled = true;
            this.lstStructure.Location = new System.Drawing.Point(364, 6);
            this.lstStructure.Name = "lstStructure";
            this.lstStructure.Size = new System.Drawing.Size(382, 368);
            this.lstStructure.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.chkForeignKey);
            this.tabPage2.Controls.Add(this.cmbRefColumn);
            this.tabPage2.Controls.Add(this.cmbRefTable);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.btnCreateTable);
            this.tabPage2.Controls.Add(this.lstColumns);
            this.tabPage2.Controls.Add(this.btnRemoveColumn);
            this.tabPage2.Controls.Add(this.btnAddColumn);
            this.tabPage2.Controls.Add(this.cmbColType);
            this.tabPage2.Controls.Add(this.txtColName);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.txtNewTableName);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(752, 412);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Создать таблицу";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // chkForeignKey
            // 
            this.chkForeignKey.AutoSize = true;
            this.chkForeignKey.Location = new System.Drawing.Point(20, 105);
            this.chkForeignKey.Name = "chkForeignKey";
            this.chkForeignKey.Size = new System.Drawing.Size(112, 17);
            this.chkForeignKey.TabIndex = 14;
            this.chkForeignKey.Text = "Внешний ключ";
            this.chkForeignKey.UseVisualStyleBackColor = true;
            this.chkForeignKey.CheckedChanged += new System.EventHandler(this.chkForeignKey_CheckedChanged);
            // 
            // cmbRefColumn
            // 
            this.cmbRefColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRefColumn.FormattingEnabled = true;
            this.cmbRefColumn.Location = new System.Drawing.Point(530, 102);
            this.cmbRefColumn.Name = "cmbRefColumn";
            this.cmbRefColumn.Size = new System.Drawing.Size(150, 21);
            this.cmbRefColumn.TabIndex = 13;
            // 
            // cmbRefTable
            // 
            this.cmbRefTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRefTable.FormattingEnabled = true;
            this.cmbRefTable.Location = new System.Drawing.Point(350, 102);
            this.cmbRefTable.Name = "cmbRefTable";
            this.cmbRefTable.Size = new System.Drawing.Size(150, 21);
            this.cmbRefTable.TabIndex = 12;
            this.cmbRefTable.SelectedIndexChanged += new System.EventHandler(this.cmbRefTable_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(506, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(18, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "на";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(295, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Ссылка:";
            // 
            // btnCreateTable
            // 
            this.btnCreateTable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCreateTable.BackColor = System.Drawing.Color.SeaGreen;
            this.btnCreateTable.ForeColor = System.Drawing.Color.White;
            this.btnCreateTable.Location = new System.Drawing.Point(20, 377);
            this.btnCreateTable.Name = "btnCreateTable";
            this.btnCreateTable.Size = new System.Drawing.Size(150, 23);
            this.btnCreateTable.TabIndex = 9;
            this.btnCreateTable.Text = "Создать таблицу";
            this.btnCreateTable.UseVisualStyleBackColor = false;
            this.btnCreateTable.Click += new System.EventHandler(this.btnCreateTable_Click);
            // 
            // lstColumns
            // 
            this.lstColumns.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstColumns.FormattingEnabled = true;
            this.lstColumns.Location = new System.Drawing.Point(20, 180);
            this.lstColumns.Name = "lstColumns";
            this.lstColumns.Size = new System.Drawing.Size(660, 186);
            this.lstColumns.TabIndex = 8;
            // 
            // btnRemoveColumn
            // 
            this.btnRemoveColumn.Location = new System.Drawing.Point(150, 140);
            this.btnRemoveColumn.Name = "btnRemoveColumn";
            this.btnRemoveColumn.Size = new System.Drawing.Size(100, 23);
            this.btnRemoveColumn.TabIndex = 7;
            this.btnRemoveColumn.Text = "Удалить колонку";
            this.btnRemoveColumn.UseVisualStyleBackColor = true;
            this.btnRemoveColumn.Click += new System.EventHandler(this.btnRemoveColumn_Click);
            // 
            // btnAddColumn
            // 
            this.btnAddColumn.Location = new System.Drawing.Point(20, 140);
            this.btnAddColumn.Name = "btnAddColumn";
            this.btnAddColumn.Size = new System.Drawing.Size(100, 23);
            this.btnAddColumn.TabIndex = 6;
            this.btnAddColumn.Text = "Добавить колонку";
            this.btnAddColumn.UseVisualStyleBackColor = true;
            this.btnAddColumn.Click += new System.EventHandler(this.btnAddColumn_Click);
            // 
            // cmbColType
            // 
            this.cmbColType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbColType.FormattingEnabled = true;
            this.cmbColType.Location = new System.Drawing.Point(100, 60);
            this.cmbColType.Name = "cmbColType";
            this.cmbColType.Size = new System.Drawing.Size(150, 21);
            this.cmbColType.TabIndex = 5;
            // 
            // txtColName
            // 
            this.txtColName.Location = new System.Drawing.Point(100, 35);
            this.txtColName.Name = "txtColName";
            this.txtColName.Size = new System.Drawing.Size(150, 20);
            this.txtColName.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Тип данных:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Имя колонки:";
            // 
            // txtNewTableName
            // 
            this.txtNewTableName.Location = new System.Drawing.Point(100, 10);
            this.txtNewTableName.Name = "txtNewTableName";
            this.txtNewTableName.Size = new System.Drawing.Size(150, 20);
            this.txtNewTableName.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Имя таблицы:";
            // 
            // CreateTableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 462);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "CreateTableForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Конструктор таблиц";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvExistingTables)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dgvExistingTables;
        private System.Windows.Forms.Button btnViewStructure;
        private System.Windows.Forms.Button btnDeleteTable;
        private System.Windows.Forms.ListBox lstStructure;
        private System.Windows.Forms.TextBox txtNewTableName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtColName;
        private System.Windows.Forms.ComboBox cmbColType;
        private System.Windows.Forms.Button btnAddColumn;
        private System.Windows.Forms.Button btnRemoveColumn;
        private System.Windows.Forms.ListBox lstColumns;
        private System.Windows.Forms.Button btnCreateTable;
        private System.Windows.Forms.CheckBox chkForeignKey;
        private System.Windows.Forms.ComboBox cmbRefColumn;
        private System.Windows.Forms.ComboBox cmbRefTable;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
    }
}