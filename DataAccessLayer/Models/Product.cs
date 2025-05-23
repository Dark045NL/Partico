using DataAccessLayer.Models;

public class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public ICollection<Order> Orders { get; set; } = new List<Order>();

    public ICollection<Part> Parts { get; } = new List<Part>();
}
