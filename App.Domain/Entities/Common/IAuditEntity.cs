using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Entities.Common
{
    public interface IAuditEntity
    {
        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }
    }
}
