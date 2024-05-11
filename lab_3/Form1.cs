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

        private bool not_calculate = true;
        private double S, V0, f, angle, mass;
        private double x, minX, maxX, minY, maxY, minV, maxV, minEk, maxEk, minEp, maxEp;

        private void reBuild(object sender, EventArgs e)
        {
            try
            {
                S = double.Parse(textBox5.Text);
                f = double.Parse(textBox6.Text);
                angle = float.Parse(textBox7.Text);
                label6.Text = $"Угол (°): {angle}";

                V0 = double.Parse(textBox8.Text);
                not_calculate = true;
                elements_count = 0;
                pointPosition = 0;
                mass = double.Parse(textBox1.Text);
                functionCalculation();
                DrawDiagram();
                Draw();
                PointInGrap.Start();
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
        }


        private void PointInGrap_Tick(object sender, EventArgs e)
        {
            pointPosition++;
            if (not_calculate) functionCalculation();

            AnT1.MakeCurrent();
            Draw();
            AnT1.Invalidate(); // или AnT1.Refresh();

            AnT2.MakeCurrent();
            DrawEnergy();
            AnT2.Invalidate(); // или AnT2.Refresh();

            AnT3.MakeCurrent();
            DrawGraph();
            AnT3.Invalidate(); // или AnT3.Refresh();

            label8.Text = $"Время (с): {pointPosition * 0.063}";

        }

        private void DrawDiagram()
        {
            Gl.glViewport(0, 0, AnT1.Width, AnT1.Height);

            if (maxY / (maxX - minX) > 1)
            {
                ScreenH = maxY * 1.2;
                ScreenW = (maxY * AnT1.Width / AnT1.Height) * 1.2;
            }
            else
            {
                ScreenW = (maxX - minX) * 1.2;
                ScreenH = (maxX - minX) * 1.2;
            }



            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Glu.gluOrtho2D(0.0, ScreenW, 0.0, ScreenH);

            // траектория
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glTranslated(ScreenW * 0.1 + Math.Abs(minX), ScreenH * 0.1, 0);
            Gl.glPointSize(2);
            Gl.glBegin(Gl.GL_LINE_STRIP);
            Gl.glVertex2d(minX, 0);
            Gl.glVertex2d(maxX, 0);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_LINE_STRIP);
            for (int i = 0; i < elements_count; i++)
            {
                Gl.glVertex2d(GrapValuesArray[i, 0], GrapValuesArray[i, 1]);
            }
            Gl.glEnd();

            Gl.glBegin(Gl.GL_POINTS);

            Gl.glColor3ub(colors[0].R, colors[0].G, colors[0].B);
            if (GrapValuesArray[0, 1] >= 0 && GrapValuesArray[0, 1] <= maxY)
                Gl.glVertex2d(GrapValuesArray[0, 0], GrapValuesArray[0, 1]);

            Gl.glEnd();
            Gl.glPointSize(15);
            Gl.glColor3ub(colors[3].R, colors[3].G, colors[3].B);
            Gl.glBegin(Gl.GL_POINTS);
            if (pointPosition == elements_count - 1)
            {
                Gl.glVertex2d(GrapValuesArray[0, 0], GrapValuesArray[0, 1]);
            }
            else if (pointPosition == elements_count)
            {
                pointPosition = 0;
                Gl.glVertex2d(GrapValuesArray[pointPosition, 0], GrapValuesArray[pointPosition, 1]);
            }
            else
                Gl.glVertex2d(GrapValuesArray[pointPosition, 0], GrapValuesArray[pointPosition, 1]);
            Gl.glEnd();
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
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            Gl.glClearColor(255, 255, 255, 0);

            Gl.glViewport(0, 0, AnT2.Width, AnT2.Height);
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            ScreenW = AnT2.Width;
            ScreenH = AnT2.Height;
            Glu.gluOrtho2D(0.0, ScreenW, 0.0, ScreenH);

            Gl.glMatrixMode(Gl.GL_MODELVIEW);

            Gl.glPushMatrix();


            double Ep = GrapValuesArray[pointPosition, 3];
            double Ek = GrapValuesArray[pointPosition, 4];
            Gl.glRectd(0, 0, 0.5 * ScreenW, Ep / maxEp * ScreenH);
            Gl.glColor3d(0, 0.5f, 1.0f);
            Gl.glRectd(0.5 * ScreenW, 0, ScreenW, Ek / maxEk * ScreenH);
            Gl.glColor3d(0, 1.0f, 0.5f);
            label12.Text = $"{(int)Ep}";
            label13.Text = $"{(int)Ek}";

            Gl.glPopMatrix();

            Gl.glFlush();

            AnT2.Invalidate();
        }

        // рисуем график
        private void DrawDiagram2()
        {
            Gl.glViewport(0, 0, AnT3.Width, AnT3.Height);

            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();

            ScreenW = elements_count * 1.1;
            double temp;
            if (selectedItem == 2)
            {
                ScreenH = (maxV - minV) * 1.1;
                temp = minV;
            }
            else if (selectedItem == 3)
            {
                ScreenH = (maxEp - minEp) * 1.1;
                temp = minEp;
            }
            else
            {
                ScreenH = (maxEk - minEk) * 1.1;
                temp = minEk;
            }

            Glu.gluOrtho2D(0.0, ScreenW, 0.0, ScreenH);

            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glTranslated(ScreenW * 0.05, ScreenH * 0.05 + Math.Abs(temp), 0);
            Gl.glPointSize(5);

            Gl.glBegin(Gl.GL_LINE_STRIP);
            Gl.glVertex2d(ScreenW * (-0.04), ScreenH);
            Gl.glVertex2d(0, ScreenH * 1.04);
            Gl.glVertex2d(ScreenW * 0.04, ScreenH);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_LINE_STRIP);
            Gl.glVertex2d(0, ScreenH * 1.04);
            Gl.glVertex2d(0, temp);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_LINE_STRIP);
            Gl.glVertex2d(0, 0);
            Gl.glVertex2d(ScreenW * 1.04, 0);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_LINE_STRIP);
            Gl.glVertex2d(ScreenW, ScreenH * (-0.04));
            Gl.glVertex2d(ScreenW * 1.04, 0);
            Gl.glVertex2d(ScreenW, ScreenH * 0.04);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_LINE_STRIP);
            Gl.glColor3ub(colors[0].R, colors[0].G, colors[0].B);
            // рисуем начальную точку 
            Gl.glVertex2d(0, GrapValuesArray[0, selectedItem]);

            // проходим по массиву с координатами вычисленных точек 
            for (int ax = 1; ax < elements_count; ax += 1)
                Gl.glVertex2d(ax, GrapValuesArray[ax, selectedItem]);
            Gl.glEnd();

            Gl.glPointSize(15);
            Gl.glColor3ub(colors[3].R, colors[3].G, colors[3].B);

            Gl.glBegin(Gl.GL_POINTS);
            Gl.glVertex2d(pointPosition, GrapValuesArray[pointPosition, selectedItem]);
            Gl.glEnd();
        }

        // вызываем отрисовку графика
        private void DrawGraph()
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            Gl.glClearColor(255, 255, 255, 0);

            Gl.glLoadIdentity();

            Gl.glPushMatrix();

            // вызываем функцию рисования графика 
            DrawDiagram2();

            Gl.glPopMatrix();

            Gl.glFlush();

            AnT3.Invalidate();

        }
    }


