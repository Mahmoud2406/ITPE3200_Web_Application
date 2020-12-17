



$(function () {
    //Henter ut tiden det ble kjøpt billetter
    let tid = sessionStorage.getItem("tid");
    $("#tid").html(tid);
    let tur = sessionStorage.getItem("billettTurId");
    let retur = sessionStorage.getItem("billettReturId");
    let pris = sessionStorage.getItem("pris");
    let url = "billett/hentBillett";


    //Henter ut billett hvis det kun var kjøpt en tur
    if (tur != null) {

        $.ajax({
            type: "POST",
            url: url + "?id=" +
                encodeURIComponent(tur),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (turBillett) {
                if (turBillett) {
                    let ut = "";
                    ut += "<tr><td>" + turBillett.avgang + "</td><td>" +
                        turBillett.destinasjon + "</td><td>" +
                        turBillett.tid + "</td></tr>";

                    $("#billett").append(ut);
                }
                else {
                    $("#feil").html("Feil i db - prøv igjen senere");
                }
            }
        });


    }

    //Henter ut retur billett hvis var kjøpt rettur
    if (retur != null) {
        pris = pris * 2;
        $.ajax({
            type: "POST",
            url: url + "?id=" +
                encodeURIComponent(retur),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (returBillett) {
                if (returBillett) {
                    let ut = "";
                    ut += "<tr><td>" + returBillett.avgang + "</td><td>" +
                        returBillett.destinasjon + "</td><td>" +
                        returBillett.tid + "</td></tr>";

                    $("#billett").append(ut);
                }
                else {
                    $("#feil").html("Feil i db - prøv igjen senere");
                }
            }
        });

    }

    $("#pris").html(pris + "kr");

});