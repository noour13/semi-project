using System;

namespace clTextFormat
{
    /// <summary>
    /// Provides text formatting and user input handling utilities for console applications
    /// </summary>
    public class clsTextFormat
    {
        #region variables

        /// <summary>
        /// Array of menu items for contacts and countries management
        /// </summary>
        public static string[] menuItems = {
            "1-FindContact Contact",
            "2-Add New Contact",
            "3-Update Contact",
            "4-Delete Contact",
            "5-List Contact",
            "***************",
            "6-FindContact Country",
            "7-Add New Country",
            "8-Update Country",
            "9-Delete Country",
            "10-List Country",
            "11-Exit"
        };

        /// <summary>
        /// Enumeration of available menu options
        /// </summary>
        public enum MenuOptions
        {
            /// <summary>FindContact contact option</summary>
            FindContactContact = 1,
            /// <summary>Add new contact option</summary>
            AddNewContact,
            /// <summary>Update contact option</summary>
            UpdateContact,
            /// <summary>Delete contact option</summary>
            DeleteContact,
            /// <summary>List contacts option</summary>
            ListContacts,
            /// <summary>FindContact country option</summary>
            FindContactCountry,
            /// <summary>Add new country option</summary>
            AddNewCountry,
            /// <summary>Update country option</summary>
            UpdateCountry,
            /// <summary>Delete country option</summary>
            DeleteCountry,
            /// <summary>List countries option</summary>
            ListCountry,
            /// <summary>Exit application option</summary>
            Exit
        }

        /// <summary>
        /// Title separator for menu display
        /// </summary>
        public static string[] title = { "====================================" };

        #endregion

        #region functions

        #region formatScreen

        /// <summary>
        /// Prints a menu centered on the console screen with a title
        /// </summary>
        /// <param name="title">The title to display at the top of the menu</param>
        /// <param name="items">Array of menu items to display</param>
        /// <remarks>
        /// This method calculates the maximum length of all items to ensure proper centering.
        /// Each item is centered individually based on its own length.
        /// </remarks>
        public static void PrintCenteredMenu(string title, string[] items)
        {
            // حساب أطول سطر في القائمة
            int maxLength = title.Length;
            foreach (var item in items)
            {
                if (item.Length > maxLength)
                    maxLength = item.Length;
            }

            // توسيط العنوان
            Console.SetCursorPosition((Console.WindowWidth - title.Length) / 2, Console.CursorTop);
            Console.WriteLine(title);

            // توسيط كل عنصر في القائمة
            foreach (var item in items)
            {
                Console.SetCursorPosition((Console.WindowWidth - items[0].Length) / 2, Console.CursorTop);
                Console.WriteLine(item);
            }
        }

        #endregion

        #region process inputs

        /// <summary>
        /// Gets and validates user's menu choice
        /// </summary>
        /// <param name="msgChoice">Prompt message to display</param>
        /// <param name="msgError">Error message to display for invalid input</param>
        /// <returns>Selected MenuOptions value</returns>
        /// <remarks>
        /// Continues to prompt until a valid numeric choice within MenuOptions range is entered.
        /// Displays error message in red color for invalid input.
        /// </remarks>
        public static MenuOptions GetUserChoice(string msgChoice, string msgError)
        {
            while (true)
            {
                Console.Write(msgChoice);
                if (int.TryParse(Console.ReadLine(), out int choice) &&
                    Enum.IsDefined(typeof(MenuOptions), choice))
                {
                    return (MenuOptions)choice;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(msgError);
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Gets and validates a numeric input from user
        /// </summary>
        /// <param name="msgNumber">Prompt message to display</param>
        /// <param name="msgError">Error message to display for invalid input</param>
        /// <returns>Valid integer entered by user</returns>
        /// <remarks>
        /// Continues to prompt until a valid integer is entered.
        /// Displays error message in red color for invalid input.
        /// </remarks>
        public static int GetUserNumber(string msgNumber, string msgError)
        {
            while (true)
            {
                Console.Write(msgNumber);
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    return choice;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(msgError);
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Gets and validates a text input from user
        /// </summary>
        /// <param name="msgNumber">Prompt message to display</param>
        /// <param name="msgError">Error message to display for invalid input</param>
        /// <returns>Valid text string entered by user</returns>
        /// <remarks>
        /// Validates that input is not empty and not numeric.
        /// Displays error message in red color for invalid input.
        /// Uses recursion to re-prompt on invalid input.
        /// </remarks>
        public static string GetUsertext(string msgNumber, string msgError)
        {
            while (true)
            {
                Console.WriteLine(msgNumber);
                string input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input) && !int.TryParse(input, out _))
                {
                    return input;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(msgError);
                    Console.ResetColor();
                    return GetUsertext(msgNumber, msgError); // استدعاء ذاتي لإعادة المحاولة
                }
            }
        }

        #endregion

        #endregion
    }
}