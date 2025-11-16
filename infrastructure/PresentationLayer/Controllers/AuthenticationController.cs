using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstractionLayer;
using Shared.Dtos.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AuthenticationController(IServiceManager _serviceManager):ControllerBase
	{
		[HttpPost("login")]
		public async Task<ActionResult<UserDto>> LoginAsync(LoginDto loginDto)
		{
			var user = await _serviceManager.AuthenticationService.LoginServiceAsync(loginDto);
			return Ok(user);
		}
		[HttpPost("register")]
		public async Task<ActionResult<UserDto>> RegisterAsync(RegisterDto registerDto)
		{
			var user = await _serviceManager.AuthenticationService.RegisterServiceAsync(registerDto);
			return Ok(user);
		}

		[HttpGet("CheckEmail")]
		public async Task<ActionResult<bool>> CheckEmail(string email) {
			var Result = await _serviceManager.AuthenticationService.CheckEmailAsync(email);
			return Ok(Result);
		}
		[Authorize]
		[HttpGet("CurrentUser")]
		public async Task<ActionResult<UserDto>> GetCurrentUser() {
			var email = User.FindFirstValue(ClaimTypes.Email);
			var user = await _serviceManager.AuthenticationService.GetCurrentUserAsync(email!);
			return Ok(user);
		}
		[Authorize]
		[HttpGet("CurrentAddress")]
		public async Task<ActionResult<AddressDto>> GetCurrentUserAddress() {
			var email = User.FindFirstValue(ClaimTypes.Email);
			var Address = _serviceManager.AuthenticationService.GetCurrentUserAddressAsync(email!);
			return Ok(Address);
		}
		[Authorize]
		[HttpGet("UpdateAddress")]
		public async Task<ActionResult<AddressDto>> UpdateCurrentUserAddress(AddressDto address) {
			var email = User.FindFirstValue(ClaimTypes.Email);
			var UpdateAddress=_serviceManager.AuthenticationService.UpdateCurrentUserAddressAsync(email!, address);
			return Ok(UpdateAddress);
		}
	}
}
