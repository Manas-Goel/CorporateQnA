import { Component, OnInit } from '@angular/core';
import { faComments } from '@fortawesome/free-regular-svg-icons';

@Component({
  selector: 'app-side-nav',
  templateUrl: './side-nav.component.html',
})
export class SideNavComponent implements OnInit {
  faComments = faComments;
  constructor() {}

  ngOnInit(): void {}
}
