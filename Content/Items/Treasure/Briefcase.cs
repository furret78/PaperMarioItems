using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System.IO;
using Terraria.ModLoader.IO;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.Audio;

namespace PaperMarioItems.Content.Items.Treasure
{
	public class Briefcase : ModItem
	{
        public static LocalizedText BriefcaseContain { get; private set; }
        public static LocalizedText BriefcaseFull { get; private set; }
        public static LocalizedText BriefcaseEmpty { get; private set; }
        private const int paperNumberMax = 20;
        public int paperNumber = 0;
        public override void SetStaticDefaults()
        {
            BriefcaseContain = this.GetLocalization(nameof(BriefcaseContain));
            BriefcaseFull = this.GetLocalization(nameof(BriefcaseFull));
            BriefcaseEmpty = this.GetLocalization(nameof(BriefcaseEmpty));
            Item.ResearchUnlockCount = 1;
        }
        public override void SetDefaults()
		{
			Item.width = 39;
			Item.height = 40;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(silver: 15);
        }

        public override bool CanRightClick() => true;

        public override bool ConsumeItem(Player player) => false;

        public override void RightClick(Player player)
        {
            if (Main.mouseItem.IsAir)
            {
                if (paperNumber > 0)
                {
                    SoundEngine.PlaySound(SoundID.Grab, player.Center);
                    paperNumber--;
                    Main.mouseItem = new Item(PMItemID.VitalPaper);
                }
                else SoundEngine.PlaySound(PMSoundID.wrong, player.Center);
            }
            else if (Main.mouseItem.type == PMItemID.VitalPaper)
            {
                if (paperNumber < paperNumberMax)
                {
                    SoundEngine.PlaySound(SoundID.Grab, player.Center);
                    int addAmountMax = paperNumberMax - paperNumber;
                    if (Main.mouseItem.stack > addAmountMax)
                    {
                        Main.mouseItem.stack -= addAmountMax;
                        paperNumber += addAmountMax;
                    }
                    else
                    {
                        paperNumber += Main.mouseItem.stack;
                        Main.mouseItem.TurnToAir();
                    }
                }
                else SoundEngine.PlaySound(PMSoundID.wrong, player.Center);
            }
        }

        public override void SaveData(TagCompound tag)
        {
            tag.Add("paperNumber", paperNumber);
        }

        public override void LoadData(TagCompound tag)
        {
            paperNumber = tag.GetInt("paperNumber");
        }

        public override void NetSend(BinaryWriter writer)
        {
            writer.Write(paperNumber);
        }

        public override void NetReceive(BinaryReader reader)
        {
            paperNumber = reader.ReadInt32();
        }

        public override bool CanStack(Item source)
        {
            return false;
        }

        public override bool OnPickup(Player player)
        {
            return true;
        }

        public override void OnCreated(ItemCreationContext context)
        {
            if (context is JourneyDuplicationItemCreationContext) paperNumber = 20;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (paperNumber == paperNumberMax)
            {
                tooltips.Add(new TooltipLine(Mod, "BriefcaseFull", Language.GetTextValue(BriefcaseFull.Format(paperNumberMax))));
                tooltips.Find(x => x.Name == "ItemName");
            }
            else if (paperNumber > 0)
            {
                tooltips.Add(new TooltipLine(Mod, "BriefcaseContain", Language.GetTextValue(BriefcaseContain.Format(paperNumber, paperNumberMax))));
            }
            else
            {
                tooltips.Add(new TooltipLine(Mod, "BriefcaseEmpty", Language.GetTextValue(BriefcaseEmpty.Format())));
            }
        }

    }
}
