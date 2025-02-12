using PaperMarioItems.Content;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Common.NPCs
{
    public partial class PaperNPCDrops : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (!NPCID.Sets.CountsAsCritter[npc.type])
            {
                if (!npc.boss && !NPCID.Sets.ShouldBeCountedAsBoss[npc.type])
                {
                    npcLoot.Add(ItemDropRule.NormalvsExpert(PMItemID.PointSwap, 47, 44));
                    npcLoot.Add(ItemDropRule.NormalvsExpert(PMItemID.SpitePouch, 57, 55));
                    npcLoot.Add(ItemDropRule.NormalvsExpert(PMItemID.InnCoupon, 85, 78));
                    npcLoot.Add(ItemDropRule.NormalvsExpert(PMItemID.FrightMask, 60, 55));
                    npcLoot.Add(ItemDropRule.NormalvsExpert(PMItemID.MysteryBox, 54, 50));
                    npcLoot.Add(ItemDropRule.NormalvsExpert(PMItemID.MrSoftener, 64, 60));
                    npcLoot.Add(ItemDropRule.NormalvsExpert(PMItemID.POWBlock, 100, 98));
                    npcLoot.Add(ItemDropRule.NormalvsExpert(PMItemID.PackageBox, 370, 320));
                }
                else if (!NPCID.Sets.ShouldBeCountedAsBoss[npc.type])
                {
                    npcLoot.Add(ItemDropRule.NormalvsExpert(PMItemID.PackageBox, 250, 200));
                }
                if (npc.type != NPCID.Harpy && npc.type != NPCID.Vulture && npc.type != NPCID.Raven)
                    npcLoot.Add(ItemDropRule.NormalvsExpert(PMItemID.MysticEgg, 580, 480));
            }
            if (npc.type == NPCID.Harpy) npcLoot.Add(ItemDropRule.NormalvsExpert(PMItemID.MysticEgg, 36, 33));
            if (npc.type == NPCID.Vulture) npcLoot.Add(ItemDropRule.NormalvsExpert(PMItemID.MysticEgg, 110, 108));
            if (npc.type == NPCID.Raven) npcLoot.Add(ItemDropRule.NormalvsExpert(PMItemID.MysticEgg, 80, 76));
            if (npc.type == NPCID.BlueSlime || npc.type == NPCID.YellowSlime || npc.type == NPCID.GoldenSlime || npc.type == NPCID.DungeonSlime)
                npcLoot.Add(ItemDropRule.Common(PMItemID.WhackaBump, 177));
            if (npc.type == NPCID.Pinky || npc.type == NPCID.ShimmerSlime)
                npcLoot.Add(ItemDropRule.NormalvsExpert(PMItemID.WhackaBump, 45, 40));
            if (npc.type == NPCID.Ghost || npc.type == NPCID.Wraith)
            {
                npcLoot.Add(ItemDropRule.NormalvsExpert(PMItemID.BoosSheet, 11, 9));
                npcLoot.Add(ItemDropRule.NormalvsExpert(PMItemID.RepelCape, 13, 11));
            }
            if (npc.type == NPCID.Reaper || npc.type == NPCID.Poltergeist || npc.type == NPCID.ShadowFlameApparition || npc.type == NPCID.PirateGhost)
            {
                npcLoot.Add(ItemDropRule.NormalvsExpert(PMItemID.BoosSheet, 9, 8));
                npcLoot.Add(ItemDropRule.NormalvsExpert(PMItemID.RepelCape, 11, 9));
            }
            if (npc.type == NPCID.Ghost || npc.type == NPCID.PirateGhost)
            {
                npcLoot.Add(ItemDropRule.NormalvsExpert(PMItemID.PrimordialFruit, 136, 133));
            }
            if (npc.type == NPCID.StardustSoldier)
                npcLoot.Add(ItemDropRule.NormalvsExpert(PMItemID.ShootingStar, 12, 9));
            if (npc.type == NPCID.CaveBat || npc.type == NPCID.GiantBat || npc.type == NPCID.IceBat || npc.type == NPCID.IlluminantBat || npc.type == NPCID.JungleBat || npc.type == NPCID.SporeBat || npc.type == NPCID.VampireBat || npc.type == NPCID.Hellbat || npc.type == NPCID.Lavabat)
            {
                npcLoot.Add(ItemDropRule.NormalvsExpert(PMItemID.DizzyDial, 59, 57));
                npcLoot.Add(ItemDropRule.NormalvsExpert(PMItemID.HPDrain, 40, 35));
            }
            if (npc.type == NPCID.GraniteFlyer || npc.type == NPCID.GraniteGolem)
                npcLoot.Add(ItemDropRule.NormalvsExpert(PMItemID.EarthQuake, 2, 1));
            if (npc.type == NPCID.Dandelion || npc.type == NPCID.Snatcher ||
                npc.type == NPCID.AngryTrapper || npc.type == NPCID.Clinger ||
                npc.type == NPCID.FungiBulb || npc.type == NPCID.GiantFungiBulb ||
                npc.type == NPCID.ManEater)
                npcLoot.Add(ItemDropRule.NormalvsExpert(PMItemID.SapSoup, 74, 70));
            if (npc.type == NPCID.FungiBulb || npc.type == NPCID.GiantFungiBulb ||
                npc.type == NPCID.AnomuraFungus || npc.type == NPCID.MushiLadybug ||
                npc.type == NPCID.SporeBat || npc.type == NPCID.SporeSkeleton ||
                npc.type == NPCID.ZombieMushroom || npc.type == NPCID.ZombieMushroomHat ||
                npc.type == NPCID.FungoFish)
            {
                npcLoot.Add(ItemDropRule.NormalvsExpert(PMItemID.SlimyMushroom, 399, 380));
            }
            if (npc.type == NPCID.Truffle)
            {
                npcLoot.Add(ItemDropRule.NormalvsExpert(PMItemID.SlimyMushroom, 4, 2));
            }
            if (npc.type == NPCID.Dandelion)
            {
                npcLoot.Add(ItemDropRule.Common(PMItemID.DayzeeTear, 205, 1, 7));
            }
            if (npc.type == NPCID.SpikedJungleSlime || npc.type == NPCID.SpikedIceSlime)
            {
                npcLoot.Add(ItemDropRule.NormalvsExpert(PMItemID.MightyTonic, 470, 450));
            }
            if (npc.type == NPCID.SlimeSpiked)
            {
                npcLoot.Add(ItemDropRule.NormalvsExpert(PMItemID.MightyTonic, 750, 735));
            }
            if (npc.type == NPCID.PigronCorruption || npc.type == NPCID.PigronCrimson || npc.type == NPCID.PigronHallow)
            {
                npcLoot.Add(ItemDropRule.NormalvsExpert(PMItemID.BoneinCut, 640, 620));
            }

            //boss specific drops
            if (System.Array.IndexOf([NPCID.EaterofWorldsBody, NPCID.EaterofWorldsHead, NPCID.EaterofWorldsTail], npc.type) > -1)
            {
                LeadingConditionRule leadingConditionRule = new(new Conditions.LegacyHack_IsABoss());
                leadingConditionRule.OnSuccess(ItemDropRule.NormalvsExpert(PMItemID.DizzyDial, 3, 2));
                leadingConditionRule.OnSuccess(ItemDropRule.BossBagByCondition(new Conditions.LegacyHack_IsBossAndExpert(), PMItemID.ShineSprite));
                leadingConditionRule.OnSuccess(ItemDropRule.ByCondition(new Conditions.LegacyHack_IsBossAndNotExpert(), PMItemID.ShineSprite, 2));
                npcLoot.Add(leadingConditionRule);
            }
            else
            {
                npcLoot.Add(ItemDropRule.BossBagByCondition(new Conditions.LegacyHack_IsBossAndExpert(), PMItemID.ShineSprite));
                npcLoot.Add(ItemDropRule.ByCondition(new Conditions.LegacyHack_IsBossAndNotExpert(), PMItemID.ShineSprite, 2));
            }
            if (npc.type == NPCID.BrainofCthulhu)
            {
                npcLoot.Add(ItemDropRule.BossBagByCondition(new Conditions.LegacyHack_IsBossAndExpert(), PMItemID.DizzyDial));
                npcLoot.Add(ItemDropRule.ByCondition(new Conditions.LegacyHack_IsBossAndNotExpert(), PMItemID.DizzyDial, 3));
            }
            if (npc.type == NPCID.Plantera)
            {
                /*
                foreach (var rule in npcLoot.Get())
                {
                    if (rule is DropBasedOnExpertMode dropBasedOnExpertMode && dropBasedOnExpertMode.ruleForNormalMode is OneFromOptionsNotScaledWithLuckDropRule oneFromOptionsDrop)
                    {
                        var original = oneFromOptionsDrop.dropIds.ToList();
                        original.Add(PMItemID.SapSoup);
                        oneFromOptionsDrop.dropIds = original.ToArray();
                    }
                }
                */
                npcLoot.Add(ItemDropRule.BossBagByCondition(new Conditions.LegacyHack_IsBossAndExpert(), PMItemID.SapSoup));
                npcLoot.Add(ItemDropRule.ByCondition(new Conditions.LegacyHack_IsBossAndNotExpert(), PMItemID.SapSoup, 4));
            }

            //shine sprite drops
            if (npc.type == NPCID.Retinazer || npc.type == NPCID.Spazmatism)
            {
                LeadingConditionRule leadingConditionRule = new(new Conditions.MissingTwin());
                leadingConditionRule.OnSuccess(ItemDropRule.NormalvsExpert(PMItemID.ShineSprite, 2, 1));
                npcLoot.Add(leadingConditionRule);
            }
        }
    }
}