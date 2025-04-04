using Application.Errors;

namespace Application.Validators
{
    public class ValidatePaginationParams
    {
        public static void ValidatePageAndPageSize(int page, int pageSize)
        {
            if (page < 1 || pageSize < 1)
            {
                throw new ArgumentException(ErrorMsg.InvalidPaginationValues);
            }
        }
    }
}