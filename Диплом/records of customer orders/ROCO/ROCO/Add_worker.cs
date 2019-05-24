using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ROCO
{
    public partial class Add_worker : MetroFramework.Forms.MetroForm
    {
        public MySqlConnection Connect = new MySqlConnection(Properties.Settings.Default.ConStr);
        FormAdmin f;
        General gen = new General();
        public Add_worker(FormAdmin f)
        {
            InitializeComponent();
            this.Icon = Properties.Resources.WCL_1;
            this.f = f;
            label1.Left = panel1.Width / 2 - label1.Width / 2;
            button2.Left = panel1.Width / 2 - button2.Width / 2;
            button2.BackColor = Color.FromArgb(44, 183, 227);
            gen.position_for_cb(cb_status);
        }

        private void Add_worker_Load(object sender, EventArgs e)
        {
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.FromArgb(53, 173, 220));
            pen.Width = 3;
            foreach (TextBox textBox in panel1.Controls.OfType<TextBox>())
            {
                e.Graphics.DrawLine(pen, textBox.Left, textBox.Top + textBox.Height, textBox.Left + textBox.Width, textBox.Top + textBox.Height);
                e.Graphics.DrawLine(pen, textBox.Left, textBox.Top, textBox.Left, textBox.Top + textBox.Height + 2);
                textBox.MaxLength = 100;
            }
            tb_fio.MaxLength = 100;
            tb_telephone.MaxLength = 11;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (tb_fio.Text.Length != 0 && tb_login.Text.Length != 0 && tb_password.Text.Length != 0)
                {
                    if (tb_email.Text.IndexOf("@") == -1)
                    {
                        metroProgressSpinner1.Visible = true;
                        MyMessageBox.Show(Owner, "Введена не корректная почта. Почта должна содержать символ '@'", "Предупреждение пользователю", MessageBoxButtons.OK, MessageBoxIcon.Warning, 150, 400);
                        metroProgressSpinner1.Visible = false;
                    }
                    else
                    {
                        Regex reg = new Regex("[0-9]{11}");
                        MatchCollection matches = reg.Matches(tb_telephone.Text);
                        if (matches.Count > 0)
                        {
                            Connect.Open();
                            MySqlCommand add_wrkr = new MySqlCommand("INSERT INTO `workers`(`id`, `fio`, `birth`, `corp_email`, `employment`, `role_id`, `login`, `password`, `telephone`, `position_id`) VALUES (null,@fio,@birth,@email,@empl,@role,@login,@pass,@tel,@pos)", Connect);
                            add_wrkr.Parameters.AddWithValue("@fio", Crypt.Crypt.Encrypt(tb_fio.Text));
                            add_wrkr.Parameters.AddWithValue("@birth", metroDateTime1.Value.ToString("yyyy-MM-dd"));
                            add_wrkr.Parameters.AddWithValue("@email", Crypt.Crypt.Encrypt(tb_email.Text));
                            add_wrkr.Parameters.AddWithValue("@empl", metroDateTime2.Value.ToString("yyyy-MM-dd"));
                            add_wrkr.Parameters.AddWithValue("@role", 1);
                            add_wrkr.Parameters.AddWithValue("@login", Crypt.Crypt.Encrypt(tb_login.Text));
                            add_wrkr.Parameters.AddWithValue("@pass", Crypt.Crypt.Encrypt(tb_password.Text));
                            add_wrkr.Parameters.AddWithValue("@tel", Crypt.Crypt.Encrypt(tb_telephone.Text));
                            add_wrkr.Parameters.AddWithValue("@pos", ((KeyValuePair<string, string>)cb_status.SelectedItem).Key);
                            add_wrkr.ExecuteNonQuery();
                            Connect.Close();
                            gen.workers(f.dg_workers);
                            //Обновление отчета
                            if (gen.get_setting(10))
                            {
                                gen.display_by_date(f.dg_report, gen);
                            }
                            else
                            {
                                gen.refresh_reports(f.dg_report, gen);
                            }
                            metroProgressSpinner1.Visible = true;
                            MyMessageBox.Show(Owner, "Сотрудник успешно добавлен в базу", "Сообщение пользователю", MessageBoxButtons.OK, MessageBoxIcon.Information, 150, 400);
                            metroProgressSpinner1.Visible = false;
                            foreach (TextBox textBox in panel1.Controls.OfType<TextBox>())
                            {
                                textBox.Clear();
                            }
                        }
                        else
                        {
                            metroProgressSpinner1.Visible = true;
                            MyMessageBox.Show(Owner, "Введен не корректный номер телефона", "Предупреждение пользователю", MessageBoxButtons.OK, MessageBoxIcon.Warning, 150, 400);
                            metroProgressSpinner1.Visible = false;
                        }
                    }
                }
                else
                {
                    metroProgressSpinner1.Visible = true;
                    MyMessageBox.Show(Owner, "Не все поля заполнены", "Предупреждение пользователю", MessageBoxButtons.OK, MessageBoxIcon.Warning, 150, 400);
                    metroProgressSpinner1.Visible = false;
                }
            }
            catch (Exception ex)
            {
                metroProgressSpinner1.Visible = true;
                MyMessageBox.Show(Owner, ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, 150, 400);
                metroProgressSpinner1.Visible = false;
            }
        }
    }
}
