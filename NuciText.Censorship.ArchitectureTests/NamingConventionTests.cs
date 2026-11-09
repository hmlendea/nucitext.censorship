using System;
using System.Linq;
using System.Reflection;

using NuciText.Censorship.English;

using NUnit.Framework;

namespace NuciText.Censorship.ArchitectureTests
{
    [TestFixture]
    public sealed class NamingConventionTests
    {
        private static readonly Assembly ProductionAssembly = typeof(ITextCensor).Assembly;

        [Test]
        public void GivenEligibleInterfaces_WhenChecked_ThenNameStartsWithI()
        {
            Type[] interfaces = ProductionAssembly
                .GetTypes()
                .Where(type => type.IsInterface && type.IsPublic)
                .ToArray();

            Assert.That(interfaces, Is.Not.Empty);
            Assert.That(
                interfaces.Where(type => !type.Name.StartsWith('I')),
                Is.Empty,
                "Interfaces must use the I-prefixed PascalCase naming convention.");
        }

        [Test]
        public void GivenPublicTypes_WhenChecked_ThenNamesArePascalCase()
        {
            string[] nonPascalTypes = ProductionAssembly
                .GetExportedTypes()
                .Select(type => type.Name)
                .Where(name => name.Length == 0 || !char.IsUpper(name, 0))
                .ToArray();

            Assert.That(nonPascalTypes, Is.Empty);
        }

        [Test]
        public void GivenITextCensor_WhenChecked_ThenItResidesInEnglishNamespace()
            => Assert.That(
                typeof(ITextCensor).Namespace,
                Is.EqualTo("NuciText.Censorship.English"));

        [Test]
        public void GivenITextCensor_WhenChecked_ThenItIsALanguageSpecificAbstraction()
            => Assert.That(
                typeof(ITextCensor).IsInterface,
                Is.True,
                "ITextCensor must remain an interface abstraction.");
    }
}
