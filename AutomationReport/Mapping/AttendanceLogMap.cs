namespace AutomationReport.Mapping;
public class AttendanceLogMap
{
    public DateOnly DateFiled { get; set; }

    public string? FullName { get; set; }

    public string? COAType { get; set; }

    public DateOnly DateFrom { get; set; }

    public DateOnly DateTo { get; set; }

    public string Reason { get; set; }

    public string Status { get; set; }

    public DateTime DateApproved { get; set; }

    public string DateRejected { get; set; }

    public string RejectReason { get; set; }
}
