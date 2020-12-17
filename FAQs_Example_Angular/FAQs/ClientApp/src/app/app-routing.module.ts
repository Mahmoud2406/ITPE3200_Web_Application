import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Hjem } from './hjem/hjem';
import { Liste } from './liste/liste';

const appRoots: Routes = [
  { path: 'liste', component: Liste },
  { path: 'hjem', component: Hjem },
  { path: '', redirectTo: 'liste', pathMatch: 'full' }
]

@NgModule({
  imports: [
    RouterModule.forRoot(appRoots)
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }
