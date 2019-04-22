using System.Threading.Tasks;
using WebApp.Data;

/// <summary>
/// Interface provides implementation of operation requiring email token confirmation
/// </summary>
public interface IConfirmationProvider
{
    Task ConfirmAsync(ApplicationUser user, string token, params object[] parameters);
}