namespace FitnessCenterApp.Forms
{
    partial class CreateTableForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.chkLink = new System.Windows.Forms.CheckBox();
            this.cmbLinkTable = new System.Windows.Forms.ComboBox();
            this.labelLink = new System.Windows.Forms.Label();
            this.btnCreateTable = new System.Windows.Forms.Button();
            this.lstFields = new System.Windows.Forms.ListBox();
            this.btnRemoveField = new System.Windows.Forms.Button();
            this.btnAddField = new System.Windows.Forms.Button();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.txtFieldName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTableName = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnDeleteTable = new System.Windows.Forms.Button();
            this.txtDeleteTable = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(480, 380);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.chkLink);
            this.tabPage1.Controls.Add(this.cmbLinkTable);
            this.tabPage1.Controls.Add(this.labelLink);
            this.tabPage1.Controls.Add(this.btnCreateTable);
            this.tabPage1.Controls.Add(this.lstFields);
            this.tabPage1.Controls.Add(this.btnRemoveField);
            this.tabPage1.Controls.Add(this.btnAddField);
            this.tabPage1.Controls.Add(this.cmbType);
            this.tabPage1.Controls.Add(this.txtFieldName);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.txtTableName);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(10);
            this.tabPage1.Size = new System.Drawing.Size(472, 354);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Создать таблицу";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // chkLink
            // 
            this.chkLink.AutoSize = true;
            this.chkLink.Location = new System.Drawing.Point(30, 180);
            this.chkLink.Name = "chkLink";
            this.chkLink.Size = new System.Drawing.Size(220, 17);
            this.chkLink.TabIndex = 11;
            this.chkLink.Text = "Добавить поле-ссылку (напр. TrainerID)";
            this.chkLink.UseVisualStyleBackColor = true;
            // 
            // cmbLinkTable
            // 
            this.cmbLinkTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLinkTable.FormattingEnabled = true;
            this.cmbLinkTable.Location = new System.Drawing.Point(30, 150);
            this.cmbLinkTable.Name = "cmbLinkTable";
            this.cmbLinkTable.Size = new System.Drawing.Size(150, 21);
            this.cmbLinkTable.TabIndex = 10;
            // 
            // labelLink
            // 
            this.labelLink.AutoSize = true;
            this.labelLink.Location = new System.Drawing.Point(15, 130);
            this.labelLink.Name = "labelLink";
            this.labelLink.Size = new System.Drawing.Size(132, 13);
            this.labelLink.TabIndex = 9;
            this.labelLink.Text = "Связать с таблицей:";
            // 
            // btnCreateTable
            // 
            this.btnCreateTable.Location = new System.Drawing.Point(15, 310);
            this.btnCreateTable.Name = "btnCreateTable";
            this.btnCreateTable.Size = new System.Drawing.Size(100, 23);
            this.btnCreateTable.TabIndex = 8;
            this.btnCreateTable.Text = "Создать";
            this.btnCreateTable.UseVisualStyleBackColor = true;
            this.btnCreateTable.Click += new System.EventHandler(this.btnCreateTable_Click);
            // 
            // lstFields
            // 
            this.lstFields.FormattingEnabled = true;
            this.lstFields.Location = new System.Drawing.Point(15, 210);
            this.lstFields.Name = "lstFields";
            this.lstFields.Size = new System.Drawing.Size(250, 95);
            this.lstFields.TabIndex = 7;
            // 
            // btnRemoveField
            // 
            this.btnRemoveField.Location = new System.Drawing.Point(275, 240);
            this.btnRemoveField.Name = "btnRemoveField";
            this.btnRemoveField.Size = new System.Drawing.Size(90, 23);
            this.btnRemoveField.TabIndex = 6;
            this.btnRemoveField.Text = "Удалить поле";
            this.btnRemoveField.UseVisualStyleBackColor = true;
            this.btnRemoveField.Click += new System.EventHandler(this.btnRemoveField_Click);
            // 
            // btnAddField
            // 
            this.btnAddField.Location = new System.Drawing.Point(275, 105);
            this.btnAddField.Name = "btnAddField";
            this.btnAddField.Size = new System.Drawing.Size(90, 23);
            this.btnAddField.TabIndex = 5;
            this.btnAddField.Text = "Добавить";
            this.btnAddField.UseVisualStyleBackColor = true;
            this.btnAddField.Click += new System.EventHandler(this.btnAddField_Click);
            // 
            // cmbType
            // 
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Location = new System.Drawing.Point(100, 107);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(120, 21);
            this.cmbType.TabIndex = 4;
            // 
            // txtFieldName
            // 
            this.txtFieldName.Location = new System.Drawing.Point(100, 75);
            this.txtFieldName.Name = "txtFieldName";
            this.txtFieldName.Size = new System.Drawing.Size(120, 20);
            this.txtFieldName.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Тип данных:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Имя поля:";
            // 
            // txtTableName
            // 
            this.txtTableName.Location = new System.Drawing.Point(15, 30);
            this.txtTableName.Name = "txtTableName";
            this.txtTableName.Size = new System.Drawing.Size(200, 20);
            this.txtTableName.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnDeleteTable);
            this.tabPage2.Controls.Add(this.txtDeleteTable);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(10);
            this.tabPage2.Size = new System.Drawing.Size(472, 354);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Удалить таблицу";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnDeleteTable
            // 
            this.btnDeleteTable.Location = new System.Drawing.Point(15, 90);
            this.btnDeleteTable.Name = "btnDeleteTable";
            this.btnDeleteTable.Size = new System.Drawing.Size(100, 23);
            this.btnDeleteTable.TabIndex = 2;
            this.btnDeleteTable.Text = "Удалить";
            this.btnDeleteTable.UseVisualStyleBackColor = true;
            this.btnDeleteTable.Click += new System.EventHandler(this.btnDeleteTable_Click);
            // 
            // txtDeleteTable
            // 
            this.txtDeleteTable.Location = new System.Drawing.Point(15, 60);
            this.txtDeleteTable.Name = "txtDeleteTable";
            this.txtDeleteTable.Size = new System.Drawing.Size(200, 20);
            this.txtDeleteTable.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Имя таблицы для удаления:";
            // 
            // CreateTableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 380);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateTableForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Управление таблицами";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtTableName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFieldName;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Button btnAddField;
        private System.Windows.Forms.Button btnRemoveField;
        private System.Windows.Forms.ListBox lstFields;
        private System.Windows.Forms.Button btnCreateTable;
        private System.Windows.Forms.Button btnDeleteTable;
        private System.Windows.Forms.TextBox txtDeleteTable;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelLink;
        private System.Windows.Forms.ComboBox cmbLinkTable;
        private System.Windows.Forms.CheckBox chkLink;
    }
}