// функция визуализации текста 
/*


        // функция, производящая вычисления координат графика 
        // и заносящая их в массив GrapValuesArray 
        private void functionCalculation()
        {

            // определение локальных переменных X и Y 
            float x = 0, y = 0;

            // инициализация массива, который будет хранить значение 300 точек, 
            // из которых будет состоять график 

            GrapValuesArray = new float[300, 2];

            // счетчик элементов массива 
            elements_count = 0;

            // вычисления всех значений y для x, принадлежащего промежутку от -15 до 15 с шагом в 0.01f 
            for (x = -15; x < 15; x += 0.1f)
            {
                // вычисление y для текущего x 
                // по формуле y = (float)Math.Sin(x)*3 + 1; 
                // эта строка задает формулу, описывающую график функции для нашего уравнения y = f(x). 
                y = (float)Math.Sin(x) * 3 + 1;

                // запись координаты x 
                GrapValuesArray[elements_count, 0] = x;
                // запись координаты y 
                GrapValuesArray[elements_count, 1] = y;
                // подсчет элементов 
                elements_count++;

            }

            // изменяем флаг, сигнализировавший о том, что координаты графика не вычислены 
            not_calculate = false;

        }

    */

    /*

        private void DrawDiagram()
        {

            // проверка флага, сигнализирующего о том, что координаты графика вычислены 
            if (not_calculate)
            {
                // если нет, то вызываем функцию вычисления координат графика 
                functionCalculation();
            }

            // стартуем отрисовку в режиме визуализации точек 
            // объединяемых в линии (GL_LINE_STRIP) 
            Gl.glBegin(Gl.GL_LINE_STRIP);

            // рисуем начальную точку 
            Gl.glVertex2d(GrapValuesArray[0, 0], GrapValuesArray[0, 1]);

            // проходим по массиву с координатами вычисленных точек 
            for (int ax = 1; ax < elements_count; ax += 2)
            {
                // передаем в OpenGL информацию о вершине, участвующей в построении линий 
                Gl.glVertex2d(GrapValuesArray[ax, 0], GrapValuesArray[ax, 1]);
            }

            // завершаем режим рисования 
            Gl.glEnd();
            // устанавливаем размер точек, равный 5 пикселям 
            Gl.glPointSize(5);
            // устанавливаем текущим цветом - красный цвет 
            Gl.glColor3f(255, 0, 0);
            // активируем режим вывода точек (GL_POINTS) 
            Gl.glBegin(Gl.GL_POINTS);
            // выводим красную точку, используя ту ячейку массива, до которой мы дошли (вычисляется в функции обработчике событий таймера) 
            Gl.glVertex2d(GrapValuesArray[pointPosition, 0], GrapValuesArray[pointPosition, 1]);
            // завершаем режим рисования 
            Gl.glEnd();
            // устанавливаем размер точек равный единице 
            Gl.glPointSize(1);

        }
    */

    /*

        // функция, управляющая визуализацией сцены 
        private void Draw()
        {

            // очистка буфера цвета и буфера глубины 
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);

            // очищение текущей матрицы 
            Gl.glLoadIdentity();

            // установка черного цвета 
            Gl.glColor3f(0, 0, 0);

            // помещаем состояние матрицы в стек матриц 
            Gl.glPushMatrix();

            // выполняем перемещение в пространстве по осям X и Y 
            Gl.glTranslated(15, 15, 0);

            // активируем режим рисования (Указанные далее точки будут выводиться как точки GL_POINTS) 
            Gl.glBegin(Gl.GL_POINTS);

            // с помощью прохода вдумя циклами, создаем сетку из точек 
            for (int ax = -15; ax < 15; ax++)
            {
                for (int bx = -15; bx < 15; bx++)
                {
                    // вывод точки 
                    Gl.glVertex2d(ax, bx);
                }
            }

            // завершение режима рисования примитивов 
            Gl.glEnd();

            // активируем режим рисования, каждые 2 последовательно вызванные команды glVertex 
            // объединяются в линии 
            Gl.glBegin(Gl.GL_LINES);

            // далее мы рисуем координатные оси и стрелки на их концах 
            Gl.glVertex2d(0, -15);
            Gl.glVertex2d(0, 15);

            Gl.glVertex2d(-15, 0);
            Gl.glVertex2d(15, 0);

            // вертикальная стрелка 
            Gl.glVertex2d(0, 15);
            Gl.glVertex2d(0.1, 14.5);
            Gl.glVertex2d(0, 15);
            Gl.glVertex2d(-0.1, 14.5);

            // горизонтальная трелка 
            Gl.glVertex2d(15, 0);
            Gl.glVertex2d(14.5, 0.1);
            Gl.glVertex2d(15, 0);
            Gl.glVertex2d(14.5, -0.1);

            // завершаем режим рисования 
            Gl.glEnd();

            // выводим подписи осей "x" и "y" 
            PrintText2D(15.5f, 0, "x");
            PrintText2D(0.5f, 14.5f, "y");

            // вызываем функцию рисования графика 
            DrawDiagram();

            // возвращаем матрицу из стека 
            Gl.glPopMatrix();

            // выводим текст со значением координат возле курсора 
            PrintText2D(devX * Mcoord_X + 0.2f,
                (float)ScreenH - devY * Mcoord_Y + 0.4f,
                "[ x: " + (devX * Mcoord_X - 15).ToString() +
                " ; y: " + ((float)ScreenH - devY * Mcoord_Y - 15).ToString() + "]");

            // устанавливаем красный цвет 
            Gl.glColor3f(255, 0, 0);

            // включаем режим рисования линий, для того чтобы нарисовать 
            // линии от курсора мыши к координатным осям 
            Gl.glBegin(Gl.GL_LINES);

            Gl.glVertex2d(lineX, 15);
            Gl.glVertex2d(lineX, lineY);
            Gl.glVertex2d(15, lineY);
            Gl.glVertex2d(lineX, lineY);

            Gl.glEnd();

            // дожидаемся завершения визуализации кадра 
            Gl.glFlush();

            // сигнал для обновление элемента реализующего визуализацию. 
            AnT1.Invalidate();

        }
    }
    */
   
}
