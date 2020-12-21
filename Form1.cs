using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    /* Размер рисовалки w = 900px, h = 700 px*/
    public partial class Form1 : Form
    {
        double _aX = 0, _aY = 0, _aL = 0, _bX = 0, _bY = 0, _bL = 0, _cX = 0, _cY = 0, _cL = 0; int _net = 50; // ингридиенты 
        /* простой яблочный пирог
          ингридиенты:
         * сахар-  1 стакан
         * яйцо - 3 штуки
         * пшеничная мука - 1 стакан 
         * яблоко -  2 штуки
         * разрыхлитель - 0,5 чайной ложки
       1) Три яйца разбить в миску, добавить сахар и взбить миксером до белой пены
       2) Затем добавить муку и разрыхлитель, и все перемешать.
       3) Яблоки порезать на ломтики, убрать сердцевину.
       4) Форму для выпечки смазать маслом, посыпать сухарями и уложить яблоки на дно. Залить тестом и выпекать около 40 минут при средней температуре. Готовность пирога можно проверять зубочисткой или спичкой — пирог готов, когда на зубочистку не налипает тесто при погружении ее вглубь пирога.
        а вообще, очень интересно, читает ли кто нибудь мой код, или сразу запускают ??

        */
        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            try
            {
                _net = Convert.ToInt32(textBox10.Text); // задаём размер сетки по умолчанию = 50 px
            }
            catch { }
        }

        public Form1()
        {

            InitializeComponent();
            comboBox1.Items.AddRange(new string[] { "Сложение", "Вычитание" }); comboBox1.SelectedIndex = 0; // надо потом убрать, иначе руки оторвут
        }

        public void ax()
        {
            Graphics g = pictureBox1.CreateGraphics();
            g.DrawLine(new Pen(Brushes.Black, 2), new Point(0, 300), new Point(900, 300)); // ось Х
            g.DrawLine(new Pen(Brushes.Black, 2), new Point(350, 0), new Point(350, 700)); //ось у
                                                                                           //координаты подгона

            //   g.DrawLine(new Pen(Brushes.Black, 1), new Point(0, 350), new Point(900, 350));
            //   g.DrawLine(new Pen(Brushes.Black, 1), new Point(450, 0), new Point(450, 700));
            // идеальные координаты, но вижла взяла 110% маштаба и они не подходят, если выставить нормально то можно юзать оба (на выбор)
        }

        public void net() // отрисовка сетки
        {
            for (int i = 350; i < 900; i = i + _net) // отрисока сетки оу по правую сторону
            {
                Graphics g = pictureBox1.CreateGraphics();
                g.DrawLine(new Pen(Brushes.Gray, 1), new Point(i, 0), new Point(i, 700));
            }

            for (int i = 350; i > 0; i = i - _net) // отрисока сетки оу по левую сторону
            {
                Graphics g = pictureBox1.CreateGraphics();
                g.DrawLine(new Pen(Brushes.Gray, 1), new Point(i, 0), new Point(i, 700));
            }


            for (int i = 300; i < 700; i = i + _net) // отрисока сетки ох по правую сторону
            {
                Graphics g = pictureBox1.CreateGraphics();
                g.DrawLine(new Pen(Brushes.Gray, 1), new Point(0, i), new Point(900, i));
            }

            for (int i = 300; i > 0; i = i - _net) // отрисока сетки ох по левую сторону
            {
                Graphics g = pictureBox1.CreateGraphics();
                g.DrawLine(new Pen(Brushes.Gray, 1), new Point(0, i), new Point(900, i));
            }

        }

        public void input()
        {
            try
            {
                _aX = Convert.ToDouble(textBox7.Text) * _net;
                _aY = Convert.ToDouble(textBox2.Text) * _net;

                _bX = Convert.ToDouble(textBox4.Text) * _net;
                _bY = Convert.ToDouble(textBox5.Text) * _net;
                
                
            }
            catch
            {
                // если я скажу что случится - этого не случится 
            }

        }

        public void count()
        {
            input();
            if (comboBox1.SelectedIndex == 0)
            {
                _cX = _aX + _bX;
                _cY = _aY + _bY;
            }
            if (comboBox1.SelectedIndex == 1)       // этот код бесполезен как я, он никак не используется, можно удалить
            {
                _cX = _aX - _bX;                
                _cY = _aY - _bY;
            }


        }



        public void convert() // конвертация координат и отрисовка
        {
            input();
            count();

            _aL = Math.Sqrt((_aX) * (_aX) + (_aY) * (_aY));
            _bL = Math.Sqrt((_bX) * (_bX) + (_bY) * (_bY));
          //  _cL = Math.Sqrt((_cX) * (_cX) + (_cY) * (_cY));
            // начало координат 350(x)/300(y)
            _aX = (_aX + 350); // не лезь, оно сожрёт
            _aY = (300 - _aY);

            _bX = (_bX + 350); // не лезь, оно сожрёт
            _bY = (300 - _bY);

            _cX = (_cX + 350); // не лезь, оно сожрёт
            _cY = (300 - _cY);
             // получили новые координаты, радуемся, можно рисовать, рисуем ниже

            Graphics g = pictureBox1.CreateGraphics();
            g.DrawLine(new Pen(Brushes.Blue, 1), new Point(350, 300), new Point(Convert.ToInt32(_aX), Convert.ToInt32(_aY)));

            label9.Location = new Point(Convert.ToInt32(_aX) + 10 , Convert.ToInt32(_aY) + 10); // те самые обозначения над векторами
            label9.Visible = true;

            g.DrawLine(new Pen(Brushes.Blue, 1), new Point(350, 300), new Point(Convert.ToInt32(_bX), Convert.ToInt32(_bY)));

            label10.Location = new Point(Convert.ToInt32(_bX) + 10, Convert.ToInt32(_bY) + 10); // те самые обозначения над векторами
            label10.Visible = true;

            if (comboBox1.SelectedIndex == 0)
            {
                g.DrawLine(new Pen(Brushes.Red, 1), new Point(350, 300), new Point(Convert.ToInt32(_cX), Convert.ToInt32(_cY)));
                label11.Location = new Point(Convert.ToInt32(_cX) + 10, Convert.ToInt32(_cY) + 10); // те самые обозначения над векторами
                label11.Visible = true;

            }
            if (comboBox1.SelectedIndex == 1)
            {
                g.DrawLine(new Pen(Brushes.Red, 1), new Point(Convert.ToInt32(_bX), Convert.ToInt32(_bY)), new Point(Convert.ToInt32(_aX), Convert.ToInt32(_aY)));
                label11.Location = new Point(Convert.ToInt32(_bX) - 10, Convert.ToInt32(_bY) - 20); // те самые обозначения над векторами
                label11.Visible = true;
            }


        }

        private void button1_Click(object sender, EventArgs e) // обработка нажатия кнопки
        {
            Graphics g = pictureBox1.CreateGraphics(); // отчистка поля при нажатии и/или смене сложения и вычитания
            g.Clear(SystemColors.Control);
            net();
            ax();
            convert();
            output();
        }

        private void output()
        {
            input();
            textBox3.Text = Convert.ToString(_aL/_net);
            textBox6.Text = Convert.ToString(_bL/_net);
            if (comboBox1.SelectedIndex == 0){ // тут 2 варианта, для сложения и вычитания координаты приходится считать по разному, тут сложение

                _cX = (_aX / _net) + (_bX / _net);
                _cY = (_aY / _net) + (_bY / _net);

                textBox1.Text = Convert.ToString(_cX);
                textBox8.Text = Convert.ToString(_cY);
                _cL = Math.Sqrt((_cX) * (_cX) + (_cY) * (_cY));
                textBox9.Text = Convert.ToString(_cL);
                textBox11.Visible = false;
                textBox12.Visible = false;

            }

            if (comboBox1.SelectedIndex == 1) // тут вычитание
            {
                textBox11.Visible = true;
                textBox12.Visible = true;
                textBox11.Text = Convert.ToString(_bY / _net);
                textBox12.Text = Convert.ToString(_bX / _net);
                textBox1.Text = Convert.ToString(_aX / _net);
                textBox8.Text = Convert.ToString(_aY / _net);
                _cL = Math.Sqrt(((_bY / _net) -(_aY / _net)) * ((_bY / _net) - (_aY / _net)) + ((_bX / _net) - (_aX / _net)) * ((_bX / _net) - (_aX / _net)));
                textBox9.Text = Convert.ToString(_cL); // из формулы что длинна вектора это корень из ((х2-х1)^2 + (y2-y1)^2)
            }




        }
    }
}
