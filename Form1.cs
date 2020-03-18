using System;
using Svg;
using System.Collections.Generic;
using System.ComponentModel;
using Charts;
using SVGObjects;
using System.Data;
using System.Xml;
using SvgLib;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Collections.ObjectModel;

namespace SVGExercise
{
    /// Author: Brandon Guerin
    /// Project: SVG Exercise
    /// Purpose : Display Vehicle sales in a graph or chart rendered in an SVG format.
    /// 
    public partial class Form1 : Form
    {
        public string fileName = "";
        public Svg.SvgDocument svgDoc = new Svg.SvgDocument();
        
        //public SvgLib
//barGraph, mktShareChart;
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Variable names for data tables
        /// </summary>
        public string typeTitle = "Type";
        public string yearName2018 = "2018", yearName2019 = "2019";
        public string tableName = "Vehicles Sold By Year", mktShare = "Vehicle Market Share for 2019";
        public BindingList<String> vehicleType = new BindingList<string>();
        public BindingList<int> vehicleYear2018Sales = new BindingList<int>();
        public BindingList<int> vehicleYear2019Sales = new BindingList<int>();

        //public int[] vehicleSales2018, vehicleSales2019;
        
        /// <summary>
        /// Open a picture to display in picturebox
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       
        private void OpenFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

            fileName = openFileDialog1.FileName;

            //svgPictureBox.Ima

            //  svgPictureBox.ImageLocation = openFileDialog1.FileName;


            //  widthTextBox.Text = svgPictureBox.Width.ToString();
            //  heightTextBox.Text = svgPictureBox.Height.ToString();

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {

            
            DialogResult dialogResult = openFileDialog1.ShowDialog();
            if (dialogResult.Equals(DialogResult.OK))
            {

                StreamReader fileIN;
                //  fileName = openFileDialog1.FileName;
                fileIN = new StreamReader(fileName);

                string tableTitle = fileIN.ReadLine();
                string[] values;

                while (fileIN.Peek() != -1)
                {
                    values = fileIN.ReadLine().Split(',');
                    vehicleType.Add(values[0]);
                    vehicleYear2018Sales.Add(Int32.Parse(values[1]));
                    vehicleYear2019Sales.Add(Int32.Parse(values[2]));

                }
                fileIN.Close();

            }
            // Plotting the Data in a graph

            //salesBarGraph.Titles.Add(tableName);

            //salesBarGraph.Series.Add("2019");
            //salesBarGraph.Series[0].Points.DataBindY(vehicleYear2018Sales);
            //salesBarGraph.Series[0].LegendText = "2018";
            //salesBarGraph.Series[1].Points.DataBindY(vehicleYear2019Sales);
            //for (int i = 0; i < vehicleType.Count; i++)
            //    salesBarGraph.Series[0].Points[i].AxisLabel = vehicleType[i];
            //salesBarGraph.SaveImage("VehicleSales.emf", ChartImageFormat.Emf);
            //mktShareChart.Titles.Add("Market Share of Vehicles 2019");
            //mktShareChart.Series[0].Points.DataBindY(vehicleYear2019Sales);
            //mktShareChart.Series[0].LegendText = "2019";
            //for (int i=0; i<vehicleType.Count; i++)
            //mktShareChart.Series[0].Points[i].AxisLabel = vehicleType[i];
            //mktShareChart.SaveImage("Market Share of 2019.emf", ChartImageFormat.Emf);



            //Convert Charts To SVG

            DataTable table1 = new DataTable("Vehicle Sales 2018 & 2019"), table2 = new DataTable("2019 Market Share");

            //table1 = ConvertToDataTable(vehicleYear2018Sales);
            //table2 = ConvertToDataTable(vehicleYear2019Sales);
            table1.Columns.Add("Type", typeof(string));
            table1.Columns.Add("Quantity", typeof(double));
            table2.Columns.Add("Type", typeof(string));
            table2.Columns.Add("Quantity", typeof(double));

            for(int i = 0; i<vehicleYear2018Sales.Count;i++)
            {
                table1.Rows.Add("2018 " + vehicleType[i], vehicleYear2018Sales[i].ToString());
                table1.Rows.Add("2019 " + vehicleType[i], vehicleYear2019Sales[i].ToString());
                table2.Rows.Add(vehicleType[i], vehicleYear2019Sales[i].ToString());
                
            }

            SVGObject svg = new SVGObject();
            HistogramChart his = new HistogramChart(300, 300,table1.Rows.Count);
           
            //his.
          // Credits to Gerard Viader 
            XmlDocument xml = his.GenerateChart(table1,table1.Columns["Type"].ToString(),table1.Columns["Quantity"].ToString());
            xml.Save("2018 & 2019 Vehicle Sales.svg");

            PieChart pie = new PieChart(500, 500, table2.Rows.Count);

            xml = pie.GenerateChart(table2, table2.Columns["Type"].ToString(), table2.Columns["Quantity"].ToString());
            xml.Save("2019 Market Share.svg");

            //xml = his.GenerateChart(table1, table1.Columns["Type"].ToString(), table1.Columns["2019"].ToString());
            //xml.Save("2019 Market Share.svg");
            Application.Exit();
        }

        //static DataTable ConvertListToDataTable(List<string[]> list)
        //{
        //    // New table.
        //    DataTable table = new DataTable();

        //    // Get max columns.
        //    int columns = 0;
        //    foreach (var array in list)
        //    {
        //        if (array.Length > columns)
        //        {
        //            columns = array.Length;
        //        }
        //    }

        //    // Add columns.
        //    for (int i = 0; i < columns; i++)
        //    {
        //        table.Columns.Add();
        //    }

        //    // Add rows.
        //    foreach (var array in list)
        //    {
        //        table.Rows.Add(array);
        //    }

        //    return table;
        //}

       

      
    }
}
