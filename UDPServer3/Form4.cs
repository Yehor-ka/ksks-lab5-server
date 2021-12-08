using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UDPServer3
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Graphics field = pictureBox1.CreateGraphics();
            string bg = "pinPending2";
            if (ArduinoStatus.response == "Неверный номер функции" || ArduinoStatus.response == "Недостаточно входных параметров" || ArduinoStatus.response == "Параметры имеют недопустимые значения" || ArduinoStatus.response == "Входная строка имела неверный формат.")
            {
                bg = "pinError2";
            }
            else if (ArduinoStatus.response != "" && ArduinoStatus.pending != true)
            {
                bg = "pinSuccess2";
            }
            string urlPic = @"D:\Photo\" + bg + ".png";
            pictureBox1.Image = Image.FromFile(urlPic);
        }
    }
}
