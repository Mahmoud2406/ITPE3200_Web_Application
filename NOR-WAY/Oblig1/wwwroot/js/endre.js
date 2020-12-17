
function loggut() {
    $.get("billett/LoggUt", function () {
        window.location.href = "/logginn.html";
    });
}

$(function () {
    const id = window.location.search.substring(1);
    const url = "billett/HentEnRute?" + id;

    $.get(url, function (ok) {
        console.dir(ok.result)
        $("#buss").val(ok.result.buss.bussNavn); // må ha med id inn skjemaet, hidden i html
        $("#avg1").val(ok.result.rute.avganger[0].stopp.stasjonNavn);
        $("#avg2").val(ok.result.rute.avganger[1].stopp.stasjonNavn);
        $("#avg3").val(ok.result.rute.avganger[2].stopp.stasjonNavn);
        $("#avg4").val(ok.result.rute.avganger[3].stopp.stasjonNavn);
        $("#avg5").val(ok.result.rute.avganger[4].stopp.stasjonNavn);
        $("#avg6").val(ok.result.rute.avganger[5].stopp.stasjonNavn);
        $("#pris").val(ok.result.rute.pris);

        // id'ene 
        
        $("#bussruteid").val(ok.result.buss_RuteId); // må ha med id inn skjemaet, hidden i html
        $("#bussid").val(ok.result.buss.bussId); // må ha med id inn skjemaet, hidden i html
        $("#avg1Id").val(ok.result.rute.avganger[0].stopp.stasjonId);
        $("#avg2Id").val(ok.result.rute.avganger[1].stopp.stasjonId);
        $("#avg3Id").val(ok.result.rute.avganger[2].stopp.stasjonId);
        $("#avg4Id").val(ok.result.rute.avganger[3].stopp.stasjonId);
        $("#avg5Id").val(ok.result.rute.avganger[4].stopp.stasjonId);
        $("#avg6Id").val(ok.result.rute.avganger[5].stopp.stasjonId);
        $("#ruteId").val(ok.result.rute.ruteId);

        // Avgang ide'ene for å lagre nye avgangstider
        $("#avganger1").val(ok.result.rute.avganger[0].stoppId);
        $("#avganger2").val(ok.result.rute.avganger[1].stoppId);
        $("#avganger3").val(ok.result.rute.avganger[2].stoppId);
        $("#avganger4").val(ok.result.rute.avganger[3].stoppId);
        $("#avganger5").val(ok.result.rute.avganger[4].stoppId);
        $("#avganger6").val(ok.result.rute.avganger[5].stoppId);




        for (let tid of ok.result.rute.avganger[0].tider) {
            $("#tid1").append(tid.tid+"<br/>");
            console.log(tid.tid);
        }
        for (let tid of ok.result.rute.avganger[1].tider) {
            $("#tid2").append(tid.tid + "<br/>");
            console.log(tid.tid);
        }
        for (let tid of ok.result.rute.avganger[2].tider) {
            $("#tid3").append(tid.tid + "<br/>");
            console.log(tid.tid);
        }
        for (let tid of ok.result.rute.avganger[3].tider) {
            $("#tid4").append(tid.tid + "<br/>");
            console.log(tid.tid);
        }
        for (let tid of ok.result.rute.avganger[4].tider) {
            $("#tid5").append(tid.tid + "<br/>");
            console.log(tid.tid);
        }
        for (let tid of ok.result.rute.avganger[5].tider) {
            $("#tid6").append(tid.tid+"<br/>");
            console.log(tid.tid);
        }
       
    }).fail(function (feil) {
        if (feil.status == 401) {
            window.location.href = 'loggInn.html';  // ikke logget inn, redirect til loggInn.html
        }
        else {
            $("#feil").html("Feil på server - prøv igjen senere");
        }
    });

    console.log($("#ruteId").val());
});



