using BlazorApp.Components;
using BlazorApp;
using BlazorApp.Components.Pages;
using System.Runtime.InteropServices.JavaScript;
using DataAccessLibrary;
using Structure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddBlazorBootstrap();
builder.Services.AddServerSideBlazor();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddTransient<ISqlDataAccess, SqlDataAccess>();
builder.Services.AddTransient<IProfileData, ProfileData>();


//testing roster display by initializing a Roster and adding wrestlers here (will need to be refactored with a hosted database later (sql or N))
builder.Services.AddSingleton<Structure.Roster>(ServiceProvider =>{
    Structure.Roster rosterObj = new Structure.Roster("test SchoolName");
    //rosterObj.rosterList = new Dictionary<string, Structure.Wrestler>();
    rosterObj.addWrestler("jim", "smith", 7, 5, "male");
    rosterObj.addWrestler("john", "green", 6, 2, "male");
    rosterObj.addWrestler("jack", "taylor", 6, 3, "male");
    rosterObj.addWrestler("jessica", "graham", 7, 4, "female");
    rosterObj.addWrestler("tom", "phillips", 5, 5, "male");
    rosterObj.addWrestler("simon", "jefferson", 6, 3, "male");
    rosterObj.addWrestler("frank", "linberg", 6, 2, "male");
    rosterObj.addWrestler("tina", "tomlinson", 7, 1, "female");
    return rosterObj;
    

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

Structure.Roster roster = new Structure.Roster("test schoolname");


app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
