using PaperMarioItems.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace PaperMarioItems
{
	partial class PaperMarioItems : Mod
    {
        const string PMSoundPath = "PaperMarioItems/Assets/Sounds/";
        //custom sounds
        public readonly static SoundStyle useItemPM = new(PMSoundPath + "useItemPM"),
            useItemSPM = new(PMSoundPath + "useItemSPM"),
            luckyPM = new(PMSoundPath + "luckyPM"),
            healPM = new(PMSoundPath + "healPM"),
            starPM = new(PMSoundPath + "starPM"),
            thunderPM = new(PMSoundPath + "thunderPM"),
            watchPM = new(PMSoundPath + "watchPM"),
            dizzyPM = new(PMSoundPath + "dizzyPM"),
            fireFlowerPM = new(PMSoundPath + "fireFlowerPM"),
            causeStatusPM = new(PMSoundPath + "causeStatusPM"),
            chargedPM = new(PMSoundPath + "chargedPM"),
            invisPM = new(PMSoundPath + "invisPM"),
            recoverPM = new(PMSoundPath + "recoverPM"),
            wrongPM = new(PMSoundPath + "wrongPM"),
            fullHealPM = new(PMSoundPath + "fullHealPM"),
            fullManaPM = new(PMSoundPath + "fullManaPM"),
            dangerPM = new(PMSoundPath + "dangerPM"),
            perilPM = new(PMSoundPath + "perilPM"),
            returnPipeSPM = new(PMSoundPath + "returnPipeSPM"),
            damagePM = new(PMSoundPath + "damagePM");
        //custom recipe groups
        public override void AddRecipeGroups()
        {
            RecipeGroup mushgroup = new(() => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.Mushroom)}", ItemID.Mushroom, ItemID.TealMushroom, ItemID.GlowingMushroom, ItemID.GreenMushroom);
            RecipeGroup.RegisterGroup(nameof(ItemID.Mushroom), mushgroup);
            RecipeGroup syrupgroup = new(() => $"{Language.GetTextValue("LegacyMisc.37")} {Language.GetTextValue($"Mods.PaperMarioItems.Content.SyrupGroup")}", PMItemID.HoneySyrup, PMItemID.MapleSyrup, PMItemID.JamminJelly);
            RecipeGroup.RegisterGroup(Language.GetTextValue($"Mods.PaperMarioItems.Content.SyrupGroup"), syrupgroup);
            RecipeGroup flowergroup = new(() => $"{Language.GetTextValue("LegacyMisc.37")} {Language.GetTextValue($"Mods.PaperMarioItems.Content.FlowerGroup")}", ItemID.Daybloom, ItemID.Blinkroot, ItemID.Fireblossom, ItemID.Moonglow, ItemID.Shiverthorn, ItemID.Sunflower, ItemID.SkyBlueFlower, ItemID.YellowMarigold);
            RecipeGroup.RegisterGroup(Language.GetTextValue($"Mods.PaperMarioItems.Content.FlowerGroup"), flowergroup);
        }
        //NPCID sets
        public static bool[] IsGroundedBoss = NPCID.Sets.Factory.CreateBoolSet(NPCID.KingSlime, NPCID.Deerclops, NPCID.WallofFlesh, NPCID.QueenSlimeBoss, NPCID.Plantera, NPCID.Golem, NPCID.DD2OgreT2, NPCID.DD2OgreT3, NPCID.MourningWood, NPCID.Everscream, NPCID.SantaNK1);
        //checking if PaperMarioBadgesMod is loaded
        public static bool isLoadedBadgeMod;
        public override void PostSetupContent()
        {
            isLoadedBadgeMod = ModLoader.TryGetMod("PaperMarioBadgeMod", out Mod badgeMod);
        }
    }
}