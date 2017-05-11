using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeMaps
{
    class Review
    {
        public int CafeID { get; set; }
        public string UserID { get; set; }
        public int Rate { get; set; }
        public string Comment { get; set; }

        public Review() { }

        public Review(int cafeID, string userID, int rate, string comment)
        {
            CafeID = cafeID;
            UserID = userID;
            Rate = rate;
            Comment = comment;
        }

        //public override string ToString()
        //{
            // return cafeID + " From: " + From + " - To: " + To;
        //}
    }
}
