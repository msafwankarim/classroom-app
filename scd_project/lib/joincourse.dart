import 'dart:convert';

import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;

import 'data_file.dart';

class JoinCoursePopup extends StatefulWidget {
  final Student student;
  JoinCoursePopup(this.student);
  @override
  _JoinCoursePopupState createState() => _JoinCoursePopupState(student);
}

class _JoinCoursePopupState extends State<JoinCoursePopup> {
  final Student student;
  final joiningCodeController = TextEditingController();
  final errorDialog = AlertDialog(
    title: Text("Error"),
    content: Text("Not connected to the server. Please try again later"),
  );

  _JoinCoursePopupState(this.student);

  @override
  Widget build(BuildContext context) {
    return SingleChildScrollView(
      child: Container(
        color: Color(0xff757575),
        child: Container(
          decoration: BoxDecoration(
            color: Colors.white,
            borderRadius: BorderRadius.only(
              topRight: Radius.circular(25),
              topLeft: Radius.circular(25),
            ),
          ),
          child: Padding(
            padding: const EdgeInsets.all(25),
            child: Column(
              children: [
                TextField(
                  textAlign: TextAlign.center,
                  style: TextStyle(fontSize: 25),
                  controller: joiningCodeController,
                  decoration: InputDecoration(
                      hintText: "Joining Code",
                      hintStyle: TextStyle(fontSize: 25, color: Colors.grey)),
                ),
                SizedBox(height: 15),
                FlatButton(
                  color: Colors.deepOrange,
                  onPressed: () async {
                    var response = await http.post(
                      Uri.parse("https://192.168.43.164:5001/api/requests"),
                      headers: <String, String>{
                        "Content-Type": "application/json; charset=UTF-8",
                      },
                      body: jsonEncode(<String, dynamic>{
                        "studentId": student.id,
                        "studentName": student.name,
                        "classCode": joiningCodeController.text
                      }),
                    );
                    print(response);
                    ScaffoldMessenger.of(context)
                        .showSnackBar(SnackBar(content: Text("Request sent")));
                    Navigator.pop(context);
                  },
                  child: Padding(
                    padding: const EdgeInsets.all(18),
                    child: Text(
                      "Join Course",
                      style: TextStyle(color: Colors.white),
                    ),
                  ),
                )
              ],
            ),
          ),
        ),
      ),
    );
  }
}
