using PaperMarioItems.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PaperMarioItems.Common.NPCs
{
    public partial class PaperNPCShop : GlobalNPC
    {
        public override void ModifyShop(NPCShop shop)
        {
            if (shop.NpcType == NPCID.Merchant)
            {
                shop.Add(PMItemID.MildCocoaBean, Condition.InJungle);
            }
            if (shop.NpcType == NPCID.Wizard)
            {
                shop.Add(PMItemID.BlockBlock, [Condition.InSpace]);
            }
        }
    }
}