using System.Net;

namespace DemoAPI01.Models.DTOs;

public class Status
{
    public HttpStatusCode StatusCode { get; set; }
    public string Message { get; set; }
}