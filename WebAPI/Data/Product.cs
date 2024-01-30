using System.ComponentModel.DataAnnotations;

namespace WebAPI.Data
{
    public class Product
    {
        [Key]
        public required int Id { get; set; }

        public required string Name { get; set; }

        public required string SKU { get; set; }



    }
}
