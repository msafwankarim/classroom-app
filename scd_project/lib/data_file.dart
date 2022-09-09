enum RequestStatus { PENDING, APPROVED, REJECTED }
const api_url = "https://192.168.43.164:5001/api";

class Course {
  String courseName, instructorName;
  String day1, day2;
  String time1, time2;

  Course(this.courseName, this.instructorName);

  factory Course.fromJson(Map<String, dynamic> json) {
    Course course = Course(json['coursename'], json["instructor"]);
    course.day1 = json['day1'];
    course.day2 = json['day2'];
    course.time1 = json['time1'];
    course.time2 = json['time2'];
    return course;
  }
  factory Course.fromCourse(Course param) {
    Course course = Course(param.courseName, param.instructorName);
    course.day1 = param.day1;
    course.day2 = param.day2;
    course.time1 = param.time1;
    course.time2 = param.time2;
    return course;
  }
}

class Student {
  int id;
  String name;
  List<Course> courses = [];

  Student(this.id, this.name);
  Student.fromStudent(Student param) {
    this.id = param.id;
    this.name = param.name;
    this.courses = param.courses;
  }

  @override
  String toString() {
    return 'Student{id: $id, name: $name, courses: $courses}';
  }

  factory Student.fromJson(Map<String, dynamic> json) {
    Student student = Student(json['id'], json['name']);
    var classrooms = json['classrooms'];
    if (classrooms != null) {
      for (var room in classrooms) {
        student.courses.add(Course.fromJson(room));
      }
    } else
      student.courses = [];
    return student;
  }
}
