using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.GameInput;
using Terraria.UI;

namespace PaperMarioItems.Common.UI;

public class ItemSlotWrapper : UIElement
{
    internal Item Item;
    public readonly int _context;
    public readonly float _scale;
    internal Func<Item, bool> ValidItemFunc;

    public ItemSlotWrapper(int context = ItemSlot.Context.BankItem, float scale = 1f)
    {
        _context = context;
        _scale = scale;
        Item = new Item();
        Item.SetDefaults(0);

        Width.Set(TextureAssets.InventoryBack9.Value.Width * scale, 0f);
        Height.Set(TextureAssets.InventoryBack9.Value.Height * scale, 0f);
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        var oldScale = Main.inventoryScale;
        Main.inventoryScale = _scale;
        var rectangle = GetDimensions().ToRectangle();

        if (ContainsPoint(Main.MouseScreen) && !PlayerInput.IgnoreMouseInterface)
        {
            Main.LocalPlayer.mouseInterface = true;
            if (ValidItemFunc == null || ValidItemFunc(Main.mouseItem))
            {
                ItemSlot.Handle(ref Item, _context);
            }
        }
        ItemSlot.Draw(spriteBatch, ref Item, _context, rectangle.TopLeft());
        Main.inventoryScale = oldScale;
    }
}