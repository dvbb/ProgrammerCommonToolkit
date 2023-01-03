using ProgrammerToolkit.Core.Errors;
using ProgrammerToolkitBackend.IProvider;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Text;

namespace ProgrammerToolkitBackend.Provider
{
    public class WebToolsProvider: IWebToolsProvider
    {
        public async Task<string> GetWebTools(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            try
            {
                var claims = handler.ReadToken(token);
                return claims.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("Invalid format.",ex);
            }
        }
    }
}
