using PaperMarioItems.Content.Buffs;
using PaperMarioItems.Content.Items;
using PaperMarioItems.Content.Items.Consumables;
using PaperMarioItems.Content.Items.Cooking;
using Terraria.ModLoader;

namespace PaperMarioItems.Content
{
    //items
    public class PMItemID : ModSystem
    {
        public static readonly int BoosSheet = ModContent.ItemType<BoosSheet>(),
            RepelCape = ModContent.ItemType<RepelCape>(),
            PointSwap = ModContent.ItemType<PointSwap>(),
            CourageShell = ModContent.ItemType<CourageShell>(),
            DizzyDial = ModContent.ItemType<DizzyDial>(),
            DriedBouquet = ModContent.ItemType<DriedBouquet>(),
            DriedMushroom = ModContent.ItemType<DriedMushroom>(),
            EarthQuake = ModContent.ItemType<EarthQuake>(),
            FireFlower = ModContent.ItemType<FireFlower>(),
            FreshPasta = ModContent.ItemType<FreshPasta>(),
            FrightMask = ModContent.ItemType<FrightMask>(),
            GoldenLeaf = ModContent.ItemType<GoldenLeaf>(),
            GradualSyrup = ModContent.ItemType<GradualSyrup>(),
            HoneySyrup = ModContent.ItemType<HoneySyrup>(),
            Horsetail = ModContent.ItemType<Horsetail>(),
            HotSauce = ModContent.ItemType<HotSauce>(),
            HPDrain = ModContent.ItemType<HPDrain>(),
            InnCoupon = ModContent.ItemType<InnCoupon>(),
            JamminJelly = ModContent.ItemType<JamminJelly>(),
            LifeMushroom = ModContent.ItemType<LifeMushroom>(),
            MapleSyrup = ModContent.ItemType<MapleSyrup>(),
            MrSoftener = ModContent.ItemType<MrSoftener>(),
            Mushroom = ModContent.ItemType<Mushroom>(),
            MysteryBox = ModContent.ItemType<MysteryBox>(),
            MysticEgg = ModContent.ItemType<MysticEgg>(),
            POWBlock = ModContent.ItemType<POWBlock>(),
            PowerPunch = ModContent.ItemType<PowerPunch>(),
            Stopwatch = ModContent.ItemType<Stopwatch>(),
            SuperMushroom = ModContent.ItemType<SuperMushroom>(),
            SpitePouch = ModContent.ItemType<SpitePouch>(),
            ShootingStar = ModContent.ItemType<ShootingStar>(),
            SlowMushroom = ModContent.ItemType<SlowMushroom>(),
            RuinPowder = ModContent.ItemType<RuinPowder>(),
            TastyTonic = ModContent.ItemType<TastyTonic>(),
            ThunderBolt = ModContent.ItemType<ThunderBolt>(),
            ThunderRage = ModContent.ItemType<ThunderRage>(),
            TurtleyLeaf = ModContent.ItemType<TurtleyLeaf>(),
            UltraMushroom = ModContent.ItemType<UltraMushroom>(),
            VoltMushroom = ModContent.ItemType<VoltMushroom>(),
            WhackaBump = ModContent.ItemType<WhackaBump>(),
            CakeMix = ModContent.ItemType<CakeMix>(),
            Cookbook = ModContent.ItemType<Cookbook>(),

            ChocoCake = ModContent.ItemType<ChocoCake>(),
            CocoCandy = ModContent.ItemType<CocoCandy>(),
            FreshJuice = ModContent.ItemType<FreshJuice>(),
            FriedEgg = ModContent.ItemType<FriedEgg>(),
            FirePop = ModContent.ItemType<FirePop>(),
            ElectroPop = ModContent.ItemType<ElectroPop>(),
            HottestDog = ModContent.ItemType<HottestDog>(),
            InkPasta = ModContent.ItemType<InkPasta>(),
            InkySauce = ModContent.ItemType<InkySauce>(),
            Koopasta = ModContent.ItemType<Koopasta>(),
            KoopaTea = ModContent.ItemType<KoopaTea>(),
            MushroomBroth = ModContent.ItemType<MushroomBroth>(),
            MushroomCake = ModContent.ItemType<MushroomCake>(),
            MushroomCrepe = ModContent.ItemType<MushroomCrepe>(),
            MushroomFry = ModContent.ItemType<MushroomFry>(),
            MushroomRoast = ModContent.ItemType<MushroomRoast>(),
            MushroomSteak = ModContent.ItemType<MushroomSteak>(),
            MousseCake = ModContent.ItemType<MousseCake>(),
            PoisonMushroom = ModContent.ItemType<PoisonMushroom>(),
            Spaghetti = ModContent.ItemType<SpaghettiPlate>(),
            SpicyPasta = ModContent.ItemType<SpicyPasta>(),
            ZessFrappe = ModContent.ItemType<ZessFrappe>(),
            ZessTea = ModContent.ItemType<ZessTea>(),
            Mistake = ModContent.ItemType<Mistake>();
    }
    //buffs
    public class PMBuffID : ModSystem
    {
        public static readonly int Charged = ModContent.BuffType<ChargedBuff>(),
            Dizzy = ModContent.BuffType<DizzyDebuff>(),
            Dodgy = ModContent.BuffType<DodgyBuff>(),
            Huge = ModContent.BuffType<HugeBuff>(),
            Fright = ModContent.BuffType<FrightDebuff>(),
            Payback = ModContent.BuffType<PaybackBuff>(),
            Revived = ModContent.BuffType<RevivedBuff>(),
            Timestop = ModContent.BuffType<TimestopDebuff>(),
            Soft = ModContent.BuffType<SoftDebuff>(),
            Electrified = ModContent.BuffType<ElectrifiedBuff>();
    }
}