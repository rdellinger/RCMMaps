$(document).ready(function () {


    // Set the focus on the search box by default
    $("#SearchString").focus();


    // Clear all localStorage items when any of the navigation links is clicked on the home page
    $(".homeNav").click(function () {
        localStorage.clear();
    });



    // ----------------- Save search to localStorage ------------------------//

    // Search text
    $("#boardSearchButton").click(function () {
        localStorage.clear();
        localStorage.setItem("SearchString", $('#SearchString').val());
        localStorage.setItem("ReportStatusID", $('#ReportStatusID').val());
        localStorage.setItem("ReportingPeriodID", $('#ReportingPeriodID').val());
    });



    // If the item exists in localStorage, then save it in the hidden field to post back
    if (localStorage.getItem('SearchString') !== null) {
        $('#SearchString').val(localStorage.getItem('SearchString'));
        $('#ReportStatusID').val(localStorage.getItem('ReportStatusID'));
        $('#ReportingPeriodID').val(localStorage.getItem('ReportingPeriodID'));
    };



    // Clear all localStorage items when the View All link is clicked
    $(".viewAll").click(function () {
        localStorage.clear();
    });


    // Clear all localStorage items when the Create button is clicked
    $(".createNew").click(function () {
        localStorage.clear();
    });


});