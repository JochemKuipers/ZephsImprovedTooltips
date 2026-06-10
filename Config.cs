using System.ComponentModel;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Terraria.ModLoader.Config;

namespace ZephsImprovedTooltips
{

// Labels and tooltips live in Localization/en-US_Mods.ZephsImprovedTooltips.hjson
// tModLoader instantiates and populates this class via reflection
[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public class TooltipConfig : ModConfig
{
    public override ConfigScope Mode => ConfigScope.ClientSide;

    private readonly Settings _configSettings = new();

    [DefaultValue(true)]
    public bool Enabled {
        get => _configSettings.enabled;
        set => _configSettings.enabled = value;
    }

    [DrawTicks]
    [DefaultValue(Settings.ReforgeVisibility.ShowIfTinkererExists)]
    public Settings.ReforgeVisibility ReforgeVisibility {
        get => _configSettings.reforgeVisiblity;
        set => _configSettings.reforgeVisiblity = value;
    }

    [DrawTicks]
    [DefaultValue(Settings.SellVisibility.AlwaysShow)]
    public Settings.SellVisibility SellVisibility {
        get => _configSettings.sellVisibility;
        set => _configSettings.sellVisibility = value;
    }

    [DefaultValue(true)]
    public bool HighlightColourEnabled {
        get => _configSettings.useHighlightColour;
        set => _configSettings.useHighlightColour = value;
    }

    [DefaultValue(true)]
    public bool ShowModName {
        get => _configSettings.showModName;
        set => _configSettings.showModName = value;
    }

    [DefaultValue(true)]
    public bool ShowAmmunition {
        get => _configSettings.showAmmunition;
        set => _configSettings.showAmmunition = value;
    }

    [DefaultValue(true)]
    public bool ShowMeasuredDps {
        get => _configSettings.showMeasuredDps;
        set => _configSettings.showMeasuredDps = value;
    }

    [DefaultValue(typeof(Color), "80, 140, 80, 255")]
    public Color ReforgeColour {
        get => _configSettings.reforgeColour;
        set => _configSettings.reforgeColour = value;
    }

    [DefaultValue(typeof(Color), "255, 180, 0, 255")]
    public Color HighlightColour {
        get => _configSettings.highlightColour;
        set => _configSettings.highlightColour = value;
    }

    [DefaultValue(typeof(Color), "200, 40, 200, 255")]
    public Color ModColour {
        get => _configSettings.modColour;
        set => _configSettings.modColour = value;
    }

    public override void OnChanged() {
        ZephsImprovedTooltipsGlobalItem.Settings = _configSettings;
    }
}

}
