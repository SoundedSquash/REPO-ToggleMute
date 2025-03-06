using System.Collections.Generic;
using HarmonyLib;

namespace MuteToggle.Patches
{
    [HarmonyPatch(typeof(InputManager))]
    public static class InputManagerPatches
    {
        [HarmonyPatch("InitializeInputs")]
        [HarmonyPostfix]
        static void InputManagerInitializeInputsPostfix(ref Dictionary<InputKey, bool> ___inputToggle)
        {
            Settings.Logger.LogDebug("Patching InitializeInputs.");
            if (!Settings.IsToggleMuteModEnabled.Value) return;
            
            ___inputToggle.Add(InputKey.PushToTalk, true);
            Settings.Logger.LogDebug("Input toggle added for Push To Talk.");
        }
    }
}