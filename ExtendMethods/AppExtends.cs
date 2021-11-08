using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace App.ExtendMethods
{
    public static class AppExtends
    {
        public static void AddStatusCodePage(this IApplicationBuilder app)
        {
             app.UseStatusCodePages(appError => {
                appError.Run(async context => {
                    var response =  context.Response;
                    var code = response.StatusCode;
                    var content = @$"<html>
                        <head>
                            <meta charset='UTF-8' />
                            <title>Lỗi {code}</title>
                        </head>
                         <body>
                            <p style = 'color: red; font-size: 30px'>
                                có lỗi xảy ra rồi bạn ơi: {code} - {(HttpStatusCode)code}
                            </p>
                         </body>
                    </html>
                    ";

                    await response.WriteAsync(content);
                });


            }); // tạo ra lỗi từ response status 400 trở đi
        }
    }
}