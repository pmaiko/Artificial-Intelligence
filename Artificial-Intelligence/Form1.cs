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
        Random rand = new Random();
        double delta = 20;
        int verticalLength = 40;
        double[] sourseData = {30.7, 39.9, 42.6, 42.5, 49.9, 43.5, 43.4, 43.9, 44.4, 48.6, 43.6, 42.9, 44.4, 42.8, 51.9, 43.5, 44.4, 43.5, 43.8, 52.8, 43.4, 42.7, 41.8, 42.5, 46.4, 42.5, 41.6, 42.8, 44.4, 44.5, 43.7, 42.9, 43.5, 54.9, 54.3, 56.7, 64.6, 70.4, 69.4, 31.9, 31.9, 35.6, 42.8, 41.9, 40.9, 41.4, 54.7, 55.1, 50.9, 20.2, 52.8, 22.1, 30.7, 70.4, 38.5, 31.9, 44.5, 13.8, 54.9, 23, 47.8, 47.8, 49.8, 47.6, 47.9, 45.9, 46.7, 46.5, 47.5, 47.7, 45.6, 50.4, 48.5, 45.9, 47.9, 47.6, 48.9, 47.8, 50.5, 50.7, 50.9, 49.9, 47.5, 45.7, 45.9, 46.4, 48.6, 48.5, 46.6, 45.7, 168.7, 92.9, 193.4, 0.871, 50, 16.05, 16.05, 15.94, 16.01, 6943, 7100, 7044, 7029, 334, 1584, 22, 19, 16, 24, 14, 7, 2.42, 2.11, 1.79, 2.67, 1.5, 0.76, 334, 1584, 0.21, 63.1, 31.9, 31.2};
        double[] meanValues; 
        double[] limitUp;
        double[] limitDown;
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

        public double[] findMean(double[,] Matrix) {
            double z = 0;
            double [] meanMas = new double[Matrix.GetLength(0)];

            for (int i = 0; i < Matrix.GetLength(0); i++) { //124
                for (int j = 0; j <  Matrix.GetLength(1); j++) { //40
                    z = z + Matrix[i, j];
                }
                meanMas[i] = z / Matrix.GetLength(1);
                z = 0;
            }
            return meanMas;
        }

        public double[] findLimit(double[] array, string which__one) { //поиск верхнего, нижнего допуска
            double[] retArray = new double[array.Length];
            if (which__one == "Up") {
                for (int i = 0; i < array.Length; i++) {
                    retArray[i] = array[i] + delta;
                }
            }
            if (which__one == "Down") {
                for (int i = 0; i < array.Length; i++) {
                    retArray[i] = array[i] - delta;
                }
            }
            return retArray;
        }

        public double[,] createMatrix() {
            double[,] matrix = new double[sourseData.Length, verticalLength];
            for (int j = 0; j < verticalLength; j += 1) {
                for (int i = 0; i < sourseData.Length; i++) {
                    if (j == 0) {
                        matrix[i, j] = sourseData[i];
                    }
                    else {
                        int random = getRandom(0, 100);
                        int plasMinus = getRandom(0, 2);

                        if (plasMinus == 1) {
                            matrix[i, j] = sourseData[i] + (sourseData[i] / 100d * random); //увеличения числа на random
                            //matrix[i, j] = sourseData[i] + (sourseData[i]+(sourseData[i]/100d*random));
                        }
                        else {
                            matrix[i, j] = sourseData[i] - (sourseData[i] / 100d * random); //уменьшения числа на random
                           //matrix[i, j] = sourseData[i] - (sourseData[i]-(sourseData[i]/100d*random));
                        }
                    }

                }
            }

            return matrix;
        }

        public double[,] BinMatrix(double[,] matrix, double[] dopuskDownn, double[] dopuskUpp) { //Перевод в бинарную матрицу
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
            return Math.Round(rand.NextDouble() * (to - from) + from,0);
        }

        public int getRandom(int from, int to) {
            return rand.Next(from, to);
        }

        private void Form1_Load(object sender, EventArgs e) {
            Form2 form2 = new Form2();

            //Создания классу А с помощью исходных данных
            classA = createMatrix();

            //Поиск среднего значения
            meanValues = findMean(classA);

            limitUp = findLimit(sourseData, "Up");
            limitDown = findLimit(sourseData, "Down");
            bin = BinMatrix(classA, limitDown, limitUp);

            //Количество единичек
            isTrue = countTrue(bin);

            //Вывод любои инфы
            form2.Output(classA);
            form2.Output2(meanValues);
            form2.Output3(limitUp);
            form2.Output4(limitDown);
            form2.Output5(bin);
            form2.Output6(isTrue);
            form2.Show();
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
