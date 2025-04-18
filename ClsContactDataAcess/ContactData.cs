using System;
using System.Data;
using System.Data.SqlClient;
using ClsContactDataAcess;


/// <summary>
/// يوفر طرق الوصول إلى البيانات لإدارة جهات الاتصال في قاعدة البيانات
/// </summary>
namespace ContactDataAccess
{


    /// <summary>
    ///  على جهات الاتصال  CRUD يحتوي على التوابع تقوم بإجراء العمليات  
    /// </summary>
    public class ContactData 
    {
        public static bool GetContactById(int contactId, ref string firstName, ref string lastName, ref string email, ref string phone, ref string address, ref DateTime? dateOfBirth, ref int countryId, ref string imagePath)
        {
            SqlConnection connection = new SqlConnection(ClsContactDataAcess.clsDataAccessSettings.ConnectionString);
            const string query = @"
        SELECT 
            FirstName,
            LastName,
            Email,
            Phone,
            Address,
            DateOfBirth,
            CountryID,
            ImagePath
        FROM Contacts
        WHERE ContactID = @contactId ";
            bool Isfound = false;
            try
            {

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@ContactID", contactId);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // قراءة البيانات مع التعامل مع القيم NULL
                    firstName = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                    lastName = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                    email = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                    phone = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                    address = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                    dateOfBirth = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5);
                    countryId = reader.IsDBNull(6) ? -1 : reader.GetInt32(6);
                    imagePath = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);

