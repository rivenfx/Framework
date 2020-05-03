using Riven.AspNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riven.AspNetCore.Models
{
    /// <summary>
    /// This class is used to create standard responses for AJAX/remote requests.
    /// </summary>
    [Serializable]
    public class AjaxResponse<TResult> : AjaxResponseBase
    {
        /// <summary>
        /// The actual result object of AJAX request.
        /// It is set if <see cref="AjaxResponseBase.Success"/> is true.
        /// </summary>
        public TResult Result { get; set; }

        /// <summary>
        /// Creates an <see cref="AjaxResponse"/> object with <see cref="Result"/> specified.
        /// <see cref="AjaxResponseBase.Success"/> is set as true.
        /// </summary>
        /// <param name="result">The actual result object of AJAX request</param>
        public AjaxResponse(TResult result)
        {
            Result = result;
            Success = true;
        }

        /// <summary>
        /// Creates an <see cref="AjaxResponse"/> object with <see cref="Result"/> specified.
        /// <see cref="AjaxResponseBase.Success"/> is set as true.
        /// </summary>
        /// <param name="result">The actual result object of AJAX request</param>
        /// <param name="success">Indicates success status of the result</param>
        public AjaxResponse(TResult result, bool success = true)
        {
            Result = result;
            Success = success;
        }

        /// <summary>
        /// Creates an <see cref="AjaxResponse"/> object.
        /// <see cref="AjaxResponseBase.Success"/> is set as true.
        /// </summary>
        public AjaxResponse()
        {
            Success = true;
        }

        /// <summary>
        /// Creates an <see cref="AjaxResponse"/> object with <see cref="AjaxResponseBase.Success"/> specified.
        /// </summary>
        /// <param name="success">Indicates success status of the result</param>
        public AjaxResponse(bool success)
        {
            Success = success;
        }


    }
}