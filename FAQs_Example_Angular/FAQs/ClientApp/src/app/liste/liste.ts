import { Component, OnInit } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Modal } from '../modal';
import { Questions } from "../Questions";
import { faCaretDown, faCaretUp, faReply, faQuestion, faComments, faArrowUp, faShare } from '@fortawesome/free-solid-svg-icons';
import { Ratings } from "../Ratings";

@Component({
  templateUrl: "liste.html"
})
export class Liste {
  up = faCaretUp;
  down = faCaretDown;
  faVoteYea = faArrowUp;
  faQuestion = faQuestion;
  faComment = faComments;
  faShare = faShare;
  reply = faReply;
  allQuestions: Questions[];
  laster: boolean;
  kundeTilSletting: string;
  slettingOK: boolean;
  sporsmalText: string;

  constructor(private http: HttpClient, private router: Router, private modalService: NgbModal) { }

  ngOnInit() {
    this.laster = true;
    this.fetchQuestions();
  }

  fetchQuestions() {
    this.http.get<Questions[]>("api/faq/")
      .subscribe(questions => {
        this.allQuestions = questions;
        this.laster = false;
        console.dir(this.allQuestions);
      },
       error => console.log(error)
      );
  };

  stillSporsmaal() {
    const modalRef = this.modalService.open(Modal, {
      backdrop: 'static',
      keyboard: false,
      size: 'lg'
    });
    modalRef.componentInstance.sporsmalText = this.sporsmalText;
    modalRef.result.then(retur => {
      if (retur !== "Lukk") {
        const question = new Questions();
        question.question = retur;
        this.http.post("api/faq/", question)
          .subscribe(
            retur => {
              if (retur) {
                alert("Du har sendt inn et spørsmål ");
                this.allQuestions = [...this.allQuestions];
                this.ngOnInit();
              }
            },
            error => console.log(error)
          );


      }
      else {

      }
    });
  }

  oppdaterListe() {
    this.allQuestions = this.allQuestions.sort((a, b) => b.rating.rating - a.rating.rating);
  }
  oppdaterRating(id: number, endretRating: number, indexOfelement: number) {



    let enRating = new Ratings();
    enRating.id = id;
    enRating.rating = endretRating;


    this.http.put("api/faq/", enRating)
      .subscribe(
        retur => {
          this.allQuestions[indexOfelement].rating.rating = enRating.rating;
          console.log(this.allQuestions[indexOfelement]);
          this.allQuestions = [...this.allQuestions];
          this.oppdaterListe();

          //Oppdaterer nye verdien i listen
          console.log(retur);
        },
        error => console.log(error)
      );

  }

}
