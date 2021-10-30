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
    class StatusViewModel : INotifyPropertyChanged
    {
        private Status _status;
        public StatusViewModel(Status status)
        {
            this._status = status;
        }
        public int Id
        {
            get { return _status.Id; }
            set { _status.Id = value; OnProperyChanged("Id"); }
        }
        public string Name
        {
            get { return _status.Name; }
            set { _status.Name = value; OnProperyChanged("Name"); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnProperyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
