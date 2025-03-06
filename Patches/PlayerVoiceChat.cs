using HarmonyLib;
using Photon.Voice.Unity;

namespace MuteToggle.Patches
{
    [HarmonyPatch(typeof(PlayerVoiceChat))]
    public static class PlayerVoiceChatPatches
    {
        private static bool toggleMute = Settings.StartUnmuted.Value;
        
        [HarmonyPatch("Update")]
        [HarmonyPostfix]
        static void PlayerVoiceChatUpdatePostfix(ref Recorder ___recorder, bool ___microphoneEnabled)
        {
            // Don't run if the mod is disabled or the microphone is disabled.
            if (!Settings.IsToggleMuteModEnabled.Value || !___microphoneEnabled) return;
            
            // Check for push-to-talk key press and change value of toggleMute.
            if (InputManager.instance.InputToggleGet(InputKey.PushToTalk))
            {
                if (SemiFunc.InputDown(InputKey.PushToTalk))
                {
                    toggleMute = !toggleMute;
                }
            }
            else
                toggleMute = false;
            
            ___recorder.TransmitEnabled = toggleMute;
        }
    }
}