using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace ZephsImprovedTooltips
{

//Tracks the real damage dealt per weapon while the local player uses it.
//Unlike the theoretical DPS calculation, this captures secondary effects
//(split projectiles, on-hit explosions, multi-hits, etc.) because it measures actual hits.
public static class DpsTracker
{
    private class WeaponData
    {
        public long sessionDamage;
        public uint sessionStartTick;
        public uint lastHitTick;
        public float measuredDps;
    }

    //after this many ticks without a hit, the next hit starts a fresh measurement session
    private const uint SessionTimeoutTicks = 180;
    //pretend a session lasts at least a second so the first few hits don't show an inflated value
    private const uint MinSessionTicks = 60;

    private static readonly Dictionary<int, WeaponData> WeaponDamage = [];

    public static void RecordDamage(int itemType, int damageDone)
    {
        if (itemType <= 0 || damageDone <= 0)
            return;

        var now = Main.GameUpdateCount;

        if (!WeaponDamage.TryGetValue(itemType, out var data))
        {
            data = new WeaponData();
            WeaponDamage[itemType] = data;
        }

        if (now - data.lastHitTick > SessionTimeoutTicks)
        {
            data.sessionDamage = 0;
            data.sessionStartTick = now;
        }

        data.sessionDamage += damageDone;
        data.lastHitTick = now;

        var elapsed = Math.Max(MinSessionTicks, now - data.sessionStartTick);
        data.measuredDps = data.sessionDamage * 60f / elapsed;
    }

    public static bool TryGetMeasuredDps(int itemType, out int dps)
    {
        if (WeaponDamage.TryGetValue(itemType, out var data) && data.measuredDps > 0)
        {
            dps = (int)data.measuredDps;
            return true;
        }

        dps = 0;
        return false;
    }

    public static void Clear()
    {
        WeaponDamage.Clear();
    }
}

//Remembers which weapon spawned each projectile, following parent chains so that
//secondary projectiles (splits, explosions, minion attacks) credit the original weapon
[UsedImplicitly]
public class DpsTrackerProjectile : GlobalProjectile
{
    public override bool InstancePerEntity => true;

    private int _sourceItemType = -1;

    public override void OnSpawn(Projectile projectile, IEntitySource source)
    {
        _sourceItemType = source switch
        {
            //also covers EntitySource_ItemUse_WithAmmo
            EntitySource_ItemUse itemUse => itemUse.Item.type,
            EntitySource_Parent { Entity: Projectile parent } when parent.TryGetGlobalProjectile(
                out DpsTrackerProjectile parentTracker) => parentTracker._sourceItemType,
            _ => _sourceItemType
        };
    }

    public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (projectile.owner == Main.myPlayer)
            DpsTracker.RecordDamage(_sourceItemType, damageDone);
    }
}

[UsedImplicitly]
public class DpsTrackerSystem : ModSystem
{
    public override void OnWorldUnload()
    {
        DpsTracker.Clear();
    }
}

}
