using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Collections;

namespace Crypt
{
    class Crypt
    {
        class Cryptogr
        {
            char symb;
            char encSymb;
            public char Symb
            {
                get
                {
                    return symb;
                }

                set
                {
                    symb = value;
                }
            }

            public char EncSymb
            {
                get
                {
                    return encSymb;
                }

                set
                {
                    encSymb = value;
                }
            }

            public Cryptogr(char symb, char encSymb)
            {
                this.symb = symb;
                this.encSymb = encSymb;
            }
        }

        public static string Encrypt(string text)
        {
            string encrypt_string = "";
            ArrayList array = new ArrayList();
            StreamReader sr = new StreamReader("key.txt", Encoding.Default);
            string line;
            int c = 0;
            while ((line = sr.ReadLine()) != null)
            {
                c++;
                array.Add(new Cryptogr(Convert.ToChar(line.Split(' ')[0]), Convert.ToChar(line.Split(' ')[1])));
            }

            for (int i = 0; i < text.Length; i++)
            {
                bool flag = false;
                foreach (Cryptogr symb in array)
                {
                    if (symb.Symb == text[i])
                    {
                        flag = true;
                    }
                }
                if (flag == false)
                {
                    encrypt_string += text[i];
                }
                else
                {
                    foreach (Cryptogr symb in array)
                    {
                        if (text[i] == symb.Symb)
                        {
                            encrypt_string += symb.EncSymb;
                        }
                    }
                }
            }

            return encrypt_string;
        }

        public static string Decrypt(string text)
        {

            string decrypt_string = "";
            ArrayList array = new ArrayList();
            StreamReader sr = new StreamReader("key.txt", Encoding.Default);
            string line;
            int c = 0;
            while ((line = sr.ReadLine()) != null)
            {
                c++;
                array.Add(new Cryptogr(Convert.ToChar(line.Split(' ')[0]), Convert.ToChar(line.Split(' ')[1])));
            }

            for (int i = 0; i < text.Length; i++)
            {
                bool flag = false;
                foreach (Cryptogr symb in array)
                {
                    if (symb.EncSymb == text[i])
                    {
                        flag = true;
                    }
                }
                if (flag == false)
                {
                    decrypt_string += text[i];
                }
                else
                {
                    foreach (Cryptogr symb in array)
                    {
                        if (text[i] == symb.EncSymb)
                        {
                            decrypt_string += symb.Symb;
                        }
                    }
                }
            }

            return decrypt_string;
        }
    }
}