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

namespace Költségvetés_Elemző
{
    public partial class Form1 : Form
    {

        List<Szamlamozgas> _szamlamozgas = new List<Szamlamozgas>();
        

        public Form1()
        {
            InitializeComponent();
                       

            LoadSzamlatortenet();

            
            dataGridView1.DataSource = _szamlamozgas;
           


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
                    szm.összeg =   double.Parse(line[6]);
                    szm.deviza = line[7];

                    _szamlamozgas.Add(szm);

                }
            }
        }


    }



}
