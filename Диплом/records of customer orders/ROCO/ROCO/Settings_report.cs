using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ROCO
{
    public partial class Settings_report : MetroFramework.Forms.MetroForm
    {
        General gen = new General();
        FormAdmin f;
        public Settings_report(FormAdmin f)
        {
            InitializeComponent();
            this.f = f;
            string[] arr = {
                "Дата начала (= От)",
                "Дата начала (От и До)",
                "Дата окончания (= От)",
                "Дата окончания (От и До)",
                "Дата начала и окончания (>=От и <=До)",
                "Дата начала и окончания (От и До)",
                "Не законченные"
            };
            foreach (string s in arr)
            {
                cb_style_date.Items.Add(s);
            }
            cb_style_date.Text = cb_style_date.Items[0].ToString();


            string[] array = {
                    "id",
                    "Сотрудник" ,
                    "Клиент" ,
                    "Тема" ,
                    "Статус",
                    "Стоимость",
                    "html код" };
            foreach (string s in array)
            {
                cb_field.Items.Add(s);
            }
            cb_field.Text = cb_field.Items[0].ToString();
            string[] arr_size = {
                "6",
                "8",
                "10",
                "12",
                "14",
                "16",
                "18",
                "20"
            };
            cb_size_text.Items.Clear();
            foreach (string s in arr_size)
            {
                cb_size_text.Items.Add(s);
            }
            string[] array_styles = {
                "Calibri",
                "Times New Roman",
                "Arial",
                "Georgia",
                "Tahoma",
                "Verdana",
            };
            foreach (string s in array_styles)
            {
                cb_style_text.Items.Add(s);
            }
        }

        private void Settings_report_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.WCL_1;
            button2.BackColor = Color.FromArgb(44, 183, 227);
            button2.Left = panel1.Width / 2 - button2.Width / 2;
            //gen.get_field();
            gen.set_control(this,gen);
            

        }

        private void checkBox9_CheckStateChanged(object sender, EventArgs e)
        {
            if (cb_multi.CheckState == CheckState.Checked)
            {
                cb_field.Visible = true;
                cb_date.CheckState = CheckState.Unchecked;
            }
            else
            {
                cb_field.Visible = false;
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                StreamWriter sw = new StreamWriter("setting.txt", false, Encoding.Default);
                //worker client theme date_start date_end status price html
                //worker
                if (cb_worker.CheckState == CheckState.Checked)
                {
                    sw.WriteLine("true");
                }
                else
                {
                    sw.WriteLine("false");
                }
                //client
                if (cb_client.CheckState == CheckState.Checked)
                {
                    sw.WriteLine("true");
                }
                else
                {
                    sw.WriteLine("false");
                }
                //theme
                if (cb_theme.CheckState == CheckState.Checked)
                {
                    sw.WriteLine("true");
                }
                else
                {
                    sw.WriteLine("false");
                }
                //start
                if (cb_start_date.CheckState == CheckState.Checked)
                {
                    sw.WriteLine("true");
                }
                else
                {
                    sw.WriteLine("false");
                }
                //end
                if (cb_end_date.CheckState == CheckState.Checked)
                {
                    sw.WriteLine("true");
                }
                else
                {
                    sw.WriteLine("false");
                }
                //status
                if (cb_status.CheckState == CheckState.Checked)
                {
                    sw.WriteLine("true");
                }
                else
                {
                    sw.WriteLine("false");
                }
                //price
                if (cb_price.CheckState == CheckState.Checked)
                {
                    sw.WriteLine("true");
                }
                else
                {
                    sw.WriteLine("false");
                }
                //html
                if (cb_html.CheckState == CheckState.Checked)
                {
                    sw.WriteLine("true");
                }
                else
                {
                    sw.WriteLine("false");
                }
                //id
                if (cb_show_id.CheckState == CheckState.Checked)
                {
                    sw.WriteLine("true");
                }
                else
                {
                    sw.WriteLine("false");
                }
                //multi
                if (cb_multi.CheckState == CheckState.Checked)
                {
                    sw.WriteLine("true");
                    sw.WriteLine(cb_field.Text);
                }
                else
                {
                    sw.WriteLine("false");
                    sw.WriteLine("undefined");
                }
                //search_date
                if (cb_date.CheckState == CheckState.Checked)
                {
                    sw.WriteLine("true");
                    sw.WriteLine(cb_style_date.Text);
                    sw.WriteLine(dt_s_date.Value.ToString("yyyy-MM-dd"));
                    sw.WriteLine(dt_e_date.Value.ToString("yyyy-MM-dd"));
                }
                else
                {
                    sw.WriteLine("false");
                    sw.WriteLine(cb_style_date.Text);
                    sw.WriteLine(dt_s_date.Value.ToString("yyyy-MM-dd"));
                    sw.WriteLine(dt_e_date.Value.ToString("yyyy-MM-dd"));
                }
                //size_text_word
                sw.WriteLine(cb_size_text.Text);
                //header word
                if (cb_header.CheckState == CheckState.Checked)
                {
                    sw.WriteLine("true");
                    sw.WriteLine(tb_header_text.Text);
                }
                else
                {
                    sw.WriteLine("false");
                    sw.WriteLine(tb_header_text.Text);
                }
                //style_text_word
                sw.WriteLine(cb_style_text.Text);
                ///////////////
                sw.Close();
                gen.get_field();
                gen.refresh_reports(f.dg_report,gen);
                gen.set_only_view(f.pb_warning,f.l_selected_field,f.textBox1,f.s_d,f.e_d);
                if (gen.get_setting(10))
                {
                    gen.display_by_date(f.dg_report, gen);
                }
                metroProgressSpinner1.Visible = true;
                MyMessageBox.Show(Owner, "Настройки успешно сохранены", "Сообщение пользователю", MessageBoxButtons.OK, MessageBoxIcon.Information, 150, 400);
                metroProgressSpinner1.Visible = false;
            }
            catch (Exception ex)
            {
                metroProgressSpinner1.Visible = true;
                MyMessageBox.Show(Owner, ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, 150, 400);
                metroProgressSpinner1.Visible = false;
            }
        }

        private void cb_multi_CheckStateChanged(object sender, EventArgs e)
        {
            if (cb_multi.CheckState == CheckState.Checked)
            {
                cb_field.Visible = true;
            }
            else
            {
                cb_field.Visible = false;
            }
        }

        private void cb_date_CheckStateChanged(object sender, EventArgs e)
        {
            if (cb_date.CheckState == CheckState.Checked)
            {
                cb_multi.CheckState = CheckState.Unchecked;
                cb_multi.Visible = false;
            }
            else
            {
                cb_multi.Visible = true;
            }
        }

        private void metroTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void metroTextBox1_TextChanged(object sender, EventArgs e)
        {
        }
    }
}
