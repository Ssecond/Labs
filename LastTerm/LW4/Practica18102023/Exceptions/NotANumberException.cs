namespace Practica18102023.Exceptions
{
    internal class NotANumberException : Exception
    {
        internal NotANumberException() { }
        internal NotANumberException(string? message) : base(message) { }
    }
}
