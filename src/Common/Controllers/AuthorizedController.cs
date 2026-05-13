using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace nebula.api.src.Common.Controllers
{
    public abstract class AuthorizedController : ControllerBase
    {
        protected Guid? GetUserId()
        {
            var value = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.TryParse(value, out var id) ? id : null;
        }
    }
}
