import { Component, OnInit } from '@angular/core';
import { UserService } from '../user.service';
import { SessionStorageService } from 'ngx-store';
import { UserSignalRService } from '../user-signal-r.service';
import { User } from '../user';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {
  users: User[];
  private currentUser: User;

  constructor(private _userService: UserService,
    private _sessionService: SessionStorageService,
    private _userSignalRService: UserSignalRService) { }

  ngOnInit() {
    this._userSignalRService.start().then(() => {
      this._userSignalRService.onConnected((res: boolean) => {
        this.onUpdated(res);
      });
      this._userSignalRService.onUpdated((res: boolean) => {
        this.onUpdated(res);
      });
    });
  }

  private onUpdated(result: boolean) {
    if (result) {
      this.currentUser = this._sessionService.get('user');
      this._userService.getAll().subscribe((users: User[]) => {
        const filter = users.filter(u => u.id !== this.currentUser.id);
        this.users = filter;
      },
      err => {
        console.log(err);
      });
    }
  }
}
