using Terraria;
using Terraria.Localization;

namespace PaperMarioItems.Content
{
	public static class PaperMarioConditions
	{
        public static Condition HasCookbook = new Condition(Language.GetTextValue($"Mods.PaperMarioItems.Items.Cookbook.DisplayName"), ()
            => Main.LocalPlayer.HasItem(PMItemID.Cookbook));
    }
}