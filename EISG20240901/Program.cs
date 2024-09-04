var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Crear una lista para almacenar objetos de tipo Producto (productos)
var productos = new List<Producto>();

// Configurar una ruta GET para obtener todos los productos
app.MapGet("/productos", () =>
{
    return productos; // Devuelve la lista de productos
});

// Configurar una ruta GET para obtener un producto específico por su ID
app.MapGet("/productos/{id}", (int id) =>
{
    // Busca un producto en la lista que tenga el ID especificado
    var producto = productos.FirstOrDefault(c => c.Id == id);
    return producto; // Devuelve el producto encontrado (o null si no se encuentra)
});

app.MapPost("/productos", (Producto producto) =>
{
    productos.Add(producto); // Agrega el nuevo producto a la lista
    return Results.Ok(); // Devuelve una respuesta HTTP 200 OK
});

// Configurar una ruta PUT para actualizar un producto existente por su ID
app.MapPut("/productos/{id}", (int id, Producto producto) =>
{
    // Busca un producto en la lista que tenga el ID especificado
    var existingProducto = productos.FirstOrDefault(p => p.Id == id);
    if (existingProducto != null)
    {
        // Actualiza los datos del producto existente con los datos proporcionados
        existingProducto.Nombre = producto.Nombre;
        existingProducto.Cantidad = producto.Cantidad;
        return Results.Ok(); // Devuelve una respuesta HTTP 200 OK
    }
    else
    {
        return Results.NotFound(); // Devuelve una respuesta HTTP 404 Not Found si el producto no existe
    }
});

// Configurar una ruta DELETE para eliminar un producto por su ID
app.MapDelete("/productos/{id}", (int id) =>
{
    // Busca un producto en la lista que tenga el ID especificado
    var existingProducto = productos.FirstOrDefault(c => c.Id == id);
    if (existingProducto != null)
    {
        // Elimina el producto de la lista
        productos.Remove(existingProducto);
        return Results.Ok(); // Devuelve una respuesta HTTP 200 OK
    }
    else
    {
        return Results.NotFound(); // Devuelve una respuesta HTTP 404 Not Found si el producto no existe
    }
});

// Ejecutar la aplicación
app.Run();

// Definición de la clase Producto que representa la estructura de un producto
internal class Producto
{
    public int Id {  get; set; }

    public string Nombre { get; set; }

    public int Cantidad { get; set; }
}