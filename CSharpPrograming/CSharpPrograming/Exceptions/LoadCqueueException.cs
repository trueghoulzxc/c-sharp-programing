namespace CSharpPrograming.Exceptions;

internal class LoadCqueueException : CqueueException
{
    public LoadCqueueException(string? message = "Возникла ошибка при загрузке множества", Exception? innerException = null)
        : base(message, innerException)
    {
    }
}
