using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ZephsImprovedTooltips
{
    public class ZephsImprovedTooltipsGlobalItem : GlobalItem
    {
        public static Settings settings = new();

        public void splitValue(int totalCopper, out int plat, out int gold, out int silver, out int copper)
        {
            plat = totalCopper / 1000000;
            totalCopper %= 1000000;
            gold = totalCopper / 10000;
            totalCopper %= 10000;
            silver = totalCopper / 100;
            totalCopper %= 100;
            copper = totalCopper;
        }

        public string valueAsString(int plat, int gold, int silver, int copper)
        {
            string line = "";

            if (plat > 0)
            {
                line += plat + " platinum ";
            }

            if (gold > 0)
            {
                line += gold + " gold ";
            }

            if (silver > 0)
            {
                line += silver + " silver ";
            }

            //don't bother with copper if we have plat, makes the line too long and no one cares
            if (plat == 0 && copper > 0)
            {
                line += copper + " copper ";
            }

            return line;
        }

        public void reforgePriceTooltip(Item item, TooltipLine line)
        {
            var enabled = !(settings.reforgeVisiblity == Settings.ReforgeVisibility.NeverShow
                            || (settings.reforgeVisiblity == Settings.ReforgeVisibility.ShowIfTinkererExists && !NPC.savedGoblin));

            var totalValue = (int)(item.GetStoreValue() / 3.0f);

            if (!enabled || item.maxStack > 1 || item.vanity || totalValue == 0
                || (!item.accessory && item.defense > 0))
            {
                line.Text = "";
                return;
            }

            line.Text = "Reforge price: ";
            line.OverrideColor = settings.reforgeColour;

            splitValue(totalValue, out var plat, out var gold, out var silver, out var copper);
            line.Text += valueAsString(plat, gold, silver, copper);
        }

        public void sellPriceTooltip(Item item, TooltipLine line)
        {
            bool enabled = settings.sellVisibility == Settings.SellVisibility.AlwaysShow;

            int totalValue = (int)((item.GetStoreValue() * (long)item.stack) / 5.0f);

            if (!enabled ||
                item.type == ItemID.CopperCoin ||
                item.type == ItemID.SilverCoin ||
                item.type == ItemID.GoldCoin ||
                item.type == ItemID.PlatinumCoin ||
                totalValue == 0)
            {
                line.Text = "";
                return;
            }

            line.Text = "Sell price: ";

            splitValue(totalValue, out var plat, out var gold, out var silver, out var copper);
            line.Text += valueAsString(plat, gold, silver, copper);

            if (plat > 0)
            {
                line.OverrideColor = settings.platColour;
            }
            else if (gold > 0)
            {
                line.OverrideColor = settings.goldColour;
            }
            else if (silver > 0)
            {
                line.OverrideColor = settings.silverColour;
            }
            else
            {
                line.OverrideColor = settings.copperColour;
            }
        }

        public string colourAsHexString(Color colour)
        {
            return $"{colour.R:x2}{colour.G:x2}{colour.B:x2}";
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (!settings.enabled)
                return;

            string startColour = settings.useHighlightColour ? $"[c/{colourAsHexString(settings.highlightColour)}:" : "";
            string endColour = settings.useHighlightColour ? "]" : "";

            bool isTool = false;
            bool firesProjectile = false;

            if (item.pick > 0 ||
                item.axe > 0 ||
                item.hammer > 0)
            {
                isTool = true;
            }

            if (item.CountsAsClass(DamageClass.Melee) &&
                item.shoot > ProjectileID.None &&
                item.useAnimation <= item.useTime)
            {
                //Swords with projectiles end up having high use-time (projectile shot delay) with low animation time (sword hits) for some reason
                //This is the opposite of how every other swords works (swords hits is use time, even if the animation takes longer)
                firesProjectile = true;
            }

            Item ammoItem = null;

            if (item.CountsAsClass(DamageClass.Ranged) && item.useAmmo != 0)
            {
                //try ammo slots first
                for (int i = 54; i < 58; ++i)
                {
                    Item pItem = Main.LocalPlayer.inventory[i];
                    if (pItem.ammo > 0)
                    {
                        if (pItem.ammo == item.useAmmo && pItem.Name != "")
                        {
                            ammoItem = pItem;
                            break;
                        }
                    }
                }

                //then any slot if not
                if (ammoItem == null)
                {
                    foreach (var pItem in Main.LocalPlayer.inventory)
                    {
                        if (pItem.ammo <= 0) continue;
                        if (pItem.ammo != item.useAmmo || pItem.Name == "") continue;
                        ammoItem = pItem;
                        break;
                    }
                }
            }

            int speed;

            if (firesProjectile ||
                isTool || item.useAnimation is > 0 and < 100)
            {
                speed = item.useAnimation;
            }
            else
            {
                speed = item.useTime;
            }

            //takes in to account attack speed increases (1.4: multiplier above 1 means faster)
            float realSpeed = speed;
            float attackSpeedMult = Main.LocalPlayer.GetTotalAttackSpeed(item.DamageType);
            if (attackSpeedMult > 0)
            {
                realSpeed /= attackSpeedMult;
            }

            //takes in to account class damage increases
            float realDamage = 0;
            float ammoDamage = 0;

            if (ammoItem != null)
            {
                ammoDamage = ammoItem.damage;
            }

            //the average crit damage given the total crit chance, e.g. 20 damage with 50% crit is 30 crit damage
            int critDamage = 0;
            int critAmmoDamage = 0;
            if (item.damage > 0 && item.DamageType != DamageClass.Default)
            {
                //1.4: GetWeaponDamage/GetWeaponCrit already include the player's class bonuses and the item's own crit
                realDamage = Main.LocalPlayer.GetWeaponDamage(item);
                float realCritChance = Math.Min(1, Main.LocalPlayer.GetWeaponCrit(item) / 100.0f); //Return 1 if above, over 100% crit doesn't actually increase damage at all
                critDamage = (int)(realDamage * (2.0f * realCritChance));
                critAmmoDamage = (int)(ammoDamage * (2.0f * realCritChance));
            }

            //Not sure if this should go before or after the attack speed changes
            realSpeed += item.reuseDelay + (item.autoReuse ? -1 : 0);
            //60 ticks in a second, if realSpeed is <=0 just set it to cap at 60 per second, don't divide by 0/negative attacks per second)
            float attacksPerSecond = 60.0f / (realSpeed > 0 ? realSpeed : 1);
            int dps = (int)(realDamage / (1.0f / attacksPerSecond));
            int dpsCrit = (int)(critDamage / (1.0f / attacksPerSecond));
            int dpsAmmoOnly = (int)(ammoDamage / (1.0f / attacksPerSecond));
            int dpsCritAmmoOnly = (int)(critAmmoDamage / (1.0f / attacksPerSecond));
            int totalDPS = dps + dpsCrit; //excludes ammo

            for (int i = 0; i < tooltips.Count; ++i)
            {
                TooltipLine line = tooltips[i];

                if (settings.useHighlightColour && (line.Name == "Damage" ||
                    line.Name == "CritChance" ||
                    line.Name == "PickPower" ||
                    line.Name == "AxePower" ||
                    line.Name == "HammerPower" ||
                    line.Name == "Defense"))
                {
                    int spaceIndex = line.Text.IndexOf(' ');
                    if (spaceIndex >= 0)
                    {
                        line.Text = startColour + line.Text.Substring(0, spaceIndex) + endColour + line.Text.Substring(spaceIndex, line.Text.Length - spaceIndex);
                    }
                }

                if (line.Name == "Damage" && ammoDamage > 0)
                {
                    int spaceIndex = line.Text.IndexOf(' ');
                    if (spaceIndex >= 0)
                    {
                        line.Text = $"{line.Text[..spaceIndex]}+{ammoDamage + line.Text.Substring(spaceIndex, line.Text.Length - spaceIndex)}";
                    }
                }
                else if (line.Name == "Speed" && line.Text.Length >= 6)
                {
                    line.Text = $"{startColour + attacksPerSecond.ToString("0.#") + endColour} attacks per second ({line.Text.Substring(0, line.Text.Length - 6)})"; //5 = 5 in speed + space

                    TooltipLine dpsLine = new TooltipLine(Mod, "DPS", "");

                    if (ammoDamage > 0)
                    {
                        dpsLine.Text = startColour + totalDPS + endColour + "+" + (dpsAmmoOnly + dpsCritAmmoOnly) + " damage per second";
                        if (dpsCrit > 0)
                        {
                            if (dpsCritAmmoOnly > 0)
                            {
                                dpsLine.Text += " (" + dpsCrit + "+" + dpsCritAmmoOnly + " from crits)";
                            }
                            else
                            {
                                dpsLine.Text += " (" + dpsCrit + " from crits)";
                            }
                        }
                    }
                    else
                    {
                        dpsLine.Text = startColour + totalDPS + endColour + " damage per second";
                        if (dpsCrit > 0)
                        {
                            dpsLine.Text += " (" + dpsCrit + " from crits)";
                        }
                    }
                    tooltips.Insert(i + 1, dpsLine);
                    i++;
                }
                else if (line.Name == "Knockback" && line.Text.Length >= 10)
                {
                    if (item.knockBack > 0)
                    {
                        line.Text = startColour + item.knockBack.ToString("0.#") + endColour + " knockback (" + line.Text.Substring(0, line.Text.Length - 10) + ")"; //10 = 9 characters in knockback + space
                    }

                    if (ammoDamage > 0 && settings.showAmmunition)
                    {
                        TooltipLine l = new TooltipLine(Mod, "Ammo", "")
                        {
                            Text = "Using Ammunition " + ammoItem.Name
                        };
                        if (i < tooltips.Count - 1)
                        {
                            tooltips.Insert(i + 1, l);
                        }
                        else
                        {
                            tooltips.Add(l);
                        }
                    }
                }
            }

            // Player is not in an NPC's Shop
            if (Main.npcShop <= 0)
            {
                {
                    TooltipLine line = new TooltipLine(Mod, "Reforge", "");
                    reforgePriceTooltip(item, line);

                    if (line.Text != "")
                    {
                        tooltips.Add(line);
                    }
                }

                {
                    TooltipLine line = new TooltipLine(Mod, "Sell", "");
                    sellPriceTooltip(item, line);

                    if (line.Text != "")
                    {
                        tooltips.Add(line);
                    }
                }
            }

            if (settings.showModName && item.ModItem != null)
            {
                string startModColour = settings.showModName ? $"[c/{colourAsHexString(settings.modColour)}:" : "";
                string endModColour = settings.showModName ? "]" : "";
                tooltips.Add(new TooltipLine(Mod, "ModName", $"{startModColour}{item.ModItem.Mod.DisplayName}{endModColour}"));
            }
        }
    }
}
