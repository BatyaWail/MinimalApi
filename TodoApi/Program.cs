using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using TodoApi;

 var builder = WebApplication.CreateBuilder(args);
// Add services
builder.Services.AddDbContext<ToDoDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("ToDoDB"), new MySqlServerVersion(new Version(8, 0, 36))));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});
var app = builder.Build();


// app.UseCors("CorsPolicy");

app.MapGet("/items", async (ToDoDbContext db)=> {

    var items=await db.Items.ToListAsync();
    return Results.Ok(items);
});

app.MapGet("/items/:{id}", async (int id,ToDoDbContext db) =>
{
    var item=await db.Items.FindAsync(id);
    if(item==null)
    {
        return Results.NoContent();
    }
    return Results.Ok(item);
});

app.MapPost("/items",async (Item item,ToDoDbContext db) =>
{
    if (item == null)
    {
        return Results.BadRequest("Invalid item data");
    }
    db.Items.Add(item);
    await db.SaveChangesAsync();
    return Results.Created($"/todoitems/{item.Id}", item);
});


app.MapPut("/items/{id}", async (int id, Item item, [FromServices] ToDoDbContext context) =>
{
    if (item == null)
    {
        return Results.BadRequest("Invalid item data");
    }
    var existingItem = await context.Items.FindAsync(id);
    if (existingItem == null) 
    {
        return Results.NotFound("Item not found!");
    }

    existingItem.Name = item.Name;
    existingItem.IsComplete = item.IsComplete;
    await context.SaveChangesAsync();
    return Results.Ok(existingItem);
});

app.MapDelete("/items/{id}", async (int id,ToDoDbContext db) =>
{
    var item= await db.Items.FindAsync(id) ;
    if (item == null)
    {
        return Results.NotFound();
    }
   
    db.Items.Remove(item);
    await db.SaveChangesAsync();
    return Results.Ok(item);

});

app.MapGet("/", () => "hello  World!");
app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}
app.UseSwagger(options =>
{
    options.SerializeAsV2 = true;
});
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});


app.Run();