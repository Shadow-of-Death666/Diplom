using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ROCO
{
    public partial class db_setting : MetroFramework.Forms.MetroForm
    {
        public string string_key = "";
        public db_setting()
        {
            InitializeComponent();
        }

        private void db_setting_Load(object sender, EventArgs e)
        {

        }

        private void db_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (string_key == "D8D9D8D7D1D9D1D2D7D3D9F12")
                {
                    string_key = "";
                    db_string_connection f = new db_string_connection();
                    f.ShowDialog();
                }
                else
                {
                    bool flag = false;
                    string[] valid = { "D8", "D9", "D7", "D1", "D2", "D3", "D9", "F12" };
                    foreach (string s in valid)
                    {
                        if (s == e.KeyData.ToString())
                        {
                            flag = true;
                        }
                    }
                    if (flag == false)
                    {
                        string_key = "";
                    }
                    else
                    {
                        string_key += e.KeyData;
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessageBox.Show(Owner, ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, 150, 400);
            }
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.ConStr = "Server="+
                serv.Text + ";Port="+
                port.Text + ";Database="+
                db.Text + ";User Id="+
                user.Text + ";Password="+
                pass.Text + "; SslMode=none;Pooling=False;charset=utf8";
            Properties.Settings.Default.ConStr = "Server=shdwfdth.beget.tech;Port=3306;Database=shdwfdth_roco;User Id=shdwfdth_roco;Password=qazxsw; SslMode=none;charset=utf8";
            MyMessageBox.Show(Owner, "Применение настроек на данный сеанс произошло успешно", "Сообщение пользователю", MessageBoxButtons.OK, MessageBoxIcon.Information, 150, 400);
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.ConStr = "Server=" +
                serv.Text + ";Port=" +
                port.Text + ";Database=" +
                db.Text + ";User Id=" +
                user.Text + ";Password=" +
                pass.Text + "; SslMode=none;charset=utf8";
            DialogResult dialogResult = MyMessageBox.Show(Owner, "Вы точно хотите сохранить эти настройки навсегда?" + Environment.NewLine+ "Последующий вход будет выполнен по этим настройкам.", "Подтверждение сохранения", MessageBoxButtons.YesNo, MessageBoxIcon.Question, 150, 400);
            if (dialogResult == DialogResult.Yes)
            {
                BinaryWriter bwrite = new BinaryWriter(File.Open("string", FileMode.OpenOrCreate));
                bwrite.Write(Crypt.Crypt.Encrypt(Properties.Settings.Default.ConStr));
                bwrite.Close();
                MyMessageBox.Show(Owner, "Применение постоянных настроек произошло успешно", "Сообщение пользователю", MessageBoxButtons.OK, MessageBoxIcon.Information, 150, 400);
            }
        }
    }
}
