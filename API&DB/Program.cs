using API_DB.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Configure CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin()   // Allows any origin
               .AllowAnyMethod()   // Allows any HTTP method (GET, POST, etc.)
               .AllowAnyHeader();  // Allows any headers
    });
});

/*
//Restrict CORS to Specific Origins (More Secure)

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", builder =>
    {
        builder.WithOrigins("http://your-client-app.com") // Allow only your specific client URL
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
*/

// Register UserService in Program.cs for Dependency Injection
builder.Services.AddScoped<UsersService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure IIS options
builder.Services.Configure<IISServerOptions>(options =>
{
    options.MaxRequestBodySize = 104857600; // 100 MB
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable CORS for your API
app.UseCors("AllowAllOrigins");

//app.UseCors("AllowSpecificOrigins");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
