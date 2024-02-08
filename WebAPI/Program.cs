using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using WebAPI;
using WebAPI.Authentication;
using WebAPI.Data;





var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options => {
    
    //dependency inversion
    options.Filters.Add<LogActivityFilter>();

    });




// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// set database
builder.Services.AddDbContext<inherDbContext>(bulider => bulider.UseSqlServer("server=HESHAM\\HESHAMDB; database= Products; integrated security = true ; trust server certificate= true; "));


// authentication
builder.Services.AddAuthentication()
// can add more than one scheme. but we use the default now.
        .AddScheme<AuthenticationSchemeOptions,BasicAuthenticationHandler>("Basic", null);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}




app.UseHttpsRedirection();

app.UseAuthorization();


// middlewares 
app.UseMiddleware<RateLimitingMiddleWare>();
app.UseMiddleware<ProfilingMiddleWare>();

app.MapControllers();

app.Run();
