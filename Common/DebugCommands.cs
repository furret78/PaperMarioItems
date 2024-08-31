using PaperMarioItems.Common.UI;
using Terraria.ModLoader;

namespace ExampleMod.Common.Commands
{
    public class ExampleCoinsCommand : ModCommand
    {
        // CommandType.Chat means that command can be used in Chat in SP and MP
        public override CommandType Type
            => CommandType.Chat;

        // The desired text to trigger this command
        public override string Command
            => "cook";

        // A short description of this command
        public override string Description
            => "Show the cooking UI";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            ModContent.GetInstance<PaperCookingSystem>().ShowUI();
        }
    }
}