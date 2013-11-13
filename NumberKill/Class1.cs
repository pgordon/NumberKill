using System;


namespace NumberKill
{
    class Sprites : Form
    {
        // The objects that will be used to show 
        // the uses of the Sprite class 
        private Device device;
        private Microsoft.WindowsMobile.DirectX.Direct3D.Font d3dFont;
        private Sprite sprite;
        private Texture texture;

        public Sprites()
        {
            PresentParameters present;
            System.Drawing.Font gdiFont;

            this.Text = "Using Sprites";

            // Give the application a way to be closed. 
            // This must be done before the device is created 
            // as it will cause the hwnd of the Form to change. 
            this.MinimizeBox = false;

            present = new PresentParameters();
            present.Windowed = true;
            present.SwapEffect = SwapEffect.Discard;

            device = new Device(0, DeviceType.Default, this,
                CreateFlags.None, present);
            device.DeviceReset += new EventHandler(OnDeviceReset);

            // Construct a new Sprite. 
            // Sprites do not need to be recreated 
            // when a device is reset.
            sprite = new Sprite(device);

            gdiFont = new System.Drawing.Font
                (FontFamily.GenericSansSerif,
            10.0f, FontStyle.Regular);

            // Construct a new font. Fonts do not need 
            // to be recreated when a device is reset.
            d3dFont = new Microsoft.WindowsMobile.DirectX.Direct3D.Font
                (device, gdiFont);

            OnDeviceReset(null, EventArgs.Empty);
        }

        private void OnDeviceReset(object sender, EventArgs e)
        {
            // Textures must be recreated whenever a device is reset 
            // no matter what pool they are created in.
            texture = TextureLoader.FromFile(device, "image.bmp");
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // Do nothing.
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Begin the scene and clear the back buffer to black
            device.BeginScene();
            device.Clear(ClearFlags.Target, Color.Black, 1.0f, 0);

            // When using sprites it is important to 
            // specify sprite flags passed to Sprite.Begin

            sprite.Begin(SpriteFlags.SortTexture | SpriteFlags.AlphaBlend);

            // Draw an image to the screen using Sprite.Draw 

            int spriteY = 5;

            sprite.Draw(texture, Vector3.Empty, new Vector3(0,
                spriteY, 0),
                Color.White.ToArgb());
            spriteY += texture.GetLevelDescription(0).Height + 5;

            // Draw a portion of an image to the screen 
            // using Sprite.Draw. This shall be drawn such 
            // that the image is modulated with the color green.

            sprite.Draw(texture, new Rectangle(4, 4, 24, 24),
                Vector3.Empty,
                new Vector3(0, spriteY, 0), Color.Green);

            spriteY += 30;

            // Draw text to the screen. Using a sprite to draw text 
            // to the  screen is essential for good performance. 
            // Otherwise the font object will perform a 
            // Sprite.Begin/Sprite.End internally for 
            // each call to Font.DrawText. This can cause severe 
            // performance problems.

            spriteY = 150;

            d3dFont.DrawText(sprite, "This is text.",
                5, spriteY, Color.Red);
            spriteY += d3dFont.Description.Height + 5;

            d3dFont.DrawText(sprite, "This is another line of text.",
                5, spriteY, Color.Green);
            spriteY += d3dFont.Description.Height + 5;

            d3dFont.DrawText(sprite, "Only one call to Sprite.Begin.",
                5, spriteY, Color.Blue);

            // End drawing using this sprite. This will cause the 
            // sprites to be flushed to the graphics driver and will 
            // reset the transformation matrices, textures states, 
            // and renderstates if the SpriteFlags specified in Begin 
            // call for that to happen.
            sprite.End();

            // Finish the scene and present it on the screen.
            device.EndScene();
            device.Present();
        }

        static void Main()
        {
            Application.Run(new Sprites());
        }
    }
}