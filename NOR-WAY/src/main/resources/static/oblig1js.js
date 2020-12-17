const filmregister = [];


function validate() {

    const Antall = document.getElementById("antall");
    const Fornavn = document.getElementById("fornavn");
    const Etternavn = document.getElementById("etternavn");
    const Telefonnr = document.getElementById("tlf");
    const Epost = document.getElementById("epost");


    const utAntall = document.getElementById("antallValidering");
    const utFornavn = document.getElementById("fornavnValidering");
    const utEtternavn = document.getElementById("etternavnValidering");
    const utTelefonnr = document.getElementById("tlfValidering");
    const utEpost = document.getElementById("epostValidering");


    let vNavn = /^[A-ZÆØÅa-zæøå]*$/;
    let vNummer = /^[0-9]{8}$/;
    let vAntall = /^[0-9]{1,2}$/;
    let vEpost = /^[A-ZÆØÅa-zæøå.-_]+[@]{1}.[A-ZÆØÅa-zæøå]+[.].[a-zæøå]{0,4}$/;



    Antall.addEventListener("input", function () {
        if (!Antall.value.match(vAntall)) {
            utAntall.innerHTML = "Feil i antall";
        } else {
            utAntall.innerHTML = "";
        }
    });

    Fornavn.addEventListener("input", function () {
        if (!Fornavn.value.match(vNavn)) {
            utFornavn.innerHTML = "Skriv riktig";
        } else {
            utFornavn.innerHTML = "";
        }
    });

    Etternavn.addEventListener("input", function () {
        if (!Etternavn.value.match(vNavn)) {
            utEtternavn.innerHTML = "Skriv riktig";
        } else {
            utEtternavn.innerHTML = "";
        }
    });

    Telefonnr.addEventListener("input", function () {
        if (!Telefonnr.value.match(vNummer)) {
            utTelefonnr.innerHTML = "Skriv riktig";
        } else {
            utTelefonnr.innerHTML = "";
        }
    });

    Epost.addEventListener("input", function () {
        if (!Epost.value.match(vEpost)) {
            utEpost.innerHTML = "skriv riktig";
        } else {
            utEpost.innerHTML = "";
        }
    });

   let ut = utFornavn.innerHTML+utEtternavn.innerHTML+utEpost.innerHTML+utAntall.innerHTML+utTelefonnr.innerHTML;

    if(ut == ""){
        return true;
    }
    else {
        return false;
    }


}


