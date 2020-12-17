import { Component } from '@angular/core';

@Component({
  templateUrl: './hjem.html'
})
export class Hjem {
  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
