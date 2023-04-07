﻿using BepInEx;
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
        public const string pluginVersion = "1.0.0";

        public void Awake()
        {
            Harmony.CreateAndPatchAll(typeof(Main));
        }

        [HarmonyPatch(typeof(SkillShipBonus), nameof(SkillShipBonus.GetString))]
        [HarmonyPostfix]
        private static void SSB_GetStrPost(SkillShipBonus __instance, ref string __result)
        {
            __result += " (" + __instance.modifier * 100 + "%)";
        }
    }
}
