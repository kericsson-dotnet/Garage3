using Microsoft.EntityFrameworkCore;
using Garage.Data;
using Garage.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<GarageDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")));

builder.Services.AddScoped<IRepository<User>, UserRepository>();
builder.Services.AddScoped<IRepository<ParkingEvent>, ParkingEventRepository>();

builder.Services.AddDbContext<GarageDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Services.GetService<IHostApplicationLifetime>().ApplicationStarted.Register(async () =>
{
    // Create initial data here.
    using (var scope = app.Services.CreateScope())
    {
        var userRepository = scope.ServiceProvider.GetRequiredService<IRepository<User>>();
        var users = await userRepository.GetAll();
        // Only create when database is empty. 
        if (!users.Any())
        {
            var newUser = new User
            {
                PersonalNumber = "8001010352",
                FirstName = "Första",
                LastName = "Användarensson",
                Age = 44
            };
            await userRepository.Add(newUser);
        }
    }
});

app.Run();
