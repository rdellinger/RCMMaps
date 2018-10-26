$(document).ready(function () {


    // Hide all of the arrows when the page loads
    $('.arrow').hide();



    // Rotate the arrow 90 degrees when clicked
    $('.arrow').click(function () {
        $('.rollover').removeClass("active");
        $('.rollover').trigger('mouseout');
    });



    // When rolling over a header, switch the image src to the "on" version of the graphic
    $('.rollover').mouseover(function () {

        var imageOff = $(this).attr("src");
        var imageOn = imageOff.replace("_off", "_on");
        $(this).attr("src", imageOn);

        // also display the next image (the arrow)
        $(this).next().show();

    });




    // When rolling off a header, switch the image src to the "off" version of the graphic
    $('.rollover').mouseout(function () {

        // If this header hasn't been clicked on (doesn't have class "active")
        if (!($(this).hasClass("active"))) {

            // do the rollover effect
            var imageOn = $(this).attr("src");
            var imageOff = imageOn.replace("_on", "_off");
            $(this).attr("src", imageOff);

            // also, reset the arrow (which is next) to its original position and hide it
            $(this).next().animate({ rotate: 0 }).stop(true, true).hide();
        }
    });



    // When clicking on a header
    $('.rollover').click(function () {

        // If this header hasn't been clicked on (doesn't have class "active")
        if (!($(this).hasClass("active"))) {

            // remove the "active" class from all headers and reset all the graphics
            $('.rollover').removeClass("active");
            $('.rollover').trigger('mouseout');


            // add a class called "active" to know which head has been clicked on
            $(this).addClass("active");

            // switch the image src to the "on" version of the graphic
            var imageThisOff = $(this).attr("src");
            var imageThisOn = imageThisOff.replace("_off", "_on");
            $(this).attr("src", imageThisOn);

            // show the arrow and rotate it
            $(this).next().show().animate({ rotate: '+=90deg' });

        }

        // If this header is already active when you click on it, remove the "active" class and turn the graphic "off"
        else {
            $('.rollover').removeClass("active");
            $('.rollover').trigger('mouseout');
        }
    });





    // Function to extend jQuery and make a scrollTo function;  example: $('#wrapper').scrollTo(-200, 800);
    jQuery.fn.extend({
        scrollTo: function (fromTop, speed, easing) {
            return this.each(function () {
                var targetOffset = $(this).offset().top - fromTop;
                $('html,body').animate({ scrollTop: targetOffset }, speed, easing);
            });
        }
    });




    // When clicking on the logo, reset the page
    $('#logo').click(function () {
        $('#headerBetter').trigger('click');
        $('#headerBetter').trigger('click');
        $('.rollover').removeClass("active");
        $('.rollover').trigger('mouseout');
    });


    // When clicking on Better
    $('#headerBetter').click(function () {
        $('#headerBetter').scrollTo(259, 1000, 'easeOutCubic');
    });


    // When clicking on Different
    $('#headerDifferent').click(function () {
        $('#headerDifferent').scrollTo($(this).offset().top - 100, 1000, 'easeOutCubic');
    });


    // When clicking on Demystified
    $('#headerDemystified').click(function () {
        $('#headerDemystified').scrollTo($(this).offset().top - 202, 1000, 'easeOutCubic');
    });


    // When clicking on Proven
    $('#headerProven').click(function () {
        $('#headerProven').scrollTo($(this).offset().top - 305, 1000, 'easeOutCubic');
    });


    // When clicking on Red Comma
    $('#headerRedComma').click(function () {
        $('#headerRedComma').scrollTo($(this).offset().top - 404, 1000, 'easeOutCubic');
    });



    //stick the footer at the bottom of the page if we're on an iPad/iPhone due to viewport/page bugs in mobile webkit
    if (navigator.platform == 'iPad') {
        var pageHeight = $("body").attr("scrollHeight");
        $("#footerBar").css("position", "absolute");
        $("#footerBar").css("margintop", pageHeight);
    };

    // add some extra space to the bottom of iPhone pages because the text needs more space when it expands
    //if(navigator.platform == 'iPhone' || navigator.platform == 'iPod'){
    //	var pageHeight = $("body").attr("scrollHeight");
    // 	$("#footerBar").css("position", "absolute");
    // 	$("#footerBar").css("margintop", pageHeight + 200);
    //};


    // If the browser is IE7, makes some fixes to the CSS
    if ($.browser.msie && parseFloat($.browser.version) < 8) {
        $("#logoBar").css("margin-left", "-375px");
        $("#footerBar").css("margin-left", "-375px");
        $("#leftColumn").css("margin-left", "-375px");
        $("#topGradient").hide();
        $("#bottomGradient").hide();
        $('.arrow').attr("src", "images/spacer.gif");
        $('.arrow').css("height", "1px");
        $("#middleColumn").css("padding-bottom", "300px");
    }



    // Get the current year for the copyright in the footer
    $("#currentYear").text( (new Date).getFullYear() );

});

