using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;
using System.Net;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContext;

        public UserController(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            string ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();
            string userAgent = HttpContext.Request.Headers["User-Agent"].ToString();
            string acceptLanguage = HttpContext.Request.Headers["Accept-Language"].ToString();
            string referer = HttpContext.Request.Headers["Referer"].ToString();
            string cookies = HttpContext.Request.Headers["Cookie"].ToString();
            string ipAddress2 = HttpContext.Request.Headers["X-Forwarded-For"];

            var ipAddress3 = _httpContext.GetIpAddress();


            if (string.IsNullOrEmpty(ipAddress2))
            {
                ipAddress2 = HttpContext.Connection.RemoteIpAddress?.ToString();
            }

            string machineIp = string.Empty;
            string machineName = string.Empty;
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                machineName = host.HostName;
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    machineIp = ip.ToString();
                }
            }

            var info = new
            {
                IpAddress = ipAddress,
                IpAddress2 = ipAddress2,
                IpAddress3 = ipAddress3,
                //MachineName = machineName,
                //MachineIp = machineIp,
                UserAgent = userAgent,
                AcceptLanguage = acceptLanguage,
                Referer = referer,
                Cookies = cookies
            };

            return Ok(info);
        }
    }
}
