export interface RequestTypeBase{
    requestTypeId:string,
    name:string,
    maxDays:string
}
export function instanceOfRequestType(object:any):object is RequestTypeBase{
    return 'name' in object
}