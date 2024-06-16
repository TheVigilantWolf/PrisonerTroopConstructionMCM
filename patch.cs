﻿using System;
using System.Diagnostics;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.Localization;
using HarmonyLib;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.CampaignSystem.Party;
using MCM.Abstractions.Base.Global;

namespace PrisonerTroopConstruction
{
    [HarmonyPatch(typeof(DefaultBuildingConstructionModel), "CalculateDailyConstructionPower")]
    class patches
    {
        public const int TownBoostCost = 500;
        public const int TownBoostBonus = 50;
        public const int CastleBoostCost = 250;
        public const int CastleBoostBonus = 20;
        private static readonly TextObject ArmyConstructionBonusText = new TextObject("{=armycon}Army Bonus", (Dictionary<string, object>)null);
        private static readonly TextObject PrisonerConstructionBonusText = new TextObject("Dungeon Bonus", (Dictionary<string, object>)null);

        private static void Postfix(ref ExplainedNumber __result, Town town, bool includeDescriptions = false)
        {
            var settings = GlobalSettings<Settings>.Instance;

            if (settings.PrisonerConstructionEnable)
            {
                if (town.OwnerClan == Hero.MainHero.Clan)
                {
                    float prisonerBonus = 0.0f;
                    prisonerBonus = town.Settlement.Party.NumberOfPrisoners;
                    float totalPrisonerBonus = prisonerBonus / SubModule.PrisonersPerBrick;
                    __result.Add(totalPrisonerBonus, PrisonerConstructionBonusText);
                }
            }
            
            if (settings.TroopConstructionEnable)
            {  
                if (Hero.MainHero.CurrentSettlement == town.Settlement && town.OwnerClan == Hero.MainHero.Clan)
                {
                    float armyEngineerBonus = GetArmyEngineerBonus();
                    float manpowerBonus = 0.0f; // Declare the variable here
                    if (MobileParty.MainParty.Army == null)
                    {
                        manpowerBonus = MobileParty.MainParty.Party.NumberOfHealthyMembers;
                    }
                    else
                    {
                        foreach (MobileParty andAttachedParty in MobileParty.MainParty.Army.LeaderParty.AttachedParties)
                        {
                            if (andAttachedParty.CurrentSettlement == Hero.MainHero.CurrentSettlement)
                                manpowerBonus += andAttachedParty.Party.NumberOfHealthyMembers;
                        }
                        if (MobileParty.MainParty.CurrentSettlement == Hero.MainHero.CurrentSettlement)
                        {
                            manpowerBonus += MobileParty.MainParty.Party.NumberOfHealthyMembers;
                        }
                    }
                    float totalArmyBonus = armyEngineerBonus + ((manpowerBonus -1) / SubModule.MenPerBrick);
                    __result.Add(totalArmyBonus, ArmyConstructionBonusText, null);
                }
            }
            __result.LimitMin(0.0f);
        }
        private static float GetArmyEngineerBonus()
        {
            MobileParty mainParty = MobileParty.MainParty;
            if (mainParty.EffectiveEngineer == null)
                return 0.0f;
            float num = mainParty.EffectiveEngineer.GetSkillValue(DefaultSkills.Engineering);
            if (num > 300.0f)
                num = 300f;
            return num * SubModule.BricksPerEngineerSkillPoint;
        }
    }
}
