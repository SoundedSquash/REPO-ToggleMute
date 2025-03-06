using BepInEx.Configuration;
using BepInEx.Logging;

namespace MuteToggle
{
    public static class Settings
    {
        public static ConfigEntry<bool> IsToggleMuteModEnabled { get; private set; }
        public static ConfigEntry<bool> StartUnmuted { get; private set; }
        
        public static ManualLogSource Logger { get; private set; }

        internal static void Initialize(ConfigFile config, ManualLogSource logger)
        {
            Logger = logger;
            
            IsToggleMuteModEnabled = config.Bind(
                "General",
                "Enabled",
                true,
                "Enable this mod. This overrides push to talk setting.");
            
            StartUnmuted = config.Bind(
                "General",
                "StartUnmuted",
                true,
                "Start the game with an unmuted mic.");
        }
    }
}