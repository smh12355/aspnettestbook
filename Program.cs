namespace WebApplication1
{
    public class Program
    {
        async static Task HandleRequest(HttpContext context)
        {
            await context.Response.WriteAsync("Hello oleg");
        }
        async static Task dosome(HttpContext context, int x)
        {
            x = x * 2;
            await context.Response.WriteAsync($"Result:{x}");
        }
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
  
            var app = builder.Build();
            //app.Run(async (context) =>
            //{
            //    x = x * 2;  //  2 * 2 = 4
            //    await context.Response.WriteAsync($"Result: {x}");
            //});
            app.MapGet("/", async (context) =>
            {
                int x = 2;
                x = x * 2;  // 2 * 2 = 4
                await context.Response.WriteAsync($"Result: {x}");
            });
            app.MapGet("/dosome", async (context) =>
            {
                int x = 3;  // Example value
                await dosome(context, x);
            });
            app.MapGet("/REDIRECT", async (context) =>
            {
                context.Response.Redirect("https://github.com/");
                await context.Response.CompleteAsync();
            });
            app.MapGet("/headers", async (context) =>
            {
                var response = context.Response;
                response.Headers.ContentLanguage = "ru-RU";
                response.ContentType = "text/plain; charset=utf-8";
                response.Headers.Append("secret-id", "256"); // Добавление кастомного заголовка
                await response.WriteAsync("Привет METANIT.COM");
            });
            app.MapGet("/cssstyle", async (context) =>
            {
                var response = context.Response;
                response.Headers.ContentLanguage = "ru-RU";
                response.ContentType = "text/html; charset=utf-8";
                response.Headers.Append("secret-id", "256"); // Добавление кастомного заголовка

                var htmlContent = @"
        <!DOCTYPE html>
        <html lang='ru'>
        <head>
            <meta charset='UTF-8'>
            <title>Привет METANIT.COM</title>
            <style>
                body {
                    font-family: Arial, sans-serif;
                    background-color: #f0f0f0;
                    color: #333;
                }
                h1 {
                    color: #007acc;
                }
            </style>
        </head>
        <body>
            <h1>Привет METANIT.COM</h1>
            <p>Это пример HTML-контента с CSS-стилями.</p>
        </body>
        </html>";

                await response.WriteAsync(htmlContent);
            });
            app.MapGet("/testrequest", async (context) =>
            {
                //context.Response.ContentType = "text/html; charset=utf-8";
                //var stringBuilder = new System.Text.StringBuilder("<table>");

                //foreach (var header in context.Request.Headers)
                //{
                //    stringBuilder.Append($"<tr><td>{header.Key}</td><td>{header.Value}</td></tr>");
                //}
                //stringBuilder.Append("</table>");
                //await context.Response.WriteAsync(stringBuilder.ToString());


                //var acceptHeaderValue = context.Request.Headers.ContentType;
                //await context.Response.WriteAsync($"Accept: {acceptHeaderValue}");

                //await context.Response.WriteAsync($"Path: {context.Request.Path}");

                //context.Response.ContentType = "text/html; charset=utf-8";
                //await context.Response.WriteAsync($"<p>Path: {context.Request.Path}</p>" +
                //    $"<p>QueryString: {context.Request.QueryString}</p>");

                //context.Response.ContentType = "text/html; charset=utf-8";
                //var stringBuilder = new System.Text.StringBuilder("<h3>Параметры строки запроса</h3><table>");
                //stringBuilder.Append("<tr><td>Параметр</td><td>Значение</td></tr>");
                //foreach (var param in context.Request.Query)
                //{
                //    stringBuilder.Append($"<tr><td>{param.Key}</td><td>{param.Value}</td></tr>");
                //}
                //stringBuilder.Append("</table>");
                //await context.Response.WriteAsync(stringBuilder.ToString());

                string name = context.Request.Query["name"];
                string age = context.Request.Query["age"];
                await context.Response.WriteAsync($"{name} - {age}");
            });

            app.Run();
        }
    }
}
