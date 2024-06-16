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

        [SettingPropertyInteger("Men Per Brick", 1, 100, Order = 2, RequireRestart = false)]
        [SettingPropertyGroup("Troop Construction")]
        public float MenPerBrick { get; set; } = 4;

        [SettingPropertyBool("Prisoner Construction Bonus", IsToggle = true, Order = 3, RequireRestart = false)]
        [SettingPropertyGroup("Prisoner Construction")]
        public bool PrisonerConstructionEnable { get; set; } = true;

        [SettingPropertyInteger("Prisoners Per Brick", 1, 100, Order = 4, RequireRestart = false)]
        [SettingPropertyGroup("Prisoner Construction")]
        public float PrisonersPerBrick { get; set; } = 10;

        [SettingPropertyBool("Engineer Construction Bonus", IsToggle = true, Order = 5, RequireRestart = false)]
        [SettingPropertyGroup("Party Engineer Skill Boost")]
        public bool EngineerConstructionEnable { get; set; } = true;

        [SettingPropertyFloatingInteger("Construction Bonus Per Skill Point", 0.25f, 1, Order = 6, RequireRestart = false)]
        [SettingPropertyGroup("Party Engineer Skill Boost")]
        public float BricksPerEngineerSkillPoint { get; set; } = 0.25f;
    }
}