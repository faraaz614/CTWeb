$(document).ready(function () {
    $('.toggle').on('click', function () {
        $('.container').stop().addClass('active');
    });

    $('.close').on('click', function () {
        $('.container').stop().removeClass('active');
    });
    var errorMessage = $("#hfErrorMessage").val();
    if (errorMessage !== "") {
        $("#loginAlert").modal('show');
    }
});