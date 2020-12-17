$(function () {
    //Hent alle stasjoner ved document redy for første select. (Stasjonerfra)
    hentAlleStasjoner();
 
});

let stasjonerTilId;
let stasjonerId;
let pris;
let globAvgangTur;
let globAvgangRetur;

//Bekrefter kjøp med validering
function bekreftKjop() {
    
        validerCvc();
        validerKortnr();
        validerEpost();
        validerTlfnr();

    //validering
    if (validerCvc() && validerEpost() && validerKortnr() && validerTlfnr()) {

        //retur verdier
        let stasjonfra = $("#stasjoner").val();
        let tidtil = globAvgangRetur;
        let datotil = $("#returDato").val();
        let tidTilString = datotil + " " + tidtil;
        //tur verdier
        let stasjontil = $("#stasjonertil").val();
        let tidfra = globAvgangTur;
        let datofra = $("#turDato").val();
        let tidFraString = datofra + " " + tidfra;
        //Betalingsinformasjon
        let epost = $("#epost").val();
        let tlfNr = $("#tlfnr").val();
        let kortnummer = $("#kortnr").val();
        let cvc = $("#cvc").val();
        //url
        let url = "billett/kjopBillett";
        //fjerner buss ikon fra streng
        stasjonfra = stasjonfra.substring(1);
        stasjontil = stasjontil.substring(1);

        //Api kall for tur og retur
        if (document.getElementById("timetil").style.display === "block") {
            const billettRetur = {
                avgang: stasjontil,
                destinasjon: stasjonfra,
                tid: tidTilString,
                pris: pris,
                nummer: tlfNr,
                epost: epost,
                kortnummer: kortnummer,
                cvc: cvc
            }
            const billettTur = {
                avgang: stasjonfra,
                destinasjon: stasjontil,
                tid: tidFraString,
                pris: pris,
                nummer: tlfNr,
                epost: epost,
                kortnummer: kortnummer,
                cvc: cvc
            }
            //2 POST en for tur en for retur
            $.post(url, billettTur, function (OK) {
                if (OK) {
                    sessionStorage.setItem("billettTurId", OK);
                    // Hvis POST for tur er ok, kjør POST for retur.
                    $.post(url, billettRetur, function (OK) {
                        if (OK) {
                            let idag = new Date();
                            let dato = idag.getFullYear() + '-' + (idag.getMonth() + 1) + '-' + idag.getDate();
                            let tid = idag.getHours() + ":" + idag.getMinutes() + ":" + idag.getSeconds();
                            let datoOgTid = dato + ' ' + tid;
                            sessionStorage.setItem("pris", pris);
                            sessionStorage.setItem("tid", datoOgTid);
                            sessionStorage.setItem("billettReturId", OK);
                            window.location.href = "kvittering.html";
                        }
                        else {
                            console.log("Feil i db for retur - prøv igjen senere");
                        }
                    });
                }
                else {
                    console.log("Feil i db for tur - prøv igjen senere");
                }
            });
        }
        //Api kall for bare tur 
        else {
            const billettTur = {
                avgang: stasjonfra,
                destinasjon: stasjontil,
                tid: tidFraString,
                pris: pris,
                nummer: tlfNr,
                epost: epost,
                kortnummer: kortnummer,
                cvc: cvc
            }
            // POST for tur
            $.post(url, billettTur, function (OK) {
                if (OK) {
                    let idag = new Date();
                    let dato = idag.getFullYear() + '-' + (idag.getMonth() + 1) + '-' + idag.getDate();
                    let tid = idag.getHours() + ":" + idag.getMinutes() + ":" + idag.getSeconds();
                    let datoOgTid = dato + ' ' + tid;
                    sessionStorage.setItem("tid", datoOgTid);
                    sessionStorage.setItem("pris", pris);
                    sessionStorage.setItem("billettTurId", OK);
                    sessionStorage.removeItem("billettReturId");
                    window.location.href = "kvittering.html";
                }
                else {
                    $("#feil").html("Feil i db for tur - prøv igjen senere");
                }
            });
        }
       
    }
    
}





