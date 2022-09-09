import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import 'package:scd_project/data_file.dart';

class MyNotification {
  String heading, content, courseName;
  bool unread;
  RequestStatus requestStatus;

  MyNotification();

  factory MyNotification.fromJson(Map<String, dynamic> json) {
    MyNotification notification = MyNotification();
    notification.requestStatus = RequestStatus.values[json['status']];
    notification.courseName = json["classCode"] + " " + json["courseName"];
    notification.heading =
        "Request ${notification.requestStatus == RequestStatus.APPROVED ? "Approved" : "Rejected"}";
    notification.content =
        "Your request to join ${notification.courseName} has been ${notification.requestStatus == RequestStatus.APPROVED ? "Approved" : "Rejected"}";
    notification.unread = true;
    return notification;
  }
}

List<MyNotification> notifications = [];

class NotificationPage extends StatelessWidget {
  final Student student;

  NotificationPage(this.student) {
    generateList();
  }

  @override
  Widget build(BuildContext context) {
    return ListView.builder(
      padding: EdgeInsets.only(top: 10),
      itemCount: notifications.length,
      itemBuilder: (context, index) {
        // print(notifications[index].unread);
        return SimpleTextNotification(notifications.elementAt(index));
      },
    );
  }

  generateList() async {
    var response = await http
        .get(Uri.parse("$api_url/requests/notifications/${student.id}"));
    Iterable i = jsonDecode(response.body);
    for (var elem in i) {
      notifications.add(MyNotification.fromJson(elem));
    }

    //   notifications.add(makeCourseNotification("Mathematics"));
    //   notifications.add(makeCourseNotification("Networking"));
    //   notifications.add(makeCourseNotification("Hacking"));
    //   notifications.add(makeCourseNotification("Programming Fundamentals"));
    // }

    // MyNotification makeCourseNotification(String courseName) {
    //   return MyNotification(
    //       "Request accepted",
    //       "Your request to join $courseName has been accepted",
    //       courseName == "Networking",
    //       RequestStatus.APPROVED);
    // }
  }
}

class SimpleTextNotification extends StatefulWidget {
  final MyNotification notification;
  SimpleTextNotification(this.notification);
  @override
  _SimpleTextNotificationState createState() =>
      _SimpleTextNotificationState(notification);
}

class _SimpleTextNotificationState extends State<SimpleTextNotification> {
  MyNotification notification;
  _SimpleTextNotificationState(this.notification);

  @override
  Widget build(BuildContext context) {
    return Center(
      child: ListTile(
        tileColor: notification.unread ? Colors.black12 : Colors.white,
        title: Text(
          notification.heading,
          style: TextStyle(
            fontWeight:
                notification.unread ? FontWeight.bold : FontWeight.normal,
          ),
        ),
        subtitle: Text(
          notification.content,
          style: TextStyle(
            fontWeight:
                notification.unread ? FontWeight.bold : FontWeight.normal,
          ),
        ),
      ),
    );
  }
}
