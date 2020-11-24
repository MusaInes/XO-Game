using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XO_GAME
{
    public partial class Form1 : Form
    {
        int i = Properties.Settings.Default.i, k = Properties.Settings.Default.k, brojac = 0;
        int[,] array = new int[19683, 9];
        public Form1()
        {
            InitializeComponent();
            labelMarko.Text = Properties.Settings.Default.labelMarko + k.ToString();
            labelDarko.Text = Properties.Settings.Default.labelDarko + i.ToString();
            if (brojac % 2 == 0)
            {
                labelMarko.Font = new Font(labelMarko.Font, FontStyle.Underline);
                labelDarko.Font = new Font(labelDarko.Font, FontStyle.Regular);
            }
            else
            {
                labelMarko.Font = new Font(labelMarko.Font, FontStyle.Regular);
                labelDarko.Font = new Font(labelDarko.Font, FontStyle.Underline);
            }
            labelVrijeme.Text = DateTime.Now.ToString("HH:mm:ss - dd/MM/yyyy");

            for (int r = 0; r < 19683; r++)
            {
                for(int j=0; j<9; j++)
                {
                    array[r,j] = 1;
                }
            }
            
        }


        private void tmrVrijeme_Tick(object sender, EventArgs e)
        {
            //labelVrijeme.Text = DateTime.Now.ToString("HH:mm:ss - dd/MM/yyyy");
            labelVrijeme.Text = array[100, 2].ToString(); ;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.i = i;
            Properties.Settings.Default.k = k;
            Properties.Settings.Default.Save();
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reset();
            btn1.Enabled = false;
            progressBar1.Show();
            
            i = 0;
            k = 0;
            
            for(int j=0; j<100; j++)
            {
                progressBar1.Value += progressBar1.Step;
            }
            labelMarko.Text = Properties.Settings.Default.labelMarko + k.ToString();
            
            labelDarko.Text = Properties.Settings.Default.labelDarko + i.ToString();
            txt1.Text = "Player 1";
            txt2.Text = "Player 2";
            txt1.Show();
            txt2.Show();
            btnUpisi.Show();
            
            foreach(Label lbl in tableLayoutPanel1.Controls)
            {
                lbl.Text = "";
            }
            brojac = 0;
            labelMarko.Font = new Font(labelMarko.Font, FontStyle.Underline);
            labelDarko.Font = new Font(labelDarko.Font, FontStyle.Regular);
        }

        private void tmrProgressBar_Tick(object sender, EventArgs e)
        {
            if(progressBar1.Value==100)
            {
                
                progressBar1.Hide();
                progressBar1.Value = 0;
                btn1.Enabled = true;
            }
        }

        private void btnUpisi_Click(object sender, EventArgs e)
        {
            labelMarko.Text = txt1.Text + ": ";
            labelDarko.Text = txt2.Text + ": ";
            Properties.Settings.Default.labelDarko = labelDarko.Text;
            Properties.Settings.Default.labelMarko = labelMarko.Text;
            txt1.Hide();
            txt2.Hide();
            btnUpisi.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Label lbl = (Label)sender;
            if (lbl.Text.Length == 0)
            {
                if (brojac % 2 == 0)
                {
                    lbl.Text = "X";
                    lbl.ForeColor = Color.Blue;
                }
                else
                {
                    lbl.Text = "O";
                    lbl.ForeColor = Color.SeaGreen;
                }
                brojac++;
            }

            if(PronadjiPobjednika())
            {
                if(brojac%2==0)
                {
                    i += 3;
                    MessageBox.Show("Pobjednik je "+txt2.Text+",\nCestitamo!!", "Pobjeda...", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    labelDarko.Text = Properties.Settings.Default.labelDarko + i.ToString();
                }
                else
                {
                    k += 3;
                    MessageBox.Show("Pobjednik je "+txt1.Text+",\nCestitamo!!", "Pobjeda...", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    
                    labelMarko.Text= Properties.Settings.Default.labelMarko+ k.ToString();
                }
                foreach (Label lbl1 in tableLayoutPanel1.Controls)
                {
                    lbl1.Text = "";
                    brojac = 0;
                }
            }
            else
            {
                if(brojac==9)
                {
                    MessageBox.Show("Nerijeseno !!","Nerijeseno...", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    i += 1;
                    k += 1;
                    labelDarko.Text = Properties.Settings.Default.labelDarko + i.ToString();
                    labelMarko.Text = Properties.Settings.Default.labelMarko + k.ToString();
                    foreach (Label lbl1 in tableLayoutPanel1.Controls)
                    {
                        lbl1.Text = "";
                        brojac = 0;
                    }
                }
            }
            if (brojac % 2 == 0)
            {
                labelMarko.Font = new Font(labelMarko.Font, FontStyle.Underline);
                labelDarko.Font = new Font(labelDarko.Font, FontStyle.Regular);
            }
            else
            {
                labelMarko.Font = new Font(labelMarko.Font, FontStyle.Regular);
                labelDarko.Font = new Font(labelDarko.Font, FontStyle.Underline);
            }
            txt1.Hide();
            txt2.Hide();
            btnUpisi.Hide();

        }

        bool PronadjiPobjednika()
        {


            if (label1.Text.Length > 0)
            {
                if (label1.Text.CompareTo(label2.Text) == 0 && label1.Text.CompareTo(label3.Text)==0)
                { return true; }
                else if (label1.Text.CompareTo(label5.Text)==0&& label1.Text.CompareTo(label9.Text) == 0)
                {
                    return true;
                }
                else if (label1.Text.CompareTo(label4.Text)==0&& label1.Text.CompareTo(label7.Text) == 0)
                {
                    return true;
                }
            }
            if(label2.Text.Length>0)
            {
                if(label2.Text.CompareTo(label5.Text)==0&& label2.Text.CompareTo(label8.Text) == 0)
                {
                    return true;
                }
            }
            if (label3.Text.Length > 0)
            {
                if (label3.Text.CompareTo(label6.Text) == 0 && label3.Text.CompareTo(label9.Text) == 0)
                {
                    return true;
                }
                else if(label3.Text.CompareTo(label5.Text) == 0 && label3.Text.CompareTo(label7.Text) == 0)
                {
                    return true;
                }
            }
            if (label4.Text.Length > 0)
            {
                if (label4.Text.CompareTo(label5.Text) == 0 && label4.Text.CompareTo(label6.Text) == 0)
                {
                    return true;
                }
            }
            if (label7.Text.Length > 0)
            {
                if (label7.Text.CompareTo(label8.Text) == 0 && label7.Text.CompareTo(label9.Text) == 0)
                {
                    return true;
                }
            }

            return false;
            
        }

        
    }
}
