//Gemaakt door Michel Mertens Januari 2017
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
using Newtonsoft.Json;

namespace HotelSysteem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //event handeler voor paint
            Paint += new PaintEventHandler(Circle_Paint);
            //geef details wee in de listview
            lv_boeking.View = View.Details;
            //pas de groote aan van het form
            Size = new Size(548, 428);
        }

        enum Geslacht
        {
            Man,
            Vrouw,
            Onbekend
        };

        enum Gasten1
        {
            Een = 1,
            Twee = 2
        };

        enum Gasten2
        {
            Een = 1,
            Twee = 2,
            Drie = 3,
            Vier = 4
        };

        private void Form1_Load(object sender, EventArgs e)
        {
            Fill_combo();
            //vull de combo boxen met enum Geslacht
            cb_geslacht1.DataSource = Enum.GetValues(typeof(Geslacht));
            cb_geslacht2.DataSource = Enum.GetValues(typeof(Geslacht));
            cb_geslacht3.DataSource = Enum.GetValues(typeof(Geslacht));
            cb_geslacht4.DataSource = Enum.GetValues(typeof(Geslacht));
            //geef de minimum datum aan voor de picker
            dtp_datum.MinDate = DateTime.Now;
        }

        private void Export()
        {
            //maak een streamwriter met als pad directory overzicht boekingen.txt
            StreamWriter sw = new StreamWriter(@"Overzicht Boekingen.txt");

            //loop door de listview items heen
            foreach (ListViewItem listItem in lv_boeking.Items)
            {
                sw.WriteLine("Kamer ID: " + listItem.Text + " - Klant naam: " + listItem.SubItems[1].Text + " - Gender: " + listItem.SubItems[2].Text + " - Adres: " + listItem.SubItems[3].Text + " - Leeftijd " + listItem.SubItems[4].Text + " - Eten: " + listItem.SubItems[5].Text + " - Datum " + listItem.SubItems[6].Text);                
            }
            MessageBox.Show("Het overzicht is succesvol opgeslagen.", "Grand Hotel D'n Gaarkeuken", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            sw.Close();
        }

        private void Fill_combo()
        {
            for (int i = 0; i < 313; i++)
            {
                if (i > 0)
                {
                    //vull de combobox met de cijfers
                    cb_kamer.Items.Add(i);
                }
                if (i == 13)
                {
                    //verwijdert het numemr 13
                    cb_kamer.Items.Remove(13);
                }
            }
        }

        private void bezet()
        {
            //Kijk of de kamer die dag al bezet is
            foreach (Boeking boeking in Boeking.boekingen)
            {
                //ga verder als het kamer id in het object boeking overeen komt met de value uit de combobox
                if (boeking.kamerId == Convert.ToInt32(cb_kamer.Text))
                {
                    //ga verder als de boeking datum van die kamer overeenkomet met de datum uit de date picker
                    if (boeking.boekingDatum == dtp_datum.Value.Date.ToString())
                    {
                        //maak de kamer bezet
                        Boeking.bezet = true;
                    }
                }
            }
        }

        private void Toevoegen()
        {
            bezet();
            try
            {
                if (Boeking.bezet == false)
                {
                    //voeg klant toe aan boeking
                    Boeking boeking = new Boeking(Convert.ToInt32(cb_kamer.Text), tb_naam1.Text, cb_geslacht1.Text, tb_adres1.Text, (int)num_leeftijd1.Value, dtp_datum.Value.Date.ToString(), cb_diner1.Text);
                    //voeg gasten toe aan gasten
                    if (tb_naam2.Text != "")
                    {
                        boeking.gasten.Add(new Gasten(Convert.ToInt32(cb_kamer.Text), tb_naam2.Text, cb_geslacht2.Text, tb_adres2.Text, (int)num_leeftijd2.Value, dtp_datum.Value.Date.ToString(), cb_diner2.Text));
                    }
                    if (tb_naam3.Text != "")
                    {
                        boeking.gasten.Add(new Gasten(Convert.ToInt32(cb_kamer.Text), tb_naam3.Text, cb_geslacht3.Text, tb_adres3.Text, (int)num_leeftijd3.Value, dtp_datum.Value.Date.ToString(), cb_diner3.Text));
                    }
                    if (tb_naam4.Text != "")
                    {
                        boeking.gasten.Add(new Gasten(Convert.ToInt32(cb_kamer.Text), tb_naam4.Text, cb_geslacht4.Text, tb_adres4.Text, (int)num_leeftijd4.Value, dtp_datum.Value.Date.ToString(), cb_diner4.Text));
                    }
                    Boeking.boekingen.Add(boeking);
                }
                else
                {
                    //bericht als de kamer al geboekt is.
                    MessageBox.Show("Helaas deze kamer is al geboekt voor deze datum", "Grand Hotel D'n Gaarkeuken", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {
                //bericht als niet alle velden zijn ingevuld
                MessageBox.Show("U heeft niet alle velden ingevuld.", "Grand Hotel D'n Gaarkeuken", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ListviewVullen()
        {
            //listview vullen met de objecten in boeking
            lv_boeking.Items.Clear();
            foreach (Boeking Boekingen in Boeking.boekingen)
            {
                ListViewItem lvi = new ListViewItem(Boekingen.kamerId.ToString());
                lvi.SubItems.Add(Boekingen.klantNaam);
                lvi.SubItems.Add(Boekingen.klantGeslacht);
                lvi.SubItems.Add(Boekingen.klantAdres);
                lvi.SubItems.Add(Boekingen.klantLeeftijd.ToString());
                lvi.SubItems.Add(Boekingen.klantEten);
                lvi.SubItems.Add(Boekingen.boekingDatum);

                lv_boeking.Items.Add(lvi);

                //listview vullen met de objecten in gasten
                foreach (Gasten gast in Boekingen.gasten)
                {
                    ListViewItem lvi2 = new ListViewItem(gast.kamerId.ToString());
                    lvi2.SubItems.Add(gast.gastNaam);
                    lvi2.SubItems.Add(gast.gastGeslacht);
                    lvi2.SubItems.Add(gast.gastAdres);
                    lvi2.SubItems.Add(gast.gastLeeftijd.ToString());
                    lvi2.SubItems.Add(gast.gastEten);
                    lvi2.SubItems.Add(gast.boekingDatum);

                    lv_boeking.Items.Add(lvi2);
                }
            }


        }

        void Circle_Paint(object sender, PaintEventArgs e)
        {
            //maak een nieuwe pen aan met de kleur rood
            Pen pen = new Pen(Color.Red);
            //maak een nieuwe brush aan met de kleur groen
            SolidBrush brush = new SolidBrush(Color.Green);

            // teken het groene rondje
            e.Graphics.FillEllipse(brush, 460, 25, 50, 50);
            // teken de rode streep
            e.Graphics.DrawLine(pen, 520, 50, 450, 50);            
        }

        private void btn_overzicht_Click(object sender, EventArgs e)
        {
            // maak eht form groter overzicht
            Size = new Size(1152, 428);
            btn_export.Visible = true;
            ListviewVullen();
        }

        private void btn_toevoegen_Click(object sender, EventArgs e)
        {
            Toevoegen();
            ListviewVullen();
        }

        private void cb_gasten_SelectedIndexChanged(object sender, EventArgs e)
        {
            //boxen legen
            tb_naam1.Clear();
            tb_naam2.Clear();
            tb_naam3.Clear();
            tb_naam4.Clear();
            tb_adres1.Clear();
            tb_adres2.Clear();
            tb_adres3.Clear();
            tb_adres4.Clear();
            cb_geslacht1.SelectedIndex = -1;
            cb_geslacht2.SelectedIndex = -1;
            cb_geslacht3.SelectedIndex = -1;
            cb_geslacht4.SelectedIndex = -1;
            cb_diner1.SelectedIndex = -1;
            cb_diner2.SelectedIndex = -1;
            cb_diner3.SelectedIndex = -1;
            cb_diner4.SelectedIndex = -1;
            // geef de invul velden voor meerdre gasten weer
            if ((int)cb_gasten.SelectedItem == 1)
            {
                tb_naam2.Visible = false;
                tb_naam3.Visible = false;
                tb_naam4.Visible = false;
                tb_adres2.Visible = false;
                tb_adres3.Visible = false;
                tb_adres4.Visible = false;
                cb_geslacht2.Visible = false;
                cb_geslacht3.Visible = false;
                cb_geslacht4.Visible = false;
                num_leeftijd2.Visible = false;
                num_leeftijd3.Visible = false;
                num_leeftijd4.Visible = false;
                cb_diner2.Visible = false;
                cb_diner3.Visible = false;
                cb_diner4.Visible = false;
            }
            else if ((int)cb_gasten.SelectedItem == 2)
            {
                tb_naam2.Visible = true;
                tb_naam3.Visible = false;
                tb_naam4.Visible = false;
                tb_adres2.Visible = true;
                tb_adres3.Visible = false;
                tb_adres4.Visible = false;
                cb_geslacht2.Visible = true;
                cb_geslacht3.Visible = false;
                cb_geslacht4.Visible = false;
                num_leeftijd2.Visible = true;
                num_leeftijd3.Visible = false;
                num_leeftijd4.Visible = false;
                cb_diner2.Visible = true;
                cb_diner3.Visible = false;
                cb_diner4.Visible = false;
            }
            else if ((int)cb_gasten.SelectedItem == 3)
            {
                tb_naam2.Visible = true;
                tb_naam3.Visible = true;
                tb_naam4.Visible = false;
                tb_adres2.Visible = true;
                tb_adres3.Visible = true;
                tb_adres4.Visible = false;
                cb_geslacht2.Visible = true;
                cb_geslacht3.Visible = true;
                cb_geslacht4.Visible = false;
                num_leeftijd2.Visible = true;
                num_leeftijd3.Visible = true;
                num_leeftijd4.Visible = false;
                cb_diner2.Visible = true;
                cb_diner3.Visible = true;
                cb_diner4.Visible = false;
            }
            else if ((int)cb_gasten.SelectedItem == 4)
            {
                tb_naam2.Visible = true;
                tb_naam3.Visible = true;
                tb_naam4.Visible = true;
                tb_adres2.Visible = true;
                tb_adres3.Visible = true;
                tb_adres4.Visible = true;
                cb_geslacht2.Visible = true;
                cb_geslacht3.Visible = true;
                cb_geslacht4.Visible = true;
                num_leeftijd2.Visible = true;
                num_leeftijd3.Visible = true;
                num_leeftijd4.Visible = true;
                cb_diner2.Visible = true;
                cb_diner3.Visible = true;
                cb_diner4.Visible = true;
            }
        }

        private void btn_export_Click(object sender, EventArgs e)
        {
            Export();
        }

        private void cb_soortKamer_SelectedIndexChanged(object sender, EventArgs e)
        {
            //keuze enum voor soort kamer
            if (cb_soortKamer.Text == "Twee persoons kamer")
            {
                cb_gasten.DataSource = Enum.GetValues(typeof(Gasten1));
            }
            else
            {
                cb_gasten.DataSource = Enum.GetValues(typeof(Gasten2));
            }
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            //boxen legen
            tb_naam1.Clear();
            tb_naam2.Clear();
            tb_naam3.Clear();
            tb_naam4.Clear();
            tb_adres1.Clear();
            tb_adres2.Clear();
            tb_adres3.Clear();
            tb_adres4.Clear();
            cb_geslacht1.SelectedIndex = -1;
            cb_geslacht2.SelectedIndex = -1;
            cb_geslacht3.SelectedIndex = -1;
            cb_geslacht4.SelectedIndex = -1;
            cb_diner1.SelectedIndex = -1;
            cb_diner2.SelectedIndex = -1;
            cb_diner3.SelectedIndex = -1;
            cb_diner4.SelectedIndex = -1;
        }
    }
}
