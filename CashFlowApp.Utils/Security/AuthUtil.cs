using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CashFlowApp.Utils.Security
{
    public static class AuthUtil
    {
        public static int GetUserIdFromContext(HttpContext context)
        {
            var userId = context.Items["UserId"] ?? throw new ValidationException("user id not found");
            return Convert.ToInt32(userId);
        }
    }
}
