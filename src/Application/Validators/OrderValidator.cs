using Application.DTOs;

namespace Application.Validators
{
    public class OrderValidator
    {
        public static void ValidateCreateOrderDTO(CreateOrderDTO request)
        {
            if (string.IsNullOrEmpty(request.CustomerPhoneNumber))
            {
                throw new ArgumentException("O telefone do cliente é obrigatório.");
            }

            if (request.OrderItems == null || !request.OrderItems.Any())
            {
                throw new ArgumentException("O pedido deve conter pelo menos um item.");
            }

            foreach (var item in request.OrderItems)
            {
                if (item.Quantity <= 0)
                {
                    throw new ArgumentException($"A quantidade do item com ID {item.ItemId} deve ser maior que zero.");
                }
            }
        }
    }
}