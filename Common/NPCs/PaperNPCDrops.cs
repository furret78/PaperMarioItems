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
                npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<PointSwap>(), 25, 23));
                npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<SpitePouch>(), 29, 27));
                npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<InnCoupon>(), 25, 23));
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
        }
    }
}