using PaperMarioItems.Content;
using Terraria;
using Terraria.Localization;

namespace PaperMarioItems.Common
{
	public static class PaperMarioConditions
	{
        public static Condition HasCookbook = new Condition(Language.GetTextValue($"Mods.PaperMarioItems.Items.Cookbook.DisplayName"), ()
            => Main.LocalPlayer.HasItem(PMItemID.Cookbook)),
        ExpertAndHardmode = new Condition(Language.GetOrRegister($"Mods.PaperMarioItems.Conditions.ExpertAndHardmode"), ()
            => Main.hardMode && Main.expertMode),
        ClassicAndHardmode = new Condition(Language.GetOrRegister($"Mods.PaperMarioItems.Conditions.NormalAndHardmode"), ()
            => Main.hardMode && !Main.expertMode);
    }
}