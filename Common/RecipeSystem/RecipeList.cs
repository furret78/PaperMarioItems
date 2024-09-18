using PaperMarioItems.Content;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Common.RecipeSystem
{
    public class RecipeList : ModSystem
    {
        public override void SetStaticDefaults()
        {
            var IngredientList = new List<PMRecipe>()
            {
                //Point Swap recipes
                new(PMItemID.BoosSheet, PMItemID.PointSwap, PMItemID.RepelCape),
                new(PMItemID.RepelCape, PMItemID.PointSwap, PMItemID.BoosSheet),
                new(PMItemID.CourageShell, PMItemID.PointSwap, PMItemID.MrSoftener),
                new(PMItemID.MrSoftener, PMItemID.PointSwap, PMItemID.CourageShell),
                new(PMItemID.EarthQuake, PMItemID.PointSwap, PMItemID.ThunderBolt),
                new(PMItemID.ThunderBolt, PMItemID.PointSwap, PMItemID.EarthQuake),
                new(PMItemID.HoneySyrup, PMItemID.PointSwap, PMItemID.GradualSyrup),
                new(PMItemID.HoneySyrup, PMItemID.PointSwap, PMItemID.Mushroom),
                new(PMItemID.JamminJelly, PMItemID.PointSwap, PMItemID.UltraMushroom),
                new(PMItemID.UltraMushroom, PMItemID.PointSwap, PMItemID.JamminJelly),
                new(PMItemID.UltraMushroom, PMItemID.PointSwap, PMItemID.PoisonMushroom),
                new(PMItemID.MapleSyrup, PMItemID.PointSwap, PMItemID.SuperMushroom),
                new(PMItemID.Mushroom, PMItemID.PointSwap, PMItemID.DriedMushroom),
                new(PMItemID.Mushroom, PMItemID.PointSwap, ItemID.GoldBar),
                new(PMItemID.Mushroom, PMItemID.PointSwap, PMItemID.HoneySyrup),
                new(PMItemID.PoisonMushroom, PMItemID.PointSwap, PMItemID.SlowMushroom),
                new(PMItemID.RuinPowder, PMItemID.PointSwap, PMItemID.SpitePouch),
                new(PMItemID.SpitePouch, PMItemID.PointSwap, PMItemID.RuinPowder),
                new(PMItemID.ShootingStar, PMItemID.PointSwap, PMItemID.ThunderRage),
                new(PMItemID.ThunderRage, PMItemID.PointSwap, PMItemID.ShootingStar),
                new(PMItemID.SuperMushroom, PMItemID.PointSwap, PMItemID.MapleSyrup),
                new(PMItemID.SuperMushroom, PMItemID.PointSwap, PMItemID.LifeMushroom),
                new(PMItemID.SuperMushroom, PMItemID.PointSwap, PMItemID.VoltMushroom),
                new(ItemID.FlowerofFrost, PMItemID.PointSwap, PMItemID.FireFlower, true),
                new(ItemID.FlowerofFrost, PMItemID.PointSwap, ItemID.FlowerofFire, true),
                //unique cooking (non-cookbook)
                new(PMItemID.TastyTonic, ItemID.Coconut),
                new(PMItemID.FreshJuice, PMItemID.HotSauce, PMItemID.HoneySyrup),
                new(PMItemID.FreshJuice, PMItemID.HotSauce, PMItemID.MapleSyrup),
                new(PMItemID.FreshJuice, PMItemID.HotSauce, PMItemID.JamminJelly),
                new(PMItemID.FreshJuice, PMItemID.HotSauce, ItemID.Peach),
                new(PMItemID.FreshJuice, PMItemID.HotSauce),
                new(PMItemID.FreshJuice, ItemID.Peach),
                new(PMItemID.FreshJuice, ItemID.Mango),
                new(PMItemID.FreshJuice, PMItemID.HoneySyrup),
                new(PMItemID.FreshJuice, PMItemID.MapleSyrup),
                new(PMItemID.FreshJuice, PMItemID.JamminJelly),
                new(PMItemID.FreshJuice, PMItemID.GradualSyrup),
                //unique cooking
                new(PMItemID.VoltMushroom, PMItemID.ThunderBolt, PMItemID.Mushroom),
                new(PMItemID.VoltMushroom, PMItemID.ThunderBolt, PMItemID.SuperMushroom),
                new(PMItemID.VoltMushroom, PMItemID.ThunderBolt, PMItemID.UltraMushroom),
                new(PMItemID.VoltMushroom, PMItemID.ThunderBolt, PMItemID.LifeMushroom),
                new(PMItemID.VoltMushroom, PMItemID.ThunderRage, PMItemID.Mushroom),
                new(PMItemID.VoltMushroom, PMItemID.ThunderRage, PMItemID.SuperMushroom),
                new(PMItemID.VoltMushroom, PMItemID.ThunderRage, PMItemID.UltraMushroom),
                new(PMItemID.VoltMushroom, PMItemID.ThunderRage, PMItemID.LifeMushroom),
                new(PMItemID.DriedMushroom, PMItemID.MrSoftener, PMItemID.Mushroom),
                new(PMItemID.DriedMushroom, PMItemID.MrSoftener, PMItemID.SuperMushroom),
                new(PMItemID.DriedMushroom, PMItemID.MrSoftener, PMItemID.UltraMushroom),
                new(PMItemID.DriedMushroom, PMItemID.MrSoftener, PMItemID.LifeMushroom),
                new(PMItemID.SlowMushroom, PMItemID.SlowMushroom, PMItemID.GradualSyrup),
                new(ItemID.Coconut, PMItemID.DriedBouquet, PMItemID.CoconutBomb),
                new(PMItemID.CoconutBomb, ItemID.Coconut, PMItemID.FireFlower),
                new(PMItemID.CourageMeal, PMItemID.CourageShell, PMItemID.ZessDeluxe),
                new(PMItemID.CourageMeal, PMItemID.CourageShell, PMItemID.ZessDinner),
                new(PMItemID.CourageMeal, PMItemID.CourageShell, PMItemID.ZessSpecial),
                new(PMItemID.FreshJuice, ItemID.Coconut, ItemID.Peach),
                new(PMItemID.FreshJuice, ItemID.Coconut, ItemID.Mango),
                new(PMItemID.FreshJuice, ItemID.Coconut, PMItemID.TurtleyLeaf),
                new(PMItemID.FreshJuice, PMItemID.GradualSyrup, PMItemID.TurtleyLeaf),
                new(PMItemID.FreshJuice, PMItemID.GradualSyrup, PMItemID.HoneySyrup),
                new(PMItemID.FreshJuice, PMItemID.GradualSyrup, PMItemID.MapleSyrup),
                new(PMItemID.FreshJuice, PMItemID.GradualSyrup, PMItemID.JamminJelly),
                new(PMItemID.FreshJuice, PMItemID.HoneySyrup, PMItemID.JamminJelly),
                new(PMItemID.FreshJuice, PMItemID.HoneySyrup, PMItemID.MapleSyrup),
                new(PMItemID.FreshJuice, PMItemID.HoneySyrup, PMItemID.TurtleyLeaf),
                new(PMItemID.FreshJuice, PMItemID.TurtleyLeaf, PMItemID.JamminJelly),
                new(PMItemID.FreshJuice, PMItemID.TurtleyLeaf, PMItemID.MapleSyrup),
                new(PMItemID.FirePop, PMItemID.CakeMix, PMItemID.FireFlower),
                new(PMItemID.FirePop, PMItemID.CakeMix, ItemID.FlowerofFire),
                new(PMItemID.FirePop, PMItemID.CakeMix, PMItemID.HotSauce),
                new(PMItemID.FruitParfait, ItemID.Mango, PMItemID.GradualSyrup),
                new(PMItemID.FruitParfait, ItemID.Mango, PMItemID.HoneySyrup),
                new(PMItemID.FruitParfait, ItemID.Mango, PMItemID.MapleSyrup),
                new(PMItemID.FruitParfait, ItemID.Mango, PMItemID.JamminJelly),
                new(PMItemID.FruitParfait, ItemID.Mango, ItemID.Peach),
                new(PMItemID.FruitParfait, ItemID.Peach, PMItemID.HoneySyrup),
                new(PMItemID.FruitParfait, ItemID.Peach, PMItemID.MapleSyrup),
                new(PMItemID.FruitParfait, ItemID.Peach, PMItemID.JamminJelly),
                new(PMItemID.ElectroPop, PMItemID.CakeMix, PMItemID.VoltMushroom),
                new(PMItemID.EggBomb, PMItemID.DriedBouquet, PMItemID.ZessDynamite),
                new(PMItemID.EggBomb, PMItemID.MysticEgg, PMItemID.FireFlower),
                new(PMItemID.ChocoCake, PMItemID.InkySauce, PMItemID.CakeMix),
                new(PMItemID.ChocoCake, PMItemID.InkySauce, PMItemID.MousseCake),
                new(PMItemID.CocoCandy, PMItemID.CakeMix, ItemID.Coconut),
                new(PMItemID.CouplesCake, PMItemID.SnowBunny, PMItemID.SpicySoup),
                new(PMItemID.FriedEgg, PMItemID.MysticEgg, ItemID.None, false, true),
                new(ItemID.FriedEgg, PMItemID.MysticEgg, ItemID.None, true),
                new(PMItemID.KoopaTea, PMItemID.TurtleyLeaf),
                new(PMItemID.HottestDog, PMItemID.HotSauce, ItemID.Hotdog),
                new(PMItemID.HealthySalad, PMItemID.TurtleyLeaf, PMItemID.GoldenLeaf),
                new(PMItemID.HealthySalad, PMItemID.TurtleyLeaf, PMItemID.Horsetail),
                new(PMItemID.HeartfulCake, PMItemID.RuinPowder, PMItemID.CakeMix),
                new(PMItemID.HeartfulCake, PMItemID.RuinPowder, ItemID.Peach),
                new(PMItemID.HoneyCandy, PMItemID.HoneySyrup, PMItemID.CakeMix),
                new(PMItemID.HoneyMushroom, PMItemID.HoneySyrup, PMItemID.Mushroom),
                new(PMItemID.HoneyMushroom, PMItemID.HoneySyrup, PMItemID.VoltMushroom),
                new(PMItemID.HoneyMushroom, PMItemID.HoneySyrup, PMItemID.SlowMushroom),
                new(PMItemID.HoneySuper, PMItemID.HoneySyrup, PMItemID.LifeMushroom),
                new(PMItemID.HoneySuper, PMItemID.HoneySyrup, PMItemID.SuperMushroom),
                new(PMItemID.HoneyUltra, PMItemID.HoneySyrup, PMItemID.UltraMushroom),
                new(PMItemID.Spaghetti, PMItemID.FreshPasta),
                new(PMItemID.Koopasta, PMItemID.Spaghetti, PMItemID.TurtleyLeaf),
                new(PMItemID.Koopasta, PMItemID.FreshPasta, PMItemID.TurtleyLeaf),
                new(PMItemID.SpicyPasta, PMItemID.Koopasta, PMItemID.HotSauce),
                new(PMItemID.SpicyPasta, PMItemID.FreshPasta, PMItemID.HotSauce),
                new(PMItemID.SpicyPasta, PMItemID.Spaghetti, PMItemID.HotSauce),
                new(PMItemID.InkPasta, PMItemID.Koopasta, PMItemID.InkySauce),
                new(PMItemID.InkPasta, PMItemID.FreshPasta, PMItemID.InkySauce),
                new(PMItemID.InkPasta, PMItemID.SpicyPasta, PMItemID.InkySauce),
                new(PMItemID.InkPasta, PMItemID.Spaghetti, PMItemID.InkySauce),
                new(PMItemID.InkySauce, PMItemID.HotSauce, PMItemID.FreshJuice),
                new(PMItemID.InkySauce, PMItemID.HotSauce, PMItemID.KoopaTea),
                new(PMItemID.InkySauce, PMItemID.HotSauce, PMItemID.MushroomBroth),
                new(PMItemID.InkySauce, PMItemID.HotSauce, PMItemID.TurtleyLeaf),
                new(PMItemID.InkySauce, PMItemID.HotSauce, PMItemID.ZessTea),
                new(PMItemID.InkySauce, PMItemID.HotSauce, PMItemID.TastyTonic),
                new(PMItemID.JellyCandy, PMItemID.JamminJelly, PMItemID.CakeMix),
                new(PMItemID.JellyMushroom, PMItemID.JamminJelly, PMItemID.VoltMushroom),
                new(PMItemID.JellyMushroom, PMItemID.JamminJelly, PMItemID.Mushroom),
                new(PMItemID.JellySuper, PMItemID.JamminJelly, PMItemID.LifeMushroom),
                new(PMItemID.JellySuper, PMItemID.JamminJelly, PMItemID.SlowMushroom),
                new(PMItemID.JellySuper, PMItemID.JamminJelly, PMItemID.SuperMushroom),
                new(PMItemID.JellyUltra, PMItemID.JamminJelly, PMItemID.UltraMushroom),
                new(PMItemID.KoopaBun, PMItemID.TurtleyLeaf, ItemID.Mango),
                new(PMItemID.KoopaTea, PMItemID.TurtleyLeaf),
                new(PMItemID.LifeMushroom, PMItemID.LifeMushroom, PMItemID.GoldenLeaf),
                new(PMItemID.LifeMushroom, PMItemID.LifeMushroom, PMItemID.TurtleyLeaf),
                new(PMItemID.LovePudding, PMItemID.MysticEgg, PMItemID.MangoDelight),
                new(PMItemID.MousseCake, PMItemID.CakeMix),
                new(PMItemID.MangoDelight, PMItemID.CakeMix, ItemID.Mango),
                new(PMItemID.MushroomBroth, PMItemID.TurtleyLeaf, PMItemID.PoisonMushroom),
                new(PMItemID.MushroomBroth, PMItemID.TurtleyLeaf, PMItemID.SlowMushroom),
                new(PMItemID.MushroomBroth, PMItemID.GoldenLeaf, PMItemID.PoisonMushroom),
                new(PMItemID.MushroomBroth, PMItemID.GoldenLeaf, PMItemID.SlowMushroom),
                new(PMItemID.MushroomCake, PMItemID.CakeMix, PMItemID.Mushroom),
                new(PMItemID.MushroomCake, PMItemID.CakeMix, PMItemID.SuperMushroom),
                new(PMItemID.MushroomCake, PMItemID.CakeMix, PMItemID.SlowMushroom),
                new(PMItemID.MushroomCake, PMItemID.CakeMix, PMItemID.LifeMushroom),
                new(PMItemID.MushroomCrepe, PMItemID.CakeMix, PMItemID.UltraMushroom),
                new(PMItemID.MushroomFry, PMItemID.Mushroom),
                new(PMItemID.MushroomFry, PMItemID.SuperMushroom),
                new(PMItemID.MushroomFry, PMItemID.DriedMushroom),
                new(PMItemID.MushroomFry, PMItemID.PoisonMushroom),
                new(PMItemID.MushroomFry, PMItemID.DriedMushroom, PMItemID.FireFlower),
                new(PMItemID.MushroomFry, PMItemID.DriedMushroom, PMItemID.VoltMushroom),
                new(PMItemID.MushroomFry, PMItemID.Mushroom, PMItemID.DriedMushroom),
                new(PMItemID.MushroomFry, PMItemID.Mushroom, PMItemID.GoldenLeaf),
                new(PMItemID.MushroomFry, PMItemID.Mushroom, PMItemID.TurtleyLeaf),
                new(PMItemID.MushroomRoast, PMItemID.Mushroom, PMItemID.FireFlower),
                new(PMItemID.MushroomRoast, PMItemID.Mushroom, PMItemID.SuperMushroom),
                new(PMItemID.MushroomRoast, PMItemID.Mushroom, PMItemID.VoltMushroom),
                new(PMItemID.MushroomRoast, PMItemID.Mushroom, PMItemID.GradualSyrup),
                new(PMItemID.MushroomRoast, PMItemID.SuperMushroom, PMItemID.DriedMushroom),
                new(PMItemID.MushroomRoast, PMItemID.SuperMushroom, PMItemID.VoltMushroom),
                new(PMItemID.MushroomRoast, PMItemID.SuperMushroom, PMItemID.TurtleyLeaf),
                new(PMItemID.MushroomRoast, PMItemID.SuperMushroom, PMItemID.GoldenLeaf),
                new(PMItemID.MushroomRoast, PMItemID.SlowMushroom, PMItemID.VoltMushroom),
                new(PMItemID.MushroomRoast, PMItemID.LifeMushroom, PMItemID.VoltMushroom),
                new(PMItemID.MushroomRoast, PMItemID.SlowMushroom),
                new(PMItemID.MushroomRoast, PMItemID.LifeMushroom),
                new(PMItemID.MushroomSteak, PMItemID.UltraMushroom),
                new(PMItemID.MushroomSteak, PMItemID.UltraMushroom, PMItemID.DriedMushroom),
                new(PMItemID.MushroomSteak, PMItemID.UltraMushroom, PMItemID.Mushroom),
                new(PMItemID.MushroomSteak, PMItemID.UltraMushroom, PMItemID.SuperMushroom),
                new(PMItemID.MushroomSteak, PMItemID.UltraMushroom, PMItemID.UltraMushroom),
                new(PMItemID.MushroomSteak, PMItemID.UltraMushroom, PMItemID.TurtleyLeaf),
                new(PMItemID.MushroomSteak, PMItemID.UltraMushroom, PMItemID.GoldenLeaf),
                new(PMItemID.MushroomSteak, PMItemID.LifeMushroom, PMItemID.DriedMushroom),
                new(PMItemID.MushroomSteak, PMItemID.LifeMushroom, PMItemID.Mushroom),
                new(PMItemID.MushroomSteak, PMItemID.LifeMushroom, PMItemID.SuperMushroom),
                new(PMItemID.MushroomSteak, PMItemID.LifeMushroom, PMItemID.VoltMushroom),
                new(PMItemID.MapleMushroom, PMItemID.MapleSyrup, PMItemID.Mushroom),
                new(PMItemID.MapleMushroom, PMItemID.MapleSyrup, PMItemID.VoltMushroom),
                new(PMItemID.MapleMushroom, PMItemID.MapleSyrup, PMItemID.SlowMushroom),
                new(PMItemID.MapleSuper, PMItemID.MapleSyrup, PMItemID.LifeMushroom),
                new(PMItemID.MapleSuper, PMItemID.MapleSyrup, PMItemID.SuperMushroom),
                new(PMItemID.MapleUltra, PMItemID.MapleSyrup, PMItemID.UltraMushroom),
                new(PMItemID.MeteorMeal, PMItemID.ShootingStar, PMItemID.MushroomFry),
                new(PMItemID.MeteorMeal, PMItemID.ShootingStar, PMItemID.MushroomRoast),
                new(PMItemID.MeteorMeal, PMItemID.ShootingStar, PMItemID.MushroomSteak),
                new(PMItemID.OmeletteMeal, PMItemID.MysticEgg, PMItemID.Horsetail),
                new(PMItemID.OmeletteMeal, PMItemID.MysticEgg, PMItemID.Mushroom),
                new(PMItemID.OmeletteMeal, PMItemID.MysticEgg, PMItemID.SuperMushroom),
                new(PMItemID.OmeletteMeal, PMItemID.MysticEgg, PMItemID.UltraMushroom),
                new(PMItemID.OmeletteMeal, PMItemID.MysticEgg, PMItemID.LifeMushroom),
                new(PMItemID.PoisonMushroom, PMItemID.InkySauce, PMItemID.SlowMushroom),
                new(PMItemID.PoisonMushroom, PMItemID.DriedBouquet, PMItemID.TrialStew),
                new(PMItemID.PeachTart, PMItemID.CakeMix, ItemID.Peach),
                new(PMItemID.ThunderRage, PMItemID.DriedBouquet, PMItemID.VoltMushroom),
                new(PMItemID.SpicySoup, PMItemID.FireFlower),
                new(PMItemID.SpicySoup, PMItemID.Horsetail, ItemID.None, true),
                new(PMItemID.SpicySoup, PMItemID.SnowBunny),
                new(PMItemID.SpicySoup, PMItemID.DriedBouquet, ItemID.None, true),
                new(PMItemID.SpicySoup, PMItemID.FireFlower, PMItemID.HotSauce),
                new(PMItemID.SpicySoup, PMItemID.FireFlower, PMItemID.DriedBouquet),
                new(PMItemID.SnowBunny, PMItemID.GoldenLeaf, ItemID.IceBlock),
                new(PMItemID.SnowBunny, PMItemID.GoldenLeaf, ItemID.IceBrick),
                new(PMItemID.SnowBunny, PMItemID.GoldenLeaf, ItemID.FlowerofFrost),
                new(PMItemID.SnowBunny, PMItemID.GoldenLeaf, ItemID.SnowBlock),
                new(PMItemID.SnowBunny, PMItemID.GoldenLeaf, ItemID.Shiverthorn),
                new(PMItemID.IciclePop, PMItemID.HoneySyrup, ItemID.IceBlock),
                new(PMItemID.IciclePop, PMItemID.HoneySyrup, ItemID.IceBrick),
                new(PMItemID.IciclePop, PMItemID.HoneySyrup, ItemID.FlowerofFrost),
                new(PMItemID.IciclePop, PMItemID.HoneySyrup, ItemID.SnowBlock),
                new(PMItemID.IciclePop, PMItemID.HoneySyrup, ItemID.Shiverthorn),
                new(PMItemID.ZessFrappe, PMItemID.MapleSyrup, ItemID.IceBlock),
                new(PMItemID.ZessFrappe, PMItemID.MapleSyrup, ItemID.IceBrick),
                new(PMItemID.ZessFrappe, PMItemID.MapleSyrup, ItemID.FlowerofFrost),
                new(PMItemID.ZessFrappe, PMItemID.MapleSyrup, ItemID.SnowBlock),
                new(PMItemID.ZessFrappe, PMItemID.MapleSyrup, ItemID.Shiverthorn),
                new(PMItemID.ZessFrappe, PMItemID.JamminJelly, ItemID.IceBlock),
                new(PMItemID.ZessFrappe, PMItemID.JamminJelly, ItemID.IceBrick),
                new(PMItemID.ZessFrappe, PMItemID.JamminJelly, ItemID.FlowerofFrost),
                new(PMItemID.ZessFrappe, PMItemID.JamminJelly, ItemID.SnowBlock),
                new(PMItemID.ZessFrappe, PMItemID.JamminJelly, ItemID.Shiverthorn),
                new(PMItemID.ZessTea, PMItemID.GoldenLeaf),
                new(PMItemID.ZessTea, PMItemID.MapleSyrup, PMItemID.JamminJelly),
                new(PMItemID.TrialStew, PMItemID.PoisonMushroom, PMItemID.CouplesCake),
                new(PMItemID.TrialStew, PMItemID.ThunderBolt, PMItemID.ThunderRage),
                new(PMItemID.ZessCookie, PMItemID.CakeMix, PMItemID.GradualSyrup),
                new(PMItemID.ZessCookie, PMItemID.CakeMix, PMItemID.MapleSyrup),
                new(PMItemID.ZessCookie, PMItemID.CakeMix, PMItemID.MysticEgg),
                new(PMItemID.ZessDeluxe, PMItemID.WhackaBump, PMItemID.GoldenLeaf, true),
                new(PMItemID.ZessDeluxe, PMItemID.MushroomSteak, PMItemID.HealthySalad, true),
                new(PMItemID.ZessDeluxe, PMItemID.UltraMushroom, PMItemID.FreshPasta, true),
                new(PMItemID.ZessDinner, PMItemID.Mushroom, PMItemID.Horsetail),
                new(PMItemID.ZessDinner, PMItemID.Mushroom, ItemID.Peach),
                new(PMItemID.ZessDinner, PMItemID.Mushroom, ItemID.Mango),
                new(PMItemID.ZessDinner, PMItemID.SuperMushroom, PMItemID.FireFlower),
                new(PMItemID.ZessDinner, PMItemID.SuperMushroom, PMItemID.GradualSyrup),
                new(PMItemID.ZessDinner, PMItemID.SuperMushroom, PMItemID.Horsetail),
                new(PMItemID.ZessDinner, PMItemID.SuperMushroom, ItemID.Peach),
                new(PMItemID.ZessDinner, PMItemID.SuperMushroom, ItemID.Mango),
                new(PMItemID.ZessDinner, PMItemID.UltraMushroom, ItemID.Peach),
                new(PMItemID.ZessDinner, PMItemID.UltraMushroom, ItemID.Mango),
                new(PMItemID.ZessDinner, PMItemID.LifeMushroom, PMItemID.FireFlower),
                new(PMItemID.ZessDinner, PMItemID.LifeMushroom, PMItemID.GradualSyrup),
                new(PMItemID.ZessDinner, PMItemID.LifeMushroom, PMItemID.Horsetail),
                new(PMItemID.ZessDinner, PMItemID.LifeMushroom, ItemID.Peach),
                new(PMItemID.ZessDinner, PMItemID.LifeMushroom, PMItemID.SlowMushroom),
                new(PMItemID.ZessDinner, PMItemID.FreshPasta, PMItemID.Mushroom),
                new(PMItemID.ZessDinner, PMItemID.FreshPasta, PMItemID.DriedMushroom),
                new(PMItemID.ZessDinner, PMItemID.FreshPasta, PMItemID.SuperMushroom),
                new(PMItemID.ZessDinner, PMItemID.FreshPasta, PMItemID.LifeMushroom),
                new(PMItemID.ZessDinner, PMItemID.FreshPasta, PMItemID.MysticEgg),
                new(PMItemID.ZessDinner, PMItemID.FreshPasta, PMItemID.HealthySalad),
                new(PMItemID.ZessDinner, PMItemID.FreshPasta, ItemID.Coconut),
                new(PMItemID.ZessDinner, PMItemID.HealthySalad, PMItemID.MushroomFry),
                new(PMItemID.ZessDinner, PMItemID.HealthySalad, PMItemID.Koopasta),
                new(PMItemID.ZessDinner, PMItemID.HealthySalad, PMItemID.Spaghetti),
                new(PMItemID.ZessDinner, PMItemID.SpicyPasta, ItemID.Coconut),
                new(PMItemID.ZessDinner, PMItemID.MeteorMeal, PMItemID.FruitParfait),
                new(PMItemID.ZessSpecial, PMItemID.WhackaBump),
                new(PMItemID.ZessSpecial, PMItemID.UltraMushroom, PMItemID.FreshPasta, false, true),
                new(PMItemID.ZessSpecial, PMItemID.UltraMushroom, PMItemID.FireFlower),
                new(PMItemID.ZessSpecial, PMItemID.UltraMushroom, PMItemID.GradualSyrup),
                new(PMItemID.ZessSpecial, PMItemID.UltraMushroom, PMItemID.Horsetail),
                new(PMItemID.ZessSpecial, PMItemID.HealthySalad, PMItemID.InkPasta),
                new(PMItemID.ZessSpecial, PMItemID.HealthySalad, PMItemID.SpicyPasta),
                new(PMItemID.ZessSpecial, PMItemID.HealthySalad, PMItemID.MushroomRoast),
                new(PMItemID.ZessDynamite, PMItemID.EggBomb, PMItemID.CoconutBomb),
                //original recipes
                new(PMItemID.ShootingStar, ItemID.FragmentNebula, ItemID.FragmentStardust),
                new(PMItemID.FreshJuice, ItemID.SpicyPepper, ItemID.BottledWater),
                new(PMItemID.DeliciousCake, PMItemID.PoisonedCake, ItemID.LunarOre),
                new(PMItemID.PoisonedCake, PMItemID.CakeMix, ItemID.RedPotion),
                new(PMItemID.PoisonedCake, PMItemID.PoisonMushroom, PMItemID.MousseCake),
                new(PMItemID.PoisonedCake, PMItemID.PoisonMushroom, PMItemID.CakeMix),
                new(PMItemID.TrialStew, PMItemID.PoisonMushroom, ItemID.StrangeBrew),
                //switch item
                new(PMItemID.HottestDog, PMItemID.HotSauce, ItemID.Hotdog)
            };

            var MysteryItemList = new List<int>()
            {
                PMItemID.FreshJuice, PMItemID.FireFlower, PMItemID.Spaghetti, PMItemID.TastyTonic,
                PMItemID.SpicySoup, PMItemID.FriedEgg, PMItemID.HoneyMushroom, PMItemID.Koopasta,
                PMItemID.KoopaTea, PMItemID.MushroomFry, PMItemID.ThunderRage, PMItemID.VoltMushroom,
                PMItemID.ZessCookie, PMItemID.ZessDinner, PMItemID.ZessTea, PMItemID.EggBomb
            };

            var SpaceFoodBlacklist = new List<int>()
            {
                ItemID.None, ItemID.LesserHealingPotion, ItemID.Mushroom, ItemID.LesserRestorationPotion, ItemID.BottledWater
            };

            var SpaceFoodList = new List<int>()
            {
                ItemID.Coconut, PMItemID.CouplesCake, PMItemID.ElectroPop, PMItemID.FirePop,
                PMItemID.HeartfulCake, PMItemID.GoldenLeaf, PMItemID.HealthySalad, PMItemID.HoneyCandy,
                ItemID.Hotdog, PMItemID.JellyCandy, ItemID.Mango, PMItemID.KoopaBun, PMItemID.LovePudding,
                PMItemID.MeteorMeal, PMItemID.MousseCake, PMItemID.PeachTart, ItemID.Peach,
                PMItemID.PoisonMushroom, PMItemID.TurtleyLeaf
            };

            for (int i = 0; i < IngredientList.Count; i++)
            {
                RecipeRegister.MainRecipeDictionary.Add(i, IngredientList[i]);
            }

            for (int j = 0; j < MysteryItemList.Count; j++)
            {
                RecipeRegister.MysteryBoxRecipeList.Add(MysteryItemList[j]);
            }

            //Space Food stuff
            for (int k = 0; k < SpaceFoodBlacklist.Count; k++)
            {
                RecipeRegister.SpaceFoodBlacklist.Add(SpaceFoodBlacklist[k]);
            }

            for (int l = 0; l < SpaceFoodList.Count; l++)
            {
                RecipeRegister.SpaceFoodList.Add(SpaceFoodList[l]);
            }
        }

        public override void PostSetupContent()
        {
            for (int m = 0; m < ContentSamples.ItemsByType.Count; m++)
            {
                Item item = ContentSamples.ItemsByType[m];
                if (item.healLife > 0 || (item.buffType == BuffID.Regeneration && item.buffTime > 0))
                {
                    RecipeRegister.SpaceFoodList.Add(item.type);
                }
            }
        }
    }
}