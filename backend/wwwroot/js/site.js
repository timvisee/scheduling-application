$(document).ready(function () {
    $('body').on('click', '.event-request', function () {
        axios.get('/events/jsondetails/' + $(this).data('id'))
        .then(function (response) {
            $("#detail-title").text(response.data.event.title);


            if (response.data.event.description !== null) {
                $("#detail-description").text(response.data.event.description);
            } else {
                $("#detail-description").text("No description available");
            }

            $("#detail-time-duration").text(response.data.timeDuration);
            $("#detail-start-date").text(response.data.startDate);
            $("#detail-end-date").text(response.data.endDate);
        })
        .catch(function (error) {
            console.log(error);
        });
    });
});
