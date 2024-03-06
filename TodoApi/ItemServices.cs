using TodoApi;

class ItemService { 

    private readonly ToDoDbContext _toDoDbContext;

    public ItemService(ToDoDbContext context)
    {
        _toDoDbContext = context;
    }
    public IEnumerable<Item> GetAll()
    {
        return _toDoDbContext.Items.ToList();
    }
    public async Task<Item> AddAsync(Item newItem)
    {
        
        _toDoDbContext.Items.Add(newItem);
        await _toDoDbContext.SaveChangesAsync();
        return newItem;
    }
    public async Task UpdateAsync(Item item)
    {
        _toDoDbContext.Items.Update(item);
        await _toDoDbContext.SaveChangesAsync();
    }
    public async Task DeleteAsync(Item item)
    {
        _toDoDbContext.Items.Remove(item);
        await _toDoDbContext.SaveChangesAsync();
    }

    public async Task<Item> GetByIdAsync(int id)
    {
        return await _toDoDbContext.Items.FindAsync(id);
    }
}



// app.MapGet("/items", async ([FromServices] ItemService service) =>
// {
//     var items=service.GetAll();
//     return Results.Ok(items);
// });

// app.MapPost("/items", async ([FromBody] Item item, ItemService service) =>
// {
//     // var item = await request.ReadFromJsonAsync<Item>();
//     if (item == null)
//     {
//         return Results.BadRequest("Invalid item data");
//     }

//     // await dbContext.Items.AddAsync(item);
//     // await dbContext.SaveChangesAsync();
//     await service.AddAsync(item);
//     return Results.Created($"/items/{item.Id}", item);
// });

// app.MapPut("/items/{id}", async (int id,[FromBody] Item item, ItemService service) =>
// {
//     // var existingItem = await dbContext.Items.FindAsync(id);
//     if (item == null)
//     {
//         return Results.BadRequest("Invalid item data");
//     }

//     // var updatedItem = await request.ReadFromJsonAsync<Item>();

//     // if (updatedItem == null)
//     // {
//     //     return Results.BadRequest("Invalid request body");
//     // }
//     var existingItem= await service.GetByIdAsync(id);
//     if(existingItem==null)
//     {
//         return Results.NotFound("Item not found!");
//     }
//     // existingItem.Name = updatedItem.Name;
//     // existingItem.IsComplete = updatedItem.IsComplete;
//     existingItem.IsComplete = item.IsComplete;///????????

//     // await dbContext.SaveChangesAsync();
//     await service.UpdateAsync(existingItem);
//     return Results.Ok(existingItem);
// });

// app.MapDelete("/items/{id}", async (int id, ItemService service) =>
// {
//     var item = await service.GetByIdAsync(id);
//     if (item == null)
//     {
//         return Results.NotFound();
//     }

//     // dbContext.Items.Remove(item);
//     // await dbContext.SaveChangesAsync();
//     await service.DeleteAsync(item);
//     return Results.Ok("Item is deleted");
// });

// app.MapGet("/", () => "Hello World!");