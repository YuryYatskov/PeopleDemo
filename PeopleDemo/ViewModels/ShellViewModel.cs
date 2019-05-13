using Caliburn.Micro;
using PeopleDemo.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleDemo.ViewModels
{
    public class ShellViewModel : PropertyChangedBase
    {
        public ShellViewModel()
        {
            PersonRepository.Current.Add(new PersonModel("Angelina", "Jolie", new DateTime(1975, 6, 4)));
            PersonRepository.Current.Add(new PersonModel("Julia", "Roberts", new DateTime(1967, 10, 28)));
            PersonRepository.Current.Add(new PersonModel("George", "Michael", new DateTime(1963, 6, 25)));
            PersonRepository.Current.Add(new PersonModel("Leonardo", "DiCaprio", new DateTime(1974, 11, 11)));
            PersonRepository.Current.Add(new PersonModel("Brad", "Pitt", new DateTime(1963, 12, 18)));
            PersonRepository.Current.Add(new PersonModel("George", "Clooney", new DateTime(1961, 5, 6)));
        }

        public ObservableCollection<ViewModelBase> Pages { get; set; } = new ObservableCollection<ViewModelBase>();

        public void AddPage()
        {
            Pages.Add(new PeopleViewModel());
        }
    }
}
