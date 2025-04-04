using System.ComponentModel.DataAnnotations;
using Application.DTOs.Request;
using Application.Errors;

namespace Application.Validators
{
    public class MenuItemValidator : ValidatePaginationParams
    {

        private const decimal MinimumMenuItemPrice = 0.01M;

        public static void ValidateCreateMenuItemDTO(CreateMenuItemDTO request)
        {
            if (string.IsNullOrEmpty(request.Name))
                throw new ValidationException(string.Format(ErrorMsg.ParamRequired, "Nome"));

            if (!IsPriceValid(request.PriceCents))
                throw new ArgumentException(string.Format(ErrorMsg.MinimumPrice, MinimumMenuItemPrice));
        }

        public static void ValidateUpdateMenuItemDTO(long menuItemId, UpdateMenuItemDTO request)
        {
            if (menuItemId <= 0)
                throw new ArgumentException(string.Format(ErrorMsg.ParamRequired, menuItemId));
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ArgumentException(string.Format(ErrorMsg.ParamRequired, "Nome"));
            if (!IsPriceValid(request.PriceCents))
                throw new ArgumentException(string.Format(ErrorMsg.MinimumPrice, MinimumMenuItemPrice));
        }

        private static bool IsPriceValid(decimal price)
        {
            return price >= MinimumMenuItemPrice;
        }
    }
}