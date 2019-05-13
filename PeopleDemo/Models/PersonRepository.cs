using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleDemo.Models
{
    public class PersonRepository
    {
        public void Add(PersonModel person)
        {
            Store.Add(person);
        }

        public static PersonRepository Current { get; } = new PersonRepository();

        public ObservableCollection<PersonModel> Store { get; set; } = new ObservableCollection<PersonModel>();
    }
}
