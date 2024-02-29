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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int pozitie_conectare;        ///pentru retinerea pozitiei din tabel a persoanei conectate
        int valoare_score = 0;        ///scorul primului sarpe care se modifica cand locatia sarpelui este egala cu cea a marului+
        int valoare_score2 = 0;       ///scorul celui de al doilea sarpe    -ll-              -ll-                -ll-
        int pauza_joc = 0;            ///se modifica in momentul apasarii tastei space pentru o opri timer ul
        int nr_juc = -1;              ///nr de jucatori setat din butoanele din groupbox
        int nr_mere = -1;             ///nr de mere care vor aparea pe ecran simultan
        int nr_ziduri = -1;
        int lungime_zid = 0;
        int nr_banuti = 0;
        int timer3_start = 0;         ///verificam daca timerul 3 este pornit
        int punctaj_5 = 0;            ///verficarea daca punctajul obtinut este 5
        int ok_gen_banut = 0;
        int ok_numarare_banuti = 0;

        public PictureBox tabla = new PictureBox();        ///declararea si initializarea tablei
        public PictureBox Snake = new PictureBox();        ///declararea si initializarea sarpelui
        public PictureBox Snake2 = new PictureBox();       ///declararea si initializarea celui de al doilea sarpe
        public PictureBox Mar = new PictureBox();          ///delcararea si initializarea marului
        public PictureBox[] Coada = new PictureBox[1001];  ///delcararea si initializarea coadei sarpelui     
        public PictureBox[] Coada2 = new PictureBox[1001]; ///declararea si initializarea coadei sarpelui doi
                                                           ///cu ajutorul unui vector de pictureboc-uri
        public PictureBox[] Mere = new PictureBox[5];
        public PictureBox[] Zid = new PictureBox[150];
        public PictureBox banut = new PictureBox();

        public int dx = 1, dy = 0;    ///declararea variabileleor ce modifica miscarea sarpelui 1 in functie de tasta apasata
        public int cl = 0;            ///declararea numarului ce reprezinta lungimea coadei sarpelui 1

        public int dx2 = 1, dy2 = 0;    ///declararea variabileleor ce modifica miscarea sarpelui 2 in functie de tasta apasata
        public int cl2 = 0;            ///declararea numarului ce reprezinta lungimea coadei sarpelui 2
       
        public int k = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'informatiiDataSet.Utilizator' table. You can move, or remove it, as needed.
            this.utilizatorTableAdapter.Fill(this.informatiiDataSet.Utilizator);
            // TODO: This line of code loads data into the 'informatiiDataSet.Utilizator' table. You can move, or remove it, as needed.
            this.utilizatorTableAdapter.Fill(this.informatiiDataSet.Utilizator);


            tabla.Width = 500;
            tabla.Height = 520;                   ///latimea si inaltimea picturebox-ului
            tabla.BackColor = Color.Black;        ///culoare picturebox
            tabla.Location = new Point(0, 0);     ///locatia de lansare a picturebox-ului in fereastra

            this.Location = new Point(50, 50);    ///locatia de lansare a ferestrei in ecarn
            this.Width = 519;                     ///latimea ferestrei
            this.Height = 561;                    ///inaltimea ferestrei
            this.Controls.Add(tabla);             ///adaugarea picturebox-ului in fereastra

            tabla.Visible = false;
            pauza_joc = 0;

            ///introducearea in labelul bestscore a valorii scorului cel mai mare obtinut a jucatorului
            ///
            bestscore.Text = Convert.ToString(informatiiDataSet.Utilizator[pozitie_conectare].Scor_maxim);

        }

        private void conectare_Click(object sender, EventArgs e)
        {

            ///datorita faptului ca panel2 se afla in interiorul panelului 1 este nevoie ca acesta sa fie visible false
            ///dupa care se afiseaza label 1 si cum acesta este visible false se afiseaza ceea ce se afla in interiorul sau
            ///adica panel 2
           
            panel1.Show();
            panel2.Visible = true;
            
        }

        private void inregistrare_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel1.Visible = true;
        }

        private void utilizatorBindingNavigatorSaveItem_Click_4(object sender, EventArgs e)
        {
            this.Validate();
            this.utilizatorBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.informatiiDataSet);

        }


        //INREGISTRAREA UNNUI JUCATOR IN BAZA DE DATE
        
        private void button2_Click(object sender, EventArgs e)
        {
            if(NumeBox.Text!=""&&PrenumeBox.Text!=""&&EmailBox.Text!=""&&ParolaBox.Text!=""&&comboBox1.Text!="")
            {
                int perfect = 1;      ///se presupune ca toate conditile sunt satisfacute
                string nume = Convert.ToString(NumeBox.Text);
                string prenume = Convert.ToString(PrenumeBox.Text);
                string email = Convert.ToString(EmailBox.Text);
                string parola = Convert.ToString(ParolaBox.Text);
                int varsta = Convert.ToInt32(comboBox1.Text);
                int n = informatiiDataSet.Utilizator.Count();   ///numararea inregistrarilor din tabelul utilizatori
                for(int i=0;i<n;i++)
                    if(informatiiDataSet.Utilizator[i].E_mail==email)    ///se verifica ca email ul sa nu fie deja folosit
                    {
                        perfect = 0;        ///nu se indeplinesc toate conditiile
                        MessageBox.Show("Acest e-mail este deja folosit!",
                        "Introduceti un e-mail nou",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                        EmailBox.Text = "";
                    }
                if(parola.Length<4)    ///se verifica ca paprola sa aiba cel putin patru caractere
                {
                    perfect = 0;
                    MessageBox.Show("Parola prea scurta!",
                    "Introduceti alta parola!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                    ParolaBox.Text = "";
                   
                }
                if(perfect!=0)    ///daca toate datele sunt validate
                {
                    ///Golirea casetelor
                    NumeBox.Text = PrenumeBox.Text = ParolaBox.Text = EmailBox.Text = comboBox1.Text = "";
                    comboBox1.SelectedItem = null;
                    ///Inserearea propriu zisa in baza de date
                    utilizatorTableAdapter.Inserare(false,nume, prenume, varsta, email, parola, 1,0);
                    utilizatorTableAdapter.Fill(this.informatiiDataSet.Utilizator);

                    
                    MessageBox.Show("Inregistrare reusita!",
                    "Contul este salvat!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                }

            }
            else
            {
                MessageBox.Show("Introduceti toate datele!",
                "Exista casete goale!",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            }

        }

        private void utilizatorBindingNavigatorSaveItem_Click_2(object sender, EventArgs e)
        {
            this.Validate();
            this.utilizatorBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.informatiiDataSet);

        }

        int knt_parola = 0;
        private void button1_Click(object sender, EventArgs e)   ///buton pentru afisarea parolei
        {

            if(ParolaBox.Text!="")
            {
                knt_parola++;
                if (knt_parola % 2 == 0)
                    ParolaBox.PasswordChar = '*';
                else
                    ParolaBox.PasswordChar = '\0';   ///anularea Passwordchar ului
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            NumeBox.Text = PrenumeBox.Text = ParolaBox.Text = EmailBox.Text = comboBox1.Text = "";
            comboBox1.SelectedItem = null;
        }


        //CONECTAREA IN JOC, AUTENTIFICAREA

        private void button4_Click(object sender, EventArgs e)
        {
            string email, parola;
            if(emailconectarebox.Text!=""&&parolaconectarebox.Text!="")
            {
                int pozitie = -1;
                email = emailconectarebox.Text;
                parola = parolaconectarebox.Text;
                int n = informatiiDataSet.Utilizator.Count();
                for (int i = 0; i < n; i++)
                    if (email == informatiiDataSet.Utilizator[i].E_mail)   ///se cauta utilizatorul in baza de date
                    {
                        pozitie = i;
                        i = n;
                    }
                if(pozitie == -1)  ///in cazul in care nu s-a gasit se va afisa urmatorul mesaj
                {
                     MessageBox.Show("Contul nu a fost gasit!",
                     "Mai incercati!",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Information);
                    emailconectarebox.Text = "";
                    parolaconectarebox.Text = "";
                }
                else     ///daca utilizatorul este gasit se verifica ca parola sa fie cea corecta
                {

                    ///CONECTAREA PROPRIU ZISA IN CONT
                    ///in cazul in care parolele coincid se intra in cont
                    if(parola==informatiiDataSet.Utilizator[pozitie].Parola)
                    {
                        pozitie_conectare = pozitie;  ///salvarea indicelui jucatorului din tabelul utilizator
                        label14.Text = informatiiDataSet.Utilizator[pozitie_conectare].Prenume;
                        MessageBox.Show("Conectare reusita!",
                        "Ati intrat in cont!",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                        nr_banuti = informatiiDataSet.Utilizator[pozitie_conectare].Monede;

                        panel1.Visible = false;
                        panel2.Visible = false;
                        inregistrare.Visible = false;
                        conectare.Visible = false;

                        ///afisarea jocului
                        setaributton.Visible = true;
                        tabla.Visible = true;
                        Snake.Visible = true;
                        Mar.Visible = true;
                        label1.Visible = true;
                        label1.ForeColor = Color.Red;
                        label1.Text = "Apasati tasta SPACE pentru a incepe";
                        label1.BackColor = Color.Blue;
                        label1.Font = new Font("Ravie", 13, FontStyle.Bold);
                        label1.Location = new Point(0, 10);
                        bestscore.Text = Convert.ToString(informatiiDataSet.Utilizator[pozitie_conectare].Scor_maxim);


                        conectare.TabIndex = 10;
                        inregistrare.TabIndex = 11;

                        button1jucator.Visible = true;
                        button2jucatori.Visible = true;
                        label15.Visible = true;
                        label16.Visible = true;
                        setaributton.Visible = true;

                        this.Focus();
                        ///eliminarea celor doua panel uri pentru a se reusi focalizarea pe Form
                        ///si pentru a putea fi recunoscuta functia KeyDown

                        panel1.Controls.Remove(button2);
                        panel2.Controls.Remove(button2);
                        this.Focus();

                        groupBox1.Visible = true;
                        groupBox1.Focus();

                        bestscore.Text = Convert.ToString(informatiiDataSet.Utilizator[pozitie_conectare].Scor_maxim);

                    }
                    else    ///daca parolele nu coincid, jucatorul este rugat sa incerce iar
                    {
                        MessageBox.Show("Parola incorecta!",
                        "Reintroduceti parola!",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                        parolaconectarebox.Text = "";
                    }
                }
            }
            else
            {
                MessageBox.Show("Introduceti toate datele!",
                "Exista casete goale!",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel1.Visible = false;
            emailconectarebox.Text = "";
            parolaconectarebox.Text = "";
        }

        public int pozitie_mere;

        //GENERAREA MERELOR PE ECRAN
        public void generare_mar()
        {
            int okk = 0;
            Random r = new Random();
            int x1 ,y1;
            while (okk==0)    ///se cauta o pozitie din tabla care sa nu fie ocupata
            {
                 x1 = r.Next(1,23) * 20;
                 y1 = r.Next(4,24) * 20;
                 Mere[pozitie_mere].Location = new Point(x1, y1);
                 okk = 1;
 
                if (Mere[pozitie_mere].Location == Snake.Location)   ///se verifica ca marul sa nu fie plasat pe capul sarpelui
                    okk = 0;
                for (int i = 1; i <= cl&&okk==1; i++)
                    if (Coada[i].Location == Mere[pozitie_mere].Location)   ///se verifica ca marul sa nu fie plasat intr-un element din coada
                        okk = 0;
                for (int i = 1; i <= cl2 && okk == 1; i++)
                    if (Coada2[i].Location == Mere[pozitie_mere].Location)   ///se verifica ca marul sa nu fie plasat intr-un element din coada
                        okk = 0;
            }
        }

        //GENERAREA BANUTULUI PE TABLA
        public void generare_banut()
        {
            int okk = 0;
            Random r = new Random();
            int x1, y1;
            while (okk == 0)     ///se cauta o pozitie libera pe tabla
            {
                x1 = r.Next(1, 23) * 20;
                y1 = r.Next(4, 24) * 20;
                banut.Location = new Point(x1, y1);
                okk = 1;

                if (banut.Location == Snake.Location)   ///se verifica ca banutul sa nu fie plasat pe capul sarpelui
                    okk = 0;
                for (int i = 1; i <= cl && okk == 1; i++)
                    if (Coada[i].Location == banut.Location)   ///se verifica ca banutul sa nu fie plasat intr-un element din coada
                        okk = 0;
                for (int i = 1; i <= nr_mere && okk==1; i++)   ///se verifica ca banutul sa nu fie plasat in pozitia unui mar
                    if (Mere[i].Location == banut.Location)
                        okk = 0;
            }
            tabla.Controls.Add(banut);   
            banut.Image = Image.FromFile("banut.png");
            banut.Width = banut.Height = 18;
            ok_gen_banut = 1;
        }

        public void generare_coada()   //GENERARE COADA PRIMULUI SARPE
        {
            Coada[++cl] = new PictureBox();
            Coada[cl].BackColor = Color.White;
            Coada[cl].Location = Snake.Location;
            Coada[cl].Width = Coada[cl].Height = 18;
        }

        public void generare_coada2()    //GENERAREA COADA AL DOILEA SARPE
        {
            Coada2[++cl2] = new PictureBox();
            Coada2[cl2].BackColor = Color.White;
            Coada2[cl2].Location = Snake2.Location;
            Coada2[cl2].Width = Coada2[cl2].Height = 18;
        }
        int mutare = 0;   ///variablila care determina daca miscarea plasata anterior s-a
                          ///executat pentru a se putea realiza urmatoarea miscare

        //MISCAREA PRIMULUI SARPE
        private void timer1_Tick(object sender, EventArgs e)
        {
            ///NUMEROTAREA COZII INCEPE CU CEL DE LANGA CAPUL SARPELUI
            for (int i = cl; i >= 2; --i)                      ///se parcurge vectorul de cozi
                Coada[i].Location = Coada[i - 1].Location;     ///se permuta elementul cozii de pe pozitia i pe pozitia i-1

            if (cl > 0)                                        ///daca exista cel putin un element in coada
                Coada[1].Location = Snake.Location;            ///se muta primul element din coada pe pozitia sarpelui

            mutare = 1;
            Snake.Location = new Point(Snake.Location.X + dx * 20, Snake.Location.Y + dy * 20);
            ///locatia sarpelui se modifica in functie de locatia anteriaora si coordonatele dx si dy
            ///care sunt modificate la randul sau de tastele apasate 
            int x = Snake.Location.X;
            int y = Snake.Location.Y;

            ///TRECEREA SARPELUI PRIN PERETI
            if (x > 480)
                x = 0;
            if (x < 0)
                x = 480;
            if (y > 500)
                y = 60; 
            if (y < 57)
                y = 500;
            Snake.Location = new Point(x, y);
            if(nr_juc==2)
            {
                for(int i=1;i<=cl2;i++)
                {
                    if (Snake.Location == Coada2[i].Location)    ///daca sarpele 1 atinge coada saperlui 2
                    {
                        timer2.Stop();     ///se opresc serpii
                        timer1.Stop();

                        MessageBox.Show("Jucatorul numarul 2 a castigat ",
                        "Jocul s-a terminat",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                        label11.Visible = false;
                        label12.Visible = false;
                        label13.Visible = false;
                        label14.Visible = false;
                        score.Visible = false;
                        bestscore.Visible = false;

                        groupBox1.Visible = true;
                        groupBox1.Focus();

                        label1.Visible = true;

                        valoare_score = 0;
                        valoare_score2 = 0;
                        pauza_joc = 0;

                        tabla.Controls.Clear();
                        
                    }
                }
            }
            for (int i = 1; i <= cl; ++i)
            {
                if (Snake.Location == Coada[i].Location)      ///se verifica da sarpele este pe aceasi pozitie cu un element din coada
                {
                    timer1.Stop();
                    if (nr_juc == 2)
                        timer2.Stop();

                    label11.Visible = false;
                    label12.Visible = false;
                    label13.Visible = false;
                    label14.Visible = false;
                    score.Visible = false;
                    bestscore.Visible = false;
                    
                    if(nr_juc==1)
                    {
                        label3.Text = Convert.ToString(k);
                        if (valoare_score > informatiiDataSet.Utilizator[pozitie_conectare].Scor_maxim)
                        {
                            utilizatorTableAdapter.Scor_maxim_update(valoare_score,informatiiDataSet.Utilizator[pozitie_conectare].E_mail);
                            utilizatorTableAdapter.Fill(this.informatiiDataSet.Utilizator);
                        }
                        MessageBox.Show("Scorul obtinut este: " + Convert.ToString(valoare_score),
                        "Ati pierdut!",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Jucatorul numarul 2 a castigat",
                        "Jocul s-a terminat",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    }

                    parolaconectarebox.Text = "";
                    groupBox1.Visible = true;

                    label1.Visible = true;

                    this.Focus();


                    valoare_score = 0;
                    pauza_joc = 0;

                    tabla.Controls.Clear();

                    score.Text = "0";
                }

            }

            for(int i=1;i<=nr_mere;i++)
            {
                if (Snake.Location == Mere[i].Location)
                {
                    if (timer3_start == 0 && nr_juc == 1 && progressBar1.Value < 100)
                    {
                        progressBar1.Value = progressBar1.Value + 20;
                        punctaj_5++;
                    }
                    pozitie_mere = i;

                    generare_mar();
                    generare_coada();

                    valoare_score++;
                    score.Text = (Convert.ToString(valoare_score));
                    if (valoare_score > Convert.ToInt32(bestscore.Text)&&nr_juc!=2)
                        bestscore.Text = (Convert.ToString(valoare_score));

                    tabla.Controls.Add(Coada[cl]);
                }
            }
            
            if (nr_ziduri == 1 && (Snake.Location.Y > 480 || Snake.Location.Y < 80 || Snake.Location.X < 20 || Snake.Location.X > 460))
            {
                timer1.Stop();
                if(nr_juc==2)
                {
                    timer2.Stop();
                    MessageBox.Show("Jucatorul 2 a castigat",
                    "Jocul s-a terminat",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Scorul obtinut est: " + Convert.ToString(valoare_score),
                    "Jocul s-a terminat",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                }


                groupBox1.Visible = true;
            }

            if(nr_juc==1&&punctaj_5==5&&valoare_score!=0)
            {
                timer3.Start();
                timer3_start = 1;
                
                if(ok_gen_banut==0)
                    generare_banut();
            }
            if(timer3_start==1&&Snake.Location==banut.Location)
            {
                nr_banuti++;
                tabla.Controls.Remove(banut);
                punctaj_5 = 0;
                progressBar1.Value = 0;
                timer3_start = 0;
                timer3.Stop();
                ok_gen_banut = 0;
            }

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //IMPORTANT
            /* 
                  Tabla de joc este asemanata cadranului unu, adica ambele valori ale lui x si y sunt pozitive,
               dar este asemanata si cadrnului 4, adica originea este in partea din stanga sus si in functie de 
               cum dorim sa se deplaseze sarpele modficam pe dx si dy.
                  Spre exemplu,dorim ca sarpele sa mearga in jos, atunci ordonata sarpelui trebuie sa scada, 
               prin urmare dy trebuie sa fie negativ pentru a i se adauga ordonatei o valoare negativa, scazand 
               valoarea acesteiea. In consecinta srapele se apropie de laura de jos.
      
            */
            
            if (e.KeyCode == Keys.Space)
            {
                pauza_joc++;
               
                if (pauza_joc%2==1)
                {
                    if (pauza_joc != 1)
                    {
                        score.Text = "0";
                    }

                    label11.Visible = false;
                    timer1.Start();                      ///inceperea jocului la apasarea Space-ului
                    if (nr_juc == 2)
                    {
                        timer2.Start();
                        label11.Text = "SCORE2 :";
                    }
                    else
                    {
                        label11.Text = "BEST SCORE:";
                    }
                        
                    label1.Visible = false;              ///dispare label ul cu intrusctiunea de pornire
                   
                    label11.Visible = true;
                    label12.Visible = true;
                    label13.Visible = true;
                    label14.Visible = true;
                    score.Visible = true;
                    bestscore.Visible = true;

                    groupBox1.Visible = false;

                }
                else
                {
                    timer1.Stop();
                    if(nr_juc==2)
                        timer2.Stop();
                    label17.Visible = true;
                }    

            }


            if(pauza_joc%2==1)
            {
                label17.Visible = false;
                if (e.KeyCode == Keys.Left && (dx != 1 && dy != 0)&&mutare!=0)   ///daca s a apasat pe tasta "left" si sarpele
                {                                                      ///nu a mers inainte spre dreapta se pot modifica coordonatele
                    dx = -1;                   ///dx scade pentru ca sarpele sa se aproprie de origine
                    dy = 0;                    ///dy ramane constant
                    mutare = 0;
                }
                else
                {
                    if (e.KeyCode == Keys.Right && (dx != -1 && dy != 0) && mutare != 0)
                    {
                        dx = 1;
                        dy = 0;
                        mutare = 0;
                    }
                    else
                    {
                        if (e.KeyCode == Keys.Up && (dx != 0 && dy != 1) && mutare != 0) ///daca s a apasat pe tasta "up" si sparele
                        {                                                  ///nu a mers inainte in jos se pot modifica coordonatele
                            dx = 0;           ///dx ul ramane constant
                            dy = -1;          ///dy scade pentru a se putea apropia de origine
                            mutare = 0;
                        }
                        else
                        {
                            if (e.KeyCode == Keys.Down && (dx != 0 && dy != -1) && mutare != 0)
                            {
                                dx = 0;
                                dy = 1;
                                mutare = 0;
                            }
                        }

                    }

                }
            }

            if(nr_juc==2)
            {
                if (pauza_joc % 2 == 1)
                {
                    
                    if (e.KeyCode == Keys.A && (dx2 != 1 && dy2 != 0) && mutare2 != 0)   ///daca s a apasat pe tasta "left" si sarpele
                    {                                                      ///nu a mers inainte spre dreapta se pot modifica coordonatele
                        dx2 = -1;                   ///dx scade pentru ca sarpele sa se aproprie de origine
                        dy2 = 0;                    ///dy ramane constant
                        mutare2 = 0;
                    }
                    else
                    {
                        if (e.KeyCode == Keys.D && (dx2 != -1 && dy2 != 0) && mutare2 != 0)
                        {
                            dx2 = 1;
                            dy2 = 0;
                            mutare2 = 0;
                        }
                        else
                        {
                            if (e.KeyCode == Keys.W && (dx2 != 0 && dy2 != 1) && mutare2 != 0) ///daca s a apasat pe tasta "up" si sparele
                            {                                                  ///nu a mers inainte in jos se pot modifica coordonatele
                                dx2 = 0;           ///dx ul ramane constant
                                dy2 = -1;          ///dy scade pentru a se putea apropia de origine
                                mutare2 = 0;
                            }
                            else
                            {
                                if (e.KeyCode == Keys.S && (dx2 != 0 && dy2 != -1) && mutare2 != 0)
                                {
                                    dx2 = 0;
                                    dy2 = 1;
                                    mutare2 = 0;
                                }
                            }

                        }

                    }
                }
            }

        }

        private void setaributton_Click(object sender, EventArgs e)
        {
            Form2 Setari = new Form2();
            Setari.pozitie_conectare2 = pozitie_conectare;
            Setari.Show();
        }

        private void button1jucator_Click(object sender, EventArgs e)
        {
            nr_juc = 1;
            ok_numarare_banuti = 1;
        }
        
        private void button2jucatori_Click(object sender, EventArgs e)
        {
            nr_juc = 2;
            ok_numarare_banuti = 0;
        }


        //INCEPEREA JOCULUI
        private void buttoninchis_Click(object sender, EventArgs e)
        {
            label1.Visible = true;
            if(ok_numarare_banuti==1)
            {
                utilizatorTableAdapter.Adaugare_monede(nr_banuti, informatiiDataSet.Utilizator[pozitie_conectare].E_mail);
                utilizatorTableAdapter.Fill(this.informatiiDataSet.Utilizator);
            }
            label17.Visible = false;
            nr_banuti = 0;
            pauza_joc = 0;
            punctaj_5 = 0;
            valoare_score = 0;
            valoare_score2 = 0;
            progressBar1.Value = 0;
            progressBar1.Visible = false;
            ///se verfica ca fiecare camp sa fie completat
            if (nr_juc != -1 && nr_mere != -1 && nr_ziduri != -1)
            {
                groupBox1.Visible = false;
                groupBox1.Focus();
                if(nr_juc==2)
                {
                    ///se adauga cei doi serpi
                    tabla.Controls.Clear();
                    cl = 0;
                    dx = 1;
                    dy = 0;

                    Snake.Width = Snake.Height = 18;      ///latimea si inaltimea sarpelui
                    Snake.Location = new Point(100, 100); ///locatia de lansare a sarpelui
                    Snake.BackColor = Color.Green;        ///culoarea sarpelui
                    tabla.Controls.Add(Snake);

                    Coada[++cl] = new PictureBox();
                    Coada[cl].BackColor = Color.White;
                    Coada[cl].Location = new Point(100 - 20, 100);
                    Coada[cl].Width = Coada[cl].Height = 18;
                    tabla.Controls.Add(Coada[cl]);

                    Coada[++cl] = new PictureBox();
                    Coada[cl].BackColor = Color.White;
                    Coada[cl].Location = new Point(100 - 40, 100);
                    Coada[cl].Width = Coada[cl].Height = 18;
                    tabla.Controls.Add(Coada[cl]);
                    score.Text = "0";
                    bestscore.Text = "0";

                    cl2 = 0;
                    dx2 = 1;
                    dy2 = 0;
                    Snake2.Width = Snake2.Height = 18;      ///latimea si inaltimea sarpelui
                    Snake2.Location = new Point(100, 460); ///locatia de lansare a sarpelui
                    Snake2.BackColor = Color.Yellow;        ///culoarea sarpelui
                    tabla.Controls.Add(Snake2);

                    Coada2[++cl2] = new PictureBox();
                    Coada2[cl2].BackColor = Color.White;
                    Coada2[cl2].Location = new Point(100 - 20, 460);
                    Coada2[cl2].Width = Coada2[cl2].Height = 18;
                    tabla.Controls.Add(Coada2[cl2]);

                    Coada2[++cl2] = new PictureBox();
                    Coada2[cl2].BackColor = Color.White;
                    Coada2[cl2].Location = new Point(100 - 40, 460);
                    Coada2[cl2].Width = Coada2[cl2].Height = 18;
                    tabla.Controls.Add(Coada2[cl2]);
                }
                else
                {
                    ///se adauga un singur sarpe
                    tabla.Controls.Clear();
                    progressBar1.Visible = true;
                    progressBar1.Value = 0;
                    cl = 0;
                    dx = 1;
                    dy = 0;
                    Snake.Width = Snake.Height = 18;      ///latimea si inaltimea sarpelui
                    Snake.Location = new Point(100, 100); ///locatia de lansare a sarpelui
                    Snake.BackColor = Color.Green;        ///culoarea sarpelui
                    tabla.Controls.Add(Snake);

                    Coada[++cl] = new PictureBox();
                    Coada[cl].BackColor = Color.White;
                    Coada[cl].Location = new Point(100 - 20, 100);
                    Coada[cl].Width = Coada[cl].Height = 18;
                    tabla.Controls.Add(Coada[cl]);

                    Coada[++cl] = new PictureBox();
                    Coada[cl].BackColor = Color.White;
                    Coada[cl].Location = new Point(100 - 40, 100);
                    Coada[cl].Width = Coada[cl].Height = 18;
                    tabla.Controls.Add(Coada[cl]);
                    score.Text = "0";
                    bestscore.Text = Convert.ToString(informatiiDataSet.Utilizator[pozitie_conectare].Scor_maxim);
                }

                ///se adauga merele cerute 
                Mere[1] = new PictureBox();
                Mere[1].Width = Mere[1].Height = 18;
                Mere[1].Location = new Point(100, 280);
                Mere[1].Image = Image.FromFile("apple1.png");
                tabla.Controls.Add(Mere[1]);

                if (nr_mere >= 2)
                {
                    Mere[2] = new PictureBox();
                    Mere[2].Width = Mere[2].Height = 18;
                    Mere[2].Location = new Point(200, 280);
                    Mere[2].Image = Image.FromFile("apple1.png");
                    tabla.Controls.Add(Mere[2]);
                }

                if (nr_mere == 3)
                {                   
                    Mere[3] = new PictureBox();
                    Mere[3].Width = Mere[3].Height = 18;
                    Mere[3].Location = new Point(300, 280);
                    Mere[3].Image = Image.FromFile("apple1.png");
                    tabla.Controls.Add(Mere[3]);
                }

                ///se adauga zidul daca a fost ales acest mod
                ///jucatorii nu o sa poata trece prin el
                ///se foloseste un vector de PictureBox
                if(nr_ziduri==1)
                {
                    Zid[1] = new PictureBox();
                    Zid[1].Width = Zid[1].Height = 18;
                    Zid[1].Location = new Point(0, 60);
                    Zid[1].Image = Image.FromFile("zid.png");
                    tabla.Controls.Add(Zid[1]);
                    lungime_zid = 1;
                    for(int i=1;i<=24;i++)
                    {
                        Zid[i+1] = new PictureBox();
                        Zid[i+1].Width = Zid[i+1].Height = 18;
                        Zid[i+1].Location = new Point(20*i, 60);
                        Zid[i+1].Image = Image.FromFile("zid.png");
                        tabla.Controls.Add(Zid[i+1]);
                        lungime_zid++;
                    }
                    for(int i=0;i<=24;i++)
                    {
                        Zid[lungime_zid] = new PictureBox();
                        Zid[lungime_zid].Width = Zid[lungime_zid].Height = 18;
                        Zid[lungime_zid].Location = new Point(20 * i, 500);
                        Zid[lungime_zid].Image = Image.FromFile("zid.png");
                        tabla.Controls.Add(Zid[lungime_zid]);
                        lungime_zid++;
                    }
                    for (int i = 4; i <= 24; i++)
                    {
                        Zid[lungime_zid] = new PictureBox();
                        Zid[lungime_zid].Width = Zid[lungime_zid].Height = 18;
                        Zid[lungime_zid].Location = new Point(0, 20*i);
                        Zid[lungime_zid].Image = Image.FromFile("zid.png");
                        tabla.Controls.Add(Zid[lungime_zid]);
                        lungime_zid++;
                    }
                    for (int i = 4; i <= 24; i++)
                    {
                        Zid[lungime_zid] = new PictureBox();
                        Zid[lungime_zid].Width = Zid[lungime_zid].Height = 18;
                        Zid[lungime_zid].Location = new Point(480, 20*i);
                        Zid[lungime_zid].Image = Image.FromFile("zid.png");
                        tabla.Controls.Add(Zid[lungime_zid]);
                        lungime_zid++;
                    }

                }
                this.Focus();
            }
            else
            {
                MessageBox.Show("Alegeti setarile jocului",
                "Introduceti toate optiunile",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            }
        }

        int mutare2 = 0;

        //DEPLSAREA AL DOILEA SARPE
        private void timer2_Tick(object sender, EventArgs e)
        {
            for (int i = cl2; i >= 2; --i)                      ///se parcurge vectorul de cozi
                Coada2[i].Location = Coada2[i - 1].Location;     ///se permuta elementul cozii de pe pozitia i pe pozitia i-1

            if (cl2 > 0)                                        ///daca exista cel putin un element in coada
                Coada2[1].Location = Snake2.Location;            ///se muta primul element din coada pe pozitia sarpelui

            mutare2 = 1;
            Snake2.Location = new Point(Snake2.Location.X + dx2 * 20, Snake2.Location.Y + dy2 * 20);
            ///locatia sarpelui se modifica in functie de locatia anteriaora si coordonatele dx si dy
            ///care sunt modificate la randul sau de tastele apasate 
            int x2 = Snake2.Location.X;
            int y2 = Snake2.Location.Y;
            if (x2 > 480)
                x2 = 0;
            if (x2 < 0)
                x2 = 480;
            if (y2 > 500)
                y2 = 60;
            if (y2 < 57)
                y2 = 500;
            Snake2.Location = new Point(x2, y2);

            for(int i=1;i<=cl;i++)
            {
                if(Snake2.Location==Coada[i].Location)    ///verifica daca sarpele 2 a intrat in coada sarpelui 1
                {
                    timer1.Stop();     ///se opresc serpii
                    timer2.Stop();

                    label11.Visible = false;
                    label12.Visible = false;
                    label13.Visible = false;
                    label14.Visible = false;
                    score.Visible = false;
                    bestscore.Visible = false;

                    MessageBox.Show("Jucatorul 1 a castigat",
                    "Jocul s-a terminat",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                    
                    valoare_score = 0;
                    valoare_score2 = 0;
                    pauza_joc = 0;

                    label1.Visible = true;
                    label5.Visible = true;

                    groupBox1.Visible = true;
                    groupBox1.Focus();

                    score.Text = "0";
                }

            }
            if (Snake2.Location == Snake.Location)    ///se opreste jocul daca serpii au aceasi locatie
            {
                timer1.Stop();
                timer2.Stop();

                if(valoare_score==valoare_score2) ///daca scoruruile sunt egale nu exista castigatori
                {
                    MessageBox.Show("Nu exista castigatori",
                    "Jocul s-a terminat",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                }
                if(valoare_score>valoare_score)
                {
                    MessageBox.Show("Jucatorul 1 a castigat",
                    "Jocul s-a terminat",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                }
                if(valoare_score2>valoare_score)
                {
                    MessageBox.Show("Jucatorul 2 a catigat",
                    "Jocul s-a terminat",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                }
                groupBox1.Visible = true;
            }

            for (int i = 1; i <= cl2; ++i)
            {
                if(Snake2.Location==Coada2[i].Location)        ///se verifica daca sarpele 2 intra in porpria coada
                {
                    timer1.Stop();     ///se opresc serpii
                    timer2.Stop();

                    label11.Visible = false;
                    label12.Visible = false;
                    label13.Visible = false;
                    label14.Visible = false;
                    score.Visible = false;
                    bestscore.Visible = false;

                    MessageBox.Show("Jucatorul 1 a castigat",
                    "Jocul s-a terminat",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                
                    groupBox1.Visible = true;
                    groupBox1.Focus();

                    label1.Visible = true;

                    valoare_score = 0;
                    pauza_joc = 0;

                    tabla.Controls.Clear();
                }

            }

            for(int i=1;i<=nr_mere;i++)
            {
                if (Snake2.Location == Mere[i].Location)
                {
                    pozitie_mere = i;
                    generare_mar();
                    generare_coada2();

                    valoare_score2++;
                    bestscore.Text = Convert.ToString(valoare_score2);

                    tabla.Controls.Add(Coada2[cl2]);
                }
            }

            ///se verifica daca sarpele a intrat in zid in cazul in care a fost ales acest mode de joc
            if(nr_ziduri==1&&(Snake2.Location.Y >480||Snake2.Location.Y<80||Snake2.Location.X<20||Snake2.Location.X>460))
            {
                timer1.Stop();
                timer2.Stop();

                MessageBox.Show("Jucatorul 1 a castigat",
                "Jocul s-a terminat",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

                groupBox1.Visible = true;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            nr_mere = 1;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            nr_mere = 2;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            nr_mere = 3;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            nr_ziduri = 1;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            nr_ziduri = 0;
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value <= 0)
            {
                timer3.Stop();
                timer3_start = 0;
                progressBar1.Value = 0;
                punctaj_5 = 0;
                tabla.Controls.Remove(banut);
                ok_gen_banut = 0;
            }
            else
                progressBar1.Value -= 20;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void utilizatorBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.utilizatorBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.informatiiDataSet);

        }
    }
}
