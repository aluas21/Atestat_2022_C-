using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Super_Snake
{

    public partial class Form2 : Form
    {
        
        public int pozitie_conectare2;

        string cautare;
        public Form2()
        {
            InitializeComponent();
        }

        public int sortare = 0;
        private void utilizatorBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.utilizatorBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.informatiiDataSet);

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'informatiiDataSet.Utilizator' table. You can move, or remove it, as needed.
            this.utilizatorTableAdapter.Fill(this.informatiiDataSet.Utilizator);
            if (informatiiDataSet.Utilizator[pozitie_conectare2].Admin == true)
                tabPage2.Text = "STATISTICI SI ADMIN";
            else
                tabPage2.Text = "STATISTICI";
            if (informatiiDataSet.Utilizator[pozitie_conectare2].Admin == true)
            {
                dupaEmailToolStripMenuItem1.Visible = true;
                utilizatoriToolStripMenuItem.Visible = true;

            }
            textBox2.Text = "Monede: " + Convert.ToString(informatiiDataSet.Utilizator[pozitie_conectare2].Monede);
        }

        private void crescatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            utilizatorTableAdapter.Fill(informatiiDataSet.Utilizator);
            button2.Visible = false;
            utilizatorDataGridView.Visible = true;
            dataGridViewTextBoxColumn6.Visible = true;
            dataGridViewTextBoxColumn4.Visible = false;
            dataGridViewTextBoxColumn8.Visible = false;
            if (informatiiDataSet.Utilizator[pozitie_conectare2].Admin == true)
            {
                coloana_email.Visible = true;
                coloana_parola.Visible = true;
                Id_utilizator.Visible = true;
            }
            utilizatorTableAdapter.SortareScorCresc(informatiiDataSet.Utilizator);
      
        }

        private void descrescatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            utilizatorTableAdapter.Fill(informatiiDataSet.Utilizator);
            button2.Visible = false;
            utilizatorDataGridView.Visible = true;
            dataGridViewTextBoxColumn6.Visible = true;
            dataGridViewTextBoxColumn4.Visible = false;
            dataGridViewTextBoxColumn8.Visible = false;
            if (informatiiDataSet.Utilizator[pozitie_conectare2].Admin == true)
            {
                coloana_email.Visible = true;
                coloana_parola.Visible = true;
                Id_utilizator.Visible = true;
            }
            utilizatorTableAdapter.SortareScorDesc(informatiiDataSet.Utilizator);
    
        }

        private void crescatorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            button2.Visible = false;
            utilizatorDataGridView.Visible = true;
            dataGridViewTextBoxColumn4.Visible = true;
            dataGridViewTextBoxColumn6.Visible = false;
            dataGridViewTextBoxColumn8.Visible = false;
            if (informatiiDataSet.Utilizator[pozitie_conectare2].Admin == true)
            {
                coloana_email.Visible = true;
                coloana_parola.Visible = true;
                Id_utilizator.Visible = true;
            }
            utilizatorTableAdapter.SortareVarstaCresc(informatiiDataSet.Utilizator);
   
        }

        private void descrescatorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            utilizatorTableAdapter.Fill(informatiiDataSet.Utilizator);
            button2.Visible = false;
            utilizatorDataGridView.Visible = true;
            dataGridViewTextBoxColumn4.Visible = true;
            dataGridViewTextBoxColumn6.Visible = false;
            dataGridViewTextBoxColumn8.Visible = false;
            if (informatiiDataSet.Utilizator[pozitie_conectare2].Admin == true)
            {
                coloana_email.Visible = true;
                coloana_parola.Visible = true;
                Id_utilizator.Visible = true;
            }
            utilizatorTableAdapter.SortareVarstaDesc(informatiiDataSet.Utilizator);

        }

        private void crescatorToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            utilizatorTableAdapter.Fill(informatiiDataSet.Utilizator);
            button2.Visible = false;
            utilizatorDataGridView.Visible = true;
            dataGridViewTextBoxColumn4.Visible = false;
            dataGridViewTextBoxColumn6.Visible = false;
            dataGridViewTextBoxColumn8.Visible = true;
            if (informatiiDataSet.Utilizator[pozitie_conectare2].Admin == true)
            {
                coloana_email.Visible = true;
                coloana_parola.Visible = true;
                Id_utilizator.Visible = true;
            }
            utilizatorTableAdapter.SortareMonedeCresc(informatiiDataSet.Utilizator);

        }

        private void descrescatorToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            utilizatorTableAdapter.Fill(informatiiDataSet.Utilizator);
            button2.Visible = false;
            utilizatorDataGridView.Visible = true;
            dataGridViewTextBoxColumn4.Visible = false;
            dataGridViewTextBoxColumn6.Visible = false;
            dataGridViewTextBoxColumn8.Visible = true;
            if (informatiiDataSet.Utilizator[pozitie_conectare2].Admin == true)
            {
                coloana_email.Visible = true;
                coloana_parola.Visible = true;
                Id_utilizator.Visible = true;
            }

            utilizatorTableAdapter.SortareMonedeDesc(informatiiDataSet.Utilizator);
        }

        private void utilizatoriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            utilizatorTableAdapter.Fill(informatiiDataSet.Utilizator);
            button2.Visible = false;
            coloana_email.Visible = true;
            coloana_parola.Visible = true;
            Id_utilizator.Visible = true;
            utilizatorDataGridView.Visible = true;
            dataGridViewTextBoxColumn4.Visible = true;
            dataGridViewTextBoxColumn6.Visible = true;
            dataGridViewTextBoxColumn8.Visible = true;
        }

        private void dupaEmailToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            cautare = "email";
            utilizatorDataGridView.Visible = false;
            label1.Visible = true;
            textBox1.Visible = true;
            button1.Visible = true;
            button2.Visible = true;
            textBox1.Text = "";
        }

        private void dupaNumeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cautare = "nume";
            utilizatorDataGridView.Visible = false;
            label1.Visible = true;
            textBox1.Visible = true;
            button1.Visible = true;
            button2.Visible = false;
            textBox1.Text = "";
        }

        private void dupaEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            utilizatorDataGridView.Visible = false;
            cautare = "scor";
            label1.Visible = true;
            textBox1.Visible = true;
            button1.Visible = true;
            button2.Visible = false;
            textBox1.Text = "";
        }

        private void dupaVarstaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cautare = "varsta";
            utilizatorDataGridView.Visible = false;
            label1.Visible = true;
            textBox1.Visible = true;
            button1.Visible = true;
            button2.Visible = false;
            textBox1.Text = "";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text=="")
            {
                MessageBox.Show("Introuduceti un text!",
                "Casuta este goala",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
            else
            {
                string nume;
                nume = textBox1.Text;
                if(cautare=="nume")
                {
                    utilizatorTableAdapter.CautareNume(informatiiDataSet.Utilizator,nume);
                    utilizatorDataGridView.Visible = true;
                }

                if(cautare=="scor")
                {
                    try
                    {
                        int scor;
                        scor = Convert.ToInt32(nume);
                        utilizatorTableAdapter.CautareScor(informatiiDataSet.Utilizator, scor);
                        utilizatorDataGridView.Visible = true;
                    }
                    catch
                    {
                        MessageBox.Show("Introuduceti un numar",
                        "Valoare este gresita",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    }

                }

                if (cautare=="email")
                {
                    utilizatorTableAdapter.CautareEmail(informatiiDataSet.Utilizator, nume);
                    utilizatorDataGridView.Visible = true;
                }

                if (cautare=="varsta")
                {
                    try
                    {
                        int scor;
                        scor = Convert.ToInt32(nume);
                        utilizatorTableAdapter.CautareVarsta(informatiiDataSet.Utilizator, scor);
                        utilizatorDataGridView.Visible = true;
                    }
                    catch
                    {
                        MessageBox.Show("Introuduceti un numar",
                        "Valoare este gresita",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text!="")
            { 
                string email;
                email = textBox1.Text;
                int n = informatiiDataSet.Utilizator.Count();
                int poz=-1;
                for(int i =0;i<=n;i++)
                {
                    if (email == informatiiDataSet.Utilizator[i].E_mail)
                    {
                        poz = i;
                        i = n;
                    }
                }
                if(poz!=-1)
                {
                    if (informatiiDataSet.Utilizator[poz].Admin == true)
                    {
                        MessageBox.Show("Acest cont nu poate fi sters",
                       "Valoare gresita",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
                    }
                    else
                    {

                        utilizatorTableAdapter.StergereCont(email);
                        utilizatorTableAdapter.Fill(informatiiDataSet.Utilizator);
                    }
                }
            }
            else
            {
                MessageBox.Show("Introuduceti un email",
                "Caseta este goala",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
        }

        private void tabPage9_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string parola_veche = parolaveche.Text;
                string parola1 = parolanoua1.Text;
                string parola2 = parolanoua2.Text;

                if (parola_veche != informatiiDataSet.Utilizator[pozitie_conectare2].Parola)
                {
                    MessageBox.Show("Mai incercati",
                    "Parola veche este gresita!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                    parolanoua1.Text = parolanoua1.Text = parolanoua2.Text = "";
                }
                else
                {
                    if(parola1!=parola2)
                    {
                        MessageBox.Show("Parolele nu corespund",
                        "Reintroduceti parola noua",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                        parolanoua1.Text = parolanoua1.Text = parolanoua2.Text = "";
                    }
                    else
                    {
                        parolanoua1.Text = parolanoua1.Text = parolanoua2.Text = "";
                        MessageBox.Show("Actiune realizata cu succes",
                        "Parola a fost actualizata",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                        parolanoua1.Text = parolanoua1.Text = parolanoua2.Text = "";
                        string email = informatiiDataSet.Utilizator[pozitie_conectare2].E_mail;
                        utilizatorTableAdapter.Actualizare_parola(parola1, email);
                        utilizatorTableAdapter.Fill(this.informatiiDataSet.Utilizator);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Introduceti toate datele!",
                "Exista casete goale!",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            }

            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                string email = emailbox.Text;
                string emailnou = emailnoubox.Text;
                int n = informatiiDataSet.Utilizator.Count(), ok = 1 ;
                for(int i=0;i<n;i++)
                    if(emailnou==informatiiDataSet.Utilizator[i].E_mail)
                    {
                        ok = 0;
                        emailbox.Text = emailnoubox.Text = "";
                        MessageBox.Show("Email folosit",
                        "Exista deja un cont asociat acestui email",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    }
                if(ok==1)
                {
                    emailbox.Text = emailnoubox.Text = "";
                    MessageBox.Show("Actiune realizata cu succes",
                    "Email-ul a fost actualizata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                    utilizatorTableAdapter.Email_update(emailnou, email);
                    utilizatorTableAdapter.Fill(this.informatiiDataSet.Utilizator);

                }
            }
            catch
            {
                MessageBox.Show("Introduceti toate datele!",
                "Exista casete goale!",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            }


        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(informatiiDataSet.Utilizator[pozitie_conectare2].Admin==true)
            {
                MessageBox.Show("Contul nu poate fi sters",
                "Contul este de tip administrator",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Apasati ok pentru a parasi aplicatia",
                "Contul a fost sters",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
                utilizatorTableAdapter.Stergere_cont(informatiiDataSet.Utilizator[pozitie_conectare2].E_mail);
                utilizatorTableAdapter.Fill(this.informatiiDataSet.Utilizator);
                Application.Exit();
            }

        }
    }
}
