using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using LeaderBoardApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace IntegrationTest
{
    [TestClass]
    public class Test1
    {
        private const string UsersUri = "https://localhost:44365/api/users";
        [TestMethod]
        public void TestMethod1()
        {
            IList<User> users = GetUsersAsync().Result;
            Assert.AreEqual(2, users.Count);

            User user = DeleteUser(1).Result;
            Assert.AreEqual("anders", user.Username);

            users = GetUsersAsync().Result;
            Assert.AreEqual(1, users.Count);
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

        private static async Task<User> DeleteUser(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.DeleteAsync(UsersUri + "/" + id);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    User user = JsonConvert.DeserializeObject<User>(content);
                    return user;
                }
                throw new Exception(response.StatusCode + " " + response.ReasonPhrase);
            }
        }
    }
}
