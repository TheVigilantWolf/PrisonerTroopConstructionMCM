using MCM.Abstractions.Attributes;
using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Base.Global;

namespace PrisonerTroopConstruction
{
    public class Settings : AttributeGlobalSettings<Settings>
    {
        [SettingPropertyBool("Troop Construction Bonus", IsToggle = true, Order = 1, RequireRestart = false)]
        [SettingPropertyGroup("Troop Construction")]
        public bool TroopConstructionEnable { get; set; } = true;

        [SettingPropertyInteger("Men Per Brick", 1, 10, HintText = "The amount of troops per bonus point to construction.", Order = 2, RequireRestart = false)]
        [SettingPropertyGroup("Troop Construction")]
        public float MenPerBrick { get; set; } = 4;

        [SettingPropertyFloatingInteger("Construction Bonus Per Skill Point", 0, 1, HintText = "The amount of bonus construction points per skill point your parties assigned engineer has.", Order = 3, RequireRestart = false)]
        [SettingPropertyGroup("Troop Construction")]
        public float BricksPerEngineerSkillPoint { get; set; } = 0.25f;

        [SettingPropertyBool("Prisoner Construction Bonus", IsToggle = true, Order = 4, RequireRestart = false)]
        [SettingPropertyGroup("Prisoner Construction")]
        public bool PrisonerConstructionEnable { get; set; } = true;

        [SettingPropertyInteger("Prisoners Per Brick", 1, 10, HintText = "The amount of prisoners per bonus point to construction.", Order = 5, RequireRestart = false)]
        [SettingPropertyGroup("Prisoner Construction")]
        public float PrisonersPerBrick { get; set; } = 4;

        public override string Id => "ConstructionBonuses";

        public override string DisplayName => "Construction Bonuses";
    }
}