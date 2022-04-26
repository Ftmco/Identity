namespace Identity.Server.Grpc.ServiceResponse;

public static class UserResponse
{
    public static GetUserReply Success()
    {
        return new();
    }

    public static GetUserReply NotFound()
    {
        return new();
    }

    public static GetUserReply Exception()
    {
        return new();
    }
}
