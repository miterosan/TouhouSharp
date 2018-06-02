using osu.Framework.Allocation;
using osu.Framework.Platform;
using touhou.sharp.Game.Config;

namespace touhou.sharp.Game
{
    public class THSharpBaseGame : osu.Framework.Game
    {
        protected THSharpConfigManager THSharpConfigManager;

        protected override string MainResourceFile => "touhou.sharp.Game.Resources.dll";

        private DependencyContainer dependencies;

        protected override IReadOnlyDependencyContainer CreateLocalDependencies(IReadOnlyDependencyContainer parent) =>
            dependencies = new DependencyContainer(base.CreateLocalDependencies(parent));

        public THSharpBaseGame()
        {
            Name = @"TouhouSharp";
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            dependencies.Cache(this);
            dependencies.Cache(THSharpConfigManager);
        }

        public override void SetHost(GameHost host)
        {
            if (THSharpConfigManager == null)
                THSharpConfigManager = new THSharpConfigManager(host.Storage);

            base.SetHost(host);
        }

        protected override void Dispose(bool isDisposing)
        {
            THSharpConfigManager?.Save();
            base.Dispose(isDisposing);
        }
    }
}
