using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Microsoft.IdentityModel.Tokens;

namespace iPortal.Exceptions
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // Allow the request to continue
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex); // Handle exception if one occurs
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            var message = "Lỗi hệ thống: " + exception.Message;

            switch (exception)
            {
                case BadHttpRequestException:
                    statusCode = HttpStatusCode.BadRequest;
                    message = "Lỗi request không hợp lệ";
                    break;
                case SecurityTokenExpiredException:
                    statusCode = HttpStatusCode.Unauthorized;
                    message = "Phiên đăng nhập đã hết hạn";
                    break;
                case SecurityTokenException:
                    statusCode = HttpStatusCode.Unauthorized;
                    message = "Token không hợp lệ";
                    break;
                case UnauthorizedAccessException:
                    statusCode = HttpStatusCode.Unauthorized;
                    message = "Lỗi xác thực: " + exception.Message;
                    break;
                case ArgumentException:
                    statusCode = HttpStatusCode.BadRequest;
                    message = "Lỗi nhập liệu: " + exception.Message;
                    break;
                case InvalidOperationException ex when ex.Message.Contains("username"):
                    statusCode = HttpStatusCode.Conflict;
                    message = "Lỗi: Username đã tồn tại!";
                    break;
                case DbUpdateException dbEx when dbEx.InnerException is MySqlException sqlEx &&
                                                sqlEx.Message.Contains("Duplicate entry"):
                    statusCode = HttpStatusCode.Conflict;
                    message = "Lỗi: Dữ liệu đã tồn tại trong cơ sở dữ liệu, vui lòng kiểm tra lại!";
                    break;
                case NotSupportedException:
                    statusCode = HttpStatusCode.MethodNotAllowed;
                    message = "Phương thức yêu cầu không hỗ trợ";
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var result = JsonSerializer.Serialize(new { error = message });
            return context.Response.WriteAsync(result);
        }
    }
}
