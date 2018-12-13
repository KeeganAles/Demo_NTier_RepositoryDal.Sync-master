using Demo_NTier_DataAccessLayer;
using Demo_NTier_DomainLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_NTier_PresentationLayer
{
    public class PersonBLL
    {
        IPersonRepository _personRepository;

        public PersonBLL(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        /// <summary>
        /// get IEnumberable of all characters sorted by Id
        /// </summary>
        /// <param name="dalErrorCode">DAL error code</param>
        /// <param name="message">error message</param>
        /// <returns></returns>
        public IEnumerable<Person> GetAllPersons(out DalErrorCode dalErrorCode, out string message)
        {
            List<Person> persons = null;
            message = "";

            using (_personRepository)
            {
                persons = _personRepository.GetAll(out dalErrorCode) as List<Person>;

                if (dalErrorCode == DalErrorCode.GOOD)
                {
                    if (persons != null)
                    {
                        persons.OrderBy(c => c.Id);
                    }
                }
                else
                {
                    message = "An error occurred connecting to the database.";
                }
            }

            return persons;
        }

        /// <summary>
        /// get character by id
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="dalErrorCode">DAL error code</param>
        /// <param name="message">message</param>
        /// <returns></returns>
        public Person GetPersonById(int id, out DalErrorCode dalErrorCode, out string message)
        {
            message = "";
            Person person;

            using (_personRepository)
            {
                person = _personRepository.GetById(id, out dalErrorCode);

                if (dalErrorCode == DalErrorCode.GOOD)
                {
                    if (person == null)
                    {
                        message = $"No person has id {id}.";
                        dalErrorCode = DalErrorCode.ERROR;
                    }
                }
                else
                {
                    message = "An error occurred connecting to the database.";
                }
            }

            return person;
        }

        /// <summary>
        /// add a character to the data file
        /// </summary>
        /// <param name="character">character</param>
        /// <param name="dalErrorCode">DAL error code</param>
        /// <param name="message">message</param>
        public void AddPerson(Person person, out DalErrorCode dalErrorCode, out string message)
        {
            message = "";

            _personRepository.Insert(person, out dalErrorCode);

            if (dalErrorCode == DalErrorCode.ERROR)
            {
                message = "There was an error connecting to the data file.";
            }
        }

        /// <summary>
        /// delete a character from the data file
        /// </summary>
        /// <param name="character">character</param>
        /// <param name="dalErrorCode">status code</param>
        /// <param name="message">message</param>
        public void DeletePerson(int id, out DalErrorCode dalErrorCode, out string message)
        {
            message = "";

            using (_personRepository)
            {
                if (PersonDocumentExists(id, out dalErrorCode))
                {
                    _personRepository.Delete(id, out dalErrorCode);

                    if (dalErrorCode == DalErrorCode.ERROR)
                    {
                        message = "There was an error connecting to the data file.";
                    }
                }
                else
                {
                    message = $"Person with id {id} does not exist.";
                    dalErrorCode = DalErrorCode.ERROR;
                }
            }
        }

        private bool PersonDocumentExists(int id, out DalErrorCode dalErrorCode)
        {
            using (_personRepository)
            {
                if (_personRepository.GetById(id, out dalErrorCode) != null)
                {
                    return true;
                }
                else
                {
                    dalErrorCode = DalErrorCode.ERROR;
                    return false;
                }
            }
        }

        /// <summary>
        /// update a character in the data file
        /// </summary>
        /// <param name="character">character</param>
        /// <param name="dalErrorCode">status code</param>
        /// <param name="message">message</param>
        public void UpdatePerson(Person person, out DalErrorCode dalErrorCode, out string message)
        {
            message = "";

            using (_personRepository)
            {
                if (PersonDocumentExists(person.Id, out dalErrorCode))
                {
                    _personRepository.Update(person, out dalErrorCode);

                    if (dalErrorCode == DalErrorCode.ERROR)
                    {
                        message = "There was an error connecting to the data file.";
                    }
                }
                else
                {
                    message = $"Person with id {person.Id} does not exist.";
                    dalErrorCode = DalErrorCode.ERROR;
                }
            }
        }

        /// <summary>
        /// generate the next id increment
        /// </summary>
        /// <returns>id value</returns>
        public int NextIdNumber()
        {
            int nextIdNumber = 0;

            List<Person> person = _personRepository.GetAll(out DalErrorCode statusCode) as List<Person>;

            if (statusCode == DalErrorCode.GOOD)
            {
                nextIdNumber = person.Max(c => c.Id) + 1;
            }

            return nextIdNumber;
        }
    }
}
