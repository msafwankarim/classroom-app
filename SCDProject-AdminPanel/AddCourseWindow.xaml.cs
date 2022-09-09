using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using URSBackend.Models;

namespace SCDProject_AdminPanel
{
    /// <summary>
    /// Interaction logic for AddCourseWindow.xaml
    /// </summary>
    public partial class AddCourseWindow : Window
    {
        Course course;
        bool isEditClicked;
        public AddCourseWindow()
        {
            InitializeComponent();
        }
        public AddCourseWindow(Course _course)
        {
            InitializeComponent();
            course = _course;

        }
        public AddCourseWindow(Course _course, bool isEditClicked)
        {
            InitializeComponent();
            course = _course;
            this.isEditClicked = isEditClicked;

            courseCodeTextbox.Text = course.CourseCode;
            courseCodeTextbox.IsReadOnly = true;
            courseNameTextbox.Text = course.CourseName;
            creditHoursTextbox.Text = $"{course.CreditHours}";



        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (courseNameTextbox.Text.Length == 0 || courseCodeTextbox.Text.Length == 0)
            {
                MessageBox.Show("Error: Empty required fields detected");

            }

            Course responseObject = new Course();
            try
            {
                Course tempCourse = new Course()
                {

                    CourseCode = courseCodeTextbox.Text,
                    CourseName = courseNameTextbox.Text,
                    CreditHours = int.Parse(creditHoursTextbox.Text)
                };

                if (isEditClicked)
                {
                    tempCourse.CourseCode = this.course.CourseCode;

                    responseObject = JsonConvert.DeserializeObject<Course>(RequestHandler.UpdateRecord($"courses/update-course/{tempCourse.CourseCode}", tempCourse).Result);
                }
                else
                    responseObject = JsonConvert.DeserializeObject<Course>(RequestHandler.SaveRecord("courses", tempCourse).Result);

                course.CopyFrom(responseObject);
                Close();
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
}
