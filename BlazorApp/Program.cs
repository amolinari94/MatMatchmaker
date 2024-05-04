using BlazorApp.Components;
using BlazorApp;
using BlazorApp.Components.Pages;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json;
using System.Text.Json.Serialization;
using BlazorApp.EmailSystem.Model;
using BlazorApp.EmailSystem.Services;
using Blazored.LocalStorage;
using Blazored.SessionStorage;
using DataAccessLibrary;
using DataAccessLibrary.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Structure;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddBlazorBootstrap();
builder.Services.AddServerSideBlazor();
builder.Services.AddRazorComponents().AddInteractiveServerComponents();


builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<ProfileData>();




builder.Services.AddTransient<ISqlDataAccess, SqlDataAccess>();
builder.Services.AddTransient<IProfileData, ProfileData>();
builder.Services.AddTransient<IWrestlerData, WrestlerData > ();
builder.Services.AddTransient<IEventData, EventData > ();
builder.Services.AddTransient<IMatchData, MatchData > ();
builder.Services.AddTransient<IEventGuestsData, EventGuestsData>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddBlazoredSessionStorage(config => {
        config.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
        //config.JsonSerializerOptions.IgnoreNullValues = true;
        config.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        config.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
        config.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        config.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        config.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
        config.JsonSerializerOptions.WriteIndented = false;
    }
);
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddScoped<IMailService, MailService>();


/*builder.Services.AddAntiforgery(options =>
{
    options.Cookie.Expiration = TimeSpan.Zero;
    // Disable anti-forgery token validation for testing purposes
    options.SuppressXFrameOptionsHeader = true;
    
});*/


//testing roster display by initializing a Roster and adding wrestlers here (will need to be refactored with a hosted database later (sql or N))
builder.Services.AddSingleton<Structure.Roster>(ServiceProvider =>{
    Structure.Roster rosterObj = new Structure.Roster("test SchoolName");
    //rosterObj.rosterList = new Dictionary<string, Structure.Wrestler>();
    // rosterObj.AddWrestler("testEmail1","Jim", "Smith", 7, 5, "male");
   // rosterObj.AddWrestler("testEmail1","John", "Green", 6, 2, "male");
    //rosterObj.AddWrestler("testEmail1","Jack", "Taylor", 6, 3, "male");
   // rosterObj.AddWrestler("testEmail1","Jessica", "Graham", 7, 4, "female");
    //rosterObj.AddWrestler("testEmail1","Tom", "Phillips", 5, 5, "male");
  //  rosterObj.AddWrestler("testEmail1","Simon", "Jefferson", 6, 3, "male");
   // rosterObj.AddWrestler("testEmail1","Frank", "Linberg", 6, 2, "male");
   // rosterObj.AddWrestler("testEmail1","Tina", "Tomlinson", 7, 1, "female");
    return rosterObj;
    

});

/*
builder.Services.AddSingleton<Structure.Event>(ServiceProvider => {
    Structure.Event newEvent = new Structure.Event("testID", new School("testID", "cityt", "statet","addresst"), "guestListTest",
    DateTime.Now);
    return newEvent;
});
*/

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
