namespace AdressBook
{
    internal class AdressBookRecord
    {
        private string name, surname, patronimic;
        private string phoneNumber;
        private string email;
        private bool isBlocked;

        public AdressBookRecord(string name, string surname, string patronimic, string phoneNumber, string email, bool isBlocked = false)
        {
            this.name = name;
            this.surname = surname;
            this.patronimic = patronimic;
            this.phoneNumber = phoneNumber;
            this.email = email;
            this.isBlocked = isBlocked;
        }
        public void ChangeTo(string name, string surname, string patronimic, string phoneNumber, string email, bool isBlocked = false)
        {
            this.name = name;
            this.surname = surname;
            this.patronimic = patronimic;
            this.phoneNumber = phoneNumber;
            this.email = email;
            this.isBlocked = isBlocked;
        }
        internal void Block() => isBlocked = true;
        internal void UnBlock() => isBlocked = false;
        public override string ToString()
        {
            return string.Format("ФИО: {0} {1} {2}\nТелефон: {3}\nEmail: {4}\nЗаблокирован: {5}", surname, name, patronimic, phoneNumber, email, isBlocked ? "да" : "нет");
        }
    }
}
