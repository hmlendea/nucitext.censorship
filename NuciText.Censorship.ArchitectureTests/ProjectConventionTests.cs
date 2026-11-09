using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

using NUnit.Framework;

namespace NuciText.Censorship.ArchitectureTests
{
    [TestFixture]
    public sealed class ProjectConventionTests
    {
        private static string ProductionProjectPath => Path.Combine(
            TestContext.CurrentContext.TestDirectory,
            "ProductionProjects",
            "NuciText.Censorship.csproj");

        private static string SolutionPath => Path.Combine(
            TestContext.CurrentContext.TestDirectory,
            "Solution",
            "NuciText.Censorship.slnx");

        [Test]
        public void GivenProductionProject_WhenLoaded_ThenRequiredPropertiesAreEnabled()
        {
            XDocument project = XDocument.Load(ProductionProjectPath);
            XElement propertyGroup = project.Descendants("PropertyGroup").First();

            Assert.That(GetProperty(propertyGroup, "TargetFramework"), Is.EqualTo("net10.0"));
            Assert.That(GetProperty(propertyGroup, "ImplicitUsings"), Is.EqualTo("disable"));
            Assert.That(GetProperty(propertyGroup, "LangVersion"), Is.EqualTo("latest"));
            Assert.That(GetProperty(propertyGroup, "Nullable"), Is.EqualTo("enable"), "Nullable must be explicitly enabled.");
        }

        [Test]
        public void GivenProductionProject_WhenLoaded_ThenPackageReferencesAreAlphabetical()
        {
            XDocument project = XDocument.Load(ProductionProjectPath);
            string[] packages = project.Descendants("PackageReference")
                .Select(reference => reference.Attribute("Include"))
                .OfType<XAttribute>()
                .Select(attribute => attribute.Value)
                .ToArray();

            Assert.That(packages, Is.Empty);
            Assert.That(packages, Is.EqualTo(packages.OrderBy(package => package, StringComparer.Ordinal).ToArray()));
        }

        [Test]
        public void GivenProductionProject_WhenLoaded_ThenItIsPackable()
        {
            XDocument project = XDocument.Load(ProductionProjectPath);

            Assert.That(project.Descendants("IsPackable").Single().Value, Is.EqualTo("true"));
        }

        [Test]
        public void GivenSolution_WhenLoaded_ThenItIsSlnxWithoutDeclarationOrVersion()
        {
            string content = File.ReadAllText(SolutionPath);

            Assert.That(content.TrimStart(), Does.StartWith("<Solution>"));
            Assert.That(content.Contains("<?xml", StringComparison.OrdinalIgnoreCase), Is.False);
            Assert.That(content.Contains("<Version>", StringComparison.OrdinalIgnoreCase), Is.False);
        }

        [Test]
        public void GivenSolution_WhenLoaded_ThenProjectsAreOrderedMainFirstThenTests()
        {
            XDocument solution = XDocument.Load(SolutionPath);
            string[] projectPaths = solution.Descendants("Project")
                .Select(element => element.Attribute("Path"))
                .OfType<XAttribute>()
                .Select(attribute => attribute.Value)
                .ToArray();

            Assert.That(projectPaths, Has.Length.GreaterThan(1));
            Assert.That(projectPaths[0], Is.EqualTo("NuciText.Censorship/NuciText.Censorship.csproj"));
            Assert.That(projectPaths[1], Is.EqualTo("NuciText.Censorship.ArchitectureTests/NuciText.Censorship.ArchitectureTests.csproj"));
            Assert.That(projectPaths[2], Is.EqualTo("NuciText.Censorship.UnitTests/NuciText.Censorship.UnitTests.csproj"));
        }

        [Test]
        public void GivenTestProject_WhenLoaded_ThenTestFlagsAreSet()
        {
            XDocument project = XDocument.Load(Path.Combine(
                TestContext.CurrentContext.TestDirectory,
                "TestProjects",
                "NuciText.Censorship.ArchitectureTests.csproj"));
            XElement propertyGroup = project.Descendants("PropertyGroup").First();

            Assert.That(GetProperty(propertyGroup, "IsTestProject"), Is.EqualTo("true"));
            Assert.That(GetProperty(propertyGroup, "ExcludeFromCodeCoverage"), Is.EqualTo("true"));
            Assert.That(GetProperty(propertyGroup, "IsPackable"), Is.EqualTo("false"));
        }

        private static string GetProperty(XElement propertyGroup, string name)
        {
            XElement? element = propertyGroup.Element(name);

            if (element is null)
            {
                return string.Empty;
            }

            return element.Value;
        }
    }
}
