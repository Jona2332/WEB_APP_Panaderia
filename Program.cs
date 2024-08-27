using WEB_APP_Panaderia.Interfaces;
using WEB_APP_Panaderia.Models;
using WEB_APP_Panaderia.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddScoped<IUsuariosModel, UsuariosModel>();
builder.Services.AddScoped<IProveedoresModel, ProveedoresModel>();
builder.Services.AddScoped<IUsuariosRolesModel, UsuariosRolesModel>();
builder.Services.AddScoped<IRegistroDesechosModel, RegistroDesechosModel>();
builder.Services.AddScoped<IProductosModel, ProductosModel>();
builder.Services.AddScoped<ISaboresPizzaModel, SaboresPizzaModel>();
builder.Services.AddScoped<IOrdenPDVModel, OrdenPDVModel>();
builder.Services.AddScoped<ILogsModel, LogsModel>();
builder.Services.AddScoped<IInsumosModel, InsumosModel>();
builder.Services.AddScoped<IMetricasModel, MetricasModel>();
builder.Services.AddScoped<IContabilidadModel, ContabilidadModel>();
builder.Services.AddScoped<ICotizacionesModel, CotizacionModel>();
builder.Services.AddScoped<IPdfProcessingService, PdfProcessingService>();
builder.Services.AddScoped<GeminiService>();
builder.Services.AddHttpClient<IApiService, WEB_APP_Panaderia.Models.ApiService>(client =>
{
	client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"]);
})
.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
{
	AllowAutoRedirect = false,
	UseDefaultCredentials = true,
});

builder.Services.AddScoped<IApiService>(sp =>
{
	var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
	var httpClient = httpClientFactory.CreateClient(nameof(IApiService));
	var baseUrl = builder.Configuration["ApiSettings:BaseUrl"];
	return new WEB_APP_Panaderia.Models.ApiService(httpClient, baseUrl);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}
else
{
	app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
