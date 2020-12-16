using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Költségvetés_Elemző
{
    public partial class Form1 : Form
    {

        BindingList<Szamlamozgas> _szamlamozgas = new BindingList<Szamlamozgas>();
       

        public Form1()
        {
            InitializeComponent();
            LoadCash();
            LoadSzamlatortenet();
            Osszegzes();


        }

        private void LoadCash()
        {
            Random rnd = new Random();
            double kezdo = rnd.Next(200000, 400000);
            txbKezdo.Text = kezdo.ToString();
        }

        private void Osszegzes()
        {
            
                
            double osszmozg = (from Szamlamozgas in _szamlamozgas
                       select Szamlamozgas.összeg).Sum();
            label1.Text = osszmozg.ToString();

            label4.Text = ((from Szamlamozgas in _szamlamozgas
                            where Szamlamozgas.összeg<0
                            select Szamlamozgas.összeg).Sum()).ToString();

            label6.Text = ((from Szamlamozgas in _szamlamozgas
                            where Szamlamozgas.összeg > 0
                            select Szamlamozgas.összeg).Sum()).ToString();

            double v = (double.Parse(txbKezdo.Text) + osszmozg);
            textBox2.Text = v.ToString();
        }

        private void LoadSzamlatortenet()
        {
            using (StreamReader sr = new StreamReader("szamlatortenet.csv", Encoding.UTF8))
            {
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    string[] line = sr.ReadLine().Split(',');
                    Szamlamozgas szm = new Szamlamozgas();
                    szm.könyvelés_dátuma = DateTime.Parse(line[0]);
                    szm.tranzakció_azonosító = line[1];
                    szm.típus = line[2];
                    szm.könyvelési_számla = line[3];
                    szm.könyvelési_számla_elnevezése = line[4];
                    szm.partner_számla_elnevezése = line[5];
                    szm.összeg = double.Parse(line[6]);
                    szm.deviza = line[7];
                    szm.besorolás = false;
                    _szamlamozgas.Add(szm);
                }
            }

            
        }

        private void btnAdat_Click(object sender, EventArgs e)
        {
            var _szmlmzg =( from Szamlamozgas in _szamlamozgas
                       select new
                       {
                           Dátum = Szamlamozgas.könyvelés_dátuma,
                           Tranzakció_azonosító = Szamlamozgas.tranzakció_azonosító,
                           Típus = Szamlamozgas.típus,
                           Partnerszamlaneve = Szamlamozgas.partner_számla_elnevezése,
                           Összeg = Szamlamozgas.összeg,
                           Besorolás = Szamlamozgas.besorolás


                       }).ToList();

            dataGridView1.DataSource = _szmlmzg;
            chart1.DataSource = _szamlamozgas;

        }

        private void btn_Diagram_Click(object sender, EventArgs e)
        {
            var series = chart1.Series[0];
            series.XValueMember = "könyvelés_dátuma";
            series.XValueType = ChartValueType.Date;
            series.YValueMembers = "összeg";

            var legend = chart1.Legends[0];
            legend.Enabled = false;

            var chartArea = chart1.ChartAreas[0];           
            chartArea.AxisY.MajorGrid.Enabled = false;
            chartArea.AxisY.IsStartedFromZero = false;

        }

       
        
    }
}
