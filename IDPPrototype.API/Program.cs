using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
/*
builder.Services.AddAuthentication()
    .AddJwtBearer(options =>
    {
        options.Authority = "https://localhost:5001/";
        // Audience validation is disabled here because access to the
        // api is modeled with ApiScopes only. By default,
        // no audience will be emitted unless the api is modeled with
        // ApiResources instead. See here for a more in-depth discussion:
        // 
        // https://docs.duendesoftware.com/identityserver/v7/apis/aspnetcore/jwt/#adding-audience-validation
        options.TokenValidationParameters.ValidateAudience = false;
    });
*/

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseAuthorization();

// ----- ROUTES ---------------------------------------

app.MapGet("/identity",
    (ClaimsPrincipal user) => user.Claims.Select(c => new { c.Type, c.Value }))
    .RequireAuthorization();

app.MapGet("/ping", () => DateTime.UtcNow);

// -----------------------------------------------------

app.Run();