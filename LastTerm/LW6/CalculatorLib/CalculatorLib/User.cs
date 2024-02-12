namespace CalculatorLib
{
    public class User
    {
        private int id;
        private string login;
        private string password;
        private bool isBlocked;

        public User(int id, string login, string password, bool isBlocked)
        {
            this.id = id;
            this.login = login;
            this.password = password;
            this.isBlocked = isBlocked;
        }

        public string Login { get => login; set => login = value; }
        public bool IsBlocked { get => isBlocked; set => isBlocked = value; }
    }
}
