$(function () {
    $("#movieSearch").on('keydown', function () {
        $('#movieId').val("");
    });

    $("#movieSearch").autocomplete({
        source: '/rentings/MoviesAutoComplete',
        select: function (event, ui) {
            $('#movieId').val(ui.item.movieId);
        }
    });
});

$(function () {
    $("#movieTitle").autocomplete({
        source: '/movies/MoviesAutoComplete',
        select: function (event, ui) {
            $('#movieId').val(ui.item.movieId);
        }
    });
});

$(function () {
    $("#userSearch").autocomplete({
        source: '/rentings/UsersAutoComplete'
    });
});

$(document).ready(function () {
    $('#genresDropDown').selectize({
        maxItems: 5
    });
});

$(document).ready(function () {
    $('.btn-return-renting').on('click', function (e) {
        var button = $(e.target);
        bootbox.confirm({
            title: "Επιστροφή Κράτησης",
            message: "Id: #" + button.attr("data-rentingId") + "<br>Τίτλος: " + button.attr("data-rentingMovie") + "<br>Πελάτης: " + button.attr("data-rentingUser"),
            callback: function (result) {
                if (result) {
                    $.ajax({
                        type: 'POST',
                        url: '/rentings/returnNow',
                        data: JSON.stringify({ 'rentingId': button.attr("data-rentingId") }),
                        contentType: 'application/json; charset=utf-8',
                        success: function (response) {
                            var status = response.status;
                            if (status == "OK") {
                                bootbox.alert("Η επιστροφή ήταν επιτυχής!");
                                $("#" + button.attr("data-rentingId")).remove();
                            } else {
                                bootbox.alert("Η επιστροφή απέτυχε.");
                            }
                        },
                        error: function () {
                            bootbox.alert("Η επιστροφή απέτυχε :(");
                        }
                    })
                }
            }
        })
    });
});