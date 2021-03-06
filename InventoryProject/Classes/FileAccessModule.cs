﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InventoryProject.Classes
{
    class FileAccessModule
    {
        //--------------------------------------------------------------//
                            
                            //Properties//
        //Location of the Inventory is in, "InventoryProject/bin/debug"
        public static string TextFile = (@"InventoryList.txt");


        RandomizeGame don = new RandomizeGame();

        List<Game> GameList = new List<Game>();

        //--------------------------------------------------------------//
                                //GAMES//
                                //FILE HANDLER STUFF//
        //Read File and save contents to a gameList
        public void FromGameFile(string textfile,List<Game> games)
        {
            var parent = System.IO.Directory.GetParent(Environment.CurrentDirectory);
            var gameLocation = parent.Parent.FullName + "\\SaveFiles\\GameLibrary\\GameLibrary.txt";

            List<String> TextStrings = new List<String>();
            if (textfile.Length > 0)
            {
                TextStrings = File.ReadAllLines(textfile).ToList();
            }
            else
            {
                TextStrings = File.ReadAllLines(gameLocation).ToList();
            }

            //Goes through every line
            for (int i = 0; i < TextStrings.Count; i++)
            {
                //In the line
                String[] SplitEnds = Regex.Split(TextStrings[i], "/,/");
                //Split the line by "//"
                //We're now in between the "//"
                Game newGame = new Game(SplitEnds[0],                       //name
                                        SplitEnds[1],                       //studio
                                        SplitEnds[2],                       //genre
                                        Convert.ToDouble(SplitEnds[3]),     //price
                                        Convert.ToDateTime(SplitEnds[4]),   //date
                                        Convert.ToInt32(SplitEnds[5]),      //ratings
                                        Convert.ToInt32(SplitEnds[6]));     //items sold
                games.Add(newGame);
            }
        }

        //Save to the game file using the GameList

        public void ToGameFile(string textfile,List<Game> games)
        {
            var parent = System.IO.Directory.GetParent(Environment.CurrentDirectory);
            var gameLocation = parent.Parent.FullName + "\\SaveFiles\\GameLibrary\\GameLibrary.txt";

            if (textfile.Length > 0)
            {
                gameLocation = textfile;
            }
            else
            {
                gameLocation = parent.Parent.FullName + "\\SaveFiles\\GameLibrary\\GameLibrary.txt";
            }

            List<String> TextStrings = new List<String>();
            File.WriteAllText((gameLocation), "");
            for (int i = 0; i < games.Count; i++)
            {
                File.AppendAllText(gameLocation, games[i].saveInfo().ToString() + Environment.NewLine);
            }
        }


        //--------------------------------------------------------------//
                            //MakingGames//
        public void initateGames(int amount, List<Game> games)
        {
            for (int i = 0; i < amount; i++)
            {
                DateTime newDate = new DateTime(2016, 3, 15);
                int ratings = don.RandomRating();
                Game newGame = new Game(don.RandomizeName("PreName") + " " + don.RandomizeName("SufName"),
                                don.RandomizeName("PreStudio") + " " + don.RandomizeName("SufStudio"),
                                don.RandomizeName("Genre"),
                                don.RandomizePrice(),
                                don.RandomizeDate(),
                                ratings,
                                don.RandomSold(ratings));
                games.Add(newGame);
            }
        }



        //--------------------------------------------------------------//
                            //USERS//
                            //FILE HANDLER STUFF//

        //Finding where the User is
        //Version 1
        public string FindUserLocation(string selectUserName)
        {
            var parent = System.IO.Directory.GetParent(Environment.CurrentDirectory);           //get the original root
            var usersLocation = parent.Parent.FullName + "\\SaveFiles\\Users";                  //find the users folder
            String match = "";
            //DirectoryInfo d = new DirectoryInfo(usersLocation);
            //FileInfo[] Files = d.GetFiles("*.txt");         //get all the users registered
            string[] Files = Directory.GetFiles(usersLocation, "User.txt", SearchOption.AllDirectories);     //find all the txt files
            for (int i = 0; i < Files.Length; i++)          //we're in that User file
            {
                var selectedUserLocation = Directory.GetParent(Files[i]);       //The user main folder
                if (selectUserName.Equals(selectedUserLocation.Name, StringComparison.InvariantCultureIgnoreCase))
                {
                    match = selectedUserLocation.FullName;
                }
            }
            return match;
        }


        //Check Username exist
        public Boolean checkUsernameExist(string username)
        {
            var parent = Directory.GetParent(Environment.CurrentDirectory);           //get the original root
            var newUserLocation = parent.Parent.FullName + "\\SaveFiles\\Users\\" + username;                  //find the users folder
            Boolean userexist = false;
            if (Directory.Exists(newUserLocation))
            {
                userexist = true;
            }
            return userexist;
        }



            //Creates the user folder
            public void saveUser(User newuser)
        {
            var parent = Directory.GetParent(Environment.CurrentDirectory);           //get the original root
            var newUserLocation = parent.Parent.FullName + "\\SaveFiles\\Users\\"+newuser.UserName;                  //find the users folder

            if (Directory.Exists(newUserLocation)) {
                Console.WriteLine("Directory already exist");
                using (System.IO.FileStream fs = File.Create(newUserLocation + "\\User.txt"))   //create the user txt initiate it
                {
                    Byte[] userinfo = new UTF8Encoding(true).GetBytes(newuser.SaveInfo());      //make the string to bytes
                    fs.Write(userinfo, 0, userinfo.Length);                                       //make the thing
                }
                using (System.IO.FileStream fs = File.Create(newUserLocation + "\\uGameLibrary.txt"))   //create the gamelibrary txt initiate it
                {
                    String allgames = "";
                    for (int i = 0; i < newuser.gameLibrary.Count; i++)
                    {
                        allgames += newuser.gameLibrary[i].saveInfo() + "\n";
                    }
                    Byte[] gameinfo = new UTF8Encoding(true).GetBytes(allgames);      //make the string to bytes
                    fs.Write(gameinfo, 0, gameinfo.Length);                                       //make the thing
                }
            }
            else
            {
                Directory.CreateDirectory(newUserLocation);                                     //making the folder                                      
                File.Create(newUserLocation + "\\uGameLibrary.txt");                            //make the gamelibrary

                using (System.IO.FileStream fs = File.Create(newUserLocation + "\\User.txt"))   //create the user txt initiate it
                {
                    Byte[] userinfo = new UTF8Encoding(true).GetBytes(newuser.SaveInfo());      //make the string to bytes
                    fs.Write(userinfo,0,userinfo.Length);                                       //make the thing
                }

                Console.WriteLine("We created the directory. "+newUserLocation);
            }
        }

        //Find logged user Version2
        //find the user location. Then use readUserFile to make a user from the textfile. then compare the user and pass
        //stricter check
        public Boolean checkUser(string username, string password)
        {
            var parent = System.IO.Directory.GetParent(Environment.CurrentDirectory);           //get the original root
            var usersLocation = parent.Parent.FullName + "\\SaveFiles\\Users";                  //find the users folder
            Boolean isIt = false;

            string[] Files = Directory.GetFiles(usersLocation, "User.txt", SearchOption.AllDirectories);     //find all the txt files
            for (int i = 0; i < Files.Length; i++)          //we're in that User file
            {
                var selectedUserLocation = Directory.GetParent(Files[i]);       //The user main folder
                if (username.Equals(selectedUserLocation.Name, StringComparison.InvariantCultureIgnoreCase))
                {
                    //we're in the matched person.
                    //now deconstruct it lol
                    User compare = readUserFile(Files[i]);
                    if (compare.UserName.Equals(username,StringComparison.InvariantCultureIgnoreCase)&&
                        (compare.Password.Equals(password)))
                        //we're making the username non case sensitive
                        //but the password is case sensitive
                    {
                        isIt = true;
                    }
                }
            }
            return isIt;
        }


        //read the textfile and then spit out a user
        public User readUserFile(string location)
        {
            User suspect = new User("","",""); //make generic, because we're going to change it
            List<String> TextStrings = new List<String>();
            TextStrings = File.ReadAllLines(location).ToList();

            for (int i = 0; i < TextStrings.Count; i++)
            {
                //In the line
                String[] SplitEnds = Regex.Split(TextStrings[i], "/,/");    //split that thing up
                suspect.IGName = SplitEnds[0];
                suspect.UserName = SplitEnds[1];
                suspect.Password = SplitEnds[2];
                suspect.Funds = Convert.ToDouble(SplitEnds[3]);
            }
           return suspect;
        }

        //Make the game library for the user

        public void readpersonalGameLibrary(User suspect)
        {
            string suspectLocation = FindUserLocation(suspect.UserName);

            FromGameFile(suspectLocation+"\\uGameLibrary.txt",suspect.gameLibrary);

        }


    }
}
