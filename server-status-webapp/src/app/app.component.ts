import { Component, OnInit } from '@angular/core';
import {ServerStatuses, AppComponentService, ServerStatus} from '../app/app.component.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  public title = 'XYZ Co Dashboard';
  public serverStatuses: any;
  constructor(
    protected appComponentService: AppComponentService,
  ){}

  ngOnInit() {
    this.appComponentService.getServerStatuses()
    .subscribe((response: ServerStatuses) => {
      this.serverStatuses = response;
    })
  }
}
