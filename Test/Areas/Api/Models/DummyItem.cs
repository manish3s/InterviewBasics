using System.ComponentModel.DataAnnotations;

namespace Test.Areas.Api.Models;

public sealed class DummyItem
{
    public int Id { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;

    [Range(0.01, 999999.99)]
    public decimal Price { get; set; }

    public bool IsActive { get; set; } = true;
}
