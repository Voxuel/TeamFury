export class User {
    id:string;
    userName:string;
    normalizedUserName:string;
    email:string;
    passwordHash:string;
    securityStamp:string;
    concurrencyStamp:string;

    constructor(){
        this.id = "";
        this.userName = "";
        this.normalizedUserName = "";
        this.email = "";
        this.passwordHash = "";
        this.securityStamp = "";
        this.concurrencyStamp ="";
    }
}
