using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artificial_Intelligence {
    class AlgMachine {
        public double[,] classA;
        public double[,] classB;
        public double[,] classC;
        public double[] meanValues; 
        double[] limitUp;
        double[] limitDown;
        double[,] binA;
        double[,] binB;
        double[,] binC;
        double[] meanBinA;
        double[] EtalVecBinA;
        double[] meanBinB;
        double[] EtalVecBinB;
        double[] meanBinC;
        double[] EtalVecBinC;
        double[] dck_xkA; // к своим 
        double[] dck_xnA;// к чужим 
        double[] dck_xkB; // к своим 
        double[] dck_xnB; // к чужим 
        double[] dck_xkC; // к своим 
        double[] dck_xnC; // к чужим 
        public double[] k1_A;
        public double[] k2_A;
        public double[] k1_B;
        public double[] k2_B;
        public double[] k1_C;
        public double[] k2_C;
        public double[] E_A;
        public double[] E_B;
        public double[] E_C;
        public double k1_sum;
        public double k2_sum;
        public double E_sum;

        Functions functions = new Functions();


        public AlgMachine(double[,] classA, double[,] classB, double[,]classC) {
            this.classA = classA;
            this.classB = classB;
            this.classC = classC;
        }

        public void alg(double delta) {
            meanValues = functions.findMean(classA); //Поиск среднего значения
            algMain(delta);
        }

        public void alg(double delta, int baseClass) {
            switch (baseClass) {
                case 0:
                    meanValues = functions.findMean(classA);
                    break;
                case 1:
                    meanValues = functions.findMean(classB); 
                    break;
                case 2:
                    meanValues = functions.findMean(classC); 
                    break;
            }
            algMain(delta);
        }

        private void algMain(double delta) {
            limitUp = functions.findLimit(meanValues, "Up", delta); //верехний допуск по среднем значении classA
            limitDown = functions.findLimit(meanValues, "Down", delta); //нижний допуск по среднем значении classA

            binA = functions.BinMatrix(classA, limitDown, limitUp); 
            binB = functions.BinMatrix(classB, limitDown, limitUp); 
            binC = functions.BinMatrix(classC, limitDown, limitUp);

            meanBinA = functions.findMean(binA); //Поиск среднего значения bin A 0,54 - 0,6 - 0,3
            EtalVecBinA = functions.FindEtalVecBin(meanBinA); //Поиск еталоного вектора meanBinA 1, 0, 1
            meanBinB = functions.findMean(binB); //Поиск среднего значения bin B 0,54 - 0,6 - 0,3
            EtalVecBinB = functions.FindEtalVecBin(meanBinB); //Поиск еталоного вектора meanBinB 1, 0, 1
            meanBinC = functions.findMean(binC); //Поиск среднего значения bin C 0,54 - 0,6 - 0,3
            EtalVecBinC = functions.FindEtalVecBin(meanBinC); //Поиск еталоного вектора meanBinC 1, 0, 1

            double[] distancesA = new double[2];
            double[] distancesB = new double[2];
            double[] distancesC = new double[2];
            double[] minDistances = new double[3];
            int[] index = new int[3];

            distancesA[0] = functions.findCountXOR(EtalVecBinA, EtalVecBinB);//0 9 0
            distancesA[1] = functions.findCountXOR(EtalVecBinA, EtalVecBinC);

            distancesB[0] = functions.findCountXOR(EtalVecBinB, EtalVecBinA); //9 0 9
            distancesB[1] = functions.findCountXOR(EtalVecBinB, EtalVecBinC);

            distancesC[0] = functions.findCountXOR(EtalVecBinC, EtalVecBinA); //0 9 0
            distancesC[1] = functions.findCountXOR(EtalVecBinC, EtalVecBinB);


            minDistances[0] = functions.FindMinCount(distancesA); // поиск минимально значения 
            minDistances[1] = functions.FindMinCount(distancesB); // поиск минимально значения 
            minDistances[2] = functions.FindMinCount(distancesC); // поиск минимально значения 

            index[0] = Array.IndexOf(distancesA, minDistances[0]); //ижем индекс минимального числа в масиве distances
            index[1] = Array.IndexOf(distancesB, minDistances[1]); //ижем индекс минимального числа в масиве distances
            index[2] = Array.IndexOf(distancesC, minDistances[2]); //ижем индекс минимального числа в масиве distances


            dck_xkA = functions.findCountXORforEachLinesMatrix(EtalVecBinA, binA); // поиск несовпадений для каждой строки матрицы  74	37	41	32	41	38	45	39	40	32	34	37	37	35	30	48	37	45	40	37	45
            switch (index[0]) {
                case 0:
                    dck_xnA = functions.findCountXORforEachLinesMatrix(EtalVecBinA, binB);
                    break;
                case 1:
                    dck_xnA = functions.findCountXORforEachLinesMatrix(EtalVecBinA, binC); // 71	37	41	33	41	38	44	39	38	32	34	36	37
                    break;
            }

            dck_xkB = functions.findCountXORforEachLinesMatrix(EtalVecBinB, binB);
            switch (index[1]) {
                case 0:
                    dck_xnB = functions.findCountXORforEachLinesMatrix(EtalVecBinB, binA);
                    break;
                case 1:
                    dck_xnB = functions.findCountXORforEachLinesMatrix(EtalVecBinB, binC);
                    break;
            }

            dck_xkC = functions.findCountXORforEachLinesMatrix(EtalVecBinC, binC);
            switch (index[2]) {
                case 0:
                    dck_xnC = functions.findCountXORforEachLinesMatrix(EtalVecBinC, binA);
                    break;
                case 1:
                    dck_xnC = functions.findCountXORforEachLinesMatrix(EtalVecBinC, binB);
                    break;       
            }
        }


        public void algFind_K_KFE() {
            k1_A = functions.findK(dck_xkA, meanValues.Length);
            k2_A = functions.findK(dck_xnA, meanValues.Length);
            k1_B = functions.findK(dck_xkB, meanValues.Length);
            k2_B = functions.findK(dck_xnB, meanValues.Length);
            k1_C = functions.findK(dck_xkC, meanValues.Length);
            k2_C = functions.findK(dck_xnC, meanValues.Length);

            E_A = functions.find_KFE(k1_A, k2_A, meanValues.Length);
            E_B = functions.find_KFE(k1_B, k2_B, meanValues.Length);
            E_C = functions.find_KFE(k1_C, k2_C, meanValues.Length);


            
            //for (int i = 0; i < meanValues.Length; i++) {
            //    if (radius[1] != 0) {
            //        if (E_A[i] ==  KFE_max[1]) {
            //            k1_A_mid = k1_A[i];
            //            k2_A_mid = k2_A[i];
            //        }
            //    }
            //    else {
            //        k1_A_mid = k1_A[i] + double.PositiveInfinity;
            //        k2_A_mid = k2_A[i] + double.PositiveInfinity;
            //    }
            //}
        }
    }
}
