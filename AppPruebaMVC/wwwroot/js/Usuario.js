$(document).ready(function () {
    $('input.autocomplete').autocomplete({
        data: function (request, response) {
            $.ajax({
                url: "@Url.Action("BusquedaPersona","Home")?busqueda" + request.term,
                dataType: "json",
                success: function (resp) {
                    response(resp)
                }
            })
        }
        /*data: {
            "Apple": null,
            "Microsoft": null,
            "Google": 'https://placehold.it/250x250'
        },*/
    });
});