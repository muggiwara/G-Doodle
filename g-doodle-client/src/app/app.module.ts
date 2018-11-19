import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { WebStorageModule } from 'ngx-store';
import {
  BsDropdownModule,
  ModalModule,
  CollapseModule,
  AlertModule,
  AccordionModule
} from 'ngx-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { UserEditComponent } from './user/edit/user-edit.component';
import { UserListComponent } from './user/list/user-list.component';
import { UserService } from './user/user.service';
import { UserLoginComponent } from './user/login/user-login.component';
import { UserComponent } from './user/user.component';
import { EnumToArrayPipe } from './enum-to-array.pipe';
import { AppHttpInterceptor } from './app-http.interceptor';
import { UserSignalRService } from './user/user-signal-r.service';
import { ChannelComponent } from './channel/channel.component';
import { ClickOutsideDirective } from './click-outside.directive';

@NgModule({
  declarations: [
    AppComponent,
    UserEditComponent,
    UserListComponent,
    UserComponent,
    EnumToArrayPipe,
    ClickOutsideDirective,
    UserLoginComponent,
    ChannelComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    WebStorageModule,
    BsDropdownModule.forRoot(),
    CollapseModule.forRoot(),
    ModalModule.forRoot(),
    AlertModule.forRoot(),
    AccordionModule.forRoot(),
  ],
  providers: [
    UserService,
    UserSignalRService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AppHttpInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent],
  entryComponents: [UserEditComponent, UserLoginComponent]
})
export class AppModule { }
