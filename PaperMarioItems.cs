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
        public readonly static SoundStyle useItemPM = new("PaperMarioItems/Assets/Sounds/useItemPM"),
            luckyPM = new("PaperMarioItems/Assets/Sounds/luckyPM"),
            healPM = new("PaperMarioItems/Assets/Sounds/healPM"),
            starPM = new("PaperMarioItems/Assets/Sounds/starPM"),
            thunderPM = new("PaperMarioItems/Assets/Sounds/thunderPM"),
            watchPM = new("PaperMarioItems/Assets/Sounds/watchPM"),
            dizzyPM = new("PaperMarioItems/Assets/Sounds/dizzyPM"),
            fireFlowerPM = new("PaperMarioItems/Assets/Sounds/fireFlowerPM"),
            causeStatusPM = new("PaperMarioItems/Assets/Sounds/causeStatusPM"),
            chargedPM = new("PaperMarioItems/Assets/Sounds/chargedPM"),
            damagePM = new("PaperMarioItems/Assets/Sounds/damagePM");
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
        //NPCID sets
        public static SetFactory Factory = new SetFactory(NPCID.Count);
        public static bool[] IsGroundedBoss = Factory.CreateBoolSet(NPCID.KingSlime, NPCID.Deerclops, NPCID.WallofFlesh, NPCID.QueenSlimeBoss, NPCID.Plantera, NPCID.Golem, NPCID.DD2OgreT2, NPCID.DD2OgreT3, NPCID.MourningWood, NPCID.Everscream, NPCID.SantaNK1);
    }
}