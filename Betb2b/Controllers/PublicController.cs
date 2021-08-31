using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using B2B.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Betb2b.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class PublicController : ControllerBase
	{
		private readonly IUserService _userService;

		public PublicController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpGet("[action]")]
		[Produces("text/html")]
		public ContentResult UserInfo([FromQuery(Name = "id")] int id)
		{
			var user = _userService.GetUserInfo(id);
			return base.Content(UserHtmlTemplate.GetUserHtmlResponse(user), "text/html");
		}
	}
}
