using Grpc.Core;
using Grpc.Net.Client;
using Identity.Client.Models;
using Identity.Server.Grpc.Protos;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Identity.Authentication.Extension;

public static class AuthExtensions
{
    public static async Task<bool> CheckAccessAsync(this HttpContext context, string pageName, IConfiguration configuration)
    {
        var session = context.Request.Headers["Auth-Token"].ToString() ?? " ";
        GrpcChannel channel = GrpcChannel.ForAddress(configuration["Identity:Address:gRPC"]);
        Auth.AuthClient client = new(channel);

        Metadata headers = new();
        Application app = new(
            ApiKey: configuration["Identity:Application:ApiKey"],
            Key: configuration["Identity:Application:Key"]);
        headers.Add("application", JsonConvert.SerializeObject(app));

        CheckAccessReply checkAccess = await client.CheckAccessAsync(new()
        {
            Session = session,
            Address = context.Request.Path,
            PageName = pageName
        }, headers);
        return checkAccess.HasAccess;
    }
}
