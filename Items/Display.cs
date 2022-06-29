    using Terraria.UI;
    using Terraria.GameContent.UI.Elements;
    using Terraria;

    namespace ATB.Items
    {
        class Display : UIState
        {
            public LCARS LCARS;
            public LCARSButton1 LCARSButton1;
            public LCARSButton2 LCARSButton2;
            public LCARSButton3 LCARSButton3;
            public LCARSButton4 LCARSButton4;
            public PADDFrame PADDFrame;
            public UIText text;
            public UIText text2; 
            public UIElement panel = new UIElement();
            public UIElement panel2 = new UIElement();

            public override void OnInitialize()
            {
                LCARS = new LCARS();
                LCARSButton1 = new LCARSButton1();
                LCARSButton2 = new LCARSButton2();
                LCARSButton3 = new LCARSButton3();
                LCARSButton4 = new LCARSButton4();
                PADDFrame = new PADDFrame();

                Append(LCARS);
                Append(LCARSButton1);
                Append(LCARSButton2);
                Append(LCARSButton3);
                Append(LCARSButton4);
                Append(PADDFrame);

                panel.Width.Set(100, 0);
                panel.Height.Set(100, 0);
                panel.HAlign = 0.5f;
                panel.VAlign = 0.3f;
                Append(panel);

                panel2.Width.Set(100, 0);
                panel2.Height.Set(100, 0);
                panel2.HAlign = 0.5f;
                panel2.VAlign = 0.3f;
                Append(panel2);

                // text = new UIText(Main.LocalPlayer.position.Y.ToString());
                // text.HAlign = 0.5f; // 1
                // text.VAlign = 0.5f; // 1
                // panel.Append(text);

                // text2 = new UIText(Main.LocalPlayer.position.X.ToString());
                // text.HAlign = 0.4f; // 1
                // text.VAlign = 0.5f; // 1
                // panel2.Append(text2);
            }

        public override void Recalculate(){
                panel.RemoveAllChildren();
                panel2.RemoveAllChildren();
                text = new UIText((Main.LocalPlayer.Center.Y / 16).ToString());
                text.HAlign = -1f; // 1
                text.VAlign = -1f; // 1

                text2 = new UIText((Main.LocalPlayer.position.X /16).ToString());
                text.HAlign = -2f; // 1
                text.VAlign = -2f; // 1

                panel = new UIElement();
                panel.Width.Set(300, 0);
                panel.Height.Set(300, 0);
                panel.HAlign = 0.5f;
                panel.VAlign = 0.3f;
                Append(panel);

                panel2 = new UIElement();
                panel2.Width.Set(300, 0);
                panel2.Height.Set(300, 0);
                panel2.HAlign = 0.5f;
                panel2.VAlign = 0.3f;
                Append(panel2);

                panel.Append(text);
                panel2.Append(text2);
        }
    }
        

}