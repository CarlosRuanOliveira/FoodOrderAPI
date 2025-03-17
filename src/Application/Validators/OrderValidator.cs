using Application.DTOs.Request;
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

        public static void ValidateUpdateOrder(UpdateOrderDTO request)
        {
            if (!Enum.IsDefined(typeof(Domain.Enums.OrderStatus), request.Status))
                throw new ArgumentException(ErrorMsg.InvalidOrderStatus);
        }
    }
}