using Microsoft.Xna.Framework;
using PaperMarioItems.Content;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace PaperMarioItems.Common.UI
{
    [Autoload(Side = ModSide.Client)]
    public class PaperCookingSystem : ModSystem
    {
        public Vector2? NearestCookingPotPosition;
        internal MenuBar MenuBar;
        private UserInterface MenuBarUserInterface;

        public override void Load()
        {
            if (Main.dedServ) return;
            MenuBar = new MenuBar();
            MenuBarUserInterface = new UserInterface();
            MenuBar.Activate();
        }

        public override void Unload()
        {
            base.Unload();
            MenuBar = null;
        }

        public override void PostUpdatePlayers()
        {
            base.PostUpdatePlayers();
            if (NearestCookingPotPosition != null)
            {
                if (MenuBarUserInterface.CurrentState == null)
                {
                    ShowUI();
                    return;
                }

                if (Main.LocalPlayer.Center.Distance((Vector2)NearestCookingPotPosition) > 320f
                    || !Main.LocalPlayer.HasItemInInventoryOrOpenVoidBag(PMItemID.Cookbook))
                {
                    HideUI();
                    NearestCookingPotPosition = null;
                }
            }
            else if (MenuBarUserInterface != null)
            {
                if (MenuBarUserInterface.CurrentState is MenuBar)
                {
                    HideUI();
                }
            }
        }

        public override void UpdateUI(GameTime gameTime)
        {
            if (MenuBarUserInterface?.CurrentState != null)
            {
                MenuBarUserInterface?.Update(gameTime);
            }
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
            SoundEngine.PlaySound(SoundID.MenuOpen);
        }

        public void HideUI()
        {
            MenuBarUserInterface?.SetState(null);
            SoundEngine.PlaySound(SoundID.MenuClose);
        }
    }
}