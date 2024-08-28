using Terraria.UI;

namespace PaperMarioItems.Common.UI
{
    class MenuBar : UIState
    {
        public CookButton cookButton;
        public override void OnInitialize()
        {
            cookButton = new CookButton();
            Append(cookButton);
        }
    }
}