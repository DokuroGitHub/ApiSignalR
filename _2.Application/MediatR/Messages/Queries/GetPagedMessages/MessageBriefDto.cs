﻿using Application.Common.Mappings;
using Domain.Entities;

namespace Application.MediatR.Messages.Queries.GetPagedMessages;

#pragma warning disable 
public class MessageBriefDto : IMapFrom<Message>
{
    public int Id { get; init; }
    public int ConversationId { get; init; }
    public int CreatedBy { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
    public int? DeletedBy { get; init; }
    public DateTime? DeletedAt { get; init; }
    public string? Content { get; init; }
    public int? ReplyTo { get; init; }
}
