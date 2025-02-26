namespace ExpenseTracker.Core;

/// <summary>
/// Interface for repository.
/// </summary>
/// <typeparam name="TEntity">The type of the entity</typeparam>
public interface IRepository<TEntity> where TEntity : EntityBase
{
    /// <summary>
    /// Get all entities.
    /// </summary>
    /// <returns>Collection of entities</returns>
    Task<IEnumerable<TEntity>> GetAllAsync();

    /// <summary>
    /// Get entity by id.
    /// </summary>
    /// <param name="id">The id of the entity</param>
    /// <returns>The entity</returns>
    Task<TEntity?> GetByIdAsync(int id);

    /// <summary>
    /// Add entity.
    /// </summary>
    /// <param name="entity">The entity</param>
    /// <returns>The entity</returns>
    Task<TEntity?> AddAsync(TEntity entity);

    /// <summary>
    /// Update entity.
    /// </summary>
    /// <param name="entity">The entity</param>
    /// <returns>The entity</returns>
    Task<TEntity?> UpdateAsync(TEntity entity);

    /// <summary>
    /// Delete entity.
    /// </summary>
    /// <param name="id">The id of the entity</param>
    /// <returns>The entity</returns>
    Task<bool> DeleteAsync(int id);
}
