namespace ROCO
{
    partial class db_string_connection
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
            this.str_conn = new MetroFramework.Controls.MetroLabel();
            this.SuspendLayout();
            // 
            // str_conn
            // 
            this.str_conn.AutoSize = true;
            this.str_conn.Location = new System.Drawing.Point(24, 75);
            this.str_conn.Name = "str_conn";
            this.str_conn.Size = new System.Drawing.Size(83, 19);
            this.str_conn.TabIndex = 0;
            this.str_conn.Text = "metroLabel1";
            // 
            // db_string_connection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(823, 122);
            this.Controls.Add(this.str_conn);
            this.Name = "db_string_connection";
            this.Padding = new System.Windows.Forms.Padding(0, 60, 0, 0);
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.Text = "Никому не показывайте эту строку!!!";
            this.Load += new System.EventHandler(this.db_string_connection_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroLabel str_conn;
    }
}