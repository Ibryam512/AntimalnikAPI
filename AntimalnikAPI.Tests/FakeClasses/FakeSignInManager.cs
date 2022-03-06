using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using AntimalnikAPI.Data;
using AntimalnikAPI.Models;

namespace AntimalnikAPI.Tests.FakeClasses {
	public class FakeSignInManager : SignInManager<ApplicationUser> {
		public FakeSignInManager(ApplicationDbContext context) : base(
			new FakeUserManager(context),
			new Mock<IHttpContextAccessor>().Object,
			new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>().Object,
			new Mock<IOptions<IdentityOptions>>().Object,
			new Mock<ILogger<SignInManager<ApplicationUser>>>().Object,
			new Mock<IAuthenticationSchemeProvider>().Object,
			new Mock<DefaultUserConfirmation<ApplicationUser>>().Object
		) {}
	}
}
