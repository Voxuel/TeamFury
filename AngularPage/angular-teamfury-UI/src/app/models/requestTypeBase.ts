export interface RequestTypeBase{
    requestTypeId:string,
    daysLeft:string,
    leaveType:string
}
export function instanceOfRequestType(object:any):object is RequestTypeBase{
    return 'daysLeft' in object
}