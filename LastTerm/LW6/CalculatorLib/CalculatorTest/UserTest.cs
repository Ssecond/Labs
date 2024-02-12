using CalculatorLib;
using System.Diagnostics;

namespace CalculatorTest
{
    [TestClass]
    public class UserTest
    {
        private List<User> users;
        [TestMethod]
        [TestInitialize]
        public void Initialize()
        {
            users = new List<User>();
            Random rand = new Random();

            for (short i = 0; i < rand.Next(1, short.MaxValue); i++)
            {
                string password = string.Empty;
                for (int j = 0; j < rand.Next(8, 100 + 1); j++)
                    password += (char)rand.Next('A', 'z' + 1);

                string login = string.Empty;
                for (int j = 0; j < rand.Next(8, 100 + 1); j++)
                    login += (char)rand.Next('A', 'z' + 1);

                users.Add(new User(i, login, password, rand.Next(0, 1 + 1) == 1));
            }

            Debug.WriteLine("В коллекции создано User'ов: " + users.Count);
        }
        [TestMethod]
        public void UserIsNotNull()
        {
            foreach (User user in users)
                Assert.AreNotEqual(null, user);
        }
        [TestMethod]
        public void UniqueUsers()
        {
            foreach (User comparingUser in users)
                foreach (User user in users)
                    if (comparingUser != user)
                        Assert.AreNotEqual(comparingUser.Login, user.Login);
        }
        [TestMethod]
        public void SearachForUsers()
        {
            Random rand = new Random();
            string password = string.Empty;
            for (int j = 0; j < rand.Next(8, 100 + 1); j++)
                password += (char)rand.Next('A', 'z' + 1);

            string login = string.Empty;
            for (int j = 0; j < rand.Next(8, 100 + 1); j++)
                login += (char)rand.Next('A', 'z' + 1);

            User userToSearch = new User(0, login, password, rand.Next(0, 1 + 1) == 1);

            foreach (User user in users)
                if (userToSearch.Login == user.Login)
                    Assert.Fail();
        }
    }
}
