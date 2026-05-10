using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.DesignPatterns.DesignPatterns.Behavioral.CommandPattern
{
    public class CreatePdfTableActionCommand<T> : ITableButtonActionCommand
    {//Concrete command
        private readonly PdfFile<T> _pdfFile;// strategy design pattern could be used here for pdf and excelFile 

        public CreatePdfTableActionCommand(PdfFile<T> pdfFile)
        {
            _pdfFile = pdfFile;
        }

        public IActionResult Execute()
        {
            var excelMemoryStream = _pdfFile.Create();

            return new FileContentResult(excelMemoryStream.ToArray(), _pdfFile.FileType) { FileDownloadName = _pdfFile.FileName };
        }
    }
}