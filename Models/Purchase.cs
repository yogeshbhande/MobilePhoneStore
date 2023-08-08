using System.ComponentModel.DataAnnotations;

namespace MobilePhoneStore.Models
{
    public class Purchase
    {
        [Key]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int MobilePhoneId { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}
