// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function TextAreaProsses() {
    var object = document.getElementById("InputText");
    var text = object.value;
    var textPross = text.split("\n");
    var countLines = textPross.length;
    var textPross = text.split(" ");
    var countWords = textPross.length;
    document.getElementById("Lines").innerHTML = "Lines: " + countLines;
    document.getElementById("Words").innerHTML = "Words: " + countWords;
}

// Поиск на странице MachineJournal


