using Demo_NTier_DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_NTier_PresentationLayer
{
    public class PersonPresenter
    {
        static PersonBLL _personsBLL;

        public PersonPresenter(PersonBLL personBLL)
        {
            _personsBLL = personBLL;
            ManageApplicationLoop();
        }

        private void ManageApplicationLoop()
        {
            DisplayWelcomeScreen();
            DisplayMainMenu();
            DisplayClosingScreen();
        }

        /// <summary>
        /// display main menu
        /// </summary>
        private void DisplayMainMenu()
        {
            char menuChoice;
            bool runApplicationLoop = true;

            while (runApplicationLoop)
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("Main Menu");
                Console.WriteLine();

                Console.WriteLine("\t1) Retrieve Persons from Data File (Debug Only)");
                Console.WriteLine("\t2) Display Person List");
                Console.WriteLine("\t3) Display Person Detail");
                Console.WriteLine("\t4) Add Person");
                Console.WriteLine("\t5) Delete Person");
                Console.WriteLine("\t6) Update Person");
                Console.WriteLine("\tE) Exit");
                Console.WriteLine();
                Console.Write("Enter Choice:");
                menuChoice = Console.ReadKey().KeyChar;

                runApplicationLoop = ProcessMainMenuChoice(menuChoice);
            }

        }

        /// <summary>
        /// process main menu choice
        /// </summary>
        /// <param name="menuChoice">menu choice</param>
        /// <returns></returns>
        private bool ProcessMainMenuChoice(char menuChoice)
        {
            bool runApplicationLoop = true;

            switch (menuChoice)
            {
                case '1':
                    DisplayLoadPersonsFromDataFile();
                    break;

                case '2':
                    DisplayListOfPersons();
                    break;

                case '3':
                    DisplayPersonDetail();
                    break;

                case '4':
                    DisplayAddPerson();
                    break;

                case '5':
                    DisplayDeletePerson();
                    break;

                case '6':
                    DisplayUpdatePerson();
                    break;

                case 'e':
                case 'E':
                    runApplicationLoop = false;
                    break;

                default:
                    break;
            }

            return runApplicationLoop;
        }

        /// <summary>
        /// display list of character screen - ids and full name
        /// </summary>
        private void DisplayListOfPersons()
        {
            DisplayHeader("List of Persons");

            List<Person> persons = _personsBLL.GetAllPersons(out DalErrorCode statusCode, out string message) as List<Person>;

            if (statusCode == DalErrorCode.GOOD)
            {
                DisplayPersonListTable(persons);
            }
            else
            {
                Console.WriteLine(message);
            }

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display character detail screen
        /// </summary>
        private void DisplayPersonDetail()
        {
            Person person;

            DisplayHeader("Person Detail");

            List<Person> persons = _personsBLL.GetAllPersons(out DalErrorCode statusCode, out string message) as List<Person>;

            if (statusCode == DalErrorCode.GOOD)
            {
                DisplayPersonListTable(persons);
            }
            else
            {
                Console.WriteLine(message);
            }

            Console.Write("Enter Id of Person to View:");
            int.TryParse(Console.ReadLine(), out int id);

            person = _personsBLL.GetPersonById(id, out statusCode, out message);

            if (statusCode == DalErrorCode.GOOD)
            {
                DisplayHeader("Person Detail");
                DisplayPersonDetailTable(person);
            }
            else
            {
                Console.WriteLine(message);
            }

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display add character screen
        /// </summary>
        private void DisplayAddPerson()
        {
            Person person = new Person();

            // get current max id and increment for new id
            person.Id = _personsBLL.NextIdNumber();

            DisplayHeader("Add Person");

            Console.Write("Enter First Name:");
            person.FirstName = Console.ReadLine();
            Console.Write("Enter Last Name:");
            person.LastName = Console.ReadLine();
            Console.Write("Enter Address:");
            person.Address = Console.ReadLine();
            Console.Write("Enter City:");
            person.City = Console.ReadLine();
            Console.Write("Enter State:");
            person.State = Console.ReadLine();
            Console.Write("Enter Zip Code:");
            int.TryParse(Console.ReadLine(), out int zip);
            person.Zip = zip;
            Console.Write("Enter Work Phone:");
            double.TryParse(Console.ReadLine(), out double workPhone);
            person.WorkPhone = workPhone;
            Console.Write("Enter Home Phone:");
            double.TryParse(Console.ReadLine(), out double homePhone);
            person.HomePhone = homePhone;
            Console.Write("Enter Cell Phone:");
            double.TryParse(Console.ReadLine(), out double cellPhone);
            person.CellPhone = cellPhone;
            Console.Write("Enter Work Email:");
            person.WorkEmail = Console.ReadLine();
            Console.Write("Enter Home Email:");
            person.HomeEmail = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine("New Person To Add");
            Console.WriteLine($"\tId: {person.Id}");
            Console.WriteLine($"\tFirst Name: {person.FirstName}");
            Console.WriteLine($"\tLast Name: {person.LastName}");
            Console.WriteLine($"\tAddress: {person.Address}");
            Console.WriteLine($"\tCity: {person.City}");
            Console.WriteLine($"\tState: {person.State}");
            Console.WriteLine($"\tZip Code: {person.Zip}");
            Console.WriteLine($"\tWork Phone: {person.WorkPhone}");
            Console.WriteLine($"\tHome Phone: {person.HomePhone}");
            Console.WriteLine($"\tCell Phone: {person.CellPhone}");
            Console.WriteLine($"\tWork Email: {person.WorkEmail}");
            Console.WriteLine($"\tHome Email: {person.HomeEmail}");

            _personsBLL.AddPerson(person, out DalErrorCode statusCode, out string message);

            if (statusCode == DalErrorCode.GOOD)
            {
                Console.WriteLine();
                Console.WriteLine("Person Added");
            }
            else
            {
                Console.WriteLine(message);
            }

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display delete character screen
        /// </summary>
        private void DisplayDeletePerson()
        {
            DisplayHeader("Delete Person");

            List<Person> persons = _personsBLL.GetAllPersons(out DalErrorCode statusCode, out string message) as List<Person>;

            if (statusCode == DalErrorCode.GOOD)
            {
                DisplayPersonListTable(persons);
            }
            else
            {
                Console.WriteLine(message);
            }

            Console.Write("Enter Id of Person to Delete:");
            int.TryParse(Console.ReadLine(), out int id);

            _personsBLL.DeletePerson(id, out statusCode, out message);

            if (statusCode == DalErrorCode.GOOD)
            {
                Console.WriteLine("Person deleted.");
            }
            else
            {
                Console.WriteLine(message);
            }

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display update character screen
        /// </summary>
        private void DisplayUpdatePerson()
        {
            Person person;
            string userResponse;

            DisplayHeader("Update Person");

            List<Person> persons = _personsBLL.GetAllPersons(out DalErrorCode statusCode, out string message) as List<Person>;

            if (statusCode == DalErrorCode.GOOD)
            {
                DisplayPersonListTable(persons);
            }
            else
            {
                Console.WriteLine(message);
            }

            Console.Write("Enter Id of Person to Update:");
            int.TryParse(Console.ReadLine(), out int id);

            person = persons.FirstOrDefault(c => c.Id == id);

            if (person != null)
            {
                DisplayHeader("Person Detail");
                Console.WriteLine("Current Person Information");
                DisplayPersonDetailTable(person);
                Console.WriteLine();

                Console.WriteLine("Update each field or use the Enter key to keep the current information.");
                Console.WriteLine();

                Console.Write("Enter First Name:");
                userResponse = Console.ReadLine();
                if (userResponse != "")
                {
                    person.FirstName = userResponse;
                }

                Console.Write("Enter Last Name:");
                userResponse = Console.ReadLine();
                if (userResponse != "")
                {
                    person.LastName = userResponse;
                }

                Console.Write("Enter Address:");
                userResponse = Console.ReadLine();
                if (userResponse != "")
                {
                    person.Address = userResponse;
                }

                Console.Write("Enter City:");
                userResponse = Console.ReadLine();
                if (userResponse != "")
                {
                    person.City = userResponse;
                }

                Console.Write("Enter State:");
                userResponse = Console.ReadLine();
                if (userResponse != "")
                {
                    person.State = userResponse;
                }

                Console.Write("Enter Zip Code:");
                userResponse = Console.ReadLine();
                if (userResponse != "")
                {
                    int.TryParse(userResponse, out        int zip);
                    person.Zip = zip;
                }

                Console.Write("Enter Work Phone:");
                userResponse = Console.ReadLine();
                if (userResponse != "")
                {
                    double.TryParse(userResponse,         out double workPhone);
                    person.WorkPhone = workPhone;
                }

                Console.Write("Enter Home Phone:");
                userResponse = Console.ReadLine();
                if (userResponse != "")
                {
                    double.TryParse(userResponse,         out double homePhone);
                    person.HomePhone = homePhone;
                }

                Console.Write("Enter Cell Phone:");
                userResponse = Console.ReadLine();
                if (userResponse != "")
                {
                    double.TryParse(userResponse,         out double cellPhone);
                    person.CellPhone = cellPhone;
                }

                Console.Write("Enter Work Email:");
                userResponse = Console.ReadLine();
                if (userResponse != "")
                {
                    person.WorkEmail =                    userResponse;
                }

                Console.Write("Enter Home Email:");
                userResponse = Console.ReadLine();
                if (userResponse != "")
                {
                    person.HomeEmail =                    userResponse;
                }

                _personsBLL.UpdatePerson(person, out statusCode, out message);

                if (statusCode == DalErrorCode.GOOD)
                {
                    Console.WriteLine("Person updated.");
                }
                else
                {
                    Console.WriteLine(message);
                }
            }
            else
            {
                Console.WriteLine($"No person has id {id}.");
            }

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display load list of characters from data file screen (debug only)
        /// </summary>
        private void DisplayLoadPersonsFromDataFile()
        {
            DisplayHeader("Retrieve Persons from Data File");

            Console.WriteLine("Press any key to begin retrieving the data.");
            Console.ReadKey();

            List<Person> _persons = _personsBLL.GetAllPersons(out DalErrorCode statusCode, out string message) as List<Person>;

            if (statusCode == DalErrorCode.GOOD)
            {
                Console.WriteLine("Data retrieved.");
            }
            else
            {
                Console.WriteLine(message);
            }

            DisplayContinuePrompt();
        }

        #region HELPER METHODS

        /// <summary>
        /// display details of a character table
        /// </summary>
        /// <param name="character">character</param>
        private void DisplayPersonDetailTable(Person person)
        {
            Console.WriteLine($"\tId: {person.Id}");
            Console.WriteLine($"\tName: {person.FirstName} {person.LastName}");
            Console.WriteLine($"\tAddress: {person.Address}");
            Console.WriteLine($"\tCity: {person.City}");
            Console.WriteLine($"\tState: {person.State}");
            Console.WriteLine($"\tZip Code: {person.Zip}");
            Console.WriteLine($"\tWork Phone: {person.WorkPhone}");
            Console.WriteLine($"\tHome Phone: {person.HomePhone}");
            Console.WriteLine($"\tCell Phone: {person.CellPhone}");
            Console.WriteLine($"\tWork Email: {person.WorkEmail}");
            Console.WriteLine($"\tHome Email: {person.HomeEmail}");
        }

        /// <summary>
        /// display a table with id and full name columns
        /// </summary>
        /// <param name="characters">characters</param>
        private void DisplayPersonListTable(List<Person> persons)
        {
            if (persons != null)
            {
                StringBuilder columnHeader = new StringBuilder();

                columnHeader.Append("Id".PadRight(8));
                columnHeader.Append("Full Name".PadRight(25));

                Console.WriteLine(columnHeader.ToString());

                persons = persons.OrderBy(c => c.Id).ToList();

                foreach (Person person in persons)
                {
                    StringBuilder personInfo = new StringBuilder();

                    personInfo.Append(person.Id.ToString().PadRight(8));
                    personInfo.Append(person.FullName().PadRight(25));

                    Console.WriteLine(personInfo.ToString());
                }
            }
            else
            {
                Console.WriteLine("No persons exist currently.");
            }
        }

        /// <summary>
        /// display page header
        /// </summary>
        /// <param name="headerText">text for header</param>
        static void DisplayHeader(string headerText)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine($"\t\t{headerText}");
            Console.WriteLine();
        }

        /// <summary>
        /// display continue prompt
        /// </summary>
        static void DisplayContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        /// <summary>
        /// display Welcome Screen
        /// </summary>
        static void DisplayWelcomeScreen()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tWelcome to Keegan's Address Book program!");

            DisplayContinuePrompt();
        }

        /// <summary>
        /// Display Closing Screen
        /// </summary>
        static void DisplayClosingScreen()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tThank you for using my application.");

            DisplayContinuePrompt();
        }

        #endregion
    }
}
