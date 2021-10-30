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
    class CategoryViewModel :INotifyPropertyChanged
    {
        private Category _category;
        public CategoryViewModel(Category category)
        {
            this._category = category;
        }
        public int Id
        {
            get{return _category.Id;}
            set { _category.Id = value; OnProperyChanged("Id"); }
        }
        public string Name
        {
            get { return _category.Name; }
            set { _category.Name = value; OnProperyChanged("Name"); }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnProperyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
