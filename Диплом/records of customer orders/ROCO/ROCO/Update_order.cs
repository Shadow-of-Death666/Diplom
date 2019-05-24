using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ROCO
{
    public partial class Update_order : MetroFramework.Forms.MetroForm
    {
        public FormAdmin f;
        public FormMaster fm;
        User u;
        General gen = new General();
        public MySqlConnection Connect = new MySqlConnection(Properties.Settings.Default.ConStr);
        public int order_id;
        public Update_order(FormAdmin f, int order_id)
        {
            InitializeComponent();
            this.f = f;
            this.order_id = order_id;
            Properties.Settings.Default.FlagRole = true;
        }
        public Update_order(FormMaster f,User u, int order_id)
        {
            InitializeComponent();
            fm = f;
            this.u = u;
            this.order_id = order_id;
            Properties.Settings.Default.FlagRole = false;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.FromArgb(53, 173, 220));
            pen.Width = 3;
            foreach (TextBox textBox in panel1.Controls.OfType<TextBox>())
            {
                e.Graphics.DrawLine(pen, textBox.Left, textBox.Top + textBox.Height, textBox.Left + textBox.Width, textBox.Top + textBox.Height);
                e.Graphics.DrawLine(pen, textBox.Left, textBox.Top, textBox.Left, textBox.Top + textBox.Height + 2);
            }
            tb_price.MaxLength = 10;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (Properties.Settings.Default.FlagRole == true)
                {
                    if (tb_theme.Text.Length != 0 && tb_price.Text.Length != 0)
                    {
                        Regex reg = new Regex("[0-9]");
                        MatchCollection matches = reg.Matches(tb_price.Text);
                        if (matches.Count == tb_price.Text.Length)
                        {
                            MySqlCommand updt_rdr;
                            string query = "1";
                            if (metroDateTime2.Enabled == true)
                            {
                                updt_rdr = new MySqlCommand("UPDATE `orders` SET `worker_id`=@wrkr,`client_id`=@clnt,`theme`=@theme,`start_date`=@start,`end_date`=@end_date,`status_id`=@status,`price`=@price WHERE id=" + order_id, Connect);
                                updt_rdr.Parameters.AddWithValue("@end_date", metroDateTime2.Value.ToString("yyyy-MM-dd"));
                                query = "UPDATE `orders` SET ,`worker_id`=@wrkr,`client_id`=@clnt,`theme`=@theme,`start_date`=@start,`end_date`=@end_date,`status_id`=@status,`price`=@price WHERE id=" + order_id;
                            }
                            else
                            {
                                updt_rdr = new MySqlCommand("UPDATE `orders` SET `worker_id`=@wrkr,`client_id`=@clnt,`theme`=@theme,`start_date`=@start,`status_id`=@status,`price`=@price WHERE id=" + order_id, Connect);
                                query = "UPDATE `orders` SET ,`worker_id`=@wrkr,`client_id`=@clnt,`theme`=@theme,`start_date`=@start,`status_id`=@status,`price`=@price WHERE id=" + order_id;
                            }
                            updt_rdr.Parameters.AddWithValue("@wrkr", ((KeyValuePair<string, string>)cb_worker.SelectedItem).Key);
                            updt_rdr.Parameters.AddWithValue("@clnt", ((KeyValuePair<string, string>)cb_client.SelectedItem).Key);
                            updt_rdr.Parameters.AddWithValue("@theme", tb_theme.Text);
                            updt_rdr.Parameters.AddWithValue("@start", metroDateTime1.Value.ToString("yyyy-MM-dd"));
                            updt_rdr.Parameters.AddWithValue("@status", ((KeyValuePair<string, string>)cb_status.SelectedItem).Key);
                            updt_rdr.Parameters.AddWithValue("@price", tb_price.Text);
                            if (metroDateTime2.Value > DateTime.Now)
                            {
                                metroProgressSpinner1.Visible = true;
                                MyMessageBox.Show(Owner, "Указана некорректная дата завершения заказа", "Предупреждение пользователю", MessageBoxButtons.OK, MessageBoxIcon.Warning, 150, 400);
                                metroProgressSpinner1.Visible = false;
                            }
                            else
                            {
                                Connect.Open();
                                updt_rdr.ExecuteNonQuery();
                                Connect.Close();
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
                                MyMessageBox.Show(Owner, "Информация о заказе успешно изменена", "Сообщение пользователю", MessageBoxButtons.OK, MessageBoxIcon.Information, 150, 400);
                                metroProgressSpinner1.Visible = false;
                            }
                        }
                        else
                        {
                            metroProgressSpinner1.Visible = true;
                            MyMessageBox.Show(Owner, "Указана некорректная стоимость", "Предупреждение пользователю", MessageBoxButtons.OK, MessageBoxIcon.Warning, 150, 400);
                            metroProgressSpinner1.Visible = false;
                        }
                    }
                    else
                    {
                        metroProgressSpinner1.Visible = true;
                        MyMessageBox.Show(Owner, "Не все поля заполнены", "Предупреждение пользователю", MessageBoxButtons.OK, MessageBoxIcon.Warning, 150, 400);
                        metroProgressSpinner1.Visible = false;
                    }
                }
                else
                {
                    Connect.Open();
                    MySqlCommand updt_rdr = new MySqlCommand("UPDATE `orders` SET `status_id`=@status WHERE id=" + order_id,Connect);
                    updt_rdr.Parameters.AddWithValue("@status", ((KeyValuePair<string, string>)cb_status.SelectedItem).Key);
                    updt_rdr.ExecuteNonQuery();
                    Connect.Close();
                    u.orders(fm.dg_orders);
                    metroProgressSpinner1.Visible = true;
                    MyMessageBox.Show(Owner, "Информация о заказе успешно изменена", "Сообщение пользователю", MessageBoxButtons.OK, MessageBoxIcon.Information, 150, 400);
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.CheckState == CheckState.Checked)
            {
                metroDateTime2.Enabled = true;
            }
            else if(checkBox1.CheckState == CheckState.Unchecked)
            {
                metroDateTime2.Enabled = false;
            }
        }

        private void Update_order_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.WCL_1;
            label1.Left = panel1.Width / 2 - label1.Width / 2;
            button2.Left = panel1.Width / 2 - button2.Width / 2;
            button2.BackColor = Color.FromArgb(44, 183, 227);
            gen.client_for_cb(cb_client);
            gen.workers_for_cb(cb_worker);
            try
            {
                Connect.Open();
                MySqlCommand order_data = new MySqlCommand("select * from `orders` where id=" + order_id, Connect);
                MySqlDataReader reader = order_data.ExecuteReader();
                while (reader.Read())
                {
                    DateTime dt = DateTime.Now;
                    if (reader[5].ToString() != "")
                    {
                        dt = Convert.ToDateTime(reader[5]);
                        metroDateTime2.Value = dt;
                        checkBox1.Visible = false;
                    }
                    else
                    {
                        metroDateTime2.Enabled = false;
                        checkBox1.Visible = true;
                    }
                    Order rdr = new Order(Convert.ToInt32(reader[0]),
                        Convert.ToInt32(reader[1]), Convert.ToInt32(reader[2]),
                        reader[3].ToString(), Convert.ToDateTime(reader[4]),
                        dt,
                        Convert.ToInt32(reader[6]), Convert.ToInt32(reader[7]));
                    tb_price.Text = rdr.Price.ToString();
                    tb_theme.Text = rdr.Theme;
                    metroDateTime1.Value = rdr.Start_date;
                    cb_client.Text = rdr.client();
                    cb_worker.Text = rdr.worker();
                }
                Connect.Close();
                if (Properties.Settings.Default.FlagRole == false)
                {
                    gen.status_for_cb_master(cb_status, gen.get_status_id(order_id));
                    foreach (TextBox t in panel1.Controls.OfType<TextBox>())
                    {
                        t.Enabled = false;
                    }
                    foreach (DateTimePicker t in panel1.Controls.OfType<DateTimePicker>())
                    {
                        t.Enabled = false;
                    }
                    checkBox1.Visible = false;
                    foreach (ComboBox t in panel1.Controls.OfType<ComboBox>())
                    {
                        t.Enabled = false;
                    }
                    cb_status.Enabled = true;
                    if (cb_status.Items.Count == 0)
                    {
                        gen.get_status_name(cb_status,order_id);
                        cb_status.Enabled = false;
                        button2.Enabled = false;
                    }
                }
                else {
                    gen.status_for_cb(cb_status,gen.get_status_id(order_id));
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
