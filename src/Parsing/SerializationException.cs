using System;

namespace WallyMapSpinzor2;

[Serializable]
public class SerializationException : Exception
{
    public SerializationException() { }
    public SerializationException(string message) : base(message) { }
    public SerializationException(string message, Exception inner) : base(message, inner) { }
}