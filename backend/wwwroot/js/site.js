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

    //init map
    var mymap = L.map('leafmap').setView([51.505, -0.09], 2);

    // Set up the tile layers
    L.tileLayer('https://api.mapbox.com/styles/v1/timvisee/cirawmn8f001ch4m27llnb45d/tiles/256/{z}/{x}/{y}?access_token=pk.eyJ1IjoidGltdmlzZWUiLCJhIjoiY2lyZXY5cDhzMDAxM2lsbTNicGViaTZkYyJ9.RqbUkoWLWeh_WZoyoxxt-Q', {
        attribution: 'Hosted by <a href="https://timvisee.com/" target="_blank">timvisee.com</a>'
    }).addTo(mymap);

    // use one marker instance
    var marker;

    // get lat and long from fields IF filled in
    var lat = $("#Latitude").val();
    var long = $("#Longitude").val();

    //construct a marker and add to the map
    if (lat && long){
        marker = L.marker([lat, long]).addTo(mymap);
        mymap.setView([lat, long], 15);
    }

    //create new marker and set fields
    mymap.on('click', function (e) {

        resetMarker(e.latlng);

        $("#Latitude").val(e.latlng.lat);
        $("#Longitude").val(e.latlng.lng);
    });

    $('#Latitude').bind('keyup change', function (e) {
        lat = $(this).val();

        console.log(lat);
        console.log(long);

        long = $("#Longitude").val();
        if (long)
            resetMarker([lat, long])
    });

    $('#Longitude').bind('keyup change', function (e) {
        long = $(this).val();

        lat = $("#Latitude").val();
        if (lat)
            resetMarker([lat, long])
    });

    function resetMarker(latlng) {
        if (marker == null)
            marker = L.marker(latlng).addTo(mymap);
        else
            marker.setLatLng(latlng);
    }
});

