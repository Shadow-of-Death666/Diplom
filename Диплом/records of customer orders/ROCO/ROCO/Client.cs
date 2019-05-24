using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROCO
{
    class Client
    {
        private int id;
        private string fio;
        private string telephone;
        private string email;
        private string additional;

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

        public string Email
        {
            get
            {
                return email;
            }

            set
            {
                email = value;
            }
        }

        public string Additional
        {
            get
            {
                return additional;
            }

            set
            {
                additional = value;
            }
        }

        public Client(int id, string fio, string telephone, string email, string additional)
        {
            this.Id = id;
            this.Fio = fio;
            this.Telephone = telephone;
            this.Email = email;
            this.Additional = additional;
        }
    }
}
