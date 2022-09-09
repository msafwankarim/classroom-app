import 'dart:math';

import 'package:flutter/material.dart';
import 'package:scd_project/coursepage.dart';

import 'data_file.dart';

class HomePage extends StatelessWidget {
  final Student student;

  HomePage(this.student) {
    print("Inside Homepage constructor $student");
  }

  @override
  Widget build(BuildContext context) {
    return Builder(
      builder: (context) => Center(
        child: Column(
          children: <Widget>[
            Padding(
              padding: const EdgeInsets.only(top: 10, bottom: 15),
              child: CircleAvatar(
                radius: 40.0,
                backgroundImage: AssetImage("images/empty-profile-pic.png"),
              ),
            ),
            Padding(
              padding: const EdgeInsets.only(bottom: 8.0),
              child: Text(
                "Hi ${student.name}!",
                style: TextStyle(fontSize: 25, fontWeight: FontWeight.bold),
              ),
            ),
            Text(
              "Welcome back! No assignments yet :)",
              style: TextStyle(color: Colors.grey, fontSize: 15),
            ),
            Divider(
              indent: 80,
              endIndent: 80,
              thickness: 1,
            ),
            Expanded(
              child: CourseFeed(student.courses),
            )
          ],
        ),
      ),
    );
  }
}

class CourseFeed extends StatelessWidget {
  final courses;
  final images = [
    "images/vector1.png",
    "images/vector2.png",
    "images/vector3.png",
    "images/vector4.png"
  ];
  final colors = [Colors.red, Colors.blue, Colors.blueGrey, Colors.brown];
  CourseFeed(this.courses);

  @override
  Widget build(BuildContext context) {
    return ListView(
      children: courses.isEmpty
          ? [Center(child: Text("No Courses Joined"))]
          : List<CourseCard>.from(
              courses.map(
                (e) => CourseCard(
                  e,
                  images[Random().nextInt(3)],
                  colors[Random().nextInt(3)],
                ),
              ),
            ),
    );
  }
}

class CourseCard extends StatelessWidget {
  final Course course;
  final String imagePath;
  final Color color;
  CourseCard(this.course, this.imagePath, this.color);
  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.fromLTRB(8, 2, 8, 5),
      child: GestureDetector(
        onTap: () {
          Navigator.push(
              context,
              MaterialPageRoute(
                  builder: (context) => CoursePage(course, imagePath, color)));
        },
        child: Stack(
          alignment: Alignment.center,
          children: [
            Card(
              color: color,
              child: ListTile(
                selectedTileColor: Colors.white30,
                title: Padding(
                  padding: EdgeInsets.fromLTRB(0, 18, 80, 50),
                  child: Text(
                    course.courseName,
                    overflow: TextOverflow.fade,
                    textScaleFactor: 1.8,
                    style: TextStyle(
                        color: Colors.white, fontWeight: FontWeight.normal),
                  ),
                ),
                subtitle: Text(
                  "Instructed by ${course.instructorName}",
                  style: TextStyle(
                      color: Colors.white, fontWeight: FontWeight.normal),
                ),
              ),
            ),
            Positioned(
              right: 5,
              height: 150,
              width: 150,
              child: ClipRect(
                child: Container(
                  child: imagePath.isNotEmpty
                      ? Image.asset(imagePath)
                      : Image.asset("images/empty-profile-pic.png"),
                ),
              ),
            )
          ],
        ),
      ),
    );
  }
}
