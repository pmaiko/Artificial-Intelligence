using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artificial_Intelligence {
    class AlgExam : AlgMachine {
        double[,] classExam;
        double[,] classExamBin;
        double[] sk1;
        double[] sk2;
        double[] sk3;
        double EmaxIndexA;
        double EmaxIndexB;
        double EmaxIndexC;
        double[] M = new double[3];



        Functions functions = new Functions();
        Form1 form1 = new Form1();

        public AlgExam(double[,] classA, double[,] classB, double[,] classC,  double[,] classExam) : base(classA, classB, classC) {
            base.alg(16);
            this.classExamBin = functions.BinMatrix(classExam, limitDown, limitUp);
            base.algFind_K_KFE();
            this.classExam = classExam;
            main();
        }

        void main() {
            double[] funNaLA = new double[form1.verticalLength];
            double[] funNaLB = new double[form1.verticalLength];
            double[] funNaLC = new double[form1.verticalLength];
            sk1 = functions.findCountXORforEachLinesMatrix(base.EtalVecBinA, this.classExamBin);
            sk2 = functions.findCountXORforEachLinesMatrix(base.EtalVecBinB, this.classExamBin);
            sk3 = functions.findCountXORforEachLinesMatrix(base.EtalVecBinC, this.classExamBin);

            EmaxIndexA = functions.FindMaxInxex(E_A);
            EmaxIndexB = functions.FindMaxInxex(E_B);
            EmaxIndexC = functions.FindMaxInxex(E_C);

            for (int i = 0; i < form1.verticalLength; i++) {
                funNaLA[i] = 1 - (sk1[i] / EmaxIndexA);
                funNaLB[i] = 1 - (sk2[i] / EmaxIndexB);
                funNaLC[i] = 1 - (sk3[i] / EmaxIndexC);

            }

            M[0] = functions.findMean(funNaLA);
            M[1] = functions.findMean(funNaLB);
            M[2] = functions.findMean(funNaLC);

            Write.txt1.Text = Convert.ToString(M[0]);
            Write.txt2.Text = Convert.ToString(M[1]);
            Write.txt3.Text = Convert.ToString(M[2]);


            double M_max = functions.FindMaxCount(M);
            int M_max_Index = Convert.ToInt32(functions.FindMaxInxex(M));

            if (M_max <= 0) {
                Write.txt4.Text = "Нікому не принадлежить";
            }
            else {
                switch (M_max_Index) {
                    case 0:
                        Write.txt4.Text = "Принадлежить 1";
                        break;
                    case 1:
                        Write.txt4.Text = "Принадлежить 2";
                        break;
                    case 2:
                        Write.txt4.Text = "Принадлежить 3";
                        break;
                    default:
                        Write.txt4.Text = "Нікому не принадлежить";
                        break;
                }
            }
        } 
    }
}
