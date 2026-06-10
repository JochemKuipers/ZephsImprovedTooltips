using JetBrains.Annotations;
using Microsoft.Xna.Framework;

namespace ZephsImprovedTooltips
{

public class Settings
{
    // Enum members are selected via the config UI, not all are referenced in code
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public enum ReforgeVisibility
    {
        NeverShow,
        ShowIfTinkererExists,
        AlwaysShow
    }

    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public enum SellVisibility
    {
        NeverShow,
        AlwaysShow,
    }

        public ReforgeVisibility reforgeVisiblity = ReforgeVisibility.ShowIfTinkererExists;
        public SellVisibility sellVisibility = SellVisibility.AlwaysShow;

        public Color copperColour = new(246, 138, 96);
        public Color silverColour = new(181, 192, 193);
        public Color goldColour = new(221, 199, 91);
        public Color platColour = new(220, 220, 198);
        public Color reforgeColour = new(80, 140, 80);
        public Color highlightColour = new(255, 180, 0);
        public Color modColour = new(200, 40, 200);
        public bool useHighlightColour = true;
        public bool showModName = true;
        public bool showAmmunition = true;
    public bool showMeasuredDps = true;
        public bool enabled = true;

    }

}
