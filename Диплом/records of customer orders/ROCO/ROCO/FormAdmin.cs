using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Microsoft.Office.Interop.Word;

namespace ROCO
{
    public partial class FormAdmin : MetroFramework.Forms.MetroForm
    {
        User user;
        General gen = new General();
        public MySqlConnection Connect = new MySqlConnection(Properties.Settings.Default.ConStr);
        
        public FormAdmin(int user_id)
        {
            try
            {
                InitializeComponent();
                this.Icon = Properties.Resources.WCL_1;
                label1.ForeColor = Color.FromArgb(88, 88, 88);
                label2.ForeColor = Color.FromArgb(88, 88, 88);
                button1.BackColor = Color.FromArgb(53, 173, 220);
                button2.BackColor = Color.FromArgb(53, 173, 220);
                button3.BackColor = Color.FromArgb(53, 173, 220);
                user = gen.get_user(user_id);
                gen.get_field();
                label2.Text = user.Fio + " (" + user.role() + ")";
            }
            catch (Exception ex)
            {
                metroProgressSpinner1.Visible = true;
                MyMessageBox.Show(Owner, ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, 150, 400);
                metroProgressSpinner1.Visible = false;
            }
        }
        
        private void FormAdmin_Load(object sender, EventArgs e)
        {
            gen.workers(dg_workers);
            gen.clients(dg_clients);
            gen.orders(dg_orders);
            if (gen.get_setting(10))
            {
                gen.display_by_date(dg_report, gen);
            }
            else
            {
                gen.orders(dg_report);
                gen.set_columns(dg_report, gen);
            }
            gen.set_only_view(pb_warning, l_selected_field, textBox1, s_d, e_d);
        }

        private void FormAdmin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.StartForm.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Add_worker f = new Add_worker(this);
            f.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Add_client f = new Add_client(this);
            f.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Add_order f = new Add_order(this);
            f.ShowDialog();
        }

