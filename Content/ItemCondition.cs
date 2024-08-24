using PaperMarioItems.Content.Items;
using Terraria;
using Terraria.ModLoader;

namespace PaperMarioItems.Content
{
	public static class PaperMarioConditions
	{
        public static Condition HasCookbook = new Condition("Mods.PaperMarioItems.Items.Cookbook", ()
            => Main.LocalPlayer.HasItem(ModContent.ItemType<Cookbook>()));
    }
}