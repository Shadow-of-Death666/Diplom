using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ROCO
{
    public partial class Update_worker : MetroFramework.Forms.MetroForm
    {
        public FormAdmin f;
        public int worker_id;
        General gen = new General();
        public MySqlConnection Connect = new MySqlConnection(Properties.Settings.Default.ConStr);
        public Update_worker(FormAdmin f, int worker_id)
        {
            InitializeComponent();
            this.Icon = Properties.Resources.WCL_1;
            this.f = f;
            this.worker_id = worker_id;
            label1.Left = panel1.Width / 2 - label1.Width / 2;
            button2.Left = panel1.Width / 2 - button2.Width / 2;
            button2.BackColor = Color.FromArgb(44, 183, 227);
            gen.position_for_cb(cb_position);
            gen.role_for_cb(cb_role);
            cb_role.SelectedIndex = gen.role_index(worker_id);
            cb_position.SelectedIndex = gen.position_index(worker_id);
            try
            {
                Connect.Open();
                MySqlCommand get_wrkr = new MySqlCommand("select * from `workers` where id="+worker_id,Connect);
                MySqlDataReader reader = get_wrkr.ExecuteReader();
                while (reader.Read())
                {
                    tb_fio.Text = Crypt.Crypt.Decrypt(reader[1].ToString());
                    tb_email.Text = Crypt.Crypt.Decrypt(reader[3].ToString());
                    tb_login.Text = Crypt.Crypt.Decrypt(reader[6].ToString());
                    tb_password.Text = Crypt.Crypt.Decrypt(reader[7].ToString());
                    tb_telephone.Text = Crypt.Crypt.Decrypt(reader[8].ToString());
                    metroDateTime1.Value = Convert.ToDateTime(reader[2]);
                    metroDateTime2.Value = Convert.ToDateTime(reader[4]);
                }
                Connect.Close();
            }
            catch (Exception ex)
            {
                metroProgressSpinner1.Visible = true;
                MyMessageBox.Show(Owner, ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, 150, 400);
                metroProgressSpinner1.Visible = false;
            }
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
                            MySqlCommand updt_wrkr = new MySqlCommand("UPDATE `workers` SET `fio`=@fio,`birth`=@birth,`corp_email`=@email,`employment`=@empl,`role_id`=@role,`login`=@login,`password`=@password,`telephone`=@tel,`position_id`=@pos WHERE id="+worker_id, Connect);
                            updt_wrkr.Parameters.AddWithValue("@fio", Crypt.Crypt.Encrypt(tb_fio.Text));
                            updt_wrkr.Parameters.AddWithValue("@birth", metroDateTime1.Value.ToString("yyyy-MM-dd"));
                            updt_wrkr.Parameters.AddWithValue("@email", Crypt.Crypt.Encrypt(tb_email.Text));
                            updt_wrkr.Parameters.AddWithValue("@empl", metroDateTime2.Value.ToString("yyyy-MM-dd"));
                            updt_wrkr.Parameters.AddWithValue("@login", Crypt.Crypt.Encrypt(tb_login.Text));
                            updt_wrkr.Parameters.AddWithValue("@password", Crypt.Crypt.Encrypt(tb_password.Text));
                            updt_wrkr.Parameters.AddWithValue("@tel", Crypt.Crypt.Encrypt(tb_telephone.Text));
                            updt_wrkr.Parameters.AddWithValue("@pos", ((KeyValuePair<string, string>)cb_position.SelectedItem).Key);
                            updt_wrkr.Parameters.AddWithValue("@role", ((KeyValuePair<string, string>)cb_role.SelectedItem).Key);
                            updt_wrkr.ExecuteNonQuery();
                            Connect.Close();
                            gen.workers(f.dg_workers);
                            gen.orders(f.dg_orders);
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
                            MyMessageBox.Show(Owner, "Информация о сотруднике успешно изменена", "Сообщение пользователю", MessageBoxButtons.OK, MessageBoxIcon.Information, 150, 400);
                            metroProgressSpinner1.Visible = false;
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

        private void Update_worker_Load(object sender, EventArgs e)
        {

        }
    }
}
