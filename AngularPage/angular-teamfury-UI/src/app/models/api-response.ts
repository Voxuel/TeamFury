export interface ApiResponse {
    isSuccess: boolean;
    result: any[];
    statusCode: Int32Array;
    errorMessages: string[];
}
