namespace Message_Api.Extensions
{
    public static class AddCorsCollection
    {
        public static IServiceCollection AddCorsServices(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowBlazor",
                policy => policy
                    .WithOrigins("http://localhost:5012") // URL för Blazor WASM app
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            });
            return services;
        }
    }
}