using System.Threading.Tasks;
using WebApp.Data;

/// <summary>
/// Interface provides token for sended in email in email confirmation operation.
/// </summary>
public interface IConfirmationTokenProvider
{
    Task<string> GenerateTokenAsync(ApplicationUser user);
}