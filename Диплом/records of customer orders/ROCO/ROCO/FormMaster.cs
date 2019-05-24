using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ROCO
{
    public partial class FormMaster : MetroFramework.Forms.MetroForm
    {
        User user;
        General gen = new General();
        public MySqlConnection Connect = new MySqlConnection(Properties.Settings.Default.ConStr);
        public FormMaster(int user_id)
        {
            InitializeComponent();
            this.Icon = Properties.Resources.WCL_1;
            try
            {
                label1.ForeColor = Color.FromArgb(88, 88, 88);
                label2.ForeColor = Color.FromArgb(88, 88, 88);
                user = gen.get_user(user_id);
                user.orders(dg_orders);
                user.clients(dg_clients);
            }
            catch (Exception ex)
            {
                metroProgressSpinner1.Visible = true;
                MyMessageBox.Show(Owner, ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, 150, 400);
                metroProgressSpinner1.Visible = false;
            }
        }

        private void FormMaster_Load(object sender, EventArgs e)
        {
            label2.Text = user.Fio + " (" + user.role() + ")";
        }

        private void FormMaster_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.StartForm.Show();
            this.Close();
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.FromArgb(53, 173, 220));
            pen.Width = 3;
            foreach (TextBox textBox in panel7.Controls.OfType<TextBox>())
            {
                e.Graphics.DrawLine(pen, textBox.Left, textBox.Top + textBox.Height, textBox.Left + textBox.Width, textBox.Top + textBox.Height);
                e.Graphics.DrawLine(pen, textBox.Left, textBox.Top + textBox.Height, textBox.Left, textBox.Top);
            }
            foreach (PictureBox pb in panel7.Controls.OfType<PictureBox>())
            {
                e.Graphics.DrawLine(pen, pb.Left, pb.Top + pb.Height, pb.Left + pb.Width, pb.Top + pb.Height);
            }
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.FromArgb(53, 173, 220));
            pen.Width = 3;
            foreach (TextBox textBox in panel5.Controls.OfType<TextBox>())
            {
                e.Graphics.DrawLine(pen, textBox.Left, textBox.Top + textBox.Height, textBox.Left + textBox.Width, textBox.Top + textBox.Height);
                e.Graphics.DrawLine(pen, textBox.Left, textBox.Top + textBox.Height, textBox.Left, textBox.Top);
            }
            foreach (PictureBox pb in panel5.Controls.OfType<PictureBox>())
            {
                e.Graphics.DrawLine(pen, pb.Left, pb.Top + pb.Height, pb.Left + pb.Width, pb.Top + pb.Height);
            }
        }

        private void dg_orders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dg_orders.CurrentCell.ColumnIndex == 8)
            {
                Update_order f = new Update_order(this,user, Convert.ToInt32(dg_orders.CurrentRow.Cells[0].Value));
                f.ShowDialog();
            }
        }

        private void s_order_TextChanged(object sender, EventArgs e)
        {

            try
            {
                if (s_order.Text == "")
                {
                    user.orders(dg_orders);
                }
                else
                {
                    user.search_order(dg_orders,s_order.Text);
                }
            }
            catch (Exception ex)
            {
                metroProgressSpinner1.Visible = true;
                MyMessageBox.Show(Owner, ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, 150, 400);
                metroProgressSpinner1.Visible = false;
            }
        }

        private void s_client_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (s_client.Text == "")
                {
                    user.clients(dg_clients);
                }
                else
                {
                    user.search_client(dg_clients, s_client.Text);
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
