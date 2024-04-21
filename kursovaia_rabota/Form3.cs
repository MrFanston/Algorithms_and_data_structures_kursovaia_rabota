using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace kursovaia_rabota
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            listView1.Items.Clear();
            textBox_n.Clear();
            textBox_x.Clear();
            textBox_y.Clear();
            textBox_x1.Clear();
            textBox_xy.Clear();
            textBox_xx.Clear();
            textBox_a0.Clear();
            textBox_a1.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            textBox8.Clear();
            textBox9.Clear();
        }

        private List<double> arr = new List<double>(); // массив с данными
        private Random rand = new Random();
        private void button_sort_Click(object sender, EventArgs e)
        {
            int n = Convert.ToInt32(numericUpDown_n.Value);
            int A = Convert.ToInt32(numericUpDown_A.Value);
            int B = Convert.ToInt32(numericUpDown_B.Value);
            int C = Convert.ToInt32(numericUpDown_C.Value);
            double[] result = new double[3];

            double x;
            for (int i = 0; i < n; i++)
            {
                x = generateRandomNumber(A, B, C);
                dataGridView1.Rows.Add(i+1, x);
                arr.Add(x);
            }

            result = Sort(arr);  //   Partience Sort

            textBox1.Text = Convert.ToString(result[0]);
            textBox2.Text = Convert.ToString(result[1]);
            textBox3.Text = Convert.ToString(result[2]);

            for (int i = 0; i < arr.Count(); i++)
                dataGridView2.Rows.Add(i+1, arr[i]);
        }
        double generateRandomNumber(double A, double B, double C) // зак. распр. Fisk
        {
            int u;
            double root, right;
            u = rand.Next();
            right = (Convert.ToDouble(u) / Convert.ToDouble(int.MaxValue)); // Проекция u на интервале (0,1)
            root = A + B * Math.Pow((1 / right - 1), (-1 / C)); // обратная функция распределения Fisk
            return root;
        }
        public double[] Sort(List<double> arr)
        {
            int n = arr.Count; // количество элементов в таблице
            int c = 0; // количество операций сравнения элементов массива
            int m = 0; // количество перестановок элементов массива
            List<List<double>> deck = new List<List<double>>(); // колоды для сортировки
            //создаем объект
            Stopwatch stopwatch = new Stopwatch();
            //засекаем время начала операции
            stopwatch.Start();

            deck.Add(new List<double>() { arr[0] });

            // перебираем элементы массива
            for (int i = 1; i < n; i++)
            {
                bool flag = true;
                // перебираем колоды
                for (int j = 0; j < deck.Count; j++)
                {
                    c++;
                    // если элемент меньше или равен последниму элементу колоды
                    if (arr[i] <= deck[j].Last())
                    {
                        deck[j].Add(arr[i]);
                        flag = false;
                        m++;
                        break;
                    }
                }
                if (flag)
                {
                    m++;
                    deck.Add(new List<double>() { arr[i] });
                }

            }

            arr.Clear();
            double a = -1.0;
            int index = 0;
            while (true)
            {
                // перебираем колоды
                for (int i = 0; i < deck.Count; i++)
                {
                    // берем первый элемент для сравнения
                    if (a == -1.0 && deck[i].Count != 0)
                    {
                        a = deck[i].Last();
                        index = i;
                    }
                    // ищем минимальный элемент в колодах
                    else if (deck[i].Count != 0 && a >= deck[i].Last())
                    {
                        c++;
                        a = deck[i].Last();
                        index = i;
                    }
                }
                if (a == -1.0)
                {
                    break;
                }
                m++;
                // добавление элемента
                arr.Add(a);
                a = -1.0;
                deck[index].RemoveAt(deck[index].Count - 1);
            }

            //останавливаем счётчик
            stopwatch.Stop();

            return new double[] { stopwatch.ElapsedMilliseconds, n, m };

        }
        private void button4_Click_1(object sender, EventArgs e)
        {
            int x = 0;
            long y = 0;
            long x1 = 0;
            long y1 = 0;
            long xy = 0;
            int count = 9000;

            for (int i = 0; i <= 9; i++)
            {
                double[] result = new double[3];
                List<double> array = new List<double>(); // массив с данными

                for (int k = 0; k < count; k++)
                {
                    array.Add(generateRandomNumber(1, 1, 1));  // Fisk
                }

                result = Sort(array);  // Partience Sort

                long elapsedMilliseconds = (long)result[0];

                ListViewItem item1 = new ListViewItem(Convert.ToString(i+1));
                item1.SubItems.Add(Convert.ToString(count));
                item1.SubItems.Add(Convert.ToString(elapsedMilliseconds));
                item1.SubItems.Add(Convert.ToString(count * count));
                item1.SubItems.Add(Convert.ToString(count * elapsedMilliseconds));
                item1.SubItems.Add(Convert.ToString(elapsedMilliseconds * elapsedMilliseconds));

                listView1.Items.Add( item1 );


                this.chart1.Series[0].Points.AddXY(count, elapsedMilliseconds);

                x += count;
                y += elapsedMilliseconds;
                x1 += count * count;
                y1 += elapsedMilliseconds * elapsedMilliseconds;
                xy += count * elapsedMilliseconds;
                count += 4000;
            }

            ListViewItem item2 = new ListViewItem("∑");
            item2.SubItems.Add(Convert.ToString(x));
            item2.SubItems.Add(Convert.ToString(y));
            item2.SubItems.Add(Convert.ToString(Convert.ToString(x1)));
            item2.SubItems.Add(Convert.ToString(Convert.ToString(xy)));
            item2.SubItems.Add(Convert.ToString(Convert.ToString(y1)));

            listView1.Items.Add(item2);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Система нормальных уравнений:
            textBox_n.Text = "10";
            textBox_x.Text = listView1.Items[10].SubItems[1].Text;
            textBox_y.Text = listView1.Items[10].SubItems[2].Text;
            textBox_x1.Text = listView1.Items[10].SubItems[1].Text;
            textBox_xx.Text = listView1.Items[10].SubItems[3].Text;
            textBox_xy.Text = listView1.Items[10].SubItems[4].Text;

            // Создание матрицы и решение по Гауссу
            double[] result = new double[2];
            double[,] matrix = new double[2,3];
            matrix[0, 0] = 10;
            matrix[0, 1] = Convert.ToDouble(listView1.Items[10].SubItems[1].Text);
            matrix[0, 2] = Convert.ToDouble(listView1.Items[10].SubItems[2].Text);
            matrix[1, 0] = matrix[0, 1];
            matrix[1, 1] = Convert.ToDouble(listView1.Items[10].SubItems[3].Text);
            matrix[1, 2] = Convert.ToDouble(listView1.Items[10].SubItems[4].Text);
            result = Gauss(matrix);
            textBox_a0.Text = result[0].ToString();
            textBox_a1.Text = result[1].ToString();

            // Коэффициенты уравнения связи:
            double a0 = Convert.ToDouble(textBox_a0.Text);
            double a1 = Convert.ToDouble(textBox_a1.Text);

            textBox8.Text = textBox_a0.Text;
            textBox9.Text = textBox_a1.Text;

            bool flag = true;

            // Добавляем точки на прямой в соответствии с формулой
            for (int i = 0; i <= 45000; i++)
            {
                double j = a0 + a1*i; // формулa прямой
                if (j >= 0 && flag)
                {
                    chart1.Series[1].Points.AddXY(i, j);
                    flag = false;
                }
                else if (i == 45000)
                {
                    chart1.Series[1].Points.AddXY(i, j);
                }

            }

            int n = 10;
            long x = Convert.ToInt64(textBox_x.Text);
            long y = Convert.ToInt64(textBox_y.Text);
            long xy = Convert.ToInt64(textBox_xy.Text);
            long xx = Convert.ToInt64(textBox_xx.Text);
            long yy = Convert.ToInt64(listView1.Items[10].SubItems[5].Text);
            
            // Парный коэффициент корреляции
            double r = ((n * xy) - (x * y)) / (Math.Sqrt(((n * xx) - (x * x)) * ((n * yy) - (y * y))));
            textBox4.Text = r.ToString();
            
            // Совокупный коэффициент детерминации
            double r2 = r * r;
            textBox5.Text = r2.ToString();

            // Коэффициент эластичности
            double x_medium = x / n;
            double y_medium = y / n;
            double elasticity = a1 * x_medium / y_medium;
            textBox6.Text = elasticity.ToString();

            // Бета-коэффициент
            double sum_x_x_medium = 0;
            for(int i = 0; i < n; i++)
            {
                sum_x_x_medium += Math.Pow(Convert.ToDouble(listView1.Items[i].SubItems[1].Text) - x_medium, 2);
            }
            double sx = Math.Sqrt(sum_x_x_medium / n);
            double sum_y_y_medium = 0;
            for (int i = 0; i < n; i++)
            {
                sum_y_y_medium += Math.Pow(Convert.ToDouble(listView1.Items[i].SubItems[2].Text) - y_medium, 2);
            }
            double sy = Math.Sqrt(sum_y_y_medium / n);
            double beta = a1 * sx / sy;
            textBox7.Text = beta.ToString();

        }
        public double[] Gauss(double[,] matrix)
        {
            int n = matrix.GetLength(0);
            double[] solution = new double[n];

            // Прямой ход метода Гаусса
            for (int k = 0; k < n - 1; k++)
            {
                for (int i = k + 1; i < n; i++)
                {
                    double factor = matrix[i, k] / matrix[k, k];
                    for (int j = k; j < n + 1; j++)
                    {
                        matrix[i, j] -= factor * matrix[k, j];
                    }
                }
            }

            // Обратный ход метода Гаусса
            for (int i = n - 1; i >= 0; i--)
            {
                double sum = 0;
                for (int j = i + 1; j < n; j++)
                {
                    sum += matrix[i, j] * solution[j];
                }
                solution[i] = (matrix[i, n] - sum) / matrix[i, i];
            }

            return solution;
        }
    }
}
