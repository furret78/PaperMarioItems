using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PaperMarioItems.Common.RecipeSystem;
using PaperMarioItems.Content;
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
        private readonly UIText plusSign = new("+");
        private List<ItemSlotWrapper> itemSlots;
        private readonly float itemSlotSize = 52 * 0.65f;
        private readonly int[] itemSlotsL = [20, 20 + (int)(2.5f * (52 * 0.65f))];
        public readonly string CookItem = Language.GetTextValue($"Mods.PaperMarioItems.Common.UI.CookItem");

        public override void OnInitialize()
        {
            DraggablePanel = new PanelUI();
            DraggablePanel.SetPadding(0);
            float panelWidth = itemSlotsL[itemSlotsL.Length - 1] + itemSlotSize + 30f;
            //Vector2 InitPos2 = new(-1000, Main.screenHeight / 5);
            //SetRect(DraggablePanel, InitPos2.X, InitPos2.Y, panelWidth, 96f);
            Vector2 InitPos = new((Main.screenWidth / 2) - (panelWidth / 2), Main.screenHeight / 5);
            SetRect(DraggablePanel, InitPos.X, InitPos.Y, panelWidth, 96f);
            DraggablePanel.BackgroundColor = new(73, 94, 171, 200);

            itemSlots = [new(scale: 0.75f), new(scale: 0.75f)];

            for (int index = 0; index < itemSlots.Count; index++)
            {
                SetRect(itemSlots[index], itemSlotsL[index], 40f, itemSlotSize, itemSlotSize);
                DraggablePanel.Append(itemSlots[index]);
                if (index < itemSlots.Count - 1)
                {
                    plusSign.VAlign = 0.5f;
                    plusSign.Left.Set(itemSlotsL[index] + ((itemSlotsL[index + 1] - itemSlotsL[index]) / 2) - 6, 0f);
                    itemSlots[index].Append(plusSign);
                }
            }

            Asset<Texture2D> buttonPlayTexture = ModContent.Request<Texture2D>("Terraria/Images/UI/ButtonFavoriteActive");
            HoverImageButton cookButton = new(buttonPlayTexture, "Cook");
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
                if (item.type != ItemID.None && item.stack > 0)
                    Main.LocalPlayer.QuickSpawnItem(new EntitySource_Misc("Closed cooking UI"), item, item.stack);
                item.TurnToAir();
            }
        }

        private static void SetRect(UIElement uiElement, float left, float top, float width, float height)
        {
            uiElement.Left.Set(left, 0f);
            uiElement.Top.Set(top, 0f);
            uiElement.Width.Set(width, 0f);
            uiElement.Height.Set(height, 0f);
        }

        private void PlayButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            int item1 = itemSlots[0].Item.type, amount1 = itemSlots[0].Item.stack, item2 = itemSlots[1].Item.type, amount2 = itemSlots[1].Item.stack;
            if (CanCook(item1, amount1, item2, amount2) && Main.LocalPlayer.HasItemInInventoryOrOpenVoidBag(PMItemID.Cookbook))
            {
                int maxDeduction = ItemAmountFinalize(amount1, amount2);
                if (!MysteryBoxCheck(item1, item2))
                {
                    int resultItem = CookItems(item1, amount1, item2, amount2);
                    SpawnResultItem(resultItem, maxDeduction);
                }
                else
                {
                    for (int j = 0; j < maxDeduction; j++)
                    {
                        int resultItem = CookItems(item1, amount1, item2, amount2);
                        SpawnResultItem(resultItem, 1);
                    }
                }
                for (int i = 0; i < itemSlots.Count; i++)
                {
                    if (itemSlots[i].Item.type == ItemID.None || itemSlots[i].Item.stack <= 0)
                        itemSlots[i].Item.TurnToAir();
                }
            }
        }

        private void CloseButtonClicked(UIMouseEvent evt, UIElement listeningElement)
        {
            CloseCookingUI();
        }

        private static void CloseCookingUI()
        {
            ModContent.GetInstance<PaperCookingSystem>().NearestCookingPotPosition = null;
            ModContent.GetInstance<PaperCookingSystem>().HideUI();
        }

        private static bool CanCook(int item1 = ItemID.None, int amount1 = 0, int item2 = ItemID.None, int amount2 = 0)
        {
            if (!(item1 == ItemID.None && item2 == ItemID.None)
                && (amount1 > 0 || amount2 > 0)
                && !(amount1 <= 0 && amount2 <= 0)) return true;
            else return false;
        }

        private static bool MysteryBoxCheck(int item1 = ItemID.None, int item2 = ItemID.None)
        {
            if ((item1 == PMItemID.MysteryBox && item2 == ItemID.None) ||
                (item1 == ItemID.None && item2 == PMItemID.MysteryBox) ||
                (item1 == PMItemID.MysteryBox && item2 == PMItemID.MysteryBox)) return true;
            else return false;
        }

        private static bool SpaceFoodCheck(int item1 = ItemID.None, int item2 = ItemID.None)
        {
            if (item1 == PMItemID.DriedBouquet || item2 == PMItemID.DriedBouquet) return true;
            return false;
        }

        private static int ItemAmountFinalize(int amount1 = 0, int amount2 = 0)
        {
            int itemAmount = amount1;
            if (amount1 > amount2) itemAmount = amount2;
            if (amount1 <= 0) itemAmount = amount2;
            if (amount2 <= 0) itemAmount = amount1;
            return itemAmount;
        }

        private static int CookItems(int item1 = ItemID.None, int amount1 = 0, int item2 = ItemID.None, int amount2 = 0)
        {
            int resultItem;
            resultItem = BeginCooking(item1, item2);
            if (amount1 <= 0 && amount2 <= 0) resultItem = ItemID.None;
            return resultItem;
        }

        /// <summary>
        /// This method handles the cooking part. Mystery Box check comes first, followed by Space Food check, then the standard recipe table last.
        /// </summary>
        /// <param name="itemslot1"></param>
        /// <param name="itemslot2"></param>
        /// <returns></returns>
        private static int BeginCooking(int itemslot1 = ItemID.None, int itemslot2 = ItemID.None)
        {
            int result = PMItemID.Mistake;
            var FirstRoundChosenList = new List<PMRecipe>();
            var SecondRoundChosenList = new List<PMRecipe>();
            if (MysteryBoxCheck(itemslot1, itemslot2))
            {
                if (Main.rand.NextBool(2))
                {
                    result = RecipeRegister.MysteryBoxRecipeList[Main.rand.Next(0, RecipeRegister.MysteryBoxRecipeList.Count)];
                }
                return result;
            }
            if (SpaceFoodCheck(itemslot1, itemslot2))
            {
                for (int i = 0; i < RecipeRegister.SpaceFoodList.Count; i++)
                {
                    int currentItem = RecipeRegister.SpaceFoodList[i];
                    if (!RecipeRegister.SpaceFoodBlacklist.Contains(currentItem) &&
                        ((itemslot1 == currentItem && itemslot2 == PMItemID.DriedBouquet) ||
                        (itemslot1 == PMItemID.DriedBouquet && itemslot2 == currentItem))) result = PMItemID.SpaceFood;
                }
            }
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
                        var searchResult = SecondRoundChosenList.Find(x => x.Ingredient1 == itemslot2);
                        if (IsLegalResult(searchResult.ResultingItem, searchResult.Hardmode, searchResult.Prehard))
                        {
                            return result = searchResult.ResultingItem;
                        };
                    }
                }
            }
            else
            {
                //found something (slot #1 item in recipe slot #1, now find slot #2 item in recipe slot #2)
                if (FirstRoundChosenList.Exists(x => x.Ingredient2 == itemslot2))
                {
                    var searchResult = FirstRoundChosenList.Find(x => x.Ingredient2 == itemslot2);
                    if (IsLegalResult(searchResult.ResultingItem, searchResult.Hardmode, searchResult.Prehard))
                    {
                        return result = searchResult.ResultingItem;
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
                        var searchResult = SecondRoundChosenList.Find(x => x.Ingredient1 == itemslot2);
                        if (IsLegalResult(searchResult.ResultingItem, searchResult.Hardmode, searchResult.Prehard))
                        {
                            return result = searchResult.ResultingItem;
                        };
                    }
                }
            }
            //if all fails, return a Mistake
            return result;
        }

        /// <summary>
        /// Checks whether the recipe's properties are valid at the time of checking.
        /// </summary>
        /// <param name="result">The result of the recipe.</param>
        /// <param name="hardmode">Is this recipe Hardmode-only?</param>
        /// <param name="prehard">Is this recipe pre-Hardmode-only?</param>
        /// <returns></returns>
        private static bool IsLegalResult(int result, bool hardmode = false, bool prehard = false)
        {
            if (result != ItemID.None &&
                ((!hardmode && prehard && !Main.hardMode) ||
                (hardmode && Main.hardMode) ||
                (!hardmode && !prehard))) return true;
            else return false;
        }

        private void SpawnResultItem(int resultItem, int amount)
        {
            if (resultItem != ItemID.None)
            {
                if (resultItem == PMItemID.Mistake) SoundEngine.PlaySound(SoundID.MenuClose);
                else
                {
                    SoundEngine.PlaySound(SoundID.MenuOpen);
                    SoundEngine.PlaySound(SoundID.ResearchComplete);
                }
                Main.LocalPlayer.QuickSpawnItem(new EntitySource_Misc("Finished cooking"), resultItem, amount);
                for (int i = 0; i < itemSlots.Count; i++)
                {
                    itemSlots[i].Item.stack -= amount;
                }
            }
        }
    }
}