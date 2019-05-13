using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleDemo.Models
{
    public class PersonModel
    {
        public PersonModel(string name, string surname, DateTime dateOfBirth)
        {
            Name = name;
            Surname = surname;
            DateOfBirth = dateOfBirth;
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
