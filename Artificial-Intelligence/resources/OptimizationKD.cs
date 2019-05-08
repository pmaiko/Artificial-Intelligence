using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artificial_Intelligence {
    class OptimizationKD : AlgMachine {
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

        Functions functions = new Functions();


        public OptimizationKD(double[,] classA, double[,] classB, double[,] classC) : base(classA, classB, classC) {
        }

        public void main() {
            E = new double[classA.GetLength(0)];
            k1 = new double[classA.GetLength(0)];
            k2 = new double[classA.GetLength(0)];
            for (int i = 0; i <= 100; i++) {
                base.alg(i);
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
            
                k1_sum =  k1_A_mid +  k1_B_mid +  k1_C_mid;
                k2_sum = k2_A_mid +  k2_B_mid +  k2_C_mid;
                E_sum = KFE_max[0] + KFE_max[1] + KFE_max[2];
                E[i] = E_sum / 3d;
                k1[i] = k1_sum / 3d;
                k2[i] = k2_sum / 3d;
            }
        }
    }
}
