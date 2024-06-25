using PaperMarioItems.Content.Items.Consumables;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace PaperMarioItems
{
	partial class PaperMarioItems : Mod
	{
        //custom sounds
        public readonly static SoundStyle useItemPM = new SoundStyle("PaperMarioItems/Assets/Sounds/useItemPM");
        public readonly static SoundStyle luckyPM = new SoundStyle("PaperMarioItems/Assets/Sounds/luckyPM");
        public readonly static SoundStyle healPM = new SoundStyle("PaperMarioItems/Assets/Sounds/healPM");
        public readonly static SoundStyle starPM = new SoundStyle("PaperMarioItems/Assets/Sounds/starPM");
        //custom recipe groups
        public override void AddRecipeGroups()
        {
            RecipeGroup mushgroup = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.Mushroom)}", ItemID.Mushroom, ItemID.TealMushroom, ItemID.GlowingMushroom, ItemID.GreenMushroom);
            RecipeGroup.RegisterGroup(nameof(ItemID.Mushroom), mushgroup);
            RecipeGroup syrupgroup = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {Language.GetTextValue($"Mods.PaperMarioItems.Content.SyrupGroup")}", ModContent.ItemType<HoneySyrup>(), ModContent.ItemType<MapleSyrup>(), ModContent.ItemType<JamminJelly>());
            RecipeGroup.RegisterGroup(Language.GetTextValue($"Mods.PaperMarioItems.Content.SyrupGroup"), syrupgroup);
            RecipeGroup flowergroup = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {Language.GetTextValue($"Mods.PaperMarioItems.Content.FlowerGroup")}", ItemID.Daybloom, ItemID.Blinkroot, ItemID.Fireblossom, ItemID.Moonglow, ItemID.Shiverthorn, ItemID.Sunflower, ItemID.SkyBlueFlower, ItemID.YellowMarigold);
            RecipeGroup.RegisterGroup(Language.GetTextValue($"Mods.PaperMarioItems.Content.FlowerGroup"), flowergroup);
        }

        public override void Load()
        {

        }
    }
}