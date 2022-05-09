// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
"use strict";
const IndexOfFirstElement = 0

var connection = new signalR.HubConnectionBuilder().withUrl("/demoKaibayHub").build();

$(window).bind('beforeunload', function () {
    connection.stop().then(() => {
        console.log("signalR connection closed");
    });
});

connection.on("UpdatePrice", function (auctionid, price) {
    $("#price-" + auctionid)[IndexOfFirstElement].innerHTML = price;
});

(async () => {
    try {
        await connection.start();
    }
    catch (e) {
        console.error(e.toString());
    }
})();