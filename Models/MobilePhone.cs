using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MobilePhoneStore.Models
{
    public class MobilePhone
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int BrandId { get; set; }
        public string ModelName { get; set; }
        public string Specifications { get; set; }
        public decimal Price { get; set; }
        public Brand Brand { get; set; }



    }
}
