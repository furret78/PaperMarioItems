using PaperMarioItems.Content;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Common.Players
{
    public class NotAllergicToBuffs : ModSystem
    {
        /// <summary>
        /// Status effects that the Allergic buff will not nullify
        /// </summary>
        private static List<int> notAllergicList = new();
        public override void PostSetupContent()
        {
            var buffList = new List<int>()
            {
                PMBuffID.Allergic, PMBuffID.Revived, BuffID.CoolWhipPlayerBuff, BuffID.WindPushed,
                BuffID.Sunflower, BuffID.WaterCandle, BuffID.ShadowCandle, BuffID.MonsterBanner,
                BuffID.Horrified, BuffID.TheTongue, BuffID.BallistaPanic, BuffID.Shimmer,
                BuffID.Campfire, BuffID.HeartLamp,
                //summons
                BuffID.AbigailMinion, BuffID.BabyBird, BuffID.BabySlime, BuffID.DeadlySphere, BuffID.StormTiger,
                BuffID.Smolstar, BuffID.FlinxMinion, BuffID.HornetMinion, BuffID.ImpMinion, BuffID.PirateMinion,
                BuffID.Pygmies, BuffID.Ravens, BuffID.BatOfLight, BuffID.SharknadoMinion, BuffID.SpiderMinion,
                BuffID.StardustMinion, BuffID.StardustDragonMinion, BuffID.EmpressBlade, BuffID.TwinEyesMinion,
                BuffID.UFOMinion, BuffID.VampireFrog,
                //mounts
                BuffID.BasiliskMount, BuffID.BeeMount, BuffID.WolfMount, BuffID.DarkMageBookMount,
                BuffID.BunnyMount, BuffID.CuteFishronMount, BuffID.DarkHorseMount, BuffID.DrillMount,
                BuffID.Flamingo, BuffID.WallOfFleshGoatMount, BuffID.GolfCartMount, BuffID.LavaSharkMount,
                BuffID.MajesticHorseMount, BuffID.PaintedHorseMount, BuffID.PigronMount, BuffID.PirateShipMount,
                BuffID.PogoStickMount, BuffID.Rudolph, BuffID.SantankMount, BuffID.ScutlixMount, BuffID.SlimeMount,
                BuffID.QueenSlimeMount, BuffID.SpookyWoodMount, BuffID.TurtleMount, BuffID.UFOMount,
                BuffID.UnicornMount, BuffID.WitchBroom,
                //minecarts
                BuffID.DiggingMoleMinecartLeft, BuffID.DiggingMoleMinecartRight, BuffID.FartMinecartLeft, BuffID.FartMinecartRight,
                BuffID.MinecartLeft, BuffID.MinecartRight, BuffID.MinecartLeftMech, BuffID.MinecartRightMech,
                BuffID.MinecartLeftWood, BuffID.MinecartRightWood, BuffID.AmberMinecartLeft, BuffID.AmberMinecartRight,
                BuffID.AmethystMinecartLeft, BuffID.AmethystMinecartRight, BuffID.BeeMinecartLeft, BuffID.BeeMinecartRight,
                BuffID.BeetleMinecartLeft, BuffID.BeetleMinecartRight, BuffID.CoffinMinecartLeft, BuffID.CoffinMinecartRight,
                BuffID.DesertMinecartLeft, BuffID.DesertMinecartRight, BuffID.DiamondMinecartLeft, BuffID.DiamondMinecartRight,
                BuffID.EmeraldMinecartLeft, BuffID.EmeraldMinecartRight, BuffID.FishMinecartLeft, BuffID.FishMinecartRight,
                BuffID.HellMinecartLeft, BuffID.HellMinecartRight, BuffID.LadybugMinecartLeft, BuffID.LadybugMinecartRight,
                BuffID.MeowmereMinecartLeft, BuffID.MeowmereMinecartRight, BuffID.PartyMinecartLeft, BuffID.PartyMinecartRight,
                BuffID.PigronMinecartLeft, BuffID.PigronMinecartRight, BuffID.PigronMinecartRight, BuffID.PirateMinecartRight,
                BuffID.RubyMinecartLeft, BuffID.RubyMinecartRight, BuffID.SapphireMinecartLeft, BuffID.SapphireMinecartRight,
                BuffID.ShroomMinecartLeft, BuffID.ShroomMinecartRight, BuffID.SteampunkMinecartLeft, BuffID.SteampunkMinecartRight,
                BuffID.SunflowerMinecartLeft, BuffID.SunflowerMinecartRight, BuffID.TopazMinecartLeft, BuffID.TopazMinecartRight,
                BuffID.TerraFartMinecartLeft, BuffID.TerraFartMinecartRight,
                //pets
                BuffID.BabyDinosaur, BuffID.BabyEater, BuffID.BabyFaceMonster, BuffID.BabyGrinch,
                BuffID.BabyHornet, BuffID.BabyImp, BuffID.BabyPenguin, BuffID.BabyRedPanda,
                BuffID.BabySkeletronHead, BuffID.BabySnowman, BuffID.BabyTruffle, BuffID.BabyWerewolf,
                BuffID.BerniePet, BuffID.BlackCat, BuffID.BlueChickenPet, BuffID.PetBunny,
                BuffID.CavelingGardener, BuffID.ChesterPet, BuffID.CompanionCube, BuffID.CursedSapling,
                BuffID.DirtiestBlock, BuffID.DynamiteKitten, BuffID.UpbeatStar, BuffID.EyeballSpring,
                BuffID.FennecFox, BuffID.GlitteryButterfly, BuffID.GlommerPet, BuffID.PetDD2Dragon,
                BuffID.JunimoPet, BuffID.LilHarpy, BuffID.PetLizard, BuffID.MiniMinotaur,
                BuffID.PetParrot, BuffID.PigPet, BuffID.Plantero, BuffID.PetDD2Gato,
                BuffID.Puppy, BuffID.PetSapling, BuffID.SpiderMinion, BuffID.ShadowMimic,
                BuffID.SharkPup, BuffID.Spiffo, BuffID.Squashling, BuffID.SugarGlider,
                BuffID.TikiSpirit, BuffID.PetTurtle, BuffID.VoltBunny, BuffID.ZephyrFish,
                //master mode pets
                BuffID.MartianPet, BuffID.DD2OgrePet, BuffID.EaterOfWorldsPet, BuffID.DestroyerPet,
                BuffID.EverscreamPet, BuffID.QueenBeePet, BuffID.IceQueenPet, BuffID.DD2BetsyPet,
                BuffID.SkeletronPrimePet, BuffID.MoonLordPet, BuffID.LunaticCultistPet, BuffID.PlanteraPet,
                BuffID.TwinsPet, BuffID.SkeletronPet, BuffID.KingSlimePet, BuffID.QueenSlimePet,
                BuffID.DualSlimePet, BuffID.BrainOfCthulhuPet, BuffID.EyeOfCthulhuPet, BuffID.DeerclopsPet,
                BuffID.DukeFishronPet,
                //light pets
                BuffID.ShadowOrb, BuffID.CrimsonHeart, BuffID.MagicLantern, BuffID.FairyQueenPet,
                BuffID.FairyBlue, BuffID.FairyGreen, BuffID.FairyRed, BuffID.PetDD2Ghost,
                BuffID.Wisp, BuffID.SuspiciousTentacle, BuffID.PumpkingPet, BuffID.GolemPet
            };

            for (int i = 0; i < buffList.Count; i++)
            {
                notAllergicList.Add(buffList[i]);
            }
        }
        public readonly static List<int> notAllergicToBuffs = notAllergicList;
    }
}