using System;
using System.IO;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using NUnit.Framework;

namespace NuciText.Censorship.ArchitectureTests
{
    [TestFixture]
    public sealed class CodingConventionTests
    {
        [Test]
        public void GivenProductionSources_WhenChecked_ThenBlockNamespacesAreUsed()
        {
            SyntaxNode[] roots = ReadProductionRoots();

            SyntaxNode[] fileScopedNamespaces = roots
                .SelectMany(root => root.DescendantNodes().OfType<FileScopedNamespaceDeclarationSyntax>())
                .ToArray();

            Assert.That(fileScopedNamespaces, Is.Empty, "Production code must use block namespaces.");
        }

        [Test]
        public void GivenProductionSources_WhenChecked_ThenNoVarIsUsed()
        {
            SyntaxNode[] roots = ReadProductionRoots();

            SyntaxNode[] varDeclarations = roots
                .SelectMany(root => root.DescendantNodes().OfType<VariableDeclarationSyntax>())
                .Where(declaration => declaration.Type.IsVar)
                .ToArray();

            Assert.That(varDeclarations, Is.Empty, "Production code must use explicit types instead of var.");
        }

        [Test]
        public void GivenProductionSources_WhenChecked_ThenNoNullableDirectiveIsUsed()
        {
            SyntaxNode[] roots = ReadProductionRoots();

            SyntaxTrivia[] nullableDirectives = roots
                .SelectMany(root => root.DescendantTrivia())
                .Where(trivia => trivia.IsKind(SyntaxKind.NullableDirectiveTrivia))
                .ToArray();

            Assert.That(nullableDirectives, Is.Empty, "Production code must not use #nullable directives.");
        }

        [Test]
        public void GivenProductionSources_WhenChecked_ThenNoRegionDirectivesAreUsed()
        {
            SyntaxNode[] roots = ReadProductionRoots();

            SyntaxTrivia[] regionDirectives = roots
                .SelectMany(root => root.DescendantTrivia())
                .Where(trivia => trivia.IsKind(SyntaxKind.RegionDirectiveTrivia))
                .ToArray();

            Assert.That(regionDirectives, Is.Empty, "Production code must not use #region directives.");
        }

        [Test]
        public void GivenPublicInterfaceMembers_WhenChecked_ThenXmlSummaryIsPresent()
        {
            string root = ArchitecturePaths.ProductionSourceRoot;
            string[] files = Directory.GetFiles(root, "*.cs", SearchOption.AllDirectories)
                .Where(file => !file.Contains(Path.DirectorySeparatorChar + "bin" + Path.DirectorySeparatorChar)
                    && !file.Contains(Path.DirectorySeparatorChar + "obj" + Path.DirectorySeparatorChar))
                .OrderBy(file => file, StringComparer.Ordinal)
                .ToArray();

            Assert.That(files, Is.Not.Empty, "No production source files were found.");

            foreach (string file in files)
            {
                SyntaxNode rootNode = CSharpSyntaxTree
                    .ParseText(File.ReadAllText(file), path: file)
                    .GetRoot();

                InterfaceDeclarationSyntax[] publicInterfaces = rootNode
                    .DescendantNodes()
                    .OfType<InterfaceDeclarationSyntax>()
                    .Where(declaration => declaration.Modifiers.Any(SyntaxKind.PublicKeyword))
                    .ToArray();

                foreach (InterfaceDeclarationSyntax interfaceDeclaration in publicInterfaces)
                {
                    Assert.That(
                        HasXmlSummary(interfaceDeclaration),
                        Is.True,
                        $"{file}: public interface '{interfaceDeclaration.Identifier.ValueText}' must have an XML summary.");

                    foreach (MemberDeclarationSyntax member in interfaceDeclaration.Members)
                    {
                        Assert.That(
                            HasXmlSummary(member),
                            Is.True,
                            $"{file}: public interface member in '{interfaceDeclaration.Identifier.ValueText}' must have an XML summary.");
                    }
                }
            }
        }

        private static SyntaxNode[] ReadProductionRoots()
        {
            string root = ArchitecturePaths.ProductionSourceRoot;
            string[] files = Directory.GetFiles(root, "*.cs", SearchOption.AllDirectories)
                .Where(file => !file.Contains(Path.DirectorySeparatorChar + "bin" + Path.DirectorySeparatorChar)
                    && !file.Contains(Path.DirectorySeparatorChar + "obj" + Path.DirectorySeparatorChar))
                .OrderBy(file => file, StringComparer.Ordinal)
                .ToArray();

            Assert.That(files, Is.Not.Empty, "No production source files were found.");

            return files
                .Select(file => CSharpSyntaxTree.ParseText(File.ReadAllText(file), path: file).GetRoot())
                .ToArray();
        }

        private static bool HasXmlSummary(SyntaxNode node)
            => node.GetLeadingTrivia()
                .Any(trivia => trivia.IsKind(SyntaxKind.SingleLineDocumentationCommentTrivia));
    }
}
