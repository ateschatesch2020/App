using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.DesignPatterns.CommandPattern
{
    public class CreateExcelTableActionCommand<T> : ITableButtonActionCommand
    {//ConcreteCommand
        private readonly ExcelFile<T> _excelFile;

        public CreateExcelTableActionCommand(ExcelFile<T> excelFile)
        {
            _excelFile = excelFile;
        }
        public IActionResult Execute()
        {
            var bytes = _excelFile.Create(); // artık byte[]
            return new FileContentResult(bytes, _excelFile.FileType)
            {
                FileDownloadName = _excelFile.FileName
            };
        }
    }
}
