using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace MuteToggle
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    public class MuteToggleBase : BaseUnityPlugin
    {
        private const string PluginGuid = "soundedsquash.togglemute";
        private const string PluginName = "Toggle Mute";
        private const string PluginVersion = "0.1.0.0";
        
        private readonly Harmony _harmony = new Harmony(PluginGuid);

        private static readonly ManualLogSource ManualLogSource = BepInEx.Logging.Logger.CreateLogSource(PluginGuid);

        public void Awake()
        {
            // Initialize global objects
            Settings.Initialize(Config, ManualLogSource);

            _harmony.PatchAll();
            ManualLogSource.LogInfo($"{PluginName} loaded");
        }
    }
}