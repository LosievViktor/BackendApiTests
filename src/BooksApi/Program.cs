 using BooksApi.Repositories;
 using Microsoft.OpenApi.Models;

 var builder = WebApplication.CreateBuilder(args);

 builder.Services.AddControllers();

 builder.Services.AddEndpointsApiExplorer();
 builder.Services.AddSwaggerGen(options =>
 {
     options.SwaggerDoc("v1", new OpenApiInfo
     {
         Title = "Books API",
         Version = "v1",
         Description = "Swagger for backend for automation tests portfolio. Developed by Losiev Viktor. ",
     });
 });


 builder.Services.AddSingleton<IBookRepository, InMemoryBookRepository>();

 var app = builder.Build();

 app.UseSwagger();
 app.UseSwaggerUI(options =>
 {
     options.SwaggerEndpoint("/swagger/v1/swagger.json", "Books API v1");
 });

 app.UseHttpsRedirection();
 app.MapControllers();

 app.Run();

 public partial class Program { }
