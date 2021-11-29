using GraphQL.Server;
using MyConferece.Data;
using MyConferece.Repositories;
using MyConference.GraphQL;

var builder = WebApplication.CreateBuilder(args);

// Add MongoDB and Repositories ######
builder.Services.AddSingleton<MyConferenceDataContext>();
builder.Services.AddScoped<SpeakerRepository>();
builder.Services.AddScoped<SessionsRepository>();

// Add GraphQL #######################
builder.Services.AddScoped<MyConferenceSchema>();
builder.Services.AddGraphQL(options =>
{
    options.EnableMetrics = false;
}).AddSystemTextJson(o => o.PropertyNameCaseInsensitive = true)
.AddGraphTypes(ServiceLifetime.Scoped);

builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Add GraphQL #########################
app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.UseGraphQL<MyConferenceSchema>();
app.UseGraphQLGraphiQL(); // /ui/graphiql
app.UseGraphQLAltair();   // /ui/altair

app.Run();