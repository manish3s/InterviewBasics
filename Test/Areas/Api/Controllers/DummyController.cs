using Microsoft.AspNetCore.Mvc;
using Test.Areas.Api.Models;

namespace Test.Areas.Api.Controllers;

[ApiController]
[Area("Api")]
[Route("api/[controller]")]
public sealed class DummyController : ControllerBase
{
    private static readonly object SyncRoot = new();
    private static readonly List<DummyItem> Items =
    [
        new DummyItem { Id = 1, Name = "Interview Laptop", Price = 899.99m },
        new DummyItem { Id = 2, Name = "Practice Keyboard", Price = 49.50m }
    ];

    [HttpGet]
    public ActionResult<IEnumerable<DummyItem>> GetAll()
    {
        lock (SyncRoot)
        {
            return Ok(Items.Select(Clone).ToList());
        }
    }

    [HttpGet("{id:int}")]
    public ActionResult<DummyItem> GetById(int id)
    {
        lock (SyncRoot)
        {
            var item = Items.FirstOrDefault(x => x.Id == id);
            return item is null ? NotFound() : Ok(Clone(item));
        }
    }

    [HttpPost]
    public ActionResult<DummyItem> Create(DummyItem item)
    {
        lock (SyncRoot)
        {
            item.Id = Items.Count == 0 ? 1 : Items.Max(x => x.Id) + 1;
            Items.Add(Clone(item));
        }

        return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(int id, DummyItem item)
    {
        lock (SyncRoot)
        {
            var existing = Items.FirstOrDefault(x => x.Id == id);
            if (existing is null)
            {
                return NotFound();
            }

            existing.Name = item.Name;
            existing.Price = item.Price;
            existing.IsActive = item.IsActive;
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        lock (SyncRoot)
        {
            var item = Items.FirstOrDefault(x => x.Id == id);
            if (item is null)
            {
                return NotFound();
            }

            Items.Remove(item);
        }

        return NoContent();
    }

    private static DummyItem Clone(DummyItem item) => new()
    {
        Id = item.Id,
        Name = item.Name,
        Price = item.Price,
        IsActive = item.IsActive
    };
}
