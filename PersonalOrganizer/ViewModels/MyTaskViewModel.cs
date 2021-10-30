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
    class MyTaskViewModel : INotifyPropertyChanged
    {
        private MyTask _myTask;
        public MyTaskViewModel(MyTask myTask)
        {
            this._myTask = myTask;
        }
        public int Id
        {
            get { return _myTask.Id; }
            set { _myTask.Id = value; OnProperyChanged("Id"); }
        }
        public string Title
        {
            get { return _myTask.Title; }
            set { _myTask.Title = value; OnProperyChanged("Title"); }
        }
        public string About
        {
            get { return _myTask.About; }
            set { _myTask.About = value; OnProperyChanged("About"); }
        }
        public int CategoryId
        {
            get { return _myTask.CategoryId; }
            set { _myTask.CategoryId = value; OnProperyChanged("CategoryId"); }
        }
        public int PriorityId
        {
            get { return _myTask.PriorityId; }
            set { _myTask.PriorityId = value; OnProperyChanged("PriorityId"); }
        }
        public int StatusId
        {
            get { return _myTask.StatusId; }
            set { _myTask.StatusId = value; OnProperyChanged("StatusId"); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnProperyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
