using System.Text.Json.Serialization;

namespace CatalogoMinimal.Models
{
    public class Category : Entity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        public ICollection<Product>? Products { get; set; }
    }
}
