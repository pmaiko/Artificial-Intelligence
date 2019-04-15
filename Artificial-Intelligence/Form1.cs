using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Artificial_Intelligence
{
    public partial class Form1 : Form {
        Form2 form2 = new Form2();
        DrawGraph drawGraph = new DrawGraph();
        public double ro = 0.5d;
        public double delta = 20;
        public int verticalLength = 40;
        public double[] sourseData = {30.7, 39.9, 42.6, 42.5, 49.9, 43.5, 43.4, 43.9, 44.4, 48.6, 43.6, 42.9, 44.4, 42.8, 51.9, 43.5, 44.4, 43.5, 43.8, 52.8, 43.4, 42.7, 41.8, 42.5, 46.4, 42.5, 41.6, 42.8, 44.4, 44.5, 43.7, 42.9, 43.5, 54.9, 54.3, 56.7, 64.6, 70.4, 69.4, 31.9, 31.9, 35.6, 42.8, 41.9, 40.9, 41.4, 54.7, 55.1, 50.9, 20.2, 52.8, 22.1, 30.7, 70.4, 38.5, 31.9, 44.5, 13.8, 54.9, 23, 47.8, 47.8, 49.8, 47.6, 47.9, 45.9, 46.7, 46.5, 47.5, 47.7, 45.6, 50.4, 48.5, 45.9, 47.9, 47.6, 48.9, 47.8, 50.5, 50.7, 50.9, 49.9, 47.5, 45.7, 45.9, 46.4, 48.6, 48.5, 46.6, 45.7, 168.7, 92.9, 193.4, 0.871, 50, 16.05, 16.05, 15.94, 16.01, 6943, 7100, 7044, 7029, 334, 1584, 22, 19, 16, 24, 14, 7, 2.42, 2.11, 1.79, 2.67, 1.5, 0.76, 334, 1584, 0.21, 63.1, 31.9, 31.2, 158.9, 161.9, 101.7, 97.3, 0.84, 330.8, 338.3, 330, 1579.3, 1600, 1600, 0.20946, 65.95, 33.15, 522.43};
        double[] meanValues; 
        double[] limitUp;
        double[] limitDown;
        double[] isTrue;
        public double[,] classA;
        public double[,] classB;
        public double[,] classC;
        double[,] binA;
        double[,] binB;
        double[,] binC;
        double[] meanBinA;
        double[] EtalVecBinA;
        double[] meanBinB;
        double[] EtalVecBinB;
        double[] meanBinC;
        double[] EtalVecBinC;

        double[] dck_xk; // к своим 
        double[] dck_xn; // к чужим 
        double[] k1;
        double[] k2;

        double[] E;
      


        CreateClassA createClassA = new CreateClassA();
        CreateClassB createClassB = new CreateClassB();
        CreateClassC createClassC = new CreateClassC();
        Functions functions = new Functions(20d);
        UsingFiles usingFiles = new UsingFiles();

        public Form1() {
            InitializeComponent();
        }

        public void consondeb(double[] value) {
            NativeMethods.AllocConsole();
            //IntPtr stdHandle = NativeMethods.GetStdHandle(NativeMethods.STD_OUTPUT_HANDLE);
            for (int i = 0; i<value.Length; i++) {
                Console.Write(Convert.ToString(value[i]));
            }           
        }

        public void callCreateWriteClasses() {
            classA = createClassA.create(); //Создания классу А с помощью исходных данных
            classB = createClassB.create(classA); //Создания классу B с помощью классу A
            classC = createClassC.create(classA); //Создания классу C с помощью классу C
            usingFiles.writeFile(classA, "classA");
            usingFiles.writeFile(classB, "classB");
            usingFiles.writeFile(classC, "classC");

            mainMethod();
        }

        public void mainMethod() {
            meanValues = functions.findMean(classA); //Поиск среднего значения

            limitUp = functions.findLimit(meanValues, "Up"); //верехний допуск по среднем значении classA
            limitDown = functions.findLimit(meanValues, "Down"); //нижний допуск по среднем значении classA

            binA = functions.BinMatrix(classA, limitDown, limitUp); 
            binB = functions.BinMatrix(classB, limitDown, limitUp); 
            binC = functions.BinMatrix(classC, limitDown, limitUp);

            meanBinA = functions.findMean(binA); //Поиск среднего значения bin A 0,54 - 0,6 - 0,3
            EtalVecBinA = functions.FindEtalVecBin(meanBinA); //Поиск еталоного вектора meanBinA 1, 0, 1
            meanBinB = functions.findMean(binB); //Поиск среднего значения bin B 0,54 - 0,6 - 0,3
            EtalVecBinB = functions.FindEtalVecBin(meanBinB); //Поиск еталоного вектора meanBinB 1, 0, 1
            meanBinC = functions.findMean(binC); //Поиск среднего значения bin C 0,54 - 0,6 - 0,3
            EtalVecBinC = functions.FindEtalVecBin(meanBinC); //Поиск еталоного вектора meanBinC 1, 0, 1

            double[] distances = new double[3];
            double[] minDistances = new double[3];
            int index;
            double[] indexMinValues = new double[3];
            k1 = new double[sourseData.Length];
            k2 = new double[sourseData.Length];
            E = new double[sourseData.Length];

            for (int k = 0; k < 3; k++) {

                switch (k) {
                    case 0:
                            distances[0] = functions.findCountXOR(EtalVecBinA, EtalVecBinA); //0 9 0
                            distances[1] = functions.findCountXOR(EtalVecBinA, EtalVecBinB);
                            distances[2] = functions.findCountXOR(EtalVecBinA, EtalVecBinC);
                            break;
                        case 1:
                            distances[0] = functions.findCountXOR(EtalVecBinB, EtalVecBinA); //9 0 9
                            distances[1] = functions.findCountXOR(EtalVecBinB, EtalVecBinB);
                            distances[2] = functions.findCountXOR(EtalVecBinB, EtalVecBinC);
                            break;
                        case 2:
                            distances[0] = functions.findCountXOR(EtalVecBinC, EtalVecBinA); //0 9 0
                            distances[1] = functions.findCountXOR(EtalVecBinC, EtalVecBinB);
                            distances[2] = functions.findCountXOR(EtalVecBinC, EtalVecBinC);
                            break;    
                }

                distances[k] = double.PositiveInfinity; // + бесконечность EtalVecBinA, EtalVecBinA : EtalVecBinB, EtalVecBinB : EtalVecBinC, EtalVecBinC 
                minDistances[k] = functions.FindMinCount(distances); // поиск минимально значения 
                index = Array.IndexOf(distances, minDistances[k]); //ижем индекс минимального числа в масиве distances
                indexMinValues[k] = index; // записуем в масив минимальные значения

                switch (k) {
                    case 0:
                        dck_xk = functions.findCountXORforEachLinesMatrix(EtalVecBinA, binA); // поиск несовпадений для каждой строки матрицы  74	37	41	32	41	38	45	39	40	32	34	37	37	35	30	48	37	45	40	37	45
                        switch (index) {
                            case 0:
                                dck_xn = functions.findCountXORforEachLinesMatrix(EtalVecBinA, binA);
                                break;
                            case 1:
                                dck_xn = functions.findCountXORforEachLinesMatrix(EtalVecBinA, binB);
                                break;
                            case 2:
                                dck_xn = functions.findCountXORforEachLinesMatrix(EtalVecBinA, binC); // 71	37	41	33	41	38	44	39	38	32	34	36	37
                                break;
                        }

                        break;
                    case 1:
                        dck_xk = functions.findCountXORforEachLinesMatrix(EtalVecBinB, binB);
                        switch (index) {
                            case 0:
                                dck_xn = functions.findCountXORforEachLinesMatrix(EtalVecBinB, binA);
                                break;
                            case 1:
                                dck_xn = functions.findCountXORforEachLinesMatrix(EtalVecBinB, binB);
                                break;
                            case 2:
                                dck_xn = functions.findCountXORforEachLinesMatrix(EtalVecBinB, binC);
                                break;
                        }
                        break;

                    case 2:
                        dck_xk = functions.findCountXORforEachLinesMatrix(EtalVecBinC, binC);
                        switch (index) {
                            case 0:
                                dck_xn = functions.findCountXORforEachLinesMatrix(EtalVecBinC, binA);
                                break;
                            case 1:
                                dck_xn = functions.findCountXORforEachLinesMatrix(EtalVecBinC, binB);
                                break;
                            case 2:
                                dck_xn = functions.findCountXORforEachLinesMatrix(EtalVecBinC, binC);
                                break;
                        }
                        break;
                }

                for (int i = 0; i < 40; i++) {
                    double z = 0;
                    for (int j = 0; j < dck_xk.Length; j++) {
                        if (dck_xk[j] <= i) {
                            z++;
                        }
                    }

                    k1[i] = z / 40;
                }

                for (int i = 0; i < 40; i++) {
                    double z = 0;
                    for (int j = 0; j < dck_xn.Length; j++) {
                        if (dck_xn[j] <= i) {
                            z++;
                        }
                    }

                    k2[i] = z / 40;
                }

                for (int i = 0; i < 40; i++) {
                    double[] pt = new double[40];
                    double[] pf = new double[40];
                    double[] sump = new double[40];
                    pt[i] = ((k1[i] + 1 - k2[i]) / 2);
                    pf[i] = ((1 - k1[i] + k2[i]) / 2);
                    sump[i] = pt[i] - pf[i];
                    E[i] = Math.Log((pt[i] + 0.005) / (pf[i] + 0.005), 2.0) * (sump[i]);
                }

                //Вывод графиков
                switch (k)
                    {
                        case 0:
                            drawGraph.GetGraph(drawGraph.chart1, E, "Кульбак", 1, k1, k2);
                            
                            break;
                        case 1:
                            drawGraph.GetGraph(drawGraph.chart2, E, "Кульбак", 2, k1, k2);
                            
                            break;
                        case 2:
                            drawGraph.GetGraph(drawGraph.chart3, E, "Кульбак", 3, k1, k2);
                            
                            break;
                    }

            }

            form2.Output(k1);
            drawGraph.Show();



            //Вывод любои инфы
            //form2.Output(meanBinA);
            //form2.Output2(meanValues);
            //form2.Output3(limitUp);
            //form2.Output4(limitDown);
            //form2.Output5(binA);
            //form2.Output6(isTrue);
            form2.Show();
        }

        

        private void Form1_Load(object sender, EventArgs e) {

            if (File.Exists(usingFiles.url+"classA"+usingFiles.format) && File.Exists(usingFiles.url+"classB"+usingFiles.format) && File.Exists(usingFiles.url+"classC"+usingFiles.format)) {
                classA = usingFiles.readFile("classA"); //читаем даные с файла classA
                classB = usingFiles.readFile("classB"); //читаем даные с файла classB
                classC = usingFiles.readFile("classC"); //читаем даные с файла classC

                mainMethod();

                
            }

            else {
                callCreateWriteClasses();
            }
        }//pause

        private void button1_Click(object sender, EventArgs e) {
            callCreateWriteClasses();
        }
    }

    public partial class NativeMethods
    {
        public static Int32 STD_OUTPUT_HANDLE = -11;

        [System.Runtime.InteropServices.DllImportAttribute("kernel32.dll", EntryPoint = "GetStdHandle")]
        public static extern System.IntPtr GetStdHandle(Int32 nStdHandle);

        [System.Runtime.InteropServices.DllImportAttribute("kernel32.dll", EntryPoint = "AllocConsole")]

        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]

        public static extern bool AllocConsole();
    }
}
