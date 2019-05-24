using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;

namespace ROCO
{
    class General
    {
        public MySqlConnection Connect = new MySqlConnection(Properties.Settings.Default.ConStr);
        public ArrayList orders_collections = new ArrayList();
        public ArrayList clients_collections = new ArrayList();
        public General()
        {

        }//Конструктор
        public void refresh_clients_collections()
        {
            clients_collections.Clear();
            Connect.Open();
            MySqlCommand wrkr = new MySqlCommand("select * from `cliente`", Connect);
            MySqlDataReader reader = wrkr.ExecuteReader();
            while (reader.Read())
            {
                Client cl_t = new Client(Convert.ToInt32(reader[0]),
                    Crypt.Crypt.Decrypt(reader[1].ToString()),
                    Crypt.Crypt.Decrypt(reader[2].ToString()),
                    Crypt.Crypt.Decrypt(reader[3].ToString()),
                    Crypt.Crypt.Decrypt(reader[4].ToString()));
                clients_collections.Add(cl_t);
            }
            Connect.Close();
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
        public User get_user(int user_id)
        {
            User user = new User(1, "", DateTime.Now, "", DateTime.Now, 1, "", "", "", 1);
            Connect.Open();
            MySqlCommand get_user = new MySqlCommand("SELECT * from `workers` where `id`=" + user_id, Connect);
            MySqlDataReader reader = get_user.ExecuteReader();
            while (reader.Read())
            {
                user = new User(Convert.ToInt32(reader[0]),
                    Crypt.Crypt.Decrypt(reader[1].ToString()),
                    Convert.ToDateTime(reader[2]),
                    Crypt.Crypt.Decrypt(reader[3].ToString()),
                    Convert.ToDateTime(reader[4]), Convert.ToInt32(reader[5]),
                    Crypt.Crypt.Decrypt(reader[6].ToString()),
                    Crypt.Crypt.Decrypt(reader[7].ToString()),
                    reader[8].ToString(), Convert.ToInt32(reader[9]));
                if (user.Role_id == 2)
                {
                    Properties.Settings.Default.FlagRole = true;
                }
                else
                {
                    Properties.Settings.Default.FlagRole = false;
                }
            }
            Connect.Close();
            return user;
        }//Получение данных о текущем (вошедшем) пользователе
        public void workers(DataGridView tbl)
        {
            Connect.Open();
            MySqlCommand wrkr = new MySqlCommand("select * from `workers`", Connect);
            MySqlDataReader reader = wrkr.ExecuteReader();
            tbl.RowCount = 0;
            tbl.Rows.Clear();
            table_styling(tbl);
            int rows = 0;
            while (reader.Read())
            {
                User t_user = new User(Convert.ToInt32(reader[0]),
                    Crypt.Crypt.Decrypt(reader[1].ToString()), 
                    Convert.ToDateTime(reader[2]),
                    Crypt.Crypt.Decrypt(reader[3].ToString()), 
                    Convert.ToDateTime(reader[4]),
                    Convert.ToInt32(reader[5]),
                    Crypt.Crypt.Decrypt(reader[6].ToString()),
                    Crypt.Crypt.Decrypt(reader[7].ToString()),
                    Crypt.Crypt.Decrypt(reader[8].ToString()), Convert.ToInt32(reader[9]));
                tbl.RowCount++;
                tbl.Rows[rows].Cells[0].Value = t_user.Id;
                tbl.Rows[rows].Cells[1].Value = t_user.Fio;
                tbl.Rows[rows].Cells[2].Value = t_user.Birth.ToShortDateString();
                tbl.Rows[rows].Cells[3].Value = t_user.Corp_email;
                tbl.Rows[rows].Cells[4].Value = t_user.Employment.ToShortDateString();
                tbl.Rows[rows].Cells[5].Value = t_user.role();
                tbl.Rows[rows].Cells[6].Value = t_user.Login;
                tbl.Rows[rows].Cells[7].Value = t_user.Password;
                tbl.Rows[rows].Cells[8].Value = t_user.Telephone;
                tbl.Rows[rows].Cells[9].Value = t_user.position();
                rows++;
            }
            Connect.Close();
        }//Вывод списка всех рабочих в таблицу
        public void clients(DataGridView tbl)
        {
            Connect.Open();
            MySqlCommand wrkr = new MySqlCommand("select * from `cliente`", Connect);
            MySqlDataReader reader = wrkr.ExecuteReader();
            tbl.RowCount = 0;
            tbl.Rows.Clear();
            int rows = 0;
            table_styling(tbl);
            while (reader.Read())
            {
                Client cl_t = new Client(Convert.ToInt32(reader[0]),
                    Crypt.Crypt.Decrypt(reader[1].ToString()),
                    Crypt.Crypt.Decrypt(reader[2].ToString()),
                    Crypt.Crypt.Decrypt(reader[3].ToString()),
                    Crypt.Crypt.Decrypt(reader[4].ToString()));
                tbl.RowCount++;
                tbl.Rows[rows].Cells[0].Value = cl_t.Id;
                tbl.Rows[rows].Cells[1].Value = cl_t.Fio;
                tbl.Rows[rows].Cells[2].Value = cl_t.Telephone;
                tbl.Rows[rows].Cells[3].Value = cl_t.Email;
                tbl.Rows[rows].Cells[4].Value = cl_t.Additional;
                rows++;
            }
            Connect.Close();
            this.refresh_clients_collections();
        }//Вывод списка всех клиентов в таблицу
        public void orders(DataGridView tbl)
        {
            Connect.Open();
            MySqlCommand wrkr = new MySqlCommand("SELECT * from `orders`,`html_orders` where orders.id=html_orders.order_id", Connect);
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
                    tbl.Rows[rows].Cells[5].Value = Convert.ToDateTime(reader[5]).ToShortDateString();
                }
                else
                {
                    tbl.Rows[rows].Cells[5].Value = "";
                }
                tbl.Rows[rows].Cells[0].Value = ord_t.Id;
                tbl.Rows[rows].Cells[1].Value = ord_t.worker();
                tbl.Rows[rows].Cells[2].Value = ord_t.client();
                tbl.Rows[rows].Cells[3].Value = ord_t.Theme;
                tbl.Rows[rows].Cells[4].Value = ord_t.Start_date.ToShortDateString();
                tbl.Rows[rows].Cells[6].Value = ord_t.status();
                tbl.Rows[rows].Cells[7].Value = ord_t.Price;
                tbl.Rows[rows].Cells[8].Value = Crypt.Crypt.Decrypt(reader[9].ToString());
                rows++;
            }
            Connect.Close();
        }//Вывод списка всех заказов в таблицу
        public void workers_for_cb(ComboBox cb)
        {
            Dictionary<string, string> comboSource = new Dictionary<string, string>();
            Connect.Open();
            MySqlCommand wrkr = new MySqlCommand("select * from `workers`", Connect);
            MySqlDataReader reader = wrkr.ExecuteReader();
            while (reader.Read())
            {
                comboSource.Add(reader[0].ToString(), Crypt.Crypt.Decrypt(reader[1].ToString()));
            }
            Connect.Close();
            cb.DataSource = new BindingSource(comboSource, null);
            cb.DisplayMember = "Value";
            cb.ValueMember = "Key";
        }//Получение списка рабочих для cb
        public void client_for_cb(ComboBox cb)
        {
            Dictionary<string, string> comboSource = new Dictionary<string, string>();
            Connect.Open();
            MySqlCommand wrkr = new MySqlCommand("select * from `cliente`", Connect);
            MySqlDataReader reader = wrkr.ExecuteReader();
            while (reader.Read())
            {
                comboSource.Add(reader[0].ToString(), Crypt.Crypt.Decrypt(reader[1].ToString()));
            }
            Connect.Close();
            cb.DataSource = new BindingSource(comboSource, null);
            cb.DisplayMember = "Value";
            cb.ValueMember = "Key";
        }//Получение списка клиентов для cb
        public void status_for_cb(ComboBox cb, int status)
        {
            Dictionary<string, string> comboSource = new Dictionary<string, string>();
            Connect.Open();
            MySqlCommand wrkr = new MySqlCommand("select * from `statuses` where id>=" + status, Connect);
            MySqlDataReader reader = wrkr.ExecuteReader();
            while (reader.Read())
            {
                comboSource.Add(reader[0].ToString(), reader[1].ToString());
            }
            Connect.Close();
            cb.DataSource = new BindingSource(comboSource, null);
            cb.DisplayMember = "Value";
            cb.ValueMember = "Key";
        }//Получение статуса для cb
        public void status_for_cb(ComboBox cb)
        {
            Dictionary<string, string> comboSource = new Dictionary<string, string>();
            Connect.Open();
            MySqlCommand wrkr = new MySqlCommand("select * from `statuses`", Connect);
            MySqlDataReader reader = wrkr.ExecuteReader();
            while (reader.Read())
            {
                comboSource.Add(reader[0].ToString(), reader[1].ToString());
            }
            Connect.Close();
            cb.DataSource = new BindingSource(comboSource, null);
            cb.DisplayMember = "Value";
            cb.ValueMember = "Key";
        }//Занесение статусов заказа в cb для создания
        public int status_index(int order_id)
        {

            string stat = "";
            Connect.Open();
            MySqlCommand comm = new MySqlCommand("select status_id from `orders` where id=" + order_id, Connect);
            MySqlDataReader r = comm.ExecuteReader();
            while (r.Read())
            {
                stat = r.GetValue(0).ToString();
            }
            Connect.Close();
            Connect.Open();
            MySqlCommand wrkr = new MySqlCommand("select * from `statuses`", Connect);
            MySqlDataReader reader = wrkr.ExecuteReader();
            int counterIndex = 0;
            int index = 0;
            while (reader.Read())
            {
                if (reader[0].ToString() == stat)
                {
                    index = counterIndex;
                }
                counterIndex++;
            }
            Connect.Close();
            return index;
        }//Получение индекса статуса
        public void status_for_cb_master(ComboBox cb, int status)
        {
            Dictionary<string, string> comboSource = new Dictionary<string, string>();
            Connect.Open();
            MySqlCommand wrkr = new MySqlCommand("select * from `statuses` where id>=" + status + " and id<=5", Connect);
            MySqlDataReader reader = wrkr.ExecuteReader();
            int count = 0;
            while (reader.Read())
            {
                comboSource.Add(reader[0].ToString(), reader[1].ToString());
                count++;
            }
            Connect.Close();
            if (count != 0)
            {
                cb.DataSource = new BindingSource(comboSource, null);
                cb.DisplayMember = "Value";
                cb.ValueMember = "Key";
            }
        }//Занесение статусов в cb для мастера
        public void position_for_cb(ComboBox cb)
        {
            Dictionary<string, string> comboSource = new Dictionary<string, string>();
            Connect.Open();
            MySqlCommand wrkr = new MySqlCommand("select * from `positions`", Connect);
            MySqlDataReader reader = wrkr.ExecuteReader();
            while (reader.Read())
            {
                comboSource.Add(reader[0].ToString(), reader[1].ToString());
            }
            Connect.Close();
            cb.DataSource = new BindingSource(comboSource, null);
            cb.DisplayMember = "Value";
            cb.ValueMember = "Key";
        }//Занесение должностей в cb
        public int role_index(int worker_id)
        {
            string role = "";
            Connect.Open();
            MySqlCommand comm = new MySqlCommand("select role_id from `workers` where id=" + worker_id,Connect);
            MySqlDataReader r = comm.ExecuteReader();
            while (r.Read())
            {
                role = r.GetValue(0).ToString();
            }
            Connect.Close();
            Connect.Open();
            MySqlCommand wrkr = new MySqlCommand("select * from `roles`", Connect);
            MySqlDataReader reader = wrkr.ExecuteReader();
            int counterIndex = 0;
            int index = 0;
            while (reader.Read())
            {
                if (reader[0].ToString() == role)
                {
                    index = counterIndex;
                }
                counterIndex++;
            }
            Connect.Close();
            return index;
        }//Вернуть индекс роли
        public int position_index(int worker_id)//Вернуть индекс должности
        {
            string position = "";
            Connect.Open();
            MySqlCommand comm = new MySqlCommand("select position_id from `workers` where id=" + worker_id, Connect);
            MySqlDataReader r = comm.ExecuteReader();
            while (r.Read())
            {
                position = r.GetValue(0).ToString();
            }
            Connect.Close();
            Connect.Open();
            MySqlCommand wrkr = new MySqlCommand("select * from `positions`", Connect);
            MySqlDataReader reader = wrkr.ExecuteReader();
            int counterIndex = 0;
            int index = 0;
            while (reader.Read())
            {
                if (reader[0].ToString() == position)
                {
                    index = counterIndex;
                }
                counterIndex++;
            }
            Connect.Close();
            return index;
        }
        public void role_for_cb(ComboBox cb)
        {
            Dictionary<string, string> comboSource = new Dictionary<string, string>();
            Connect.Open();
            MySqlCommand wrkr = new MySqlCommand("select * from `roles`", Connect);
            MySqlDataReader reader = wrkr.ExecuteReader();
            while (reader.Read())
            {
                comboSource.Add(reader[0].ToString(), reader[1].ToString());
            }
            Connect.Close();
            cb.DataSource = new BindingSource(comboSource, null);
            cb.DisplayMember = "Value";
            cb.ValueMember = "Key";
        }//Занесение ролей в cb
        public int max_id_orders()
        {
            Connect.Open();
            MySqlCommand max_id = new MySqlCommand("select max(id) from `orders`", Connect);
            MySqlDataReader reader = max_id.ExecuteReader();
            int max = 0;
            while (reader.Read())
            {
                max = Convert.ToInt32(reader[0]);
            }
            Connect.Close();
            return max;
        }//Получение максимального номера заказа для создания html кода
        public int get_status_id(int id)
        {
            Connect.Open();
            int id_order = 0;
            MySqlCommand st = new MySqlCommand("select status_id from `orders` where id = " + id, Connect);
            MySqlDataReader reader = st.ExecuteReader();
            while (reader.Read())
            {
                id_order = Convert.ToInt32(reader[0]);
            }
            Connect.Close();
            return id_order;
        }//Получение статуса по номеру
        public void search_worker(DataGridView tbl, string text)
        {
            Connect.Open();
            tbl.RowCount = 0;
            tbl.Rows.Clear();
            MySqlCommand sr_wrkr = new MySqlCommand("SELECT * FROM `workers` WHERE `id` like '%" + text + "%' or `fio` like '%" + Crypt.Crypt.Encrypt(text) + "%' or `corp_email` like '%" +
                Crypt.Crypt.Encrypt(text) + 
                "%' or `telephone` like '%" +
                Crypt.Crypt.Encrypt(text) + "%'", Connect);
            MySqlDataReader reader = sr_wrkr.ExecuteReader();
            int rows = 0;
            while (reader.Read())
            {
                User t_user = new User(Convert.ToInt32(reader[0]),
                        Crypt.Crypt.Decrypt(reader[1].ToString()), 
                        Convert.ToDateTime(reader[2]),
                        Crypt.Crypt.Decrypt(reader[3].ToString()), 
                        Convert.ToDateTime(reader[4]),
                        Convert.ToInt32(reader[5]),
                        Crypt.Crypt.Decrypt(reader[6].ToString()),
                        Crypt.Crypt.Decrypt(reader[7].ToString()),
                        Crypt.Crypt.Decrypt(reader[8].ToString()), 
                        Convert.ToInt32(reader[9]));
                tbl.RowCount++;
                tbl.Rows[rows].Cells[0].Value = t_user.Id;
                tbl.Rows[rows].Cells[1].Value = t_user.Fio;
                tbl.Rows[rows].Cells[2].Value = t_user.Birth.ToShortDateString();
                tbl.Rows[rows].Cells[3].Value = t_user.Corp_email;
                tbl.Rows[rows].Cells[4].Value = t_user.Employment.ToShortDateString();
                tbl.Rows[rows].Cells[5].Value = t_user.role();
                tbl.Rows[rows].Cells[6].Value = t_user.Login;
                tbl.Rows[rows].Cells[7].Value = t_user.Password;
                tbl.Rows[rows].Cells[8].Value = t_user.Telephone;
                tbl.Rows[rows].Cells[9].Value = t_user.position();
                rows++;
            }
            Connect.Close();
        }//Поиск рабочего
        public void search_client(DataGridView tbl, string text)
        {
            tbl.RowCount = 0;
            tbl.Rows.Clear();
            int rows = 0;
            Connect.Open();
            MySqlCommand sr_clnt = new MySqlCommand("SELECT * FROM `cliente` WHERE `id` like '%" + text + "%' or `fio` like '%" + 
                Crypt.Crypt.Encrypt(text) + "%' or `email` like '%" +
                Crypt.Crypt.Encrypt(text) +
                "%' or `telephone` like '%" +
                Crypt.Crypt.Encrypt(text) + "%'", Connect);
            MySqlDataReader reader = sr_clnt.ExecuteReader();
            while (reader.Read())
            {
                tbl.RowCount++;
                tbl.Rows[rows].Cells[0].Value = reader[0].ToString();
                tbl.Rows[rows].Cells[1].Value = Crypt.Crypt.Decrypt(reader[1].ToString());
                tbl.Rows[rows].Cells[2].Value = Crypt.Crypt.Decrypt(reader[2].ToString());
                tbl.Rows[rows].Cells[3].Value = Crypt.Crypt.Decrypt(reader[3].ToString());
                tbl.Rows[rows].Cells[4].Value = Crypt.Crypt.Decrypt(reader[4].ToString());
                rows++;
            }
            Connect.Close();
        }//Поиск клиента
        public void search_order(DataGridView tbl, string text)
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
                    tbl.Rows[rows].Cells[1].Value = o.worker();
                    tbl.Rows[rows].Cells[2].Value = o.client();
                    tbl.Rows[rows].Cells[3].Value = o.Theme;
                    tbl.Rows[rows].Cells[4].Value = o.Start_date.ToShortDateString();
                    if (o.end_and_html()[0] != "")
                    {
                        tbl.Rows[rows].Cells[5].Value = Convert.ToDateTime(o.end_and_html()[0]).ToShortDateString();
                    }
                    tbl.Rows[rows].Cells[6].Value = o.status();
                    tbl.Rows[rows].Cells[7].Value = o.Price;
                    tbl.Rows[rows].Cells[8].Value = o.end_and_html()[1];
                    rows++;
                }
            }
        }//Поиск заказа
        public void get_status_name(ComboBox cb, int id_order)
        {
            Dictionary<string, string> comboSource = new Dictionary<string, string>();
            Connect.Open();
            MySqlCommand status = new MySqlCommand("select s.id,s.name from `statuses` s, `orders` o where o.status_id=s.id and o.id=" + id_order, Connect);
            MySqlDataReader reader = status.ExecuteReader();
            string st = "";
            while (reader.Read())
            {
                st = reader[0].ToString();
                comboSource.Add(reader[0].ToString(), reader[1].ToString());
            }
            Connect.Close();
            cb.DataSource = new BindingSource(comboSource, null);
            cb.DisplayMember = "Value";
            cb.ValueMember = "Key";
        }//Занесение статусов заказа в cb для изменения
        public void get_field()
        {
            StreamReader sr = new StreamReader("setting.txt", Encoding.Default);
            string line;
            string[] array = new string[19];
            int c = 0;
            //worker client theme date_start date_end status price html
            while ((line = sr.ReadLine()) != null)
            {
                array[c] = line;
                c++;
            }
            sr.Close();
            Properties.Settings.Default.worker = Convert.ToBoolean(array[0]);
            Properties.Settings.Default.client = Convert.ToBoolean(array[1]);
            Properties.Settings.Default.theme = Convert.ToBoolean(array[2]);
            Properties.Settings.Default.start_data = Convert.ToBoolean(array[3]);
            Properties.Settings.Default.end_data = Convert.ToBoolean(array[4]);
            Properties.Settings.Default.status = Convert.ToBoolean(array[5]);
            Properties.Settings.Default.price = Convert.ToBoolean(array[6]);
            Properties.Settings.Default.html = Convert.ToBoolean(array[7]);
            Properties.Settings.Default.show_id = Convert.ToBoolean(array[8]);
            Properties.Settings.Default.multi = Convert.ToBoolean(array[9]);
            Properties.Settings.Default.only = array[10].ToString();
            Properties.Settings.Default.date_set = Convert.ToBoolean(array[11]);
            Properties.Settings.Default.style_date = array[12].ToString();
            Properties.Settings.Default.s_date = Convert.ToDateTime(array[13]);
            Properties.Settings.Default.e_date = Convert.ToDateTime(array[14]);
            Properties.Settings.Default.size_text_word = Convert.ToInt32(array[15]);
            Properties.Settings.Default.header_word = Convert.ToBoolean(array[16]);
            Properties.Settings.Default.text_header_word = array[17].ToString();
            Properties.Settings.Default.text_style_word = array[18].ToString();
        }//Запись данных в параметры из текстового файла с параметрами
        public bool get_setting(int index)
        {
            //worker client theme date_start date_end status price html
            bool flag = true;
            switch (index)
            {
                case 0://worker
                    if (Properties.Settings.Default.worker)
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }
                    break;
                case 1://client
                    if (Properties.Settings.Default.client)
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }
                    break;
                case 2://theme
                    if (Properties.Settings.Default.theme)
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }
                    break;
                case 3://start_data
                    if (Properties.Settings.Default.start_data)
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }
                    break;
                case 4://end_data
                    if (Properties.Settings.Default.end_data)
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }
                    break;
                case 5://status
                    if (Properties.Settings.Default.status)
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }
                    break;
                case 6://price
                    if (Properties.Settings.Default.price)
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }
                    break;
                case 7://html
                    if (Properties.Settings.Default.html)
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }
                    break;
                case 8://show id
                    if (Properties.Settings.Default.show_id)
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }
                    break;
                case 9://multi
                    if (Properties.Settings.Default.multi)
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }
                    break;
                case 10://searh for data
                    if (Properties.Settings.Default.date_set)
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }
                    break;
                case 11://header
                    if (Properties.Settings.Default.header_word)
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }
                    break;
                default:
                    flag = false;
                    break;
            }
            return flag;
        }//Получение состояния параметра по его индексу
        public void set_control(Settings_report f, General gen)
        {
            //size_text_word
            f.cb_size_text.Text = Properties.Settings.Default.size_text_word.ToString();
            f.tb_header_text.Text = Properties.Settings.Default.text_header_word;
            f.cb_style_text.Text = Properties.Settings.Default.text_style_word;
            //header_word
            if (gen.get_setting(11))
            {
                f.cb_header.Checked = true;
            }
            else
            {
                f.cb_header.Checked = false;
            }
            //worker
            if (gen.get_setting(0))
            {
                f.cb_worker.Checked = true;
            }
            else
            {
                f.cb_worker.Checked = false;
            }
            //client
            if (gen.get_setting(1))
            {
                f.cb_client.Checked = true;
            }
            else
            {
                f.cb_client.Checked = false;
            }
            //theme
            if (gen.get_setting(2))
            {
                f.cb_theme.Checked = true;
            }
            else
            {
                f.cb_theme.Checked = false;
            }
            //start data
            if (gen.get_setting(3))
            {
                f.cb_start_date.Checked = true;
            }
            else
            {
                f.cb_start_date.Checked = false;
            }
            //end data
            if (gen.get_setting(4))
            {
                f.cb_end_date.Checked = true;
            }
            else
            {
                f.cb_end_date.Checked = false;
            }
            //status
            if (gen.get_setting(5))
            {
                f.cb_status.Checked = true;
            }
            else
            {
                f.cb_status.Checked = false;
            }
            //price
            if (gen.get_setting(6))
            {
                f.cb_price.Checked = true;
            }
            else
            {
                f.cb_price.Checked = false;
            }
            //html
            if (gen.get_setting(7))
            {
                f.cb_html.Checked = true;
            }
            else
            {
                f.cb_html.Checked = false;
            }
            //show id
            if (gen.get_setting(8))
            {
                f.cb_show_id.Checked = true;
            }
            else
            {
                f.cb_show_id.Checked = false;
            }
            //multi
            if (gen.get_setting(9))
            {
                f.cb_multi.Checked = true;
                f.cb_field.Visible = true;
                f.cb_field.Text = Properties.Settings.Default.only;
            }
            else
            {
                f.cb_multi.Checked = false;
                f.cb_field.Visible = false;
            }
            //search for date
            if (gen.get_setting(10))
            {
                f.cb_date.Checked = true;
                f.cb_style_date.Text = Properties.Settings.Default.style_date;
                f.dt_s_date.Value = Properties.Settings.Default.s_date;
                f.dt_e_date.Value = Properties.Settings.Default.e_date;
            }
            else
            {
                f.cb_date.Checked = false;
            }
        }//Установление состояния контролов на форме настройки
        public void set_columns(DataGridView tbl, General gen)
        {
            //worker
            if (gen.get_setting(7) == false)
            {
                tbl.Columns.RemoveAt(8);
            }
            //client
            if (gen.get_setting(6) == false)
            {
                tbl.Columns.RemoveAt(7);
            }
            //theme
            if (gen.get_setting(5) == false)
            {
                tbl.Columns.RemoveAt(6);
            }
            //start data
            if (gen.get_setting(4) == false)
            {
                tbl.Columns.RemoveAt(5);
            }
            //end data
            if (gen.get_setting(3) == false)
            {
                tbl.Columns.RemoveAt(4);
            }
            //status
            if (gen.get_setting(2) == false)
            {
                tbl.Columns.RemoveAt(3);
            }
            //price
            if (gen.get_setting(1) == false)
            {
                tbl.Columns.RemoveAt(2);
            }
            //html
            if (gen.get_setting(0) == false)
            {
                tbl.Columns.RemoveAt(1);
            }
        }//Удаление ячеек из таблицы исходя из отбора
        public void refresh_reports(DataGridView tbl, General gen)
        {
            int count = tbl.Columns.Count;
            for (int i = 0; i < count; i++)
            {
                tbl.Columns.RemoveAt(0);
            }
            string[] array = { "id", "Сотрудник", "Клиент", "Тема", "Дата начала", "Конечная дата", "Статус", "Стоимость", "html код" };
            for (int i = 0; i < 9; i++)
            {
                tbl.Columns.Add("column" + i, array[i]);
            }
            gen.orders(tbl);
            gen.set_columns(tbl, gen);
        }//Обновление таблицы для отображения изменений
        public void search_report_multi(DataGridView tbl, string text, General gen)
        {
            int count = tbl.Columns.Count;
            for (int i = 0; i < count; i++)
            {
                tbl.Columns.RemoveAt(0);
            }
            string[] array = { "id", "Сотрудник", "Клиент", "Тема", "Дата начала", "Конечная дата", "Статус", "Стоимость", "html код" };
            for (int i = 0; i < 9; i++)
            {
                tbl.Columns.Add("column" + i, array[i]);
            }
            gen.search_order(tbl, text);
            gen.set_columns(tbl, gen);
        }//Обычный поиск на форме отчета восстановление колонок
        public void search_report_only(DataGridView tbl, string text, General gen)
        {
            string query = "";
            switch (Properties.Settings.Default.only)
            {
                case "id":
                    query = "select * from `orders` o, `html_orders` h where o.id=h.order_id and o.id like '%" + text + "%'";
                    break;
                case "Сотрудник":
                    query = "select * from `orders` o, `html_orders` h, `workers` w where o.id=h.order_id and w.fio like '%" + Crypt.Crypt.Encrypt(text) + "%' and w.id=o.worker_id";
                    break;
                case "Клиент":
                    query = "select * from `orders` o, `html_orders` h, `cliente` c where o.id=h.order_id and c.fio like '%" + Crypt.Crypt.Encrypt(text) + "%' and c.id=o.client_id";
                    break;
                case "Тема":
                    query = "select * from `orders` o, `html_orders` h where o.id=h.order_id and o.theme like '%" + text + "%'";
                    break;
                case "Статус":
                    query = "select* from `orders` o, `html_orders` h, `statuses` s where o.id = h.order_id and s.name like '%" + text + "%' and s.id = o.status_id";
                    break;
                case "Стоимость":
                    query = "select * from `orders` o, `html_orders` h where o.id=h.order_id and o.price like '%" + text + "%'";
                    break;
                case "html код":
                    query = "select * from `orders` o, `html_orders` h where o.id=h.order_id and h.html_id like '%" + Crypt.Crypt.Encrypt(text) + "%'";
                    break;
            }
            int count = tbl.Columns.Count;
            for (int i = 0; i < count; i++)
            {
                tbl.Columns.RemoveAt(0);
            }
            string[] array = { "id", "Сотрудник", "Клиент", "Тема", "Дата начала", "Конечная дата", "Статус", "Стоимость", "html код" };
            for (int i = 0; i < 9; i++)
            {
                tbl.Columns.Add("column" + i, array[i]);
            }
            Connect.Open();
            MySqlCommand comm = new MySqlCommand(query, Connect);
            MySqlDataReader reader = comm.ExecuteReader();
            int rows = 0;
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
                    tbl.Rows[rows].Cells[5].Value = Convert.ToDateTime(reader[5]).ToShortDateString();
                }
                else
                {
                    tbl.Rows[rows].Cells[5].Value = "";
                }
                tbl.Rows[rows].Cells[0].Value = ord_t.Id;
                tbl.Rows[rows].Cells[1].Value = ord_t.worker();
                tbl.Rows[rows].Cells[2].Value = ord_t.client();
                tbl.Rows[rows].Cells[3].Value = ord_t.Theme;
                tbl.Rows[rows].Cells[4].Value = ord_t.Start_date.ToShortDateString();
                tbl.Rows[rows].Cells[6].Value = ord_t.status();
                tbl.Rows[rows].Cells[7].Value = ord_t.Price;
                tbl.Rows[rows].Cells[8].Value = Crypt.Crypt.Decrypt(reader[9].ToString());
                rows++;
            }
            Connect.Close();
            gen.set_columns(tbl, gen);
        }//Поиск по одному полю
        public void set_only_view(PictureBox pb, Label lb,TextBox srch,Label s_d,Label e_d)
        {
            srch.Enabled = true;
            if (get_setting(9) == true)
            {
                pb.Visible = true;
                lb.Text = Properties.Settings.Default.only;
                lb.Visible = true;
                s_d.Visible = false;
                e_d.Visible = false;
            }
            else if (get_setting(10) == true)
            {
                pb.Visible = true;
                lb.Text = Properties.Settings.Default.style_date;
                lb.Visible = true;
                srch.Enabled = false;
                switch (Properties.Settings.Default.style_date)
                {
                    case "Дата начала (= От)":
                        s_d.Visible = true;
                        s_d.Text = "От " + Properties.Settings.Default.s_date.ToString("yyyy-MM-dd");
                        break;
                    case "Дата начала (От и До)":
                        s_d.Visible = true;
                        s_d.Text = "От " + Properties.Settings.Default.s_date.ToString("yyyy-MM-dd");
                        e_d.Visible = true;
                        e_d.Text = "До " + Properties.Settings.Default.e_date.ToString("yyyy-MM-dd");
                        break;
                    case "Дата окончания (= От)":
                        s_d.Visible = true;
                        s_d.Text = "От " + Properties.Settings.Default.s_date.ToString("yyyy-MM-dd");
                        break;
                    case "Дата окончания (От и До)":
                        s_d.Visible = true;
                        s_d.Text = "От " + Properties.Settings.Default.s_date.ToString("yyyy-MM-dd");
                        e_d.Visible = true;
                        e_d.Text = "До " + Properties.Settings.Default.e_date.ToString("yyyy-MM-dd");
                        break;
                    case "Дата начала и окончания (>=От и <=До)":
                        s_d.Visible = true;
                        s_d.Text = "От " + Properties.Settings.Default.s_date.ToString("yyyy-MM-dd");
                        e_d.Visible = true;
                        e_d.Text = "До " + Properties.Settings.Default.e_date.ToString("yyyy-MM-dd");
                        break;
                    case "Дата начала и окончания (От и До)":
                        s_d.Visible = true;
                        s_d.Text = "От " + Properties.Settings.Default.s_date.ToString("yyyy-MM-dd");
                        e_d.Visible = true;
                        e_d.Text = "До " + Properties.Settings.Default.e_date.ToString("yyyy-MM-dd");
                        break;
                    case "Не законченные":
                        s_d.Visible = false;
                        e_d.Visible = false;
                        break;
                }
            }
            else
            {
                pb.Visible = false;
                lb.Visible = false;
                s_d.Visible = false;
                e_d.Visible = false;
            }
        }//Скрытие или отображение предупреждения о поиске по одному полю
        public void display_by_date(DataGridView tbl, General gen)
        {
            table_styling(tbl);
            Connect.Open();
            string query = "";
            switch (Properties.Settings.Default.style_date)
            {
                case "Дата начала (= От)":
                    query = "select * from `orders` o, `html_orders` h where o.start_date='" + 
                        Properties.Settings.Default.s_date.ToString("yyyy-MM-dd") +"' and o.id=h.order_id";
                    break;
                case "Дата начала (От и До)":
                    query = "select * from `orders` o, `html_orders` h where (o.start_date between date('"+
                        Properties.Settings.Default.s_date.ToString("yyyy-MM-dd")+"') and date('"+
                        Properties.Settings.Default.e_date.ToString("yyyy-MM-dd")+"')) and o.id=h.order_id";
                    break;
                case "Дата окончания (= От)":
                    query = "select * from `orders` o, `html_orders` h where o.end_date='" + 
                        Properties.Settings.Default.s_date.ToString("yyyy-MM-dd") + "' and o.id=h.order_id";
                    break;
                case "Дата окончания (От и До)":
                    query = "select * from `orders` o, `html_orders` h where (o.end_date between date('" + 
                        Properties.Settings.Default.s_date.ToString("yyyy-MM-dd") + "') and date('" + 
                        Properties.Settings.Default.e_date.ToString("yyyy-MM-dd") + "')) and o.id=h.order_id";
                    break;
                case "Дата начала и окончания (>=От и <=До)":
                    query = "select * from `orders` o, `html_orders` h where (o.start_date between date('"+
                        Properties.Settings.Default.s_date.ToString("yyyy-MM-dd")+"') and date('"+
                        Properties.Settings.Default.e_date.ToString("yyyy-MM-dd")+"')) and (o.end_date between date('"+
                        Properties.Settings.Default.s_date.ToString("yyyy-MM-dd")+"') and date('"+
                        Properties.Settings.Default.e_date.ToString("yyyy-MM-dd")+"')) and o.id=h.order_id";
                    break;
                case "Дата начала и окончания (От и До)":
                    query = "select * from `orders` o, `html_orders` h where o.start_date='" + 
                        Properties.Settings.Default.s_date.ToString("yyyy-MM-dd") + "' and o.end_date='" + 
                        Properties.Settings.Default.e_date.ToString("yyyy-MM-dd") + "' and o.id=h.order_id";
                    break;
                case "Не законченные":
                    query = "select * from `orders` o, `html_orders` h where o.end_date is null and o.id=h.order_id";
                    break;
            }
            MySqlCommand command = new MySqlCommand(query,Connect);
            MySqlDataReader reader = command.ExecuteReader();
            int count = tbl.Columns.Count;
            for (int i = 0; i < count; i++)
            {
                tbl.Columns.RemoveAt(0);
            }
            string[] array = { "id", "Сотрудник", "Клиент", "Тема", "Дата начала", "Конечная дата", "Статус", "Стоимость", "html код" };
            for (int i = 0; i < 9; i++)
            {
                tbl.Columns.Add("column" + i, array[i]);
            }
            int rows = 0;
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
                    tbl.Rows[rows].Cells[5].Value = Convert.ToDateTime(reader[5]).ToShortDateString();
                }
                else
                {
                    tbl.Rows[rows].Cells[5].Value = "";
                }
                tbl.Rows[rows].Cells[0].Value = ord_t.Id;
                tbl.Rows[rows].Cells[1].Value = ord_t.worker();
                tbl.Rows[rows].Cells[2].Value = ord_t.client();
                tbl.Rows[rows].Cells[3].Value = ord_t.Theme;
                tbl.Rows[rows].Cells[4].Value = ord_t.Start_date.ToShortDateString();
                tbl.Rows[rows].Cells[6].Value = ord_t.status();
                tbl.Rows[rows].Cells[7].Value = ord_t.Price;
                tbl.Rows[rows].Cells[8].Value = Crypt.Crypt.Decrypt(reader[9].ToString());
                rows++;
            }
            Connect.Close();
            gen.set_columns(tbl, gen);
        }//Отображение данных по выбанному диапазону дат
    }
}