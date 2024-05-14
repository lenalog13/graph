using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// для работы с библиотекой OpenGL 
using Tao.OpenGl;
// для работы с библиотекой FreeGLUT 
using Tao.FreeGlut;
// для работы с элементом управления SimpleOpenGLControl 
using Tao.Platform.Windows;
using System.Reflection.Emit;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace lab_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            AnT2.InitializeContexts();
            AnT3.InitializeContexts();
            AnT1.InitializeContexts();
        }

        double ScreenW, ScreenH;

        private double[,] GrapValuesArray;
        private int elements_count = 0;

        private double devX;
        private double devY;

        private bool not_calculate = true;
        private double S, V0, f, angle, mass;
        private double x, minX, maxX, minY, maxY, minV, maxV, minEk, maxEk, minEp, maxEp, Vy;

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedItem = comboBox1.SelectedIndex + 2;
        }

        private double H, g, total_time, F, ti = 0.05, bnc;
        
        private void reBuild(object sender, EventArgs e)
        {
            try
            {
                if (double.TryParse(textBox6.Text, out var number))
                    F = double.Parse(textBox6.Text);
                else F = 0;

                if (double.TryParse(textBox5.Text, out number))
                    H = double.Parse(textBox5.Text);
                else H = 5;

                if (double.TryParse(textBox1.Text, out number))
                    mass = double.Parse(textBox1.Text);
                else mass = 2;

                if (double.TryParse(textBox7.Text, out number))
                    angle = double.Parse(textBox7.Text);
                else angle = 45;

                g = 9.8;

                if (double.TryParse(textBox8.Text, out number))
                    V0 = double.Parse(textBox8.Text);
                else V0 = 5;
                not_calculate = true;
                elements_count = 0;
                pointPosition = 0;
                maxX = 0;
                maxY = 0;
                maxX = 0;
                DrawDiagram();
                Draw();
                maxX = 10;
                if (selectedItem == 2)
                    maxY = 10;//maxV;
                else if (selectedItem == 3)
                    maxY = 10;// maxEp;
                else if (selectedItem == 4)
                    maxY = 10;// maxEk;


            }
            catch (Exception er)
            {
                MessageBox.Show(er.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PointInGrap.Stop();
            reBuild(sender, e );    
            functionCalculation();// Подготовка данных для графика
            PointInGrap.Start();
        }

        private int pointPosition = 0;
        private int selectedItem;


        private void Form1_Load(object sender, EventArgs e)
        {
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE);

            Gl.glClearColor(255, 255, 255, 1);

            Gl.glViewport(0, 0, AnT1.Width, AnT1.Height);

            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();

            if ((double)AnT1.Width <= (double)AnT1.Height)
            {
                ScreenW = 30.0;
                ScreenH = 30.0 * (double)AnT1.Height / (double)AnT1.Width;
                Glu.gluOrtho2D(0.0, ScreenW, 0.0, ScreenH);
            }
            else
            {
                ScreenW = 30.0 * (double)AnT1.Width / (double)AnT1.Height;
                ScreenH = 30.0;
                Glu.gluOrtho2D(0.0, 30.0 * (double)AnT1.Width / (double)AnT1.Height, 0.0, 30.0);
            }

            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            comboBox1.SelectedIndex = 0;

        }

        private double Radian(double angle)
        {
            return (Math.PI / 180.0) * angle;
        }

        private void functionCalculation()
        {
            // определение локальных переменных X и Y 
            double x = 0, y = 0;
            maxV = 0;
            maxEp = 0;
            maxEk = 0;
            maxY = H * Math.Sin(angle * Math.PI / 180);
            maxX = H * Math.Cos(angle * Math.PI / 180);
            minX = maxX;
            minY = maxY;
            // инициализация массива, который будет хранить значение 300 точек,  
            // из которых будет состоять график 
            GrapValuesArray = new double[99999, 6];

            // счетчик элементов массива 
            elements_count = 0;
            /* double diskr = Math.Sqrt( V0*V0+ 2*g*( Math.Sin(angle * Math.PI / 180)-F* Math.Cos(angle * Math.PI / 180))*H );
             double t1 = (-V0+diskr)/(g * (Math.Sin(angle * Math.PI / 180) - F * Math.Cos(angle * Math.PI / 180)));
             double t2 = (-V0 - diskr) / (g * (Math.Sin(angle * Math.PI / 180) - F * Math.Cos(angle * Math.PI / 180)));
             total_time = (t1 > 0 && t2 > 0) ? Math.Min(t1, t2) : Math.Max(t1, t2); */
            double diskr = Math.Sqrt(V0 * Math.Sin(angle * Math.PI / 180) * V0 * Math.Sin(angle * Math.PI / 180) + 2 * g * H);
            double t1 = (V0 * Math.Sin(angle * Math.PI / 180) + diskr) / (g);
            double t2 = (V0 * Math.Sin(angle * Math.PI / 180) - diskr) / (g);
            total_time = (t1 > 0 && t2 > 0) ? Math.Min(t1, t2) : Math.Max(t1, t2);
            // вычисления всех значений y для x, принадлежащего промежутку от -15 до 15 с шагом в 0.01f 
            y = H + V0 * Math.Sin(angle * Math.PI / 180 * t1 - g * t1 * t1 / 2);
            x = V0 * Math.Cos(angle * Math.PI / 180 * t1);
            Vy = g * (Math.Sin(angle * Math.PI / 180) - F * Math.Cos(angle * Math.PI / 180)) + V0;
            // подсчет элементов 

            if (V0 > 0)
            {
                GrapValuesArray[elements_count, 3] = mass * g * y;
                GrapValuesArray[elements_count, 2] = V0 * V0 * Math.Cos(angle / 180 * Math.PI) * Math.Cos(angle / 180 * Math.PI) + Vy * Vy;

                GrapValuesArray[elements_count, 4] = mass * Vy * Vy / 2;
                GrapValuesArray[elements_count, 1] = y;
                GrapValuesArray[elements_count, 0] = x;
                if (GrapValuesArray[elements_count, 0] > maxX) maxX = GrapValuesArray[elements_count, 0];
                if (GrapValuesArray[elements_count, 1] > maxY) maxY = GrapValuesArray[elements_count, 1];
                if (GrapValuesArray[elements_count, 2] > maxV) maxV = GrapValuesArray[elements_count, 2];
                if (GrapValuesArray[elements_count, 3] > maxEp) maxEp = GrapValuesArray[elements_count, 3];
                if (GrapValuesArray[elements_count, 4] > maxEk) maxEk = GrapValuesArray[elements_count, 4];
                // подсчет элементов 
                elements_count++;
            }

            for (double t = 0; t <= total_time; t += ti)
            {

                Vy = V0 * Math.Sin(angle * Math.PI / 180) - g * t;
                y = H + V0 * Math.Sin(angle * Math.PI / 180) * t - g * t * t / 2;
                x = V0 * Math.Cos(angle * Math.PI / 180) * t;

                // запись координаты x 
                GrapValuesArray[elements_count, 0] = x;
                // запись координаты y
                if (t >= total_time) y = 0;

                GrapValuesArray[elements_count, 1] = y;
                // подсчет элементов 
                GrapValuesArray[elements_count, 2] = V0 * V0 * Math.Cos(angle / 180 * Math.PI) * Math.Cos(angle / 180 * Math.PI) + Vy * Vy;

                GrapValuesArray[elements_count, 3] = mass * g * y;
                GrapValuesArray[elements_count, 4] = mass * (Vy * Vy) / 2;

                if (GrapValuesArray[elements_count, 0] > maxX) maxX = GrapValuesArray[elements_count, 0];
                if (GrapValuesArray[elements_count, 0] < minX)
                {
                    minX = GrapValuesArray[elements_count, 0];
                    minY = GrapValuesArray[elements_count, 1];
                }
                if (GrapValuesArray[elements_count, 1] > maxY) maxY = GrapValuesArray[elements_count, 1];
                if (GrapValuesArray[elements_count, 2] > maxV) maxV = GrapValuesArray[elements_count, 2];
                if (GrapValuesArray[elements_count, 3] > maxEp) maxEp = GrapValuesArray[elements_count, 3];
                if (GrapValuesArray[elements_count, 4] > maxEk) maxEk = GrapValuesArray[elements_count, 4];


                elements_count++;
            }
            // изменяем флаг, сигнализировавший о том, что координаты графика не вычислены 
            not_calculate = false;
            /*
            x = 1;
            maxV = 0;
            minV = 0;
            maxY = 0;
            minX = 0;
            maxX = S * Math.Cos(Radian(angle));
            maxEp = 0;
            minEp = 0;
            maxEk = 0;
            minEk = 0;

            GrapValuesArray = new double[99999, 6];
            elements_count = 0;

            double C = 9.8 * (Math.Sin(Radian(angle)) - f * Math.Sin(Radian(angle)));

            GrapValuesArray[elements_count, 0] = 0;
            GrapValuesArray[elements_count, 1] = S * Math.Sin(Radian(angle));
            GrapValuesArray[elements_count, 2] = V0;
            GrapValuesArray[elements_count, 3] = mass * 9.8 * S;
            GrapValuesArray[elements_count, 4] = mass * V0 * V0 / 2;
            if (GrapValuesArray[elements_count, 0] > maxX) maxX = GrapValuesArray[elements_count, 0];
            if (GrapValuesArray[elements_count, 0] < minX) minX = GrapValuesArray[elements_count, 0];
            if (GrapValuesArray[elements_count, 1] > maxY) maxY = GrapValuesArray[elements_count, 1];
            if (GrapValuesArray[elements_count, 2] > maxV) maxV = GrapValuesArray[elements_count, 2];
            if (GrapValuesArray[elements_count, 2] < minV) minV = GrapValuesArray[elements_count, 2];
            if (GrapValuesArray[elements_count, 3] > maxEp) maxEp = GrapValuesArray[elements_count, 3];
            if (GrapValuesArray[elements_count, 3] < minEp) minEp = GrapValuesArray[elements_count, 3];
            if (GrapValuesArray[elements_count, 4] > maxEk) maxEk = GrapValuesArray[elements_count, 4];
            if (GrapValuesArray[elements_count, 4] < minEk) minEk = GrapValuesArray[elements_count, 4];
            elements_count++;

            for (double t = 0.005; GrapValuesArray[elements_count - 1, 1] > 0; t += 0.005)
            {
                GrapValuesArray[elements_count, 2] = V0 + C * t;
                x = GrapValuesArray[elements_count, 2] * t;
                if (x >= S)
                    break;

                GrapValuesArray[elements_count, 0] = x * Math.Cos(Radian(angle));
                GrapValuesArray[elements_count, 1] = (S - x) * Math.Sin(Radian(angle));
                GrapValuesArray[elements_count, 3] = mass * 9.8 * (S - x);
                GrapValuesArray[elements_count, 4] = mass * GrapValuesArray[elements_count, 2] * GrapValuesArray[elements_count, 2] / 2;

                if (GrapValuesArray[elements_count, 0] > maxX) maxX = GrapValuesArray[elements_count, 0];
                if (GrapValuesArray[elements_count, 0] < minX) minX = GrapValuesArray[elements_count, 0];
                if (GrapValuesArray[elements_count, 1] > maxY) maxY = GrapValuesArray[elements_count, 1];
                if (GrapValuesArray[elements_count, 2] > maxV) maxV = GrapValuesArray[elements_count, 2];
                if (GrapValuesArray[elements_count, 2] < minV) minV = GrapValuesArray[elements_count, 2];
                if (GrapValuesArray[elements_count, 3] > maxEp) maxEp = GrapValuesArray[elements_count, 3];
                if (GrapValuesArray[elements_count, 3] < minEp) minEp = GrapValuesArray[elements_count, 3];
                if (GrapValuesArray[elements_count, 4] > maxEk) maxEk = GrapValuesArray[elements_count, 4];
                if (GrapValuesArray[elements_count, 4] < minEk) minEk = GrapValuesArray[elements_count, 4];
                elements_count++;
            }
            minY = 0;
            not_calculate = false;
            */
        }


        private void PointInGrap_Tick(object sender, EventArgs e)
        {
            pointPosition++;
            // функция визуализации
            AnT1.MakeCurrent();
            Draw();


            AnT2.MakeCurrent();
            DrawEnergy();

            AnT3.MakeCurrent();
            DrawGraph();

            label8.Text = $"Время (с): {pointPosition * ti }";

        }

        private bool traektor = true;

        private void DrawDiagram()
        {
            functionCalculation();
            Gl.glViewport(0, 0, AnT1.Width, AnT1.Height);
            // активация проекционной матрицы 
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            // очистка матрицы 
            Gl.glLoadIdentity();
            ScreenW = (maxX + 0.5 - minX) * (double)AnT1.Width / (double)AnT1.Height;
            ScreenH = (1.5 * maxY + 0.5) * (double)AnT1.Height / (double)AnT1.Width;
            Glu.gluOrtho2D(0.0, ScreenW, 0.0, ScreenH);
            // сохранение коэффициентов, которые нам необходимы для перевода координат указателя в оконной системе в координаты, 
            // принятые в нашей OpenGL сцене 
            devX = (double)ScreenW / (double)AnT1.Width;
            devY = (double)ScreenH / (double)AnT1.Height;

            // установка объектно-видовой матрицы 
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            if (V0 < 0) bnc = -minX; else bnc = 0;
            Gl.glPointSize(5);
            Gl.glBegin(Gl.GL_LINE_STRIP);
            Gl.glVertex2d(0, 0);
            Gl.glVertex2d(maxX * 5, 0);
            Gl.glEnd();

            // стартуем отрисовку в режиме визуализации точек 
            // объединяемых в линии (GL_LINE_STRIP) 
            Gl.glBegin(Gl.GL_POINTS);

            Gl.glColor3ub(0, 0, 0);
            // рисуем начальную точку 
            //if (GrapValuesArray[0, 1] >= 0 && GrapValuesArray[0, 1] <= yMax)
            //    Gl.glVertex2d(GrapValuesArray[0, 0], GrapValuesArray[0, 1]);

            if (traektor)
            {
                // проходим по массиву с координатами вычисленных точек 
                for (int ax = 1; ax < pointPosition; ax += 2)
                {
                    // передаем в OpenGL информацию о вершине, участвующей в построении линий
                    if (GrapValuesArray[ax, 1] >= 0 && GrapValuesArray[ax, 1] <= maxY)
                        Gl.glVertex2d(GrapValuesArray[ax, 0] + bnc, GrapValuesArray[ax, 1]);
                }
            }

            // завершаем режим рисования 
            Gl.glEnd();
            Gl.glColor3d(0, 0, 0);
            // устанавливаем размер точек, равный 5 пикселям
            Gl.glPointSize(15);
            // устанавливаем текущим цветом - красный цвет 
            Gl.glColor3ub(255, 0, 0);
            // активируем режим вывода точек (GL_POINTS) 


            //if (V0 >= 0) { PrintText2D((float)(bnc + (H) * Math.Cos(angle * Math.PI / 180)), 0.15f, "X"); PrintText2D((float)(bnc), (float)((H) * Math.Sin(angle * Math.PI / 180)), "Y"); Gl.glBegin(Gl.GL_LINES); Gl.glVertex2d(bnc, (H) * Math.Sin(angle * Math.PI / 180)); Gl.glVertex2d(bnc + (H) * Math.Cos(angle * Math.PI / 180), 0); Gl.glVertex2d(bnc + (H) * Math.Cos(angle * Math.PI / 180), 0.1); Gl.glVertex2d(bnc + (H) * Math.Cos(angle * Math.PI / 180), -0.1); Gl.glEnd(); }
            //if (V0 < 0) { PrintText2D((float)(bnc + (H) * Math.Cos(angle * Math.PI / 180)), 0.15f, "X"); PrintText2D((float)(bnc + xMin), (float)yMax, "Y"); Gl.glBegin(Gl.GL_LINES); Gl.glVertex2d(bnc + xMin, yMax); Gl.glVertex2d(bnc + (H) * Math.Cos(angle * Math.PI / 180), 0); Gl.glEnd(); }
            
            
            //if (true) 
            //{
            //    Gl.glBegin(Gl.GL_LINES); Gl.glVertex2d(bnc + xMin, yMax); Gl.glVertex2d(bnc + xMin+xMax, yMax+yMax); Gl.glEnd();
            //}
            //if (Pow)
            //{
            //    PrintText2D((float)(((GrapValuesArray[pointPosition, 0]+bnc))), (float)(GrapValuesArray[pointPosition, 1]+0.2), "N"); PrintText2D((float)(GrapValuesArray[pointPosition, 0] +bnc), 0.05f, "mg");
            //    Gl.glBegin(Gl.GL_LINES); Gl.glVertex2d(GrapValuesArray[pointPosition, 0] + bnc, GrapValuesArray[pointPosition, 1]); Gl.glVertex2d(GrapValuesArray[pointPosition, 0] + bnc + xMax, GrapValuesArray[pointPosition, 1] + yMax); Gl.glEnd();
            //    Gl.glBegin(Gl.GL_LINES); Gl.glVertex2d(GrapValuesArray[pointPosition, 0] + bnc, GrapValuesArray[pointPosition, 1]); Gl.glVertex2d(GrapValuesArray[pointPosition, 0] + bnc , GrapValuesArray[pointPosition, 1] - yMax); Gl.glEnd();
            //}
            Gl.glBegin(Gl.GL_POINTS);
            // выводим красную точку, используя ту ячейку массива, до которой мы дошли (вычисляется в функции обработчике событий таймера) 
            if (pointPosition >= elements_count - 1)
                pointPosition = 0;

            Gl.glVertex2d(GrapValuesArray[pointPosition, 0] + bnc, GrapValuesArray[pointPosition, 1]);
            // завершаем режим рисования 
            Gl.glEnd();
            // устанавливаем размер точек равный единице

        }


        private Color[] colors = new Color[6];

        // вызываем отрисовку задачи
        private void Draw()
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);

            Gl.glLoadIdentity();

            Gl.glPushMatrix();

            Gl.glTranslated(0, 0, 0);

            // вызываем функцию рисования графика 
            DrawDiagram();

            Gl.glPopMatrix();

            Gl.glFlush();

            AnT1.Invalidate();

        }

        private void DrawEnergy()
        {
            // очистка буфера цвета и буфера глубины 
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            Gl.glClearColor(255, 255, 255, 0); //выставление цвета основного

            Gl.glViewport(0, 0, AnT2.Width, AnT2.Height);
            // активация проекционной матрицы 
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            // очистка матрицы 
            Gl.glLoadIdentity();
            ScreenW = (double)AnT2.Width / (double)AnT2.Height;
            ScreenH = (double)AnT2.Height / (double)AnT2.Width;
            Glu.gluOrtho2D(0.0, ScreenW, 0.0, ScreenH);

            Gl.glMatrixMode(Gl.GL_MODELVIEW);


            // помещаем состояние матрицы в стек матриц
            Gl.glPushMatrix();


            double Ep = GrapValuesArray[pointPosition, 3];
            double Ek = GrapValuesArray[pointPosition, 4];

            Gl.glColor3d(255 / 255, 165 / 255, 0);
            Gl.glRectd(0.05, 0, 0.15, Ep / maxEp);
            Gl.glColor3d(0, 127 / 255, 255 / 255);
            Gl.glRectd(0.25, 0, 0.35, Ek / maxEk);
            Gl.glColor3d(0, 0, 0);


            // возвращаем матрицу из стека 
            Gl.glPopMatrix();


            // дожидаемся завершения визуализации кадра 
            Gl.glFlush();
            label10.Text = $"{(int)Ep}";
            label11.Text = $"{(int)Ek}";

            AnT2.Invalidate();
            
        }

        // рисуем график
        private void DrawDiagram2()
        {
            Gl.glViewport(0, 0, AnT3.Width, AnT3.Height);

            // активация проекционной матрицы 
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            // очистка матрицы 
            Gl.glLoadIdentity();

            if (selectedItem == 2)
            {
                ScreenW = (elements_count + 4) * (double)AnT3.Width / (double)AnT3.Height;
                ScreenH = (maxV * 2) * (double)AnT3.Height / (double)AnT3.Width;
            }
            else if (selectedItem == 3)
            {
                ScreenW = (elements_count + 4) * (double)AnT3.Width / (double)AnT3.Height;
                ScreenH = (maxEp * 2) * (double)AnT3.Height / (double)AnT3.Width;
            }
            else
            {
                ScreenW = (elements_count + 4) * (double)AnT3.Width / (double)AnT3.Height;
                ScreenH = (maxEk * 2) * (double)AnT3.Height / (double)AnT3.Width;
            }

            Glu.gluOrtho2D(0.0, ScreenW, 0.0, ScreenH);
            // сохранение коэффициентов, которые нам необходимы для перевода координат указателя в оконной системе в координаты, 
            // принятые в нашей OpenGL сцене 
            //if (ScreenW < graph.Width)
            //    devX = (double)ScreenW / (double)graph.Width;
            //else
            devX = (double)AnT3.Width / (double)ScreenW;
            //if (ScreenH > graph.Height)
            devY = (double)AnT3.Height / (double)ScreenH;
            //else
            devY = (double)ScreenH / (double)AnT3.Height;
            //devY = Double.Parse(textBox3.Text);
            //devX = Double.Parse(textBox9.Text);
            // установка объектно-видовой матрицы 
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glTranslated(1, 1, 0);
            Gl.glPointSize(5);
            Gl.glBegin(Gl.GL_LINE_STRIP);
            Gl.glVertex2d(0, 0);
            Gl.glVertex2d(elements_count, 0);
            Gl.glVertex2d(elements_count, 0);
            Gl.glVertex2d((elements_count - 1), 5);
            Gl.glEnd();
            Gl.glBegin(Gl.GL_LINE_STRIP);
            Gl.glVertex2d(0, 0);
            if (selectedItem == 2)
            {
                Gl.glVertex2d(0, maxV);
                Gl.glVertex2d(0, maxV);
                Gl.glVertex2d(1, (maxV - 0.02 * maxV));
            }
            else if (selectedItem == 3)
            {
                Gl.glVertex2d(0, maxEp);
                Gl.glVertex2d(0, maxEp);
                Gl.glVertex2d(1, (maxEp - 0.02 * maxEp));
            }
            else
            {
                Gl.glVertex2d(0, maxEk);
                Gl.glVertex2d(0, maxEk);
                Gl.glVertex2d(1, (maxEk - 0.02 * maxEk));
            }
            Gl.glEnd();

            // стартуем отрисовку в режиме визуализации точек 
            // объединяемых в линии (GL_LINE_STRIP) 
            Gl.glBegin(Gl.GL_LINE_STRIP);

            Gl.glColor3ub(0, 0, 0);
            // рисуем начальную точку 
            Gl.glVertex2d(0, GrapValuesArray[0, selectedItem]);

            // проходим по массиву с координатами вычисленных точек 
            for (int ax = 1; ax < elements_count; ax += 2)
            {
                // передаем в OpenGL информацию о вершине, участвующей в построении линий
                Gl.glVertex2d(ax, GrapValuesArray[ax, selectedItem]);
            }

            // завершаем режим рисования 
            Gl.glEnd();
            // устанавливаем размер точек, равный 5 пикселям
            Gl.glPointSize(15);
            // устанавливаем текущим цветом - красный цвет 
            //Gl.glColor3ub(colors[3].R, colors[3].G, colors[3].B);
            // активируем режим вывода точек (GL_POINTS) 
            Gl.glBegin(Gl.GL_POINTS);
            // выводим красную точку, используя ту ячейку массива, до которой мы дошли (вычисляется в функции обработчике событий таймера) 

            Gl.glVertex2d(pointPosition, GrapValuesArray[pointPosition, selectedItem]);
            // завершаем режим рисования 
            Gl.glEnd();
            // устанавливаем размер точек равный единице
        }

        // вызываем отрисовку графика
        private void DrawGraph()
        {
            // очистка буфера цвета и буфера глубины 
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            Gl.glClearColor(255, 255, 255, 0);

            // очищение текущей матрицы 
            Gl.glLoadIdentity();

            // помещаем состояние матрицы в стек матриц 
            Gl.glPushMatrix();

            // вызываем функцию рисования графика 
            DrawDiagram2();

            // возвращаем матрицу из стека 
            Gl.glPopMatrix();


            // дожидаемся завершения визуализации кадра 
            Gl.glFlush();

            // сигнал для обновление элемента реализующего визуализацию. 
            AnT3.Invalidate();

        }
    }
   
}
