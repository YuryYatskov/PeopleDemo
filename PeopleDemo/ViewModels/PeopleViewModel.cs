using PeopleDemo.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PeopleDemo.ViewModels
{
    public class PeopleViewModel : ViewModelBase
    {
        private readonly ICollectionView _peopleView;
        private string _searchType;
        private static int _numerator; 

        public PeopleViewModel()
        {
            People = new ObservableCollection<PersonViewModel>();
            _peopleView = CollectionViewSource.GetDefaultView(People);
            _peopleView.Filter = o => IsVisible((PersonViewModel)o);

            // fill collection 
            foreach (var personModel in PersonRepository.Current.Store)
            {
                People.Add(ToViewModel(personModel));
            }

            // subscribe for change
            PersonRepository.Current.Store.CollectionChanged += OnPeopleCollectionChanged;
        }

        public override string Name { get; } = "People " + ++_numerator;

        public string SearchText
        {
            get => _searchType;
            set
            {
                if (value == _searchType) return;
                _searchType = value;
                NotifyOfPropertyChange(nameof(SearchText));
                _peopleView.Refresh();
            }
        }

        public ObservableCollection<PersonViewModel> People { get; }

        private static PersonViewModel ToViewModel(PersonModel personModel)
        {
            return new PersonViewModel(personModel);
        }

        private void OnPeopleCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (PersonModel ni in e.NewItems)
                {
                    People.Add(ToViewModel(ni));
                }
            }
        }

        private bool IsVisible(PersonViewModel personViewModel)
        {
            return string.IsNullOrEmpty(SearchText) ||
                   CultureInfo.CurrentCulture.CompareInfo.IndexOf(personViewModel.DisplayName, SearchText, CompareOptions.IgnoreCase) >= 0;
        }
    }
}
