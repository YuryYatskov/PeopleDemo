using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleDemo.ViewModels
{
    public abstract class ViewModelBase : PropertyChangedBase
    {
        public abstract string Name { get; }
    }
}
