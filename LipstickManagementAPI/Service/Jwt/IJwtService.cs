namespace HotPotToYou.Service.Jwt
{
    public interface IJwtService
    {
        string CreateToken(int ID, string roles);
    }
}
