﻿using System.Text.Json;

namespace Demo.Api.Models
{
    public class ErrorDetails
    {
        public int  StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
