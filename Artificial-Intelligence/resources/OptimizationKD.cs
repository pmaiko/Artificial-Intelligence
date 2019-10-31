using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artificial_Intelligence {
    class OptimizationKD : AlgMachine {
        public static int op_class;
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
        public double[] k1_max;
        public double[] k2_max;

        //public double[] E_optMax = new double[3];
        public double[] E_optMaxA;
        public double[] E_optMaxB;
        public double[] E_optMaxC;
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
    

                double opt = 0;
                int index = 0;
                base.start();
                for (int progon = 0; progon < 3; progon++) {
                    for (int oznaka = 0; oznaka < 169; oznaka++) {
                        index = E_max_index;
                        
                        opt = E_max;
                        E_max = 0;
                        
                        for(int delta = 0; delta < 50; delta++) { 
                            base.alg(oznaka, delta, index, progon);
                            repeat(delta);

                            //if (k1[delta] >= 0.5 && k2[delta] < 0.5) {
                                //if (E[delta] > opt) {
                                //    opt = E[delta];
                                //    index = delta;
                                //}
                            //}
                        }

                        

                        if (E_max == opt) {
                           // break;
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

                


                //double optDelta = 0;
                //int optDeltaIndex = 0;

                //for (int i = 0; i < 169; i++) {
                //    base.alg(i, 16);
                //    repeat(i);

                //    if (k1[i] >= 0.5 && k2[i] < 0.5) {
                //        if (E[i] > optDelta) {
                //            optDelta = E[i];
                //            optDeltaIndex = i;
                //        }
                //    }
                //}

                for (int cl = 0; cl < Form1.countClasses; cl++) {
                    

                    //if (functions.FindMaxCount(E) > max) {
                    //    E_max = E;
                    //    k1_max = k1;
                    //    k2_max = k2;
                    //    max = functions.FindMaxCount(E);

                    //    op_class = cl;
                    //}
                    
                }
            } else if (TYPE == "consistent-2") {
                E_optMaxA = new double [169];
                 E_optMaxB = new double [169];
                 E_optMaxC = new double [169];
                Form1 form1 = new Form1();
                int s = 0;
                double optDelta = 0;
                double optDeltaIndex = 0;

                for (int cl = 0; cl < Form1.countClasses; cl++) {

                    s += form1.sourseData.Length;
                    for (int i = 0; i <= 100; i++) {
                        //switch (cl) {
                        //    case 0:
                        //        base.alg(i);
                        //        repeat(i);
                        //        E_optMaxA[i] = E[i];
                        //        break;

                        //    case 1:
                        //        base.alg(i);
                        //        repeat(i);
                        //        E_optMaxB[i] = E[i];
                        //        break;

                        //    case 2:
                        //        base.alg(i);
                        //        repeat(i);
                        //        E_optMaxC[i] = E[i];
                        //        break;

                        //}
                        //optDelta = 0;
                        //optDeltaIndex = 0;
                        

                        // if (k1[i] >= 0.5 && k2[i] < 0.5) {
                        //    if (E[i] > optDelta) {
                        //        optDelta = E[i];
                        //        optDeltaIndex = i;
                        //    }
                        //}
                    }
                    //switch (cl) {
                    //        case 0:
                    //            E_optMaxA = E;
                    //            break;

                    //        case 1:
                    //            E_optMaxB = E;
                    //            break;

                    //        case 2:
                    //            E_optMaxC = E;
                    //            break;

                    //    }

                    
                    E_optMaxIndex[cl] = cl != 0 ? functions.FindMaxInxex(E) + s: 0;

                    //E_optMax[cl] = functions.FindMaxCount(E);
                    //E_optMaxIndex[cl] = cl != 0 ? functions.FindMaxInxex(E) + s: 0;

                    
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
