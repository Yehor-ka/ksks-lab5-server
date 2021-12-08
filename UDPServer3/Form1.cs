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
using System.Net.Sockets;
using static UDPServer3.GraphicLib;

namespace UDPServer3
{

    public partial class Form1 : Form
    {
        UdpClient Server = new UdpClient(54000);

        string data = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            richTextBox1.ReadOnly = true;
            try
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    ArduinoStatus.pending = true;
                    Form4 newForm = new Form4();
                    newForm.Show();
                }));
                Server.BeginReceive(new AsyncCallback(recv), null);

            }
            catch (Exception ex)
            {
                richTextBox1.Text += ex.Message.ToString();
            }
        }
        void recv(IAsyncResult res)
        {
            IPEndPoint RemoteIP = new IPEndPoint(IPAddress.Any, 54000);
            byte[] received = Server.EndReceive(res, ref RemoteIP);
            data = Encoding.UTF8.GetString(received);
            string[] splitedArray = { };
            splitedArray = GetSplitedData(data);
            string commandResult = parserCommand(splitedArray);
            ArduinoStatus.response = commandResult;
            ArduinoStatus.pending = false;
            this.Invoke(new MethodInvoker(delegate
            {
                richTextBox1.Text += "\nReceived command: " + commandResult;
            }));

            Server.BeginReceive(new AsyncCallback(recv), null);
        }

        public static string[] GetSplitedData(string command)
        {
            return command.Trim().Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
        }

        string parserCommand(string[] command)
        {
            GraphicLib gl = new GraphicLib();
            Graphics field = pictureBox1.CreateGraphics();
            gl.initField(field, pictureBox1.Width, pictureBox1.Height);

            Int16 x0 = 0;
            Int16 y0 = 0;
            Int16 x1 = 0;
            Int16 y1 = 0;
            Int16 w = 0;
            Int16 h = 0;
            Int16 size = 0;
            string drawText = "";
            string font = "";
            Int16 colorR = 0;
            Int16 colorG = 0;
            Int16 colorB = 0;
            string errorMsg = "Неверный номер функции";
            string errorCountParams = "Недостаточно входных параметров";


            SolidBrush d;
            try
            {
                int funcNum = Convert.ToInt32(command[0]);
                switch (funcNum)
                {
                    case 1:
                        if (command.Length >= 4)
                        {
                            colorR = Convert.ToInt16(command[1]);
                            colorG = Convert.ToInt16(command[2]);
                            colorB = Convert.ToInt16(command[3]);
                            if (colorR > 255
                                    || colorG > 255
                                    || colorB > 255
                                    || colorR < 0
                                    || colorG < 0
                                    || colorB < 0)
                            {
                                return "Параметры имеют недопустимые значения";
                            }
                            else
                            {
                                gl.Clear(colorR, colorG, colorB);
                                return "Clear";
                            }
                        }
                        else
                        {
                            return errorCountParams;
                        }
                    case 2:
                        if (command.Length >= 6)
                        {
                            x0 = Convert.ToInt16(command[1]);
                            y0 = Convert.ToInt16(command[2]);
                            colorR = Convert.ToInt16(command[3]);
                            colorG = Convert.ToInt16(command[4]);
                            colorB = Convert.ToInt16(command[5]);
                            if (x0 > pictureBox1.Width
                                || y0 > pictureBox1.Height
                                || x0 < 0
                                || y0 < 0
                                || colorR > 255
                                || colorG > 255
                                || colorB > 255
                                || colorR < 0
                                || colorG < 0
                                || colorB < 0)
                            {
                                return "Параметры имеют недопустимые значения";
                            }
                            else
                            {
                                gl.DrawPixel(x0, y0, colorR, colorG, colorB);
                                return "Draw pixel";
                            }
                        }
                        else
                        {
                            return errorCountParams;
                        }
                    case 3:
                        if (command.Length >= 8)
                        {
                            x0 = Convert.ToInt16(command[1]);
                            y0 = Convert.ToInt16(command[2]);
                            x1 = Convert.ToInt16(command[3]);
                            y1 = Convert.ToInt16(command[4]);
                            colorR = Convert.ToInt16(command[5]);
                            colorG = Convert.ToInt16(command[6]);
                            colorB = Convert.ToInt16(command[7]);
                            if (x0 > pictureBox1.Width
                                || y0 > pictureBox1.Height
                                || x0 < 0
                                || y0 < 0
                                || colorR > 255
                                || colorG > 255
                                || colorB > 255
                                || colorR < 0
                                || colorG < 0
                                || colorB < 0)
                            {
                                return "Параметры имеют недопустимые значения";
                            }
                            else
                            {
                                gl.DrawLine(x0, y0, x1, y1, colorR, colorG, colorB);
                                return "Draw line";
                            }
                        }
                        else
                        {
                            return errorCountParams;
                        }
                    case 4:
                        if (command.Length >= 8)
                        {
                            x0 = Convert.ToInt16(command[1]);
                            y0 = Convert.ToInt16(command[2]);
                            w = Convert.ToInt16(command[3]);
                            h = Convert.ToInt16(command[4]);
                            colorR = Convert.ToInt16(command[5]);
                            colorG = Convert.ToInt16(command[6]);
                            colorB = Convert.ToInt16(command[7]);
                            if (x0 > pictureBox1.Width
                                || y0 > pictureBox1.Height
                                || x0 < 0
                                || y0 < 0
                                || colorR > 255
                                || colorG > 255
                                || colorB > 255
                                || colorR < 0
                                || colorG < 0
                                || colorB < 0)
                            {
                                return "Параметры имеют недопустимые значения";
                            }
                            else
                            {
                                gl.DrawRectangle(x0, y0, w, h, colorR, colorG, colorB);
                                return "Rectangle";
                            }
                        }
                        else
                        {
                            return errorCountParams;
                        }
                    case 5:
                        if (command.Length >= 8)
                        {
                            x0 = Convert.ToInt16(command[1]);
                            y0 = Convert.ToInt16(command[2]);
                            w = Convert.ToInt16(command[3]);
                            h = Convert.ToInt16(command[4]);
                            colorR = Convert.ToInt16(command[5]);
                            colorG = Convert.ToInt16(command[6]);
                            colorB = Convert.ToInt16(command[7]);
                            if (x0 > pictureBox1.Width
                                || y0 > pictureBox1.Height
                                || x0 < 0
                                || y0 < 0
                                || colorR > 255
                                || colorG > 255
                                || colorB > 255
                                || colorR < 0
                                || colorG < 0
                                || colorB < 0)
                            {
                                return "Параметры имеют недопустимые значения";
                            }
                            else
                            {
                                gl.DrawFillRectangle(x0, y0, w, h, colorR, colorG, colorB);
                                return "Fill Rectangle";
                            }
                        }
                        else
                        {
                            return errorCountParams;
                        }
                    case 6:
                        if (command.Length >= 8)
                        {
                            x0 = Convert.ToInt16(command[1]);
                            y0 = Convert.ToInt16(command[2]);
                            w = Convert.ToInt16(command[3]);
                            h = Convert.ToInt16(command[4]);
                            colorR = Convert.ToInt16(command[5]);
                            colorG = Convert.ToInt16(command[6]);
                            colorB = Convert.ToInt16(command[7]);
                            if (x0 > pictureBox1.Width
                                || y0 > pictureBox1.Height
                                || x0 < 0
                                || y0 < 0
                                || colorR > 255
                                || colorG > 255
                                || colorB > 255
                                || colorR < 0
                                || colorG < 0
                                || colorB < 0)
                            {
                                return "Параметры имеют недопустимые значения";
                            }
                            else
                            {
                                gl.DrawEllipse(x0, y0, w, h, colorR, colorG, colorB);
                                return "Draw ellipse";
                            }
                        }
                        else
                        {
                            return errorCountParams;
                        }
                    case 7:
                        if (command.Length >= 8)
                        {
                            x0 = Convert.ToInt16(command[1]);
                            y0 = Convert.ToInt16(command[2]);
                            w = Convert.ToInt16(command[3]);
                            h = Convert.ToInt16(command[4]);
                            colorR = Convert.ToInt16(command[5]);
                            colorG = Convert.ToInt16(command[6]);
                            colorB = Convert.ToInt16(command[7]);
                            if (x0 > pictureBox1.Width
                                || y0 > pictureBox1.Height
                                || x0 < 0
                                || y0 < 0
                                || colorR > 255
                                || colorG > 255
                                || colorB > 255
                                || colorR < 0
                                || colorG < 0
                                || colorB < 0)
                            {
                                return "Параметры имеют недопустимые значения";
                            }
                            else
                            {
                                gl.DrawFillEllipse(x0, y0, w, h, colorR, colorG, colorB);
                                return "Fill ellipse";
                            }
                        }
                        else
                        {
                            return errorCountParams;
                        }
                    case 8:
                        if (command.Length >= 7)
                        {
                            x0 = Convert.ToInt16(command[1]);
                            y0 = Convert.ToInt16(command[2]);
                            w = Convert.ToInt16(command[3]);
                            colorR = Convert.ToInt16(command[4]);
                            colorG = Convert.ToInt16(command[5]);
                            colorB = Convert.ToInt16(command[6]);
                            if (x0 > pictureBox1.Width
                                || y0 > pictureBox1.Height
                                || x0 < 0
                                || y0 < 0
                                || colorR > 255
                                || colorG > 255
                                || colorB > 255
                                || colorR < 0
                                || colorG < 0
                                || colorB < 0)
                            {
                                return "Параметры имеют недопустимые значения";
                            }
                            else
                            {
                                gl.DrawCircle(x0, y0, w, colorR, colorG, colorB);
                                return "Draw circle";
                            }
                        }
                        else
                        {
                            return errorCountParams;
                        }
                    case 9:
                        if (command.Length >= 7)
                        {
                            x0 = Convert.ToInt16(command[1]);
                            y0 = Convert.ToInt16(command[2]);
                            w = Convert.ToInt16(command[3]);
                            colorR = Convert.ToInt16(command[4]);
                            colorG = Convert.ToInt16(command[5]);
                            colorB = Convert.ToInt16(command[6]);
                            if (x0 > pictureBox1.Width
                                || y0 > pictureBox1.Height
                                || x0 < 0
                                || colorR > 255
                                || colorG > 255
                                || colorB > 255
                                || colorR < 0
                                || colorG < 0
                                || colorB < 0)
                            {
                                return "Параметры имеют недопустимые значения";
                            }
                            else
                            {
                                gl.DrawFillCircle(x0, y0, w, colorR, colorG, colorB);
                                return "Fill circle";
                            }

                        }
                        else
                        {
                            return errorCountParams;
                        }
                    case 10:
                        return "Ширина: " + gl.GetWidth() + "px";
                    case 11:
                        return "Высота: " + gl.GetHeight() + "px";
                    case 12:
                        if (command.Length >= 9)
                        {
                            x0 = Convert.ToInt16(command[1]);
                            y0 = Convert.ToInt16(command[2]);
                            drawText = command[3];
                            font = command[4];
                            size = Convert.ToInt16(command[5]);
                            colorR = Convert.ToInt16(command[6]);
                            colorG = Convert.ToInt16(command[7]);
                            colorB = Convert.ToInt16(command[8]);
                            if (x0 > pictureBox1.Width
                                || y0 > pictureBox1.Height
                                || x0 < 0
                                || y0 < 0
                                || colorR > 255
                                || colorG > 255
                                || colorB > 255
                                || colorR < 0
                                || colorG < 0
                                || colorB < 0)
                            {
                                return "Параметры имеют недопустимые значения";
                            }
                            else
                            {
                                gl.DrawText(x0, y0, drawText, font, size, colorR, colorG, colorB);
                                return "Draw text";
                            }

                        }
                        else
                        {
                            return errorCountParams;
                        }
                    case 13:
                        if(command.Length >= 1)
                        {
                            
                            Random rnd = new Random();

                            Int16 form = Convert.ToInt16(rnd.Next(1, 3));
                            colorR = Convert.ToInt16(rnd.Next(0, 255));
                            colorG = Convert.ToInt16(rnd.Next(0, 255));
                            colorB = Convert.ToInt16(rnd.Next(0, 255));
                            ClockData.form = form;
                            ClockData.colorR = colorR;
                            ClockData.colorG = colorG;
                            ClockData.colorB = colorB;
                            this.Invoke(new MethodInvoker(delegate
                            {
                                Form2 newForm = new Form2();
                                newForm.Show();
                            }));
                            return "Digital clock";
                         
                        } 
                        else
                        {
                            return errorCountParams;
                        }
                        
                    case 14:
                        if (command.Length >= 6)
                        {
                            x0 = Convert.ToInt16(command[1]);
                            y0 = Convert.ToInt16(command[2]);
                            colorR = Convert.ToInt16(command[3]);
                            colorG = Convert.ToInt16(command[4]);
                            colorB = Convert.ToInt16(command[5]);
                            if (colorR > 255
                                || colorG > 255
                                || colorB > 255
                                || colorR < 0
                                || colorG < 0
                                || colorB < 0)
                            {
                                return "Параметры имеют недопустимые значения";
                            }
                            else
                            {
                                gl.DrawStar(x0, y0, colorR, colorG, colorB);
                                return "Draw Star";
                            }

                        }
                        else
                        {
                            return errorCountParams;
                        }
                    case 15:
                        if (command.Length >= 7)
                        {
                            drawText = command[1];
                            font = command[2];
                            size = Convert.ToInt16(command[3]);
                            colorR = Convert.ToInt16(command[4]);
                            colorG = Convert.ToInt16(command[5]);
                            colorB = Convert.ToInt16(command[6]);
                            string[] fontArr = font.Split(new[] { "_" }, StringSplitOptions.RemoveEmptyEntries);
                            string[] drawTextArr = drawText.Split(new[] { "_" }, StringSplitOptions.RemoveEmptyEntries);
                            drawText = "";
                            font = "";
                            for (int i = 0; i < fontArr.Length; i++)
                            {
                                font += fontArr[i] + " ";
                            }

                            for (int i = 0; i < drawTextArr.Length; i++)
                            {
                                drawText += drawTextArr[i] + " ";
                            }

                            if (colorR > 255
                                || colorG > 255
                                || colorB > 255
                                || colorR < 0
                                || colorG < 0
                                || colorB < 0)
                            {
                                return "Параметры имеют недопустимые значения";
                            }
                            else
                            {
                                TikerData.text = drawText;
                                TikerData.size = size;
                                TikerData.font = font;
                                TikerData.colorR = colorR;
                                TikerData.colorG = colorG;
                                TikerData.colorB = colorB;

                                this.Invoke(new MethodInvoker(delegate
                                {
                                    Form3 newForm = new Form3();
                                    newForm.Show();
                                }));
                                return "Бегущая строка";
                            }

                        }
                        else
                        {
                            return errorCountParams;
                        }
                    default:
                        return errorMsg;
                }
            }
            catch (FormatException e)
            {
                return e.Message;//"PARSING ERROR";
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 newForm = new Form2();
            newForm.Show();
        }
    }
}