function visAvgangerTider() {
    //retur verdier
    let stasjonfra = $("#stasjoner").val();
    let tidtil = $("#returTid").val();
    let datotil = $("#returDato").val();
    //tur verdier
    let stasjontil = $("#stasjonertil").val();
    let tidfra = $("#startTid").val();
    let datofra = $("#turDato").val();
    // checkbox for billetttupe
    let barn = $("#Barn");
    let voksen = $("#voksen");
    let honnor = $("#Honor");
    let student = $("#Student");

    //Validering av valg-skjema
    if ((stasjonfra === "") || (stasjontil === "") || (tidfra === "") || (datofra === "") || (!barn.is(':checked')) &&
        (!voksen.is(':checked')) && (!honnor.is(':checked')) && (!student.is(':checked'))) {
        //kall på valideringsfunksjoner
        validateCheckBox(barn, voksen, student, honnor);
        validateColor($("#turDato"));
        validateColor($("#startTid"));
        $("#feilmelding").css('color', 'red');
        $("#feilmelding").html("Du må fylle inn alle feltene");

        // Sjekker hvis det finnes retur
        if (document.getElementById("timetil").style.display === "block") {
            //Valider retur
            if (tidtil === "" || datotil === "") {
                validateColor($("#returTid"));
                validateColor($("#returDato"));
                $("#feilmelding").css('color', 'red');
                $("#feilmelding").html("Du må fylle inn alle feltene");
            }
            validateColor($("#returTid"));
            validateColor($("#returDato"));
        }
    }
    else {
        validateColor($("#turDato"));
        validateColor($("#startTid"));

        if (document.getElementById("timetil").style.display === "block") {
            //Validerer retur uten hensyn til tur
            if (tidtil === "" || datotil === "") {
                validateColor($("#returTid"));
                validateColor($("#returDato"));
                $("#feilmelding").css('color', 'red');
                $("#feilmelding").html("Du må fylle inn alle feltene");
            }
            else {
                //Valider riktig og tøm feilmelding
                validateCheckBox(barn, voksen, student, honnor);
                validateColor($("#turDato"));
                validateColor($("#startTid"));
                validateColor($("#returTid"));
                validateColor($("#returDato"));
                document.getElementById("skjema").style.display = "none";
                document.getElementById("seAvgangstider").style.display = "block";
                visAvganger();
                $("#feilmelding").html("");
            }

        }
        else {
            //Valider riktig og tøm feilmelding
            validateCheckBox(barn, voksen, student, honnor);
            validateColor($("#turDato"));
            validateColor($("#startTid"));
            validateColor($("#returTid"));
            validateColor($("#returDato"));
            document.getElementById("skjema").style.display = "none";
            document.getElementById("seAvgangstider").style.display = "block";
            visAvganger();
            $("#feilmelding").html("");
        }
    }  
}



function visAvganger() {


    // Tur verider
    let stasjonfra = $("#stasjoner").val();
    let stasjontil = $("#stasjonertil").val();
    let datofra = $("#turDato").val();
    let tidFraString = datofra + " " + globAvgangTur;
    let ut = "";
    // checkbox verider
    let barn = $("#Barn");
    let voksen = $("#voksen");
    let honnor = $("#Honor");
    let student = $("#Student");
    let antVoksen = parseInt($("#number1").val(),10);
    let antBarn = parseInt($("#number2").val(),10);
    let antHonnor = parseInt($("#number3").val(),10);
    let antStudent = parseInt( $("#number4").val(),10);


    let val = $('#stasjoner').val();
    //Starter pris teller
    let idHent = $('#stasjoner option').filter(function () {
        return this.value == val;
    }).data('value');



    $.ajax({
        type: "POST",
        url: "billett/hentPris?id=" +
            encodeURIComponent(idHent),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            let prisen = parseInt(result, 10);
            pris = prisen;



            if (voksen.is(':checked')) {
                ut += "Voksen: " + antVoksen + "   ";
                //øker pris teller 
                pris += (100 * antVoksen);
            }
            if (barn.is(":checked")) {
                ut += "Barn: " + antBarn + "   ";
                //øker pris teller 
                pris += (30 * antBarn);
            }
            if (honnor.is(":checked")) {
                ut += "Honnør: " + antHonnor + "   ";
                //øker pris teller
                pris += (70 * antHonnor);
            }
            if (student.is(":checked")) {
                ut += "Student: " + antStudent + "   ";
                //øker pris teller 
                pris += (50 * antStudent);
            }

            //Sjekker turverdier
            if (stasjonfra != null && globAvgangTur != "" && datofra != null) {
                stasjonfra = stasjonfra.substring(1);
                stasjontil = stasjontil.substring(1);
                let turpris = pris;

                $("#stasjonfra").html(stasjonfra);
                $("#tidfra").html(tidFraString);
                $("#typefra").html(ut);
                $("#stasjonfraDest").html(stasjontil);
                $("#totalpris").html(turpris + "kr");

            }

            //Sjekker returverdier
            if (document.getElementById("timetil").style.display === "block") {
                document.getElementById("tableRetur").style.display = "block";
                let stasjonfra = $("#stasjoner").val();
                let stasjontil = $("#stasjonertil").val();
                let datotil = $("#returDato").val();
                let tidTilString = datotil + " " + globAvgangRetur;
                // Fjerner bus ikon fra stasjoner
                stasjontil = stasjontil.substring(1);
                stasjonfra = stasjonfra.substring(1);
                let returpris = pris * 2;

                $("#stasjontil").html(stasjontil);
                $("#tidtil").html(tidTilString);
                $("#typetil").html(ut);
                $("#stasjontilDest").html(stasjonfra);
                $("#totalpris").html(returpris + "kr");


            }
            else {
                //Hvis bare tur, fjern retur
                document.getElementById("tableRetur").style.display = "none";

            }










        },
        failure: function (response) {
            alert(response.d);
        }
    });



 
}


