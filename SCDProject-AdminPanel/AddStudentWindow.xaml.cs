using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using URSBackend.Models;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace SCDProject_AdminPanel
{
    
    /// <summary>
    /// Interaction logic for AddStudentWindow.xaml
    /// </summary>
    public partial class AddStudentWindow : Window
    {
        
        bool isEditClicked =  false;
        public Student student = null; 
        public AddStudentWindow()
        {
            InitializeComponent();
        }
        public AddStudentWindow(Student student)
        {
            InitializeComponent();
            this.student = student;

        }
        public AddStudentWindow(bool isEditClicked, Student student)
        {
            InitializeComponent();
            this.isEditClicked = isEditClicked;
            this.student = student;

            idTextbox.Text = $"{student.Id}";
            Title = $"Edit Student - {student.Name}";
            nameTextbox.Text = student.Name;
            addressTextbox.Text = student.Address;
            emailTextbox.Text = student.Email;
            usernameTextbox.Text = student.Username;
            passwordTextbox.Text = student.Password;
            creditHoursTextbox.Text = $"{student.CreditHoursPassed}";
            currentSemesterTextbox.Text = $"{student.CurrentSemester}";


        }

        private void SaveButtonClicked(object sender, RoutedEventArgs e)
        {
            if (nameTextbox.Text.Length == 0 || emailTextbox.Text.Length ==0)
            {
                MessageBox.Show("Error: Empty required fields detected");
                
            }

            Student responseObject = new Student();
            try
            {
                Student newStudent = new Student()
                {

                    Username = usernameTextbox.Text,
                    Password = passwordTextbox.Text,
                    Name = nameTextbox.Text,
                    Email = emailTextbox.Text,
                    Address = addressTextbox.Text,
                    CurrentSemester = int.Parse(currentSemesterTextbox.Text),
                    CreditHoursPassed = int.Parse(creditHoursTextbox.Text)
                };
                
                if (isEditClicked)
                {
                    newStudent.Id = student.Id;
                    responseObject = JsonConvert.DeserializeObject<Student>(RequestHandler.UpdateRecord($"students/update-student/{newStudent.Id}", newStudent).Result);
                }
                else
                    responseObject = JsonConvert.DeserializeObject<Student>(RequestHandler.SaveRecord("students", newStudent).Result);
                if (student == null)
                    student = responseObject;
                else student.CopyFrom(responseObject);
                
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            Close();



        }
    }
}
