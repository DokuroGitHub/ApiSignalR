namespace Domain.Common;

public static class RoleNames
{
    public const string User = "User";
    public const string Admin = "Admin";
}

public class PolicyNames
{
    public const string FullAccess = "FullAccess";
    public const string AdminOnly = "AdminOnly";
    public const string CanViewAllUsers = "CanViewAllUsers";
    public const string CanViewAllConversations = "CanViewAllConversations";
}