function leggTilTid() {

    validerTidInput();

    if (validerTidInput) {

        let arrayTid = [$("#settid1").val(), $("#settid2").val(), $("#settid3").val(),
        $("#settid4").val(), $("#settid5").val(), $("#settid6").val()];

        let arrayId = [$("#avganger1").val(), $("#avganger2").val(), $("#avganger3").val(),
        $("#avganger4").val(), $("#avganger5").val(), $("#avganger6").val()];

        for (let i = 0; i < arrayTid.length; i++) {
            let split = arrayTid[i].split(":");
            let time = split[0];
            let minutt = split[1];

            const tid = {
                id: arrayId[i],
                hours: time,
                minutes: minutt,
                seconds: 0// endrer ikke på sekunder
            };

            $.post("billett/LeggTilTId", tid, function (OK) {
                if (OK) {
                    //  window.location.href = 'index.html';
                    if (i == arrayTid.length - 1) {
                        alert("Lagt til! Trykk på visOppdaterte knappen");
                    }
                }
                else {
                    $("#feil").html("Feil i db - prøv igjen senere");
                }
            }).fail(function (feil) {
                if (feil.status == 401) {
                    window.location.href = 'loggInn.html';  // ikke logget inn, redirect til loggInn.html
                }
                else {
                    $("#feil").html("Feil på server - prøv igjen senere");
                }
            });
        }

    }
   
}
function visOppdaterteTider() {
   

    const id = window.location.search.substring(1);
    const url = "billett/HentEnRute?" + id;
    $.get(url, function (ok) {
        let ut = "";
        for (let tid of ok.result.rute.avganger[0].tider) {
            ut += tid.tid + "<br/>";
        }
        $("#tid1").html(ut);
        let ut1 = "";
        for (let tid of ok.result.rute.avganger[1].tider) {
            ut1 += tid.tid + "<br/>";
         
        }
        $("#tid2").html(ut1);
        let ut2 = "";
        for (let tid of ok.result.rute.avganger[2].tider) {
            ut2 += tid.tid + "<br/>";
         
        }
        $("#tid3").html(ut2);
        let ut3 = "";
        for (let tid of ok.result.rute.avganger[3].tider) {
            ut3 += tid.tid + "<br/>";
         
        }
        $("#tid4").html(ut3);
        let ut4 = "";
        for (let tid of ok.result.rute.avganger[4].tider) {
            ut4 += tid.tid + "<br/>";
          
        }
        $("#tid5").html(ut4);
        let ut5 = "";
        for (let tid of ok.result.rute.avganger[5].tider) {
            ut5 += tid.tid + "<br/>";
          
        }
        $("#tid6").html(ut5);
    });
   
}


function endreBuss() {
    const buss = {
        bussId: $("#bussid").val(),
        bussNavn: $("#buss").val()
    };

    $.post("billett/EndreBuss", buss, function (OK) {
        if (OK) {
            //  window.location.href = 'index.html';
        }
        else {
            $("#feil").html("Feil i db - prøv igjen senere");
        }
    }).fail(function (feil) {
            if (feil.status == 401) {
                window.location.href = 'loggInn.html';  // ikke logget inn, redirect til loggInn.html
            }
            else {
                $("#feil").html("Feil på server - prøv igjen senere");
            }
        });
  }

function endreAlt() {
    validerNavn();
    validerPris();
    validerAngangInput();

    if (validerNavn() && validerAngangInput && validerPris) {
        endreBuss();
        endreStasjoner();
        oppdaterRetur();
        alert("NB! Endringene påvirker også tur/retur strekningen");
        window.location.href = 'admin.html';
    }
}

function oppdaterRetur() {
    const reise = {
        BussRuteId: $("#bussruteid").val(),
        RuteId: $("#ruteId").val(),
        StasjonId:  $("#avg1Id").val(),
        Pris: $("#pris").val(),
        BussNavn: $("#buss").val()
    };
    $.post("billett/OppdaterRetur", reise, function (OK) {
        if (OK) {
            // window.location.href = 'index.html';
        }
        else {
            $("#feil").html("Feil i db - prøv igjen senere");
        }
    })
        .fail(function (feil) {
            if (feil.status == 401) {
                window.location.href = 'loggInn.html';  // ikke logget inn, redirect til loggInn.html
            }
            else {
                $("#feil").html("Feil på server - prøv igjen senere");
            }
        });
}

function endreStasjoner() {
    const avgang1 = {
        stasjonId: $("#avg1Id").val(),
        stasjonNavn: $("#avg1").val()
    };
    const avgang2 = {
        stasjonId: $("#avg2Id").val(),
        stasjonNavn: $("#avg2").val()
    };
    const avgang3 = {
        stasjonId: $("#avg3Id").val(),
        stasjonNavn: $("#avg3").val()
    };
    const avgang4 = {
        stasjonId: $("#avg4Id").val(),
        stasjonNavn: $("#avg4").val()
    };
    const avgang5 = {
        stasjonId: $("#avg5Id").val(),
        stasjonNavn: $("#avg5").val()
    };
    const avgang6 = {
        stasjonId: $("#avg6Id").val(),
        stasjonNavn: $("#avg6").val()
    };

    let listeStasjoner = [avgang1, avgang2, avgang3,avgang4,avgang5,avgang6];

    listeStasjoner.forEach(stasjon => {
        $.post("billett/EndreStasjon", stasjon, function (OK) {
            if (OK) {
               // window.location.href = 'index.html';
            }
            else {
                $("#feil").html("Feil i db - prøv igjen senere");
            }
        })
            .fail(function (feil) {
                if (feil.status == 401) {
                    window.location.href = 'loggInn.html';  // ikke logget inn, redirect til loggInn.html
                }
                else {
                    $("#feil").html("Feil på server - prøv igjen senere");
                }
            });

    });
}



