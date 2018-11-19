import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { API } from '../app.settings';
import { UserStatus } from './user';

@Injectable({
  providedIn: 'root',
})
export class UserSignalRService {
  private CONNECTED = 'USER__CONNECTED';
  private CONNECT = 'Connect';
  private UPDATE = 'Update';
  private UPDATED = 'USER__UPDATED';

  private hub: signalR.HubConnection;
  private isAllReadyStart = false;
  private promise: any;


  constructor() {
    this.hub = new signalR.HubConnectionBuilder()
      .withUrl(`${API}user`)
      .build();
   }

   start() {
     if (!this.isAllReadyStart) {
       this.isAllReadyStart = true;
      this.promise = this.hub.start();
     }
    return this.promise;
  }

  onConnected(method: (result: Boolean) => void) {
    this.hub.on(this.CONNECTED, method);
  }

  onUpdated(method: (result: Boolean) => void) {
    this.hub.on(this.UPDATED, method);
  }

  connect(userId: number, status: UserStatus) {
    this.hub.send(this.CONNECT, userId, status);
  }

  update(userId: number) {
    this.hub.send(this.UPDATE, userId);
  }

  stop() {
    this.hub.stop();
  }
}
