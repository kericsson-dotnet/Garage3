using Microsoft.EntityFrameworkCore;
using Garage.Data;
using Garage.Models;
using Garage.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<GarageDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")));

// Repository services
builder.Services.AddScoped<IRepository<User>, UserRepository>();
builder.Services.AddScoped<IRepository<Vehicle>, VehicleRepository>();
builder.Services.AddScoped<IRepository<VehicleType>, VehicleTypeRepository>();
builder.Services.AddScoped<IRepository<ParkingEvent>, ParkingEventRepository>();

// Other services
builder.Services.AddScoped<IGarageService, GarageService>();
// Inject services for seeding of Vehicles/vehicleTypes (todo: users next)
builder.Services.AddScoped<ISeedingService, SeedingService>();

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
        // Basic seed for Users
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

        // Basic seed for VehicleTypes
        var vehicleTypeRepository = scope.ServiceProvider.GetRequiredService<IRepository<VehicleType>>();
        var vehicleTypes = await vehicleTypeRepository.GetAll();
        if (!vehicleTypes.Any())
        {
            var newVehicleType = new VehicleType
            {
                TypeName = "Car",
                SlotsOccupied = 1
            };
            await vehicleTypeRepository.Add(newVehicleType);
        }

        // Basic seed for Vehicles
        var vehicleRepository = scope.ServiceProvider.GetRequiredService<IRepository<Vehicle>>();
        var vehicles = await vehicleRepository.GetAll();
        if (!vehicles.Any())
        {
            var user = await userRepository.Get(1);
            var vehicleType = await vehicleTypeRepository.Get(1);
            var newVehicle = new Vehicle
            {
                Owner = userRepository.Get(1).Result,
                RegNumber = "ABC123",
                Brand = "Volvo",
                Model = "V70",
                Color = "Black",
                NumberOfWheels = 4,
                VehicleType = vehicleTypeRepository.Get(1).Result,
            };
            await vehicleRepository.Add(newVehicle);
        }

        // Basic seed for ParkingEvents
        var parkingEventRepository = scope.ServiceProvider.GetRequiredService<IRepository<ParkingEvent>>();
        var parkingEvents = await parkingEventRepository.GetAll();
        if (!parkingEvents.Any())
        {
            var newParkingEvent = new ParkingEvent
            {
                CheckInTime = DateTime.Now.AddHours(-2),
                Vehicle = vehicleRepository.Get(1).Result,
            };
            await parkingEventRepository.Add(newParkingEvent);
        }
    }
});

app.Run();
