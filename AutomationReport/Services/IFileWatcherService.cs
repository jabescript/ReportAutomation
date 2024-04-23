using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationReport.Services;
public interface IFileWatcherService
{
    void RunFileWatcher();
    FileInfo FindExcelFile();

}
