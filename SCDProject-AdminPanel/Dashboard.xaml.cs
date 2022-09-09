using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using URSBackend.Models;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Newtonsoft.Json;
using URSBackend.Models.ViewModels;

namespace SCDProject_AdminPanel
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        public List<Course> courses;
        public List<Student> students;
        public List<ClassroomVM> classrooms;
        private String currentDir = System.AppDomain.CurrentDomain.BaseDirectory;

        private Page[] pages = new Page[] { new StudentManagement(), new CourseManagement() };
        ImageSourceConverter converter = new ImageSourceConverter();
        public Dashboard()
        {
            InitializeComponent();
            studentDatagrid.IsReadOnly = true;
            courseDatagrid.IsReadOnly = true;
            classroomDatagrid.IsReadOnly = true;
            LoadStudents();
            LoadCourses();
            LoadClassrooms();
            RefreshStudentGrid();
            RefreshCourseGrid();
            RefreshClassroomGrid();
            //datagrid.HorizontalContentAlignment = HorizontalAlignment.Center;
        
        }


        private void NavigationButtonMouseEnter(object sender, MouseEventArgs e)
        {
            replaceImage(false, sender as Button);
            
        }
        private void NavigationButtonMouseLeave(object sender, MouseEventArgs e)
        {
            replaceImage(true, sender as Button);
        }
        public void replaceImage(bool leave, Button sender)
        {
            String color1="white", color2="black";
            Image img = new Image();
            switch (sender.Name)
            {
                case "studentsButton":
                    img = studentBtnImage;

                    break;
                case "requestButton":
                    img = requestBtnImage;

                    break;
            }
            if(leave) { color1 = "black"; color2 = "white"; }
            img.Source = (ImageSource)converter.ConvertFromString(img.Source.ToString().Replace(color1, color2));
        }

        private void studentsButton_Click(object sender, RoutedEventArgs e)
        {

            RefreshStudentGrid();
            courseDatagrid.Visibility = Visibility.Hidden;
            classroomDatagrid.Visibility = Visibility.Hidden;
            studentDatagrid.Visibility = Visibility.Visible;
        }

        private void requestButton_Click(object sender, RoutedEventArgs e)
        {
            //studentDatagrid.ItemsSource = courses;
            studentDatagrid.Visibility = Visibility.Hidden;
            classroomDatagrid.Visibility = Visibility.Hidden;
            courseDatagrid.Visibility = Visibility.Visible;

            RefreshCourseGrid();

        }
        public void LoadStudents()
        {
            //students = new List<Student>();

            students = JsonConvert.DeserializeObject<List<Student>>(RequestHandler.GetRecord("students/get-all").Result);
            
        }
        public void LoadCourses()
        {
            courses = JsonConvert.DeserializeObject<List<Course>>(RequestHandler.GetRecord("courses/get-all").Result);
            
        }
        public void LoadClassrooms()
        {
            classrooms = JsonConvert.DeserializeObject<List<ClassroomVM>>(RequestHandler.GetRecord("classrooms/get-all").Result);
        }

        public void RefreshStudentGrid() 
        {
            studentDatagrid.ItemsSource = null;
            studentDatagrid.ItemsSource = students;

        }
        public void RefreshCourseGrid()
        {
            courseDatagrid.ItemsSource = null;
            courseDatagrid.ItemsSource = courses;
        }
        public void RefreshClassroomGrid() 
        {
            classroomDatagrid.ItemsSource = null;
            classroomDatagrid.ItemsSource = classrooms;
        }


        public void AddStudent()
        {
            AddStudentWindow window = new AddStudentWindow();
            
            window.ShowDialog();
            if (window.student != null)
            {
                students.Add(window.student);
            }
            RefreshStudentGrid();


        }

        public void EditStudent()
        {
            Student student = studentDatagrid.SelectedItem as Student;

            if (student != null)
            {
                AddStudentWindow window = new AddStudentWindow(isEditClicked: true, student);
                window.ShowDialog();
                RefreshStudentGrid();
            }

        }
        public void DeleteStudent()
        {
            Student selectedStudent = studentDatagrid.SelectedItem as Student;
            if (selectedStudent != null)
            {
                RequestHandler.RemoveRecord($"students/remove/{selectedStudent.Id}");
                students.Remove(selectedStudent);
                RefreshStudentGrid();
            }
        }

        public void AddCourse()
        {
            Course course = new Course();
            AddCourseWindow window = new AddCourseWindow(course);
            window.ShowDialog();
            courses.Add(course);
            RefreshCourseGrid();


        }

        public void EditCourse()
        {
            Course course = courseDatagrid.SelectedItem as Course;

            if (course != null)
            {
                AddCourseWindow window = new AddCourseWindow(course, isEditClicked: true);
                window.ShowDialog();
                RefreshCourseGrid();
            }

        }

        public void DeleteCourse()
        {
            Course course = courseDatagrid.SelectedItem as Course;
            if (course != null)
            {
                RequestHandler.RemoveRecord($"courses/remove/{course.CourseCode}");
                courses.Remove(course);
                RefreshCourseGrid();
            }
        }

        public void AddClassroom()
        {
            Classroom temp = new Classroom();
            AddClassroomWindow addClassroomWindow = new AddClassroomWindow(temp);
            addClassroomWindow.ShowDialog();
            classrooms.Add(temp.GetViewModel());
            RefreshClassroomGrid();
        }

        public void EditClassroom()
        {
            ClassroomVM temp = classroomDatagrid.SelectedItem as ClassroomVM;
            if(temp != null)
            {
                Classroom room = JsonConvert.DeserializeObject<Classroom>(RequestHandler.GetRecord($"classrooms/get-full/{temp.Id}").Result);
                AddClassroomWindow window = new AddClassroomWindow(room, true);
                window.ShowDialog();
                temp.CopyFrom(room.GetViewModel());
                RefreshClassroomGrid();
            }    
        }

        public void DeleteClassroom()
        {
            ClassroomVM temp = classroomDatagrid.SelectedItem as ClassroomVM;
            if (temp != null)
            {
                RequestHandler.RemoveRecord($"classrooms/remove/{temp.Id}");
                classrooms.Remove(temp);
                RefreshClassroomGrid();
            }
        }

        private void AddButtonClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (studentDatagrid.Visibility == Visibility.Visible)
                    AddStudent();
                else if (courseDatagrid.Visibility == Visibility.Visible)
                    AddCourse();
                else if (classroomDatagrid.Visibility == Visibility.Visible)
                    AddClassroom();
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }

        }


        private void EditButtonClicked(object sender, RoutedEventArgs e)
        {
            if (studentDatagrid.Visibility == Visibility.Visible)
                EditStudent();
            else if (courseDatagrid.Visibility == Visibility.Visible)
                EditCourse();
            else if (classroomDatagrid.Visibility == Visibility.Visible)
                EditClassroom();
        }
        
        private void DeleteButtonClicked(object sender, RoutedEventArgs e)
        {
            if (studentDatagrid.Visibility == Visibility.Visible)
                DeleteStudent();
            else if (courseDatagrid.Visibility == Visibility.Visible)
                DeleteCourse();
            else if (classroomDatagrid.Visibility == Visibility.Visible)
                DeleteClassroom();
        }

        private void classroomButton_Click(object sender, RoutedEventArgs e)
        {
            studentDatagrid.Visibility = Visibility.Hidden;
            courseDatagrid.Visibility = Visibility.Hidden;

            RefreshClassroomGrid();
            classroomDatagrid.Visibility = Visibility.Visible;
            

        }

        private void classroomButton_MouseEnter(object sender, MouseEventArgs e)
        {
            
            classroomBtnImage.Source = (ImageSource)converter.ConvertFromString(classroomBtnImage.Source.ToString().Replace("white", "black"));
        }

        private void classroomButton_MouseLeave(object sender, MouseEventArgs e)
        {
            classroomBtnImage.Source = (ImageSource)converter.ConvertFromString(classroomBtnImage.Source.ToString().Replace("black", "white"));
        }
    }
}
