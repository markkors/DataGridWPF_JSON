using DataGridWPF_JSON.models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DataGridWPF_JSON
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private List<Student> students;
        private string filePath = "students.json";

        public MainWindow()
        {
            InitializeComponent();
            filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\students.json");

            LoadData();
        }

        private void LoadData()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                students = JsonConvert.DeserializeObject<List<Student>>(json);
            }
            else
            {
                students = new List<Student>();
                SaveData(); // Maak het bestand met een aanvankelijke lege lijst
            }

            StudentDataGrid.ItemsSource = students;
        }

        private void SaveData()
        {
            string json = JsonConvert.SerializeObject(students, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }


        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            students.Add(new Student { Id = students.Count + 1, Name = "New Student", Age = 18 });
            StudentDataGrid.Items.Refresh();
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (StudentDataGrid.SelectedItem is Student selectedStudent)
            {
                students.Remove(selectedStudent);
                StudentDataGrid.Items.Refresh();
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string json = JsonConvert.SerializeObject(students, Formatting.Indented);
            File.WriteAllText(filePath, json);
            MessageBox.Show("Data saved successfully!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
