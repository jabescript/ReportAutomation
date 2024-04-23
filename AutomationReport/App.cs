using AutomationReport.Models;
using AutomationReport.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AutomationReport;
public class App
{
    private readonly IExcelReaderService _excelDataReader;
    private readonly IFileWatcherService _fileWatcherService;
    private readonly IConfiguration _configuration;

    public App(IExcelReaderService excelDataReader, IFileWatcherService fileWatcherService, IConfiguration configuration)
    {
        _fileWatcherService = fileWatcherService;
        _excelDataReader = excelDataReader;
        _configuration = configuration;
    }


    public void Run(string[] args)
    {
        //System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

        //var userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        //string filePath = $@"{userProfile}\OneDrive - Macquarie Technology Group\Documents\Auto Push Logs 01 to 25 Jan.xls";
        //string folderPath = $@"{userProfile}\OneDrive - Macquarie Technology Group\Documents\Logs";

        //_fileWatcherService.RunFileWatcher(folderPath);

        //var logs = _excelDataReader.GetAutoPushLogsFromExcel(filePath);
        // Wait for the user to quit the program.


    }
}
