namespace common.DTOs
{
    public class ShoppingListDto
    {
        public int Id { get; set; }
        public string Title { get; set; } // אם יש שם לרשימה
        public List<ShoppingListItemDto> Items { get; set; }
    }
}