$(document).ready(function () {

    $('.eventRequest').on('click', function () {

        axios.get('/events/jsondetails/' + $(this).data('id'))
        .then(function (response) {
            $("#detail-title").text(response.data.title);
            $("#detail-description").text(response.data.description);
            $("#detail-start").text(response.data.start.replace("T", " "));
            $("#detail-end").text(response.data.end.replace("T", " "));
        })
        .catch(function (error) {
            console.log(error);
        });
    });
});
