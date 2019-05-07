using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artificial_Intelligence {
    class CreateClassC {

        public double[,] create(double[,] classA) {
            Form1 form1 = new Form1();
            Functions functions = new Functions();

            double[,] matrix = new double[form1.sourseData.Length, form1.verticalLength];

            for (int i = 0; i < form1.sourseData.Length; i++) {
                for (int j = 0; j < form1.verticalLength; j += 1) {
                    if(i >= 128 && i<= 130) {
                        matrix[i, j] = classA[i, j] + (classA[i, j] / 100d * 10d);
                    }
                    else if(i >= 131 && i<= 133) {
                        matrix[i, j] = classA[i, j] + (classA[i, j] / 100d * 8d);
                    }
                    else if (i == 134) {
                        matrix[i, j] = classA[i, j] - (classA[i, j] / 100d * 10d);
                    }

                    else if (i >= 138 && i <= 168) {
                        int random = functions.getRandom(20, 26);
                        matrix[i, j] = form1.sourseData[i] + (form1.sourseData[i] / 100d * random); //увеличения числа на random
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
