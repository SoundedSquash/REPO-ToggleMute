using BepInEx.Configuration;
using BepInEx.Logging;
using UnityEngine;

namespace MuteToggle
{
    public static class Settings
    {
        public static ConfigEntry<bool> IsToggleMuteModEnabled { get; private set; }
        public static ConfigEntry<bool> StartUnmuted { get; private set; }
        
        public static ManualLogSource Logger { get; private set; }
        
        public static bool MuteToggleState { get; internal set; }
        public static GameObject? MutedIndicator { get; internal set; }
        
        public static GameObject? ChatGameObject { get; internal set; }

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
            
            MuteToggleState = StartUnmuted.Value;
        }
        
        /// <summary>
        /// Show/Hide the muted indicator.
        /// </summary>
        /// <param name="hide">True to hide. False to show.</param>
        public static void HideMutedIndicator(bool hide)
        {
            // When hide = true, then active = false
            MutedIndicator?.gameObject.SetActive(!hide);
        }
    }
}