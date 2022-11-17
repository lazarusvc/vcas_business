$(document).ready(function () {

    // AJAX LOADER
    // ==============================
    $('#loader').hide();
    jQuery.ajaxSetup({
        beforeSend: function () {
            $('#loader').show();
        },
        complete: function () {
            $('#loader').hide();
        },
        success: function () { }
    });

    $("input[type='text']").blur();

});