using Application.Errors;

namespace Application.Validators
{
    public class CustomerValidator
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