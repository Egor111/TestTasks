namespace TransactionService.WebHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<FinanceDbContext>(options =>
               options.UseNpgsql("Your PostgreSQL Connection String"));
            builder.Services.AddLogging();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseAuthorization();


            app.MapControllers();

            app.Run();


            //// Program Setup
            //var builder = WebApplication.CreateBuilder(args);

            //builder.Services.AddControllers();
           

            //var app = builder.Build();

            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            //app.MapControllers();

            //app.Run();
        }
    }
}
