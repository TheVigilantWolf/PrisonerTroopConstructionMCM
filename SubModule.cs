using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using HarmonyLib;

#nullable disable
namespace PrisonerTroopConstruction
{
    public class SubModule : MBSubModuleBase
    {
        public static float MenPerBrick => Settings.Instance?.MenPerBrick ?? 4.0;
        public static float BricksPerEngineerSkillPoint => Settings.Instance?.BricksPerEngineerSkillPoint ?? 0.25;
        public static float PrisonersPerBrick => Settings.Instance?.PrisonersPerBrick ?? 10;

        protected override void OnBeforeInitialModuleScreenSetAsRoot() => base.OnBeforeInitialModuleScreenSetAsRoot();

        protected override void OnGameStart(Game game, IGameStarter starterObject)
        {
            base.OnGameStart(game, starterObject);
        }

        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
            new Harmony("TroopPrisonerConstruction").PatchAll();
        }
    }
}
