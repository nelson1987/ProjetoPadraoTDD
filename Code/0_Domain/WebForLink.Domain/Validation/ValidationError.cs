﻿namespace WebForLink.Domain.Validation
{
    public class ValidationError
    {
        public ValidationError(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}