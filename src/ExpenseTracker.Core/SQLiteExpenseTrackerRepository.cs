using System.Data.SQLite;
using System.Text;
using Dapper;

namespace ExpenseTracker.Core;

public sealed class SqLiteExpenseTrackerRepository : IExpenseRepository
{
    private readonly SQLiteConnection _connection;

    public SqLiteExpenseTrackerRepository(string sqliteFilePath)
    {
        // Create db file if it doesn't exist.
        if (!File.Exists(sqliteFilePath))
        {
            SQLiteConnection.CreateFile(sqliteFilePath);
        }

        // Create the connection.
        SQLiteConnectionStringBuilder connectionStringBuilder = new()
        {
            DataSource = sqliteFilePath,
            Version = 3,
            ForeignKeys = true
        };
        _connection = new(connectionStringBuilder.ConnectionString);

        // Create the table if it doesn't exist.
        _connection.Execute(@"
            CREATE TABLE IF NOT EXISTS Expense (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                CreatedAt TEXT NOT NULL,
                UpdatedAt TEXT,
                Description TEXT NOT NULL,
                Amount REAL NOT NULL,
                IsDeleted BIT NOT NULL DEFAULT 0
            );
        ");
    }

    #region IExpenseRepository Implementation

    /// <inheritdoc/>
    public Task<IEnumerable<ExpenseEntity>> GetAllAsync()
    {
        List<ExpenseEntity> expenses = _connection
            .Query<ExpenseEntity>("SELECT Id, CreatedAt, UpdatedAt, Description, Amount, IsDeleted FROM Expense WHERE IsDeleted=0;")
            .ToList();
        return Task.FromResult((IEnumerable<ExpenseEntity>)expenses);
    }

    /// <inheritdoc/>
    public async Task<ExpenseEntity?> GetByIdAsync(int id)
    {
        return await _connection
             .QueryFirstOrDefaultAsync<ExpenseEntity?>("SELECT Id, CreatedAt, UpdatedAt, Description, Amount, IsDeleted FROM Expense WHERE Id=@id AND IsDeleted=0;", new { id });
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<ExpenseEntity>> GetAllAsync(int? day, int? month, int? year)
    {
        StringBuilder queryBuilder = new();
        queryBuilder.Append("SELECT Id, CreatedAt, UpdatedAt, Description, Amount, IsDeleted FROM Expense WHERE IsDeleted=0");

        DynamicParameters parameters = new();

        if (day.HasValue)
        {
            queryBuilder.Append(" AND CAST(strftime('%d', CreatedAt) AS INT) = @day");
            parameters.Add("@day", day);
        }
        if (month.HasValue)
        {
            queryBuilder.Append(" AND CAST(strftime('%m', CreatedAt) AS INT) = @month");
            parameters.Add("@month", month);
        }
        if (year.HasValue)
        {
            queryBuilder.Append(" AND CAST(strftime('%Y', CreatedAt) AS INT) = @year");
            parameters.Add("@year", year);
        }
        queryBuilder.Append(';');

        return await _connection.QueryAsync<ExpenseEntity>(queryBuilder.ToString(), parameters);
    }

    /// <inheritdoc/>
    public async Task<ExpenseEntity?> AddAsync(ExpenseEntity entity)
    {
        entity.IsDeleted = false;
        int id = await _connection.QuerySingleAsync<int>(@"
            INSERT INTO Expense (CreatedAt, Description, Amount, IsDeleted) VALUES (CURRENT_TIMESTAMP, @Description, @Amount, @IsDeleted);
            SELECT last_insert_rowid();", entity);
        entity.Id = id;
        return entity;
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(int id)
    {
        int affectedRows = await _connection.ExecuteAsync("UPDATE Expense SET IsDeleted=1 WHERE Id=@id;", new { id });
        return affectedRows > 0;
    }

    /// <inheritdoc/>
    public async Task<ExpenseEntity?> UpdateAsync(ExpenseEntity entity)
    {
        entity.UpdatedAt = DateTime.Now;
        int affectedRows = await _connection.ExecuteAsync(@"
            UPDATE Expense SET Description=@Description, Amount=@Amount, UpdatedAt=@UpdatedAt, IsDeleted=@IsDeleted WHERE Id=@Id", entity);
        return affectedRows > 0 ? entity : null;
    }

    #endregion
}
