namespace FitnessCenterApp.Forms
{
    partial class AdminForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnManageTrainers;
        private System.Windows.Forms.Button btnAddSession;
        private System.Windows.Forms.Button btnManageTables;
        private System.Windows.Forms.Button btnReports;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label label1;

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
            this.btnManageTrainers = new System.Windows.Forms.Button();
            this.btnAddSession = new System.Windows.Forms.Button();
            this.btnManageTables = new System.Windows.Forms.Button();
            this.btnReports = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // btnManageTrainers
            this.btnManageTrainers.BackColor = System.Drawing.Color.SteelBlue;
            this.btnManageTrainers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnManageTrainers.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnManageTrainers.ForeColor = System.Drawing.Color.White;
            this.btnManageTrainers.Location = new System.Drawing.Point(100, 120);
            this.btnManageTrainers.Name = "btnManageTrainers";
            this.btnManageTrainers.Size = new System.Drawing.Size(200, 50);
            this.btnManageTrainers.TabIndex = 0;
            this.btnManageTrainers.Text = "👥 Управление тренерами";
            this.btnManageTrainers.UseVisualStyleBackColor = false;
            this.btnManageTrainers.Click += new System.EventHandler(this.btnManageTrainers_Click);

            // btnAddSession
            this.btnAddSession.BackColor = System.Drawing.Color.Green;
            this.btnAddSession.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddSession.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnAddSession.ForeColor = System.Drawing.Color.White;
            this.btnAddSession.Location = new System.Drawing.Point(100, 180);
            this.btnAddSession.Name = "btnAddSession";
            this.btnAddSession.Size = new System.Drawing.Size(200, 50);
            this.btnAddSession.TabIndex = 1;
            this.btnAddSession.Text = "➕ Добавить тренировку";
            this.btnAddSession.UseVisualStyleBackColor = false;
            this.btnAddSession.Click += new System.EventHandler(this.btnAddSession_Click);

            // btnManageTables
            this.btnManageTables.BackColor = System.Drawing.Color.Purple;
            this.btnManageTables.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnManageTables.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnManageTables.ForeColor = System.Drawing.Color.White;
            this.btnManageTables.Location = new System.Drawing.Point(100, 240);
            this.btnManageTables.Name = "btnManageTables";
            this.btnManageTables.Size = new System.Drawing.Size(200, 50);
            this.btnManageTables.TabIndex = 2;
            this.btnManageTables.Text = "🗃️ Управление таблицами";
            this.btnManageTables.UseVisualStyleBackColor = false;
            this.btnManageTables.Click += new System.EventHandler(this.btnManageTables_Click);

            // btnReports
            this.btnReports.BackColor = System.Drawing.Color.Teal;
            this.btnReports.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReports.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnReports.ForeColor = System.Drawing.Color.White;
            this.btnReports.Location = new System.Drawing.Point(100, 300);
            this.btnReports.Name = "btnReports";
            this.btnReports.Size = new System.Drawing.Size(200, 50);
            this.btnReports.TabIndex = 3;
            this.btnReports.Text = "📊 Генерация отчетов";
            this.btnReports.UseVisualStyleBackColor = false;
            this.btnReports.Click += new System.EventHandler(this.btnReports_Click);

            // btnExit
            this.btnExit.BackColor = System.Drawing.Color.Crimson;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(100, 360);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(200, 50);
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "🚪 Выход";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);

            // label1
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(50, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(300, 31);
            this.label1.TabIndex = 5;
            this.label1.Text = "Панель администратора";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // AdminForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(400, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnReports);
            this.Controls.Add(this.btnManageTables);
            this.Controls.Add(this.btnAddSession);
            this.Controls.Add(this.btnManageTrainers);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "AdminForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Фитнес-центр - Администратор";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}