using Terraria;
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
            Item.UseSound = SoundID.Grab;
            Item.maxStack = 64;
            Item.rare = ItemRarityID.White;
            Item.value = Item.sellPrice(silver: 20);
        }

        public override bool? UseItem(Player player)
        {
            int range = 11, resultItem;
            switch (Main.rand.Next(range))
            {
                case 0:
                    {
                        resultItem = PMItemID.Mushroom;
                        if (NPC.downedBoss3) resultItem = PMItemID.SuperMushroom;
                        if (NPC.downedMechBossAny) resultItem = PMItemID.UltraMushroom;
                        break;
                    }
                case 1:
                    {
                        resultItem = PMItemID.HoneySyrup;
                        if (NPC.downedBoss3) resultItem = PMItemID.MapleSyrup;
                        if (NPC.downedMechBossAny) resultItem = PMItemID.JamminJelly;
                        break;
                    }
                case 2:
                    {
                        resultItem = PMItemID.Stopwatch;
                        break;
                    }
                case 3:
                    {
                        resultItem = PMItemID.RepelCape;
                        break;
                    }
                case 4:
                    {
                        resultItem = PMItemID.FireFlower;
                        break;
                    }
                case 5:
                    {
                        resultItem = PMItemID.ShootingStar;
                        break;
                    }
                case 6:
                    {
                        resultItem = PMItemID.ThunderRage;
                        break;
                    }
                case 7:
                    {
                        resultItem = PMItemID.DriedMushroom;
                        break;
                    }
                case 8:
                    {
                        resultItem = PMItemID.VoltMushroom;
                        break;
                    }
                case 9:
                    {
                        resultItem = PMItemID.SlowMushroom;
                        break;
                    }
                case 10:
                    {
                        resultItem = PMItemID.GradualSyrup;
                        break;
                    }
                default:
                    {
                        resultItem = ItemID.SilverCoin;
                        if (NPC.downedBoss1) resultItem = ItemID.GoldCoin;
                        if (NPC.downedEmpressOfLight) resultItem = ItemID.PlatinumCoin;
                        break;
                    }
            }
            player.QuickSpawnItem(player.GetSource_ItemUse(Item), resultItem);
            return true;
        }
    }
}
