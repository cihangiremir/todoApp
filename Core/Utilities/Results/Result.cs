namespace Core.Utilities.Results
{
    public class Result : IResult
    {
        public Result(bool success, string message) : this(success)
        {
            Message = message;
            //Success = success;this(success) yazınca alltakini alıp yollar aynı kodu bir daha yazmaya gerek yoktur.
        }
        public Result(bool success)
        {
            Success = success;
        }
        public bool Success { get; }

        public string Message { get; }
    }
}
