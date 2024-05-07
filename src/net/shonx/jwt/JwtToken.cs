namespace net.shonx.jwt;
public class JwtToken(string Token, JwtHeader Header, JwtPayload Payload, string Signature)
{
    internal string Token { get; } =  !string.IsNullOrEmpty(Token) ? Token : throw new NullReferenceException();
    public JwtHeader Header { get; } = Header ?? throw new NullReferenceException();
    public JwtPayload Payload { get; } = Payload ?? throw new NullReferenceException();
    public string Signature { get; } =  !string.IsNullOrEmpty(Signature) ? Signature : throw new NullReferenceException();

}