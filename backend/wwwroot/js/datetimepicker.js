$(document).ready(function() {
    $(".datetimepicker").datetimepicker();
    $(".datetimepicker").datetimepicker({
        dayOfWeekStart: 1,
        format: 'm/d/Y H:i',
        step: 15,
    });
});
