using System;

public class ResponseEntity<T>
{ 
    public long statusCode { get; set; }
        public string message { get; set; }
        public T content { get; set; }
        public DateTime dateTime { get; set; }
}