using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryProject.Classes
{
         
    internal class StoreSystems
    {
        Form mainform;
        internal User loggedIn;
        List<Game> InCart;

        public StoreSystems(Form parentform, User parentUser, List<Game> storedGames)
        {
            mainform = parentform;
            loggedIn = parentUser;
            InCart = storedGames;
        }


    }
}
