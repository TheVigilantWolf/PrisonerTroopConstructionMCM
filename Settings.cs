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

        [SettingPropertyFloatingInteger("Troop Construction Multiplier", 0, 1, HintText = "Multiplies Troop Tier * 0.25 * This Number. This will significantly increase or decrease the bonus given.", Order = 2, RequireRestart = false)]
        [SettingPropertyGroup("Troop Construction")]
        public float MenPerBrick { get; set; } = 0.5f;

        [SettingPropertyFloatingInteger("Construction Bonus Per Skill Point", 0, 1, HintText = "The amount of bonus construction points per skill point your parties assigned engineer has.", Order = 3, RequireRestart = false)]
        [SettingPropertyGroup("Troop Construction")]
        public float BricksPerEngineerSkillPoint { get; set; } = 0.25f;

        [SettingPropertyBool("Prisoner Construction Bonus", IsToggle = true, Order = 4, RequireRestart = false)]
        [SettingPropertyGroup("Prisoner Construction")]
        public bool PrisonerConstructionEnable { get; set; } = true;

        [SettingPropertyFloatingInteger("Prisoner Tier Multiplier", 0, 1, HintText = "Multiplies Troop Tier * 0.25 * This Number. This will significantly increase or decrease the bonus given.", Order = 5, RequireRestart = false)]
        [SettingPropertyGroup("Prisoner Construction")]
        public float PrisonersPerBrick { get; set; } = 0.5f;

        public override string Id => "ConstructionBonuses";

        public override string DisplayName => "Construction Bonuses";
    }
}