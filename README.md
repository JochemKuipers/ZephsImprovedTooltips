# Zeph's Improved Tooltips

A Terraria tModLoader mod that modifies and improves the vanilla item tooltips.

This is a singleplayer/client-side only mod and can be used when playing on any server.
Servers with this mod installed will send it to any clients without the mod.

## Example

**Before**

```text
Enchanted Boomerang
13 melee damage
4% critical strike chance
Very Fast speed
Very strong knockback
```

**After**

```text
Enchanted Boomerang
13 melee damage
4% critical strike chance
4 attacks per second (Very Fast)
56 damage per second (4 from crits)
61 measured damage per second
8 knockback (Very strong)
Reforges for 1 gold 66 silver 66 copper
Sells for 1 gold
```

## Features

- Damage per second
- Accurate crit chance calculations for DPS
- Accurate damage calculations for DPS
- Attack speed, taking into account weapon speed buffs, auto swing, and delays
- Attack speed also factors in how melee projectile weapons work
- Ranged weapons use their current ammo for damage calculations
- Ranged weapons display their current ammo
- Knockback value displayed
- Reforge price shown on all valid items
- Sell price shown on all valid items
- The mod that an item came from is displayed
- Measured DPS: shows the real damage per second a weapon dealt while you used it, including secondary effects like split projectiles, explosions, and multi-hits
- Configure various settings and colours

## Credits

- [Zephilinox](https://github.com/Zephilinox) — original mod author

### Port Disclosure

This is a port of Zephilinox's original 1.3 mod to modern tModLoader (Terraria 1.4.5).
Original project: [Zephilinox/ZephsImprovedTooltips](https://github.com/Zephilinox/ZephsImprovedTooltips)

### AI Disclosure

The port and the new measured DPS feature were written with the assistance of an AI coding assistant (Cursor), and were manually reviewed and tested in-game.

## Changelog

### v0.18.0 — 2026/06/10

- Ported to tModLoader for Terraria 1.4.5
- DPS calculations now use the modern damage class system, so damage and crit bonuses from any source (vanilla or modded, any damage class) are included automatically
- Attack speed now uses the modern attack speed system
- Added measured DPS: tracks the real damage per second a weapon deals while you use it, including secondary effects like split projectiles, explosions, and multi-hits
- Added option to enable/disable measured DPS
- Config labels now use tModLoader's localization system
- Note: config settings will reset to defaults once, due to internal renames

### v0.17.0 — 2022/01/19

- Added various configuration settings and colours
- Added option to highlight some key tooltip numbers
- Added ability to show the mod that an item is from, and an option to disable it
- Added option to enable/disable the tooltip line displaying the currently selected ammo for this weapon
- Added option to change when the reforge information is displayed
- Added option to change when the sell price information is displayed
- Changed the text for the reforge and sell price, to match NPC shop text

### v0.16.0 — 2019/07/27

- Fixed Reforge tooltip showing for Vanity items
- Fixed Reforge tooltip showing for Armour
- Now checks to see if some pre-defined tooltips have been changed by a mod to prevent some crashes

### v0.15 — 2019/07/05

- Added reforge price once the goblin tinkerer is rescued

### v0.14 — 2019/07/05

- Fixed bug introduced in v0.11 for melee projectile weapon attack speed
- Added ranged weapon ammo calculations
- Added current ammo being used in ranged weapon tooltip
- Fixed DPS for pickaxes/hammers/axes

### v0.13 — 2019/07/05

- Fixed bug in DPS relating to player weapon damage buffs

### v0.12 — 2019/07/05

- Added sell price, matching vanilla store
- Fixed debug spam to chat, sorry about that

### v0.11 — 2019/07/04

- Fixed incorrect attack speed with wooden sword/light's bane
- Reworded knockback
- Reworded speed
- Knockback rounds to the first decimal place

### v0.10.1 — Icon — 2019/07/04

- Forced an update so the icon can be uploaded

### v0.10 — Initial Release — 2019/07/04

- Damage per second
- Accurate crit chance calculations for DPS
- Accurate damage calculations for DPS
- Attack speed, taking into account melee speed, auto swing, and weapon attack delays
- Attack speed also factors in how melee projectile weapons work
- Raw knockback value displayed next to knockback strength