        private void dg_workers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dg_workers.CurrentCell.ColumnIndex == 11)
                {
                    metroProgressSpinner1.Visible = true;
                    DialogResult dialogResult = MyMessageBox.Show(Owner, "Вы точно хотите удалить сотрудника '" + dg_workers.CurrentRow.Cells[1].Value + "' из базы?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question, 150, 400);
                    metroProgressSpinner1.Visible = false;
                    if (dialogResult == DialogResult.Yes)
                    {
                        Connect.Open();
                        MySqlCommand del_wrkr = new MySqlCommand("delete from `workers` where id=" + dg_workers.CurrentRow.Cells[0].Value, Connect);
                        del_wrkr.ExecuteNonQuery();
                        Connect.Close();
                        gen.workers(dg_workers);
                        metroProgressSpinner1.Visible = true;
                        MyMessageBox.Show(Owner, "Сотрудник удален из базы", "Сообщение пользователю", MessageBoxButtons.OK, MessageBoxIcon.Information, 150, 400);
                        metroProgressSpinner1.Visible = false;
                    }
                }
                else if (dg_workers.CurrentCell.ColumnIndex == 10)
                {
                    Update_worker f = new Update_worker(this, Convert.ToInt32(dg_workers.CurrentRow.Cells[0].Value));
                    f.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                metroProgressSpinner1.Visible = true;
                MyMessageBox.Show(Owner, ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, 150, 400);
                metroProgressSpinner1.Visible = false;
            }
        }

        private void dg_clients_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dg_clients.CurrentCell.ColumnIndex == 6)
                {
                    metroProgressSpinner1.Visible = true;
                    DialogResult dialogResult = MyMessageBox.Show(Owner, "Вы точно хотите удалить клиента '" + dg_clients.CurrentRow.Cells[1].Value + "' из базы?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question, 150, 400);
                    metroProgressSpinner1.Visible = false;
                    if (dialogResult == DialogResult.Yes)
                    {
                        Connect.Open();
                        MySqlCommand del_client = new MySqlCommand("delete from `cliente` where id=" + dg_clients.CurrentRow.Cells[0].Value, Connect);
                        del_client.ExecuteNonQuery();
                        Connect.Close();
                        gen.clients(dg_clients);
                        metroProgressSpinner1.Visible = true;
                        MyMessageBox.Show(Owner, "Клиент удален из базы", "Сообщение пользователю", MessageBoxButtons.OK, MessageBoxIcon.Information, 150, 400);
                        metroProgressSpinner1.Visible = false;
                    }
                }
                else if (dg_clients.CurrentCell.ColumnIndex == 5)
                {
                    Update_client f = new Update_client(this, Convert.ToInt32(dg_clients.CurrentRow.Cells[0].Value));
                    f.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                metroProgressSpinner1.Visible = true;
                MyMessageBox.Show(Owner, ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, 150, 400);
                metroProgressSpinner1.Visible = false;
            }
        }

        private void dg_orders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dg_orders.CurrentCell.ColumnIndex == 10)
                {
                    metroProgressSpinner1.Visible = true;
                    DialogResult dialogResult = MyMessageBox.Show(Owner, "Вы точно хотите удалить заказ №" + dg_orders.CurrentRow.Cells[0].Value + " из базы?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question, 150, 400);
                    metroProgressSpinner1.Visible = false;
                    if (dialogResult == DialogResult.Yes)
                    {
                        Connect.Open();
                        MySqlCommand del_order = new MySqlCommand("delete from `orders` where id=" + dg_orders.CurrentRow.Cells[0].Value, Connect);
                        del_order.ExecuteNonQuery();
                        Connect.Close();
                        gen.orders(dg_orders);
                        metroProgressSpinner1.Visible = true;
                        MyMessageBox.Show(Owner, "Заказ удален из базы", "Сообщение пользователю", MessageBoxButtons.OK, MessageBoxIcon.Information, 150, 400);
                        metroProgressSpinner1.Visible = false;
                    }
                }
                else if (dg_orders.CurrentCell.ColumnIndex == 9)
                {
                    Update_order f = new Update_order(this,Convert.ToInt32(dg_orders.CurrentRow.Cells[0].Value));
                    f.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                metroProgressSpinner1.Visible = true;
                MyMessageBox.Show(Owner, ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, 150, 400);
                metroProgressSpinner1.Visible = false;
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.FromArgb(53, 173, 220));
            pen.Width = 3;
            foreach (TextBox textBox in panel3.Controls.OfType<TextBox>())
            {
                e.Graphics.DrawLine(pen, textBox.Left, textBox.Top + textBox.Height, textBox.Left + textBox.Width, textBox.Top + textBox.Height);
                e.Graphics.DrawLine(pen, textBox.Left, textBox.Top + textBox.Height, textBox.Left, textBox.Top);
            }
            foreach (PictureBox pb in panel3.Controls.OfType<PictureBox>())
            {
                e.Graphics.DrawLine(pen, pb.Left, pb.Top + pb.Height, pb.Left + pb.Width, pb.Top + pb.Height);
            }
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.FromArgb(53, 173, 220));
            pen.Width = 3;
            foreach (TextBox textBox in panel3.Controls.OfType<TextBox>())
            {
                e.Graphics.DrawLine(pen, textBox.Left, textBox.Top + textBox.Height, textBox.Left + textBox.Width, textBox.Top + textBox.Height);
                e.Graphics.DrawLine(pen, textBox.Left, textBox.Top + textBox.Height, textBox.Left, textBox.Top);
            }
            foreach (PictureBox pb in panel3.Controls.OfType<PictureBox>())
            {
                e.Graphics.DrawLine(pen, pb.Left, pb.Top + pb.Height, pb.Left + pb.Width, pb.Top + pb.Height);
            }
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.FromArgb(53, 173, 220));
            pen.Width = 3;
            foreach (TextBox textBox in panel3.Controls.OfType<TextBox>())
            {
                e.Graphics.DrawLine(pen, textBox.Left, textBox.Top + textBox.Height, textBox.Left + textBox.Width, textBox.Top + textBox.Height);
                e.Graphics.DrawLine(pen, textBox.Left, textBox.Top + textBox.Height, textBox.Left, textBox.Top);
            }
            foreach (PictureBox pb in panel3.Controls.OfType<PictureBox>())
            {
                e.Graphics.DrawLine(pen, pb.Left, pb.Top + pb.Height, pb.Left + pb.Width, pb.Top + pb.Height);
            }
        }

        private void s_worker_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (s_worker.Text == "")
                {
                    gen.workers(dg_workers);
                }
                else {
                    gen.search_worker(dg_workers,s_worker.Text);
                }
            }
            catch (Exception ex)
            {
                metroProgressSpinner1.Visible = true;
                MyMessageBox.Show(Owner, ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, 150, 400);
                metroProgressSpinner1.Visible = false;
            }
        }

        private void s_order_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (s_order.Text == "")
                {
                    gen.orders(dg_orders);
                }
                else {
                    gen.search_order(dg_orders,s_order.Text);
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
                    gen.clients(dg_clients);
                }
                else
                {
                    gen.search_client(dg_clients, s_client.Text);
                }
            }
            catch (Exception ex)
            {
                metroProgressSpinner1.Visible = true;
                MyMessageBox.Show(Owner, ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, 150, 400);
                metroProgressSpinner1.Visible = false;
            }
        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.FromArgb(53, 173, 220));
            pen.Width = 3;
            foreach (TextBox textBox in panel9.Controls.OfType<TextBox>())
            {
                e.Graphics.DrawLine(pen, textBox.Left, textBox.Top + textBox.Height, textBox.Left + textBox.Width, textBox.Top + textBox.Height);
                e.Graphics.DrawLine(pen, textBox.Left, textBox.Top + textBox.Height, textBox.Left, textBox.Top);
            }
            foreach (PictureBox pb in panel9.Controls.OfType<PictureBox>())
            {
                if(pb.Name!= "pb_warning")
                    e.Graphics.DrawLine(pen, pb.Left, pb.Top + pb.Height, pb.Left + pb.Width, pb.Top + pb.Height);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                Settings_report f = new Settings_report(this);
                f.ShowDialog();
            }
            catch (Exception ex)
            {
                metroProgressSpinner1.Visible = true;
                MyMessageBox.Show(Owner, ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, 150, 400);
                metroProgressSpinner1.Visible = false;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            bool flagHeader = true;
            metroProgressSpinner1.Visible = true;
            int counterRow = 0;
            int counterCell = 0;
            Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();
            Document doc = app.Documents.Add(Visible: true);
            Range r = doc.Range();
            //Запись заголовка в документ
            if (gen.get_setting(11))
            {
                var paragraph = doc.Content.Paragraphs.Add();
                paragraph.Range.Text = Properties.Settings.Default.text_header_word;
                paragraph.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                paragraph.Range.Font.Name = Properties.Settings.Default.text_style_word;
                paragraph.Range.Font.Size = Properties.Settings.Default.size_text_word;
                paragraph.Range.InsertParagraphAfter();
            }
            

            string[,] array;
            string[] arrayHeader;
            if (Properties.Settings.Default.show_id)
            {
                array = new string[dg_report.RowCount, dg_report.ColumnCount];
                arrayHeader = new string[dg_report.ColumnCount];
                for (int i = 0; i < dg_report.RowCount; i++)
                {
                    for (int j = 0; j < dg_report.ColumnCount; j++)
                    {
                        arrayHeader[j] = dg_report.Columns[j].HeaderText.ToString();
                        array[i, j] = dg_report.Rows[i].Cells[j].Value.ToString();
                    }
                }
            }
            else
            {
                array = new string[dg_report.RowCount, dg_report.ColumnCount - 1];
                arrayHeader = new string[dg_report.ColumnCount];
                for (int i = 0; i < dg_report.RowCount; i++)
                {
                    for (int j = 1; j < dg_report.ColumnCount; j++)
                    {
                        array[i, j-1] = dg_report.Rows[i].Cells[j].Value.ToString();
                        arrayHeader[j] = dg_report.Columns[j].HeaderText.ToString();
                    }
                }
                counterCell++;//Для того чтобы в заголовке не отображался id
            }
            var par = doc.Content.Paragraphs.Add();
            Table t = t = doc.Tables.Add(par.Range, array.GetLength(0) + 1, array.GetLength(1));
            par.Range.Font.Name = Properties.Settings.Default.text_style_word;
            par.Range.Font.Size = Properties.Settings.Default.size_text_word;
            t.Range.Font.Name = Properties.Settings.Default.text_style_word;
            t.Range.Font.Size = Properties.Settings.Default.size_text_word;
            t.Borders.Enable = 1;

            foreach (Row row in t.Rows)
            {
                foreach (Cell cell in row.Cells)
                {
                    cell.Range.Font.Name = Properties.Settings.Default.text_style_word;
                    cell.Range.Font.Size = Properties.Settings.Default.size_text_word;

                    if (flagHeader)
                    {
                        cell.Range.Text = dg_report.Columns[counterCell].HeaderText.ToString();

                    }
                    else
                    {
                        cell.Range.Text = array[counterRow, counterCell].ToString();
                    }
                    if (flagHeader)
                    {
                        if (counterCell == arrayHeader.Length - 1)
                        {
                            counterCell = 0;
                        }
                        else
                        {
                            counterCell++;
                        }
                    }
                    else
                    {
                        if (counterCell == array.GetLength(1) - 1)
                        {
                            counterCell = 0;
                        }
                        else
                        {
                            counterCell++;
                        }
                    }
                }
                counterRow++;
                if (flagHeader)
                {
                    counterRow = 0;
                }
                flagHeader = false;
            }

            
            try
            {
                doc.Save();
                doc.Close();
                MyMessageBox.Show(Owner, "Экспорт данных в Word выполнен успешно", "Сообщение пользователю", MessageBoxButtons.OK, MessageBoxIcon.Information, 150, 400);
                
            }
            catch (Exception ex)
            {
                MyMessageBox.Show(Owner, ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, 150, 400);
            }
            finally
            {
                app.Quit();
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(app);
                metroProgressSpinner1.Visible = false;
                app = null;
                doc = null;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "")
                {
                    gen.refresh_reports(dg_report, gen);
                }
                else
                {
                    if (gen.get_setting(9)==false)//multi     9//Обычный поиск
                    {
                        gen.search_report_multi(dg_report,textBox1.Text,gen);
                    }
                    else//По одному полю
                    {
                        gen.search_report_only(dg_report,textBox1.Text,gen);
                    }
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
