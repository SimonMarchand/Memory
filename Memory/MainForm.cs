using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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


        public MainForm()
        {
            InitializeComponent();
        }

        private void btn_retourner_Click(object sender, EventArgs e)
        {

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
            Console.WriteLine(hasard);
            
            // On veut une série de nbCartesSurTapis cartes parmi celles 
            // du réservoir
            cartesDistribuees = new int[nbCartesSurTapis];
            int[] tImagesCartes_temp = hasard.TirageAleatoire(nbCartesSurTapis/2, false).Skip(1).ToArray();

            // On copie deux fois le tableau temporaire pour avoir 4 doublons d'images
            tImagesCartes_temp.CopyTo(cartesDistribuees, 0);
            tImagesCartes_temp.CopyTo(cartesDistribuees, nbCartesSurTapis/2);

            // On mélange le tableau
            Random rnd = new Random();
            cartesDistribuees = cartesDistribuees.OrderBy(x => rnd.Next()).ToArray();

            // Affectation des images aux picturebox
            afficherCartes(cartesDistribuees);
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
            PictureBox carte = (PictureBox)CardsTableLayout.Controls[indice];
            int i_image = cartes[indice];
            carte.Image = il_cards_deck.Images[i_image];
        }

        private void btn_jouer_Click(object sender, EventArgs e)
        {
            /* TEST DE DLLLOTTO
            // On utilise la LotoMachine pour générer une série aléatoire
            // On fixe à 49 le nombre maxi que retourne la machine
            LotoMachine hasard = new LotoMachine(49);
            // On veut une série de 6 numéros distincts parmi 49 (comme quand on joue au loto)
            int[] tirageLoto = hasard.TirageAleatoire(6, false);
            // false veut dire pas de doublon : une fois qu'une boule est sortie, 
            // elle ne peut pas sortir à nouveau ;-)
            // La série d'entiers retournée par la LotoMachine correspond au loto
            // affiché sur votre écran TV ce soir...
            string grilleLoto = "* ";
            for (int i = 1; i <= 6; i++)
            {
                grilleLoto = grilleLoto + tirageLoto[i] + " * ";
            }
            MessageBox.Show(grilleLoto, "Tirage du LOTO ce jour !");
            */
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        /*
        private void pb_XX_Click(object sender, EventArgs e)
        {
            PictureBox carte;
            int i_carte, i_image;
            //if (Image_1 == null)
            //    MessageBox.Show("L'image 1 n'est pas référencée");
            //if (Image_2 == null)
            //    MessageBox.Show("L'image 2 n'est pas référencée");

            if (nb_cartes < 2)
            {
                carte = (PictureBox)sender;
                i_carte = Convert.ToInt32(carte.Tag);
                i_image = tapisCARTES[i_carte];
                carte.Image = imgListe.Images[i_image];
                if (i_image == i_hasard)
                {
                    MessageBox.Show("Bravo !");
                }
                else
                {
                    MessageBox.Show("DOMMAGE !");
                }
                if (nb_cartes == 0)
                {
                    Image_1 = carte;
                }
                if (nb_cartes == 1)
                {
                    Image_2 = carte;
                }
                nb_cartes++;

            }
            else
            {
                MessageBox.Show("Deux cartes sont déjà retournées !");
                RetournerLesCartes();
                nb_cartes = 0;
                Image_1 = null;
                Image_2 = null;
            }

        }*/

    }
}
