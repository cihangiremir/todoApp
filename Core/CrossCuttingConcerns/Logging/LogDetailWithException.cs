using System.Collections.Generic;

namespace Core.CrossCuttingConcerns.Logging
{
    public class LogDetailWithException
    {
        public string MethodName { get; set; }
        public string ExceptionMessage { get; set; }
        public string ExceptionPath { get; set; }
        public List<LogParameter> LogParameters { get; set; }
    }
}
