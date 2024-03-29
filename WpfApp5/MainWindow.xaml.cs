using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using System.IO;
using WpfApp5.Models;
using System;
using WpfApp5.DataAccess;
using WpfApp5.ViewModels;
using SQLitePCL;



namespace WpfApp5
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<TaskItem> tasks = new ObservableCollection<TaskItem>();
        private string tasksFilePath = "tasks.json";
        private TaskViewModel viewModel = new TaskViewModel();

        public MainWindow()
        {
            InitializeComponent();
            taskListView.ItemsSource = tasks;
            LoadTasks(); 
            Batteries.Init();
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {

            string title = taskTitleTextBox.Text;
            string description = taskDescriptionTextBox.Text;

            TaskItem newTask = new TaskItem
            {
                Title = title,
                Description = description,
                IsCompleted = false
            };

           
            viewModel.AddTask(newTask);

            taskTitleTextBox.Clear();
            taskDescriptionTextBox.Clear();
        }

        private void SaveTasks()
        {
         
            string tasksJson = JsonConvert.SerializeObject(tasks);
            File.WriteAllText(tasksFilePath, tasksJson);
        }

       
        private void LoadTasks()
        {
          
            if (File.Exists(tasksFilePath))
            {
                string tasksJson = File.ReadAllText(tasksFilePath);
                tasks = JsonConvert.DeserializeObject<ObservableCollection<TaskItem>>(tasksJson);
            }
        }

        private void taskTitleTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "Название задачи")
            {
                textBox.Text = "";
            }
        }
        private void OnApplicationExit(object sender, EventArgs e)
        {
            using (var context = new TaskDbContext())
            {
                context.SaveChanges();
            }
        }

        private void taskTitleTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Название задачи";
            }
        }

        private void taskDescriptionTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "Описание задачи")
            {
                textBox.Text = "";
            }
        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            TaskItem task = (TaskItem)checkBox.DataContext;
            task.IsCompleted = checkBox.IsChecked ?? false;
            SaveTasks();
        }


        private void taskDescriptionTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Описание задачи";
            }
        }
    }
}