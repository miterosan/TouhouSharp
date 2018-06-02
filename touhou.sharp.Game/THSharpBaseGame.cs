using osu.Framework.Allocation;

namespace touhou.sharp.Game
{
    public class THSharpBaseGame : osu.Framework.Game
    {
        protected override string MainResourceFile => "touhou.sharp.Game.Resources.dll";

        private DependencyContainer dependencies;

        protected override IReadOnlyDependencyContainer CreateLocalDependencies(IReadOnlyDependencyContainer parent) =>
            dependencies = new DependencyContainer(base.CreateLocalDependencies(parent));

        [BackgroundDependencyLoader]
        private void load()
        {
            dependencies.Cache(this);
        }
    }
}
