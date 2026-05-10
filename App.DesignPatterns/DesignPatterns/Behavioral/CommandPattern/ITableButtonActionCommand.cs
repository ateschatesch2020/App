using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.DesignPatterns.DesignPatterns.Behavioral.CommandPattern
{
    public interface ITableButtonActionCommand
    {// ICommand
        IActionResult Execute();
    }
}
