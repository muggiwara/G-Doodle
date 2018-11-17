import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { UserEditComponent } from './user/edit/user-edit.component';
import { UserListComponent } from './user/list/user-list.component';
import { UserService } from './user/user.service';

@NgModule({
  declarations: [
    AppComponent,
    UserEditComponent,
    UserListComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
  ],
  providers: [UserService],
  bootstrap: [AppComponent]
})
export class AppModule { }
