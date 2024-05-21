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
                response.Headers.Append("secret-id", "256"); // ���������� ���������� ���������
                await response.WriteAsync("������ METANIT.COM");
            });
            app.MapGet("/cssstyle", async (context) =>
            {
                var response = context.Response;
                response.Headers.ContentLanguage = "ru-RU";
                response.ContentType = "text/html; charset=utf-8";
                response.Headers.Append("secret-id", "256"); // ���������� ���������� ���������

                var htmlContent = @"
        <!DOCTYPE html>
        <html lang='ru'>
        <head>
            <meta charset='UTF-8'>
            <title>������ METANIT.COM</title>
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
            <h1>������ METANIT.COM</h1>
            <p>��� ������ HTML-�������� � CSS-�������.</p>
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
                //var stringBuilder = new System.Text.StringBuilder("<h3>��������� ������ �������</h3><table>");
                //stringBuilder.Append("<tr><td>��������</td><td>��������</td></tr>");
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
