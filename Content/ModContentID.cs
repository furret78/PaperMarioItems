using PaperMarioItems.Content.Buffs;
using PaperMarioItems.Content.Dusts;
using PaperMarioItems.Content.Items;
using PaperMarioItems.Content.Items.Consumables;
using PaperMarioItems.Content.Items.Cooking;
using PaperMarioItems.Content.Items.Weapons;
using PaperMarioItems.Content.Projectiles;
using Terraria.ModLoader;

namespace PaperMarioItems.Content
{
    //items
    public class PMItemID : ModSystem
    {
        public static readonly int Mistake = ModContent.ItemType<Mistake>(),
            BoosSheet = ModContent.ItemType<BoosSheet>(),
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
            CouplesCake = ModContent.ItemType<CouplesCake>(),
            CoconutBomb = ModContent.ItemType<CoconutBomb>(),
            CourageMeal = ModContent.ItemType<CourageMeal>(),
            FreshJuice = ModContent.ItemType<FreshJuice>(),
            FriedEgg = ModContent.ItemType<FriedEgg>(),
            FirePop = ModContent.ItemType<FirePop>(),
            FruitParfait = ModContent.ItemType<FruitParfait>(),
            ElectroPop = ModContent.ItemType<ElectroPop>(),
            EggBomb = ModContent.ItemType<EggBomb>(),
            HottestDog = ModContent.ItemType<HottestDog>(),
            HealthySalad = ModContent.ItemType<HealthySalad>(),
            HeartfulCake = ModContent.ItemType<HeartfulCake>(),
            HoneyCandy = ModContent.ItemType<HoneyCandy>(),
            HoneyMushroom = ModContent.ItemType<HoneyMushroom>(),
            HoneySuper = ModContent.ItemType<HoneySuper>(),
            HoneyUltra = ModContent.ItemType<HoneyUltra>(),
            InkPasta = ModContent.ItemType<InkPasta>(),
            InkySauce = ModContent.ItemType<InkySauce>(),
            IciclePop = ModContent.ItemType<IciclePop>(),
            JellyCandy = ModContent.ItemType<JellyCandy>(),
            JellyMushroom = ModContent.ItemType<JellyMushroom>(),
            JellySuper = ModContent.ItemType<JellySuper>(),
            JellyUltra = ModContent.ItemType<JellyUltra>(),
            Koopasta = ModContent.ItemType<Koopasta>(),
            KoopaTea = ModContent.ItemType<KoopaTea>(),
            KoopaBun = ModContent.ItemType<KoopaBun>(),
            LovePudding = ModContent.ItemType<LovePudding>(),
            MangoDelight = ModContent.ItemType<MangoDelight>(),
            MushroomBroth = ModContent.ItemType<MushroomBroth>(),
            MushroomCake = ModContent.ItemType<MushroomCake>(),
            MushroomCrepe = ModContent.ItemType<MushroomCrepe>(),
            MushroomFry = ModContent.ItemType<MushroomFry>(),
            MushroomRoast = ModContent.ItemType<MushroomRoast>(),
            MushroomSteak = ModContent.ItemType<MushroomSteak>(),
            MousseCake = ModContent.ItemType<MousseCake>(),
            MapleMushroom = ModContent.ItemType<MapleMushroom>(),
            MapleSuper = ModContent.ItemType<MapleSuper>(),
            MapleUltra = ModContent.ItemType<MapleUltra>(),
            MeteorMeal = ModContent.ItemType<MeteorMeal>(),
            OmeletteMeal = ModContent.ItemType<OmeletteMeal>(),
            PoisonMushroom = ModContent.ItemType<PoisonMushroom>(),
            PeachTart = ModContent.ItemType<PeachTart>(),
            SnowBunny = ModContent.ItemType<SnowBunny>(),
            SpicySoup = ModContent.ItemType<SpicySoup>(),
            Spaghetti = ModContent.ItemType<SpaghettiPlate>(),
            SpicyPasta = ModContent.ItemType<SpicyPasta>(),
            SpaceFood = ModContent.ItemType<SpaceFood>(),
            TrialStew = ModContent.ItemType<TrialStew>(),
            ZessCookie = ModContent.ItemType<ZessCookie>(),
            ZessDeluxe = ModContent.ItemType<ZessDeluxe>(),
            ZessDinner = ModContent.ItemType<ZessDinner>(),
            ZessSpecial = ModContent.ItemType<ZessSpecial>(),
            ZessFrappe = ModContent.ItemType<ZessFrappe>(),
            ZessTea = ModContent.ItemType<ZessTea>(),
            ZessDynamite = ModContent.ItemType<ZessDynamite>(),
            
            DeliciousCake = ModContent.ItemType<DeliciousCake>(),
            PoisonedCake = ModContent.ItemType<PoisonedCake>();
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
            Allergic = ModContent.BuffType<AllergicBuff>(),
            Electrified = ModContent.BuffType<ElectrifiedBuff>();
    }
    //projectiles
    public class PMProjID : ModSystem
    {
        public static readonly int CoconutBomb = ModContent.ProjectileType<CoconutBombProjectile>(),
            EggBomb = ModContent.ProjectileType<EggBombProjectile>(),
            CourageMeal = ModContent.ProjectileType<CourageMealProjectile>(),
            ZessDynamite = ModContent.ProjectileType<ZessDynamiteProjectile>(),
            Fireball = ModContent.ProjectileType<CustomFireball>();
    }
    //dusts
    public class PMDustID : ModSystem
    {
        public static readonly int BowserScare = ModContent.DustType<BowserScare>(),
            DizzyDust = ModContent.DustType<DizzyDust>(),
            ElectricDust = ModContent.DustType<ElectricDust>(),
            HPDrainDust = ModContent.DustType<HPDrainDust>(),
            LightningDust = ModContent.DustType<LightningDust>(),
            StopwatchDust = ModContent.DustType<StopwatchDust>();
    }
}