using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinForms_SimpleGraphics
{
    public partial class Form1 : Form
    {
        int x1, y1;

        // Признак первого или второго нажатия на кнопку мыши
        bool flag = false;

        List<Primitive> lst = new List<Primitive>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            // Создание объекта для рисования в окне
            Graphics gr = CreateGraphics();

            if (!flag)
            {
                // Сохранить координаты первой точки
                x1 = e.X;
                y1 = e.Y;

                // Поменять флаг
                flag = true;
            }
            else
            {
                // Создать карандаш
                Pen pen = new Pen(Color.FromArgb(0, 0, 255), 1);

                // задание формы концов линии
                pen.SetLineCap(System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.LineCap.Square, System.Drawing.Drawing2D.DashCap.Flat);

                // gr.DrawLine(Pens.Blue, e.X, e.Y, x1, y1);

                // Рисование линии
                gr.DrawLine(pen, e.X, e.Y, x1, y1);

                // Сохранение линии в списке
                lst.Add(new Primitive(e.X, e.Y, x1, y1));

                // Поменять флаг
                flag = false;
            }

            // Освободить пямять
            gr.Dispose();
        }

        /// <summary>
        /// Метод запускается при перерисовке окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // Получить объект для рисования
            Graphics gr = e.Graphics;

            // Перебрать все линии и нарисовать каждую линию
            foreach (Primitive pr in lst)
            {
                Pen p = new Pen(Color.Blue, 3);                  
                gr.DrawLine(p, pr.x1, pr.y1, pr.x2, pr.y2); 
            }
        }
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
