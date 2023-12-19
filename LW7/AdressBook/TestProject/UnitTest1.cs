using PhoneNumbers;
using System.Xml;
using System.Diagnostics;
using System.Net.Mail;
using System.Xml.Linq;

namespace TestProject
{
    [TestClass]
    public class UnitTest1
    {
        public static IEnumerable<object[]> Phones
        {
            get
            {
                var doc = new XmlDocument();
                doc.Load("Phones.xml");
                var nodes = doc.SelectNodes("//testcase");
                foreach (XmlNode node in nodes)
                {
                    var phoneNumber = Convert.ToString(node.SelectSingleNode("phoneNumber").InnerText);
                    yield return new object[] { phoneNumber };
                }
            }
        }
        [TestMethod]
        [DynamicData(nameof(Phones), DynamicDataSourceType.Property)]
        public void PhoneInputFormatCheck(string phoneNumber)
        {
            try
            {
                PhoneNumberUtil phoneNumberUtil = PhoneNumberUtil.GetInstance();
                Debug.WriteLine(phoneNumber);
                Assert.IsTrue(phoneNumberUtil.IsValidNumber(phoneNumberUtil.Parse(phoneNumber, "RU")), "It's a number but wrong format.");
            }
            catch(NumberParseException e)
            {
                Assert.Fail(e.Message);
            }
        }

        public static IEnumerable<object[]> Emails
        {
            get
            {
                var doc = new XmlDocument();
                doc.Load("Emails.xml");
                var nodes = doc.SelectNodes("//testcase");
                foreach (XmlNode node in nodes)
                {
                    var email = Convert.ToString(node.SelectSingleNode("email").InnerText);
                    yield return new object[] { email };
                }
            }
        }
        [TestMethod]
        [DynamicData(nameof(Emails), DynamicDataSourceType.Property)]
        public void IfTheNameSurnamePAtronimicStartsWithUpperCase(string email)
        {
            try
            {
                new MailAddress(email);
            }
            catch (FormatException e)
            {
                Assert.Fail(e.Message);
            }
        }


        public static IEnumerable<object[]> Nsp
        {
            get
            {
                var doc = new XmlDocument();
                doc.Load("Nsp.xml");
                var nodes = doc.SelectNodes("//testcase");
                foreach (XmlNode node in nodes)
                {
                    var name = Convert.ToString(node.SelectSingleNode("name").InnerText);
                    var surname = Convert.ToString(node.SelectSingleNode("surname").InnerText);
                    var patronimic = Convert.ToString(node.SelectSingleNode("patronimic").InnerText);
                    yield return new object[] { name, surname, patronimic };
                }
            }
        }
        [TestMethod]
        [DynamicData(nameof(ADD), DynamicDataSourceType.Property)]
        public void NameSurnamePatronimicUpperCaseStart(string name, string surname, string patronimic)
        {
            try
            {
                Assert.IsTrue(AdressBook.Program.StartsWithUppercase(name) && AdressBook.Program.StartsWithUppercase(surname) && AdressBook.Program.StartsWithUppercase(patronimic));
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        public static IEnumerable<object[]> ADD
        {
            get
            {
                var doc = new XmlDocument();
                doc.Load("ADD.xml");
                var nodes = doc.SelectNodes("//testcase");
                foreach (XmlNode node in nodes)
                {
                    var name = Convert.ToString(node.SelectSingleNode("name").InnerText);
                    var surname = Convert.ToString(node.SelectSingleNode("surname").InnerText);
                    var patronimic = Convert.ToString(node.SelectSingleNode("patronimic").InnerText);
                    var phoneNumber = Convert.ToString(node.SelectSingleNode("phoneNumber").InnerText);
                    var email = Convert.ToString(node.SelectSingleNode("email").InnerText);
                    yield return new object[] { name, surname, patronimic, phoneNumber, email };
                }
            }
        }
        [TestMethod]
        [DynamicData(nameof(ADD), DynamicDataSourceType.Property)]
        public void AddCheck(string name, string surname, string patronimic, string phoneNumber, string email)
        {
            try
            {
                List<AdressBook.AdressBookRecord> adressBookRecords = new List<AdressBook.AdressBookRecord>();
                int count = adressBookRecords.Count;
                AdressBook.Program.Add(adressBookRecords, new string[] { "add", name, surname, patronimic, phoneNumber, email, "no" });
                AdressBook.Program.Add(adressBookRecords, new string[] { "add", name, surname, patronimic, phoneNumber, email });
                AdressBook.Program.Add(adressBookRecords, new string[] { "add", name, surname, patronimic, phoneNumber, email, "yes" });
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }


        public static IEnumerable<object[]> CHANGE
        {
            get
            {
                var doc = new XmlDocument();
                doc.Load("CHANGE.xml");
                var nodes = doc.SelectNodes("//testcase");
                foreach (XmlNode node in nodes)
                {
                    var name = Convert.ToString(node.SelectSingleNode("name").InnerText);
                    var surname = Convert.ToString(node.SelectSingleNode("surname").InnerText);
                    var patronimic = Convert.ToString(node.SelectSingleNode("patronimic").InnerText);
                    var phoneNumber = Convert.ToString(node.SelectSingleNode("phoneNumber").InnerText);
                    var email = Convert.ToString(node.SelectSingleNode("email").InnerText);
                    yield return new object[] { name, surname, patronimic, phoneNumber, email };
                }
            }
        }
        [TestMethod]
        [DynamicData(nameof(CHANGE), DynamicDataSourceType.Property)]
        public void ChangeCheck(string name, string surname, string patronimic, string phoneNumber, string email)
        {
            try
            {
                List<AdressBook.AdressBookRecord> adressBookRecords = new List<AdressBook.AdressBookRecord>();
                AdressBook.Program.Add(adressBookRecords, new string[] { "add", "ToChangeName", "ToChangeSurname", "ToChangePatronimmic", "+7 999 999 99 99", "toChangeEmail@mail.com" });
                AdressBook.Program.Change(adressBookRecords, new string[] { "change", "1", name, surname, patronimic, phoneNumber, email, "no" });
                AdressBook.Program.Change(adressBookRecords, new string[] { "change", "1", name, surname, patronimic, phoneNumber, email });
                AdressBook.Program.Change(adressBookRecords, new string[] { "change", "1", name, surname, patronimic, phoneNumber, email, "yes" });
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }


        [TestMethod]
        public void DeleteeCheck()
        {
            try
            {
                List<AdressBook.AdressBookRecord> adressBookRecords = new List<AdressBook.AdressBookRecord>();
                AdressBook.Program.Add(adressBookRecords, new string[] { "add", "ToChangeName", "ToChangeSurname", "ToChangePatronimmic", "+7 999 999 99 99", "toChangeEmail@mail.com" });
                AdressBook.Program.Delete(adressBookRecords, new string[] { "delete", "1"});
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
    }
}