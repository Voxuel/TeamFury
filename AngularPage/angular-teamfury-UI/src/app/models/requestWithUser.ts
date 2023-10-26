export interface RequestWithUser {
  request: {
    requestID: string,
    startDate: string,
    endDate: string,
    requestSent: string,
    messageForDecline: string,
    requestType: {
      requestTypeID: string,
      name: string,
      maxdays: string,
    },
    statusRequest: string,
    adminName: string,
  },
  userId:string,
  userName:string,
  DaysLeft:{
    requestTypeId:string,
    daysLeft:number,
    leaveType:string
  }
}
