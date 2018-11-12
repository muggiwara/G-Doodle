# Devoleppement environment

## Node.js

Install [NodeJs](https://nodejs.org/dist/v8.12.0/node-v8.12.0-x64.msi).

## Angular-CLI
Install [Angular-CLI](https://cli.angular.io/).

## Visual Studio Code

* Install [Vscode](https://code.visualstudio.com/docs/?dv=win).
* Install [Tfs](https://download.microsoft.com/download/8/8/3/883bd5d2-00d6-49c5-8e99-7ac34c0a5a71/tfsserver2018.exe).
  * `/!\` Setting the installation with EN language.

## Vscode Extensions
You can install from Extension manager (Ctrl + Shift + X) or from urls.
`/!\` don't forget to reload vscode to handle all new extension.

* [Angular Productivity Pack](ttps://marketplace.visualstudio.com/items?itemName=devboosts.angular-productivity-pack).
  * You can remove GitLens and VS Code LiveShare.
* [Bootstrap 3 Snippets](https://marketplace.visualstudio.com/items?itemName=wcwhitehead.bootstrap-3-snippets).
* [Debugger for Chrome](https://marketplace.visualstudio.com/items?itemName=msjsdiag.debugger-for-chrome).
* [Visual Studio Team Services](ttps://marketplace.visualstudio.com/items?itemName=ms-vsts.team).
  * please follow the instructions in this [link](http://www.sharepointpals.com/post/Step-by-Step-Procedures-to-Integrate-Visual-Studio-Team-Services-(VSTS)-with-Visual-Studio-Code-(VSCode)-for-SharePoint-Framework-(SPFx)-Projects) for the installation and configuration.
    * `/!\` Skip the step 10.
  * Dependance: [vs_team Explorer](https://download.microsoft.com/download/8/8/3/883bd5d2-00d6-49c5-8e99-7ac34c0a5a71/tfsserver2018.exe`)

# Solution

## Restore npm packages
In terminal nav, open "Run task..." and launch "install node pkg".

## Run server or build
As same as restoration, open "Run task..." and launch "ng serve/build".
Now you can go to the angular SPA from this url `http://localhost:4200/`.

## Debug project
Launch with F5 or by the debugger window (Crtl + Shift + D).

# INotWeb

This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 6.2.3.

## Development server
Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The app will automatically reload if you change any of the source files.

## Code scaffolding
Run `ng generate component component-name` to generate a new component. You can also use `ng generate directive|pipe|service|class|guard|interface|enum|module`.

## Build
Run `ng build` to build the project. The build artifacts will be stored in the `dist/` directory. Use the `--prod` flag for a production build.

## Running unit tests
Run `ng test` to execute the unit tests via [Karma](https://karma-runner.github.io).

## Running end-to-end tests
Run `ng e2e` to execute the end-to-end tests via [Protractor](http://www.protractortest.org/).

## Further help
To get more help on the Angular CLI use `ng help` or go check out the [Angular CLI README](https://github.com/angular/angular-cli/blob/master/README.md).

# Best Pratice

## File declaration
* All file name in lowerCase. Example: `person.ts`.
* Every Folder is a module (right click `Angular: Generate a module`) name by the DTO object or by the DOM element which should be represent.
* Every file declaration of `module/service/interface/component/directive/schematics`.
  * Should have `.declaration.ts`. Example: The service Person file should be `person.service.ts`.
* Every folder component of the module should be named by their functionality. Example: The component person edition should be named `edit`.
* Every file componenet of the module should be named by their DTO object and functionality. Example: The component person edition should be named `person-edit.component.ts`.

## Object declaration
* All class/interface/service/component/directive/schematics declaration should be declared in `camelCase`.
* All public variable should be declared in `PascalCase`.
* All private variable should be declared with `underscore` before.

## Angular Modules
* Please refer to the `dependencies` declaration from package.json

# Documentation

## [Angular](https://angular.io/docs) `documentation`

## [TypeScript](https://www.typescriptlang.org/docs/home.htm) `documentation`

## Plugins Angular

### [ng-logger](https://github.com/noemi-salaun/ng-logger) `documentation`
* Logger service

### [signalr](https://github.com/aspnet/SignalR) `documentation`
* client side signalR service

### [ngx-store](https://github.com/zoomsphere/ngx-store) `documentation`
* local/session/webStorage

### [ng2-dnd](https://github.com/akserg/ng2-dnd) `documentation`
* Drag and drop

### [angular2-indexeddb](https://github.com/gilf/angular2-indexeddb) `documentation`
* Local Db

### [ngx-translator](https://github.com/ngx-translate/core) `documentation`
* add internationalisation on SPA

### [ngx-bootstrap](https://valor-software.com/ngx-bootstrap/#/getting-started) `documentation`
* add bootstrap template

### [ngx-order-pipe](https://github.com/VadimDez/ngx-order-pipe) `documentation`
* add pipe orderby

### [ngx-contextmenu](https://github.com/isaacplmann/ngx-contextmenu) `documentation`
* add context menu
