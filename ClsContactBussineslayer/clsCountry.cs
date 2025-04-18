using System;
using System.Data;
using ClsContactDataAcess;

namespace ClsContactBussineslayer
{
    /// <summary>
    /// يمثل الصنف الرئيسي لإدارة عمليات الدول في طبقة الأعمال
    /// </summary>
    public class clsCountry
    {
        #region CRUD

        /// <summary>
        /// تعداد يحدد وضع العملية (إضافة جديد أو تحديث)
        /// </summary>
        public enum Mode { AddNew = 0, Update = 1 };

        /// <summary>
        /// يحتفظ بالوضع الحالي للكائن (إضافة أو تحديث)
        /// </summary>
        public Mode mode = Mode.AddNew;

        /// <summary>
        /// يحصل أو يحدد معرف الدولة
        /// </summary>
        public int ID { set; get; }

        /// <summary>
        /// يحصل أو يحدد اسم الدولة
        /// </summary>
        public string CountryName { set; get; }

        /// <summary>
        /// يحصل أو يحدد كود الدولة
        /// </summary>
        public string Code { set; get; }

        /// <summary>
        /// يحصل أو يحدد كود الهاتف للدولة
        /// </summary>
        public string PhoneCode { set; get; }

        /// <summary>
        /// المُنشئ الافتراضي للصنف - يستخدم عند إنشاء دولة جديدة
        /// </summary>
        public clsCountry()
        {
            this.ID = -1;
            this.CountryName = "";
            mode = Mode.AddNew;
        }

        /// <summary>
        /// مُنشئ خاص يستخدم عند تحميل بيانات دولة موجودة
        /// </summary>
        /// <param name="ID">معرف الدولة</param>
        /// <param name="CountryName">اسم الدولة</param>
        /// <param name="Code">كود الدولة</param>
        /// <param name="PhoneCode">كود الهاتف</param>
        private clsCountry(int ID, string CountryName, string Code, string PhoneCode)
        {
            this.ID = ID;
            this.CountryName = CountryName;
            this.Code = Code;
            this.PhoneCode = PhoneCode;
            mode = Mode.Update;
        }

        /// <summary>
        /// يُضيف دولة جديدة إلى قاعدة البيانات
        /// </summary>
        /// <returns>قيمة منطقية تشير إلى نجاح العملية</returns>
        private bool _AddNewCountry()
        {
            this.ID = ClsContactDataAcess.clsCountryData.AddNewCountry(this.CountryName, this.Code, this.PhoneCode);
            return (this.ID != -1);
        }

        /// <summary>
        /// يقوم بتحديث بيانات دولة موجودة
        /// </summary>
        /// <returns>قيمة منطقية تشير إلى نجاح العملية</returns>
        private bool _UpdateContact()
        {
            return clsCountryData.UpdateCountry(this.ID, this.CountryName, this.Code, this.PhoneCode);
        }

        /// <summary>
        /// يبحث عن دولة بواسطة المعرف
        /// </summary>
        /// <param name="ID">معرف الدولة المراد البحث عنها</param>
        /// <returns>كائن من نوع clsCountry إذا تم العثور عليه، أو null إذا لم يتم العثور</returns>
        public static clsCountry Find(int ID)
        {
            string CountryName = "";
            string Code = "";
            string PhoneCode = "";

            if (clsCountryData.GetCountryInfoByID(ID, ref CountryName, ref Code, ref PhoneCode))
                return new clsCountry(ID, CountryName, Code, PhoneCode);
            else
                return null;
        }

        /// <summary>
        /// يبحث عن دولة بواسطة الاسم
        /// </summary>
        /// <param name="CountryName">اسم الدولة المراد البحث عنها</param>
        /// <returns>كائن من نوع clsCountry إذا تم العثور عليه، أو null إذا لم يتم العثور</returns>
        public static clsCountry Find(string CountryName)
        {
            int ID = -1;
            string Code = "";
            string PhoneCode = "";

            if (clsCountryData.GetCountryInfoByName(CountryName, ref ID, ref Code, ref PhoneCode))
                return new clsCountry(ID, CountryName, Code, PhoneCode);
            else
                return null;
        }

        /// <summary>
        /// يحفظ بيانات الدولة (إضافة جديدة أو تحديث) حسب الوضع الحالي
        /// </summary>
        /// <returns>قيمة منطقية تشير إلى نجاح العملية</returns>
        public bool Save()
        {
            switch (mode)
            {
                case Mode.AddNew:
                    if (_AddNewCountry())
                    {
                        mode = Mode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case Mode.Update:
                    return _UpdateContact();
            }
            return false;
        }

        /// <summary>
        /// يسترجع جميع الدول من قاعدة البيانات
        /// </summary>
        /// <returns>جدول بيانات يحتوي على جميع الدول</returns>
        public static DataTable GetAllCountries()
        {
            return clsCountryData.GetAllCountries();
        }

        /// <summary>
        /// يحذف دولة من قاعدة البيانات
        /// </summary>
        /// <param name="ID">معرف الدولة المراد حذفها</param>
        /// <returns>قيمة منطقية تشير إلى نجاح العملية</returns>
        public static bool DeleteCountry(int ID)
        {
            return clsCountryData.DeleteCountry(ID);
        }

        /// <summary>
        /// يتحقق من وجود دولة بواسطة المعرف
        /// </summary>
        /// <param name="ID">معرف الدولة المراد التحقق منها</param>
        /// <returns>قيمة منطقية تشير إلى وجود الدولة</returns>
        public static bool isCountryExist(int ID)
        {
            return clsCountryData.IsCountryExist(ID);
        }

        /// <summary>
        /// يتحقق من وجود دولة بواسطة الاسم
        /// </summary>
        /// <param name="CountryName">اسم الدولة المراد التحقق منها</param>
        /// <returns>قيمة منطقية تشير إلى وجود الدولة</returns>
        public static bool isCountryExist(string CountryName)
        {
            return clsCountryData.IsCountryExist(CountryName);
        }

        #endregion
    }
}