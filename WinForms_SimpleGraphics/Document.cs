using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms_SimpleGraphics
{
    public partial class Document : Form
    {
        int x1, y1;

        Color currentColor = Color.Black;

        // Признак первого или второго нажатия на кнопку мыши
        bool flag = false;

        Dictionary<Primitive, Color> lst = new Dictionary<Primitive, Color>();
        Dictionary<RectangleF, Color> cirl = new Dictionary<RectangleF, Color>();

        bool line = false, rectangle = false, circle = false;

        bool pencil = false, isPressed = false;

        Point CurrentPoint;
        Point PrevPoint;

        public Document()
        {
            InitializeComponent();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            rectangle = false;
            circle = false;
            pencil = false;
            line = true;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            rectangle = false;
            circle = true;
            line = false;
            pencil = false;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            rectangle = true;
            pencil = false;
            circle = false;
            line = false;
        }

        private void Document_MouseMove(object sender, MouseEventArgs e)
        {
            if (pencil)
            {
                if (isPressed)
                {
                    Graphics gr = CreateGraphics();

                    PrevPoint = CurrentPoint;
                    CurrentPoint = e.Location;

                    Pen p = new Pen(currentColor);
                    gr.DrawLine(p, PrevPoint, CurrentPoint);
                }
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            colorDialog1.FullOpen = true;

            colorDialog1.AllowFullOpen = true;

            DialogResult result = colorDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                currentColor = colorDialog1.Color;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            rectangle = false;
            circle = false;
            line = false;
            pencil = true;
        }

        private void Document_MouseUp(object sender, MouseEventArgs e)
        {
            if (pencil)
                isPressed = false;
        }

        private void Document_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;

            foreach (var item in lst)
            {
                Pen p = new Pen(item.Value, 1);
                gr.DrawLine(p, item.Key.x1, item.Key.y1, item.Key.x2, item.Key.y2);
            }

            foreach (var item in cirl)
            {
                Pen p = new Pen(item.Value, 1);
                gr.DrawEllipse(p, item.Key);
            }
        }

        private void Document_MouseDown(object sender, MouseEventArgs e)
        {
            Graphics gr = CreateGraphics();

            if (!flag)
            {
                if (line || circle || rectangle)
                {
                    // Сохранить координаты первой точки
                    x1 = e.X;
                    y1 = e.Y;

                    // Поменять флаг
                    flag = true;
                }
            }
            else
            {
                Pen pen = new Pen(currentColor, 1);

                pen.SetLineCap(System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.LineCap.Square, System.Drawing.Drawing2D.DashCap.Flat);

                if (line)
                {
                    gr.DrawLine(pen, e.X, e.Y, x1, y1);

                    lst.Add(new Primitive(e.X, e.Y, x1, y1), currentColor);

                    flag = false;
                }
                else if (circle)
                {
                    int width = e.X - x1, height = e.Y - y1;

                    RectangleF rect = new RectangleF(x1, y1, width, height);

                    gr.DrawEllipse(pen, rect);

                    cirl.Add(rect, currentColor);

                    flag = false;
                }
                else if (rectangle)
                {
                    int x3 = e.X, y3 = y1, x4 = x1, y4 = e.Y;

                    gr.DrawLine(pen, x1, y1, x4, y4);
                    lst.Add(new Primitive(x1, y1, x4, y4), currentColor);

                    gr.DrawLine(pen, x4, y4, e.X, e.Y);
                    lst.Add(new Primitive(x4, y4, e.X, e.Y), currentColor);

                    gr.DrawLine(pen, e.X, e.Y, x3, y3);
                    lst.Add(new Primitive(e.X, e.Y, x3, y3), currentColor);

                    gr.DrawLine(pen, x3, y3, x1, y1);
                    lst.Add(new Primitive(x3, y3, x1, y1), currentColor);

                    flag = false;
                }
                else if (pencil)
                {
                    isPressed = true;
                    CurrentPoint = e.Location;
                }
            }
            gr.Dispose();
        }

        class Primitive
        {
            public Primitive(int x1, int y1, int x2, int y2)
            {
                this.x1 = x1;
                this.y1 = y1;
                this.x2 = x2;
                this.y2 = y2;
            }

            public int x1, y1, x2, y2;
        }
    }

}
