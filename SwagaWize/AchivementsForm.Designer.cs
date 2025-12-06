namespace FitnessCenterApp.Forms
{
    partial class AchievementsForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.FlowLayoutPanel flpAchievements;
        private System.Windows.Forms.Label lblTotalPoints;
        private System.Windows.Forms.Label lblUnlockedCount;
        private System.Windows.Forms.ProgressBar pbProgress;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Button btnClose;

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
            this.flpAchievements = new System.Windows.Forms.FlowLayoutPanel();
            this.lblTotalPoints = new System.Windows.Forms.Label();
            this.lblUnlockedCount = new System.Windows.Forms.Label();
            this.pbProgress = new System.Windows.Forms.ProgressBar();
            this.lblProgress = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // flpAchievements
            this.flpAchievements.AutoScroll = true;
            this.flpAchievements.BackColor = System.Drawing.Color.WhiteSmoke;
            this.flpAchievements.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpAchievements.Location = new System.Drawing.Point(12, 12);
            this.flpAchievements.Name = "flpAchievements";
            this.flpAchievements.Size = new System.Drawing.Size(760, 400);
            this.flpAchievements.TabIndex = 0;

            // lblTotalPoints
            this.lblTotalPoints.AutoSize = true;
            this.lblTotalPoints.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotalPoints.ForeColor = System.Drawing.Color.Blue;
            this.lblTotalPoints.Location = new System.Drawing.Point(12, 420);
            this.lblTotalPoints.Name = "lblTotalPoints";
            this.lblTotalPoints.Size = new System.Drawing.Size(108, 17);
            this.lblTotalPoints.TabIndex = 1;
            this.lblTotalPoints.Text = "Всего очков: 0";

            // lblUnlockedCount
            this.lblUnlockedCount.AutoSize = true;
            this.lblUnlockedCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblUnlockedCount.Location = new System.Drawing.Point(12, 445);
            this.lblUnlockedCount.Name = "lblUnlockedCount";
            this.lblUnlockedCount.Size = new System.Drawing.Size(105, 17);
            this.lblUnlockedCount.TabIndex = 2;
            this.lblUnlockedCount.Text = "Достижений: 0";

            // pbProgress
            this.pbProgress.Location = new System.Drawing.Point(250, 420);
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Size = new System.Drawing.Size(300, 20);
            this.pbProgress.TabIndex = 3;

            // lblProgress
            this.lblProgress.AutoSize = true;
            this.lblProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblProgress.Location = new System.Drawing.Point(556, 422);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(33, 15);
            this.lblProgress.TabIndex = 4;
            this.lblProgress.Text = "0%";

            // btnClose
            this.btnClose.BackColor = System.Drawing.Color.SteelBlue;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(672, 415);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 35);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            // AchievementsForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.pbProgress);
            this.Controls.Add(this.lblUnlockedCount);
            this.Controls.Add(this.lblTotalPoints);
            this.Controls.Add(this.flpAchievements);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AchievementsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Мои достижения";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}