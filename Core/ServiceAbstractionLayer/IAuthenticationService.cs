using Shared.Dtos.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstractionLayer
{
	public interface IAuthenticationService
	{
		Task<UserDto> LoginServiceAsync(LoginDto loginDto);
		Task<UserDto> RegisterServiceAsync(RegisterDto registerDto);

		Task<bool> CheckEmailAsync(string email);

		Task<UserDto> GetCurrentUserAsync(string email);

		Task<AddressDto> GetCurrentUserAddressAsync(string email);

		Task<AddressDto> UpdateCurrentUserAddressAsync(string email, AddressDto address);

	}
}
