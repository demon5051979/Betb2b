using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using B2B.Domain;
using B2B.Domain.Dto;
using B2B.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Betb2b.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class AuthController : ControllerBase
	{
		private readonly ILogger<AuthController> _logger;
		private readonly IUserService _userService;

		public AuthController(ILogger<AuthController> logger, IUserService userService)
		{
			_logger = logger;
			_userService = userService;
		}

		[Produces("application/xml")]
		[HttpPost("[action]")]
		[Authorize]
		public IActionResult CreateUser(User user)
		{
			user = _userService.CreateUser(user);
			return Ok(new UserResponse() { User = user, Msg = "User created" });
		}

		[HttpPost("[action]")]
		[Authorize]
		public IActionResult RemoveUser(User user)
		{
			user = _userService.RemoveUser(user);
			return Ok(new UserResponse() { User = user, Msg = "User was removed" });
		}

		[HttpPost("[action]")]
		[Authorize]
		public IActionResult SetStatus([FromForm] User user)
		{
			user = _userService.SetStatus(user);
			return Ok(new UserResponse() { User = user, Msg = "User status has been changed" });
		}
	}
}
