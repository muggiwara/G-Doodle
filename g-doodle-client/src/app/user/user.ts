export class User {
  public id: number;
  public name: string;
  public email: string;
  public status: UserStatus;
  public connectionId: string;
}

export enum UserStatus {
  OFFLINE = 0,
  ONLINE = 1,
  HS = 2,
}
