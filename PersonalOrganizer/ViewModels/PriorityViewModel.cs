using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PersonalOrganizer.Models;
namespace PersonalOrganizer.ViewModels
{
    class PriorityViewModel :INotifyPropertyChanged
    {
        private Priority _priority;
        public PriorityViewModel(Priority priority)
        {
            this._priority = priority;
        }
        public int Id
        {
            get { return _priority.Id; }
            set { _priority.Id = value; OnProperyChanged("Id"); }
        }
        public string Name
        {
            get { return _priority.Name; }
            set { _priority.Name = value; OnProperyChanged("Name"); }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnProperyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
