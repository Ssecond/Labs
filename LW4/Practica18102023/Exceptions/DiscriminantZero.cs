namespace Practica18102023.Exceptions
{
    internal class DiscriminantZero : Exception
    {
        internal DiscriminantZero() { }
        internal DiscriminantZero(string? message) : base(message) { }
    }
}
