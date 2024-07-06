using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Consumables
{ 
	public class MysteryBox : ModItem
	{
        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 40;
            Item.useTurn = true;
            Item.useTime = 17;
            Item.useAnimation = Item.useTime;
            Item.consumable = true;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = PaperMarioItems.useItemPM;
            Item.maxStack = 64;
            Item.rare = ItemRarityID.White;
            Item.value = Item.sellPrice(silver: 20);
        }

        public override bool? UseItem(Player player)
        {
            int range = 11, PMItemID;
            switch (Main.rand.Next(range))
            {
                case 0:
                    {
                        PMItemID = ModContent.ItemType<Mushroom>();
                        if (NPC.downedBoss3) PMItemID = ModContent.ItemType<SuperMushroom>();
                        if (NPC.downedMechBossAny) PMItemID = ModContent.ItemType<UltraMushroom>();
                        break;
                    }
                case 1:
                    {
                        PMItemID = ModContent.ItemType<HoneySyrup>();
                        if (NPC.downedBoss3) PMItemID = ModContent.ItemType<MapleSyrup>();
                        if (NPC.downedMechBossAny) PMItemID = ModContent.ItemType<JamminJelly>();
                        break;
                    }
                case 2:
                    {
                        PMItemID = ModContent.ItemType<Stopwatch>();
                        break;
                    }
                case 3:
                    {
                        PMItemID = ModContent.ItemType<RepelCape>();
                        break;
                    }
                case 4:
                    {
                        PMItemID = ModContent.ItemType<FireFlower>();
                        break;
                    }
                case 5:
                    {
                        PMItemID = ModContent.ItemType<ShootingStar>();
                        break;
                    }
                case 6:
                    {
                        PMItemID = ModContent.ItemType<ThunderRage>();
                        break;
                    }
                case 7:
                    {
                        PMItemID = ModContent.ItemType<DriedMushroom>();
                        break;
                    }
                case 8:
                    {
                        PMItemID = ModContent.ItemType<VoltMushroom>();
                        break;
                    }
                case 9:
                    {
                        PMItemID = ModContent.ItemType<SlowMushroom>();
                        break;
                    }
                case 10:
                    {
                        PMItemID = ModContent.ItemType<GradualSyrup>();
                        break;
                    }
                default:
                    {
                        PMItemID = ModContent.ItemType<Mushroom>();
                        if (NPC.downedBoss1) PMItemID = ItemID.GoldCoin;
                        break;
                    }
            }
            player.QuickSpawnItem(player.GetSource_ItemUse(Item), PMItemID);
            return true;
        }
    }
}
