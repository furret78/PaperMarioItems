using PaperMarioItems.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace PaperMarioItems.Content.Items.Treasure
{ 
	public class UltraStone : ModItem
	{
        public override void SetDefaults()
		{
			Item.width = 34;
			Item.height = 36;
			Item.value = Item.sellPrice(gold: 17);
			Item.rare = ItemRarityID.Red;
			Item.accessory = true;
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<PaperPlayer>().ultraStoneAccessory = true;

            if (!player.HasItem(PMItemID.ShineSprite)) return;
            int shineNum = player.CountItem(PMItemID.ShineSprite, 9999);

            player.GetDamage(DamageClass.Summon) += shineNum * (3 / 100f);
            player.GetDamage(DamageClass.Summon).Base += shineNum;
            player.GetKnockback(DamageClass.Summon).Base += (shineNum * (9 / 100f));
        }
    }
}
