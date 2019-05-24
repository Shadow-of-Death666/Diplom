using System;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace ROCO
{
    public class User
    {
        private int id;
        private string fio;
        private DateTime birth;
        private string corp_email;
        private DateTime employment;
        private int role_id;
        private string login;
        private string password;
        private string telephone;
        private int position_id;
        public ArrayList orders_collections = new ArrayList();
        public ArrayList cliente_collections = new ArrayList();
        public MySqlConnection Connect = new MySqlConnection(Properties.Settings.Default.ConStr);

        public string role()
        {
            string role = "";
            Connect.Open();
            MySqlCommand get_role = new MySqlCommand("select r.name from `workers` w, `roles` r where w.role_id=r.id and w.id = " + this.id, Connect);
            MySqlDataReader reader = null;
            reader = get_role.ExecuteReader();
            while (reader.Read())
            {
                role = reader[0].ToString();
            }
            Connect.Close();
            return role;
        }
        public string position()
        {
            string pos = "";
            Connect.Open();
            MySqlCommand get_pos = new MySqlCommand("select s.name from `workers` w, `positions` s where w.position_id=s.id and w.id = " + this.id, Connect);
            MySqlDataReader reader = null;
            reader = get_pos.ExecuteReader();
            while (reader.Read())
            {
                pos = reader[0].ToString();
            }
            Connect.Close();
            return pos;
        }

        public static void table_styling(DataGridView tbl)
        {
            tbl.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            tbl.BorderStyle = BorderStyle.None;
            tbl.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            tbl.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            tbl.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            tbl.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            tbl.BackgroundColor = Color.White;
            tbl.EnableHeadersVisualStyles = false;
            tbl.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            tbl.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(88, 88, 88);
            tbl.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }//Стилизация таблицы
        public void clients(DataGridView tbl)
        {
            Connect.Open();
            MySqlCommand clnt_mstr = new MySqlCommand("select distinct c.id, c.fio, c.telephone, c.email, c.additional from `cliente` c, `orders` o where o.worker_id = " + this.Id + " and o.client_id = c.id ", Connect);
            MySqlDataReader reader = clnt_mstr.ExecuteReader();
            tbl.RowCount = 0;
            cliente_collections.Clear();
            tbl.Rows.Clear();
            int rows = 0;
            table_styling(tbl);
            while (reader.Read())
            {
                Client cl_t = new Client(Convert.ToInt32(reader[0]), Crypt.Crypt.Decrypt(reader[1].ToString()),
                    Crypt.Crypt.Decrypt(reader[2].ToString()),
                    Crypt.Crypt.Decrypt(reader[3].ToString()),
                    Crypt.Crypt.Decrypt(reader[4].ToString()));
                cliente_collections.Add(cl_t);
                tbl.RowCount++;
                tbl.Rows[rows].Cells[0].Value = cl_t.Id;
                tbl.Rows[rows].Cells[1].Value = cl_t.Fio;
                tbl.Rows[rows].Cells[2].Value = cl_t.Telephone;
                tbl.Rows[rows].Cells[3].Value = cl_t.Email;
                tbl.Rows[rows].Cells[4].Value = cl_t.Additional;
                rows++;
            }
            Connect.Close();
        }//Отображение всех клиентов
        public void orders(DataGridView tbl) {
            Connect.Open();
            MySqlCommand wrkr = new MySqlCommand("SELECT * from `orders`,`html_orders` where orders.id=html_orders.order_id and orders.status_id!=7 and orders.worker_id="+this.Id, Connect);
            MySqlDataReader reader = wrkr.ExecuteReader();
            tbl.RowCount = 0;
            tbl.Rows.Clear();
            int rows = 0;
            table_styling(tbl);
            orders_collections.Clear();
            while (reader.Read())
            {
                Order ord_t = new Order(Convert.ToInt32(reader[0]), Convert.ToInt32(reader[1]),
                    Convert.ToInt32(reader[2]), reader[3].ToString(), Convert.ToDateTime(reader[4]),
                    DateTime.Now, Convert.ToInt32(reader[6]),
                    Convert.ToInt32(reader[7]));//Дата текущая так как может прийти null, сделаю вывод из reader после проверки
                orders_collections.Add(ord_t);
                tbl.RowCount++;
                if (reader[5].ToString() != "")
                {
                    tbl.Rows[rows].Cells[4].Value = Convert.ToDateTime(reader[5]).ToShortDateString();
                }
                else
                {
                    tbl.Rows[rows].Cells[4].Value = "";
                }
                tbl.Rows[rows].Cells[0].Value = ord_t.Id;
                tbl.Rows[rows].Cells[1].Value = ord_t.client();
                tbl.Rows[rows].Cells[2].Value = ord_t.Theme;
                tbl.Rows[rows].Cells[3].Value = ord_t.Start_date.ToShortDateString();
                tbl.Rows[rows].Cells[5].Value = ord_t.status();
                tbl.Rows[rows].Cells[6].Value = ord_t.Price;
                tbl.Rows[rows].Cells[7].Value = Crypt.Crypt.Decrypt(reader[9].ToString());
                rows++;
            }
            Connect.Close();
        }//Отображение всех заказов
        public void search_order(DataGridView tbl,string text)
        {
            tbl.RowCount = 0;
            tbl.Rows.Clear();
            int rows = 0;
            foreach (Order o in orders_collections)
            {
                if (o.Id.ToString().IndexOf(text) != -1 || o.worker().IndexOf(text) != -1 || o.client().IndexOf(text) != -1 || o.Theme.IndexOf(text) != -1 || o.status().IndexOf(text) != -1 || o.end_and_html()[1] == text)
                {
                    tbl.RowCount++;
                    tbl.Rows[rows].Cells[0].Value = o.Id;
                    tbl.Rows[rows].Cells[1].Value = o.client();
                    tbl.Rows[rows].Cells[2].Value = o.Theme;
                    tbl.Rows[rows].Cells[3].Value = o.Start_date.ToShortDateString();
                    if (o.end_and_html()[0] != "")
                    {
                        tbl.Rows[rows].Cells[4].Value = Convert.ToDateTime(o.end_and_html()[0]).ToShortDateString();
                    }
                    tbl.Rows[rows].Cells[5].Value = o.status();
                    tbl.Rows[rows].Cells[6].Value = o.Price;
                    tbl.Rows[rows].Cells[7].Value = o.end_and_html()[1];
                    rows++;
                }
            }
        }//Поиск заказа
        public void search_client(DataGridView tbl, string text) {
            tbl.RowCount = 0;
            tbl.Rows.Clear();
            int rows = 0;
            foreach (Client c in cliente_collections)
            {
                if (c.Id.ToString().IndexOf(text) != -1 || c.Fio.IndexOf(text) != -1 || c.Telephone.IndexOf(text) != -1 || c.Email.IndexOf(text) != -1 || c.Additional.IndexOf(text) != -1)
                {
                    tbl.RowCount++;
                    tbl.Rows[rows].Cells[0].Value = c.Id;
                    tbl.Rows[rows].Cells[1].Value = c.Fio;
                    tbl.Rows[rows].Cells[2].Value = c.Telephone;
                    tbl.Rows[rows].Cells[3].Value = c.Email;
                    tbl.Rows[rows].Cells[4].Value = c.Additional;
                    rows++;
                }
            }
        }//Поиск клиента

        public User(int id, string fio, DateTime birth, string corp_email, DateTime employment, int role_id, string login, string password, string telephone, int position_id)
        {
            this.id = id;
            this.fio = fio;
            this.birth = birth;
            this.corp_email = corp_email;
            this.employment = employment;
            this.role_id = role_id;
            this.login = login;
            this.password = password;
            this.telephone = telephone;
            this.position_id = position_id;
        }

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string Fio
        {
            get
            {
                return fio;
            }

            set
            {
                fio = value;
            }
        }

        public DateTime Birth
        {
            get
            {
                return birth;
            }

            set
            {
                birth = value;
            }
        }

        public string Corp_email
        {
            get
            {
                return corp_email;
            }

            set
            {
                corp_email = value;
            }
        }

        public DateTime Employment
        {
            get
            {
                return employment;
            }

            set
            {
                employment = value;
            }
        }

        public int Role_id
        {
            get
            {
                return role_id;
            }

            set
            {
                role_id = value;
            }
        }

        public string Login
        {
            get
            {
                return login;
            }

            set
            {
                login = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
            }
        }

        public string Telephone
        {
            get
            {
                return telephone;
            }

            set
            {
                telephone = value;
            }
        }

        public int Position_id
        {
            get
            {
                return position_id;
            }

            set
            {
                position_id = value;
            }
        }
    }
}
