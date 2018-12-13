using Demo_NTier_DataAccessLayer;
using Demo_NTier_DomainLayer;
using Demo_NTier_PresentationLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_NTier_Startup
{
    class Program
    {
        static void Main(string[] args)
        {
            IPersonRepository personRepository = new MongoDbPersonRepository();
            IDataService dataService = new MongoDBSimpleDataService();

            //
            // use seed data to test the MongoDB data service
            //  
            dataService.WriteAll(GenerateListOfPersons(), out DalErrorCode statusCode);
            if (statusCode == DalErrorCode.GOOD)
            {
                PersonBLL personBLL = new PersonBLL(personRepository);
                PersonPresenter presenter = new PersonPresenter(personBLL);
            }
            else
            {
                Console.WriteLine("There was an error connecting to data file.");


                //
                // application startup
                //
                PersonBLL personBLL = new PersonBLL(personRepository);
                PersonPresenter presenter = new PersonPresenter(personBLL);
            }
        }
        private static List<Character> GenerateListOfCharacters()
        {
            List<Character> characters = new List<Character>()
            {
                new Character()
                {
                    Id = 1,
                    LastName = "Flintstone",
                    FirstName = "Fred",
                    Age = 28,
                    Gender = Character.GenderType.MALE
                },
                new Character()
                {
                    Id = 2,
                    LastName = "Rubble",
                    FirstName = "Barney",
                    Age = 28,
                    Gender = Character.GenderType.FEMALE
                },
                new Character()
                {
                    Id = 3,
                    LastName = "Flintstone",
                    FirstName = "Wilma",
                    Age = 27,
                    Gender = Character.GenderType.FEMALE
                },
                new Character()
                {
                    Id = 4,
                    LastName = "Flintstone",
                    FirstName = "Pebbles",
                    Age = 1,
                    Gender = Character.GenderType.FEMALE
                },
                new Character()
                {
                    Id = 5,
                    LastName = "Rubble",
                    FirstName = "Betty",
                    Age = 26,
                    Gender = Character.GenderType.FEMALE
                },
                new Character()
                {
                    Id = 6,
                    LastName = "Rubble",
                    FirstName = "Bamm-Bamm",
                    Age = 2,
                    Gender = Character.GenderType.MALE
                },
                new Character()
                {
                    Id = 7,
                    LastName = "",
                    FirstName = "Dino",
                    Age = 7,
                    Gender = Character.GenderType.FEMALE
                }
            };

            return characters;
        }


        private static List<Person> GenerateListOfPersons()
        {
            List<Person> persons = new List<Person>()
            {
                new Person()
                {
                    Id = 1,
                    LastName = "Flintstone",
                    FirstName = "Fred",
                    Address = "1234 Flintstone Road",
                    City = "Traverse City",
                    State = "Michigan",
                    Zip = 49686,
                    WorkPhone = 12319953600,
                    HomePhone = 12316209871,
                    CellPhone = 12318555566,
                    WorkEmail = "Fredflintstone@website.com",
                    HomeEmail = "Fredbug@website.com"
                },
                new Person()
                {
                    Id = 2,
                    LastName = "Rubble",
                    FirstName = "Barney",
                    Address = "1234 Fransisco Road",
                    City = "Linden",
                    State = "Michigan",
                    Zip = 48451,
                    WorkPhone = 9357183365,
                    HomePhone = 4654216584,
                    CellPhone = 8954444686,
                    WorkEmail = "brubble@website.com",
                    HomeEmail = "brubblebath@website.com"
                },
                new Person()
                {
                    Id = 3,
                    LastName = "Flintstone",
                    FirstName = "Wilma",
                    Address = "1234 Cave Woman Road",
                    City = "Flint",
                    State = "Michigan",
                    Zip = 49854,
                    WorkPhone = 4862135658,
                    HomePhone = 4684912925,
                    CellPhone = 3917489595,
                    WorkEmail = "wilmaF@website.com",
                    HomeEmail = "Filmaselfupwithfood@website.com"
                },
                new Person()
                {
                    Id = 4,
                    LastName = "Flintstone",
                    FirstName = "Pebbles",
                    Address = "1234 Pebble Lane",
                    City = "Traverse City",
                    State = "Michigan",
                    Zip = 49684,
                    WorkPhone = 1234567890,
                    HomePhone = 2345678901,
                    CellPhone = 3456789012,
                    WorkEmail = "FPebbles@website.com",
                    HomeEmail = "fruitypebbles@website.com"
                }   
            };

            return persons;
        }
    }
}