                    Isfound = true;
                }
                else
                    Isfound = false;
                connection.Close();
            }

            catch (Exception ex)
            {
                Isfound = false;
                Console.WriteLine($"Unexpected error in GetContactById for ID {contactId}", ex);

            }
            finally { connection.Close(); }
            return Isfound;
        }
        public static int AddContact(string firstName, string lastName, string email,
                           string phone, string address, DateTime? dateOfBirth,
                           int countryId, string imagePath)
        {
            const string query = @"
        INSERT INTO Contacts (
            FirstName,
            LastName,
            Email,
            Phone,
            Address,
            DateOfBirth,
            CountryID,
            imagePath
          
        )
        VALUES (
            @firstName,
            @lastName,
            @email,
            @phone,
            @address,
            @dateOfBirth,
            @countryId,
            @imagePath
            
        );
        SELECT SCOPE_IDENTITY();";

            try
            {
                SqlConnection connection = new SqlConnection(ClsContactDataAcess.clsDataAccessSettings.ConnectionString);
                SqlCommand command = new SqlCommand(query, connection);

                // إضافة المعلمات مع تحديد نوع البيانات بدقة
                command.Parameters.AddWithValue("@firstName", firstName);
                command.Parameters.AddWithValue("@lastName", lastName);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@phone", phone);
                command.Parameters.AddWithValue("@address", address);


                command.Parameters.AddWithValue("@dateOfBirth", dateOfBirth);



                command.Parameters.AddWithValue("@countryId", SqlDbType.Int).Value = countryId;
                if (imagePath != "")
                    command.Parameters.AddWithValue("@ImagePath", imagePath);
                else
                    command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);
                connection.Open();

                // تنفيذ الأمر وإرجاع المعرف الجديد
                object result = command.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    return Convert.ToInt32(result);
                }
                return -1; // فشل الإدراج
            }



            catch (SqlException ex) when (ex.Number == 2601 || ex.Number == 2627)
            {
                Console.WriteLine($"Duplicate entry violation in AddContact", ex);
            }

            catch (SqlException ex)
            {
                Console.WriteLine($"Database error in AddContact", ex);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error in AddContact", ex);

            }


            return -1;
        }
        public static bool UpdateContact(int contactId, string firstName, string lastName, string email, string phone, string address, DateTime? dateOfBirth, int countryId, string imagePath)
        {
            const string query =

         @" UPDATE Contacts 
          SET 
              FirstName = @FirstName,
              LastName = @LastName,
              Email = @Email,
              Phone = @Phone,
              Address = @Address,
              DateOfBirth = @DateOfBirth,
              CountryId = @CountryId,
              ImagePath = @ImagePath
          WHERE 
              ContactId = @contactId";


            try
            {
                SqlConnection connection = new SqlConnection(ClsContactDataAcess.clsDataAccessSettings.ConnectionString);
                SqlCommand command = new SqlCommand(query, connection);
                {
                    // إضافة المعلمات الأساسية
                    command.Parameters.Add("@ContactId", SqlDbType.Int).Value = contactId;
                    command.Parameters.AddWithValue("@FirstName", firstName);
                    command.Parameters.Add("@LastName", SqlDbType.NVarChar, 100).Value = lastName ?? (object)DBNull.Value;
                    command.Parameters.Add("@Email", SqlDbType.NVarChar, 255).Value = email ?? (object)DBNull.Value;
                    command.Parameters.Add("@Phone", SqlDbType.NVarChar, 50).Value = phone ?? (object)DBNull.Value;
                    command.Parameters.Add("@Address", SqlDbType.NVarChar, 500).Value = address ?? (object)DBNull.Value;

                    // معالجة خاصة للتاريخ القابل للإلغاء
                    if (dateOfBirth.HasValue)
                        command.Parameters.Add("@DateOfBirth", SqlDbType.DateTime2).Value = dateOfBirth.Value;
                    else
                        command.Parameters.Add("@DateOfBirth", SqlDbType.DateTime2).Value = DBNull.Value;

                    command.Parameters.Add("@CountryId", SqlDbType.Int).Value = countryId;
                    command.Parameters.Add("@ImagePath", SqlDbType.NVarChar, 500).Value = imagePath ?? (object)DBNull.Value;

                    connection.Open();

                    int rowsAffected = command.ExecuteNonQuery();

                    // إذا تم تحديث سجل واحد فقط
                    return rowsAffected == 1;
                }
            }
            catch (SqlException ex) when (ex.Number == 2601 || ex.Number == 2627)
            {
                Console.WriteLine($"Duplicate entry violation in UpdateContact for ID {contactId}", ex);

            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error in UpdateContact for ID {contactId}", ex);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error in UpdateContact for ID {contactId}", ex);

            }
            return false;
        }
        public static bool DeleteContact(int contactId)
        {





            string query =
                @"DELETE FROM Contacts WHERE ContactId = @contactId";





            try
            {
                SqlConnection connection = new SqlConnection(ClsContactDataAcess.clsDataAccessSettings.ConnectionString);
                SqlCommand command = new SqlCommand(query, connection);
                {
                    command.Parameters.AddWithValue("@ContactId", contactId);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    // التأكد من حذف/تحديث سجل واحد فقط
                    return rowsAffected == 1;
                }
            }
            catch (SqlException ex) when (ex.Number == 547) // Foreign Key constraint violation
            {
                Console.WriteLine($"Delete constraint violation for Contact ID {contactId}", ex);

            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error in DeleteContact for ID {contactId}", ex);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error in DeleteContact for ID {contactId}", ex);

            }
            return false;
        }
        public static DataTable GetAllContacts()
        {
            DataTable dataTable = new DataTable("Contacts");
            SqlConnection connection = new SqlConnection(ClsContactDataAcess.clsDataAccessSettings.ConnectionString);

            const string query = @"
        SELECT 
           *
        FROM Contacts";

            try
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // إنشاء أعمدة DataTable بناءً على بنية النتيجة
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            dataTable.Columns.Add(reader.GetName(i), reader.GetFieldType(i));
                        }

                        // ملء DataTable بالبيانات
                        while (reader.Read())
                        {
                            DataRow row = dataTable.NewRow();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                row[i] = reader.IsDBNull(i) ? DBNull.Value : reader.GetValue(i);
                            }
                            dataTable.Rows.Add(row);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // يمكنك تسجيل الخطأ هنا إذا لزم الأمر
                Console.WriteLine($"Erro: {ex.Message}");
                return null; // أو يمكنك إرجاع DataTable فارغ
            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                    connection.Close();
            }

            return dataTable;
        }

        public static bool IsContactExist(int ID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM Contacts WHERE ContactID = @ContactID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ContactID", ID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
            {
              Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

    }
}

