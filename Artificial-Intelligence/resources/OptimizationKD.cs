using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artificial_Intelligence {
    class OptimizationKD : AlgMachine {
        private double prevE;

        public double PrevE {
            get {
                return prevE;
            }

            set {
                prevE = value;
            }

        }

        public double[] E;
        public double[] k1;
        public double[] k2;
        public double[] KFE_max = new double[3];
        int[] radius = new int[3];
        double k1_A_mid;
        double k2_A_mid;
        double k1_B_mid;
        double k2_B_mid;
        double k1_C_mid;
        double k2_C_mid;

        public double[] E_max;
        public double[] k1_max;
        public double[] k2_max;
        Functions functions = new Functions();


        public OptimizationKD(double[,] classA, double[,] classB, double[,] classC) : base(classA, classB, classC) {
        }

        public void main(string TYPE) {
            E = new double[classA.GetLength(0)];
            k1 = new double[classA.GetLength(0)];
            k2 = new double[classA.GetLength(0)];

            if (TYPE == "parallel") {
                for (int i = 0; i <= 100; i++) {
                    base.alg(i);
                    repeat(i);
                }
            } else if (TYPE == "consistent") {
                double max = 0;
                for (int cl = 0; cl < Form1.countClasses; cl++) {
                    for (int i = 0; i <= 100; i++) {
                        base.alg(i, cl);
                        repeat(i);
                    }

                    if (functions.FindMaxCount(E) > max) {
                        E_max = E;
                        k1_max = k1;
                        k2_max = k2;
                        max = functions.FindMaxCount(E);
                    }
                    
                }
            }
            
        }

        private void repeat(int i) {
            base.algFind_K_KFE();

                KFE_max[0] = functions.FindMaxCount(E_A);
                KFE_max[1] = functions.FindMaxCount(E_B);
                KFE_max[2] = functions.FindMaxCount(E_C);

                radius[0] = functions.findWorkArea(k1_A, k2_A);
                radius[1] = functions.findWorkArea(k1_B, k2_B);
                radius[2] = functions.findWorkArea(k1_C, k2_C);

                k1_A_mid = functions.isFindWorkArea(E_A, KFE_max[0], k1_A, radius[0]);
                k2_A_mid = functions.isFindWorkArea(E_A, KFE_max[0], k2_A, radius[0]);
                k1_B_mid = functions.isFindWorkArea(E_B, KFE_max[1], k1_B, radius[1]);
                k2_B_mid = functions.isFindWorkArea(E_B, KFE_max[1], k2_B, radius[1]);
                k1_C_mid = functions.isFindWorkArea(E_C, KFE_max[2], k1_C, radius[2]);
                k2_C_mid = functions.isFindWorkArea(E_C, KFE_max[2], k2_C, radius[2]);


               
                KFE_max[0] = radius[0] == 0 ? 0 : KFE_max[0];

                KFE_max[1] = radius[1] == 0 ? 0 : KFE_max[1];

                KFE_max[2] = radius[2] == 0 ? 0 : KFE_max[2];

                k1_sum =  k1_A_mid +  k1_B_mid +  k1_C_mid;
                k2_sum = k2_A_mid +  k2_B_mid +  k2_C_mid;
                E_sum = KFE_max[0] + KFE_max[1] + KFE_max[2];

                k1[i] = k1_sum / 3d;
                k2[i] = k2_sum / 3d;
                E[i] = E_sum / 3d;

                if (radius[0] == 0 || radius[1] == 0 || radius[2] == 0) {
                    if (prevE < E[i]) {
                        E[i] = 0;
                    }
                }
                prevE = i != 0 ? E[i - 1] : 0;
        }

    }
}
