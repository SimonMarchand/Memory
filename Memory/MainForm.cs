using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using dllLoto;

namespace Memory
{
    public partial class MainForm : Form
    {
        // Déclaration des variables globales du jeu
        int nbCartesDansSabot;  // Nombre de cartes dans le sabot (en fait nombre
                                // d'images dans le réservoir)
        int nbCartesSurTapis;   // Nombre de cartes sur le tapis

        private int[] cartesDistribuees;

        private static int[] cartesCachees = { 0, 0, 0, 0, 0, 0, 0, 0 };

        private List<int> pbList;

        private List<int> cartesRetournees;
        private List<int> essaiCartes;

        private bool inGame = false;

        public MainForm()
        {
            InitializeComponent();
            cartesRetournees = new List<int>();
            essaiCartes = new List<int>();
            /*pbList = new List<int>();

            for (int i = 0; i < CardsTableLayout.Controls.Count; i++)
            {
                foreach (PictureBox pictureBox in CardsTableLayout.Controls)
                {
                    if (pictureBox.Name == "pb_0" + i)
                    {
                        pbList.Add(i);
                    }
                }
            }*/
        }

        private void btn_retourner_Click(object sender, EventArgs e)
        {
            if (cartesDistribuees != null)
                afficherCartes(cartesDistribuees);
        }

        private void btn_distribuer_Click(object sender, EventArgs e)
        {
            //Distribution_Sequentielle();

            // On récupère le nombre d'images dans le réservoir :
            nbCartesDansSabot = il_cards_deck.Images.Count - 1;
            // On enlève 1 car :
            // -> la l'image 0 ne compte pas c’est l’image du dos de carte 
            // -> les indices vont de 0 à N-1, donc les indices vont jusqu’à 39
            //    s’il y a 40 images au total *

            // On récupère également le nombre de cartes à distribuées sur la tapis
            // autrement dit le nombre de contrôles présents sur le conteneur
            nbCartesSurTapis = CardsTableLayout.Controls.Count;

            // On effectue la distribution (aléatoire) proprement dite
            Distribution_Aleatoire();

            afficherCartes(cartesCachees);
        }

        private void Distribution_Sequentielle()
        {
            int i_carte = 1;

            foreach (Control ctrl in CardsTableLayout.Controls)
            {
                Console.WriteLine(ctrl);
                if (ctrl is PictureBox)
                {
                    PictureBox carte = (PictureBox)ctrl;
                    carte.Image = il_cards_deck.Images[i_carte];
                    i_carte++;
                }
            }
        }

        private void Distribution_Aleatoire()
        {
            // On utilise la LotoMachine pour générer une série aléatoire
            LotoMachine hasard = new LotoMachine(nbCartesDansSabot);

            // On veut une série de nbCartesSurTapis cartes parmi celles 
            // du réservoir
            cartesDistribuees = new int[nbCartesSurTapis];
            int[] tImagesCartes_temp = hasard.TirageAleatoire(nbCartesSurTapis / 2, false).Skip(1).ToArray();

            // On copie deux fois le tableau temporaire pour avoir 4 doublons d'images
            tImagesCartes_temp.CopyTo(cartesDistribuees, 0);
            tImagesCartes_temp.CopyTo(cartesDistribuees, nbCartesSurTapis / 2);

            // On mélange le tableau
            Random rnd = new Random();
            cartesDistribuees = cartesDistribuees.OrderBy(x => rnd.Next()).ToArray();
        }

        private void afficherCartes(int[] cartes)
        {
            for (int i_carte = 0; i_carte < nbCartesSurTapis; i_carte++)
            {
                afficherCarte(i_carte, cartes);
            }
        }

        // Affiche la carte de l'indice donné depuis la liste de cartes donnée
        private void afficherCarte(int indice, int[] cartes)
        {
            //Console.WriteLine(indice);
            foreach (PictureBox pictureBox in CardsTableLayout.Controls)
            {
                if (pictureBox.Name == "pb_0" + (indice + 1))
                {
                    PictureBox carte = pictureBox;
                    int i_image = cartes[indice];
                    carte.Image = il_cards_deck.Images[i_image];
                }
            }
        }

        private void btn_jouer_Click(object sender, EventArgs e)
        {
            if (cartesDistribuees != null)
            {
                afficherCartes(cartesDistribuees);
                this.Refresh();
                Thread.Sleep(5000);
                afficherCartes(cartesCachees);
                inGame = true;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        // Permet de gérer le clic sur une image à l'index donné
        private void clickOnCard(int i)
        {
            if (inGame)
            {
                afficherCarte(i, cartesDistribuees);
                essaiCartes.Add(i);
                cartesRetournees.Add(i);
                if (essaiCartes.Count == 2)
                {
                    if (cartesDistribuees[essaiCartes[0]] != cartesDistribuees[essaiCartes[1]])
                    {
                        MessageBox.Show(null, "Perdu !", "Vous avez perdu !", MessageBoxButtons.OK);
                        inGame = false;
                        afficherCartes(cartesDistribuees);
                    }

                    essaiCartes = new List<int>();
                }
                if (cartesRetournees.Count == CardsTableLayout.Controls.Count)
                {
                    inGame = false;
                    MessageBox.Show(null, "Gagné !","Vous avez gagné !", MessageBoxButtons.OK);
                }
            }
        }

        private void pb_01_Click(object sender, EventArgs e)
        {
            clickOnCard(0);
        }

        private void pb_02_Click(object sender, EventArgs e)
        {
            clickOnCard(1);
        }

        private void pb_03_Click(object sender, EventArgs e)
        {
            clickOnCard(2);
        }

        private void pb_04_Click(object sender, EventArgs e)
        {
            clickOnCard(3);
        }

        private void pb_05_Click(object sender, EventArgs e)
        {
            clickOnCard(4);
        }

        private void pb_06_Click(object sender, EventArgs e)
        {
            clickOnCard(5);
        }

        private void pb_07_Click(object sender, EventArgs e)
        {
            clickOnCard(6);
        }

        private void pb_08_Click(object sender, EventArgs e)
        {
            clickOnCard(7);
        }
    }
}
