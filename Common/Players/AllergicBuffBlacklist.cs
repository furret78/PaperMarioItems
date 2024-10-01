using PaperMarioItems.Content;
using System.Collections.Generic;
using Terraria;
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
        public override void SetStaticDefaults()
        {
            var buffList = new List<int>()
            {
                PMBuffID.Allergic, PMBuffID.Revived, BuffID.CoolWhipPlayerBuff, BuffID.WindPushed,
                BuffID.Sunflower, BuffID.WaterCandle, BuffID.ShadowCandle, BuffID.MonsterBanner,
                BuffID.Horrified, BuffID.TheTongue, BuffID.BallistaPanic, BuffID.Shimmer,
                BuffID.Campfire, BuffID.HeartLamp, PMBuffID.Charged,
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
            };

            for (int i = 0; i < buffList.Count; i++)
            {
                notAllergicList.Add(buffList[i]);
            }
        }

        public override void PostSetupContent()
        {
            for (int i = 0; i < BuffLoader.BuffCount; i++)
            {
                if ((BuffID.Sets.BasicMountData[i] != null || Main.vanityPet[i] || Main.lightPet[i]) && !notAllergicList.Exists(x => x == i))
                {
                    notAllergicList.Add(i);
                }
            }
        }

        public readonly static List<int> notAllergicToBuffs = notAllergicList;
    }
}