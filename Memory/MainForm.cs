using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Memory
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btn_retourner_Click(object sender, EventArgs e)
        {

        }

        private void btn_distribuer_Click(object sender, EventArgs e)
        {
            Distribution_Sequentielle();
        }

        private void Distribution_Sequentielle()
        {
            int i_carte = 1;

            foreach (Control ctrl in CardsTableLayout.Controls)
            {
                if (ctrl is PictureBox)
                {
                    PictureBox carte = (PictureBox)ctrl;
                    carte.Image = il_cards_deck.Images[i_carte];
                    i_carte++;
                }
            }
        }

    }
}
