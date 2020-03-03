using System;
using InventoryProject.Classes;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryProject.Forms
{
    public partial class GamePage : Form
    {
        Game RegisteredGame;
        RandomizeGame RG = new RandomizeGame();

        public GamePage()
        {
            InitializeComponent();            
        }
        
        internal void initiateGamePage(Game freshgame)
        {
            RegisteredGame = freshgame;
            /*            List<String> fromStory = new List<string>(RG.RandomStory());
                        for (int i = 0; i < fromStory.Count; i++)
                        {
                            this.GameDescriptionBox.Items.Add(fromStory[i]);
                        }*/

            this.GameDescriptionBox.DataSource = RG.RandomStory();
            this.GameTitleLabel.Text = freshgame.Name;
            this.GameStudioLabel.Text = freshgame.Studio;
            this.GameRatingLabel.Text = freshgame.Ratings + "%";
            this.GameGenreLabel.Text = freshgame.Genre;
            this.GameDateLabel.Text = freshgame.DatePublished.ToString();
        }

        private void CustomItem_Hover(object sender, EventArgs e)
        {
            if (sender is Panel)
            {
                Panel suspect = (Panel)sender;
                suspect.BackColor = Color.FromArgb(80, 100, 120);
            }
            else if (sender is Label)
            {
                Label suspect = (Label)sender;
                suspect.BackColor = Color.FromArgb(80, 100, 120);
            }
            else if (sender is Button)
            {
                Button suspect = (Button)sender;
                suspect.BackColor = Color.FromArgb(80, 100, 120);
            }
        }

        private void CustomItem_MouseLeave(object sender, EventArgs e)
        {
            if (sender is Panel)
            {
                Panel suspect = (Panel)sender;
                suspect.BackColor = Color.FromArgb(10, 18, 29);
            }
            else if (sender is Label)
            {
                Label suspect = (Label)sender;
                suspect.BackColor = Color.FromArgb(0);
            }
            else if (sender is Button)
            {
                Button suspect = (Button)sender;
                suspect.BackColor = Color.FromArgb(10, 18, 29);
            }
        }
        private void CustomItem_MouseClick(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                Button suspect = (Button)sender;
                if (suspect.Name.Equals(this.GameBuyButton))
                {
                    Console.WriteLine("Add to Cart");
                }
            }
        }
    }
}
