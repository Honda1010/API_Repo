using AutoMapper;
using DomainLayer.Exceptions;
using DomainLayer.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceAbstractionLayer;
using Shared.Dtos.IdentityDtos;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
	public class AuthenticationService(UserManager<ApplicationUser> _userManager ,
		IConfiguration _configuration,
		IMapper _mapper) : IAuthenticationService
	{
		public async Task<bool> CheckEmailAsync(string email)
		{
			var user = await _userManager.FindByEmailAsync(email);
			return user is not null;
		}

		public async Task<UserDto> GetCurrentUserAsync(string email)
		{
			var user = await _userManager.FindByEmailAsync(email)?? throw new UserNotFoundException(email);
			return new UserDto() {
				Email = user.Email!,
				DisplayName = user.DisplayName,
				Token=await GenerateTokenAsync(user)
			};


		}

		public async Task<AddressDto> GetCurrentUserAddressAsync(string email)
		{
			var user = await _userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Email==email)?? throw new UserNotFoundException(email);

			if (user.Address is not null)
			{
				return _mapper.Map<AddressDto>(user.Address);
			}
			else {
				throw new AddressNotFoundException(user.UserName);
			}

		}
		public async Task<AddressDto> UpdateCurrentUserAddressAsync(string email, AddressDto address)
		{
			var user = await _userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Email == email) ?? throw new UserNotFoundException(email);
			if (user.Address is not null)
			{
				//update
				user.Address.FirstName = address.FirstName;
				user.Address.LastName = address.LastName;
				user.Address.City = address.City;
				user.Address.Country = address.Country;
				user.Address.Street = address.Street;
			}
			else { //add

				user.Address = _mapper.Map<Address>(address);
			}
			await _userManager.UpdateAsync(user);

			return _mapper.Map<AddressDto>(user.Address);

		}

		public async Task<UserDto> LoginServiceAsync(LoginDto loginDto)
		{
			//check email 
			var user =  await _userManager.FindByEmailAsync(loginDto.Email);
			if (user is null) throw new UserNotFoundException(loginDto.Email);
			//check password
			var isPasswordValid =  await _userManager.CheckPasswordAsync(user, loginDto.Password);
			if (isPasswordValid)
			{
				// return user dto with token
				var userDto = new UserDto
				{
					Email = user.Email!,
					DisplayName = user.DisplayName,
					Token = await GenerateTokenAsync(user) // Token generation logic should be implemented here
				};
				return userDto;
			}
			else
			{
				throw new UnAuthorizedException();
			}
		}

		public async Task<UserDto> RegisterServiceAsync(RegisterDto registerDto)
		{
			// map registerDto to ApplicationUser
			var user = new ApplicationUser
			{
				Email = registerDto.Email,
				UserName = registerDto.Email,
				DisplayName = registerDto.DisplayName,
				PhoneNumber = registerDto.PhoneNumber
			};
			// create user
			var result = await _userManager.CreateAsync(user, registerDto.Password);
			if (!result.Succeeded)
			{
				var errors = result.Errors.Select(e => e.Description).ToList();
				throw new BadRequestException(errors);

			}
			// return user dto with token
			var userDto = new UserDto
			{
				Email = user.Email!,
				DisplayName = user.DisplayName,
				Token = await GenerateTokenAsync(user) // Token generation logic should be implemented here
			};
			return userDto;
		}

		

		private async Task<string> GenerateTokenAsync(ApplicationUser user) {

			var Claims = new List<Claim>
			{
				new Claim(ClaimTypes.Email,user.Email!),
				new Claim(ClaimTypes.NameIdentifier,user.Id),
				new Claim(ClaimTypes.Name,user.DisplayName)
			};
			var roles = await _userManager.GetRolesAsync(user);
			foreach (var role in roles)
			{
				Claims.Add(new Claim(ClaimTypes.Role, role));
			}
			var SecretKey = _configuration["JWToptions:Key"];

			var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
			var credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);
			var Token = new JwtSecurityToken(
				issuer: _configuration["JWToptions:Issuer"],
				audience: _configuration["JWToptions:Audience"],
				claims: Claims,
				expires: DateTime.Now.AddHours(1),
				signingCredentials: credentials
				);
			return new JwtSecurityTokenHandler().WriteToken(Token);
		}
	}
}
