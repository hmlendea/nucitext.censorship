using System;
using System.Linq;
using System.Reflection;

using NuciText.Censorship.English;

using NUnit.Framework;

namespace NuciText.Censorship.ArchitectureTests
{
    [TestFixture]
    public sealed class ProductionArchitectureTests
    {
        private static readonly Assembly ProductionAssembly = typeof(ITextCensor).Assembly;

        [Test]
        public void GivenProductionAssembly_WhenLoaded_ThenAssemblyNameIsNuciTextCensorship()
            => Assert
                .That(
                    ProductionAssembly.GetName().Name,
                    Is.EqualTo("NuciText.Censorship"));

        [Test]
        public void GivenProductionAssembly_WhenInspectingExportedTypes_ThenPublicSurfaceIsStable()
        {
            string?[] exportedTypes =
                ProductionAssembly
                    .GetExportedTypes()
                    .Select(type => type.FullName)
                    .Where(fullName => fullName is not null)
                    .OrderBy(fullName => fullName, StringComparer.Ordinal)
                    .ToArray();

            string?[] expectedTypes =
            [
                "NuciText.Censorship.English.ITextCensor"
            ];

            Assert.That(exportedTypes, Is.EqualTo(expectedTypes));
        }
    }
}
