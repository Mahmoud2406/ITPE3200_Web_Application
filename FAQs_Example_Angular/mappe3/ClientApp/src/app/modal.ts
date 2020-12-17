import { Component } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { faQuestion } from '@fortawesome/free-solid-svg-icons';

@Component({
  templateUrl: 'modal.html'
})
export class Modal {
  faQuestion = faQuestion;
  constructor(public modal: NgbActiveModal) { }

}
