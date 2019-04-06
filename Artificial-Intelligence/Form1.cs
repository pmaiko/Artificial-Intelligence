using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Artificial_Intelligence
{
    public partial class Form1 : Form {
        double delta = 20;
        int verticalLength = 40;
        double[] mean = {30.70, 39.9, 42.6, 42.5, 49.9, 43.5, 43.4, 43.9, 44.4, 48.6, 43.6, 42.9, 44.4, 42.8, 51.9, 43.5, 44.4, 43.5, 43.8, 52.8, 43.4, 42.7, 41.8, 42.5, 46.4, 42.5, 46.4, 42.5, 41.6, 42.8, 44.4, 44.5, 43.7, 42.9, 43.5, 44.5, 54.9, 54.3, 56.7, 64.6, 70.4, 69.4, 31.9, 31.9, 35.6, 42.8, 40.9, 41.4, 54.7, 55.1, 50.9, 20.2, 52.8, 22.1, 30.7, 70.4, 38.5, 31.9, 44.5, 13.8, 54.9, 23, 47.6, 48.9, 47.8, 50.5, 50.7, 50.9, 49.9, 47.5, 45.7, 45.9, 46.4, 48.6, 48.5, 46.6, 45.7, 47.8, 47.8, 49.8, 47.6, 45.9, 46.7, 46.5, 47.5, 47.7, 45.6, 50.4, 48.5, 45.9, 47.9, 168.7, 92.9, 193.4, 0.871, 50, 16.05, 16.05, 15.94, 16.01, 6943, 7100, 7044, 7029, 334, 1584, 22, 19, 16, 24, 14, 7, 2.42, 2.11, 1.79, 2.67, 1.5, 0.76, 334, 1584, 0.21, 63.1, 31.9, 31.2};
        double[] dopuskUp;
        double[] dopuskDown;
        double[] isTrue;
        public double[,] classA;
        double[,] bin;

        public Form1() {
            InitializeComponent();
        }

        public void consondeb(double[] value) {
            NativeMethods.AllocConsole();
            //IntPtr stdHandle = NativeMethods.GetStdHandle(NativeMethods.STD_OUTPUT_HANDLE);
            for (int i = 0; i<value.Length; i++) {
                Console.Write(Convert.ToString(value[i]));
            }           
        }

        public double[] funFindDopusk(double[] mean, string which__one) {
            double[] dopusk = new double[mean.Length];
            if (which__one == "Up") {
                for (int i = 0; i < mean.Length; i++) {
                    dopusk[i] = mean[i] * (100 + delta) / 100;
                }
            }
            if (which__one == "Down") {
                for (int i = 0; i < mean.Length; i++) {
                    dopusk[i] = mean[i] - ((mean[i] * delta) / 100);
                }
            }
            return dopusk;
        }

        public double[,] createMatrix() {
            double[,] matrix = new double[mean.Length, verticalLength];
            for (int j = 0; j < verticalLength; j += 7) {
                for (int i = 0; i < mean.Length; i++) {
                    matrix[i, j] = mean[i];
                    if (j + 1 < verticalLength) {
                        matrix[i, j + 1] = getRandom(dopuskDown[i], dopuskUp[i]);
                    }
                    if (j + 2 < verticalLength) {
                        matrix[i, j + 2] = getRandom(dopuskDown[i], dopuskUp[i]);
                    }
                    if (j + 3 < verticalLength) {
                        matrix[i, j + 3] = dopuskUp[i] + getRandom(1, 3);
                    }
                    if (j + 4 < verticalLength) {
                        Random rand = new Random();
                        double tmp = rand.Next(0,2);
                        textBox1.Text = Convert.ToString(textBox1.Text = textBox1.Text + tmp + ',');
                        if (tmp == 1) {
                            matrix[i, j + 4] = getRandom(dopuskDown[i], dopuskUp[i]);
                        }
                        else
                        {
                            if(tmp == 0) {
                                matrix[i, j + 4] = dopuskDown[i] - getRandom(1, 3);
                            }
                            else {
                                matrix[i, j + 4] = dopuskUp[i] + getRandom(1, 3);
                            }
                        }
                    }

                    if (j + 5 < verticalLength) {
                        matrix[i, j + 5] = getRandom(dopuskDown[i], dopuskUp[i]);
                    }

                    if (j + 5 < verticalLength) {
                        matrix[i, j + 6] = dopuskDown[i] - getRandom(1, 3);
                    }

                }
            }

            return matrix;
        }

        public double[,] BinMatrix(double[,] matrix, double[] dopuskDownn, double[] dopuskUpp) {
            double[,] Bin = new double[matrix.GetLength(0), matrix.GetLength(1)];

            for (int j = 0; j < matrix.GetLength(1); j++) {
                for (int i = 0; i < matrix.GetLength(0); i++) {

                    if (matrix[i, j] < dopuskUpp[i] && matrix[i, j] > dopuskDownn[i]) {
                        Bin[i, j] = 1d;
                    }
                    else {
                        Bin[i, j] = 0d;
                    }
                }

            }
            return Bin;
        }
        public double[] countTrue(double[,] matrix) { // количестов 1 в столбике
            double[] mas = new double[matrix.GetLength(0)];

            for (int i = 0; i < matrix.GetLength(0); i++) {
                for (int j = 0; j < matrix.GetLength(1); j++) {
                    if (matrix[i, j] == 1) {
                        mas[i]++;
                    }
                }
            }
            return mas;
        }

        public double getRandom(double from, double to) {
            Random rand = new Random();
            return Math.Round(rand.NextDouble() * (to - from) + from, 2);
        }

        private void Form1_Load(object sender, EventArgs e) {
            Form2 form2 = new Form2();
            dopuskUp = funFindDopusk(mean, "Up");
            dopuskDown = funFindDopusk(mean, "Down");
            classA = createMatrix();
            bin = BinMatrix(classA, dopuskDown, dopuskUp);
            isTrue = countTrue(bin);


            form2.Output(isTrue);
            form2.Show();
            //add brange
            //textBox1.Text = Convert.ToString(classA.GetLength(0));
            //textBox1.Text = Convert.ToString(s);
        }
    }

    public partial class NativeMethods
    {
        public static Int32 STD_OUTPUT_HANDLE = -11;

        [System.Runtime.InteropServices.DllImportAttribute("kernel32.dll", EntryPoint = "GetStdHandle")]
        public static extern System.IntPtr GetStdHandle(Int32 nStdHandle);

        [System.Runtime.InteropServices.DllImportAttribute("kernel32.dll", EntryPoint = "AllocConsole")]

        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]

        public static extern bool AllocConsole();
    }
}
