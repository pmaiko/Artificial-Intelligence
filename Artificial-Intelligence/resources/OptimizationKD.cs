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

        public double[] E_consistent_1;
        public double[] E_consistent_2;
        public double[] E_consistent_3;
        public double[] E_consistent_all;
        public double[] E_optMaxIndex = new double[3];

        double E_max = 0;
        int E_max_index;
        Functions functions = new Functions();
        Form1 form1 = new Form1();

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
                E_consistent_1 = new double [form1.sourseData.Length];
                E_consistent_2 = new double [form1.sourseData.Length];
                E_consistent_3 = new double [form1.sourseData.Length];
                E_consistent_all = new double [form1.sourseData.Length*3];


                double progI = 0; /// progress Bar
                double opt = 0;
                int index = 0;
                base.start();
                for (int progon = 0; progon < 3; progon++) {
                    for (int oznaka = 0; oznaka < form1.sourseData.Length; oznaka++) {
                        ///proggressBar
                        double progress = (100 * progI) / (form1.sourseData.Length * 3);
                        Write.pbr1.Invoke(new Action(() =>{
                            Write.pbr1.Value = Convert.ToInt32(progress);
                            Write.lbl4.Text = Convert.ToString(Math.Round(progress, 5) + "%");
                        }));
                        progI++;
                        ///proggressBar End
                        index = E_max_index;
                        
                        opt = E_max;
                        E_max = 0;
                        
                        for(int delta = 0; delta < 50; delta++) { 
                            base.alg(oznaka, delta, index, progon);
                            repeat(delta);
                        }
                        
                        switch (progon) {
                            case 0:
                                E_consistent_1[oznaka] = E_max;
                                break;
                            case 1:
                                E_consistent_2[oznaka] = E_max;
                                break;
                            case 2:
                                E_consistent_3[oznaka] = E_max;
                                break;

                        } 
                    }

                    limitUp[form1.sourseData.Length -1] = limitUpSave[form1.sourseData.Length - 1] + E_max_index;
                    limitDown[form1.sourseData.Length - 1] = limitDownSave[form1.sourseData.Length - 1] - E_max_index;
                }//PA

                base.SaveLimit();

                for (int i = 0; i < form1.sourseData.Length *3; i++) {
                    if (i < form1.sourseData.Length) {
                        E_consistent_all[i] = E_consistent_1[i];

                    } else if (i < form1.sourseData.Length * 2) {
                        E_consistent_all[i] = E_consistent_2[i - form1.sourseData.Length];

                    } else if (i < form1.sourseData.Length * 3) {
                        E_consistent_all[i] = E_consistent_3[i - (form1.sourseData.Length * 2)];
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

            if (k1[i] >= 0.5 && k2[i] < 0.5) {
                if (E[i] > E_max) {
                    E_max = E[i];
                    E_max_index = i;
                }
            }
           
        }

    }
}
