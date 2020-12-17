$(function () {
    $.get("billett/SjekkLoggetIn", function () {
    }).fail(function (feil) {
        if (feil.status == 401) {
            window.location.href = 'loggInn.html';  // ikke logget inn, redirect til loggInn.html
        }
        else {
            $("#feil").html("Feil på server - prøv igjen senere");
        }
    });
});

function leggTil() {

    // må ha med id inn skjemaet, hidden i html
    $("#avg1").val();
    $("#avg2").val();
    $("#avg3").val();
    $("#avg4").val();
    $("#avg5").val();
    $("#avg6").val();




    let listAvgangsTider =
        [$("#settid1").val(), $("#settid2").val(),
            $("#settid3").val(), $("#settid4").val(),
            $("#settid5").val(), $("#settid6").val()];

    let listAvganger =
        [$("#avg1").val(), $("#avg2").val(),
        $("#avg3").val(), $("#avg4").val(),
        $("#avg5").val(), $("#avg6").val()];

    listAvgangsTider = listAvgangsTider.join(',');
    listAvganger = listAvganger.join(',');

    const Rute =
    {
        BussNavn: $("#buss").val(),
        Pris: $("#pris").val(),
        Avganger: listAvganger,
        Tider: listAvgangsTider
    }

    $.post("billett/LeggTilRute", Rute, function (OK) {
        if (OK) {
            alert("Lagt til rute. NB! Denne vil også lage retur bussen");
            window.location.href = 'admin.html';
        }
        else {
            alert("Feil i db");
        }
    }).fail(function (feil) {
        if (feil.status == 401) {
            window.location.href = 'loggInn.html';  // ikke logget inn, redirect til loggInn.html
        }
        else {
            $("#feil").html("Feil på server - prøv igjen senere");
        };

    });
}
function loggut() {
    $.get("billett/LoggUt", function () {
        window.location.href = "/logginn.html";
    });
}


