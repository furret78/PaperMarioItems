using PaperMarioItems.Content;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Common.NPCs
{
    public partial class PaperNPCDrops : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (!NPCID.Sets.CountsAsCritter[npc.type])
            {
                if (!npc.boss && !NPCID.Sets.ShouldBeCountedAsBoss[npc.type])
                {
                    npcLoot.Add(ItemDropRule.NormalvsExpert(PMItemID.PointSwap, 47, 44));
                    npcLoot.Add(ItemDropRule.NormalvsExpert(PMItemID.SpitePouch, 57, 55));
                    npcLoot.Add(ItemDropRule.NormalvsExpert(PMItemID.InnCoupon, 85, 78));
                    npcLoot.Add(ItemDropRule.NormalvsExpert(PMItemID.FrightMask, 60, 55));
                    npcLoot.Add(ItemDropRule.NormalvsExpert(PMItemID.MysteryBox, 54, 50));
                    npcLoot.Add(ItemDropRule.NormalvsExpert(PMItemID.MrSoftener, 64, 60));
                    npcLoot.Add(ItemDropRule.NormalvsExpert(PMItemID.POWBlock, 100, 98));
                    npcLoot.Add(ItemDropRule.NormalvsExpert(PMItemID.PackageBox, 370, 320));
                }
                else if (!NPCID.Sets.ShouldBeCountedAsBoss[npc.type])
                {
                    npcLoot.Add(ItemDropRule.NormalvsExpert(PMItemID.PackageBox, 250, 200));
                }
                if (npc.type != NPCID.Harpy && npc.type != NPCID.Vulture && npc.type != NPCID.Raven)
                    npcLoot.Add(ItemDropRule.NormalvsExpert(PMItemID.MysticEgg, 580, 480));
            }
            if (npc.type == NPCID.Harpy) npcLoot.Add(ItemDropRule.NormalvsExpert(PMItemID.MysticEgg, 36, 33));
            if (npc.type == NPCID.Vulture) npcLoot.Add(ItemDropRule.NormalvsExpert(PMItemID.MysticEgg, 110, 108));
            if (npc.type == NPCID.Raven) npcLoot.Add(ItemDropRule.NormalvsExpert(PMItemID.MysticEgg, 80, 76));
            if (npc.type == NPCID.BlueSlime || npc.type == NPCID.YellowSlime || npc.type == NPCID.GoldenSlime || npc.type == NPCID.DungeonSlime)
                npcLoot.Add(ItemDropRule.Common(PMItemID.WhackaBump, 177));
            if (npc.type == NPCID.Pinky || npc.type == NPCID.ShimmerSlime)
                npcLoot.Add(ItemDropRule.NormalvsExpert(PMItemID.WhackaBump, 45, 40));
            if (npc.type == NPCID.Ghost || npc.type == NPCID.Wraith)
            {
                npcLoot.Add(ItemDropRule.NormalvsExpert(PMItemID.BoosSheet, 11, 9));
                npcLoot.Add(ItemDropRule.NormalvsExpert(PMItemID.RepelCape, 13, 11));
            }
            if (npc.type == NPCID.Reaper || npc.type == NPCID.Poltergeist || npc.type == NPCID.ShadowFlameApparition || npc.type == NPCID.PirateGhost)
            {
                npcLoot.Add(ItemDropRule.NormalvsExpert(PMItemID.BoosSheet, 9, 8));
                npcLoot.Add(ItemDropRule.NormalvsExpert(PMItemID.RepelCape, 11, 9));
            }
            if (npc.type == NPCID.StardustSoldier)
                npcLoot.Add(ItemDropRule.NormalvsExpert(PMItemID.ShootingStar, 12, 9));
            if (npc.type == NPCID.CaveBat || npc.type == NPCID.GiantBat || npc.type == NPCID.IceBat || npc.type == NPCID.IlluminantBat || npc.type == NPCID.JungleBat || npc.type == NPCID.SporeBat || npc.type == NPCID.VampireBat || npc.type == NPCID.Hellbat || npc.type == NPCID.Lavabat)
            {
                npcLoot.Add(ItemDropRule.NormalvsExpert(PMItemID.DizzyDial, 59, 57));
                npcLoot.Add(ItemDropRule.NormalvsExpert(PMItemID.HPDrain, 40, 35));
            }
            if (npc.type == NPCID.GraniteFlyer || npc.type == NPCID.GraniteGolem)
                npcLoot.Add(ItemDropRule.NormalvsExpert(PMItemID.EarthQuake, 2, 1));
            //boss specific drops
            if (System.Array.IndexOf([NPCID.EaterofWorldsBody, NPCID.EaterofWorldsHead, NPCID.EaterofWorldsTail], npc.type) > -1)
            {
                LeadingConditionRule leadingConditionRule = new(new Conditions.LegacyHack_IsABoss());
                leadingConditionRule.OnSuccess(ItemDropRule.NormalvsExpert(PMItemID.DizzyDial, 3, 2));
                npcLoot.Add(leadingConditionRule);
            }
            if (npc.type == NPCID.BrainofCthulhu)
            {
                LeadingConditionRule leadingConditionRule = new(new Conditions.LegacyHack_IsABoss());
                leadingConditionRule.OnSuccess(ItemDropRule.NormalvsExpert(PMItemID.DizzyDial, 3, 2));
                npcLoot.Add(leadingConditionRule);
            }
        }
    }
}