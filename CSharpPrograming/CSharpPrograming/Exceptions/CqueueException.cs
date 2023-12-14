namespace CSharpPrograming.Exceptions;

internal class CqueueException : Exception
{
    public CqueueException(string? message = "Ошибка при работе с очередью", Exception? innerException = null)
        : base(message, innerException)
    {
    }
}
