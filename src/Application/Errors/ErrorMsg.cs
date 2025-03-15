namespace Application.Errors
{
    public static class ErrorMsg
    {
        public const string InvalidQuantity = "Quantidade deve ser pelo menos 1.";
        public const string CustomerParamRequired = "{0} do cliente é obrigatório.";
        public const string OrderMustContainAtLeastOneItem = "O pedido deve conter pelo menos um item.";
        public const string ItemQuantityMustBeGreaterThanZero = "A quantidade do item com ID {0} deve ser maior que zero.";
        public const string MenuItemNotFound = "Item com ID {0} não encontrado no cardápio.";
    }
}