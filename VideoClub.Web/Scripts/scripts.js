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
        $.ajax({
            type: 'POST',
            url: 'rentings/returnNow',
            data: JSON.stringify({ 'rentingId': button.attr("data-rentingId") }),
            contentType: 'application/json; charset=utf-8',
            success: function (response) {
                window.location.href = response.redirectToUrl;
            }
        })
    });
});
