namespace OrientDb.Tests.Model
{
    public class Ecc: E<Vc,Vc>
    {
        public Ecc(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}