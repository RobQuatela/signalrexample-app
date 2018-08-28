"use strict";

// const connection = new signalR.HubConnectionBuilder()
//   .withUrl("/messagehub")
//   .configureLogging(signalR.LogLevel.Information)
//   .build();

// connection.on("ReceiveMessage", function(user, message) {
//   var msg = message;
//   var encodedMsg = user + " says " + msg;
//   var p = document.createElement("p");
//   p.innerHTML = encodedMsg;
//   document.getElementById("pushMessage").appendChild(p);
// });

// connection.on("ServerEvent", function(message) {
//   var p = document.createElement("p");
//   p.innerHTML = message;
//   document.getElementById("serverEvent").appendChild(p);
// });

// connection.start().catch(function(err) {
//   return console.error(err.toString());
// });

// if (!("serviceWorker" in navigator)) {
//   console.log("doesnt support serviceWorker");
//   //return;
// } else {
//   console.log("supports serviceWorker!");
// }

// if (!("PushManager" in window)) {
//   console.log("doesn't support PushManager");
// } else {
//   console.log("supports PushManager!");
// }

function askPermission() {
  return new Promise(function(resolve, reject) {
    const permissionResult = Notification.requestPermission(function(result) {
      resolve(result);
    });

    console.log("Permission Result", permissionResult);

    if (permissionResult) {
      permissionResult.then(resolve, reject);
    }
  }).then(function(permissionResult) {
    if (permissionResult !== "granted") {
      throw new Error("we weren't granted permission");
    } else {
      subscribeUserToPush();
    }
  });
}

function displayNotification() {
  navigator.serviceWorker.getRegistration().then(function(reg) {
    const options = {
      body: "You have 1 new message",
      icon: "./images/chataviselogo1.png",
      vibrate: [100, 50, 100],
      data: { primaryKey: 1 }
    };
    reg.showNotification("Chatavise", options);
  });
}

function urlBase64ToUint8Array(base64String) {
  const padding = "=".repeat((4 - (base64String.length % 4)) % 4);
  const base64 = (base64String + padding)
    .replace(/\-/g, "+")
    .replace(/_/g, "/");

  const rawData = window.atob(base64);
  const outputArray = new Uint8Array(rawData.length);

  for (let i = 0; i < rawData.length; ++i) {
    outputArray[i] = rawData.charCodeAt(i);
  }
  return outputArray;
}

function registerServiceWorker() {
  return navigator.serviceWorker
    .register("service-worker.js")
    .then(function(registration) {
      console.log("Service Worker Registration", registration);
      return registration;
    })
    .catch(function(err) {
      console.err("Unable to register service worker", err);
    });
}

function subscribeUserToPush() {
  return registerServiceWorker()
    .then(function(registration) {
      const vapidKeys = urlBase64ToUint8Array(
        "BME6-YvoCvVwtas9T87mOfqI5ZfuQJiQRq-GaHWUWk3T7xW36gFUNrltXDgRZReOaSwEXq__EIuhH8DU7eQ9ITI"
      );
      const subscribeOptions = {
        userVisibleOnly: true,
        applicationServerKey: vapidKeys
      };

      return registration.pushManager.subscribe(subscribeOptions);
    })
    .then(function(pushSubscription) {
      //console.log("Received PushSubscription: ", pushSubscription);
      console.log("Push Subscription", JSON.stringify(pushSubscription));

      fetch("/api/subscriptions", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        mode: "same-origin",
        credentials: "same-origin",
        body: JSON.stringify(pushSubscription),
        redirect: "follow"
      }).then(function(response) {
        if (response.ok) {
          console.log("Successfully subscribed", response.body);
        } else {
          console.log("Failed to store subscription", response.body);
        }
      });
      // return pushSubscription;
    });
}



//window.addEventListener("load", askPermission);
window.addEventListener("load", function() {
  navigator.serviceWorker.getRegistration().then(function(registredServiceWorker) {
    registredServiceWorker.pushManager
      .getSubscription()
      .then(function(subscription) {
        console.log(subscription);
      });
  });
});

// navigator.serviceWorker.ready.then(function(reg) {
//   console.log(reg);
//   reg.pushManager.getSubscription().then(function(sub) {
//     console.log(sub);
//   });
// });

document
  .getElementById("sendButton")
  .addEventListener("click", function(event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message).catch(function(err) {
      return console.error(err.toString());
    });
    event.preventDefault();
  });

document
  .getElementById("showNotification")
  .addEventListener("click", displayNotification);
