using peajeWebApp.Repositorio.Dao;
using peajeWebApp.Repositorio.Dao.ExtraerDatos;
using peajeWebApp.Repositorio.Dao.ProcedimientosSQL;
using peajeWebApp.Repositorio.IDao;
using peajeWebApp.Repositorio.IDao.ExtraerDatos;
using peajeWebApp.Repositorio.IDao.ProcedimientosSQL;

var builder = WebApplication.CreateBuilder(args);

var CORSOpenPolicy = "OpenCORSPolicy";

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.WebHost.UseUrls("http://*:6001");
builder.Host.UseWindowsService();


builder.Services.AddScoped<IExtraerBajaDocumentos_Repositorio, ExtraerBajaDocumentos_Repositorio>();
builder.Services.AddScoped<IExtraerCabecera_Repositorio, ExtraerCabecera_Repositorio>();
builder.Services.AddScoped<IExtraerDetalles_Repositorio, ExtraerDetalle_Repositorio>();
builder.Services.AddScoped<IStoresProcedures_PostgreSQL_Repositorio, StoresProcedures_PostgreSQL_Repositorio>();
builder.Services.AddScoped<IStoresProcedures_SqlServer_Repositorio, StoresProcedures_SqlServer_Repositorio>();
builder.Services.AddScoped<IProcesarDatos_Repositorio, ProcesarDatos_Repositorio>();
builder.Services.AddScoped<IGenerarPdf, GenerarPdf>();

builder.Services.AddCors(p => p.AddPolicy("OpenCORSPolicy", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseCors(CORSOpenPolicy);

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ca4xml}/{action=Index}/{id?}");

app.Run();
