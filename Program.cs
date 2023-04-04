using Globalization.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
#region Localizer
builder.Services.AddSingleton<LanguageServices>();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddMvc().AddViewLocalization().AddDataAnnotationsLocalization(options => options.DataAnnotationLocalizerProvider =
(type, factory) =>
{
    var assemblyName = new AssemblyName(typeof(SharedResource).GetTypeInfo().Assembly.FullName);
    return factory.Create(nameof(SharedResource), assemblyName.Name);
});


builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportCultures =new List<CultureInfo>
    {
        new CultureInfo("en-US"),
        new CultureInfo("en-FR"),
        new CultureInfo("en-TR"),
    };
    options.DefaultRequestCulture=new RequestCulture(culture:"tr-Tr",uiCulture:"tr-Tr");
    options.SupportedCultures=supportCultures;
    options.SupportedUICultures=supportCultures;
    options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
});
#endregion

// Add services to the container.
builder.Services.AddControllersWithViews();

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
app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);   
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
