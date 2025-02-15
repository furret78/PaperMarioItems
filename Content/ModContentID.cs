using PaperMarioItems.Content.Buffs;
using PaperMarioItems.Content.Dusts;
using PaperMarioItems.Content.Items;
using PaperMarioItems.Content.Items.Consumables;
using PaperMarioItems.Content.Items.ConsumablesSPM;
using PaperMarioItems.Content.Items.Cooking;
using PaperMarioItems.Content.Items.Placeable;
using PaperMarioItems.Content.Items.Treasure;
using PaperMarioItems.Content.Items.Weapons;
using PaperMarioItems.Content.Projectiles;
using PaperMarioItems.Content.Tiles;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.ModLoader;

namespace PaperMarioItems.Content
{
    //items
    public class PMItemID
    {
        public static readonly int 
            //normal items - TTYD
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
            SleepySheep = ModContent.ItemType<SleepySheep>(),
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
            //key items - TTYD
            BattleTrunks = ModContent.ItemType<BattleTrunks>(),
            PackageBox = ModContent.ItemType<PackageBox>(),
            SkullGem = ModContent.ItemType<SkullGem>(),
            Necklace = ModContent.ItemType<Necklace>(),
            MoonStone = ModContent.ItemType<MoonStone>(),
            StarStone = ModContent.ItemType<StarStone>(),
            SunStone = ModContent.ItemType<SunStone>(),
            PuniOrb = ModContent.ItemType<PuniOrb>(),
            ShellEarrings = ModContent.ItemType<ShellEarrings>(),
            GoldRing = ModContent.ItemType<GoldRing>(),
            Blanket = ModContent.ItemType<Blanket>(),
            BlimpTicket = ModContent.ItemType<BlimpTicket>(),
            TrainTicket = ModContent.ItemType<TrainTicket>(),
            LotteryTicket = ModContent.ItemType<LotteryTicket>(),
            WrestlingMagazine = ModContent.ItemType<WrestlingMagazine>(),
            ChampsBelt = ModContent.ItemType<ChampsBelt>(),
            VitalPaper = ModContent.ItemType<VitalPaper>(),
            GoldbobsPass = ModContent.ItemType<GoldbobsPass>(),
            DubiousPaper = ModContent.ItemType<DubiousPaper>(),
            PresentPaper = ModContent.ItemType<PresentPaper>(),
            PresentBox = ModContent.ItemType<PresentBox>(),
            Superbombomb = ModContent.ItemType<Superbombomb>(),
            DataDisk = ModContent.ItemType<DataDisk>(),
            UltraStone = ModContent.ItemType<UltraStone>(),
            SilverCard = ModContent.ItemType<SilverCard>(),
            GoldCard = ModContent.ItemType<GoldCard>(),
            PlatinumCard = ModContent.ItemType<PlatinumCard>(),
            SpecialCard = ModContent.ItemType<SpecialCard>(),
            Briefcase = ModContent.ItemType<Briefcase>(),
            VintageRed = ModContent.ItemType<VintageRed>(),
            ShineSprite = ModContent.ItemType<ShineSprite>(),
            //cooking - TTYD
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
            PoisonedCake = ModContent.ItemType<PoisonedCake>(),

            //normal items - SPM
            BlackApple = ModContent.ItemType<BlackApple>(),
            BlockBlock = ModContent.ItemType<BlockBlock>(), //todo, not yet declared in changelog
            BlueApple = ModContent.ItemType<BlueApple>(),
            BoneinCut = ModContent.ItemType<BoneinCut>(),
            DayzeeTear = ModContent.ItemType<DayzeeTear>(),
            HPPlus = ModContent.ItemType<HPPlus>(),
            LongLastShake = ModContent.ItemType<LongLastShake>(),
            MightyTonic = ModContent.ItemType<MightyTonic>(),
            MushroomShake = ModContent.ItemType<MushroomShake>(),
            OrangeApple = ModContent.ItemType<OrangeApple>(),
            MildCocoaBean = ModContent.ItemType<MildCocoaBean>(),
            PinkApple = ModContent.ItemType<PinkApple>(),
            PowerPlus = ModContent.ItemType<PowerPlus>(),
            PowerMinus = ModContent.ItemType<PowerMinus>(),
            PrimordialFruit = ModContent.ItemType<PrimordialFruit>(),
            SapSoup = ModContent.ItemType<SapSoup>(),
            SlimyMushroom = ModContent.ItemType<SlimyMushroom>(),
            SuperMushroomShake = ModContent.ItemType<SuperMushroomShake>(),
            UltraMushroomShake = ModContent.ItemType<UltraMushroomShake>(),
            //cooking - SPM

