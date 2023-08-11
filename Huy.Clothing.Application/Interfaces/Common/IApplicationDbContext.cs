using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huy.Clothing.Application.Interfaces.Common;

public interface IApplicationDbContext
{
    //properties
    DbContext DbContext { get; }
}
