import 'package:flutter/material.dart';
import 'package:scd_project/schedule.dart';

import 'data_file.dart';

class CoursePage extends StatelessWidget {
  final Course course;
  final String image;
  final Color color;

  CoursePage(this.course, this.image, this.color);

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        backgroundColor: color,
        title: Text(
          course.courseName,
          style: TextStyle(color: Colors.white),
        ),
      ),
      body: Column(
        children: [
          Center(
              child: CourseCoverCard(
                  course.courseName, course.instructorName, image, color)),
          Expanded(
            child: Center(
              child: ListView(
                children: [
                  TaskCard(
                    description: course.day1,
                    time: course.time1,
                  ),
                  TaskCard(
                    description: course.day2,
                    time: course.time2,
                  ),
                ],
              ),
            ),
          ),
          // Container(
          //   margin: EdgeInsets.only(top: 20, bottom: 10),
          //   child: FlatButton(
          //     padding: EdgeInsets.fromLTRB(50, 20, 50, 20),
          //     color: Colors.red,
          //     onPressed: () {},
          //     child: Text(
          //       "Leave Course",
          //       style: TextStyle(color: Colors.white),
          //     ),
          //   ),
          // ),
        ],
      ),
    );
  }
}

class CourseCoverCard extends StatelessWidget {
  final String courseName, instructor, image;
  final Color color;

  CourseCoverCard(this.courseName, this.instructor, this.image, this.color);

  @override
  Widget build(BuildContext context) {
    return Container(
      decoration: BoxDecoration(
        color: color,
        borderRadius: BorderRadius.circular(15),
      ),
      margin: EdgeInsets.all(10),
      padding: EdgeInsets.all(20),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Center(
            child: Container(
              height: 150,
              width: 150,
              child: Image.asset(image),
            ),
          ),
          Text(
            courseName,
            style: TextStyle(
                fontSize: 24, fontWeight: FontWeight.bold, color: Colors.white),
          ),
          SizedBox(
            height: 15,
          ),
          Text(
            "Instructed by $instructor",
            style: TextStyle(
                fontSize: 15, fontWeight: FontWeight.bold, color: Colors.white),
          )
        ],
      ),
    );
  }
}
