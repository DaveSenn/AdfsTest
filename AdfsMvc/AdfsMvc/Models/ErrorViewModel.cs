namespace AdfsMvc.Models;

public class ErrorViewModel
{
    public String? RequestId { get; set; }

    public Boolean ShowRequestId => !String.IsNullOrEmpty( RequestId );
}