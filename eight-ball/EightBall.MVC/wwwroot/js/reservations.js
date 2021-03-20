"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/reservationHub").build();

connection.on("InsertedReservation", function (reservation) {
    console.log(reservation);
    formatDate(reservation.appointment.start)

    var htmlTable = document.getElementById('table').getElementsByTagName('tbody')[0];
    var row = htmlTable.insertRow();

    var email = row.insertCell(0);
    var table = row.insertCell(1);
    var start = row.insertCell(2);
    var end = row.insertCell(3);
    var actions = row.insertCell(4);

    var emailText = document.createTextNode(reservation.user.email);
    var tableText = document.createTextNode(reservation.table.name);
    var startText = document.createTextNode(formatDate(reservation.appointment.start));
    var endText = document.createTextNode(formatDate(reservation.appointment.end));
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

function formatDate(date) {
    const months = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
    let current_datetime = new Date(date)
    let formatted_date = appendLeadingZeroes(current_datetime.getDate()) + "-"
        + months[current_datetime.getMonth()] + "-"
        + current_datetime.getFullYear().toString().substr(2) + " "
        + appendLeadingZeroes(current_datetime.getHours()) + ":"
        + appendLeadingZeroes(current_datetime.getMinutes()) + ":"
        + appendLeadingZeroes(current_datetime.getSeconds())

    return formatted_date;
}

function appendLeadingZeroes(n) {
    if (n <= 9) {
        return "0" + n;
    }
    return n
}