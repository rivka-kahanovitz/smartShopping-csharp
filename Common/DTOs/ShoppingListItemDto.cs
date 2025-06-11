namespace common.DTOs
{
    public class ShoppingListItemDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string Barcode { get; set; }      // נדרש למיפוי

        public int Quantity { get; set; }
    }
}