function hentAvgangRetur() {
    //Henter stasjontil id
    let id = stasjonerTilId;
    id = parseInt(id, 10);

    //Henter stasjontil sin tilgjengelig avgangstider
    $.ajax({
        type: "POST",
        url: "billett/sjekkTidRetur?id=" +
            encodeURIComponent(id),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (avganger) {

            let formaterTid = avganger.hours + ":" + avganger.minutes+"0";
            globAvgangRetur = formaterTid;
            JSON.stringify(avganger);
            let startTid = $("#returTid").val();
            let hours = startTid.split(":")[0];
            let minutes = startTid.split(":")[1];


            //Sjekker nærmeste avgangstid for valgt tid 
            if (!(minutes == avganger.minutes && hours == avganger.hours)) {
                document.getElementById("returTid").setCustomValidity('Nærmeste avgang er kl ' + avganger.hours + ":" + avganger.minutes+"0");
                document.getElementById("returTid").reportValidity();
            }
            else {
                document.getElementById("returTid").setCustomValidity('');
            }
            return avganger;
        },
        failure: function (response) {
            // feilhåndtering ajax kall
            alert(response.d);
        }
    });
}



function hentAvgangTur() {

    //Henter stasjonfra id
    let id = stasjonerId;
    id = parseInt(id, 10);

    //Henter stasjonfra sin tilgjengelig avgangstider
    $.ajax({
        type: "POST",
        url: "billett/sjekkTidTur?id=" +
            encodeURIComponent(id),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (avganger) {


            let formaterTid = avganger.hours + ":" + avganger.minutes+"0";
            globAvgangTur = formaterTid;
            JSON.stringify(avganger);
 
            let startTid = $("#startTid").val();
            let hours = startTid.split(":")[0];
            let minutes = startTid.split(":")[1];

                    //Sjekker nærmeste avgangstid for valgt tid 
                    if (!(minutes == avganger.minutes && hours == avganger.hours)) {
                        document.getElementById("startTid").setCustomValidity('Nærmeste avgang er kl ' + avganger.hours + ":" + avganger.minutes+"0");
                        document.getElementById("startTid").reportValidity();
                    }
                    else {
                        document.getElementById("startTid").setCustomValidity('');
                    }
            return avganger;
        },
        failure: function (response) {
            // feilhåndtering ajax kall
            alert(response.d);
        }
    });
}



//Henter alle stasjoner for første select
function hentAlleStasjoner() {
       //Get for stasjonerfra
        $.get("billett/hentAlleStasjonerFra", function (stasjoner) {
            formaterStasjoner(stasjoner);
        });
}


//Henter alle stasjoner for andre select
function hentAlleStasjonerTil(id) {
        //Kall på ajax for stasjoner til
        $.ajax({
            type: "POST",
            url: "billett/hentAlleStasjonerTil?id=" +
                encodeURIComponent(id),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                formaterStasjonerTil(result);
            },
            failure: function (response) {
                alert(response.d);
            }
        });
}

//Formaterer stasjonertil med ikon
function formaterStasjonerTil(Stasjoner) {
    let ut = "";
    for (let stasjon of Stasjoner) {
        ut += "<option class='fa-bus' style='font-size:25px' data-value=" + stasjon.stasjonId+" >" + " &#xf207; " + stasjon.stasjonNavn + "</option>"
    }
    $("#stasjonertil").html(ut);
}

//Formaterer stasjonerfra med ikon
function formaterStasjoner(Stasjoner) {
    let ut = "";
    for (let stasjon of Stasjoner) {
        ut += "<option class='fa-bus' style='font-size:25px' data-value=" + stasjon.stasjonId + " >" + " &#xf207; " + stasjon.stasjonNavn + "</option>"
    } 
    $("#stasjoner").html(ut);
}



