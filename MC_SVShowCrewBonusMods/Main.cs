using BepInEx;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MC_SVNoAESound
{
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    public class Main : BaseUnityPlugin
    {
        public const string pluginGuid = "mc.starvalor.showcrewbonusmods";
        public const string pluginName = "SV Show Crew Bonus Mods";
        public const string pluginVersion = "1.0.3";

        public void Awake()
        {
            Harmony.CreateAndPatchAll(typeof(Main));
        }

        [HarmonyPatch(typeof(SkillShipBonus), nameof(SkillShipBonus.GetString))]
        [HarmonyPostfix]
        private static void SSB_GetStrPost(SkillShipBonus __instance, SpaceShip ss, bool secondInCommand, bool thirdInCommand, ref string __result)
        {
            float mod = __instance.modifier - 1.0f;
            string colOpen = "";
            string colClose = "";
            if (ss != null && ss.crew.isPlayer && PChar.Char.SK[47] == 1 && (secondInCommand || (thirdInCommand && ss.crew.prodigyAffects3rdSeat)))
            {
                mod = 0.5f;
                colOpen = ColorSys.infoPos;
                colClose = "</color>";
            }
            
            string sym = mod < 0 ? "" : "+";
            __result += " (" + colOpen + sym + (Math.Round(mod,2) * 100) + "%" + colClose + ")";
        }
    }
}
