using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TaskBoardDemo.Source.Application.Ports.Input;
using TaskBoardDemo.Source.Application.Ports.Output;
using TaskBoardDemo.Source.Application.UseCases;
using TaskBoardDemo.Source.Infrastructure.Adapters.Output;
using TaskBoardDemo.Source.Infrastructure.Adapters.Output.Context;
using TaskBoardDemo.Source.Infrastructure.Adapters.Output.Repository;
using TaskBoardDemo.Source.Infrastructure.Adapters.Output.Repository.impl;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddRazorPages();


builder.Services.AddDbContext<TaskBoardDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IUserOutputPort, UserOutputAdapter>();
builder.Services.AddScoped<IUserUseCase, UserInputPort>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<ITaskOutputPort, TaskOutputAdapter>();
builder.Services.AddScoped<ITaskUseCase, TaskInputPort>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();


builder.Services.Configure<RouteOptions>(options =>
{
    options.ConstraintMap["regex"] = typeof(RegexInlineRouteConstraint);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Mi API",
        Version = "v1"
    });
});

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(c => { c.RouteTemplate = "api/v1/swagger/{documentName}/swagger.json"; });

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/api/v1/swagger/v1/swagger.json", "Mi API v1");
        c.RoutePrefix = "api/v1";
    });
}
app.UseStaticFiles(new StaticFileOptions {
    OnPrepareResponse = ctx => {
        if (app.Environment.IsDevelopment()) {
            ctx.Context.Response.Headers["Cache-Control"] = "no-cache, no-store, max-age=0, must-revalidate";
            ctx.Context.Response.Headers["Pragma"] = "no-cache";
            ctx.Context.Response.Headers["Expires"] = "0";
        }
    }
});

app.MapControllers();
app.MapRazorPages();
app.Run();