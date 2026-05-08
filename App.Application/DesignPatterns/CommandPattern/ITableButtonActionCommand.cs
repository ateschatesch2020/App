using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.DesignPatterns.CommandPattern
{
    public interface ITableButtonActionCommand
    {// ICommand
        IActionResult Execute();
    }
}
