using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationReport.Services;
public class ExcelFileWatcherService : IFileWatcherService
{
    private readonly string _fullPath;

    public ExcelFileWatcherService(IConfiguration config)
    {
        var userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        var folderPath = config.GetSection("FolderPath").Value;
        var fullPath = $"{userProfile}{folderPath}";
        _fullPath = fullPath;
    }

    public FileInfo FindExcelFile()
    {
        try
        {
            RunFileWatcher();
            var excelFiles = Directory.GetFiles(_fullPath, "*.xls*");

            if (excelFiles.Length == 0)
            {
                Console.WriteLine("No excel file found.");
                return null;
            }

            var file = new FileInfo(excelFiles[0]);
            return file;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error " + ex.Message);
            return null;
        }
    }

    public void RunFileWatcher()
    {
        using var watcher = new FileSystemWatcher();

        watcher.Path = _fullPath;

        // Watch for changes in file names.
        watcher.NotifyFilter = NotifyFilters.FileName;

        // Watch both Excel 97-2003 and 2007+ files
        watcher.Filter = "*.xls*";

        // Add event handlers.
        watcher.Created += OnCreated;

        // Begin watching.
        watcher.EnableRaisingEvents = true;
    }

    public void OnCreated(object source, FileSystemEventArgs e)
    {
        Console.WriteLine($"Reading File: {e.FullPath} {e.ChangeType}");

        try
        {
            // Let's wait for the file to be released by any process.
            WaitForFile(e.FullPath);
            using var inputStream = File.Open(e.FullPath, FileMode.Open, FileAccess.Read, FileShare.None);


            // Process the file
            //var logs = _excelReaderService.GetAutoPushLogsFromExcel(e.FullPath);
            //foreach(var log in logs)
            //{
            //    Console.WriteLine($"BiometricID: {log.BiometricID}, Full Name: {log.FullName}, Log Time: {log.LogTime}, In Out Mode: {log.InOutMode}, Location: {log.Location}, Notes: {log.Notes}");
            //}
        }
        catch (IOException)
        {
            //Console.WriteLine($"An IO exception was caught: {ex.Message}");
            Console.WriteLine("File is in use. Retrying in 5 seconds...");
            Thread.Sleep(5000);
        }
    }

    public void WaitForFile(string fullPath)
    {
        bool fileIsReady = false;
        while (!fileIsReady)
        {
            try
            {
                using var inputStream = File.Open(fullPath, FileMode.Open, FileAccess.Read, FileShare.None);
                fileIsReady = inputStream.Length > 0;
            }
            catch (IOException)
            {
                // File is still being copied or used by another process
                Console.WriteLine("File is in use. Retrying in 5 seconds...");
                Thread.Sleep(5000);
            }
        }
    }
}
