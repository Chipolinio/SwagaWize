namespace FitnessCenterApp.Forms
{
    partial class UserDashboardForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvSessions;
        private System.Windows.Forms.ComboBox cmbWorkoutType;
        private System.Windows.Forms.DateTimePicker dtpDateFrom;
        private System.Windows.Forms.DateTimePicker dtpDateTo;
        private System.Windows.Forms.CheckBox chkFilterByDate;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Button btnResetFilter;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Button btnShowAchievements;
        private System.Windows.Forms.Button btnRandomWorkout;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dgvSessions = new System.Windows.Forms.DataGridView();
            this.cmbWorkoutType = new System.Windows.Forms.ComboBox();
            this.dtpDateFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpDateTo = new System.Windows.Forms.DateTimePicker();
            this.chkFilterByDate = new System.Windows.Forms.CheckBox();
            this.btnFilter = new System.Windows.Forms.Button();
            this.btnResetFilter = new System.Windows.Forms.Button();
            this.btnRegister = new System.Windows.Forms.Button();
            this.btnShowAchievements = new System.Windows.Forms.Button();
            this.btnRandomWorkout = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSessions)).BeginInit();
            this.SuspendLayout();

            // dgvSessions
            this.dgvSessions.AllowUserToAddRows = false;
            this.dgvSessions.AllowUserToDeleteRows = false;
            this.dgvSessions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSessions.BackgroundColor = System.Drawing.Color.White;
            this.dgvSessions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSessions.Location = new System.Drawing.Point(12, 100);
            this.dgvSessions.MultiSelect = false;
            this.dgvSessions.Name = "dgvSessions";
            this.dgvSessions.ReadOnly = true;
            this.dgvSessions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSessions.Size = new System.Drawing.Size(760, 350);
            this.dgvSessions.TabIndex = 0;

            // cmbWorkoutType
            this.cmbWorkoutType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWorkoutType.FormattingEnabled = true;
            this.cmbWorkoutType.Location = new System.Drawing.Point(120, 15);
            this.cmbWorkoutType.Name = "cmbWorkoutType";
            this.cmbWorkoutType.Size = new System.Drawing.Size(180, 21);
            this.cmbWorkoutType.TabIndex = 1;

            // dtpDateFrom
            this.dtpDateFrom.Enabled = false;
            this.dtpDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateFrom.Location = new System.Drawing.Point(430, 15);
            this.dtpDateFrom.Name = "dtpDateFrom";
            this.dtpDateFrom.Size = new System.Drawing.Size(100, 20);
            this.dtpDateFrom.TabIndex = 2;

            // dtpDateTo
            this.dtpDateTo.Enabled = false;
            this.dtpDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateTo.Location = new System.Drawing.Point(570, 15);
            this.dtpDateTo.Name = "dtpDateTo";
            this.dtpDateTo.Size = new System.Drawing.Size(100, 20);
            this.dtpDateTo.TabIndex = 3;

            // chkFilterByDate
            this.chkFilterByDate.AutoSize = true;
            this.chkFilterByDate.Location = new System.Drawing.Point(330, 17);
            this.chkFilterByDate.Name = "chkFilterByDate";
            this.chkFilterByDate.Size = new System.Drawing.Size(94, 17);
            this.chkFilterByDate.TabIndex = 4;
            this.chkFilterByDate.Text = "Фильтр даты";
            this.chkFilterByDate.UseVisualStyleBackColor = true;
            this.chkFilterByDate.CheckedChanged += new System.EventHandler(this.chkFilterByDate_CheckedChanged);

            // btnFilter
            this.btnFilter.BackColor = System.Drawing.Color.SteelBlue;
            this.btnFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilter.ForeColor = System.Drawing.Color.White;
            this.btnFilter.Location = new System.Drawing.Point(690, 12);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(80, 25);
            this.btnFilter.TabIndex = 5;
            this.btnFilter.Text = "Применить";
            this.btnFilter.UseVisualStyleBackColor = false;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);

            // btnResetFilter
            this.btnResetFilter.BackColor = System.Drawing.Color.Gray;
            this.btnResetFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetFilter.ForeColor = System.Drawing.Color.White;
            this.btnResetFilter.Location = new System.Drawing.Point(690, 45);
            this.btnResetFilter.Name = "btnResetFilter";
            this.btnResetFilter.Size = new System.Drawing.Size(80, 25);
            this.btnResetFilter.TabIndex = 6;
            this.btnResetFilter.Text = "Сбросить";
            this.btnResetFilter.UseVisualStyleBackColor = false;
            this.btnResetFilter.Click += new System.EventHandler(this.btnResetFilter_Click);

            // btnRegister
            this.btnRegister.BackColor = System.Drawing.Color.Green;
            this.btnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegister.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnRegister.ForeColor = System.Drawing.Color.White;
            this.btnRegister.Location = new System.Drawing.Point(12, 460);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(150, 35);
            this.btnRegister.TabIndex = 7;
            this.btnRegister.Text = "Записаться";
            this.btnRegister.UseVisualStyleBackColor = false;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);

            // btnShowAchievements
            this.btnShowAchievements.BackColor = System.Drawing.Color.Goldenrod;
            this.btnShowAchievements.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowAchievements.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnShowAchievements.ForeColor = System.Drawing.Color.White;
            this.btnShowAchievements.Location = new System.Drawing.Point(180, 460);
            this.btnShowAchievements.Name = "btnShowAchievements";
            this.btnShowAchievements.Size = new System.Drawing.Size(150, 35);
            this.btnShowAchievements.TabIndex = 8;
            this.btnShowAchievements.Text = "🏆 Мои достижения";
            this.btnShowAchievements.UseVisualStyleBackColor = false;
            this.btnShowAchievements.Click += new System.EventHandler(this.btnShowAchievements_Click);

            // btnRandomWorkout
            this.btnRandomWorkout.BackColor = System.Drawing.Color.Purple;
            this.btnRandomWorkout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRandomWorkout.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnRandomWorkout.ForeColor = System.Drawing.Color.White;
            this.btnRandomWorkout.Location = new System.Drawing.Point(350, 460);
            this.btnRandomWorkout.Name = "btnRandomWorkout";
            this.btnRandomWorkout.Size = new System.Drawing.Size(150, 35);
            this.btnRandomWorkout.TabIndex = 9;
            this.btnRandomWorkout.Text = "🎲 Случайная тренировка";
            this.btnRandomWorkout.UseVisualStyleBackColor = false;
            this.btnRandomWorkout.Click += new System.EventHandler(this.btnRandomWorkout_Click);

            // label1
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 15);
            this.label1.TabIndex = 10;
            this.label1.Text = "Тип тренировки:";

            // label2
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label2.Location = new System.Drawing.Point(12, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 15);
            this.label2.TabIndex = 11;
            this.label2.Text = "Расписание тренировок:";

            // label3
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label3.Location = new System.Drawing.Point(430, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 15);
            this.label3.TabIndex = 12;
            this.label3.Text = "С:";

            // label4
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label4.Location = new System.Drawing.Point(540, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 15);
            this.label4.TabIndex = 13;
            this.label4.Text = "По:";

            // UserDashboardForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(784, 507);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRandomWorkout);
            this.Controls.Add(this.btnShowAchievements);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.btnResetFilter);
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.chkFilterByDate);
            this.Controls.Add(this.dtpDateTo);
            this.Controls.Add(this.dtpDateFrom);
            this.Controls.Add(this.cmbWorkoutType);
            this.Controls.Add(this.dgvSessions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "UserDashboardForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Личный кабинет";
            this.Load += new System.EventHandler(this.UserDashboardForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSessions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}