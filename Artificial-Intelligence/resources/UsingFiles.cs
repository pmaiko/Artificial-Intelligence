using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Collections;

namespace Artificial_Intelligence {
    class UsingFiles {
        public string url;
        public string format;

        public UsingFiles() {
            this.url = @"..\..\resources\assets\";
            this.format = ".array";
        }

        public void writeFile(double[,] matrix, string fileWriteName) {
            StreamWriter sw = new StreamWriter(url+fileWriteName+format);

            for (int j = 0; j < matrix.GetLength(1); j++) {//40
                for (int i = 0; i < matrix.GetLength(0); i++) { //124
                    sw.Write(Convert.ToString(matrix[i, j] + " "));
                    
                    //sw.Write(Convert.ToString(classA[i, j] + " ").Replace(",","."));
                    //sw.WriteLine(String.Join(" ", classA[i,j]));
                    //File.WriteAllText(@"..\..\assets\ClassA.txt", string.Join(" ", classA[i,j]));
                }
                sw.Write("\r\n");
            }
            sw.Close();
        }

        public double[,] readFile(string fileWriteName) {
            Form1 form1 = new Form1();
            double [,] matrix = new double[form1.sourseData.Length, form1.verticalLength];
            StreamReader sr = new StreamReader(url+fileWriteName+format);

            for (int j = 0; j < matrix.GetLength(1); j++) {//40
                string temp = sr.ReadLine();
                string[] line = temp.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < matrix.GetLength(0); i++) { //124
                    matrix[i, j] = Double.Parse(line[i]);

                }
            }
            return matrix;
        }
    }
}