function validerNavn() {
    const buss = $("#buss").val();
    const regexp = /^[a-zæøåA-ZÆØÅ.\- ]{2,20}$/;
    const ok = regexp.test(buss);
    if (!ok) {
        $("#message0").css('color', 'red');
        $("#message0").html("Ugjyldig navn!");
        return false;
    }
    else {
        $("#message0").html("");
        return true;
    }
}

function validerPris() {
    const pris = $("#pris").val();
    const regexp = /^(\d*([.,](?=\d{3}))?\d+)+((?!\2)[.,]\d\d)?$/;
    const ok = regexp.test(pris);
    if (!ok) {
        $("#message1").css('color', 'red');
        $("#message1").html("Ugjyldig Pris!");
        return false;
    }
    else {
        $("#message1").html("");
        return true;
    }
}

function validerAngangInput() {
    const avg1 = $("#avg1").val();
    const avg2 = $("#avg2").val();
    const avg3 = $("#avg3").val();
    const avg4 = $("#avg4").val();
    const avg5 = $("#avg5").val();
    const avg6 = $("#avg6").val();
    const regexp = /^[a-zæøåA-ZÆØÅ.\- ]{2,20}$/;
    const ok = regexp.test(avg1);
    const ok1 = regexp.test(avg2);
    const ok2 = regexp.test(avg3);
    const ok3 = regexp.test(avg4);
    const ok4 = regexp.test(avg5);
    const ok5 = regexp.test(avg6);
    if (!ok) {
        $("#message2").css('color', 'red');
        $("#message2").html("Ugjyldig value!");
        return false;
    }

    else {
        $("#message2").html("");
    }
    if (!ok1) {
        $("#message3").css('color', 'red');
        $("#message3").html("Ugjyldig value!");
        return false;
    }

    else {
        $("#message3").html("");
    }
    if (!ok2) {
        $("#message4").css('color', 'red');
        $("#message4").html("Ugjyldig value!");
        return false;
    }
    else {
        $("#message4").html("");
    }
    if (!ok3) {
        $("#message5").css('color', 'red');
        $("#message5").html("Ugjyldig value!");
        return false;
    }
    else {
        $("#message5").html("");
    }
    if (!ok4) {
        $("#message6").css('color', 'red');
        $("#message6").html("Ugjyldig value!");
        return false;
    }
    else {
        $("#message6").html("");
    }
    if (!ok5) {
        $("#message7").css('color', 'red');
        $("#message7").html("Ugjyldig value!");
        return false;
    }
    else {
        $("#message7").html("");
    }

    return true;
   
}

function validerTidInput() {

    var godkjent = false;
    const avg1 = $("#settid1").val();
    const avg2 = $("#settid2").val();
    const avg3 = $("#settid3").val();
    const avg4 = $("#settid4").val();
    const avg5 = $("#settid5").val();
    const avg6 = $("#settid6").val();
    const regexp = /^([0-9]{2})\:([0-9]{2})$/;
    const ok = regexp.test(avg1);
    const ok1 = regexp.test(avg2);
    const ok2 = regexp.test(avg3);
    const ok3 = regexp.test(avg4);
    const ok4 = regexp.test(avg5);
    const ok5 = regexp.test(avg6);
    if (!ok) {
        $("#message8").css('color', 'red');
        $("#message8").html("Ugjyldig value!");
        godkjent = false;
    }

    else {
        $("#message8").html("");
        godkjent = true;
    }
    if (!ok1) {
        $("#message9").css('color', 'red');
        $("#message9").html("Ugjyldig value!");
        godkjent = false;
    }

    else {
        $("#message9").html("");
        godkjent = true;
    }

    if (!ok2) {
        $("#message10").css('color', 'red');
        $("#message10").html("Ugjyldig value!");
        godkjent = false;
    }
    else {
        $("#message10").html("");
        godkjent = true;
    }
    if (!ok3) {
        $("#message11").css('color', 'red');
        $("#message11").html("Ugjyldig value!");
        godkjent = false;
    }
    else {
        $("#message11").html("");
        godkjent = true;
    }
    if (!ok4) {
        $("#message12").css('color', 'red');
        $("#message12").html("Ugjyldig value!");
        godkjent = false;
    }
    else {
        $("#message12").html("");
        godkjent = true;
    }
    if (!ok5) {
        $("#message13").css('color', 'red');
        $("#message13").html("Ugjyldig value!");
        godkjent = false;
    }
    else {
        $("#message13").html("");
        godkjent = true;
    }

    return godkjent;

}