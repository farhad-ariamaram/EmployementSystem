
(function ($) {
    "use strict";




    for (var i = 2; i < 7; i++) {
        $("#section" + i).hide();
    }

    $("#licensesection").hide();
    $('#licensecheckbox').change(function () {
        if (this.checked) {
            $("#licensesection").show();
        } else {
            $("#licensesection").hide();
        }
    });

    $("#moredescription").hide();
    $('#moredescheckbox').change(function () {
        if (this.checked) {
            $("#moredescription").show();
        } else {
            $("#moredescription").hide();
        }
    });

    $('.justPersian').keypress(function (e) {
        var txt = String.fromCharCode(e.which);
        if (!persianRex.text.test(txt)) {
            return false;
        }
    });

    /*==================================================================
    [ Focus Contact2 ]*/
    $('.input3').each(function () {
        $(this).on('blur', function () {
            if ($(this).val().trim() != "") {
                $(this).addClass('has-val');
            }
            else {
                $(this).removeClass('has-val');
            }
        })
    })


    /*==================================================================
    [ Chose Radio ]*/
    $("#radio1").on('change', function () {
        if ($(this).is(":checked")) {
            $('.input3-select').slideUp(300);
        }
    });

    $("#radio2").on('change', function () {
        if ($(this).is(":checked")) {
            $('.input3-select').slideDown(300);
        }
    });



    /*==================================================================
    [ Validate ]*/
    var name = $('.validate-input input[name="name"]');
    var email = $('.validate-input input[name="email"]');
    var message = $('.validate-input textarea[name="message"]');


    $('.validate-form').on('submit', function () {
        var check = true;

        if ($(name).val().trim() == '') {
            showValidate(name);
            check = false;
        }


        if ($(email).val().trim().match(/^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{1,5}|[0-9]{1,3})(\]?)$/) == null) {
            showValidate(email);
            check = false;
        }

        if ($(message).val().trim() == '') {
            showValidate(message);
            check = false;
        }

        return check;
    });


    $('.validate-form .input3').each(function () {
        $(this).focus(function () {
            hideValidate(this);
        });
    });

    function showValidate(input) {
        var thisAlert = $(input).parent();

        $(thisAlert).addClass('alert-validate');
    }

    function hideValidate(input) {
        var thisAlert = $(input).parent();

        $(thisAlert).removeClass('alert-validate');
    }

})(jQuery);



function s2tos3(e) {
    e.preventDefault();
    $('#section2').hide();
    $('#section3').show();
}

function s3tos4(e) {
    e.preventDefault();
    $('#section3').hide();
    $('#section4').show();
}

function s4tos5(e) {
    e.preventDefault();
    $('#section4').hide();
    $('#section5').show();
}

function s5tos6(e) {
    e.preventDefault();
    $('#section5').hide();
    $('#section6').show();
}
