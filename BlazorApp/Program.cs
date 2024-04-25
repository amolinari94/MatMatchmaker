using BlazorApp.Components;
using BlazorApp;
using BlazorApp.Components.Pages;
using System.Runtime.InteropServices.JavaScript;
using DataAccessLibrary;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Structure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddBlazorBootstrap();
builder.Services.AddServerSideBlazor();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddTransient<ISqlDataAccess, SqlDataAccess>();
builder.Services.AddTransient<IProfileData, ProfileData>();

builder.Services.AddAntiforgery(options =>
{
    options.Cookie.Expiration = TimeSpan.Zero;
    // Disable anti-forgery token validation for testing purposes
    options.SuppressXFrameOptionsHeader = true;
    
});


//testing roster display by initializing a Roster and adding wrestlers here (will need to be refactored with a hosted database later (sql or N))
builder.Services.AddSingleton<Structure.Roster>(ServiceProvider =>{
    Structure.Roster rosterObj = new Structure.Roster("test SchoolName");
    //rosterObj.rosterList = new Dictionary<string, Structure.Wrestler>();
    rosterObj.addWrestler("Jim", "Smith", 7, 5, "male",true, 95);
    rosterObj.addWrestler("John", "Green", 6, 2, "male",false,100);
    rosterObj.addWrestler("Jack", "Taylor", 6, 3, "male",true,85);
    rosterObj.addWrestler("Jessica", "Graham", 7, 4, "female",true,75);
    rosterObj.addWrestler("Tom", "Phillips", 5, 5, "male",false,100);
    rosterObj.addWrestler("Simon", "Jefferson", 6, 3, "male",true,90);
    rosterObj.addWrestler("Frank", "Linberg", 6, 2, "male",true,80);
    rosterObj.addWrestler("Tina", "Tomlinson", 7, 1, "female",false,85);
    return rosterObj;
    

});

Structure.School TestSchool = new Structure.School("lune","test SchoolName","kc","mo");
Structure.Event testEvent = new Structure.Event(TestSchool,DateTime.Today);
builder.Services.AddSingleton<Structure.Event>(ServiceProvider =>{
    
    Wrestler jim=new Structure.Wrestler("Jim", "Smith", 7, 5, "male", "test",true, 95);
    Wrestler John=new Structure.Wrestler("John", "Green", 6, 2, "male","test",false,100);
    Wrestler Jack=new Structure.Wrestler("Jack", "Taylor", 6, 3, "male","test",true,85);
    Wrestler Jessica=new Structure.Wrestler("Jessica", "Graham", 7, 4, "female","test",true,75);
    Wrestler Tom=new Structure.Wrestler("Tom", "Phillips", 5, 5, "male","test",false,100);
    Wrestler Simon=new Structure.Wrestler("Simon", "Jefferson", 6, 3, "male","test",true,90);
    Wrestler Frank=new Structure.Wrestler("Frank", "Linberg", 6, 2, "male","test",true,80);
    Wrestler Tina=new Structure.Wrestler("Tina", "Tomlinson", 7, 1, "female","test",false,85);
    testEvent.matchList=[[jim,John],[Jack,Jessica],[Tom, Simon],[Frank,Tina]];
    return testEvent;
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
