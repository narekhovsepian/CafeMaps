using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace CafeMaps
{
    class User
    {
        public static bool IsLoggedin { get; set; }
        public static string Usern { get; set; }
        public static List<User> users = new List<User>();
        private string Username { get; set; }
        private string Password { get; set; }

        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public void LogIn()
        {
            string path = @"Data\Users.json";
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                users = JsonConvert.DeserializeObject<List<User>>(json);
            }

            foreach(User user in users)
            {
                if (user.Username == this.Username && user.Password == this.Password)
                {
                    IsLoggedin = true;
                    Usern = user.Username;
                    break;
                }
                else
                {
                    IsLoggedin = false;
                    Usern = "";
                }
            }
        }

        public static void LogOut ()
        {
            IsLoggedin = false;
        }
    }
}
