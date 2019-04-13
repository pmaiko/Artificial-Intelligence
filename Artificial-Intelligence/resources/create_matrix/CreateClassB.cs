using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artificial_Intelligence {
    class CreateClassB {

        public double[,] create(double [,] classA) {

            Form1 form1 = new Form1();
            Functions functions = new Functions(form1.delta);

            double[,] matrix = new double[form1.sourseData.Length, form1.verticalLength];
            for (int i = 0; i < form1.sourseData.Length; i++) {
                for (int j = 0; j < form1.verticalLength; j += 1) {
                    if(i <= 32) { //Температура дистиллята до 32
                        int tDRand = functions.getRandom(5, 10); //Рандом от 5 до 9 Температура дистиллята
                        matrix[i, j] = classA[i, j] + Convert.ToDouble(tDRand);
                    }
                    else if(i == 48) {
                        int Rand = functions.getRandom(8, 11); //Рандом от 8 до 10
                        matrix[i, j] = classA[i, j] + Convert.ToDouble(Rand);
                    }

                     else if(i == 49) {
                        int Rand = functions.getRandom(5, 7); //Рандом от 5 до 6
                        matrix[i, j] = classA[i, j] + Convert.ToDouble(Rand);
                    }

                    else if(i == 50) {
                        int Rand = functions.getRandom(8, 11); //Рандом от 8 до 10
                        matrix[i, j] = classA[i, j] + Convert.ToDouble(Rand);
                    }

                     else if(i == 51) {
                        int Rand = functions.getRandom(5, 7); //Рандом от 5 до 6
                        matrix[i, j] = classA[i, j] + Convert.ToDouble(Rand);
                    }

                    else if(i == 52) {
                        int Rand = functions.getRandom(6, 9); //Рандом от 6 до 8
                        matrix[i, j] = classA[i, j] + Convert.ToDouble(Rand);
                    }

                    else if(i == 53) {
                        int Rand = functions.getRandom(8, 12); //Рандом от 8 до 11
                        matrix[i, j] = classA[i, j] + Convert.ToDouble(Rand);
                    }

                    else if(i == 54) {
                        int Rand = functions.getRandom(5, 8); //Рандом от 5 до 7
                        matrix[i, j] = classA[i, j] + Convert.ToDouble(Rand);
                    }

                     else if(i == 55) {
                        int Rand = functions.getRandom(5, 7); //Рандом от 5 до 6
                        matrix[i, j] = classA[i, j] + Convert.ToDouble(Rand);
                    }

                    else if(i == 56) {
                        int Rand = functions.getRandom(6, 8); //Рандом от 6 до 7
                        matrix[i, j] = classA[i, j] + Convert.ToDouble(Rand);
                    }

                    else if(i == 57) {
                        int Rand = functions.getRandom(4, 6); //Рандом от 4 до 5
                        matrix[i, j] = classA[i, j] + Convert.ToDouble(Rand);
                    }

                    else if(i == 58) {
                        int Rand = functions.getRandom(8, 10); //Рандом от 8 до 9
                        matrix[i, j] = classA[i, j] + Convert.ToDouble(Rand);
                    }

                    else if(i == 59) {
                        int Rand = functions.getRandom(5, 8); //Рандом от 5 до 7
                        matrix[i, j] = classA[i, j] + Convert.ToDouble(Rand);
                    }

                    else if(i >= 60 && i < 90) {
                        int oSRand = functions.getRandom(10, 15); //Рандом от 10 до 14 Обмотка статора
                        matrix[i, j] = classA[i, j] + Convert.ToDouble(oSRand);
                    }
                    else {
                        matrix[i, j] = classA[i, j];
                    }
                   
                }
            }
                
            return matrix;
        }
    }
}
