using Core.Utilities.Results;
using System.Threading.Tasks;

namespace Core.Utilities.Business
{
    public class BusinessRules
    {
        public static IResult Run(params IResult[] logics)
        {
            foreach (var result in logics)
            {
                if (!result.Success)
                {
                    return result;
                }
            }
            return null;
        }
        public static Task<IResult> RunAsync(params Task<IResult>[] logics)
        {
            foreach (var result in logics)
            {
                if (!result.Result.Success)
                {
                    return (Task<IResult>)result;
                }
            }
            return Task.FromResult<IResult>(new SuccessResult());
        }
    }
}
