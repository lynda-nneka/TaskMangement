using System;
namespace TaskManagement.Models.Dtos.Responses
{
    public class Response<T>
    {
        public string Message { get; set; }
        public T Result { get; set; }
        public bool IsSuccessful { get; set; }
    }
}

