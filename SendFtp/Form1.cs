using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace SendFtp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string ficheroEnviar;
        private void button1_Click(object sender, EventArgs e)
        {
            SendFtpFile(textBox1.Text, ficheroEnviar, txtServidor.Text, txtRuta.Text, txtUsuario.Text, txtContraseña.Text);
        }

        public void SendFtpFile(string ftpFichero, string ficheroEnviar, string Servidor, string ruta, string usuario, string contra)
        {
            string ftpRutaCompleta = "ftp://" + Servidor + ruta + ftpFichero;
            FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(ftpRutaCompleta);
            ftp.Credentials = new NetworkCredential(usuario, contra);

            ftp.KeepAlive = true;
            ftp.UseBinary = true;
            ftp.Method = WebRequestMethods.Ftp.UploadFile;
            FileStream fs = File.OpenRead(ficheroEnviar);
            byte[] buffer = new byte[fs.Length];
            fs.Read(buffer, 0, buffer.Length);
            fs.Close();
            Stream ftpstream = ftp.GetRequestStream();
            ftpstream.Write(buffer, 0, buffer.Length);
            ftpstream.Close();
            MessageBox.Show("Elemento subido");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialogoAbrir = new OpenFileDialog();
            dialogoAbrir.Title = "Open a Image";
            dialogoAbrir.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            dialogoAbrir.Filter = "All Files (*.*)|*.*|";
            if (dialogoAbrir.ShowDialog() == DialogResult.OK)
            {
                ficheroEnviar = dialogoAbrir.FileName;
            }
        }
    }
}
