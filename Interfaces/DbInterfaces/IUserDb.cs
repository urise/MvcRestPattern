//=========================================================================
// THIS IS AUTOMATICALLY GENERATED FILE! PLEASE DON'T CHANGE IT MANUALLY!
//=========================================================================
using System;

namespace Interfaces.DbInterfaces
{
	public interface IUser
	{
		int UserId { get; set; }
		string Login { get; set; }
		string Password { get; set; }
		string Email { get; set; }
		string UserFio { get; set; }
		string RegistrationCode { get; set; }
		bool IsActive { get; set; }
	}
}
