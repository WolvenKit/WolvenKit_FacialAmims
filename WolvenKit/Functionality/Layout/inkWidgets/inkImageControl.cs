using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WolvenKit.RED4.Types;
using Rect = System.Windows.Rect;
using SizeF = System.Windows.Size;

namespace WolvenKit.Functionality.Layout.inkWidgets
{
    public class inkImageControl : inkControl
    {
        public inkImageWidget ImageWidget => Widget as inkImageWidget;

        public System.Drawing.Size OriginalImageSize { get; set; }
        private ImageSource OriginalImageSource;
        private RectF nsRect;
        private WolvenKit.RED4.Types.Rect Rect;

        private ImageSource ImageSource;
        public bool UsingNineScale { get; set; }

        public string TextureAtlas { get; set; }
        public string TexturePart { get; set; }

        public SizeF RenderedSize = new SizeF();

        public override double Width => Widget.FitToContent ? OriginalImageSize.Width : Widget.Size.X;
        public override double Height => Widget.FitToContent ? OriginalImageSize.Height : Widget.Size.Y;

        public inkImageControl(inkImageWidget widget) : base(widget)
        {

            if (ImageWidget.TextureAtlas == null)
                return;

            TextureAtlas = ImageWidget.TextureAtlas.DepotPath;
            TexturePart = ImageWidget.TexturePart;

            OriginalImageSource = (ImageSource)Application.Current.TryFindResource("ImageSource/" + TextureAtlas + "#" + TexturePart);

            if (OriginalImageSource == null)
                return;

            OriginalImageSize = new System.Drawing.Size((int)Math.Round(OriginalImageSource.Width), (int)Math.Round(OriginalImageSource.Height));

            if (ImageWidget.UseNineSliceScale)
            {
                nsRect = (RectF)Application.Current.TryFindResource("RectF/" + TextureAtlas + "#" + TexturePart);
                if (nsRect != null)
                {
                    UsingNineScale = true;
                    Rect = new RED4.Types.Rect()
                    {
                        Left = (int)Math.Round(nsRect.Left),
                        Top = (int)Math.Round(nsRect.Top),
                        Right = (int)Math.Round(nsRect.Right),
                        Bottom = (int)Math.Round(nsRect.Bottom)
                    };
                }
            }

            DrawImage(new SizeF(Width, Height));

            RenderOptions.SetBitmapScalingMode(this, BitmapScalingMode.NearestNeighbor);
        }

        protected void DrawImage(SizeF size)
        {
            RenderedSize = size;
            if (size.Width == 0 || size.Height == 0)
                return;

            Bitmap sourceBitmap;
            using (var outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create((BitmapSource)OriginalImageSource));
                enc.Save(outStream);
                sourceBitmap = new Bitmap(outStream);
            }

            Bitmap destBitmap = new Bitmap((int)Math.Round(size.Width), (int)Math.Round(size.Height));

