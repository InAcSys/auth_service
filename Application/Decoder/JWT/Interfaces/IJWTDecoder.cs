using AuthService.Domain.ObjectValue;

namespace AuthService.Application.Decoders.JWT
{
    public interface IJWTDecoder
    {
        JWTBody Decoder(string jwt);
    }
}
