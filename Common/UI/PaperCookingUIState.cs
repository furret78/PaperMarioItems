using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PaperMarioItems.Common.RecipeSystem;
using PaperMarioItems.Content.Items.Cooking;
using ReLogic.Content;
using System.Collections.Generic;
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
        private UIText plusSign;
        public Vector2 CookingPotPosition;
        private List<ItemSlotWrapper> itemSlots;
        private readonly float itemSlotSize = 52 * 0.65f;
        private readonly int[] itemSlotsL = { 20, 20 + (int)(2.5f * (52 * 0.65f)) };
        public override void OnInitialize()
        {
            CookingPotPosition.X = (int)Main.LocalPlayer.Center.X;
            CookingPotPosition.Y = (int)Main.LocalPlayer.Center.Y;
            plusSign = new("+");

            DraggablePanel = new PanelUI();
            DraggablePanel.SetPadding(0);
            float panelWidth = itemSlotsL[itemSlotsL.Length - 1] + itemSlotSize + 30f;
            Vector2 InitPos = new((Main.screenWidth / 2) - (panelWidth / 2), Main.screenHeight / 5);
            SetRect(DraggablePanel, InitPos.X, InitPos.Y, panelWidth, 96f);
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
            int item1 = itemSlots[0].Item.type, amount1 = itemSlots[0].Item.stack, item2 = itemSlots[1].Item.type, amount2 = itemSlots[1].Item.stack;
            if (CanCook(item1, amount1, item2, amount2))
            {
                int resultItem = CookItems(item1, amount1, item2, amount2);
                SpawnResultItem(resultItem, amount1, amount2);
                CloseCookingUI();
            }
        }

        private void CloseButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            CloseCookingUI();
        }

        private void CloseCookingUI()
        {
            SoundEngine.PlaySound(SoundID.MenuClose);
            ModContent.GetInstance<PaperCookingSystem>().NearestCookingPotPosition = null;
            ModContent.GetInstance<PaperCookingSystem>().HideUI();
        }

        private bool CanCook(int item1, int amount1, int item2, int amount2)
        {
            if (!(item1 == ItemID.None && item2 == ItemID.None)
                && (amount1 > 0 || amount2 > 0)
                && !(amount1 <= 0 && amount2 <= 0)) return true;
            else return false;
        }

        private int CookItems(int item1, int amount1, int item2, int amount2)
        {
            int resultItem = ItemID.None;
            //slot 1 has 0 stack, slot 2 has >0 stack
            //if (amount1 <= 0 && amount2 > 0) resultItem = BeginCooking(item2, ItemID.None);
            //if (amount1 > 0 && amount2 <= 0) resultItem = BeginCooking(item1, ItemID.None);
            resultItem = BeginCooking(item1, item2);
            if (amount1 <= 0 && amount2 <= 0) resultItem = ItemID.None;
            return resultItem;
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
            //slot #1 item finds nothing in recipe slot #1
            if (FirstRoundChosenList.Count <= 0)
            {
                //initiate second round
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
                            return result = resultingItem;
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
                        return result = resultingItem;
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
                            return result = resultingItem;
                        };
                    }
                }
            }
            //if all fails, return a Mistake
            return result;
        }

        private void SpawnResultItem(int resultItem, int amount1, int amount2)
        {
            IEntitySource itemSource = Main.LocalPlayer.GetSource_TileInteraction((int)CookingPotPosition.X, (int)CookingPotPosition.Y);
            int itemAmount = amount1;
            if (amount1 > amount2) itemAmount = amount2;
            if (amount1 <= 0) itemAmount = amount2;
            if (amount2 <= 0) itemAmount = amount1;
            if (resultItem != ItemID.None)
            {
                SoundEngine.PlaySound(SoundID.MenuOpen);
                Main.LocalPlayer.QuickSpawnItem(itemSource, resultItem, itemAmount);
                for (int i = 0; i < itemSlots.Count; i++)
                {
                    itemSlots[i].Item.stack -= itemAmount;
                    if (itemSlots[i].Item.stack <= 0) itemSlots[i].Item.type = ItemID.None;
                }
            }
        }
    }
}