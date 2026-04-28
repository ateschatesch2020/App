using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence
{
    public class ConnectionStringOption
    {
        public const string Key = "ConnectionStrings";
        public string SqlServer { get; set; } = default!;
    }
}
