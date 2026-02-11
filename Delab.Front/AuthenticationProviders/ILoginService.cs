namespace Delab.Front.AutheticationProviders;

public interface ILoginService
{
    Task LoginAsync(string token);

    Task LogoutAsync();
}