//tilbakeknapp
function tilbake() {
    document.getElementById("skjema").style.display = "block";
    document.getElementById("seAvgangstider").style.display = "none";
}
//viser viser dato og tid input ved klikk på tur eller retur
function vis2() {
    let input2 = document.getElementById("nummer2");
     input2.style.display = "block";

    let val = $('#stasjoner').val()
    let id = $('#stasjoner option').filter(function () {
        return this.value == val;
    }).data('value');

    let integer2 = parseInt(id, 10);
    hentAlleStasjonerTil(integer2);
    stasjonerId = integer2;
    hentAvgangTur();
}
//viser viser dato og tid input ved klikk på tur eller retur
function vis3() {
    let input3 = document.getElementById("stasjonertil");
    if (input3.checkValidity()) {
        btn.style.display = "block";
    }
    else {
        btn.style.display = "none";
    }

    let val = $('#stasjonertil').val()
    let id = $('#stasjonertil option').filter(function () {
        return this.value == val;
    }).data('value');
    let integer2 = parseInt(id, 10);
    stasjonerTilId = integer2;
    hentAvgangRetur();
}
//viser viser dato og tid input ved klikk på tur
function tur() {
    let time = document.getElementById("time");
    let timetil = document.getElementById("timetil");
    time.style.display = "block";
    timetil.style.display = "none";

}
//viser viser dato og tid input ved klikk på retur
function retur() {
        let time = document.getElementById("time");
        let timetil = document.getElementById("timetil");
        timetil.style.display = "block";
        time.style.display = "block";
}
//viser checkbox billetttype
function valg2() {
    let input5 = document.getElementById("valg");
    input5.style.display = "block";
    let visAvgangKnapp = document.getElementById("btnAvgang");
    visAvgangKnapp.style.display = "block";
}
//viser antall billetter ved klikk på voksen
function vokse2() {
    let checkBox = document.getElementById("voksen");
    let number = document.getElementById("number1");
    
    if (checkBox.checked == true) {
        number.style.display = "block";

    } else {
        number.style.display = "none";
    }
}
//viser antall billetter ved klikk på barn
function Barn2() {
    let checkBox = document.getElementById("Barn");
    let number = document.getElementById("number2");
    if (checkBox.checked == true) {
        number.style.display = "block";
    } else {
        number.style.display = "none";
    }
}
//viser antall billetter ved klikk på honnør
function honor2() {
    let checkBox = document.getElementById("Honor");
    let number = document.getElementById("number3");
    if (checkBox.checked == true) {
        number.style.display = "block";
    } else {
        number.style.display = "none";
    }
}
//viser antall billetter ved klikk på student
function student2() {
    let checkBox = document.getElementById("Student");
    let number = document.getElementById("number4");
    if (checkBox.checked == true) {
        number.style.display = "block";
    } else {
        number.style.display = "none";
    }
}



//<--------------------------------------------              VALIDERINGS FUNKSJONER            ------------------------------------------------------------------->



function validateDato() {
    let turdato = $("#turDato").val();
    let returdato = $("#returDato");
    returdato.prop('min', turdato);
    returdato.prop("disabled", false);
}

function validateColor(doc) {
    if (doc.val() === "") {
        doc.addClass("border border-danger");
    }
    else {
        doc.removeClass("border border-danger");
    }
}

function validateCheckBox(doc1, doc2, doc3, doc4) {
    if ((!doc1.is(':checked')) && (!doc2.is(':checked')) && (!doc3.is(':checked')) && (!doc4.is(':checked'))) {
        doc1.addClass("checkboxRed");
        doc2.addClass("checkboxRed");
        doc3.addClass("checkboxRed");
        doc4.addClass("checkboxRed");
    }
    else {
        doc1.removeClass("checkboxRed");
        doc2.removeClass("checkboxRed");
        doc3.removeClass("checkboxRed");
        doc4.removeClass("checkboxRed");
    }

}

function validerEpost() {
    const epost = $("#epost").val();
    const regexp = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
    const ok = regexp.test(epost);
    if (!ok) {
        $("#message0").css('color', 'red');
        $("#message0").html("Ugjyldig e-post!");
        return false;
    }
    else {
        $("#message0").html("");
        return true;
    }
}

function validerKortnr() {
    const kortnr = $("#kortnr").val();
    const regexp = /^4[0-9]{12}(?:[0-9]{3})?$/;
    const ok = regexp.test(kortnr);
    if (!ok) {
        $("#message1").css('color', 'red');
        $("#message1").html("Ugjyldig kortnummer, må være 16 tall!");
        return false;
    }
    else {
        $("#message1").html("");
        return true;
    }
}

function validerTlfnr() {
    const tlfnr = $("#tlfnr").val();
    const regexp = /^[0-9]{8}$/;
    const ok = regexp.test(tlfnr);
    if (!ok) {
        $("#message2").css('color', 'red');
        $("#message2").html("Ugjyldig telefonnummer!");
        return false;
    }
    else {
        $("#message2").html("");
        return true;
    }
}

    function validerCvc() {
        const cvc = $("#cvc").val();
        const regexp = /^[0-9]{3}$/;
        const ok = regexp.test(cvc);
        if (!ok) {
            $("#message3").css('color', 'red');
            $("#message3").html("Ugjyldig cvc!");
            return false;
           
        }
        else {
            $("#message3").html("");
            return true;
        }
   
}