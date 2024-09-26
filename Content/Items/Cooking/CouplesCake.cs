using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System.IO;
using Terraria.ModLoader.IO;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.Audio;

namespace PaperMarioItems.Content.Items.Cooking
{
	public class CouplesCake : ModItem
	{
        private const int effectTime = 15;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(effectTime);
        public static LocalizedText PlayerNameEmpty { get; private set; }
        public static LocalizedText PlayerNameOther { get; private set; }
        public static LocalizedText PlayerNameCreator { get; private set; }
        public string craftedPlayerName = string.Empty;
        public override void SetStaticDefaults()
        {
            PlayerNameEmpty = this.GetLocalization(nameof(PlayerNameEmpty));
            PlayerNameOther = this.GetLocalization(nameof(PlayerNameOther));
            PlayerNameCreator = this.GetLocalization(nameof(PlayerNameCreator));
            Item.ResearchUnlockCount = 50;
        }
        public override void SetDefaults()
		{
			Item.width = 39;
			Item.height = 39;
			Item.useTurn = true;
			Item.consumable = true;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.UseSound = SoundID.Item2;
            Item.useTime = 17;
			Item.useAnimation = Item.useTime;
			Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(silver: 30);
            Item.buffType = BuffID.Regeneration;
            Item.buffTime = effectTime * 60 * 60;
        }

        public override bool CanUseItem(Player player)
        {
            if (craftedPlayerName == string.Empty)
            {
                craftedPlayerName = player.name;
                SoundEngine.PlaySound(SoundID.ResearchComplete, player.Center);
            }
            if (craftedPlayerName != player.name) return true;
            return false;
        }

        public override void SaveData(TagCompound tag)
        {
            tag.Add("craftedPlayerName", craftedPlayerName);
        }

        public override void LoadData(TagCompound tag)
        {
            craftedPlayerName = tag.GetString("craftedPlayerName");
        }

        public override void NetSend(BinaryWriter writer)
        {
            writer.Write(craftedPlayerName);
        }

        public override void NetReceive(BinaryReader reader)
        {
            craftedPlayerName = reader.ReadString();
        }

        public override bool CanStack(Item source)
        {
            var name1 = craftedPlayerName;
            var name2 = ((CouplesCake)source.ModItem).craftedPlayerName;

            if (name1 == string.Empty)
            {
                name1 = Main.LocalPlayer.name;
            }
            if (name2 == string.Empty)
            {
                name2 = Main.LocalPlayer.name;
            }

            return name1 == name2;
        }

        public override void OnStack(Item source, int numToTransfer)
        {
            if (craftedPlayerName == string.Empty) craftedPlayerName = ((CouplesCake)source.ModItem).craftedPlayerName;
        }

        public override bool OnPickup(Player player)
        {
            if (craftedPlayerName == string.Empty) craftedPlayerName = player.name;
            return true;
        }

        public override void OnCreated(ItemCreationContext context)
        {
            if (context is RecipeItemCreationContext ||
                context is BuyItemCreationContext) craftedPlayerName = Main.LocalPlayer.name;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (craftedPlayerName != string.Empty)
            {
                if (Main.LocalPlayer.name == craftedPlayerName) tooltips.Add(new TooltipLine(Mod, "PlayerNameCreator", Language.GetTextValue(PlayerNameCreator.Format())));
                else tooltips.Add(new TooltipLine(Mod, "PlayerNameOther", Language.GetTextValue(PlayerNameOther.Format(craftedPlayerName))));
            }
            else tooltips.Add(new TooltipLine(Mod, "PlayerNameEmpty", Language.GetTextValue(PlayerNameEmpty.Format())));
        }
    }
}
