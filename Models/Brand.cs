using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MobilePhoneStore.Models
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }
        public string BrandName { get; set; }

        [JsonIgnore]
        public ICollection<MobilePhone>? MobilePhones { get; set; }
    }
}
