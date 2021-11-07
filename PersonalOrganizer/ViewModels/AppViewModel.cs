using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PersonalOrganizer.Models;
using System.Collections.ObjectModel;
using PersonalOrganizer.Data;
using PersonalOrganizer.Commands;

namespace PersonalOrganizer.ViewModels
{
    class AppViewModel : INotifyPropertyChanged
    {
        private DataManager _dm;
        public ObservableCollection<CategoryViewModel> Categories_VM { get; set; }
        public ObservableCollection<PriorityViewModel> Priority_VM { get; set; }
        public ObservableCollection<StatusViewModel> Status_VM { get; set; }
        public ObservableCollection<MyTaskViewModel> MyTasks_VM { get; set; }
        public AppViewModel()
        {
            _dm = new DataManager();
            Categories_VM = new ObservableCollection<CategoryViewModel>();
            Priority_VM = new ObservableCollection<PriorityViewModel>();
            Status_VM = new ObservableCollection<StatusViewModel>();
            MyTasks_VM = new ObservableCollection<MyTaskViewModel>();
            LoadCategories();
            LoadStatuses();
            LoadMyTasks();
            LoadPriorities();
        }


        //Loaders =======================
        private void LoadCategories()
        {
            Categories_VM.Clear();
            var _categories = _dm.Categories.ToList();
            foreach (Category item in _categories)
                Categories_VM.Add(new CategoryViewModel(item));
        }
        private void LoadStatuses()
        {
            Status_VM.Clear();
            var _statuses = _dm.Statuses.ToList();
            if (_statuses.Count == 0)
            {
                _dm.Statuses.Add(new Status() {Name="To do"});
                _dm.Statuses.Add(new Status() {Name="In progress"});
                _dm.Statuses.Add(new Status() {Name="Done"});
                _dm.SaveChanges();
                _statuses = _dm.Statuses.ToList();
            }
            foreach (Status status in _statuses)
                Status_VM.Add(new StatusViewModel(status));
        }
        private void LoadMyTasks()
        {
            MyTasks_VM.Clear();
            var _myTasks = _dm.MyTasks.ToList();
            foreach (MyTask task in _myTasks)
                MyTasks_VM.Add(new MyTaskViewModel(task));
        }
        private void LoadPriorities()
        {
            Priority_VM.Clear();
            var _priorities = _dm.Priorities.ToList();
            //foreach(var item in _dm.Priorities)
            //{
            //    _dm.Priorities.Remove(item);
            //}
            //_dm.SaveChanges();
            if (_priorities.Count == 0)
            {
                _dm.Priorities.Add(new Priority() {Name="Low"});
                _dm.Priorities.Add(new Priority() {Name= "Normal" });
                _dm.Priorities.Add(new Priority() {Name="High"});
                _dm.SaveChanges();
                _priorities = _dm.Priorities.ToList();
            }
            foreach (Priority priority in _priorities)
                Priority_VM.Add(new PriorityViewModel(priority));
        }
        //Selected Properties ===========
        private CategoryViewModel selectedCategory;
        public CategoryViewModel SelectedCategory
        {
            get { return selectedCategory; }
            set { selectedCategory = value; OnProperyChanged("SelectedCategory"); }
        }
        private PriorityViewModel selectedPriority;
        public PriorityViewModel SelectedPriority
        {
            get { return selectedPriority; }
            set { selectedPriority = value; OnProperyChanged("SelectedPriority"); }
        }
        private StatusViewModel selectedStatus;
        public StatusViewModel SelectedStatus {
            get { return selectedStatus; }
            set { selectedStatus = value; OnProperyChanged("SelectedStatus"); }
        }
        private MyTaskViewModel selectedMyTask;
        public MyTaskViewModel SelectedMyTask{
            get { return selectedMyTask; }
            set { selectedMyTask = value; OnProperyChanged("SelectedMyTask"); }
        }

        public int SelectedIndex1 { get; set; } = 1;
        public int SelectedIndex2 { get; set; } = 1;

        //Operations Commands ======================
        private RelayCommand addCategory;
        public RelayCommand AddCategory
        {
            get { return addCategory ?? (addCategory = new RelayCommand(
                (obj) =>
                {
                    Category addedCategory = new Category()
                    {
                        Id = 0,
                        Name = "NewCategory"
                    };
                    CategoryViewModel vm = new CategoryViewModel(addedCategory);
                    Categories_VM.Add(vm);
                    SelectedCategory = vm;
                }));}
        }
        private RelayCommand saveCategory;
        public RelayCommand SaveCategory
        {
            get { return saveCategory ?? (addCategory = new RelayCommand(
                (obj) =>
                {
                    var select1 = Categories_VM.Where(c=>c.Id == 0).ToList();
                    foreach(CategoryViewModel cvm in select1)
                    {
                        _dm.Categories.Add(new Category() { Name = cvm.Name });
                    }
                    _dm.SaveChanges();
                    LoadCategories();
                    System.Windows.MessageBox.Show("Sucessfully saved!","Informarion",System.Windows.MessageBoxButton.OK,System.Windows.MessageBoxImage.Information);
                }
                )); }
        }

        private RelayCommand deleteCategory;
        public RelayCommand DeleteCategory
        {
            get { return deleteCategory ?? (deleteCategory = new RelayCommand(
                (obj) => {
                    var deleted_Category = _dm.Categories.Where(c => c.Name == SelectedCategory.Name).FirstOrDefault();
                    if (deleted_Category != null)
                    {
                        _dm.Categories.Remove(deleted_Category);
                        _dm.SaveChanges();
                        LoadCategories();
                    }
                }
                )); }
        }

        private RelayCommand filterTasks;
        public RelayCommand FilterTasks
        {
            get{
                return filterTasks ?? (new RelayCommand(
                    (obj) =>
                    {   
                        var select2 = MyTasks_VM.Where(mt => mt.CategoryId == selectedCategory.Id).ToList();
                        //var select2 = MyTasks_VM.ToList();
                        MyTasks_VM.Clear();
                        Console.WriteLine(select2.Count);
                        if (select2.Count > 0)
                        {
                            foreach (MyTaskViewModel mtvm in select2)
                                MyTasks_VM.Add(mtvm);
                        }
                    }
                    ));}
        }
        //Property changed implement
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnProperyChanged([CallerMemberName] string prop = "")
        {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
