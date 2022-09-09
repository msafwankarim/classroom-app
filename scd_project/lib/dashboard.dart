import 'package:flutter/material.dart';
import 'package:scd_project/data_file.dart';
import 'package:scd_project/homepage.dart';
import 'notifications.dart';
import 'joincourse.dart';
import 'schedule.dart';

class Dashboard extends StatefulWidget {
  final Student student;
  @override
  _DashboardState createState() => _DashboardState(student);

  Dashboard(this.student);
}

class _DashboardState extends State<Dashboard> {
  int _currentIndex = 0;
  static Student student;
  bool floatingButtonVisible = true;
  List<Widget> _pages;

  _DashboardState(Student student) {
    _DashboardState.student = student;
    _pages = [
      HomePage(_DashboardState.student),
      NotificationPage(_DashboardState.student),
      SchedulePage(_DashboardState.student),
    ];
  }
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      floatingActionButton: Visibility(
        visible: floatingButtonVisible,
        child: FloatingActionButton(
          child: Icon(Icons.add),
          backgroundColor: Colors.red,
          onPressed: () {
            showModalBottomSheet(
                context: context,
                builder: (context) => JoinCoursePopup(student));
          },
        ),
      ),
      resizeToAvoidBottomInset: false,
      bottomNavigationBar: BottomNavigationBar(
          onTap: (index) {
            setState(() {
              floatingButtonVisible = index == 0 ? true : false;
              _currentIndex = index;
            });
          },
          currentIndex: _currentIndex,
          type: BottomNavigationBarType.shifting,
          items: [
            BottomNavigationBarItem(
                icon: Icon(Icons.home),
                label: "Home",
                backgroundColor: Colors.brown),
            BottomNavigationBarItem(
                backgroundColor: Colors.red,
                icon: Icon(Icons.notifications_active),
                label: "Notifications"),
            BottomNavigationBarItem(
                icon: Icon(Icons.alarm),
                label: "Schedule",
                backgroundColor: Colors.blue),
          ]),
      appBar: AppBar(
        iconTheme: IconThemeData(color: Colors.teal),
        // toolbarOpacity: 0.7,
        centerTitle: true,
        backgroundColor: Colors.white,
        title: Text("Dashboard", style: TextStyle(color: Colors.brown)),
      ),
      body: _pages[_currentIndex],
    );
  }
}
