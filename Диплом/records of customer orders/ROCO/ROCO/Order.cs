using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ROCO
{
    class Order
    {
        private int id;
        private int worker_id;
        private int client_id;
        private string theme;
        private DateTime start_date;
        private DateTime end_date;
        private int status_id;
        private int price;
        public MySqlConnection Connect = new MySqlConnection(Properties.Settings.Default.ConStr);

        public string worker()
        {
            string wrkr = "";
            Connect.Open();
            MySqlCommand get_wrkr = new MySqlCommand("select w.fio from `workers` w, `orders` o where o.worker_id=w.id and o.id = " + this.id, Connect);
            MySqlDataReader reader = null;
            reader = get_wrkr.ExecuteReader();
            while (reader.Read())
            {
                wrkr = Crypt.Crypt.Decrypt(reader[0].ToString());
            }
            Connect.Close();
            return wrkr;
        }
        public string client()
        {
            string clnt = "";
            Connect.Open();
            MySqlCommand get_clnt = new MySqlCommand("select c.fio from `cliente` c, `orders` o where o.client_id=c.id and o.id = " + this.id, Connect);
            MySqlDataReader reader = null;
            reader = get_clnt.ExecuteReader();
            while (reader.Read())
            {
                clnt = Crypt.Crypt.Decrypt(reader[0].ToString());
            }
            Connect.Close();
            return clnt;
        }
        public string status()
        {
            string stts = "";
            Connect.Open();
            MySqlCommand get_stts = new MySqlCommand("select s.name from `statuses` s, `orders` o where o.status_id=s.id and o.id = " + this.id, Connect);
            MySqlDataReader reader = null;
            reader = get_stts.ExecuteReader();
            while (reader.Read())
            {
                stts = reader[0].ToString();
            }
            Connect.Close();
            return stts;
        }

        public string[] end_and_html()
        {
            Connect.Open();
            MySqlCommand get_data = new MySqlCommand("select o.end_date, h.html_id from `orders` o, `html_orders` h where o.id = "+this.Id+" and o.id=h.order_id", Connect);
            MySqlDataReader reader = get_data.ExecuteReader();
            string[] array = new string[2];
            while (reader.Read())
            {
                array[0] = reader[0].ToString();
                array[1] = Crypt.Crypt.Decrypt(reader[1].ToString());
            }
            Connect.Close();
            return array;
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

        public int Worker_id
        {
            get
            {
                return worker_id;
            }

            set
            {
                worker_id = value;
            }
        }

        public int Client_id
        {
            get
            {
                return client_id;
            }

            set
            {
                client_id = value;
            }
        }

        public string Theme
        {
            get
            {
                return theme;
            }

            set
            {
                theme = value;
            }
        }

        public DateTime Start_date
        {
            get
            {
                return start_date;
            }

            set
            {
                start_date = value;
            }
        }

        public DateTime End_date
        {
            get
            {
                return end_date;
            }

            set
            {
                end_date = value;
            }
        }

        public int Status_id
        {
            get
            {
                return status_id;
            }

            set
            {
                status_id = value;
            }
        }

        public int Price
        {
            get
            {
                return price;
            }

            set
            {
                price = value;
            }
        }

        public Order(int id, int worker_id, int client_id, string theme, DateTime start_date, DateTime end_date, int status_id, int price)
        {
            this.Id = id;
            this.Worker_id = worker_id;
            this.Client_id = client_id;
            this.Theme = theme;
            this.Start_date = start_date;
            this.End_date = end_date;
            this.Status_id = status_id;
            this.Price = price;
        }
    }
}
