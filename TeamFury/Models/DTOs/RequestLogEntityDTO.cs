using Models.Models;

namespace Models.DTOs;

public class RequestLogEntityDTO
{
    public int RequestId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime RequestSent { get; set; }
    public string MessageForDecline { get; set; }
    public RequestType RequestType { get; set; }
    public StatusRequest StatusRequest { get; set; }
    public string AdminName { get; set; }
}
