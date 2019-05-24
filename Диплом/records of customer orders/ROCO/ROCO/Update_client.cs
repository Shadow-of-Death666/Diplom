using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ROCO
{
    public partial class Update_client : MetroFramework.Forms.MetroForm
    {
        public FormAdmin f;
        public int client_id;
        General gen = new General();
        MySqlConnection Connect = new MySqlConnection(Properties.Settings.Default.ConStr);
        public Update_client(FormAdmin f, int client_id)
        {
            InitializeComponent();
            this.Icon = Properties.Resources.WCL_1;
            label1.Left = panel1.Width / 2 - label1.Width / 2;
            button2.Left = panel1.Width / 2 - button2.Width / 2;
            button2.BackColor = Color.FromArgb(44, 183, 227);
            this.f = f;
            this.client_id = client_id;
            try
            {
                Connect.Open();
                MySqlCommand cl_data = new MySqlCommand("select * from `cliente` where id="+client_id,Connect);
                MySqlDataReader reader = cl_data.ExecuteReader();
                while (reader.Read())
                {
                    tb_fio.Text = Crypt.Crypt.Decrypt(reader[1].ToString());
                    tb_telephone.Text = Crypt.Crypt.Decrypt(reader[2].ToString());
                    tb_email.Text = Crypt.Crypt.Decrypt(reader[3].ToString());
                    tb_addit.Text = Crypt.Crypt.Decrypt(reader[4].ToString());
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
            }
            tb_email.MaxLength = 100;
            tb_fio.MaxLength = 100;
            tb_telephone.MaxLength = 11;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (tb_fio.Text.Length != 0)
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
                            MySqlCommand updt_client = new MySqlCommand("UPDATE `cliente` SET `fio`=@fio,`telephone`=@tel,`email`=@email,`additional`=@addit WHERE id="+client_id, Connect);
                            updt_client.Parameters.AddWithValue("@fio", Crypt.Crypt.Encrypt(tb_fio.Text));
                            updt_client.Parameters.AddWithValue("@tel", Crypt.Crypt.Encrypt(tb_telephone.Text));
                            updt_client.Parameters.AddWithValue("@email", Crypt.Crypt.Encrypt(tb_email.Text));
                            updt_client.Parameters.AddWithValue("@addit", Crypt.Crypt.Encrypt(tb_addit.Text));
                            updt_client.ExecuteNonQuery();
                            Connect.Close();
                            gen.clients(f.dg_clients);
                            gen.orders(f.dg_orders);
                            gen.refresh_clients_collections();//Обновить массив клиентов для поиска
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
                            MyMessageBox.Show(Owner, "Информация о клиенте успешно изменена", "Сообщение пользователю", MessageBoxButtons.OK, MessageBoxIcon.Information, 150, 400);
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
                    MyMessageBox.Show(Owner, "Не все поля заполнены", "Предупеждение пользователю", MessageBoxButtons.OK, MessageBoxIcon.Warning, 150, 400);
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

        private void Update_client_Load(object sender, EventArgs e)
        {

        }
    }
}
