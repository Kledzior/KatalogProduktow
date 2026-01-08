using Kledzik.KatalogSpiworow.Interfaces; // Twoje interfejsy
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Dodaj us³ugi do kontenera (Add services to the container).
builder.Services.AddControllersWithViews();

// --- POCZ¥TEK: Dynamiczne ³adowanie DAO (Late Binding) ---

// 1. Pobierz nazwê pliku DLL z appsettings.json
var daoLibraryName = builder.Configuration["DAOSettings:LibraryName"];
if (string.IsNullOrEmpty(daoLibraryName))
{
    throw new Exception("Nie skonfigurowano nazwy biblioteki DAO w appsettings.json!");
}

// 2. Ustal œcie¿kê do pliku DLL (katalog, w którym uruchomiona jest aplikacja)
var daoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, daoLibraryName);

if (!File.Exists(daoPath))
{
    throw new Exception($"Nie znaleziono pliku biblioteki DAO pod œcie¿k¹: {daoPath}. Upewnij siê, ¿e skopiowa³eœ plik .dll do folderu bin/Debug/net9.0!");
}

// 3. Za³aduj bibliotekê (Assembly)
Assembly daoAssembly = Assembly.LoadFrom(daoPath);

// 4. ZnajdŸ klasê, która implementuje IDataRepository
Type? repoType = daoAssembly.GetTypes()
    .FirstOrDefault(t => typeof(IDataRepository).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

if (repoType == null)
{
    throw new Exception($"W bibliotece {daoLibraryName} nie znaleziono klasy implementuj¹cej IDataRepository.");
}

// 5. Zarejestruj repozytorium w systemie wstrzykiwania zale¿noœci (Dependency Injection)
// U¿ywamy AddSingleton, aby dane w DAOMock nie znika³y przy ka¿dym odœwie¿eniu strony
builder.Services.AddSingleton(typeof(IDataRepository), repoType);

// --- KONIEC: Dynamiczne ³adowanie DAO ---

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();