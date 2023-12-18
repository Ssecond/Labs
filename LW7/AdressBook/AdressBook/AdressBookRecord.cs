using PhoneNumbers;
using System.Net.Mail;

namespace AdressBook
{
    internal class AdressBookRecord
    {
        private string name, surname, patronimic;
        private PhoneNumber phoneNumber;
        private MailAddress email;
        private bool isBlocked;

        public AdressBookRecord(string name, string surname, string patronimic, PhoneNumber phoneNumber, MailAddress email, bool isBlocked = false)
        {
            this.name = name;
            this.surname = surname;
            this.patronimic = patronimic;
            this.phoneNumber = phoneNumber;
            this.email = email;
            this.isBlocked = isBlocked;
        }
        public void ChangeTo(string name, string surname, string patronimic, PhoneNumber phoneNumber, MailAddress email, bool isBlocked = false)
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
            PhoneNumberUtil phoneNumberUtil = PhoneNumberUtil.GetInstance();
            return string.Format("ФИО: {0} {1} {2}\nТелефон: {3}\nEmail: {4}\nЗаблокирован: {5}", surname, name, patronimic, phoneNumberUtil.Format(phoneNumber, PhoneNumberFormat.INTERNATIONAL), email, isBlocked ? "да" : "нет");
        }
    }
}
