function displayNotification() {
  navigator.serviceWorker.getRegistration().then(function(reg) {
    const options = {
      body: "You have 1 new message",
      icon: "images/chataviselogo1.png",
      vibrate: [100, 50, 100],
      data: { primaryKey: 1 }
    };
    reg.showNotification("Chatavise", options);
  });
}

// self.addEventListener("push", function(event) {
//   console.log("notification");
//   const options = {
//     body: "You have 1 new message",
//     icon: "images/chataviselogo1.png",
//     vibrate: [100, 50, 100],
//     data: { primaryKey: 1 }
//   };
//   event.waitUntil(self.registration.showNotification("Chatavise", options));
// });

self.addEventListener('push', function(event) {
    const options = {
        body: event.data.text(),
        icon: "images/chataviselogo1.png",
        vibrate: [100, 50, 100],
        data: { primaryKey: 1 }
      };
    const promiseChain = self.registration.showNotification("Chatavise", options);
    event.waitUntil(promiseChain);
});
