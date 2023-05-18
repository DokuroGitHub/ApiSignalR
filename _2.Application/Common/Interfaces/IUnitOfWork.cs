using Application.Common.Interfaces.IRepositories;
using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IUnitOfWork : IDisposable
{
    #region repositories
    public IConversationBlockRepository ConversationBlockRepository { get; }
    public IConversationInvitationRepository ConversationInvitationRepository { get; }
    public IConversationRepository ConversationRepository { get; }
    public IDeletedMessageRepository DeletedMessageRepository { get; }
    public IMessageAttachmentRepository MessageAttachmentRepository { get; }
    public IMessageEmoteRepository MessageEmoteRepository { get; }
    public IMessageRepository MessageRepository { get; }
    public IParticipantRepository ParticipantRepository { get; }
    public ITodoListRepository TodoListRepository { get; }
    public ITodoItemRepository TodoItemRepository { get; }
    public IUserRepository UserRepository { get; }
    #endregion repositories

    // state
    EntityState State<TEntity>(TEntity entity) where TEntity : BaseEntity;

    #region save changes
    // int SaveChanges(); // not using bc mediatR events need async
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    #endregion save changes

    #region transaction

    #region begin transaction
    void BeginTransaction();
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    #endregion begin transaction

    #region commit
    void Commit();
    Task CommitAsync(CancellationToken cancellationToken = default);
    #endregion commit

    #region rollback
    void Rollback();
    Task RollbackAsync(CancellationToken cancellationToken = default);
    #endregion rollback

    #region execute transaction
    void ExecuteTransaction(Action work);
    T ExecuteTransaction<T>(Func<T> work);
    Task ExecuteTransactionAsync(
        Func<Task> work,
        CancellationToken cancellationToken = default);
    Task<T> ExecuteTransactionAsync<T>(
        Func<Task<T>> work,
        CancellationToken cancellationToken = default);
    #endregion execute transaction

    #endregion transaction
}
