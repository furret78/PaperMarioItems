using PaperMarioItems.Content.Items.Consumables;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Common.NPCs
{
    public class PaperNPCDrops : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (!NPCID.Sets.CountsAsCritter[npc.type])
            {
                npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<PointSwap>(), 47, 44));
                npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<SpitePouch>(), 47, 45));
                npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<InnCoupon>(), 28, 25));
                npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<FrightMask>(), 60, 55));
                if (npc.type != NPCID.Harpy) npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<MysticEgg>(), 101, 100));
            }
            if (npc.type == NPCID.BlueSlime || npc.type == NPCID.YellowSlime || npc.type == NPCID.GoldenSlime || npc.type == NPCID.DungeonSlime)
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<WhackaBump>(), 90));
            if (npc.type == NPCID.Pinky || npc.type == NPCID.ShimmerSlime)
                npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<WhackaBump>(), 35, 30));
            if (npc.type == NPCID.Ghost || npc.type == NPCID.Wraith)
            {
                npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<BoosSheet>(), 11, 9));
                npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<RepelCape>(), 13, 11));
            }
            if (npc.type == NPCID.Reaper || npc.type == NPCID.Poltergeist || npc.type == NPCID.ShadowFlameApparition || npc.type == NPCID.PirateGhost)
            {
                npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<BoosSheet>(), 9, 8));
                npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<RepelCape>(), 11, 9));
            }
            if (npc.type == NPCID.Harpy)
                npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<MysticEgg>(), 26, 23));
            if (npc.type == NPCID.StardustSoldier)
                npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<ShootingStar>(), 12, 9));
            //boss specific drops
            if (System.Array.IndexOf(new int[] { NPCID.EaterofWorldsBody, NPCID.EaterofWorldsHead, NPCID.EaterofWorldsTail }, npc.type) > -1)
            {
                LeadingConditionRule leadingConditionRule = new(new Conditions.LegacyHack_IsABoss());
                leadingConditionRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<DizzyDial>(), 2));
                npcLoot.Add(leadingConditionRule);
            }
            if (npc.type == NPCID.BrainofCthulhu)
            {
                LeadingConditionRule leadingConditionRule = new(new Conditions.LegacyHack_IsABoss());
                leadingConditionRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<DizzyDial>(), 2));
                npcLoot.Add(leadingConditionRule);
            }
            if (npc.type == NPCID.CaveBat || npc.type == NPCID.GiantBat || npc.type == NPCID.IceBat || npc.type == NPCID.IlluminantBat || npc.type == NPCID.JungleBat || npc.type == NPCID.SporeBat || npc.type == NPCID.VampireBat || npc.type == NPCID.Hellbat || npc.type == NPCID.Lavabat)
                npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<DizzyDial>(), 35, 30));
        }
    }
}