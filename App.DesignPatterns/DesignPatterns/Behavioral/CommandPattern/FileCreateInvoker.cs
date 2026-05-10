using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.DesignPatterns.DesignPatterns.Behavioral.CommandPattern
{
    public class FileCreateInvoker
    {// Invoker
        private ITableButtonActionCommand? _tableActionCommand;
        private List<ITableButtonActionCommand> tableActionCommands = new List<ITableButtonActionCommand>();

        public void SetCommand(ITableButtonActionCommand tableActionCommand)
        {
            _tableActionCommand = tableActionCommand;
        }

        public void AddCommand(ITableButtonActionCommand tableActionCommand)
        {
            tableActionCommands.Add(tableActionCommand);
        }

        public IActionResult CreateFile()
        {
            return _tableActionCommand .Execute();
        }

        public List<IActionResult> CreateFiles()
        {
            return tableActionCommands.Select(x => x.Execute()).ToList();
        }
    }
}