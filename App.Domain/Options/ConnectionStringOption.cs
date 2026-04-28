using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Options
{
    public class ConnectionStringOption
    {
        public const string Key = "ConnectionStrings";
        public string SqlServer { get; set; } = default!;
    }
}
