using OrientDb.Queries;

namespace OrientDb.Mutable
{
    public class IdentifierFactory
    {
        private int _counter;

        internal Identifier GetIdentifier(string prefix) => new Identifier($"{prefix}{++_counter}");
    }
}