namespace ROCO
{
    partial class Settings_report
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cb_style_text = new MetroFramework.Controls.MetroComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tb_header_text = new MetroFramework.Controls.MetroTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cb_header = new System.Windows.Forms.CheckBox();
            this.cb_size_text = new MetroFramework.Controls.MetroComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cb_style_date = new MetroFramework.Controls.MetroComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dt_e_date = new MetroFramework.Controls.MetroDateTime();
            this.dt_s_date = new MetroFramework.Controls.MetroDateTime();
            this.cb_date = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.cb_show_id = new System.Windows.Forms.CheckBox();
            this.cb_field = new MetroFramework.Controls.MetroComboBox();
            this.cb_multi = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cb_html = new System.Windows.Forms.CheckBox();
            this.cb_price = new System.Windows.Forms.CheckBox();
            this.cb_start_date = new System.Windows.Forms.CheckBox();
            this.cb_status = new System.Windows.Forms.CheckBox();
            this.cb_theme = new System.Windows.Forms.CheckBox();
            this.cb_end_date = new System.Windows.Forms.CheckBox();
            this.cb_client = new System.Windows.Forms.CheckBox();
            this.cb_worker = new System.Windows.Forms.CheckBox();
            this.metroProgressSpinner1 = new MetroFramework.Controls.MetroProgressSpinner();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.cb_show_id);
            this.panel1.Controls.Add(this.cb_field);
            this.panel1.Controls.Add(this.cb_multi);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(888, 300);
            this.panel1.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cb_style_text);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.tb_header_text);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.cb_header);
            this.groupBox3.Controls.Add(this.cb_size_text);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Location = new System.Drawing.Point(612, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(264, 203);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Настройка Word документа";
            // 
            // cb_style_text
            // 
            this.cb_style_text.FormattingEnabled = true;
            this.cb_style_text.ItemHeight = 23;
            this.cb_style_text.Location = new System.Drawing.Point(53, 147);
            this.cb_style_text.Name = "cb_style_text";
            this.cb_style_text.Size = new System.Drawing.Size(202, 29);
            this.cb_style_text.TabIndex = 6;
            this.cb_style_text.UseSelectable = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 157);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Шрифт";
            // 
            // tb_header_text
            // 
            // 
            // 
            // 
            this.tb_header_text.CustomButton.Image = null;
            this.tb_header_text.CustomButton.Location = new System.Drawing.Point(227, 1);
            this.tb_header_text.CustomButton.Name = "";
            this.tb_header_text.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.tb_header_text.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tb_header_text.CustomButton.TabIndex = 1;
            this.tb_header_text.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tb_header_text.CustomButton.UseSelectable = true;
            this.tb_header_text.CustomButton.Visible = false;
            this.tb_header_text.Lines = new string[0];
            this.tb_header_text.Location = new System.Drawing.Point(6, 111);
            this.tb_header_text.MaxLength = 32767;
            this.tb_header_text.Name = "tb_header_text";
            this.tb_header_text.PasswordChar = '\0';
            this.tb_header_text.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tb_header_text.SelectedText = "";
            this.tb_header_text.SelectionLength = 0;
            this.tb_header_text.SelectionStart = 0;
            this.tb_header_text.ShortcutsEnabled = true;
            this.tb_header_text.Size = new System.Drawing.Size(249, 23);
            this.tb_header_text.TabIndex = 4;
            this.tb_header_text.UseSelectable = true;
            this.tb_header_text.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tb_header_text.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 95);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Текст заголовка";
            // 
            // cb_header
            // 
            this.cb_header.AutoSize = true;
            this.cb_header.Location = new System.Drawing.Point(6, 64);
            this.cb_header.Name = "cb_header";
            this.cb_header.Size = new System.Drawing.Size(129, 17);
            this.cb_header.TabIndex = 2;
            this.cb_header.Text = "Вставить заголовок";
            this.cb_header.UseVisualStyleBackColor = true;
            // 
            // cb_size_text
            // 
            this.cb_size_text.FormattingEnabled = true;
            this.cb_size_text.ItemHeight = 23;
            this.cb_size_text.Location = new System.Drawing.Point(95, 17);
            this.cb_size_text.Name = "cb_size_text";
            this.cb_size_text.Size = new System.Drawing.Size(163, 29);
            this.cb_size_text.TabIndex = 1;
            this.cb_size_text.UseSelectable = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Размер текста";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cb_style_date);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.dt_e_date);
            this.groupBox2.Controls.Add(this.dt_s_date);
            this.groupBox2.Controls.Add(this.cb_date);
            this.groupBox2.Location = new System.Drawing.Point(10, 99);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(590, 110);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Настройка поиска по диапазону дат";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(142, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Формат поиска";
            // 
            // cb_style_date
            // 
            this.cb_style_date.FormattingEnabled = true;
            this.cb_style_date.ItemHeight = 23;
            this.cb_style_date.Location = new System.Drawing.Point(232, 18);
            this.cb_style_date.Name = "cb_style_date";
            this.cb_style_date.Size = new System.Drawing.Size(342, 29);
            this.cb_style_date.TabIndex = 4;
            this.cb_style_date.UseSelectable = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(346, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "До";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(98, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "От";
            // 
            // dt_e_date
            // 
            this.dt_e_date.Location = new System.Drawing.Point(374, 54);
            this.dt_e_date.MinimumSize = new System.Drawing.Size(0, 29);
            this.dt_e_date.Name = "dt_e_date";
            this.dt_e_date.Size = new System.Drawing.Size(200, 29);
            this.dt_e_date.TabIndex = 2;
            // 
            // dt_s_date
            // 
            this.dt_s_date.Location = new System.Drawing.Point(124, 54);
            this.dt_s_date.MinimumSize = new System.Drawing.Size(0, 29);
            this.dt_s_date.Name = "dt_s_date";
            this.dt_s_date.Size = new System.Drawing.Size(200, 29);
            this.dt_s_date.TabIndex = 1;
            // 
            // cb_date
            // 
            this.cb_date.AutoSize = true;
            this.cb_date.Location = new System.Drawing.Point(9, 30);
            this.cb_date.Name = "cb_date";
            this.cb_date.Size = new System.Drawing.Size(75, 17);
            this.cb_date.TabIndex = 0;
            this.cb_date.Text = "Включить";
            this.cb_date.UseVisualStyleBackColor = true;
            this.cb_date.CheckStateChanged += new System.EventHandler(this.cb_date_CheckStateChanged);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.White;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button2.Location = new System.Drawing.Point(329, 234);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(300, 42);
            this.button2.TabIndex = 11;
            this.button2.Text = "Сохранить";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // cb_show_id
            // 
            this.cb_show_id.AutoSize = true;
            this.cb_show_id.Location = new System.Drawing.Point(19, 215);
            this.cb_show_id.Name = "cb_show_id";
            this.cb_show_id.Size = new System.Drawing.Size(204, 17);
            this.cb_show_id.TabIndex = 10;
            this.cb_show_id.Text = "Отображать id заказа в документе";
            this.cb_show_id.UseVisualStyleBackColor = true;
            // 
            // cb_field
            // 
            this.cb_field.FormattingEnabled = true;
            this.cb_field.ItemHeight = 23;
            this.cb_field.Location = new System.Drawing.Point(426, 58);
            this.cb_field.Name = "cb_field";
            this.cb_field.Size = new System.Drawing.Size(174, 29);
            this.cb_field.TabIndex = 9;
            this.cb_field.UseSelectable = true;
            this.cb_field.Visible = false;
            // 
            // cb_multi
            // 
            this.cb_multi.AutoSize = true;
            this.cb_multi.Location = new System.Drawing.Point(426, 35);
            this.cb_multi.Name = "cb_multi";
            this.cb_multi.Size = new System.Drawing.Size(180, 17);
            this.cb_multi.TabIndex = 8;
            this.cb_multi.Text = "Поиск только по одному полю";
            this.cb_multi.UseVisualStyleBackColor = true;
            this.cb_multi.CheckStateChanged += new System.EventHandler(this.cb_multi_CheckStateChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cb_html);
            this.groupBox1.Controls.Add(this.cb_price);
            this.groupBox1.Controls.Add(this.cb_start_date);
            this.groupBox1.Controls.Add(this.cb_status);
            this.groupBox1.Controls.Add(this.cb_theme);
            this.groupBox1.Controls.Add(this.cb_end_date);
            this.groupBox1.Controls.Add(this.cb_client);
            this.groupBox1.Controls.Add(this.cb_worker);
            this.groupBox1.Location = new System.Drawing.Point(10, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(403, 87);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Настройка отображаемых полей";
            // 
            // cb_html
            // 
            this.cb_html.AutoSize = true;
            this.cb_html.Location = new System.Drawing.Point(298, 52);
            this.cb_html.Name = "cb_html";
            this.cb_html.Size = new System.Drawing.Size(66, 17);
            this.cb_html.TabIndex = 0;
            this.cb_html.Text = "html код";
            this.cb_html.UseVisualStyleBackColor = true;
            // 
            // cb_price
            // 
            this.cb_price.AutoSize = true;
            this.cb_price.Location = new System.Drawing.Point(201, 52);
            this.cb_price.Name = "cb_price";
            this.cb_price.Size = new System.Drawing.Size(81, 17);
            this.cb_price.TabIndex = 0;
            this.cb_price.Text = "Стоимость";
            this.cb_price.UseVisualStyleBackColor = true;
            // 
            // cb_start_date
            // 
            this.cb_start_date.AutoSize = true;
            this.cb_start_date.Location = new System.Drawing.Point(298, 29);
            this.cb_start_date.Name = "cb_start_date";
            this.cb_start_date.Size = new System.Drawing.Size(90, 17);
            this.cb_start_date.TabIndex = 0;
            this.cb_start_date.Text = "Дата начала";
            this.cb_start_date.UseVisualStyleBackColor = true;
            // 
            // cb_status
            // 
            this.cb_status.AutoSize = true;
            this.cb_status.Location = new System.Drawing.Point(119, 52);
            this.cb_status.Name = "cb_status";
            this.cb_status.Size = new System.Drawing.Size(60, 17);
            this.cb_status.TabIndex = 0;
            this.cb_status.Text = "Статус";
            this.cb_status.UseVisualStyleBackColor = true;
            // 
            // cb_theme
            // 
            this.cb_theme.AutoSize = true;
            this.cb_theme.Location = new System.Drawing.Point(201, 29);
            this.cb_theme.Name = "cb_theme";
            this.cb_theme.Size = new System.Drawing.Size(53, 17);
            this.cb_theme.TabIndex = 0;
            this.cb_theme.Text = "Тема";
            this.cb_theme.UseVisualStyleBackColor = true;
            // 
            // cb_end_date
            // 
            this.cb_end_date.AutoSize = true;
            this.cb_end_date.Location = new System.Drawing.Point(9, 52);
            this.cb_end_date.Name = "cb_end_date";
            this.cb_end_date.Size = new System.Drawing.Size(94, 17);
            this.cb_end_date.TabIndex = 0;
            this.cb_end_date.Text = "Конечня дата";
            this.cb_end_date.UseVisualStyleBackColor = true;
            // 
            // cb_client
            // 
            this.cb_client.AutoSize = true;
            this.cb_client.Location = new System.Drawing.Point(119, 29);
            this.cb_client.Name = "cb_client";
            this.cb_client.Size = new System.Drawing.Size(62, 17);
            this.cb_client.TabIndex = 0;
            this.cb_client.Text = "Клиент";
            this.cb_client.UseVisualStyleBackColor = true;
            // 
            // cb_worker
            // 
            this.cb_worker.AutoSize = true;
            this.cb_worker.Location = new System.Drawing.Point(9, 29);
            this.cb_worker.Name = "cb_worker";
            this.cb_worker.Size = new System.Drawing.Size(79, 17);
            this.cb_worker.TabIndex = 0;
            this.cb_worker.Text = "Сотрудник";
            this.cb_worker.UseVisualStyleBackColor = true;
            // 
            // metroProgressSpinner1
            // 
            this.metroProgressSpinner1.Location = new System.Drawing.Point(846, 24);
            this.metroProgressSpinner1.Maximum = 100;
            this.metroProgressSpinner1.Name = "metroProgressSpinner1";
            this.metroProgressSpinner1.Size = new System.Drawing.Size(30, 30);
            this.metroProgressSpinner1.TabIndex = 24;
            this.metroProgressSpinner1.UseSelectable = true;
            this.metroProgressSpinner1.Value = 30;
            this.metroProgressSpinner1.Visible = false;
            // 
            // Settings_report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(888, 360);
            this.Controls.Add(this.metroProgressSpinner1);
            this.Controls.Add(this.panel1);
            this.MaximumSize = new System.Drawing.Size(888, 360);
            this.MinimumSize = new System.Drawing.Size(888, 360);
            this.Name = "Settings_report";
            this.Padding = new System.Windows.Forms.Padding(0, 60, 0, 0);
            this.Text = "Настройки отчета";
            this.Load += new System.EventHandler(this.Settings_report_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
        private MetroFramework.Controls.MetroProgressSpinner metroProgressSpinner1;
        public System.Windows.Forms.CheckBox cb_show_id;
        public MetroFramework.Controls.MetroComboBox cb_field;
        public System.Windows.Forms.CheckBox cb_multi;
        public System.Windows.Forms.CheckBox cb_html;
        public System.Windows.Forms.CheckBox cb_price;
        public System.Windows.Forms.CheckBox cb_start_date;
        public System.Windows.Forms.CheckBox cb_status;
        public System.Windows.Forms.CheckBox cb_theme;
        public System.Windows.Forms.CheckBox cb_end_date;
        public System.Windows.Forms.CheckBox cb_client;
        public System.Windows.Forms.CheckBox cb_worker;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public MetroFramework.Controls.MetroDateTime dt_e_date;
        public MetroFramework.Controls.MetroDateTime dt_s_date;
        public System.Windows.Forms.CheckBox cb_date;
        public MetroFramework.Controls.MetroComboBox cb_style_date;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label4;
        public MetroFramework.Controls.MetroComboBox cb_size_text;
        private System.Windows.Forms.Label label5;
        public MetroFramework.Controls.MetroTextBox tb_header_text;
        public System.Windows.Forms.CheckBox cb_header;
        private System.Windows.Forms.Label label6;
        public MetroFramework.Controls.MetroComboBox cb_style_text;
    }
}