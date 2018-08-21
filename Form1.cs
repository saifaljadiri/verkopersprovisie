using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace verkopersprovisie
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            double eenhedenAfgelopenMaand = Convert.ToDouble(textBox1.Text);
            double omzetAfgelopenMaand = Convert.ToDouble(textBox3.Text);
            double eenhedenAfgelopen12Maanden = Convert.ToDouble(textBox4.Text);

            double kostenPerVerkochteEenheid = 4;
            double winstomzet = 0.03;

            double omzetProvisieKosten = omzetAfgelopenMaand * winstomzet + eenhedenAfgelopenMaand * kostenPerVerkochteEenheid;

            double percentageVerhogingProvisie = 0;
            if (eenhedenAfgelopen12Maanden >= 0 && eenhedenAfgelopen12Maanden <= 10000)
            {
                percentageVerhogingProvisie = berekenProvisieVerhoging(eenhedenAfgelopen12Maanden, 0.01);
            }
            else if (eenhedenAfgelopen12Maanden >= 10000 && eenhedenAfgelopen12Maanden <= 20000)
            {
                percentageVerhogingProvisie = berekenProvisieVerhoging(10000, 0.01);
                eenhedenAfgelopen12Maanden -= 10000;
                if (eenhedenAfgelopen12Maanden >= 1000)
                {
                    percentageVerhogingProvisie += berekenProvisieVerhoging(eenhedenAfgelopen12Maanden, 0.015);
                }
            }
            else if (eenhedenAfgelopen12Maanden >= 20000)
            {
                percentageVerhogingProvisie = berekenProvisieVerhoging(10000, 0.01);
                eenhedenAfgelopen12Maanden -= 10000;
                if (eenhedenAfgelopen12Maanden >= 1000)
                {
                    percentageVerhogingProvisie += berekenProvisieVerhoging(eenhedenAfgelopen12Maanden, 0.015);
                }
                eenhedenAfgelopen12Maanden -= 10000;
                if (eenhedenAfgelopen12Maanden >= 1000)
                {
                    percentageVerhogingProvisie += berekenProvisieVerhoging(eenhedenAfgelopen12Maanden, 0.02);
                }
            }

            // bewaken dat het totaalpercentage niet hoger wordt dan 50%.
            if (percentageVerhogingProvisie > 0.5)
            {
                percentageVerhogingProvisie = 0.5;
            }

            // omzetProvisieKosten en verhoging bij elkaar optellen en resultaat in textbox 2 plaatsen.
            textBox2.Text = Convert.ToString(omzetProvisieKosten * (percentageVerhogingProvisie + 1));
        }

        // berekenen van de provisieverhoging
        private double berekenProvisieVerhoging(double eenheden, double percentageVerhoging)
        {
            double totaalPercentage = 0;
            totaalPercentage = Math.Floor(eenheden / 1000);
            totaalPercentage *= percentageVerhoging;
            return totaalPercentage;
        }

        private void button2_Click(object sender, EventArgs e)
        {
        textBox1.Clear();
        textBox2.Clear();
        }
    }
}
