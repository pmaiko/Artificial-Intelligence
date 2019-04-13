using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artificial_Intelligence {
    class CreateClassA {
        //double[] sourseData = {30.7, 39.9, 42.6, 42.5, 49.9, 43.5, 43.4, 43.9, 44.4, 48.6, 43.6, 42.9, 44.4, 42.8, 51.9, 43.5, 44.4, 43.5, 43.8, 52.8, 43.4, 42.7, 41.8, 42.5, 46.4, 42.5, 41.6, 42.8, 44.4, 44.5, 43.7, 42.9, 43.5, 54.9, 54.3, 56.7, 64.6, 70.4, 69.4, 31.9, 31.9, 35.6, 42.8, 41.9, 40.9, 41.4, 54.7, 55.1, 50.9, 20.2, 52.8, 22.1, 30.7, 70.4, 38.5, 31.9, 44.5, 13.8, 54.9, 23, 47.8, 47.8, 49.8, 47.6, 47.9, 45.9, 46.7, 46.5, 47.5, 47.7, 45.6, 50.4, 48.5, 45.9, 47.9, 47.6, 48.9, 47.8, 50.5, 50.7, 50.9, 49.9, 47.5, 45.7, 45.9, 46.4, 48.6, 48.5, 46.6, 45.7, 168.7, 92.9, 193.4, 0.871, 50, 16.05, 16.05, 15.94, 16.01, 6943, 7100, 7044, 7029, 334, 1584, 22, 19, 16, 24, 14, 7, 2.42, 2.11, 1.79, 2.67, 1.5, 0.76, 334, 1584, 0.21, 63.1, 31.9, 31.2, 158.9, 161.9, 101.7, 97.3, 0.84, 330.8, 338.3, 330, 1579.3, 1600, 1600, 0.20946, 65.95, 33.15, 522.43};

        public double[,] create() {
            Form1 form1 = new Form1();
            Functions functions = new Functions(form1.delta);

            double[,] matrix = new double[form1.sourseData.Length, form1.verticalLength];

            for (int j = 0; j < form1.verticalLength; j += 1) {
                for (int i = 0; i < form1.sourseData.Length; i++) {
                    if (j == 0) {
                        matrix[i, j] = form1.sourseData[i];
                    }
                    else {
                        int random = functions.getRandom(0, 100);
                        int plasMinus = functions.getRandom(0, 2);

                        if (plasMinus == 1) {
                            matrix[i, j] = form1.sourseData[i] + (form1.sourseData[i] / 100d * random); //увеличения числа на random
                            //matrix[i, j] = sourseData[i] + (sourseData[i]+(sourseData[i]/100d*random));
                        }
                        else {
                            matrix[i, j] = form1.sourseData[i] - (form1.sourseData[i] / 100d * random); //уменьшения числа на random
                           //matrix[i, j] = sourseData[i] - (sourseData[i]-(sourseData[i]/100d*random));
                        }
                    }

                }
            }

            return matrix;
        }
    }
}
