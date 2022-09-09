import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import 'package:scd_project/data_file.dart';

import 'dashboard.dart';

class LoginPage extends StatefulWidget {
  @override
  _LoginPageState createState() => _LoginPageState();
}

class _LoginPageState extends State<LoginPage> {
  var emailController = TextEditingController();
  var passwordController = TextEditingController();
  String email, password;
  Future<void> _loginCallback(BuildContext context) async {
    email = emailController.text;
    password = passwordController.text;
    if (email.isEmpty || password.isEmpty) {
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(
          content: Text("Error: Empty fields detected"),
        ),
      );
      return;
    }
    // process email and password
    final response = await http.post(
      Uri.parse("https://192.168.43.164:5001/api/login/student"),
      headers: <String, String>{
        'Content-Type': 'application/json; charset=UTF-8'
      },
      body: jsonEncode(
          <String, dynamic>{'username': email, 'password': password}),
    );
    if (response.statusCode == 200) {
      Student student = Student.fromJson(jsonDecode(response.body));
      Navigator.pushReplacement(
        context,
        MaterialPageRoute(
          builder: (context) => Dashboard(student),
        ),
      );
    } else {
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(
          content: Text("Login Failed: Incorrect username or password"),
        ),
      );
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      resizeToAvoidBottomInset: false,
      // backgroundColor: Colors.white,
      appBar: AppBar(
        title: Text("Classroom Login"),
        backgroundColor: Colors.teal,
        centerTitle: true,
      ),
      body: Builder(
        builder: (context) => Column(
          mainAxisAlignment: MainAxisAlignment.center,
          mainAxisSize: MainAxisSize.min,
          children: <Widget>[
            Container(
                margin: EdgeInsets.only(top: 20),
                child: Image.asset("images/IIUI-logo.png")),
            //add card here for shadow look
            Padding(
              padding: const EdgeInsets.only(bottom: 20, top: 10),
              child: Column(
                children: [
                  Card(
                      margin: EdgeInsets.fromLTRB(20, 50, 20, 10),
                      elevation: 3.5,
                      child: ListTile(
                        leading: Icon(Icons.account_circle),
                        title: TextField(
                          controller: emailController,
                          decoration: InputDecoration(
                            border: InputBorder.none,
                            hintText: 'Username',
                          ),
                        ),
                      )),
                  Card(
                    margin: EdgeInsets.fromLTRB(20, 10, 20, 0),
                    elevation: 3.5,
                    child: ListTile(
                      leading: Icon(Icons.lock),
                      title: TextField(
                        obscureText: true,
                        controller: passwordController,
                        decoration: InputDecoration(
                          border: InputBorder.none,
                          hintText: 'Password',
                        ),
                      ),
                    ),
                  ),
                  SizedBox(
                    height: 20,
                  ),
                  ElevatedButton(
                    style: ButtonStyle(
                      padding: MaterialStateProperty.all(
                        EdgeInsets.fromLTRB(100, 20, 100, 20),
                      ),
                      shape: MaterialStateProperty.all(
                        RoundedRectangleBorder(
                          borderRadius: BorderRadius.circular(30),
                        ),
                      ),
                      backgroundColor: MaterialStateProperty.all(Colors.red),
                    ),
                    onPressed: () => _loginCallback(context),
                    child: Text(
                      "Login",
                      style: TextStyle(fontSize: 20.0),
                    ),
                  )
                ],
              ),
            ),
          ],
        ),
      ),
    );
  }
}
