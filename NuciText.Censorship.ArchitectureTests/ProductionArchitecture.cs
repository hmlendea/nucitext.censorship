using System.Reflection;

using ArchUnitNET.Domain;
using ArchUnitNET.Loader;

using NuciText.Censorship.English;

using Assembly = System.Reflection.Assembly;

namespace NuciText.Censorship.ArchitectureTests
{
    internal static class ProductionArchitecture
    {
        internal static Assembly Assembly => typeof(ITextCensor).Assembly;

        internal static Architecture Model { get; } = new ArchLoader()
            .LoadAssemblies(Assembly)
            .Build();
    }
}
