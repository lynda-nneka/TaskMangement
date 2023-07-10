﻿using System;
using Newtonsoft.Json;

namespace TaskManagement.Services.Infrastructure
{
    public class SuccessResponse
    {
        public bool Success { get; set; }
        public object Data { get; set; }
    }

    public class ErrorResponse
    {
        public int Status { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}

