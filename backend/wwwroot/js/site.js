﻿$(document).ready(function () {

    /**
     * Show the modal with event details
     *
     * @param {int} id The id of the event
     */
    function showModal(id) {
        axios.get('/events/jsondetails/' + id)
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

        $('#event_details').modal();
    }

    /**
     * Initialize the calendar
     */
    $('#calendar').fullCalendar({
        header: {center: 'month,agendaWeek,listWeek'}, // Multiple views
        theme: 'bootstrap4',
        height: 650,
        defaultView: 'agendaWeek',
        timeFormat: 'H:mm',
        slotLabelFormat: 'H:mm',
        scrollTime: '07:00:00',
        columnHeaderFormat: 'dddd',
        nowIndicator: true,
        // businessHours: {
        //     dow: [ 1, 2, 3, 4, 5 ], // Monday - Friday
        //     start: '08:00',
        //     end: '18:00'
        // },
        dayClick: function () {
            console.log('Clicked day')
        },
        eventClick: function (calEvent, jsEvent, view) {
            showModal(calEvent.id);
        },
        eventSources: [
            {
                url: '/events/jsonlist/'
            }
        ]
    });
});