            using (Graphics gfx = Graphics.FromImage(destBitmap))
            {
                // this is doesn't account for premultipied alpha, so the opacity mask is still needed
                var matrix = new ColorMatrix(new float[][]
                {
                        new float[] { 0, 0, 0, 0, 0},
                        new float[] { 0, 0, 0, 0, 0},
                        new float[] { 0, 0, 0, 0, 0},
                        new float[] { 0, 0, 0, 0, 0},
                        new float[] { 0, 0, 0, 0, 0},
                });
                matrix.Matrix03 = Widget.TintColor.Alpha;
                matrix.Matrix40 = Widget.TintColor.Red;
                matrix.Matrix41 = Widget.TintColor.Green;
                matrix.Matrix42 = Widget.TintColor.Blue;

                ImageAttributes attributes = new ImageAttributes();
                attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                if (UsingNineScale)
                {
                    attributes.SetWrapMode(System.Drawing.Drawing2D.WrapMode.TileFlipXY);

                    if (Rect.Top != 0)
                    {
                        if (Rect.Left != 0)
                            gfx.DrawImage(sourceBitmap, new Rectangle(
                                0, 0, Rect.Left, Rect.Top),
                                0, 0, Rect.Left, Rect.Top,
                                GraphicsUnit.Pixel, attributes);
                        gfx.DrawImage(sourceBitmap, new Rectangle(
                            Rect.Left, 0, (destBitmap.Width - Rect.Left - Rect.Right), Rect.Top),
                            Rect.Left + (Rect.Right == 0 ? -1 : 0) + (Rect.Left == 0 ? -1 : 0), 0, Math.Max(OriginalImageSize.Width - Rect.Left - Rect.Right, 1), Rect.Top,
                            GraphicsUnit.Pixel, attributes);
                        if (Rect.Right != 0)
                            gfx.DrawImage(sourceBitmap, new Rectangle(
                                (destBitmap.Width - Rect.Right), 0, (Rect.Right), Rect.Top),
                                (OriginalImageSize.Width - Rect.Right), 0, (Rect.Right), Rect.Top,
                                GraphicsUnit.Pixel, attributes);
                    }

                    if (Rect.Left != 0)
                        gfx.DrawImage(sourceBitmap, new Rectangle(
                            0, Rect.Top, Rect.Left, destBitmap.Height - Rect.Top - Rect.Bottom),
                            0, Rect.Top + (Rect.Bottom == 0 ? -1 : 0) + (Rect.Top == 0 ? -1 : 0), Rect.Left, Math.Max(OriginalImageSize.Height - Rect.Top - Rect.Bottom, 1),
                            GraphicsUnit.Pixel, attributes);
                    gfx.DrawImage(sourceBitmap, new Rectangle(
                        Rect.Left, Rect.Top, (destBitmap.Width - Rect.Left - Rect.Right), (destBitmap.Height - Rect.Top - Rect.Bottom)),
                        Rect.Left + (Rect.Right == 0 ? -1 : 0) + (Rect.Left == 0 ? -1 : 0), Rect.Top + (Rect.Bottom == 0 ? -1 : 0) + (Rect.Top == 0 ? -1 : 0), Math.Max(OriginalImageSize.Width - Rect.Left - Rect.Right, 1), Math.Max(OriginalImageSize.Height - Rect.Top - Rect.Bottom, 1),
                        GraphicsUnit.Pixel, attributes);
                    if (Rect.Right != 0)
                        gfx.DrawImage(sourceBitmap, new Rectangle(
                            (destBitmap.Width - Rect.Right), Rect.Top, (Rect.Right), (destBitmap.Height - Rect.Top - Rect.Bottom)),
                            (OriginalImageSize.Width - Rect.Right), Rect.Top + (Rect.Bottom == 0 ? -1 : 0) + (Rect.Top == 0 ? -1 : 0), (Rect.Right), Math.Max(OriginalImageSize.Height - Rect.Top - Rect.Bottom, 1),
                            GraphicsUnit.Pixel, attributes);

                    if (Rect.Bottom != 0)
                    {
                        if (Rect.Left != 0)
                            gfx.DrawImage(sourceBitmap, new Rectangle(
                            0, (destBitmap.Height - Rect.Bottom), Rect.Left, Rect.Bottom),
                            0, (OriginalImageSize.Height - Rect.Bottom), Rect.Left, Rect.Bottom,
                            GraphicsUnit.Pixel, attributes);
                        gfx.DrawImage(sourceBitmap, new Rectangle(
                            Rect.Left, (destBitmap.Height - Rect.Bottom), (destBitmap.Width - Rect.Left - Rect.Right), Rect.Bottom),
                            Rect.Left + (Rect.Right == 0 ? -1 : 0) + (Rect.Left == 0 ? -1 : 0), (OriginalImageSize.Height - Rect.Bottom), Math.Max(OriginalImageSize.Width - Rect.Left - Rect.Right, 1), Rect.Bottom,
                            GraphicsUnit.Pixel, attributes);
                        if (Rect.Right != 0)
                            gfx.DrawImage(sourceBitmap, new Rectangle(
                            (destBitmap.Width - Rect.Right), (destBitmap.Height - Rect.Bottom), (Rect.Right), Rect.Bottom),
                            (OriginalImageSize.Width - Rect.Right), (OriginalImageSize.Height - Rect.Bottom), (Rect.Right), Rect.Bottom,
                            GraphicsUnit.Pixel, attributes);
                    }

                    //        gfx.DrawImage(sourceBitmap, new Rectangle(
                    //            0, 0, Rect.Left, Rect.Top),
                    //            0, 0, Rect.Left, Rect.Top,
                    //            GraphicsUnit.Pixel, attributes);
                    //    gfx.DrawImage(sourceBitmap, new Rectangle(
                    //        Rect.Left, 0, (destBitmap.Width - Rect.Left - Rect.Right), Rect.Top),
                    //        Rect.Left, 0, OriginalImageSize.Width - Rect.Left - Rect.Right, Rect.Top,
                    //        GraphicsUnit.Pixel, attributes);

                    //        gfx.DrawImage(sourceBitmap, new Rectangle(
                    //            (destBitmap.Width - Rect.Right), 0, (Rect.Right), Rect.Top),
                    //            (OriginalImageSize.Width - Rect.Right), 0, (Rect.Right), Rect.Top,
                    //            GraphicsUnit.Pixel, attributes);



                    //    gfx.DrawImage(sourceBitmap, new Rectangle(
                    //        0, Rect.Top, Rect.Left, destBitmap.Height - Rect.Top - Rect.Bottom),
                    //        0, Rect.Top, Rect.Left, OriginalImageSize.Height - Rect.Top - Rect.Bottom,
                    //        GraphicsUnit.Pixel, attributes);
                    //gfx.DrawImage(sourceBitmap, new Rectangle(
                    //    Rect.Left, Rect.Top, (destBitmap.Width - Rect.Left - Rect.Right), (destBitmap.Height - Rect.Top - Rect.Bottom)),
                    //    Rect.Left, Rect.Top, OriginalImageSize.Width - Rect.Left - Rect.Right, OriginalImageSize.Height - Rect.Top - Rect.Bottom,
                    //    GraphicsUnit.Pixel, attributes);

                    //    gfx.DrawImage(sourceBitmap, new Rectangle(
                    //        (destBitmap.Width - Rect.Right), Rect.Top, (Rect.Right), (destBitmap.Height - Rect.Top - Rect.Bottom)),
                    //        (OriginalImageSize.Width - Rect.Right), Rect.Top, (Rect.Right), OriginalImageSize.Height - Rect.Top - Rect.Bottom,
                    //        GraphicsUnit.Pixel, attributes);


                    //        gfx.DrawImage(sourceBitmap, new Rectangle(
                    //        0, (destBitmap.Height - Rect.Bottom), Rect.Left, Rect.Bottom),
                    //        0, (OriginalImageSize.Height - Rect.Bottom), Rect.Left, Rect.Bottom,
                    //        GraphicsUnit.Pixel, attributes);
                    //    gfx.DrawImage(sourceBitmap, new Rectangle(
                    //        Rect.Left, (destBitmap.Height - Rect.Bottom), (destBitmap.Width - Rect.Left - Rect.Right), Rect.Bottom),
                    //        Rect.Left, (OriginalImageSize.Height - Rect.Bottom), OriginalImageSize.Width - Rect.Left - Rect.Right, Rect.Bottom,
                    //        GraphicsUnit.Pixel, attributes);
                    //        gfx.DrawImage(sourceBitmap, new Rectangle(
                    //        (destBitmap.Width - Rect.Right), (destBitmap.Height - Rect.Bottom), (Rect.Right), Rect.Bottom),
                    //        (OriginalImageSize.Width - Rect.Right), (OriginalImageSize.Height - Rect.Bottom), (Rect.Right), Rect.Bottom,
                    //        GraphicsUnit.Pixel, attributes);

                }
                else
                {
                    int x = 0, y = 0, width = OriginalImageSize.Width, height = OriginalImageSize.Height;
                    if (Widget.FitToContent)
                    {

                        switch (ImageWidget.Layout.HAlign.Value)
                        {
                            case Enums.inkEHorizontalAlign.Right:
                                x = destBitmap.Width - OriginalImageSize.Width;
                                break;
                            case Enums.inkEHorizontalAlign.Center:
                                x = (destBitmap.Width - OriginalImageSize.Width) / 2;
                                break;
                            case Enums.inkEHorizontalAlign.Fill:
                                width = destBitmap.Width;
                                break;
                            default:
                                break;
                        }
                        switch (ImageWidget.Layout.VAlign.Value)
                        {
                            case Enums.inkEVerticalAlign.Bottom:
                                y = destBitmap.Height - OriginalImageSize.Height;
                                break;
                            case Enums.inkEVerticalAlign.Center:
                                y = (destBitmap.Height - OriginalImageSize.Height) / 2;
                                break;
                            case Enums.inkEVerticalAlign.Fill:
                                height = destBitmap.Height;
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        width = destBitmap.Width;
                        height = destBitmap.Height;
                    }
                    gfx.DrawImage(sourceBitmap, new Rectangle(x, y, width, height), 0, 0, OriginalImageSize.Width, OriginalImageSize.Height, GraphicsUnit.Pixel, attributes);
                }
            }

            sourceBitmap.Dispose();

            var bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                destBitmap.GetHbitmap(),
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            destBitmap.Dispose();

            bitmapSource.Freeze();

            switch (ImageWidget.MirrorType.Value)
            {
                case Enums.inkBrushMirrorType.Both:
                    ImageSource = new TransformedBitmap(bitmapSource, new ScaleTransform(-1, -1));
                    break;
                case Enums.inkBrushMirrorType.Horizontal:
                    ImageSource = new TransformedBitmap(bitmapSource, new ScaleTransform(-1, 1));
                    break;
                case Enums.inkBrushMirrorType.Vertical:
                    ImageSource = new TransformedBitmap(bitmapSource, new ScaleTransform(1, -1));
                    break;
                case Enums.inkBrushMirrorType.NoMirror:
                    ImageSource = bitmapSource;
                    break;
            }

            ImageSource.Freeze();

            SetCurrentValue(OpacityMaskProperty, new ImageBrush()
            {
                ImageSource = ImageSource,
                Stretch = Stretch.Fill,
                AlignmentY = AlignmentY.Center,
                AlignmentX = AlignmentX.Center
            });
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            var newSize = new SizeF(RenderSize.Width, RenderSize.Height);
            //var newSize = DesiredSize;
            if (OriginalImageSource != null && RenderedSize != newSize)
                DrawImage(newSize);
            if (TintBrush != null && ImageSource != null)
                dc.DrawRectangle(TintBrush, null, new Rect(0, 0, newSize.Width, newSize.Height));
        }

        protected override SizeF MeasureCore(SizeF availableSize)
        {
            //if (Widget.FitToContent)
            //{
            //    var newSize = new SizeF(OriginalImageSource.Width, OriginalImageSource.Height);
            //    return base.MeasureOverride(newSize);
            //}
            //else
            //{
            //    return base.MeasureOverride(new SizeF(Width, Height));
            //}
            return base.MeasureCore(availableSize);
        }

        protected override void ArrangeCore(Rect finalRect)
        {
            base.ArrangeCore(finalRect);
        }
    }
}
