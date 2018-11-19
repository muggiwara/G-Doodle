import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-channel',
  templateUrl: './channel.component.html',
  styleUrls: ['./channel.component.css']
})
export class ChannelComponent implements OnInit {

  @Input() show = true;
  UsersCount = 0;

  constructor() { }

  ngOnInit() {
  }

  edit(isCreated = true) {

  }

}
