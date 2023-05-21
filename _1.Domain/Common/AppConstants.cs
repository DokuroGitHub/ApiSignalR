namespace Domain.Common;

public static class RoleNames
{
    public const string User = nameof(User);
    public const string Admin = nameof(Admin);
}

public class PolicyNames
{
    public const string FullAccess = nameof(FullAccess);
    public const string AdminOnly = nameof(AdminOnly);
    public const string CanViewAllConversations = nameof(CanViewAllConversations);
    public const string CanViewAllConversationBlocks = nameof(CanViewAllConversationBlocks);
    public const string CanViewAllConversationInvitations = nameof(CanViewAllConversationInvitations);
    public const string CanViewAllDeletedMessages = nameof(CanViewAllDeletedMessages);
    public const string CanViewAllMessages = nameof(CanViewAllMessages);
    public const string CanViewAllMessageAttachments = nameof(CanViewAllMessageAttachments);
    public const string CanViewAllMessageEmotes = nameof(CanViewAllMessageEmotes);
    public const string CanViewAllParticipants = nameof(CanViewAllParticipants);
    public const string CanViewAllSampleUsers = nameof(CanViewAllSampleUsers);
    public const string CanViewAllUsers = nameof(CanViewAllUsers);
}
