using osu.Framework.Configuration;
using osu.Framework.Platform;

namespace touhou.sharp.Game.Config
{
    public class THSharpConfigManager : IniConfigManager<THSharpSetting>
    {
        protected override string Filename => @"THSharp.ini";

        public THSharpConfigManager(Storage storage) : base(storage)
        {
        }

        protected override void InitialiseDefaults()
        {
            Set(THSharpSetting.Gamemode, Gamemodes.Touhou);

            Set(THSharpSetting.SavedName, "User");
            Set(THSharpSetting.SavedUserID, -1);
            Set(THSharpSetting.PlayerColor, "#ffffff");

            Set(THSharpSetting.HostIP, "Host's IP Address");
            Set(THSharpSetting.LocalIP, "Local IP Address");

            Set(THSharpSetting.HostPort, 25570);
            Set(THSharpSetting.LocalPort, 25570);
        }
    }

    public enum THSharpSetting
    {
        Gamemode,

        SavedName,
        SavedUserID,
        PlayerColor,

        HostIP,
        LocalIP,

        HostPort,
        LocalPort
    }

    public enum Gamemodes
    {
        Touhou
    }
}