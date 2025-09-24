using System.ComponentModel.DataAnnotations;

namespace sentry_core.DTOs;


public class CreateOrderDto
{
    [Required]
    public string UserId { get; set; }

    [Required]
    public string ProductId { get; set; }

    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }
}