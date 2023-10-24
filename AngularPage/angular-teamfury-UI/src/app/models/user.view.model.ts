export interface UserViewModel{
    id:string,
    username: string,
    password: string,
    email: string,
    phoneNumber: string,
    role: string[]
}

export function instanceOfUser(object:any):object is UserViewModel{
    return 'username' in object
}