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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            GraphicLib gl = new GraphicLib();
            Graphics field = pictureBox1.CreateGraphics();
            gl.initField(field, pictureBox1.Width, pictureBox1.Height);

            string mainTime = DateTime.Now.ToString("HH:mm");
            string second = DateTime.Now.ToString("ss");
            string date = DateTime.Now.ToString("MMM dd yyyy");
            string day = DateTime.Now.ToString("dddd");

            Random rnd = new Random();

            gl.Clear(255, 255, 255);

            gl.DrawFillRectangle(172, 80, 30, 25, ClockData.colorR, ClockData.colorG, ClockData.colorB);
            if(ClockData.form == 1)
            {
                gl.DrawCircle(50, 25, 200, ClockData.colorR, ClockData.colorG, ClockData.colorB);
            } 
            else if(ClockData.form == 2)
            {
                gl.DrawRectangle(50, 25, 200, 200, ClockData.colorR, ClockData.colorG, ClockData.colorB);
            }
            

            gl.DrawText(70, 70, mainTime, "Roboto", 26, 0, 0, 0);
            gl.DrawText(170, 80, second, "Roboto", 18, 255, 255, 255);
            gl.DrawText(70, 110, date, "Roboto", 20, 0, 0, 0);
            gl.DrawText(70, 135, day, "Roboto", 20, 0, 0, 0);
            
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }
}
