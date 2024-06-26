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
using System.Linq;

namespace PrisonerTroopConstruction
{
    [HarmonyPatch(typeof(DefaultBuildingConstructionModel), "CalculateDailyConstructionPower")]
    class patches
    {
        public const int TownBoostCost = 500;
        public const int TownBoostBonus = 50;
        public const int CastleBoostCost = 250;
        public const int CastleBoostBonus = 20;
        private static readonly TextObject ArmyConstructionBonusText = new TextObject("{=armycon}Troop Bonus", (Dictionary<string, object>)null);
        private static readonly TextObject PrisonerConstructionBonusText = new TextObject("Dungeon Bonus", (Dictionary<string, object>)null);

        private static void Postfix(ref ExplainedNumber __result, Town town, bool includeDescriptions = false)
        {
            var settings = GlobalSettings<Settings>.Instance;

            if (settings.PrisonerConstructionEnable)
            {
                if (town.OwnerClan == Hero.MainHero.Clan)
                {
                    float prisonerBonus = 0.0f;
                    float totalPrisonerBonus = (float)town.Settlement.Party.PrisonRoster.ToFlattenedRoster()
                          .Select(r => r.Troop.Tier * 0.25 * SubModule.PrisonersPerBrick)
                          .Sum();
                    __result.Add(totalPrisonerBonus, PrisonerConstructionBonusText);
                }
            }
            if (settings.TroopConstructionEnable)
            {  
                if (Hero.MainHero.CurrentSettlement == town.Settlement && town.OwnerClan == Hero.MainHero.Clan)
                {
                    float armyEngineerBonus = GetArmyEngineerBonus();
                    float manpowerBonus = 0.0f; // Declare the variable here
                    float manpowerBonusArmy = 0.0f;

                    //Calculates manpower bonus for the main party
                    manpowerBonus += (float)MobileParty.MainParty.Party.MemberRoster.ToFlattenedRoster()
                    .Where(r => !r.IsWounded)
                    .Select(r => r.Troop.Tier * 0.25)
                    .Sum();

                    //Calculate manpower bonus for attached parties
                    if (MobileParty.MainParty.Army != null)
                    {
                        foreach (var attachedParty in MobileParty.MainParty.Army.LeaderParty.AttachedParties)
                        {
                            if (attachedParty.CurrentSettlement == Hero.MainHero.CurrentSettlement)
                            {
                                
                                manpowerBonusArmy += (float)attachedParty.Party.MemberRoster.ToFlattenedRoster()
                                .Where(r => !r.IsWounded)
                                .Select(r => r.Troop.Tier * 0.25)
                                .Sum();
                            }
                        }
                    }
                    float totalArmyBonus = armyEngineerBonus + (manpowerBonus * SubModule.MenPerBrick);
                    __result.Add(totalArmyBonus, ArmyConstructionBonusText, null);
                }
            }
            __result.LimitMin(0.0f);
        }
        private static float GetArmyEngineerBonus()
        {
            var settings = GlobalSettings<Settings>.Instance;
            MobileParty mainParty = MobileParty.MainParty;

            if (mainParty.EffectiveEngineer == null || !settings.EngineerBonusEnable)
                return 0.0f;
            
            float num = mainParty.EffectiveEngineer.GetSkillValue(DefaultSkills.Engineering);
            if (num > 300.0f)
                num = 300f;
            return num * SubModule.BricksPerEngineerSkillPoint;
        }
    }
}
