/*
using PaperMarioItems.Content;
using System.Linq;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Common.GlobalItems
{
    public class BossBagLoot : GlobalItem
    {
        public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
        {
            if (item.type == ItemID.PlanteraBossBag)
            {
                foreach (var rule in itemLoot.Get())
                {
                    if (rule is DropBasedOnExpertMode dropBasedOnExpertMode && dropBasedOnExpertMode.ruleForNormalMode is OneFromOptionsNotScaledWithLuckDropRule oneFromOptionsDrop)
                    {
                        var original = oneFromOptionsDrop.dropIds.ToList();
                        original.Add(PMItemID.SapSoup);
                        oneFromOptionsDrop.dropIds = original.ToArray();
                    }
                }
            }
        }
    }
}
*/