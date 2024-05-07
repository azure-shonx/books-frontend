// namespace net.shonx.test;

// using net.shonx.jwt;

// internal static class TestToken
// {
//     private static readonly string TOKEN = File.ReadAllText("token.txt");
//     internal static void Run()
//     {
//         JwtToken? token = null;
//         try
//         {
//             token = TokenCreator.Create(TOKEN);
//         }
//         catch (NullReferenceException e)
//         {
//             Console.WriteLine(e.ToString());
//         }
//         if (token is null)
//         {
//             Console.WriteLine("[FAIL] Bad Token.");
//         }
//         else
//         {
//             if (string.IsNullOrEmpty(token.Token))
//             {
//                 Console.WriteLine("[FAIL] token.Token");
//                 return;
//             }
//             if (token.Header is null)
//             {
//                 Console.WriteLine("[FAIL] token.Header");
//                 return;
//             }
//             if (token.Payload is null)
//             {
//                 Console.WriteLine("[FAIL] token.Payload");
//                 return;
//             }
//             if (string.IsNullOrEmpty(token.Signature))
//             {
//                 Console.WriteLine("[FAIL] token.Signature");
//                 return;
//             }
//             Console.WriteLine("[OK] Token looks good.");
//         }
//     }
// }