using PaperMarioItems.Content.Items;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Common.RecipeSystem
{
    public class RecipeList : ModSystem
    {
        //first value is the result, second is the ingredient #1, third is the ingredient #2
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
                new(PMItemID.SuperMushroom, PMItemID.PointSwap, PMItemID.MapleSyrup),
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
                //unique cooking (non-cookbook)
                new(PMItemID.FreshJuice, ItemID.Peach, ItemID.None),
                new(PMItemID.FreshJuice, ItemID.Mango, ItemID.None),
                new(PMItemID.FreshJuice, PMItemID.HoneySyrup, ItemID.None),
                new(PMItemID.FreshJuice, PMItemID.MapleSyrup, ItemID.None),
                new(PMItemID.FreshJuice, PMItemID.JamminJelly, ItemID.None),
                new(PMItemID.FreshJuice, PMItemID.GradualSyrup, ItemID.None),
                //unique cooking
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
                new(PMItemID.FriedEgg, PMItemID.MysticEgg, ItemID.None),
                new(PMItemID.KoopaTea, PMItemID.TurtleyLeaf, ItemID.None),
                new(PMItemID.HottestDog, PMItemID.HotSauce, ItemID.Hotdog),
                new(PMItemID.Spaghetti, PMItemID.FreshPasta, ItemID.None),
                new(PMItemID.Koopasta, PMItemID.Spaghetti, PMItemID.TurtleyLeaf),
                new(PMItemID.Koopasta, PMItemID.FreshPasta, PMItemID.TurtleyLeaf),
                new(PMItemID.SpicyPasta, PMItemID.Koopasta, PMItemID.HotSauce),
                new(PMItemID.SpicyPasta, PMItemID.FreshPasta, PMItemID.HotSauce),
                new(PMItemID.SpicyPasta, PMItemID.Spaghetti, PMItemID.HotSauce),
                new(PMItemID.InkPasta, PMItemID.Koopasta, PMItemID.InkySauce),
                new(PMItemID.InkPasta, PMItemID.FreshPasta, PMItemID.InkySauce),
                new(PMItemID.InkPasta, PMItemID.SpicyPasta, PMItemID.InkySauce),
                new(PMItemID.InkPasta, PMItemID.Spaghetti, PMItemID.InkySauce),
                new(PMItemID.MushroomBroth, PMItemID.TurtleyLeaf, PMItemID.PoisonMushroom),
                new(PMItemID.MushroomBroth, PMItemID.TurtleyLeaf, PMItemID.SlowMushroom),
                new(PMItemID.MushroomBroth, PMItemID.GoldenLeaf, PMItemID.PoisonMushroom),
                new(PMItemID.MushroomBroth, PMItemID.GoldenLeaf, PMItemID.SlowMushroom),
                new(PMItemID.MushroomCake, PMItemID.CakeMix, PMItemID.Mushroom),
                new(PMItemID.MushroomCake, PMItemID.CakeMix, PMItemID.SuperMushroom),
                new(PMItemID.MushroomCake, PMItemID.CakeMix, PMItemID.SlowMushroom),
                new(PMItemID.MushroomCake, PMItemID.CakeMix, PMItemID.LifeMushroom),
                new(PMItemID.MushroomCrepe, PMItemID.CakeMix, PMItemID.UltraMushroom),
                new(PMItemID.MushroomRoast, PMItemID.Mushroom, PMItemID.FireFlower),
                new(PMItemID.MushroomRoast, PMItemID.Mushroom, PMItemID.SuperMushroom),
                new(PMItemID.MushroomRoast, PMItemID.Mushroom, PMItemID.VoltMushroom),
                new(PMItemID.MushroomRoast, PMItemID.Mushroom, PMItemID.GradualSyrup),
                new(PMItemID.MushroomRoast, PMItemID.SuperMushroom, PMItemID.DriedMushroom),
                new(PMItemID.MushroomRoast, PMItemID.SuperMushroom, PMItemID.VoltMushroom),
                new(PMItemID.MushroomRoast, PMItemID.SuperMushroom, PMItemID.TurtleyLeaf),
                new(PMItemID.MushroomRoast, PMItemID.SuperMushroom, PMItemID.GoldenLeaf),
                new(PMItemID.MushroomRoast, PMItemID.SlowMushroom, PMItemID.VoltMushroom),
                new(PMItemID.MushroomRoast, PMItemID.SlowMushroom, PMItemID.VoltMushroom),
                new(PMItemID.MushroomRoast, PMItemID.LifeMushroom, ItemID.None),
                new(PMItemID.MushroomRoast, PMItemID.LifeMushroom, ItemID.None),
                new(PMItemID.MushroomSteak, PMItemID.UltraMushroom, ItemID.None),
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
                new(PMItemID.MushroomSteak, PMItemID.LifeMushroom, PMItemID.TurtleyLeaf),
                new(PMItemID.MushroomSteak, PMItemID.LifeMushroom, PMItemID.GoldenLeaf),
                new(PMItemID.PoisonMushroom, PMItemID.InkySauce, PMItemID.SlowMushroom),
                //new(PMItemID.PoisonMushroom, PMItemID.DriedBouquet, PMItemID.TrialStew), implement later when there is Trial Stew
                new(PMItemID.ZessFrappe, PMItemID.MapleSyrup, ItemID.IceBlock),
                new(PMItemID.ZessFrappe, PMItemID.JamminJelly, ItemID.IceBlock),
                new(PMItemID.ZessFrappe, PMItemID.MapleSyrup, ItemID.IceBrick),
                new(PMItemID.ZessFrappe, PMItemID.JamminJelly, ItemID.IceBrick),
                new(PMItemID.ZessTea, PMItemID.GoldenLeaf, ItemID.None),
                new(PMItemID.ZessTea, PMItemID.MapleSyrup, PMItemID.JamminJelly),
                new(PMItemID.InkySauce, PMItemID.HotSauce, PMItemID.FreshJuice),
                new(PMItemID.InkySauce, PMItemID.HotSauce, PMItemID.KoopaTea),
                new(PMItemID.InkySauce, PMItemID.HotSauce, PMItemID.MushroomBroth),
                new(PMItemID.InkySauce, PMItemID.HotSauce, PMItemID.TurtleyLeaf),
                new(PMItemID.InkySauce, PMItemID.HotSauce, PMItemID.ZessTea),
                new(PMItemID.InkySauce, PMItemID.HotSauce, PMItemID.TastyTonic),
                //switch item
                new(PMItemID.HottestDog, PMItemID.HotSauce, ItemID.Hotdog)
            };

            for (int i = 0; i < IngredientList.Count; i++)
            {
                RecipeRegister.MainRecipeDictionary.Add(i, IngredientList[i]);
            }
        }
    }
}