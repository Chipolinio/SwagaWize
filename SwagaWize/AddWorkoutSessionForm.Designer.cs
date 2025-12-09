namespace FitnessCenterApp.Forms
{
    partial class AddWorkoutSessionForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbTrainer = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpDateTime = new System.Windows.Forms.DateTimePicker();
            this.btnAddNew = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.dgvSessions = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            // --- НОВЫЕ ЭЛЕМЕНТЫ ---
            this.label5 = new System.Windows.Forms.Label();
            this.nudDuration = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.nudPrice = new System.Windows.Forms.NumericUpDown();
            // --- КОНЕЦ НОВЫХ ЭЛЕМЕНТОВ ---
            ((System.ComponentModel.ISupportInitialize)(this.dgvSessions)).BeginInit();
            // --- НОВЫЕ ЭЛЕМЕНТЫ (инициализация) ---
            ((System.ComponentModel.ISupportInitialize)(this.nudDuration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrice)).BeginInit();
            // --- КОНЕЦ ИНИЦИАЛИЗАЦИИ ---
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Тип:";
            // 
            // cmbType
            // 
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Location = new System.Drawing.Point(160, 25);
            this.cmbType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(265, 24);
            this.cmbType.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 62);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Тренер:";
            // 
            // cmbTrainer
            // 
            this.cmbTrainer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTrainer.FormattingEnabled = true;
            this.cmbTrainer.Location = new System.Drawing.Point(160, 62);
            this.cmbTrainer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbTrainer.Name = "cmbTrainer";
            this.cmbTrainer.Size = new System.Drawing.Size(265, 24);
            this.cmbTrainer.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 98);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Дата:";
            // 
            // dtpDateTime
            // 
            this.dtpDateTime.Location = new System.Drawing.Point(160, 98);
            this.dtpDateTime.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpDateTime.Name = "dtpDateTime";
            this.dtpDateTime.Size = new System.Drawing.Size(265, 22);
            this.dtpDateTime.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 125); // Под датой
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 16);
            this.label5.TabIndex = 13; // Увеличьте индекс
            this.label5.Text = "Длительность (мин):";
            // 
            // nudDuration
            // 
            this.nudDuration.Location = new System.Drawing.Point(160, 123); // Под датой
            this.nudDuration.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.nudDuration.Name = "nudDuration";
            this.nudDuration.Size = new System.Drawing.Size(120, 22); // Ширина как у других полей
            this.nudDuration.TabIndex = 6; // Установите нужный TabIndex
            this.nudDuration.Minimum = 1; // Минимальное значение
            this.nudDuration.Maximum = 999; // Максимальное значение
            this.nudDuration.Value = 60; // Значение по умолчанию
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(287, 125); // Рядом с длительностью
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 16);
            this.label6.TabIndex = 14; // Увеличьте индекс
            this.label6.Text = "Цена (руб):";
            // 
            // nudPrice
            // 
            this.nudPrice.DecimalPlaces = 2; // Для валюты
            this.nudPrice.Location = new System.Drawing.Point(373, 123); // Рядом с длительностью
            this.nudPrice.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.nudPrice.Name = "nudPrice";
            this.nudPrice.Size = new System.Drawing.Size(120, 22); // Ширина как у других полей
            this.nudPrice.TabIndex = 7; // Установите нужный TabIndex
            this.nudPrice.Minimum = 0; // Минимальное значение
            this.nudPrice.Maximum = 999999; // Максимальное значение
            this.nudPrice.Increment = 100; // Шаг изменения
            this.nudPrice.Value = 0; // Значение по умолчанию
            // 
            // btnAddNew
            // 
            this.btnAddNew.Location = new System.Drawing.Point(160, 160); // Ниже новых элементов
            this.btnAddNew.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(100, 28);
            this.btnAddNew.TabIndex = 8; // Увеличьте TabIndex
            this.btnAddNew.Text = "Новая";
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(267, 160); // Ниже новых элементов
            this.btnEdit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(100, 28);
            this.btnEdit.TabIndex = 9; // Увеличьте TabIndex
            this.btnEdit.Text = "Редакт.";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(373, 160); // Ниже новых элементов
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 28);
            this.btnDelete.TabIndex = 10; // Увеличьте TabIndex
            this.btnDelete.Text = "Удалить";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(480, 160); // Ниже новых элементов
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 28);
            this.btnSave.TabIndex = 11; // Увеличьте TabIndex
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(587, 160); // Ниже новых элементов
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 28);
            this.btnCancel.TabIndex = 12; // Увеличьте TabIndex
            this.btnCancel.Text = "Закрыть";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // dgvSessions
            // 
            this.dgvSessions.AllowUserToAddRows = false;
            this.dgvSessions.AllowUserToDeleteRows = false;
            this.dgvSessions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSessions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSessions.Location = new System.Drawing.Point(27, 200); // Ниже кнопок
            this.dgvSessions.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvSessions.Name = "dgvSessions";
            this.dgvSessions.ReadOnly = true;
            this.dgvSessions.RowHeadersWidth = 51;
            this.dgvSessions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSessions.Size = new System.Drawing.Size(800, 246);
            this.dgvSessions.TabIndex = 11;
            this.dgvSessions.SelectionChanged += new System.EventHandler(this.dgvSessions_SelectionChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(674, 200); // Ниже кнопок
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(153, 17);
            this.label4.TabIndex = 12;
            this.label4.Text = "Список тренировок";
            // 
            // AddWorkoutSessionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 455);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dgvSessions);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAddNew);
            // --- Добавляем новые элементы в Controls ---
            this.Controls.Add(this.nudPrice);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.nudDuration);
            this.Controls.Add(this.label5);
            // --- Конец добавления ---
            this.Controls.Add(this.dtpDateTime);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbTrainer);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbType);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddWorkoutSessionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Добавить тренировку";
            ((System.ComponentModel.ISupportInitialize)(this.dgvSessions)).EndInit();
            // --- НОВЫЕ ЭЛЕМЕНТЫ (EndInit) ---
            ((System.ComponentModel.ISupportInitialize)(this.nudDuration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrice)).EndInit();
            // --- КОНЕЦ EndInit ---
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbTrainer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpDateTime;
        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridView dgvSessions;
        private System.Windows.Forms.Label label4;
        // --- НОВЫЕ ПОЛЯ ---
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudDuration;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nudPrice;
        // --- КОНЕЦ НОВЫХ ПОЛЕЙ ---
    }
}