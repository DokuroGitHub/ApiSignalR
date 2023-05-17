namespace Domain.Common;

public static class UserRoleExtensions
{
    public static string ToStringValue(this UserRole val)
    {
        switch (val)
        {
            case UserRole.Admin:
                return "admin";
            case UserRole.User:
            default:
                return "user";
        }
    }

    public static string? ToStringValueOrDefault(this UserRole val)
    {
        switch (val)
        {
            case UserRole.Admin:
                return "admin";
            case UserRole.User:
                return "user";
            default:
                return null;
        }
    }

    public static UserRole ToUserRole(this string? val)
    {
        switch (val)
        {
            case "admin":
                return UserRole.Admin;
            case "user":
            default:
                return UserRole.User;
        }
    }

    public static UserRole? ToUserRoleOrDefault(this string? val)
    {
        switch (val)
        {
            case "admin":
                return UserRole.Admin;
            case "user":
                return UserRole.User;
            default:
                return null;
        }
    }
}
