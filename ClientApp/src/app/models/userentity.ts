export class UserEntity {
    username: string;
    password: string;

    constructor(usernamevalue: string, passwordvalue: string) {
        this.username = usernamevalue;
        this.password = passwordvalue;
    }
}
