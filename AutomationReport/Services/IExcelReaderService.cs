using AutomationReport.Mapping;
using AutomationReport.Models;
using OfficeOpenXml;

namespace AutomationReport.Services;
public interface IExcelReaderService
{
    Task<List<AttendanceLog>> LoadLogsFromExcelAsync(FileInfo fileInfo);

    List<AutoPushLog> LoadAutoPushLogsFromExcelAsync(string filePath);
}
