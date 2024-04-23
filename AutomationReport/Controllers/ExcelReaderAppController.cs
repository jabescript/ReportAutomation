using AutomationReport.Models;
using AutomationReport.Services;

namespace AutomationReport.Controllers;
public class ExcelReaderAppController : IReaderAppController
{
    private readonly IFileWatcherService _fileWatcherService;
    private readonly IExcelReaderService _excelReaderService;

    public ExcelReaderAppController(IFileWatcherService fileWatcherService, IExcelReaderService excelReaderService)
    {
        _fileWatcherService = fileWatcherService;
        _excelReaderService = excelReaderService;
    }

    public async Task RunProgramAsync()
    {
        var excelFile = await ReadFromFolderAsync();

        var logsFromExcel = await LoadLogsFromExcelAsync(excelFile);

        Console.WriteLine(logsFromExcel);
    }

    private async Task<FileInfo> ReadFromFolderAsync()
    {
        Console.WriteLine("Reading from the excel file...");
        var excelFile = _fileWatcherService.FindExcelFile();
        Console.WriteLine($"Found {excelFile}");
        return excelFile;
    }

    private async Task<List<AttendanceLog>> LoadLogsFromExcelAsync(FileInfo fileInfo)
    {
        var logsFromExcel = await _excelReaderService.LoadLogsFromExcelAsync(fileInfo);
        return logsFromExcel;
    }

}
