using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Consumables
{ 
	public class MysteryBox : ModItem
	{
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 500;
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 40;
            Item.useTurn = true;
            Item.useTime = 17;
            Item.useAnimation = Item.useTime;
            Item.consumable = true;
            Item.noUseGraphic = true;
            Item.UseSound = SoundID.Grab;
            Item.maxStack = 64;
            Item.rare = ItemRarityID.White;
            Item.value = Item.buyPrice(silver: 3);
        }

        public override bool CanRightClick() => true;

        public override void RightClick(Player player)
        {
            player.QuickSpawnItem(player.GetSource_ItemUse(Item), ChooseItem());
        }

        private int ChooseItem()
        {
            int resultItem = PMItemID.Mushroom;
            switch (Main.rand.Next(11))
            {
                case 0:
                    {
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
                        resultItem = ItemID.GoldWatch;
                        if (NPC.downedPlantBoss) resultItem = PMItemID.Stopwatch;
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
                        resultItem = ItemID.ManaCrystal;
                        if (Main.hardMode) resultItem = PMItemID.ShootingStar;
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
            return resultItem;
        }
    }
}
