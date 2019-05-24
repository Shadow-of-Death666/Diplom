using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ROCO
{
    public partial class db_string_connection : MetroFramework.Forms.MetroForm
    {
        public db_string_connection()
        {
            InitializeComponent();
        }

        private void db_string_connection_Load(object sender, EventArgs e)
        {
            str_conn.Text = Properties.Settings.Default.ConStr;
            this.Width = str_conn.Width + 50;
        }
    }
}
