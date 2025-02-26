namespace ExpenseTracker.Core.Services;

/// <summary>
/// A base class for service responses.
/// </summary>
public abstract class ServiceResponseBase
{
    /// <summary>
    /// A flag to indicate if the service was successful.
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// The error message if the service was not successful.
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// A list of errors if the service was not successful.
    /// </summary>
    public List<string> Errors { get; set; }

    /// <summary>
    /// A generic class to hold the response from a service.
    /// </summary>
    public ServiceResponseBase()
    {
        Errors = [];
    }
}

/// <summary>
/// A generic class to hold the response from a service.
/// </summary>
/// <typeparam name="T">
/// The type of the data that the service will return.
/// </typeparam>
public class ServiceResponse<T> : ServiceResponseBase
{
    /// <summary>
    /// The data that the service will return.
    /// </summary>
    public T? Result { get; set; }

    public static ServiceResponse<T> Ok(T result)
    {
        return new ServiceResponse<T>
        {
            Success = true,
            Result = result
        };
    }

    public static ServiceResponse<T> Error(string errorMessage)
    {
        return new ServiceResponse<T>
        {
            Success = false,
            ErrorMessage = errorMessage
        };
    }
}

/// <summary>
/// A class to hold the response from a service.
/// </summary>
public class ServiceResponse : ServiceResponseBase
{
    public static ServiceResponse Ok()
    {
        return new ServiceResponse
        {
            Success = true
        };
    }

    public static ServiceResponse Error(string errorMessage)
    {
        return new ServiceResponse
        {
            Success = false,
            ErrorMessage = errorMessage
        };
    }
}
