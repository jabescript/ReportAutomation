using AutomationReport.Extensions;
using AutomationReport.Models;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;
using System.Collections.Generic;
using System.Data;
using System.Reflection;


namespace AutomationReport.Services;

public class EPPlusReaderService : IExcelReaderService
{

    public async Task<List<AttendanceLog>> LoadLogsFromExcelAsync(FileInfo fileInfo)
    {
        var logs = new List<AttendanceLog>();

        try
        {
            using var package = new ExcelPackage(fileInfo);

            ExcelWorkbook workbook = package.Workbook;
            if (workbook != null)
            {
                ExcelWorksheet worksheet = workbook.Worksheets[0];
                if (worksheet != null)
                {
                    logs = worksheet.ReadExcelToList<AttendanceLog>();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        return logs;
    }

    public List<AutoPushLog> LoadAutoPushLogsFromExcelAsync(string filePath)
    {
        List<AutoPushLog> list = new();
        var fileInfo = new FileInfo(filePath);

        if (File.Exists(filePath))
        {
            try
            {
                using ExcelPackage package = new(fileInfo);
                ExcelWorkbook workbook = package.Workbook;
                if (workbook != null)
                {
                    ExcelWorksheet worksheet = workbook.Worksheets.FirstOrDefault();
                    if (worksheet != null)
                    {
                        list = worksheet.ReadExcelToList<AutoPushLog>();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        return list;
    }
}
