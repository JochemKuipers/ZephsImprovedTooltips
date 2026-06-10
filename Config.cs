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

    public readonly Settings configSettings = new();

    [DefaultValue(true)]
    public bool Enabled {
        get => configSettings.enabled;
        set => configSettings.enabled = value;
    }

    [DrawTicks]
    [DefaultValue(Settings.ReforgeVisibility.ShowIfTinkererExists)]
    public Settings.ReforgeVisibility ReforgeVisibility {
        get => configSettings.reforgeVisiblity;
        set => configSettings.reforgeVisiblity = value;
    }

    [DrawTicks]
    [DefaultValue(Settings.SellVisibility.AlwaysShow)]
    public Settings.SellVisibility SellVisibility {
        get => configSettings.sellVisibility;
        set => configSettings.sellVisibility = value;
    }

    [DefaultValue(true)]
    public bool HighlightColourEnabled {
        get => configSettings.useHighlightColour;
        set => configSettings.useHighlightColour = value;
    }

    [DefaultValue(true)]
    public bool ShowModName {
        get => configSettings.showModName;
        set => configSettings.showModName = value;
    }

    [DefaultValue(true)]
    public bool ShowAmmunition {
        get => configSettings.showAmmunition;
        set => configSettings.showAmmunition = value;
    }

    [DefaultValue(true)]
    public bool ShowMeasuredDps {
        get => configSettings.showMeasuredDps;
        set => configSettings.showMeasuredDps = value;
    }

    [DefaultValue(typeof(Color), "80, 140, 80, 255")]
    public Color ReforgeColour {
        get => configSettings.reforgeColour;
        set => configSettings.reforgeColour = value;
    }

    [DefaultValue(typeof(Color), "255, 180, 0, 255")]
    public Color HighlightColour {
        get => configSettings.highlightColour;
        set => configSettings.highlightColour = value;
    }

    [DefaultValue(typeof(Color), "200, 40, 200, 255")]
    public Color ModColour {
        get => configSettings.modColour;
        set => configSettings.modColour = value;
    }

    public override void OnChanged() {
        ZephsImprovedTooltipsGlobalItem.Settings = configSettings;
    }
}

}
