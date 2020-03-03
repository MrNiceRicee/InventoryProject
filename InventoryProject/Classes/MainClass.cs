using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InventoryProject.Classes
{
         
    internal class MainClass
    {
        public User LoggedUser = new User("","","");


        public User getLoggedUser()
        {
            return LoggedUser;
        }

        public void setLoggedUser(User logged)
        {
            LoggedUser = logged;
        }
    }
}
