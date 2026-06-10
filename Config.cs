using System.ComponentModel;
using Microsoft.Xna.Framework;
using Terraria.ModLoader.Config;

namespace ZephsImprovedTooltips
{

    // Labels and tooltips now live in Localization/en-US_Mods.ZephsImprovedTooltips.hjson
    public class TooltipConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        private Settings configSettings = new();

        [DefaultValue(true)]
        public bool enabled
        {
            get => configSettings.enabled;
            set => configSettings.enabled = value;
        }

        [DrawTicks]
        [DefaultValue(Settings.ReforgeVisibility.ShowIfTinkererExists)]
        public Settings.ReforgeVisibility reforgeVisibility
        {
            get => configSettings.reforgeVisiblity;
            set => configSettings.reforgeVisiblity = value;
        }

        [DrawTicks]
        [DefaultValue(Settings.SellVisibility.AlwaysShow)]
        public Settings.SellVisibility sellVisibility
        {
            get => configSettings.sellVisibility;
            set => configSettings.sellVisibility = value;
        }

        [DefaultValue(true)]
        public bool highlightColourEnabled
        {
            get => configSettings.useHighlightColour;
            set => configSettings.useHighlightColour = value;
        }

        [DefaultValue(true)]
        public bool showModName
        {
            get => configSettings.showModName;
            set => configSettings.showModName = value;
        }

        [DefaultValue(true)]
        public bool showAmmunition
        {
            get => configSettings.showAmmunition;
            set => configSettings.showAmmunition = value;
        }

        [DefaultValue(typeof(Color), "80, 140, 80, 255")]
        public Color reforgeColour
        {
            get => configSettings.reforgeColour;
            set => configSettings.reforgeColour = value;
        }

        [DefaultValue(typeof(Color), "255, 180, 0, 255")]
        public Color highlightColour
        {
            get => configSettings.highlightColour;
            set => configSettings.highlightColour = value;
        }

        [DefaultValue(typeof(Color), "200, 40, 200, 255")]
        public Color modColour
        {
            get => configSettings.modColour;
            set => configSettings.modColour = value;
        }

        public override void OnChanged()
        {
            ZephsImprovedTooltipsGlobalItem.settings = configSettings;
        }
    }

}
