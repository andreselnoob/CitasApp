using CitasApp.Entities;

namespace CitasApp.Interfaces;

public interface ITokenService
{

    string CreateToken(AppUser user);

}
