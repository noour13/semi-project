using System;
using System.Data;
using ContactDataAccess;

namespace ContactBusinessLayer
{
    public class ContactBusinessLayer
    {

        public enum Mode { AddNew = 0, Update = 1 };
        public Mode CurrentMode { get; private set; }

        #region Properties
        public int ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime? DateOfBirth { get; set; }  // Nullable DateTime
        public int CountryId { get; set; }
        public string ImagePath { get; set; }
        #endregion




        #region Constructors
        public ContactBusinessLayer()
        {
            this.CurrentMode = Mode.AddNew;
            this.ContactId = -1;
            this.FirstName = string.Empty;
            this.LastName = string.Empty;
            this.Email = string.Empty;
            this.Phone = string.Empty;
            this.Address = string.Empty;
            this.DateOfBirth = null;
            this.CountryId = -1;
            this.ImagePath = string.Empty;
        }

        private ContactBusinessLayer(int contactId, string firstName, string lastName,
                                   string email, string phone, string address,
                                   DateTime? dateOfBirth, int countryId, string imagePath)
        {
            CurrentMode = Mode.Update;
            ContactId = contactId;
            FirstName = firstName ?? string.Empty;
            LastName = lastName ?? string.Empty;
            Email = email ?? string.Empty;
            Phone = phone ?? string.Empty;
            Address = address ?? string.Empty;
            DateOfBirth = dateOfBirth;
            CountryId = countryId;
            ImagePath = imagePath ?? string.Empty;
        }
        #endregion

        #region CRUD Contact
        public static ContactBusinessLayer FindContact(int contactId)
        {
            string firstName = string.Empty, lastName = string.Empty, email = string.Empty,
                   phone = string.Empty, address = string.Empty, imagePath = string.Empty;
            DateTime? dateOfBirth = null;
            int countryId = -1;

            if (ContactData.GetContactById(contactId, ref firstName, ref lastName,
                                               ref email, ref phone, ref address,
                                               ref dateOfBirth, ref countryId, ref imagePath))
            {
                return new ContactBusinessLayer(contactId, firstName, lastName,
                                              email, phone, address,
                                              dateOfBirth, countryId, imagePath);
            }
            return null;
        }

        public bool Save()
        {
            switch (CurrentMode)
            {
                case Mode.AddNew:
                    ContactId = ContactData.AddContact(FirstName, LastName, Email,
                                                             Phone, Address, DateOfBirth,
                                                             CountryId, ImagePath);
                    if (ContactId != -1)
                    {
                        CurrentMode = Mode.Update;
                        return true;
                    }
                    return false;

                case Mode.Update:
                    return ContactData.UpdateContact(ContactId, FirstName, LastName,
                                                         Email, Phone, Address,
                                                         DateOfBirth, CountryId, ImagePath);
                default:
                    return false;
            }

          //  return true;
        }

        public static bool Delete(int contactId)
        {
            return ContactData.DeleteContact(contactId);
        }

        public static DataTable GetAllContacts()
        {
            return ContactData.GetAllContacts();
        }
      
        public static bool IsContactExistById(int ID)
        {
            return ContactData.IsContactExist(ID);

        }



        #endregion


    }
}
