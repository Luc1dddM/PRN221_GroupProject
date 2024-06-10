

namespace PRN221_GroupProject.DTO.product
{
    public class ProductCreateDTO
    {
        public string ProductId { get; set; } = null!;

        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public double Price { get; set; }

        public string? Description { get; set; }

        public IFormFile ImageUrl { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public string UpdatedBy { get; set; } = null!;

        public DateTime UpdatedAt { get; set; }

        public bool Status { get; set; }
    }
}
