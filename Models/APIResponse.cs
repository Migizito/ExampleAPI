﻿using System.Net;

namespace ExampleAGAPI.Models
{
    public class APIResponse
    {
        public HttpStatusCode statusCode { get; set; }
        public bool IsExitoso { get; set; }
        public List<string> ErrorMessages { get; set; }
        public object Resultado { get; set; }
    }
}
