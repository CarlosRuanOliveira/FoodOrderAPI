using Application.DTOs;
using Application.Errors;

namespace Application.Validators
{
    public class OrderValidator
    {
        public static void ValidateCreateOrderDTO(CreateOrderDTO request)
        {
            if (string.IsNullOrEmpty(request.CustomerPhoneNumber))
            {
                throw new ArgumentException(string.Format(ErrorMsg.CustomerParamRequired, "Telefone"));
            }

            if (request.OrderItems == null || request.OrderItems.Count == 0)
            {
                throw new ArgumentException(ErrorMsg.OrderMustContainAtLeastOneItem);
            }

            foreach (var item in request.OrderItems)
            {
                if (item.Quantity <= 0)
                {
                    throw new ArgumentException(string.Format(ErrorMsg.ItemQuantityMustBeGreaterThanZero, item.ItemId));
                }
            }
        }
    }
}