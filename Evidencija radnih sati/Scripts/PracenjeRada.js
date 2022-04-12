$('#datepicker').change(function () {
    $("#frm").validate().cancelSubmit = true;
    $("#PromjenaDatuma").val("true");
    $("#frm").submit();
});

$(function () {
    $('.readonly').attr("readonly", "true");
});