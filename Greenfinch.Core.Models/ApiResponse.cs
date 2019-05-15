﻿using Greenfinch.Core.Models.Validation;
using System.Collections.Generic;

namespace Greenfinch.Core.Models.Models
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public IList<ValidationError> Errors { get; set; }

        public ApiResponse(T Data, IList<ValidationError> Errors = null)
        {
            this.Data = Data;
            this.Errors = Errors;
        }
    }
}
