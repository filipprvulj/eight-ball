"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/reservationHub").build();

connection.on("InsertedReservation", function (reservation) {
    console.log(reservation);

    var htmlTable = document.getElementById('table').getElementsByTagName('tbody')[0];
    var row = htmlTable.insertRow();

    var email = row.insertCell(0);
    var table = row.insertCell(1);
    var start = row.insertCell(2);
    var end = row.insertCell(3);
    var actions = row.insertCell(4);

    var emailText = document.createTextNode(reservation.user.email);
    var tableText = document.createTextNode(reservation.table.name);
    var startText = document.createTextNode(reservation.appointment.start);
    var endText = document.createTextNode(reservation.appointment.end);
    var actionsText = document.createTextNode("");

    email.appendChild(emailText);
    table.appendChild(tableText);
    start.appendChild(startText);
    end.appendChild(endText);
    actions.appendChild(actionsText);
})

connection.start().then(function () {
    return console.log("Connection successful");
}).catch(function (err) {
    return console.log(err.toString());
})