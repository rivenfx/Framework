using Riven.AspNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riven.AspNetCore.Mvc
{
    public class RequestActionInfo
    {
        public bool IsObjectResult { get; set; }

        public WrapResultAttribute WrapResultAttribute { get; set; }
    }
}
