using PaperMarioItems.Common.Players;
using System.IO;
using Terraria.ID;
using Terraria;

namespace PaperMarioItems
{
	partial class PaperMarioItems
	{
        internal enum MessageType : byte
        {
            RepelDodgeMessage,
            StatIncreaseSync,
        }
        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            MessageType msgType = (MessageType)reader.ReadByte();

            switch (msgType)
            {
                case MessageType.RepelDodgeMessage:
                    PaperPlayer.HandleDodgeMessage(reader, whoAmI);
                    break;
                case MessageType.StatIncreaseSync:
                    byte playerNumber = reader.ReadByte();
                    PaperPlayer examplePlayer = Main.player[playerNumber].GetModPlayer<PaperPlayer>();
                    examplePlayer.ReceivePlayerSync(reader);

                    if (Main.netMode == NetmodeID.Server)
                    {
                        examplePlayer.SyncPlayer(-1, whoAmI, false);
                    }
                    break;
                default:
                    Logger.WarnFormat("PaperMarioItems: Unknown Message type: {0}", msgType);
                    break;
            }
        }
    }
}
