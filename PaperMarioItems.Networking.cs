using PaperMarioItems.Common.Players;
using System.IO;

namespace PaperMarioItems
{
	partial class PaperMarioItems
	{
        internal enum MessageType : byte
        {
            RepelDodgeMessage
        }
        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            MessageType msgType = (MessageType)reader.ReadByte();

            switch (msgType)
            {
                case MessageType.RepelDodgeMessage:
                    PaperPlayer.HandleDodgeMessage(reader, whoAmI);
                    break;
                default:
                    Logger.WarnFormat("PaperMarioItems: Unknown Message type: {0}", msgType);
                    break;
            }
        }
    }
}
