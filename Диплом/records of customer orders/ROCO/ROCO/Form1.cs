using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;

namespace ROCO
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        public MySqlConnection Connection;
        public Form1()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.WCL_1;
            bt_login.BackColor = Color.FromArgb(44, 183, 227);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                BinaryReader breader = new BinaryReader(File.Open("string", FileMode.Open));
                while (breader.PeekChar() > -1)
                {
                    Properties.Settings.Default.ConStr = Crypt.Crypt.Decrypt(breader.ReadString());
                }
                breader.Close();
            }
            catch (Exception ex)
            {
                MyMessageBox.Show(Owner, ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, 150, 400);
            }
        }

        private void bt_login_Click_1(object sender, EventArgs e)
        {
            try
            {

                Connection = new MySqlConnection(Properties.Settings.Default.ConStr);
                Connection.Open();
                MySqlCommand selectUser = new MySqlCommand("select * from `workers` where `login`='" + 
                    Crypt.Crypt.Encrypt(tb_login.Text) + 
                    "' and `password`='" +
                    Crypt.Crypt.Encrypt(tb_pass.Text) + "'", Connection);
                MySqlDataReader reader = null;
                reader = selectUser.ExecuteReader();
                bool flag = false;
                int user_id = 0;
                int user_role = 1;
                while (reader.Read())
                {
                    flag = true;
                    user_id = Convert.ToInt32(reader[0]);
                    user_role = Convert.ToInt32(reader[5]);
                }
                if (flag)
                {
                    this.Hide();
                    Properties.Settings.Default.StartForm = this;
                    if (user_role == 1)
                    {
                        FormMaster f = new FormMaster(user_id);
                        f.ShowDialog();
                    }
                    else
                    {
                        FormAdmin f = new FormAdmin(user_id);
                        f.ShowDialog();
                    }
                    l_message.Text = "";
                }
                else
                {
                    l_message.Text = "Неверный логин или пароль";
                }
                Connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //MyMessageBox.Show(Owner, ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, 150, 400);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.FromArgb(53, 173, 220));
            pen.Width = 3;
            foreach (TextBox textBox in panel1.Controls.OfType<TextBox>())
            {
                e.Graphics.DrawLine(pen, textBox.Left, textBox.Top + textBox.Height, textBox.Left + textBox.Width, textBox.Top + textBox.Height);
                e.Graphics.DrawLine(pen, textBox.Left, textBox.Top + textBox.Height, textBox.Left, textBox.Top);
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void tb_login_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F12)
                {
                    db_setting f = new db_setting();
                    f.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.Show(Owner, ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, 150, 400);
            }
        }
    }
}
