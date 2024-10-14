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
        public bool AllowOpenCooking;

        public override void Load()
        {
            if (Main.dedServ) return;
            AllowOpenCooking = false;
            MenuBar = new MenuBar();
            MenuBarUserInterface = new UserInterface();
            MenuBar.Activate();
        }

        public override void Unload()
        {
            base.Unload();
            MenuBar.Deactivate();
            MenuBarUserInterface = null;
            MenuBar = null;
        }

        public override void PreUpdateTime()
        {
            if (NearestCookingPotPosition != null)
            {
                if (Main.LocalPlayer.Center.Distance((Vector2)NearestCookingPotPosition) <= 320f &&
                    Main.LocalPlayer.HasItemInInventoryOrOpenVoidBag(PMItemID.Cookbook))
                    AllowOpenCooking = true;
            }
            else AllowOpenCooking = false;

            if ((Main.autoPause && Main.netMode == NetmodeID.SinglePlayer) &&
                (!AllowOpenCooking || NearestCookingPotPosition == null) &&
                MenuBarUserInterface != null && MenuBarUserInterface.CurrentState is MenuBar)
                HideUITrue();
        }

        public override void PostUpdatePlayers()
        {
            if (NearestCookingPotPosition != null)
            {
                if (MenuBarUserInterface.CurrentState == null)
                {
                    ShowUI();
                    return;
                }

                if (Main.LocalPlayer.Center.Distance((Vector2)NearestCookingPotPosition) > 320f ||
                    !Main.LocalPlayer.HasItemInInventoryOrOpenVoidBag(PMItemID.Cookbook) || !Main.playerInventory)
                {
                    HideUI();
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
            if (!Main.playerInventory)
            {
                Main.playerInventory = true;
            }
        }

        private void HideUITrue()
        {
            NearestCookingPotPosition = null;
            MenuBarUserInterface?.SetState(null);
        }

        public void HideUI()
        {
            HideUITrue();
            SoundEngine.PlaySound(SoundID.MenuClose);
        }
    }
}