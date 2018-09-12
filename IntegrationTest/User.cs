using System;

namespace LeaderBoardApp
{
    class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHashed { get; set; }
        public DateTime Enrolled { get; set; }

        public override string ToString()
        {
            return Id + " " + Username + " " + PasswordHashed + " " + Enrolled;
        }
    }
}
