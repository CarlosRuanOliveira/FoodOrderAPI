namespace Application.Errors
{
    public static class ErrorMsg
    {
        public const string InvalidQuantity = "Quantidade deve ser pelo menos 1.";
        public const string CustomerParamRequired = "{0} do cliente é obrigatório.";
        public const string OrderMustContainAtLeastOneItem = "O pedido deve conter pelo menos um item.";
        public const string ItemQuantityMustBeGreaterThanZero = "A quantidade do item com ID {0} deve ser maior que zero.";
        public const string MenuItemNotFound = "Item com ID {0} não encontrado no cardápio.";
        public const string ParamRequired = "O campo {0} é obrigatório.";
        public const string MinimumPrice = "O valor mínimo para preço é R${0}.";
        public const string MenuItemAlreadyExists = "O item com nome {0}[ID: {1}] já existe no cardápio.";
        public const string InvalidRequest = "Requisição inválida.";
        public const string InternalError = "Ocorreu um erro interno.";
    }
}