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
    public const string CanViewAllConversations = "CanViewAllConversations";
    public const string CanViewAllConversationBlocks = "CanViewAllConversationBlocks";
    public const string CanViewAllConversationInvitations = "CanViewAllConversationInvitations";
    public const string CanViewAllDeletedMessages = "CanViewAllDeletedMessages";
    public const string CanViewAllMessages = "CanViewAllMessages";
    public const string CanViewAllMessageAttachments = "CanViewAllMessageAttachments";
    public const string CanViewAllMessageEmotes = "CanViewAllMessageEmotes";
    public const string CanViewAllParticipants = "CanViewAllParticipants";
    public const string CanViewAllUsers = "CanViewAllUsers";
}
