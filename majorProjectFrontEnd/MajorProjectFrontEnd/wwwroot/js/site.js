// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$(document).ready(function () {

    $('select.rank').on('change', function (e) {
        let selections = [];

        $('select').filter("[name=" + "'" + e.target.getAttribute("name") + "'" + "]").each(function () {
            selections.push($(this).val());
        });

        for (i = 0; i < selections.length; ++i) {
            console.log(selections[i]);
        }

        console.log("\n");


        if (selections.length == Array.from(new Set(selections)).length) {
            $("p.errorMessage").filter("." + $(this).attr('name')).html("");

        }
        else {
            $("p.errorMessage").filter("." + $(this).attr('name')).html("Duplicates are not allowed.");
            
        }

    });



    $('#menu-bar').on('click', function (e) {
        let nav = document.getElementById("nav");

        if (nav.className == "nav-hide") {

            nav.className = "nav-show";
        }
        else {

            nav.className = "nav-hide";
        }

    });



    $("#target").submit(function(e) {
        let invalid = false;
        $("p.errorMessage").each(function () {


            if ($(this).html() != "") {
                $("#mainErrorMessage").html("There are errors. Please review your responses.");
                invalid = true;
                return false;
            }
            else
            {
                $("#mainErrorMessage").html("");
            }
        }); 

        if (invalid == true) {
            return false;
        }
    });


    $("#survey").submit(function (e) {
        let startDate = new Date($("#startDate").val());
        let endDate = new Date($("#endDate").val());

        if (startDate >= endDate) {
            $(".errorMessage").html("Start date must not be on or after end date.");
            return false;
        }
    });


});