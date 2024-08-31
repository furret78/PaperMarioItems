using Microsoft.Xna.Framework;
using PaperMarioItems.Content.Items;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace PaperMarioItems.Common.UI
{
    [Autoload(Side = ModSide.Client)]
    public class PaperCookingSystem : ModSystem
    {
        internal MenuBar MenuBar;
        private UserInterface MenuBarUserInterface;

        public override void Load()
        {
            MenuBar = new MenuBar();
            MenuBarUserInterface = new UserInterface();
            MenuBar.Activate();
        }

        public override void UpdateUI(GameTime gameTime)
        {
            if (MenuBarUserInterface?.CurrentState != null)
            {
                MenuBarUserInterface?.Update(gameTime);
            }
            if (!Main.LocalPlayer.HasItemInInventoryOrOpenVoidBag(ModContent.ItemType<Cookbook>())) HideUI();
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "PaperMarioItems: Description",
                    delegate
                    {
                        MenuBarUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }

        public void ShowUI()
        {
            MenuBarUserInterface?.SetState(MenuBar);
        }

        public void HideUI()
        {
            MenuBarUserInterface?.SetState(null);
        }
    }
}