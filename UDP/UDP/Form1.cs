﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Threading;

namespace UDP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            Client.GetFileDetails();
            MessageBox.Show("----Информация о файле получена!" + "Получен файл типа ." + Client.fileDet.FILETYPE +
                    " имеющий размер " + Client.fileDet.FILESIZE.ToString() + " байт");
            MessageBox.Show("-------Открытие файла------");
            Client.ReceiveFile();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                MessageBox.Show("Введите удаленный IP-адрес в поле");
                Client.remoteIPAddress = IPAddress.Parse(textBox1.Text.ToString());//"127.0.0.1");
                Client.endPoint = new IPEndPoint(Client.remoteIPAddress, Client.remotePort);
                textBox1.Clear();

                MessageBox.Show("Введите путь к файлу и его имя в поле"); 
                Client.fs = new FileStream(textBox1.Text.ToString(), FileMode.Open, FileAccess.Read);

                if (Client.fs.Length > 8192)
                {
                    MessageBox.Show("Файл должен весить меньше 8кБ"); 
                    Client.sender.Close();
                    Client.fs.Close();
                    return;
                }

                Client.SendFileInfo();
                MessageBox.Show("Отправка файла размером " + Client.fs1.Length + " байт");
               
                Thread.Sleep(2000);

                Client.SendFile();
                MessageBox.Show("Файл успешно отправлен.");
                Console.ReadLine();

            }
            catch (Exception eR)
            {
                Console.WriteLine(eR.ToString());
            }
            Client.SendFileInfo();
            Client.SendFile();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
