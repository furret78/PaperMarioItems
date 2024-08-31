using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PaperMarioItems.Common.RecipeSystem;
using PaperMarioItems.Content.Items;
using PaperMarioItems.Content.Items.Cooking;
using ReLogic.Content;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace PaperMarioItems.Common.UI
{
    class MenuBar : UIState
    {
        public PanelUI DraggablePanel;
        public CookButton cookButton;
        private UIText plusSign;
        private List<ItemSlotWrapper> itemSlots;
        private readonly float itemSlotSize = 52 * 0.65f;
        private readonly int[] itemSlotsL = { 20, 20 + (int)(2.5f * (52 * 0.65f)) };
        public override void OnInitialize()
        {
            plusSign = new("+");

            DraggablePanel = new PanelUI();
            DraggablePanel.SetPadding(0);
            SetRect(DraggablePanel, 400f, 100f, 192f, 96f);
            DraggablePanel.BackgroundColor = new Color(73, 94, 171);

            itemSlots = new List<ItemSlotWrapper>() {new(scale: 0.75f), new(scale: 0.75f)};

            for (int index = 0; index < itemSlots.Count; index++)
            {
                SetRect(itemSlots[index], itemSlotsL[index], 40f, itemSlotSize, itemSlotSize);
                DraggablePanel.Append(itemSlots[index]);
                if (index < itemSlots.Count - 1)
                {
                    plusSign.VAlign = 0.5f;
                    plusSign.Left.Set(itemSlotsL[index] + ((itemSlotsL[index + 1] - itemSlotsL[index]) /2), 0f);
                    itemSlots[index].Append(plusSign);
                }
            }

            Asset<Texture2D> buttonPlayTexture = ModContent.Request<Texture2D>("Terraria/Images/UI/ButtonFavoriteActive");
            HoverImageButton cookButton = new(buttonPlayTexture, "Cook these items!");
            SetRect(cookButton, 20f, 10f, 22f, 22f);
            cookButton.OnLeftClick += new MouseEvent(PlayButtonClicked);
            DraggablePanel.Append(cookButton);

            Asset<Texture2D> buttonDeleteTexture = ModContent.Request<Texture2D>("Terraria/Images/UI/ButtonDelete");
            HoverImageButton closeButton = new(buttonDeleteTexture, Language.GetTextValue("LegacyInterface.52"));
            SetRect(closeButton, 50f, 10f, 22f, 22f);
            closeButton.OnLeftClick += new MouseEvent(CloseButtonClicked);
            DraggablePanel.Append(closeButton);

            Append(DraggablePanel);
            //cookButton = new CookButton();
            //Append(cookButton);
        }

        public override void OnDeactivate()
        {
            for (int i = 0; i < itemSlots.Count; i++)
            {
                var item = itemSlots[i].Item;
                Main.LocalPlayer.QuickSpawnItem(new EntitySource_Misc("Closed cooking UI"), item, itemSlots[i].Item.stack);
                itemSlots[i].Item.TurnToAir();
            }
        }

        private void SetRect(UIElement uiElement, float left, float top, float width, float height)
        {
            uiElement.Left.Set(left, 0f);
            uiElement.Top.Set(top, 0f);
            uiElement.Width.Set(width, 0f);
            uiElement.Height.Set(height, 0f);
        }

        private void PlayButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            if (itemSlots[0].Item.type != ItemID.None || itemSlots[1].Item.type != ItemID.None)
            {
                SoundEngine.PlaySound(SoundID.MenuOpen);
                int resultItem = BeginCooking(itemSlots[0].Item.type, itemSlots[1].Item.type),
                    itemAmount = itemSlots[0].Item.stack;
                if (itemSlots[1].Item.stack > itemSlots[0].Item.stack) itemAmount = itemSlots[1].Item.stack;
                IEntitySource itemSource = Main.LocalPlayer.GetSource_TileInteraction((int)Main.LocalPlayer.Center.X, (int)Main.LocalPlayer.Center.Y);
                Main.LocalPlayer.QuickSpawnItem(itemSource, resultItem, itemAmount);
                itemSlots[0].Item.stack -= itemAmount;
                itemSlots[1].Item.stack -= itemAmount;
            }
        }

        private void CloseButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            SoundEngine.PlaySound(SoundID.MenuClose);
            ModContent.GetInstance<PaperCookingSystem>().HideUI();
        }

        private int BeginCooking(int itemslot1, int itemslot2)
        {
            int result = ModContent.ItemType<Mistake>();
            var FirstRoundChosenList = new List<PMRecipe>();
            var SecondRoundChosenList = new List<PMRecipe>();
            //first scan
            for (int i = 0; i < RecipeRegister.MainRecipeDictionary.Count; i++)
            {
                PMRecipe CurrentSelect = RecipeRegister.MainRecipeDictionary.GetValueOrDefault(i);
                if (CurrentSelect.Ingredient1 == itemslot1)
                {
                    FirstRoundChosenList.Add(CurrentSelect);
                }
            }
            if (FirstRoundChosenList.Count <= 0)
            {
                //initiate second round (slot #1 item finds nothing in recipe slot #1)
                for (int i = 0; i < RecipeRegister.MainRecipeDictionary.Count; i++)
                {
                    PMRecipe CurrentSelect = RecipeRegister.MainRecipeDictionary.GetValueOrDefault(i);
                    if (CurrentSelect.Ingredient2 == itemslot1)
                    {
                        SecondRoundChosenList.Add(CurrentSelect);
                    }
                }
                if (SecondRoundChosenList.Count > 0)
                {
                    //found something (slot #1 item found in recipe slot #2)
                    //look for slot #2 item in recipe slot #1
                    if (SecondRoundChosenList.Exists(x => x.Ingredient1 == itemslot2))
                    {
                        int resultingItem = SecondRoundChosenList.Find(x => x.Ingredient1 == itemslot2).ResultingItem;
                        if (resultingItem != ItemID.None)
                        {
                            result = resultingItem;
                        };
                    }
                }
            }
            else
            {
                //found something (slot #1 item in recipe slot #1, now find slot #2 item in recipe slot #2)
                if (FirstRoundChosenList.Exists(x => x.Ingredient2 == itemslot2))
                {
                    int resultingItem = FirstRoundChosenList.Find(x => x.Ingredient2 == itemslot2).ResultingItem;
                    if (resultingItem != ItemID.None)
                    {
                        result = resultingItem;
                    };
                }
                //scan failed (cannot find matching slot #2 items)
                //find slot #1 item in recipe slot #2 instead
                for (int i = 0; i < RecipeRegister.MainRecipeDictionary.Count; i++)
                {
                    PMRecipe CurrentSelect = RecipeRegister.MainRecipeDictionary.GetValueOrDefault(i);
                    if (CurrentSelect.Ingredient2 == itemslot1)
                    {
                        SecondRoundChosenList.Add(CurrentSelect);
                    }
                }
                if (SecondRoundChosenList.Count > 0)
                {
                    //found something (slot #1 item found in recipe slot #2)
                    //look for slot #2 item in recipe slot #1
                    if (SecondRoundChosenList.Exists(x => x.Ingredient1 == itemslot2))
                    {
                        int resultingItem = SecondRoundChosenList.Find(x => x.Ingredient1 == itemslot2).ResultingItem;
                        if (resultingItem != ItemID.None)
                        {
                            result = resultingItem;
                        };
                    }
                }
            }
            //if all fails, return a Mistake
            return result;
        }
    }
}