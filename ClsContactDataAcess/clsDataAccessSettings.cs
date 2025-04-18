using System;


namespace ClsContactDataAcess
{
    internal class clsDataAccessSettings
    {

        #region variable
        /// <summary>
        /// اسم الخادم الخاص باتصال قاعدة البيانات (افتراضيًا الخادم المحلي).
        /// </summary>
        static string severName = ".";
        /// <summary>
        /// اسم قاعدة البيانات التي سيتم الاتصال بها.
        /// </summary>
        static string DataBase = "ContactsDB";
        /// <summary>
        /// سلسلة الاتصال المستخدمة للاتصال بقاعدة بيانات جهات الاتصال.
        /// تستخدم الأمان المتكامل مع التشفير والثقة في شهادة الخادم.
        /// </summary>
        /// <remarks>
        /// ملاحظة: يُرجى تعديل سلسلة الاتصال وفقًا لإعدادات الخادم الخاص بك.
        /// مثال بديل: "Server=.;Database=ContactsDB;User Id=sa;Password=123456;"
        /// </remarks>
      // public static string ConnectionString = $"Server={severName};Database={DataBase};Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";
        public static string ConnectionString = $"Server={severName};Database={DataBase};Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";
     

        #endregion

    }
}
