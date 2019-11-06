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
using System.Threading;
using System.Threading.Tasks;

namespace Artificial_Intelligence
{
    public partial class Form1 : Form {
        Form2 form2 = new Form2();
        public double ro = 0.5d;
        public int verticalLength = 40;
        public double[] sourseData = {30.7, 39.9, 42.6, 42.5, 49.9, 43.5, 43.4, 43.9, 44.4, 48.6, 43.6, 42.9, 44.4, 42.8, 51.9, 43.5, 44.4, 43.5, 43.8, 52.8, 43.4, 42.7, 41.8, 42.5, 46.4, 42.5, 41.6, 42.8, 44.4, 44.5, 43.7, 42.9, 43.5, 54.9, 54.3, 56.7, 64.6, 70.4, 69.4, 31.9, 31.9, 35.6, 42.8, 41.9, 40.9, 41.4, 54.7, 55.1, 50.9, 20.2, 52.8, 22.1, 30.7, 70.4, 38.5, 31.9, 44.5, 13.8, 54.9, 23, 47.8, 47.8, 49.8, 47.6, 47.9, 45.9, 46.7, 46.5, 47.5, 47.7, 45.6, 50.4, 48.5, 45.9, 47.9, 47.6, 48.9, 47.8, 50.5, 50.7, 50.9, 49.9, 47.5, 45.7, 45.9, 46.4, 48.6, 48.5, 46.6, 45.7, 168.7, 92.9, 193.4, 0.871, 50, 16.05, 16.05, 15.94, 16.01, 6943, 7100, 7044, 7029, 334, 1584, 22, 19, 16, 24, 14, 7, 2.42, 2.11, 1.79, 2.67, 1.5, 0.76, 334, 1584, 0.21, 63.1, 31.9, 31.2, 158.9, 161.9, 101.7, 97.3, 0.84, 330.8, 338.3, 330, 1579.3, 1600, 1600, 0.20946, 65.95, 33.15, 522.43, 188.7, 80, 62, 83, 96, 63, 68, 76.3, 96.5, 55, 64, 74.3, 68, 74, 76, 68, 188.7, 72.5, 75, 72, 66, 67.3, 35, 33, 46, 40, 10, 26, 30, 34, 66.7};
        double[] isTrue;
        public static double Length;
        public double[,] classA;
        public double[,] classB;
        public double[,] classC;
        public static int countClasses = 3;

        public static bool parallel = false;
        public static bool consistent = false;
      


        CreateClassA createClassA = new CreateClassA();
        CreateClassB createClassB = new CreateClassB();
        CreateClassC createClassC = new CreateClassC();
        Functions functions = new Functions();
        UsingFiles usingFiles = new UsingFiles();

        public Form1() {
            InitializeComponent();
            Length = sourseData.Length;
        }

        public void consondeb() {
            NativeMethod.AllocConsole();
            //IntPtr stdHandle = NativeMethods.GetStdHandle(NativeMethods.STD_OUTPUT_HANDLE);
            //for (int i = 0; i<value.Length; i++) {
            //    Console.Write(Convert.ToString(value[i]));
            //}           
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
            
            //consondeb();
           
       


            //Вывод любои инфы
           // form2.Output(algMachine.E_C);
            //form2.Output2(algMachine.k1_C);
            //form2.Output3(algMachine.k2_C);
            //form2.Output4(limitDown);
            //form2.Output5(binA);
            //form2.Output6(isTrue);
            //form2.Show();
        }

        async void lox() {
            string zz = await Task.Run(() => {
                 Thread.Sleep(3000);
                return "adssadas";
                
            });

            textBox1.Text = zz;

            

        }

        