            //tiles - SPM
            CastleBleckBrick = ModContent.ItemType<CastleBleckBrick>(),
            //key items - SPM
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
            Allergic = ModContent.BuffType<AllergicBuff>(),
            Sleep = ModContent.BuffType<SleepDebuff>(),
            Electrified = ModContent.BuffType<ElectrifiedBuff>(),
            PowerUp = ModContent.BuffType<PowerUpBuff>(),
            BlockBlock = ModContent.BuffType<BlockBlockBuff>(),
            PartnerBuff = ModContent.BuffType<PartnerBuff>();

        public static readonly List<int> debuffList = [
            Dizzy, Soft, Timestop, Sleep
            ];
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

    //sounds
    public class PMSoundID : ModSystem
    {
        private const float soundVolume = 0.5f, soundVolume2 = soundVolume / 2;
        public static readonly SoundStyle causeStatus = PaperMarioItems.causeStatusPM with { Volume = soundVolume },
            damage = PaperMarioItems.damagePM with { Volume = soundVolume },
            dizzy = PaperMarioItems.dizzyPM with { Volume = soundVolume },
            fireFlower = PaperMarioItems.fireFlowerPM with { Volume = soundVolume },
            heal = PaperMarioItems.healPM with { Volume = soundVolume },
            lucky = PaperMarioItems.luckyPM with { Volume = soundVolume },
            star = PaperMarioItems.starPM with { Volume = soundVolume, PlayOnlyIfFocused = true },
            thunder = PaperMarioItems.thunderPM with { Volume = soundVolume },
            useItem = PaperMarioItems.useItemPM with { Volume = soundVolume },
            useItem2 = PaperMarioItems.useItemSPM with { Volume = soundVolume },
            stopwatch = PaperMarioItems.watchPM with { Volume = soundVolume },
            invisible = PaperMarioItems.invisPM with { Volume = soundVolume },
            recover = PaperMarioItems.recoverPM with { Volume = soundVolume },
            wrong = PaperMarioItems.wrongPM with { Volume = soundVolume },
            fullHeal = PaperMarioItems.fullHealPM with { Volume = soundVolume },
            fullMana = PaperMarioItems.fullManaPM with { Volume = soundVolume },
            danger = PaperMarioItems.dangerPM with { Volume = soundVolume2, PlayOnlyIfFocused = true },
            peril = PaperMarioItems.perilPM with { Volume = soundVolume2, PlayOnlyIfFocused = true },
            returnPipe = PaperMarioItems.returnPipeSPM with { Volume = soundVolume },
            charged = PaperMarioItems.chargedPM with { Volume = soundVolume };
    }

    //tiles
    public class PMTileID : ModSystem
    {
        public static readonly int CastleBleckBrick = ModContent.TileType<CastleBleckBrickTile>();
    }

    public class ImportantThings : ModSystem
    {
        public static readonly List<int> ImportantThingsList = new()
        {
            PMItemID.BattleTrunks,
            PMItemID.PackageBox,
            PMItemID.SkullGem,
            PMItemID.Necklace,
            PMItemID.MoonStone,
            PMItemID.StarStone,
            PMItemID.SunStone,
            PMItemID.PuniOrb,
            PMItemID.ShellEarrings,
            PMItemID.GoldRing,
            PMItemID.Blanket,
            PMItemID.BlimpTicket,
            PMItemID.TrainTicket,
            PMItemID.LotteryTicket,
            PMItemID.WrestlingMagazine,
            PMItemID.ChampsBelt,
            PMItemID.VitalPaper,
            PMItemID.GoldbobsPass,
            PMItemID.DubiousPaper,
            PMItemID.PresentPaper,
            PMItemID.PresentBox,
            PMItemID.Superbombomb,
            PMItemID.DataDisk,
            PMItemID.UltraStone,
            PMItemID.SilverCard,
            PMItemID.GoldCard,
            PMItemID.PlatinumCard,
            PMItemID.SpecialCard,
            PMItemID.Briefcase,
            PMItemID.VintageRed,
            PMItemID.ShineSprite,
        };
    }
}