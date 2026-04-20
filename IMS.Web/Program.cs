using IMS.Application.Inventories.Interfaces;
using IMS.Application.Inventories;
using IMS.InMemory;
using IMS.Web.Components;
using IMS.Application.Products.Interfaces;
using IMS.Application.Products;
using IMS.Application.Activities.Interfaces;
using IMS.Application.Activities;
using IMS.Application.Reports.Interfaces;
using IMS.Application.Reports;
using Microsoft.EntityFrameworkCore;
using IMS.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Db Context
builder.Services.AddPersistence(builder.Configuration.GetConnectionString("LocalConnection")!);

// Inventory DIs
builder.Services.AddSingleton<IInventoryRepository, InventoryRepository>();
builder.Services.AddTransient<IViewInventoriesByNameUseCase, ViewInventoriesByNameUseCase>();
builder.Services.AddTransient<IAddInventoryUseCase, AddInventoryUseCase>();
builder.Services.AddTransient<IEditInventoryUseCase, EditInventoryUseCase>();
builder.Services.AddTransient<IViewInventoryByIdUseCase, ViewInventoryByIdUseCase>();
builder.Services.AddTransient<IDeleteInventoryUseCase, DeleteInventoryUseCase>();
builder.Services.AddTransient<ISearchInventoryTransactionUseCase, SearchInventoryTransactionUseCase>();

// Product DIs
builder.Services.AddSingleton<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IViewProductsByNameUseCase, ViewProductsByNameUseCase>();
builder.Services.AddTransient<IAddProductUseCase, AddProductUseCase>();
builder.Services.AddTransient<IEditProductUseCase, EditProductUseCase>();
builder.Services.AddTransient<IViewProductByIdUseCase, ViewProductByIdUseCase>();
builder.Services.AddTransient<IDeleteProductUseCase, DeleteProductUseCase>();
builder.Services.AddTransient<IProduceProductUseCase, ProduceProductUseCase>();
builder.Services.AddTransient<ISellProductUseCase, SellProductUseCase>();
builder.Services.AddTransient<ISearchProductTransactionUseCase, SearchProductTransactionUseCase>();

// Activity DIs
builder.Services.AddSingleton<IInventoryTransactionRepository, InventoryTransactionRepository>();
builder.Services.AddTransient<IPurchaseInventoryUseCase, PurchaseInventoryUseCase>();

// Purchase DIs
builder.Services.AddSingleton<IProductTransactionRepository, ProductTransactionRepository>();

var app = builder.Build();

// Database initializer
await DatabaseInitializer.MigrateAsync(app.Services);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
