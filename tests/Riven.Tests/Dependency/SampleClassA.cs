namespace Riven.Dependency
{
    public partial class Registrar_And_Resolver_Tests
    {
        public class SampleClassA : ISampleClass, ITransientDependency
        {
            public string Name { get; set; }

            public string Call()
            {
                return nameof(SampleClassA);
            }
        }
    }
}
