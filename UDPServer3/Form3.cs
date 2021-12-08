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
    public partial class Form3 : Form
    {

        bool toRight = true;
        int x = 0;

        public Form3()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            GraphicLib gl = new GraphicLib();
            Graphics field = pictureBox1.CreateGraphics();
            gl.initField(field, pictureBox1.Width, pictureBox1.Height);

            Random rnd = new Random();
            
            gl.Clear(255, 255, 255);
            Int16 colorR = (Int16)rnd.Next(0, 255);
            Int16 colorG = (Int16)rnd.Next(0, 255);
            Int16 colorB = (Int16)rnd.Next(0, 255);

            gl.DrawFillRectangle(0, 0, (Int16)pictureBox1.Width, (Int16)(TikerData.size * 2), colorR, colorG, colorB);

            if (x >= pictureBox1.Width)
            {
                toRight = false;
            }
            else if (x <= 0)
            {
                toRight = true;
            }

            if (toRight == true)
            {
                x = x + 10;
            }
            else if (toRight == false)
            {
                x = x - 10;
            }
            gl.DrawText((Int16)x, (Int16)(TikerData.size/4), TikerData.text, TikerData.font, TikerData.size, TikerData.colorR, TikerData.colorG, TikerData.colorB);


        }

        private void Form3_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }
}
