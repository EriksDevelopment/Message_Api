namespace Message_Api.Extensions
{
    public static class AddCorsCollection
    {
        public static IServiceCollection AddCorsServices(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend",
                policy => policy.WithOrigins("http://localhost:5002") // frontend port
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            });
            return services;
        }
    }
}