using BepInEx;
using System.ComponentModel;

namespace PolaroidMenu.Patches
{
    [Description(PolaroidMenu.PluginInfo.Description)]
    [BepInPlugin(PolaroidMenu.PluginInfo.GUID, PolaroidMenu.PluginInfo.Name, PolaroidMenu.PluginInfo.Version)]
    public class HarmonyPatches : BaseUnityPlugin
    {
        private void OnEnable()
        {
            Menu.ApplyHarmonyPatches();
        }

        private void OnDisable()
        {
            Menu.RemoveHarmonyPatches();
        }
    }
}
