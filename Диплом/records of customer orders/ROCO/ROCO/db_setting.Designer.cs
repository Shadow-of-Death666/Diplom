namespace ROCO
{
    partial class db_setting
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
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.serv = new System.Windows.Forms.TextBox();
            this.db = new System.Windows.Forms.TextBox();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.pass = new System.Windows.Forms.TextBox();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.user = new System.Windows.Forms.TextBox();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.port = new System.Windows.Forms.TextBox();
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.metroButton1 = new MetroFramework.Controls.MetroButton();
            this.metroButton2 = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(26, 83);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(46, 19);
            this.metroLabel1.TabIndex = 0;
            this.metroLabel1.Text = "Server";
            // 
            // serv
            // 
            this.serv.Location = new System.Drawing.Point(96, 82);
            this.serv.Name = "serv";
            this.serv.Size = new System.Drawing.Size(247, 20);
            this.serv.TabIndex = 1;
            // 
            // db
            // 
            this.db.Location = new System.Drawing.Point(96, 136);
            this.db.Name = "db";
            this.db.Size = new System.Drawing.Size(247, 20);
            this.db.TabIndex = 3;
            this.db.KeyUp += new System.Windows.Forms.KeyEventHandler(this.db_KeyUp);
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(26, 137);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(63, 19);
            this.metroLabel2.TabIndex = 2;
            this.metroLabel2.Text = "Database";
            // 
            // pass
            // 
            this.pass.Location = new System.Drawing.Point(96, 190);
            this.pass.Name = "pass";
            this.pass.Size = new System.Drawing.Size(247, 20);
            this.pass.TabIndex = 5;
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(26, 191);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(64, 19);
            this.metroLabel3.TabIndex = 4;
            this.metroLabel3.Text = "Password";
            // 
            // user
            // 
            this.user.Location = new System.Drawing.Point(96, 163);
            this.user.Name = "user";
            this.user.Size = new System.Drawing.Size(247, 20);
            this.user.TabIndex = 7;
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.Location = new System.Drawing.Point(26, 164);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(35, 19);
            this.metroLabel4.TabIndex = 6;
            this.metroLabel4.Text = "User";
            // 
            // port
            // 
            this.port.Location = new System.Drawing.Point(96, 109);
            this.port.Name = "port";
            this.port.Size = new System.Drawing.Size(247, 20);
            this.port.TabIndex = 9;
            // 
            // metroLabel5
            // 
            this.metroLabel5.AutoSize = true;
            this.metroLabel5.Location = new System.Drawing.Point(26, 110);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(34, 19);
            this.metroLabel5.TabIndex = 8;
            this.metroLabel5.Text = "Port";
            // 
            // metroButton1
            // 
            this.metroButton1.BackColor = System.Drawing.Color.White;
            this.metroButton1.Location = new System.Drawing.Point(26, 225);
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.Size = new System.Drawing.Size(317, 23);
            this.metroButton1.TabIndex = 10;
            this.metroButton1.Text = "Сохранить на этот сеанс";
            this.metroButton1.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroButton1.UseSelectable = true;
            this.metroButton1.Click += new System.EventHandler(this.metroButton1_Click);
            // 
            // metroButton2
            // 
            this.metroButton2.BackColor = System.Drawing.Color.White;
            this.metroButton2.Location = new System.Drawing.Point(26, 254);
            this.metroButton2.Name = "metroButton2";
            this.metroButton2.Size = new System.Drawing.Size(317, 23);
            this.metroButton2.TabIndex = 10;
            this.metroButton2.Text = "Сохранить навсегда";
            this.metroButton2.UseSelectable = true;
            this.metroButton2.Click += new System.EventHandler(this.metroButton2_Click);
            // 
            // db_setting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 303);
            this.Controls.Add(this.metroButton2);
            this.Controls.Add(this.metroButton1);
            this.Controls.Add(this.port);
            this.Controls.Add(this.metroLabel5);
            this.Controls.Add(this.user);
            this.Controls.Add(this.metroLabel4);
            this.Controls.Add(this.pass);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.db);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.serv);
            this.Controls.Add(this.metroLabel1);
            this.MaximumSize = new System.Drawing.Size(373, 303);
            this.MinimumSize = new System.Drawing.Size(373, 303);
            this.Name = "db_setting";
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.Text = "Настройки подключения к БД";
            this.Theme = MetroFramework.MetroThemeStyle.Default;
            this.Load += new System.EventHandler(this.db_setting_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroLabel metroLabel1;
        private System.Windows.Forms.TextBox serv;
        private System.Windows.Forms.TextBox db;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private System.Windows.Forms.TextBox pass;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private System.Windows.Forms.TextBox user;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private System.Windows.Forms.TextBox port;
        private MetroFramework.Controls.MetroLabel metroLabel5;
        private MetroFramework.Controls.MetroButton metroButton1;
        private MetroFramework.Controls.MetroButton metroButton2;
    }
}