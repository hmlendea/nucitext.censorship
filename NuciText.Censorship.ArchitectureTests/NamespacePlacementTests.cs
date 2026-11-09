using System.Linq;

using ArchUnitNET.Domain;
using ArchUnitNET.Fluent;
using ArchUnitNET.NUnit;

using NUnit.Framework;

using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace NuciText.Censorship.ArchitectureTests
{
    [TestFixture]
    public sealed class NamespacePlacementTests
    {
        [Test]
        public void GivenProductionTypes_WhenChecked_ThenTheyResideInLibraryNamespace()
        {
            IObjectProvider<IType> productionTypes = Types()
                .That()
                .ResideInAssembly(ProductionArchitecture.Assembly);
            AssertHasTypes(productionTypes);

            Types().That()
                .Are(productionTypes)
                .Should()
                .ResideInNamespaceMatching("^NuciText\\.Censorship(\\.|$)")
                .Because("production types must reside under the library namespace")
                .Check(ProductionArchitecture.Model);
        }

        private static void AssertHasTypes(IObjectProvider<IType> selection)
            => Assert.That(
                selection.GetObjects(ProductionArchitecture.Model),
                Is.Not.Empty,
                $"The {selection.Description} selector must match production types.");
    }
}
