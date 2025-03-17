using Application.DTOs.Request;
using Application.Errors;

namespace Application.Validators
{
    public class OrderItemValidator
    {
        public static void ValidateUpdateOrderItemsDTO(long orderId, UpdateOrderItemsDTO request)
        {
            if (request.OrderItems == null || request.OrderItems.Count == 0)
                throw new ArgumentException(ErrorMsg.OrderMustContainAtLeastOneItem);

            foreach (var item in request.OrderItems)
            {
                if (item.Quantity <= 0)
                    throw new ArgumentException(string.Format(ErrorMsg.ItemQuantityMustBeGreaterThanZero, item.ItemId));
            }
        }
    }
}