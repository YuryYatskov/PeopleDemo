using PeopleDemo.Models;
using System;

namespace PeopleDemo.ViewModels
{
    public class PersonViewModel
    {
        private readonly PersonModel _model;

        public PersonViewModel(PersonModel model)
        {
            _model = model;
        }

        public DateTime DateOfBirth => _model.DateOfBirth;
        public int Age => (int)((DateTime.Now - DateOfBirth).Days / 365);
        public string DisplayName => $"{_model.Name} {_model.Surname}";
    }
}
