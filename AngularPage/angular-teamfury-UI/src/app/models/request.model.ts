export interface Request {
  requestId: string,
  startDate: string,
  endDate: string,
  requestSent: string,
  messageForDecline: string,
  requestType:{
    requestTypeID: string,
    name: string,
    maxdays: string
  },
  statusRequest: string,
  adminName: string
}
