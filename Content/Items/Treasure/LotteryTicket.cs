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
	public class LotteryTicket : ModItem
	{
        public static LocalizedText LottoEmpty { get; private set; }
        public static LocalizedText LottoFilled { get; private set; }
        public string lottoNumber = string.Empty;
        public override void SetStaticDefaults()
        {
            LottoEmpty = this.GetLocalization(nameof(LottoEmpty));
            LottoFilled = this.GetLocalization(nameof(LottoFilled));
            Item.ResearchUnlockCount = 1;
        }
        public override void SetDefaults()
		{
			Item.width = 40;
			Item.height = 40;
			Item.maxStack = Item.CommonMaxStack;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(copper: 25);
        }

        public override bool CanUseItem(Player player)
        {
            if (lottoNumber == string.Empty)
            {
                lottoNumber = DetermineLottery();
                SoundEngine.PlaySound(SoundID.ResearchComplete, player.Center);
            }
            return false;
        }

        public override void SaveData(TagCompound tag)
        {
            tag.Add("lottoNumber", lottoNumber);
        }

        public override void LoadData(TagCompound tag)
        {
            lottoNumber = tag.GetString("lottoNumber");
        }

        public override void NetSend(BinaryWriter writer)
        {
            writer.Write(lottoNumber);
        }

        public override void NetReceive(BinaryReader reader)
        {
            lottoNumber = reader.ReadString();
        }

        public override bool CanStack(Item source)
        {
            var name1 = lottoNumber;
            var name2 = ((LotteryTicket)source.ModItem).lottoNumber;
            return name1 == name2;
        }

        public override void OnStack(Item source, int numToTransfer)
        {
            if (lottoNumber == string.Empty) lottoNumber = ((LotteryTicket)source.ModItem).lottoNumber;
        }

        public override bool OnPickup(Player player)
        {
            if (lottoNumber == string.Empty) lottoNumber = DetermineLottery();
            return true;
        }

        public override void OnCreated(ItemCreationContext context)
        {
            if (context is RecipeItemCreationContext ||
                context is BuyItemCreationContext) lottoNumber = DetermineLottery();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (lottoNumber != string.Empty)
            {
                tooltips.Add(new TooltipLine(Mod, "LottoFilled", Language.GetTextValue(LottoFilled.Format(lottoNumber))));
            }
            else tooltips.Add(new TooltipLine(Mod, "LottoEmpty", Language.GetTextValue(LottoEmpty.Format())));
        }

        private string DetermineLottery()
        {
            int num1 = Main.rand.Next(0, 10);
            int num2 = Main.rand.Next(0, 10);
            int num3 = Main.rand.Next(0, 10);
            int num4 = Main.rand.Next(0, 10);
            return num1.ToString("0") + num2.ToString("0") + num3.ToString("0") + num4.ToString("0");
        }
    }
}
