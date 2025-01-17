﻿$(document).ready(function () {

    // AJAX LOADER
    // **********************************
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
    //$(":button").click(function () {
    //    $('#loader').show();
    //});

    // iFrame CSS
    // **********************************
    $('#myIframe').ready(function () {
        $('#myIframe').contents().find('head')
            .html('<link href="/Content/site.css" rel="stylesheet">');
    });

    // DISBALE BUTTONS ON SUBMIT
    // **********************************
    $('form').submit(function () {
        $(this).find("button[type='submit']").prop('disabled', true);
    });

    // TINYMCE - WYSWYGET EDITOR
    // **********************************
    $('textarea#tinymce1').tinymce({
        height: 500,
        menubar: false,
        plugins: [
            'advlist', 'autolink', 'lists', 'link', 'image', 'charmap', 'preview',
            'anchor', 'searchreplace', 'visualblocks', 'fullscreen',
            'insertdatetime', 'media', 'table', 'code', 'help', 'wordcount'
        ],
        toolbar: 'undo redo | blocks | bold italic backcolor | ' +
            'alignleft aligncenter alignright alignjustify | ' +
            'bullist numlist outdent indent | removeformat | help'
    });
    $('textarea#tinymce2').tinymce({
        height: 500,
        menubar: false,
        plugins: [
            'advlist', 'autolink', 'lists', 'link', 'image', 'charmap', 'preview',
            'anchor', 'searchreplace', 'visualblocks', 'fullscreen',
            'insertdatetime', 'media', 'table', 'code', 'help', 'wordcount'
        ],
        toolbar: 'undo redo | blocks | bold italic backcolor | ' +
            'alignleft aligncenter alignright alignjustify | ' +
            'bullist numlist outdent indent | removeformat | help'
    });

    // Select 2 DROPDOWN 
    // **********************************
    $(".select2").select2({
        theme: "classic"
    });

});



