﻿using System;
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
        User LoggedUser;
        Form parentForm;
        RandomizeGame RG = new RandomizeGame();
        FileAccessModule FAM = new FileAccessModule();
        Boolean Owned = false;


                //set up the form
        internal GamePage(Game freshgame, User logged, Form ParentForm)
        {
            InitializeComponent();
            RegisteredGame = freshgame;
            LoggedUser = logged;
            parentForm = ParentForm;
            initiateGamePage();
        }
        

            //Set up the page
        internal void initiateGamePage()
        {
            this.GameDescriptionBox.DataSource = RG.RandomStory();
            this.GameTitleLabel.Text = RegisteredGame.Name;
            this.GameStudioLabel.Text = RegisteredGame.Studio;
            this.GameRatingLabel.Text = RegisteredGame.Ratings + "%";
            this.GameGenreLabel.Text = RegisteredGame.Genre;
            this.GameDateLabel.Text = RegisteredGame.DatePublished.ToString();
            if (Owned)
            {
                this.GameBuyButton.Text = "Play Game";
            }
        }

            //Make the game be owned
        internal void beOwned()
        {
            Owned = true;
            initiateGamePage();
        }


                            //Listeners
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
                    if (parentForm is WelcomePage)
                    {
                        WelcomePage ChangeForm = (WelcomePage)parentForm;
                        if (!ChangeForm.InCart.Contains(RegisteredGame))
                        {
                            Console.WriteLine("Add to Cart");
                            ChangeForm.InCart.Add(RegisteredGame);
                            ChangeForm.updateCart();
                        }
                    }else if (parentForm is UserProfile)
                    {
                        UserProfile ChangeForm = (UserProfile)parentForm;       //Play game
                        if (!ChangeForm.checkCart(RegisteredGame))
                        {
                            Console.WriteLine("User Played game");

                        }
                    }
                }
            }
        }
    }
}
