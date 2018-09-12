using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json; // NuGet 

namespace LeaderBoardApp
{
    class Program
    {
        private const string UsersUri = "https://localhost:44365/api/users";

        static void Main()
        {
            Console.WriteLine("Users:");
            IList<User> users = GetUsersAsync().Result;
           
            Console.WriteLine(string.Join("\n", users));
        }

        private static async Task<IList<User>> GetUsersAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                string content = await client.GetStringAsync(UsersUri);
                IList<User> cList = JsonConvert.DeserializeObject<IList<User>>(content);
                return cList;
            }
        }
    }
}
