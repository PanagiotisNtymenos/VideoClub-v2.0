function callOMDbAPI(id, title) {
    $.getJSON('../OMDbAPIKEY.json').then(function (json) {
        var API_KEY = json.API_KEY;

        $.getJSON('https://www.omdbapi.com/?apikey=' + API_KEY + '&t=' + encodeURI(title)).then(function (result) {

            var poster = result.Poster;

            document.getElementById(id).src = poster;

        });
    });

}