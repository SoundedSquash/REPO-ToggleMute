using System.Collections.Generic;
using HarmonyLib;
using TMPro;
using UnityEngine;

namespace MuteToggle.Patches
{
    [HarmonyPatch(typeof(HUDCanvas))]
    public static class HUDCanvasPatches
    {
        [HarmonyPatch("Awake")]
        [HarmonyPostfix]
        static void HUDCanvasAwakePostfix()
        {
            // Ignore on menu levels.
            if (!Settings.IsToggleMuteModEnabled.Value) return;
            if (GameManager.instance.gameMode == 0) return;
            if (RunManager.instance.levelCurrent == RunManager.instance.levelMainMenu
                || RunManager.instance.levelCurrent == RunManager.instance.levelLobbyMenu) return;

            var toClone = GameObject.Find("EnergyMax");
            if (toClone == null)
            {
                Settings.Logger.LogError("Could not find text to clone (EnergyMax). HUD patching failed.");
                return;
            }
            // Copy reference GameObject up one more level to Game Hud
            var mutedText = Object.Instantiate(toClone, toClone.transform.parent.parent, true);
            mutedText.name = "MutedText";
            mutedText.transform.position = new Vector3(5, 30, 0);
            
            var mutedTextTMP = mutedText.GetComponent<TextMeshProUGUI>();
            mutedTextTMP.text = "MUTED";
            mutedTextTMP.color = Color.red;
            
            Settings.MutedIndicator = mutedText;
            Settings.HideMutedIndicator(Settings.MuteToggleState);
        }
    }
}