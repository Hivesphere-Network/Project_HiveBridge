using Grpc.Core;
using Hive.Protocol;

namespace Project_HiveBridge.Services;

public class HiveVaultService : HiveHandshake.HiveHandshakeBase
{
    public override Task<HandshakeResponse> Handshake(HandshakeRequest request, ServerCallContext context)
    {
        return Task.FromResult(new HandshakeResponse
        {
            ServerVersion = "1.0.0",    
            ServerName = "HiveBridge"
        });  
    }
}