$(function () {

    hentAlleStrekninger();
});



function loggut() {
    $.get("billett/LoggUt", function () {
            window.location.href = "/logginn.html";
    });
}


function hentAlleStrekninger() {


    $.get("billett/HentAlleRuter", function (bussruter) {
        let ut = "";

        console.dir(bussruter);
        let teller = 0;
        for (let bussrute of bussruter.result) {
            if (teller % 2 == 0) {
                ut += "<tr style='background-color: #E0E0E0;'>";
            }
            else {
                ut += "<tr>";
            }
            ut += "<td class='align-middle'> " + " <button class='btn' type='button' data-toggle='collapse' data-target='#" + bussrute.buss.bussNavn +
                "' aria-expanded='false' aria-controls='" + bussrute.buss.bussNavn +  "'><i class='fa fa-arrow-down' aria-hidden='true'></i>"+
                "</button>" +
                " <strong> <i class='fa fa-bus' aria-hidden='true'></i> " + bussrute.buss.bussNavn + "</strong>"; 
            let first = true;
            ut += "<td  class='align-middle'>";
            for (let avgang of bussrute.rute.avganger) {
                if (first) {
                    ut += "<strong >" + avgang.stopp.stasjonNavn + "</strong> ";
                    first = false;
                }
                else {
                    ut += "<i class='fa fa-arrow-right' aria-hidden='true'></i>" + " <strong>" + avgang.stopp.stasjonNavn + "</strong> ";
                }
            }
            ut += "</td>";
            ut += "<td class='align-middle'>" + bussrute.rute.pris + "kr </td>";
            ut += "<td class='align-middle' > <a class='btn btn-primary' href='endre.html?id=" + bussrute.buss_RuteId + "'><i class='fa fa-edit'></i></a> ";
            ut += " <button type='button' class='btn btn-danger' onClick='slett(" + bussrute.buss_RuteId + ")'><i class='fa fa-trash'></i></button></td>";
            ut += "<div class='collapse' id='collapse" + bussrute.buss_RuteId + "'> <div class='card card-body'>  ente ea proident  </div ></div>" + "</tr>";
            ut += "<tr><td colspan='1'></td><td colspan='1'><div class='collapse ' id='" + bussrute.buss.bussNavn + "'> <div class='card card-body'>  ";
            ut += "<strong> Avgangstider </strong>";
            for (let avgang of bussrute.rute.avganger) {
                ut += avgang.stopp.stasjonNavn + " :  ";
                for (let i = 0; i < avgang.tider.length; i++) {
                    if (avgang.tider[i].length == 0) {
                        ut += avgang.tider[i].tid;
                    }
                    else {
                        ut += avgang.tider[i].tid + "&nbsp&nbsp&nbsp";
                    }
                    if (avgang.tider[i] == avgang.tider.length - 1) {
                        ut += "<br/>";
                    }
                }
                ut += "<br/>";
            }
            ut += " </div ></div><td/><tr/><td></td><td></td>";
            teller++;
        }
        $("#strekning").append(ut);
        
    }).fail(function (feil) {
        if (feil.status == 401) {
            window.location.href = 'loggInn.html';  // ikke logget inn, redirect til loggInn.html
        }
        else {
            $("#feil").html("Feil på server - prøv igjen senere");
        }
    });;


   

}




function slett(id) {
    console.log("Denne" + id);
    $.ajax({
        type: "POST",
        url: "billett/SlettRute?id=" +
            encodeURIComponent(id),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (OK) {
            alert("Slettet rute");
            location.reload();

        },
        failure: function (response) {
            // feilhåndtering ajax kall
            alert(response.d);
        }
    });
        }

