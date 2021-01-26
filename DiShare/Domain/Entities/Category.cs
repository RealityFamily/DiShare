
using System;

namespace DiShare.Domain.Entities
{
  public class Category
  {
    public int Id { get; set; }

    public string Name { get; set; }

    public int? CategoryId { get; set; }

    public DateTime CreatedAt { get; set; }
  }
}
