using HarmonyLib;
using Photon.Voice.Unity;

namespace MuteToggle.Patches
{
    [HarmonyPatch(typeof(PlayerVoiceChat))]
    public static class PlayerVoiceChatPatches
    {
        
        [HarmonyPatch("Update")]
        [HarmonyPostfix]
        static void PlayerVoiceChatUpdatePostfix(ref Recorder ___recorder, bool ___microphoneEnabled)
        {
            // Don't run if the mod is disabled or the microphone is disabled.
            if (!Settings.IsToggleMuteModEnabled.Value || !___microphoneEnabled) return;
            
            // Don't run if the chat window is open.
            if (Settings.ChatGameObject != null && Settings.ChatGameObject.activeSelf) return;
            
            // Check for push-to-talk key press and change value of toggleMute.
            if (SemiFunc.InputDown(InputKey.PushToTalk))
            {
                Settings.MuteToggleState = !Settings.MuteToggleState;
                Settings.HideMutedIndicator(Settings.MuteToggleState);
            }
            
            ___recorder.TransmitEnabled = Settings.MuteToggleState;
        }
    }
}