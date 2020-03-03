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
        User LoggeedUser;
        Form parentForm;
        RandomizeGame RG = new RandomizeGame();
        FileAccessModule FAM = new FileAccessModule();

        internal GamePage(Game freshgame, User logged, Form ParentForm)
        {
            InitializeComponent();
            RegisteredGame = freshgame;
            LoggeedUser = logged;
            parentForm = ParentForm;
            initiateGamePage();
        }
        
        internal void initiateGamePage()
        {
            this.GameDescriptionBox.DataSource = RG.RandomStory();
            this.GameTitleLabel.Text = RegisteredGame.Name;
            this.GameStudioLabel.Text = RegisteredGame.Studio;
            this.GameRatingLabel.Text = RegisteredGame.Ratings + "%";
            this.GameGenreLabel.Text = RegisteredGame.Genre;
            this.GameDateLabel.Text = RegisteredGame.DatePublished.ToString();
            //Hello there!
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
                if (suspect.Name.Equals(this.GameBuyButton.Name))
                {
                    Console.WriteLine("Add to Cart");
                    if (parentForm is WelcomePage)
                    {
                        WelcomePage ChangeForm = (WelcomePage)parentForm;
                        if (!ChangeForm.InCart.Contains(RegisteredGame))
                        {
                            ChangeForm.InCart.Add(RegisteredGame);
                            ChangeForm.updateCart();
                        }
                    }else if (parentForm is UserProfile)
                    {
                        UserProfile ChangeForm = (UserProfile)parentForm;
                        if (!ChangeForm.checkCart(RegisteredGame))
                        {
                            ChangeForm.addCart(RegisteredGame);
                            ChangeForm.updateCart();
                        }
                    }
                }
            }
        }
    }
}
