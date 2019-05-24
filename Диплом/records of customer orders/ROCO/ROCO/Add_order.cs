using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ROCO
{
    public partial class Add_order : MetroFramework.Forms.MetroForm
    {
        public FormAdmin f;
        General gen = new General();
        public MySqlConnection Connect = new MySqlConnection(Properties.Settings.Default.ConStr);
        public Add_order(FormAdmin f)
        {
            InitializeComponent();
            this.Icon = Properties.Resources.WCL_1;
            this.f = f;
            label1.Left = panel1.Width / 2 - label1.Width / 2;
            button2.Left = panel1.Width / 2 - button2.Width / 2;
            button2.BackColor = Color.FromArgb(44, 183, 227);
        }

        private void Add_order_Load(object sender, EventArgs e)
        {
            gen.workers_for_cb(cb_worker);
            gen.client_for_cb(cb_client);
            gen.status_for_cb(cb_status);
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
                if (tb_theme.Text.Length != 0 && tb_price.Text.Length != 0)
                {
                    Regex reg = new Regex("[0-9]");
                    MatchCollection matches = reg.Matches(tb_price.Text);
                    if (matches.Count == tb_price.Text.Length)
                    {
                        Connect.Open();
                        MySqlCommand add_rdr = new MySqlCommand("INSERT INTO `orders`(`id`, `worker_id`, `client_id`, `theme`, `start_date`, `end_date`, `status_id`, `price`) VALUES (null,@wrkr,@clnt,@theme,@start,null,@status,@price)", Connect);
                        add_rdr.Parameters.AddWithValue("@wrkr", ((KeyValuePair<string, string>)cb_worker.SelectedItem).Key);
                        add_rdr.Parameters.AddWithValue("@clnt", ((KeyValuePair<string, string>)cb_client.SelectedItem).Key);
                        add_rdr.Parameters.AddWithValue("@status", ((KeyValuePair<string, string>)cb_status.SelectedItem).Key);
                        add_rdr.Parameters.AddWithValue("@theme",tb_theme.Text);
                        add_rdr.Parameters.AddWithValue("@start", metroDateTime1.Value.ToString("yyyy-MM-dd"));
                        add_rdr.Parameters.AddWithValue("@price", tb_price.Text);
                        add_rdr.ExecuteNonQuery();
                        Connect.Close();
                        Connect.Open();
                        MySqlCommand add_html = new MySqlCommand("INSERT INTO `html_orders`(`order_id`, `html_id`) VALUES (@order,@html)", Connect);
                        add_html.Parameters.AddWithValue("@order",gen.max_id_orders());
                        string html_id = gen.max_id_orders() + "" + DateTime.Now.Second + "" + DateTime.Now.Hour + "" + DateTime.Now.Year + "" + DateTime.Now.Minute + "" + DateTime.Now.Day + "" + DateTime.Now.Month;
                        add_html.Parameters.AddWithValue("@html",Crypt.Crypt.Encrypt(html_id));
                        add_html.ExecuteNonQuery();
                        Connect.Close();
                        foreach (TextBox textBox in panel1.Controls.OfType<TextBox>())
                        {
                            textBox.Clear();
                        }
                        gen.orders(f.dg_orders);
                        //Обновление отчета
                        if (gen.get_setting(10))
                        {
                            gen.display_by_date(f.dg_report, gen);
                        }
                        else
                        {
                            gen.refresh_reports(f.dg_report,gen);
                        }
                        metroProgressSpinner1.Visible = true;
                        MyMessageBox.Show(Owner, "Заказ успешно добавлен в базу", "Сообщение пользователю", MessageBoxButtons.OK, MessageBoxIcon.Information, 150, 400);
                        metroProgressSpinner1.Visible = false;
                    }
                    else {
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
            catch (Exception ex)
            {
                metroProgressSpinner1.Visible = true;
                MyMessageBox.Show(Owner, ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, 150, 400);
                metroProgressSpinner1.Visible = false;
            }
        }
    }
}
