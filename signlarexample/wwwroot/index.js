'use strict';

const connection = new signalR.HubConnectionBuilder().withUrl('/messagehub').configureLogging(signalR.LogLevel.Information).build();

connection.on('ReceiveMessage', function (user, message) {
    var msg = message;
    var encodedMsg = user + ' says ' + msg;
    var p = document.createElement('p');
    p.innerHTML = encodedMsg;
    document.getElementById('pushMessage').appendChild(p);
});

connection.on('ServerEvent', function (message) {
    var p = document.createElement('p');
    p.innerHTML = message;
    document.getElementById('serverEvent').appendChild(p);
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});

document.getElementById('sendButton').addEventListener('click', function (event) {
    var user = document.getElementById('userInput').value;
    var message = document.getElementById('messageInput').value;
    connection.invoke('SendMessage', user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});