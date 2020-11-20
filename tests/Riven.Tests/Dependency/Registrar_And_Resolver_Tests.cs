using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using RivenTestsBase;

using Shouldly;

using Xunit;

namespace Riven.Dependency
{
    public partial class Registrar_And_Resolver_Tests : TestBaseWithLocalIocManager
    {

        [Fact]
        public void Should_RegisterAssembly()
        {
            this.Services.RegisterAssembly(this.GetType().Assembly);

            this.Build();

            var sampleClass = this.ServiceProvider.GetService<ISampleClass>();
            var valA = nameof(SampleClassA);
            Assert.Equal(sampleClass.Call(), valA);

        }


        [Fact]
        public void Should_LifeStyle_Transient()
        {
            this.Services.RegisterAssembly(this.GetType().Assembly);
            this.Build();

            var sampleClass1 = this.ServiceProvider.GetService<ISampleClass>();

            var sampleClass2 = this.ServiceProvider.GetService<ISampleClass>();

            sampleClass1.ShouldNotBe(sampleClass2);
        }

        [Fact]
        public void Should_LifeStyle_Scope()
        {
            this.Services.RegisterAssembly(this.GetType().Assembly);
            this.Build();

            using (var scope = this.ServiceProvider.CreateScope())
            {
                var scopeServiceProvider = scope.ServiceProvider;

                var sampleClass1 = scopeServiceProvider.GetService<ISampleClass>();

                var sampleClass2 = scopeServiceProvider.GetService<ISampleClass>();

                sampleClass1.ShouldBe(sampleClass2);
            }
        }


        [Fact]
        public void Should_LifeStyle_Singleton()
        {
            this.Services.RegisterAssembly(this.GetType().Assembly);
            this.Build();

            var sampleClass1 = this.ServiceProvider.GetService<ISampleClass>();

            var sampleClass2 = this.ServiceProvider.GetService<ISampleClass>();

            sampleClass1.ShouldBe(sampleClass2);
        }
    }
}
