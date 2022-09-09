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
    /// Interaction logic for AddClassroomWindow.xaml
    /// </summary>
    public partial class AddClassroomWindow : Window
    {
        public Classroom ClassroomProp;

        bool isEditClicked = false;
        List<Course> list;
        List<string> courseNames, days;
        public AddClassroomWindow()
        {
            courseNames = new List<string>();
            InitializeComponent();

            list = JsonConvert.DeserializeObject<List<Course>>(RequestHandler.GetRecord("courses/get-all").Result);
            list.ForEach(item => courseNames.Add($"{item.CourseCode} {item.CourseName}"));
            days = new List<string>() { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };
            courseCombobox.ItemsSource = courseNames;
            dayCombobox1.ItemsSource = days;
            dayCombobox2.ItemsSource = days;


        }

        public AddClassroomWindow(Classroom temp) : this()
        {
            ClassroomProp = temp;

        }

        public AddClassroomWindow(Classroom temp, bool editflag) : this(temp)
        {
            isEditClicked = editflag;
            classroomCodeTextbox.Text = ClassroomProp.ClassRoomCode;
            instructorTextbox.Text = ClassroomProp.Instructor;
            courseCombobox.SelectedItem = courseNames.Find(x => x.Contains($"{ClassroomProp.ClassCourse.CourseCode}"));
            IdTextbox.Text = $"{ClassroomProp.Id}";
            time1Tf.Text = $"{ClassroomProp.Time1.TimeOfDay}";
            time2Tf.Text = $"{ClassroomProp.Time2.TimeOfDay}";
            dayCombobox1.SelectedItem = days.Find(x=> x.Contains(ClassroomProp.Day1));
            dayCombobox2.SelectedItem = days.Find(x=> x.Contains(ClassroomProp.Day2));
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (courseCombobox.SelectedIndex != -1)
                {
                    string response;
                    var newClassroom = new Classroom()
                    {
                        ClassRoomCode = classroomCodeTextbox.Text,
                        Instructor = instructorTextbox.Text,
                        ClassCourse = list[courseCombobox.SelectedIndex],
                        Day1 = days[dayCombobox1.SelectedIndex] as string,
                        Day2 = days[dayCombobox2.SelectedIndex] as string,
                        Time1 = DateTime.Parse(time1Tf.Text),
                        Time2 = DateTime.Parse(time2Tf.Text),

                    };

                    if (isEditClicked)
                    {
                        newClassroom.Id = ClassroomProp.Id;
                        response = RequestHandler.UpdateRecord($"classrooms/update-classroom/{newClassroom.Id}", newClassroom).Result;
                    }
                    else
                        response = RequestHandler.SaveRecord("classrooms", newClassroom).Result;

                    var obj = JsonConvert.DeserializeObject<Classroom>(response);
                    ClassroomProp.CopyFrom(obj);
                    
                    Close();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
}
