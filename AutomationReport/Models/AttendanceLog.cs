using System.Globalization;

namespace AutomationReport.Models;
public class AttendanceLog
{
    public int BiometricID { get; set; }
    public string FullName { get; set; }
    public DateTime LogTime { get; set; }
    public string InOutMode { get; set; }
    public string Location { get; set; }
    public string? Notes { get; set; }
}
