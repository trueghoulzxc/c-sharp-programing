namespace CSharpPrograming.Exceptions;

internal class SaveCqueueException : CqueueException
{
    public SaveCqueueException(string? message = "Ошибка при сохранении очереди", Exception? innerException = null)
        : base(message, innerException)
    {
    }
}
