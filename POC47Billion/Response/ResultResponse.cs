using System;
using static POC47Billion.Model.PocModel;

namespace POC47Billion.Response
{
    public class ErrorResponse
    {
        public string? Message { get; set; }
        public int ErrorCode { get; set; }
    }
    public class ResultResponse
    {
            public bool IsSuccessful { get; set; }
            public string? TimeOfRequest { get; set; }
            public string? Message { get; set; }
            public RikiResultSet? RikiResultSet { get; set; }
            public Data? Data { get; set; }
            public byte[]? PdfByte { get; set; }
    }
    public class Data
    {
        public object? JsonData { get; set; }
        public string? Htmlcode { get; set; }
        public byte[]? Bytarr { get; set; }
    }

   public class Result<T>

    {
        public Result(int responseCode,  T data)
        {
            ResponseCode = responseCode;
            Data = data;
        }
        public Result(int responseCode, string message )
        {
            ResponseCode = responseCode;
        }
        public int ResponseCode { get; set; }
        public T? Data { get; set; }
        public static Result<T> Success(string message, T data)
        {
            return new Result<T>(200,  data);
        }
        public static Result<T> Failure(string message)
        {
            return new Result<T>(500,  message);
        }
    }
}
