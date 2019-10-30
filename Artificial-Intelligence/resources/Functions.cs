using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artificial_Intelligence {
    class Functions {
        Random rand = new Random();

        public double[] findMean(double[,] Matrix) {
            double z = 0;
            double [] meanMas = new double[Matrix.GetLength(0)];

            for (int i = 0; i < Matrix.GetLength(0); i++) { //124
                for (int j = 0; j <  Matrix.GetLength(1); j++) { //40
                    z = z + Matrix[i, j];
                }
                meanMas[i] = z / 40d;
                z = 0;
            }
            return meanMas;
        }

        public double[] findLimit(double[] array, string which__one, double delta) { //поиск верхнего, нижнего допуска
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

        public double[] findLimit(double[] array, string which__one, double delta, int oznaka) { //поиск верхнего, нижнего допуска
            double[] retArray = new double[array.Length];
            if (which__one == "Up") {
                for (int i = 0; i < array.Length; i++) {
                    retArray[i] = array[i];
                }
                retArray[oznaka] = retArray[oznaka] + delta; 
            }
            if (which__one == "Down") {
                for (int i = 0; i < array.Length; i++) {
                    retArray[i] = array[i];
                }

                retArray[oznaka] = retArray[oznaka] - delta; 
            }
            return retArray;
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

        public double[] FindEtalVecBin(double[] array) {
            double[] newMeanBin = new double[array.Length];
            for (int i = 0; i < array.Length; i++) {
                if (array[i] > 0.5d) {
                    newMeanBin[i] = 1;
                }

                else {
                    newMeanBin[i] = 0;
                }
            }
            return newMeanBin;
        }

        public double findCountXOR(double[] mainEtal, double[] AnotherEtal) {
            double count = 0;
                for (int i = 0; i < mainEtal.Length; i++) {
                    if (mainEtal[i] != AnotherEtal[i]) {
                        count = count + 1;
                    }
                }
     
            return count;
        }

        public double FindMaxCount(double[] arr) {
            double max = arr[0];
            for (int i = 1; i < arr.Length; i++) {
                if (arr[i] > max) {
                    max = arr[i];
                }
            }
            return max;
        }

        public double FindMaxInxex(double[] arr) {
            double max = arr[0];
            double maxIndex = 1;
            for (int i = 1; i < arr.Length; i++) {
                if (arr[i] > max) {
                    max = arr[i];
                    maxIndex = i;
                }
            }
            return maxIndex;
        }

        public double FindMinCount (double[] arr) {
            double min = arr[0];
            for (int i = 1; i <arr.Length; i++) {
                if (arr[i] < min) {
                    min = arr[i];
                }  
            }
            return min;
        }

        public double[] findCountXORforEachLinesMatrix(double[] mainEtal, double[,] bin) {
            double[] array = new double[bin.GetLength(1)];
            int sum;
            for (int j = 0; j < bin.GetLength(1); j++) { //40
                sum = 0;
                for (int i = 0; i < bin.GetLength(0); i++) { //124
                    if (mainEtal[i] != bin[i, j]) {
                        sum++;
                    }
                }
                array[j] = sum;
            }
            return array;
        }

        public double[] findK(double[] array, int length) {
            double[] k = new double[length];
            for (int i = 0; i < length; i++) {
                double tmp = 0;
                for (int j = 0; j < array.Length; j++) {
                    if (array[j] <= i) {
                        tmp++;
                    }
                }
                k[i] = tmp / array.Length;
            }
            return k;
        }

        public double[] find_KFE(double[] k1, double[] k2, int length) {
            double[] E = new double[length];
            double[] pt = new double[length];
            double[] pf = new double[length];
            double[] sump = new double[length];

            for (int i = 0; i < length; i++) {
                pt[i] = ((k1[i] + 1 - k2[i]) / 2);
                pf[i] = ((1 - k1[i] + k2[i]) / 2);
                sump[i] = pt[i] - pf[i];
                E[i] = Math.Log((pt[i] + 0.005) / (pf[i] + 0.005), 2.0) * (sump[i]);
            }

            return E;
        }

        public int findWorkArea(double[] k1, double [] k2) {
            int radius = 0;
            for (int i = 0; i < k2.Length; i++) {
                if (k1[i] >= 0.5 && k2[i] < 0.5) {
                    radius++;
                }
            }

            return radius;
        }

        public double isFindWorkArea(double[] E, double E_max, double[] k, int radius) {
            double k_mid = 0;
            for (int i = 0; i < k.Length; i++) {
                if (radius != 0) {
                    if (E[i] ==  E_max) {
                        k_mid = k[i];
                    }
                }
                else {
                    k_mid = k[i] + double.PositiveInfinity;
                }
            }
            return k_mid;
        }
    }
}
