"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/LoginHub").build();


document.getElementById("loginButton").addEventListener("click", function () {

    //if i want to keep the login button as submit type, add this below:
    //event.preventDefault();

    var email = document.getElementById("loginEmail").value;
    var pass = document.getElementById("loginPassword").value;

    connection.invoke("Login", email, pass)
        .then(function (result) {
            if (result == true) {
                connection.stop();
                window.location.href = "/index"; // redirect to Index
            } else {
                document.getElementById("error-msg").innerText = "Invalid credentials.";
            }
        })
        .catch(function (err) {
            console.error(err.toString());
        });
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});