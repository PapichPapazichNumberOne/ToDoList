using System.Collections.ObjectModel;
using System.ComponentModel;
using WpfApp5.DataAccess;
using WpfApp5.Models;

namespace WpfApp5.ViewModels
{
    public class TaskViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<TaskItem> _tasks;

        public ObservableCollection<TaskItem> Tasks
        {
            get { return _tasks; }
            set
            {
                _tasks = value;
                OnPropertyChanged(nameof(Tasks));
            }
        }

        
        public TaskViewModel()
        {
            Tasks = new ObservableCollection<TaskItem>();
        }

        public void AddTask(TaskItem task)
        {
            Tasks.Add(task);
            SaveTasks();
        }


        private void SaveTasks()
        {
            using (var context = new TaskDbContext())
            {
                foreach (var task in Tasks)
                {
                    if (task.Id == 0)
                    {
                        context.Tasks.Add(task);
                    }
                    else
                    {
                        context.Entry(task).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }
                }
                context.SaveChanges();
            }
        }



      
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}