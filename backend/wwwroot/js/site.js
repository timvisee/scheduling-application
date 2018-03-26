$(document).ready(function () {
    $('body').on('click', '.eventRequest', function () {
        axios.get('/events/jsondetails/' + $(this).data('id'))
        .then(function (response) {
            $("#detail-title").text(response.data.event.title);
            $("#detail-description").text(response.data.event.description);
            $("#detail-timeDuration").text(response.data.timeDuration);
            $("#detail-start-date").text(response.data.startDate);
            $("#detail-end-date").text(response.data.endDate);
        })
        .catch(function (error) {
            console.log(error);
        });
    });
});
