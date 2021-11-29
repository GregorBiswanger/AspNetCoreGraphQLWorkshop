using MyConferece.Data;
using MyConferece.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add MongoDB and Repositories ######
builder.Services.AddSingleton<MyConferenceDataContext>();
builder.Services.AddScoped<SpeakerRepository>();
builder.Services.AddScoped<SessionsRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.Run();