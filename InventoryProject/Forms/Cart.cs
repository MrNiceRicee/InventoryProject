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
    public partial class GameCart : Form
    {

        List<Game> checkout;
        User loggedBuyer;

        public GameCart()
        {
            InitializeComponent();
        }

        internal void setItems(List<Game> InCartGames, User Buyer)
        {
            checkout = InCartGames;
            loggedBuyer = Buyer;
        }
    }
}
