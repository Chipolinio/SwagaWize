namespace FitnessCenterApp.Forms
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Button btnTestConnection;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Label lblQuote;
        private System.Windows.Forms.Button btnRefreshQuote;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;

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
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnRegister = new System.Windows.Forms.Button();
            this.btnTestConnection = new System.Windows.Forms.Button();
            this.lblError = new System.Windows.Forms.Label();
            this.lblQuote = new System.Windows.Forms.Label();
            this.btnRefreshQuote = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // txtUsername
            this.txtUsername.Location = new System.Drawing.Point(150, 50);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(200, 20);
            this.txtUsername.TabIndex = 0;

            // txtPassword
            this.txtPassword.Location = new System.Drawing.Point(150, 90);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(200, 20);
            this.txtPassword.TabIndex = 1;

            // btnLogin
            this.btnLogin.BackColor = System.Drawing.Color.SteelBlue;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(150, 130);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(95, 30);
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Text = "Войти";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);

            // btnRegister
            this.btnRegister.BackColor = System.Drawing.Color.Gray;
            this.btnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegister.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnRegister.ForeColor = System.Drawing.Color.White;
            this.btnRegister.Location = new System.Drawing.Point(255, 130);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(95, 30);
            this.btnRegister.TabIndex = 3;
            this.btnRegister.Text = "Регистрация";
            this.btnRegister.UseVisualStyleBackColor = false;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);

            // btnTestConnection
            this.btnTestConnection.BackColor = System.Drawing.Color.LightGray;
            this.btnTestConnection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTestConnection.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.btnTestConnection.ForeColor = System.Drawing.Color.Black;
            this.btnTestConnection.Location = new System.Drawing.Point(150, 170);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new System.Drawing.Size(200, 25);
            this.btnTestConnection.TabIndex = 4;
            this.btnTestConnection.Text = "Тест подключения к БД";
            this.btnTestConnection.UseVisualStyleBackColor = false;
            this.btnTestConnection.Click += new System.EventHandler(this.btnTestConnection_Click);

            // lblError
            this.lblError.AutoSize = true;
            this.lblError.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblError.ForeColor = System.Drawing.Color.Red;
            this.lblError.Location = new System.Drawing.Point(150, 200);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(0, 15);
            this.lblError.TabIndex = 5;

            // lblQuote
            this.lblQuote.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Italic);
            this.lblQuote.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblQuote.Location = new System.Drawing.Point(30, 230);
            this.lblQuote.Name = "lblQuote";
            this.lblQuote.Size = new System.Drawing.Size(440, 60);
            this.lblQuote.TabIndex = 6;
            this.lblQuote.Text = "💪 Мотивационная цитата";
            this.lblQuote.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblQuote.Visible = false;

            // btnRefreshQuote
            this.btnRefreshQuote.BackColor = System.Drawing.Color.Transparent;
            this.btnRefreshQuote.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefreshQuote.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.btnRefreshQuote.ForeColor = System.Drawing.Color.Blue;
            this.btnRefreshQuote.Location = new System.Drawing.Point(400, 290);
            this.btnRefreshQuote.Name = "btnRefreshQuote";
            this.btnRefreshQuote.Size = new System.Drawing.Size(70, 20);
            this.btnRefreshQuote.TabIndex = 7;
            this.btnRefreshQuote.Text = "Обновить";
            this.btnRefreshQuote.UseVisualStyleBackColor = false;
            this.btnRefreshQuote.Click += new System.EventHandler(this.btnRefreshQuote_Click);

            // label1
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label1.Location = new System.Drawing.Point(80, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 15);
            this.label1.TabIndex = 8;
            this.label1.Text = "Логин:";

            // label2
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label2.Location = new System.Drawing.Point(80, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 15);
            this.label2.TabIndex = 9;
            this.label2.Text = "Пароль:";

            // LoginForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(500, 320);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRefreshQuote);
            this.Controls.Add(this.lblQuote);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.btnTestConnection);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsername);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Фитнес-центр - Вход";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}