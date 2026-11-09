using ArchUnitNET.Domain;
using ArchUnitNET.Fluent;
using ArchUnitNET.NUnit;

using NUnit.Framework;

using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace NuciText.Censorship.ArchitectureTests
{
    [TestFixture]
    public sealed class DependencyBoundaryTests
    {
        private static readonly string[] TestFrameworkNamespacePatterns =
        [
            "^NUnit\\.",
            "^Moq\\.",
            "^TngTech\\.ArchUnitNET",
            "^Microsoft\\.NET\\.Test\\.Sdk"
        ];

        [TestCaseSource(nameof(TestFrameworkNamespacePatterns))]
        public void GivenProductionAssembly_WhenInspectingTypeDependencies_ThenTestFrameworksAreNotUsed(string pattern)
        {
            IObjectProvider<IType> productionTypes = Types()
                .That()
                .ResideInAssembly(ProductionArchitecture.Assembly);
            AssertHasTypes(productionTypes);

            Types().That()
                .Are(productionTypes)
                .Should()
                .NotDependOnAny(
                    Types().That()
                        .ResideInNamespaceMatching(pattern))
                .Because("production code must not depend on test frameworks or test tooling")
                .Check(ProductionArchitecture.Model);
        }

        [Test]
        public void GivenProductionComponents_WhenCheckingDependencies_ThenOnlySystemTypesAreReferenced()
        {
            IObjectProvider<IType> productionTypes = Types()
                .That()
                .ResideInAssembly(ProductionArchitecture.Assembly);
            AssertHasTypes(productionTypes);

            Types().That()
                .Are(productionTypes)
                .Should()
                .NotDependOnAny(
                    Types().That()
                        .ResideInNamespaceMatching(
                            "^NuciText\\.Censorship\\.UnitTests"))
                .Because("production code must not depend on test projects")
                .Check(ProductionArchitecture.Model);
        }

        private static void AssertHasTypes(IObjectProvider<IType> selection)
            => Assert.That(
                selection.GetObjects(ProductionArchitecture.Model),
                Is.Not.Empty,
                $"The {selection.Description} selector must match production types.");
    }
}
