using System.Collections.Generic;
using HarmonyLib;

namespace MuteToggle.Patches
{
    [HarmonyPatch(typeof(AudioManager))]
    public static class AudioManagerPatches
    {
        [HarmonyPatch(nameof(AudioManager.UpdatePushToTalk))]
        [HarmonyPrefix]
        static bool AudioManagerUpdatePushToTalkPrefix(ref bool ___pushToTalk)
        {
            // Use original method if the mod is disabled.
            if (!Settings.IsToggleMuteModEnabled.Value) return true;
            
            // Disable push to talk if the mod is enabled.
            // With it on, mic sensitivity is too low and the mod doesn't work correctly.
            ___pushToTalk = false;
            return false;
        }
    }
}