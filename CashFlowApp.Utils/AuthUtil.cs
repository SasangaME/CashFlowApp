using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowApp.Utils
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
