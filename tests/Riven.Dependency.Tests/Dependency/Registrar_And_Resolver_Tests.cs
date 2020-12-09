//using System;
//using System.Collections.Generic;
//using System.Text;

//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.DependencyInjection.Extensions;

//using RivenTestsBase;

//using Shouldly;

//using Xunit;

//namespace Riven.Dependency
//{
//    public partial class Registrar_And_Resolver_Tests : TestBaseWithLocalIocManager
//    {

//        [Fact]
//        public void Should_RegisterAssembly()
//        {
//            this.Services.RegisterAssembly(this.GetType().Assembly);

//            this.Build();

//            var sampleClass = this.ServiceProvider.GetService<ITransient>();
//            var valA = nameof(Transient);
//            Assert.Equal(sampleClass.Call(), valA);

//        }


//        [Fact]
//        public void Should_LifeStyle_Transient()
//        {
//            this.Services.RegisterAssembly(this.GetType().Assembly);
//            this.Build();

//            var sampleClass1 = this.ServiceProvider.GetService<ITransient>();

//            var sampleClass2 = this.ServiceProvider.GetService<ITransient>();

//            sampleClass1.ShouldNotBe(sampleClass2);
//        }

//        [Fact]
//        public void Should_LifeStyle_Scope()
//        {
//            this.Services.RegisterAssembly(this.GetType().Assembly);
//            this.Build();

//            using (var scope = this.ServiceProvider.CreateScope())
//            {
//                var scopeServiceProvider = scope.ServiceProvider;

//                var sampleClass1 = scopeServiceProvider.GetService<IScope>();

//                var sampleClass2 = scopeServiceProvider.GetService<IScope>();

//                sampleClass1.ShouldBe(sampleClass2);
//            }
//        }


//        [Fact]
//        public void Should_LifeStyle_Singleton()
//        {
//            this.Services.RegisterAssembly(this.GetType().Assembly);
//            this.Build();

//            var sampleClass1 = this.ServiceProvider.GetService<ISingleton>();

//            var sampleClass2 = this.ServiceProvider.GetService<ISingleton>();

//            sampleClass1.ShouldBe(sampleClass2);
//        }

//        public interface ITransient
//        {
//            string Name { get; set; }
//            string Call();
//        }

//        public class Transient : ITransient, ITransientDependency
//        {
//            public string Name { get; set; }

//            public string Call()
//            {
//                return nameof(Transient);
//            }
//        }

//        public interface IScope
//        {
//            string Name { get; set; }
//            string Call();
//        }

//        public class Scope : IScope, IScopeDependency
//        {
//            public string Name { get; set; }

//            public string Call()
//            {
//                return nameof(Scope);
//            }
//        }

//        public interface ISingleton
//        {
//            string Name { get; set; }
//            string Call();
//        }

//        public class Singleton : ISingleton, ISingletonDependency
//        {
//            public string Name { get; set; }

//            public string Call()
//            {
//                return nameof(Singleton);
//            }
//        }
//    }
//}
