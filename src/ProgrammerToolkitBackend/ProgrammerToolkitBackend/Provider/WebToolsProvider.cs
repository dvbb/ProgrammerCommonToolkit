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

            var bytes=Convert.FromBase64String(token);
            var decodedStr=Encoding.UTF8.GetString(bytes);
            return decodedStr;
        }
    }
}
