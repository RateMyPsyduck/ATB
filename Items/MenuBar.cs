    using Terraria.UI;

    namespace ATB.Items
    {
        class MenuBar : UIState
        {
            public LCARS LCARS;

            public override void OnInitialize()
            {
                LCARS = new LCARS();

                Append(LCARS);
            }
        }
    }