        private void Form1_Load(object sender, EventArgs e) {
            lox();
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

        private void button2_Click(object sender, EventArgs e) {
            consistent = true;
            parallel = false;

            DrawGraph drawGraph = new DrawGraph();
            drawGraph.Text = "Послідовна оптимізація КД";

            OptimizationKD optimizationKD = new OptimizationKD(classA, classB, classC);
            optimizationKD.main("consistent");

            drawGraph.chart2.Series.Clear();
            drawGraph.chart3.Series.Clear();
            drawGraph.chart4.Series.Clear();

            drawGraph.GetGraph(drawGraph.chart1, optimizationKD.E_consistent_all, "Послідовна оптимізація КД", 4);
            drawGraph.Show();

            form2.Output(optimizationKD.E_consistent_all);
                form2.Show();

            
        }

        private void button3_Click(object sender, EventArgs e) {
            parallel = true;
            consistent = false;

            DrawGraph drawGraph = new DrawGraph();
            drawGraph.Text = "Паралельна оптимізація КД";

            OptimizationKD optimizationKD = new OptimizationKD(classA, classB, classC);
            optimizationKD.main("parallel");

            drawGraph.chart2.Series.Clear();
            drawGraph.chart3.Series.Clear();
            drawGraph.chart4.Series.Clear();
            drawGraph.GetGraph(drawGraph.chart1, optimizationKD.E, "Паралельна оптимізація КД", 4, optimizationKD.k1, optimizationKD.k2);
            drawGraph.Show();

           

            
        }

        private void button4_Click(object sender, EventArgs e) {
            if (parallel == false && consistent == false) {
                DrawGraph drawGraph = new DrawGraph();
                drawGraph.Text = "Графіки залежності критерію Кульбака від радіусів контейнерів класів розпізнавання";

                AlgMachine algMachine = new AlgMachine(classA,classB,classC);
                algMachine.alg(20d);
                algMachine.algFind_K_KFE();

                drawGraph.GetGraph(drawGraph.chart1, algMachine.E_A, "Кульбак", 1, algMachine.k1_A, algMachine.k2_A);
                drawGraph.GetGraph(drawGraph.chart2, algMachine.E_B, "Кульбак", 2, algMachine.k1_B, algMachine.k2_B);
                drawGraph.GetGraph(drawGraph.chart3, algMachine.E_C, "Кульбак", 3, algMachine.k1_C, algMachine.k2_C);
                drawGraph.Show();

            } else if (parallel == true) {
                DrawGraph drawGraph = new DrawGraph();
                drawGraph.Text = "Графіки одержані при оптимальному значенні дельтая";

                AlgMachine algMachine = new AlgMachine(classA,classB,classC);
                algMachine.alg(DrawGraph.op_delta);
                algMachine.algFind_K_KFE();

                drawGraph.GetGraph(drawGraph.chart1, algMachine.E_A, "Кульбак", 1, algMachine.k1_A, algMachine.k2_A);
                drawGraph.GetGraph(drawGraph.chart2, algMachine.E_B, "Кульбак", 2, algMachine.k1_B, algMachine.k2_B);
                drawGraph.GetGraph(drawGraph.chart3, algMachine.E_C, "Кульбак", 3, algMachine.k1_C, algMachine.k2_C);
                drawGraph.Show();

                parallel = false;

            } else if (consistent == true) {
                DrawGraph drawGraph = new DrawGraph();
                drawGraph.Text = "Графіки після послідовної оптимізації";

                AlgMachine algMachine = new AlgMachine(classA,classB,classC);
                algMachine.alg("consistent");
                algMachine.algFind_K_KFE();

                drawGraph.GetGraph(drawGraph.chart1, algMachine.E_A, "Кульбак", 1, algMachine.k1_A, algMachine.k2_A);
                drawGraph.GetGraph(drawGraph.chart2, algMachine.E_B, "Кульбак", 2, algMachine.k1_B, algMachine.k2_B);
                drawGraph.GetGraph(drawGraph.chart3, algMachine.E_C, "Кульбак", 3, algMachine.k1_C, algMachine.k2_C);

                

                drawGraph.Show();

                consistent = false;

            }

        }

        private void button5_Click(object sender, EventArgs e) {
            DrawGraph drawGraph = new DrawGraph();
            drawGraph.Text = "Послідовна оптимізація КД - 2";

            OptimizationKD optimizationKD = new OptimizationKD(classA, classB, classC);
            optimizationKD.main("consistent-2");

            drawGraph.chart2.Series.Clear();
            drawGraph.chart3.Series.Clear();
            drawGraph.chart4.Series.Clear();

            //drawGraph.GetGraph(drawGraph.chart1, optimizationKD.E_optMax, optimizationKD.E_optMaxIndex,  "Послідовна оптимізація КД", 4);
            //drawGraph.GetGraph(drawGraph.chart1, optimizationKD.E_optMaxA, optimizationKD.E_optMaxB, optimizationKD.E_optMaxC, optimizationKD.E_optMaxIndex,  "Послідовна оптимізація КД", 4);
            drawGraph.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e) {
        }
    }

    public partial class NativeMethod
    {
        public static Int32 STD_OUTPUT_HANDLE = -11;

        [System.Runtime.InteropServices.DllImportAttribute("kernel32.dll", EntryPoint = "GetStdHandle")]
        public static extern System.IntPtr GetStdHandle(Int32 nStdHandle);

        [System.Runtime.InteropServices.DllImportAttribute("kernel32.dll", EntryPoint = "AllocConsole")]

        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]

        public static extern bool AllocConsole();
    }
}
