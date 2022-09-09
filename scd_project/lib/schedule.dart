import 'package:flutter/material.dart';
import 'package:intl/intl.dart';
import 'data_file.dart';

class TaskCard extends StatefulWidget {
  final String description, time;
  TaskCard({this.description, this.time});
  @override
  _TaskCardState createState() =>
      _TaskCardState(description: description, time: time);
}

class _TaskCardState extends State<TaskCard> {
  String description, time;
  bool isDone = false;
  _TaskCardState({this.description, this.time});
  @override
  Widget build(BuildContext context) {
    return Card(
      color: !isDone ? Colors.brown : Colors.green,
      margin: EdgeInsets.all(10),
      child: ListTile(
        onTap: () {
          setState(() {
            isDone = !isDone;
          });
        },
        contentPadding: EdgeInsets.all(10),
        // tileColor: ,
        title: Text(
          description,
          style: TextStyle(color: Colors.white),
        ),
        subtitle:
            Text("1 hour 30 minutes", style: TextStyle(color: Colors.white)),
        leading: Icon(
          !isDone ? Icons.alarm : Icons.done,
          color: Colors.white,
        ),
        trailing: Text(
          time,
          textScaleFactor: 1.5,
          style: TextStyle(fontWeight: FontWeight.bold, color: Colors.white),
        ),
      ),
    );
  }
}

class SchedulePage extends StatefulWidget {
  final Student student;
  // final doneTaskList = List<TaskCard>[];
  SchedulePage(this.student);

  @override
  _SchedulePageState createState() => _SchedulePageState(student);
}

class _SchedulePageState extends State<SchedulePage> {
  final Student student;
  List<TaskCard> upcomingTaskList = [];

  _SchedulePageState(this.student) {
    var today = DateFormat("EEEE").format(DateTime.now());
    for (var course in student.courses) {
      TaskCard card;
      if (course.day1 == today)
        card = TaskCard(
          description: course.courseName,
          time: course.time1,
        );
      else if (course.day2 == today)
        card = TaskCard(description: course.courseName, time: course.time2);
      if (card != null) upcomingTaskList.add(card);
    }
  }

  @override
  Widget build(BuildContext context) {
    return Center(
      child: ListView(
        children: this.upcomingTaskList == null || this.upcomingTaskList.isEmpty
            ? [Center(child: Text("No Classes Today"))]
            : this.upcomingTaskList,
      ),
    );
  }
}
