using PaperMarioItems.Content.Items;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace PaperMarioItems.Content
{
	public static class PaperMarioConditions
	{
        public static Condition HasCookbook = new Condition(Language.GetTextValue($"Mods.PaperMarioItems.Items.Cookbook.DisplayName"), ()
            => Main.LocalPlayer.HasItem(ModContent.ItemType<Cookbook>()));
    }
}