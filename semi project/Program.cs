using System;
using System.Data;
using System.Linq;
using ContactBusinessLayer;
using clTextFormat;
using System.Collections.Generic;

namespace semi_project
{
    internal class Program
    {

        #region functions


        #region CRUD Contact
        public static void FindContact()
        {
            try
            {
                int contactId = clsTextFormat.GetUserNumber("Enter Contact ID : ", "Please enter a valid number");
                var cls = ContactBusinessLayer.ContactBusinessLayer.FindContact(contactId);

                if (cls == null)
                {
                    Console.WriteLine($"Contact with ID {contactId} not found.");
                    return;
                }

                Console.WriteLine("\nContact Details:");
                Console.WriteLine(new string('-', 100));
                Console.WriteLine($"{"ContactId ",-12}{"FirstName ",-13}{"LastName",-14}" +
                                  $"{"Address",-15}{"Phone",-16}{"Email",-17}{"CountryId",-18}");

                Console.WriteLine($"{cls.ContactId,-12}{cls.FirstName,-13}{cls.LastName,-14}" +
                                  $"{cls.Address,-15}{cls.Phone,-16}{cls.Email,-17}{cls.CountryId,-18}");

                Console.WriteLine(new string('-', 100));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }





        public static void AddContact()
        {


            ContactBusinessLayer.ContactBusinessLayer cls = new ContactBusinessLayer.ContactBusinessLayer();

            cls.FirstName = clsTextFormat.GetUsertext("enter firstName", "Enter string");
            cls.LastName = clsTextFormat.GetUsertext("enter LastName", "Enter string");
            cls.Email = clsTextFormat.GetUsertext("enter email", "Enter string");
            cls.Phone = clsTextFormat.GetUserNumber("enter phone", "Enter numbers").ToString();
            cls.Phone = clsTextFormat.GetUsertext("enter address", "Enter string");
            cls.ImagePath = clsTextFormat.GetUsertext("enter imagePath", "Enter string");
            cls.CountryId = clsTextFormat.GetUserNumber("enter countryId", "Enter string");
            Console.Write("Enter data (yyyy-mm-dd) أو اضغط Enter esc ");
            string dobInput = Console.ReadLine();
            if (!string.IsNullOrEmpty(dobInput))
            {
                cls.DateOfBirth = DateTime.Parse(dobInput);
            }
            if (cls.Save())
            {
                Console.WriteLine("Contact AddContacted Successfully with id=" + cls.ContactId);



            }
            else { Console.WriteLine("Contact Added UnSuccessfully with id=" + cls.ContactId); }


        }

        public static void UpdataContact()
        {
            int ID = clsTextFormat.GetUserNumber("Enter ID Contact ", "Enter Number Not string");

            if (IsExistContact(ID))
            {

                ContactBusinessLayer.ContactBusinessLayer cls = ContactBusinessLayer.ContactBusinessLayer.FindContact(ID);
                if (cls != null)
                {
                    Console.WriteLine($"{"ContactId ",-12}{"FirstName ",-13}{"LastName",-14}" +
                                      $"{"Address",-15}{"Phone",-16}{"Email",-17}{"CountryId",-18}");

                    Console.WriteLine($"{cls.ContactId,-12}{cls.FirstName,-13}{cls.LastName,-14}" +
                                      $"{cls.Address,-15}{cls.Phone,-16}{cls.Email,-17}{cls.CountryId,-18}");

                    cls.FirstName = clsTextFormat.GetUsertext("enter firstName", "Enter string");
                    cls.LastName = clsTextFormat.GetUsertext("enter LastName", "Enter string");
                    cls.Email = clsTextFormat.GetUsertext("enter email", "Enter string");
                    cls.Phone = clsTextFormat.GetUserNumber("enter phone", "Enter numbers").ToString();
                    cls.Phone = clsTextFormat.GetUsertext("enter address", "Enter string");
                    cls.ImagePath = clsTextFormat.GetUsertext("enter imagePath", "Enter string");
                    cls.CountryId = clsTextFormat.GetUserNumber("enter countryId", "Enter string");
                    Console.Write("Enter data (yyyy-mm-dd) أو اضغط Enter esc ");
                    string dobInput = Console.ReadLine();
                    if (!string.IsNullOrEmpty(dobInput))
                    {
                        cls.DateOfBirth = DateTime.Parse(dobInput);
                    }
                    if (cls.Save())
                    {

                        Console.WriteLine("Contact Updata Successfully with id=" + cls.ContactId);

                    }
                }
            }

            else
                Console.WriteLine("ID not found");
        }


        public static void DeleteContact()
        {

            int ID = clsTextFormat.GetUserNumber("enter ID ", "Enter Number Not string");

            if (IsExistContact(ID))
            {
                ContactBusinessLayer.ContactBusinessLayer cls = ContactBusinessLayer.ContactBusinessLayer.FindContact(ID);
                if (cls != null)
                {
                    Console.WriteLine($"{"ContactId ",-12}{"FirstName ",-13}{"LastName",-14}" +
                                      $"{"Address",-15}{"Phone",-16}{"Email",-17}{"CountryId",-18}");

                    Console.WriteLine($"{cls.ContactId,-12}{cls.FirstName,-13}{cls.LastName,-14}" +
                                      $"{cls.Address,-15}{cls.Phone,-16}{cls.Email,-17}{cls.CountryId,-18}");

                    bool IsDelet = ContactBusinessLayer.ContactBusinessLayer.Delete(ID);
                    if (IsDelet)
                    {
                        Console.WriteLine("delet sucssful");

                    }
                    else { Console.WriteLine("delet unsucssful"); }
                }
            }

            else
                Console.WriteLine("ID not found");

        }



        public static void listContact()
        {



            DataTable contactsTable = ContactBusinessLayer.ContactBusinessLayer.GetAllContacts();

            if (contactsTable != null && contactsTable.Rows.Count > 0)
            {
                Console.WriteLine("\n contact List:");
                Console.WriteLine(new string('-', 120));

                // عرض عناوين الأعمدة
                foreach (DataColumn column in contactsTable.Columns)
                {
                    Console.Write($"{column.ColumnName,-20}");
                }
                Console.WriteLine();
                Console.WriteLine(new string('-', 120));

                // عرض البيانات
                foreach (DataRow row in contactsTable.Rows)
                {
                    foreach (var item in row.ItemArray)
                    {
                        Console.Write($"{item,-20}");
                    }
                    Console.WriteLine();
                }

                Console.WriteLine(new string('-', 120));
            }
            else
            {
                Console.WriteLine("non");
            }
        }

        public static bool IsExistContact(int Id)
        {
            return ContactBusinessLayer.ContactBusinessLayer.IsContactExistById(Id);
        }



        #endregion

        #region CRUD Country
        static void FindCountryByID(int ID)

        {
            ClsContactBussineslayer.clsCountry cls = ClsContactBussineslayer.clsCountry.Find(ID);

            if (cls != null)
            {

                Console.WriteLine($"{"Name: ",-10},{"Code: ",-15},{"PhoneCode:",-20}");
                Console.WriteLine($"{cls.CountryName,-10},{cls.Code,-15},{cls.PhoneCode,-20}");


            }

            else
            {
                Console.WriteLine("Country [" + ID + "] Not found!");
            }
        }
        static void AddNewCountry()


        {
            ClsContactBussineslayer.clsCountry cls = new ClsContactBussineslayer.clsCountry();
            cls.CountryName = clsTextFormat.GetUsertext("enter Country Name", "Enter string");
            cls.PhoneCode = clsTextFormat.GetUsertext("enter Phone Code", "Enter string");
            cls.Code = clsTextFormat.GetUsertext("enter  Code", "Enter string");

            string dobInput = Console.ReadLine();


            if (cls.Save())
            {

                Console.WriteLine("Country Added Successfully with id=" + cls.ID);
            }

        }

        static void UpdateCountry(int ID)

        {
            if (ClsContactBussineslayer.clsCountry.isCountryExist(ID))
            {


                ClsContactBussineslayer.clsCountry cls = ClsContactBussineslayer.clsCountry.Find(ID);

                if (cls != null)
                {
                    cls.CountryName = clsTextFormat.GetUsertext("enter Country Name", "Enter string");
                    cls.PhoneCode = clsTextFormat.GetUserNumber("enter Phone Code", "Enter string").ToString();
                    cls.Code = clsTextFormat.GetUserNumber("enter  Code", "Enter string").ToString();


                    if (cls.Save())
                    {

                        Console.WriteLine("Country updated Successfully ");
                    }

                }
                else
                {
                    Console.WriteLine("Country is you want to update is Not found!");
                }
            }

            else
                Console.WriteLine("Country [" + ID + "] Not found!");
        }

        static void DeleteCountry(int ID)

        {

            if (ClsContactBussineslayer.clsCountry.isCountryExist(ID))

                if (ClsContactBussineslayer.clsCountry.DeleteCountry(ID))

                    Console.WriteLine("Country Deleted Successfully.");
                else
                    Console.WriteLine("Faild to delete Country.");

            else
                Console.WriteLine("Faild to delete: The Country with id = " + ID + " is not found");

        }

        static void ListCountries()
        {
            try
            {
                DataTable dataTable = ClsContactBussineslayer.clsCountry.GetAllCountries();

                if (dataTable.Rows.Count == 0)
                {
                    Console.WriteLine("No countries found.");
                    return;
                }

                // استخدام مكتبة مثل ConsoleTables لتحسين العرض
                // Console.WriteLine("\nCountries List:");
                Console.WriteLine(new string('-', 70));
                Console.WriteLine($"{"ID",-5} {"Name",-20} {"Code",-10} {"Phone Code",-15}");
                Console.WriteLine(new string('-', 70));

                foreach (DataRow row in dataTable.Rows)
                {
                    Console.WriteLine($"{row["CountryID"],-5} {row["CountryName"],-20} {row["Code"],-10} {row["PhoneCode"],-15}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error listing countries: {ex.Message}");
            }
        }



        #endregion
        /// <summary>
        /// تنفيذ دالة معينة مع إمكانية تكرارها حسب رغبة المستخدم
        /// </summary>
        /// <param name="operationTitle">عنوان العملية المعروض للمستخدم</param>
        /// <param name="operation">الدالة التي تمثل العملية المطلوب تنفيذها</param>
        /// <remarks>
        /// هذه الدالة تقوم بما يلي:
        /// 1. عرض عنوان العملية للمستخدم
        /// 2. تنفيذ العملية المطلوبة
        /// 3. سؤال المستخدم إذا كان يرغب في تكرار العملية
        /// 4. تكرار العملية طالما المستخدم يرغب بذلك
        /// </remarks>
        /// <example>
        /// كيفية استخدام الدالة:
        /// <code>
        /// HandleOperationWithRepeat("بحث جهات اتصال", FindContacts);
        /// </code>
        /// </example>
        private static void HandleOperationWithRepeat(string operationTitle, Action operation)
        {
            clsTextFormat.PrintCenteredMenu(operationTitle, clsTextFormat.title);
            Console.ForegroundColor = ConsoleColor.White;

            char continueOperation = 'y';
            while (continueOperation == 'y' || continueOperation == 'Y')
            {
                operation.Invoke();
                Console.Write($"\nDo you want to repeat {operationTitle}? (y/n): ");
                continueOperation = Console.ReadKey().KeyChar;
                Console.WriteLine();
            }
        }
        public static void ProcessSelectedOption(clsTextFormat.MenuOptions option)
        {


            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            try
            {
                switch (option)
                {
                    case clsTextFormat.MenuOptions.FindContactContact:
                        HandleOperationWithRepeat("Find Contact", FindContact);
                        break;

                    case clsTextFormat.MenuOptions.AddNewContact:

                        HandleOperationWithRepeat("Add Contact", AddContact);
                        break;

                    case clsTextFormat.MenuOptions.UpdateContact:

                        HandleOperationWithRepeat("Updata Contact", UpdataContact);
                        break;

                    case clsTextFormat.MenuOptions.DeleteContact:

                        HandleOperationWithRepeat(" Delete Contact", DeleteContact);
                        break;

                    case clsTextFormat.MenuOptions.ListContacts:

                        HandleOperationWithRepeat(" list Contact", listContact);
                        break;
                    case clsTextFormat.MenuOptions.AddNewCountry:

                        HandleOperationWithRepeat(" Add New Country", AddNewCountry);
                        break;

                    case clsTextFormat.MenuOptions.FindContactCountry:
                        clsTextFormat.PrintCenteredMenu("find for country", clsTextFormat.title);
                        FindCountryByID(clsTextFormat.GetUserNumber("Enter Id country", "number not text"));

                        break;
                    case clsTextFormat.MenuOptions.UpdateCountry:
                        HandleOperationWithRepeat("Update Country",
                    () => UpdateCountry(clsTextFormat.GetUserNumber("Enter Country ID", "Please enter a valid number")));

                        break;
                    case clsTextFormat.MenuOptions.DeleteCountry:
                        HandleOperationWithRepeat("Delete Country", () =>
                         DeleteCountry(clsTextFormat.GetUserNumber("Enter Id country", "number not text")));

                        break;
                    case clsTextFormat.MenuOptions.ListCountry:
                        HandleOperationWithRepeat("List Countries", ListCountries);

                        break;
                    case clsTextFormat.MenuOptions.Exit:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid option selected.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            finally { Console.ResetColor(); }
        }
        public static void Run()
        {

            while (true)
            {

                clsTextFormat.PrintCenteredMenu("contact systeam", clsTextFormat.title);
                clsTextFormat.PrintCenteredMenu("Please choose from menu", clsTextFormat.menuItems);
                var choice = clsTextFormat.GetUserChoice("\nEnter your choice (1-11):", "  Error: Please enter a number between 1 and 11 only!");
                ProcessSelectedOption(choice);


                Console.WriteLine("\nEnter any key...");
                Console.ReadKey();
                Console.Clear();
            }
        }








        #endregion

        static void Main()
        {


            Run();

            Console.ReadLine();
        }


    }
}