﻿using Riven.AspNetCore.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Riven.AspNetCore.Models
{
    public abstract class AjaxResponseBase
    {
        /// <summary>
        /// This property can be used to redirect user to a specified URL.
        /// </summary>
        public string TargetUrl { get; set; }

        /// <summary>
        /// Indicates success status of the result.
        /// Set <see cref="Error"/> if this value is false.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// This property can be used to indicate that the current user has no privilege to perform this request.
        /// </summary>
        public bool UnAuthorizedRequest { get; set; }

        /// <summary>
        /// Http response status code, default value: 200.
        /// </summary>
        public int Code { get; set; } = (int)HttpStatusCode.OK;

        /// <summary>
        /// Is the result wrapper class.
        /// </summary>
        public bool __wrap { get; set; } = true;
    }
}
