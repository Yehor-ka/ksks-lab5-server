using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UDPServer3
{
    class GraphicLib
    {
        Graphics field;
        Rectangle figure;
        Pen pen;
        SolidBrush d;
        int width;
        int height;

        public void initField(Graphics field, int width, int height)
        {
            this.field = field;
            this.width = width;
            this.height = height;
        }

        public void Clear(Int16 colorR, Int16 colorG, Int16 colorB)
        {
            field.Clear(Color.FromArgb(colorR, colorG, colorB));
        }

        public void DrawPixel(Int16 x, Int16 y, Int16 colorR, Int16 colorG, Int16 colorB)
        {
            figure = new Rectangle(x, y, 2, 2);
            d = new SolidBrush(Color.FromArgb(colorR, colorG, colorB));
            field.FillEllipse(d, figure);
        }

        public void DrawLine(Int16 x0, Int16 y0, Int16 x1, Int16 y1, Int16 colorR, Int16 colorG, Int16 colorB)
        {
            pen = new Pen(Color.FromArgb(colorR, colorG, colorB));
            field.DrawLine(pen, x0, y0, x1, y1);
        }

        public void DrawRectangle(Int16 x, Int16 y, Int16 w, Int16 h, Int16 colorR, Int16 colorG, Int16 colorB)
        {
            figure = new Rectangle(x, y, w, h);
            pen = new Pen(Color.FromArgb(colorR, colorG, colorB));
            field.DrawRectangle(pen, figure);
        }

        public void DrawFillRectangle(Int16 x, Int16 y, Int16 w, Int16 h, Int16 colorR, Int16 colorG, Int16 colorB)
        {
            d = new SolidBrush(Color.FromArgb(colorR, colorG, colorB));
            field.FillRectangle(d, x, y, w, h);
        }

        public void DrawEllipse(Int16 x, Int16 y, Int16 w, Int16 h, Int16 colorR, Int16 colorG, Int16 colorB)
        {
            figure = new Rectangle(x, y, w, h);
            pen = new Pen(Color.FromArgb(colorR, colorG, colorB));
            field.DrawEllipse(pen, figure);
        }

        public void DrawFillEllipse(Int16 x, Int16 y, Int16 w, Int16 h, Int16 colorR, Int16 colorG, Int16 colorB)
        {
            figure = new Rectangle(x, y, w, h);
            d = new SolidBrush(Color.FromArgb(colorR, colorG, colorB));
            field.FillEllipse(d, figure);
        }

        public void DrawCircle(Int16 x, Int16 y, Int16 w, Int16 colorR, Int16 colorG, Int16 colorB)
        {
            figure = new Rectangle(x, y, w, w);
            pen = new Pen(Color.FromArgb(colorR, colorG, colorB));
            field.DrawEllipse(pen, figure);
        }

        public void DrawFillCircle(Int16 x, Int16 y, Int16 w, Int16 colorR, Int16 colorG, Int16 colorB)
        {
            figure = new Rectangle(x, y, w, w);
            d = new SolidBrush(Color.FromArgb(colorR, colorG, colorB));
            field.FillEllipse(d, figure);
        }

        public void DrawText(Int16 x, Int16 y, string drawString, string font, Int16 size, Int16 colorR, Int16 colorG, Int16 colorB)
        {
            Font drawFont = new Font(font, size);
            d = new SolidBrush(Color.FromArgb(colorR, colorG, colorB));
            field.DrawString(drawString, drawFont, d, x, y);
        }

        public void DrawChar(Int16 x, Int16 y, string drawString, string font, string pic, Int16 size, Int16 colorR, Int16 colorG, Int16 colorB)
        {
            Font drawFont = new Font(font, size);
            d = new SolidBrush(Color.FromArgb(colorR, colorG, colorB));
            field.DrawString(drawString, drawFont, d, x, y);
        }

        public void DrawTextI(Int16 x, Int16 y, string drawString, string font, string pic, Int16 size, Int16 colorR, Int16 colorG, Int16 colorB)
        {
            Font drawFont = new Font(font, size);
            d = new SolidBrush(Color.FromArgb(colorR, colorG, colorB));
            field.DrawString(drawString, drawFont, d, x, y);
        }

        public void DrawStar(Int16 x, Int16 y, Int16 colorR, Int16 colorG, Int16 colorB)
        {
            pen = new Pen(Color.FromArgb(colorR, colorG, colorB));
            field.DrawPolygon(pen, new PointF[] { new PointF(x, y), new PointF(x - 10, y + 30), new PointF(x - 40, y + 40), new PointF(x - 15, y + 50), new PointF(x - 30, y + 85), new PointF(x, y + 65), new PointF(x + 30, y + 85), new PointF(x + 15, y + 50), new PointF(x + 40, y + 40), new PointF(x + 10, y + 30) });
        }

        public string GetWidth()
        {
            return width.ToString();
        }

        public string GetHeight()
        {
            return height.ToString();
        }
    }
}
