﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Artificial_Intelligence
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        //public void output(double[] massiv, bitmap input)
        //{
        //    double temp;
        //    datagridview1.rowcount = input.height;
        //    datagridview1.columncount = input.width;
        //    for (int i = 0; i < input.width; i++)
        //    {
        //        if (double.isnan(massiv[i]))
        //        {
        //            temp = 0.0;
        //            datagridview1.rows[0].cells[i].value = temp.tostring() + "";
        //        }
        //        else
        //        {
        //            datagridview1.rows[0].cells[i].value = massiv[i].tostring() + "";
        //        }

        //    }
        //}
        public void Output(double[] Massiv)
        {
            double temp;
            dataGridView1.RowCount = Massiv.Length;
            dataGridView1.ColumnCount = Massiv.Length;
            for (int i = 0; i < Massiv.Length; i++)
            {
                if (Double.IsNaN(Massiv[i]))
                {
                    temp = 0.0;
                    dataGridView1.Rows[0].Cells[i].Value = temp.ToString() + "";
                }
                else
                {
                    dataGridView1.Rows[0].Cells[i].Value = Massiv[i].ToString() + "";
                }

            }
        }

        public void Output(double[,] Massiv)
        {
            dataGridView1.RowCount = Massiv.GetLength(1); //height
            dataGridView1.ColumnCount = Massiv.GetLength(0); //width
            for (int i = 0; i < Massiv.GetLength(0); i++)
            {
                for (int j = 0; j < Massiv.GetLength(1); j++)
                {
                    dataGridView1.Rows[j].Cells[i].Value = Massiv[i, j].ToString() + " ";
                }
            }
        }

        //public void Output(int[,] Massiv, Bitmap input)
        //{
        //    dataGridView1.RowCount = input.Height;
        //    dataGridView1.ColumnCount = input.Width;
        //    for (int i = 0; i < input.Width; i++)
        //    {
        //        for (int j = 0; j < input.Height; j++)
        //        {
        //            dataGridView1.Rows[j].Cells[i].Value = Massiv[i, j].ToString() + " ";
        //        }
        //    }
        //}

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
