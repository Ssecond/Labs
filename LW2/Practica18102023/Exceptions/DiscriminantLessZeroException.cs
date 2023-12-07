using System.Text.Json;

namespace Practica18102023.Exceptions
{
    internal class DiscriminantLessZeroException : Exception
    {
        internal DiscriminantLessZeroException() { }
        internal DiscriminantLessZeroException(string? message) : base(message) { }
    }
}
