namespace App.API.Extensions
{
    public static class ConfigurePipelineExtensions
    {
        public static IApplicationBuilder UseConfigurePipelineExt(this WebApplication app)
        {
            app.UseExceptionHandler(x => { });
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/openapi/v1.json", "v1");
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            return app;
        }
    }
}