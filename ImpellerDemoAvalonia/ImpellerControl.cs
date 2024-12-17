using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.OpenGL;
using System.Runtime.InteropServices;
using Baksteen.Graphics.Impeller;
using static Baksteen.Graphics.Impeller.ImpellerNative;
using Avalonia.Threading;
using static Avalonia.OpenGL.GlConsts;
using System.Diagnostics;
using Avalonia;
using Avalonia.VisualTree;
using System.Diagnostics.CodeAnalysis;

namespace ImpellerDemoAvalonia;

public class ImpellerControl : ImpellerControlBase
{
    ImpellerTexture impellerTexture = default!;
    private readonly Stopwatch clock = new();
    private readonly List<Sprite> sprites;
    private long frameCount = 0;

    private class Sprite
    {
        public float StartAngle
        {
            get; set;
        }

        public float StartSize
        {
            get; set;
        }

        public float RotationSpeed
        {
            get; set;
        }

        public float X
        {
            get; set;
        }
        public float Y
        {
            get; set;
        }
    }

    public ImpellerControl() : base()
    {
        sprites = new List<Sprite>();
        for (int i = 0; i < 1000; i++)
        {
            sprites.Add(
                new Sprite
                {
                    X = Random.Shared.NextSingle(),
                    Y = Random.Shared.NextSingle(),
                    StartAngle = (float)(360.0 * Random.Shared.NextDouble()),
                    StartSize = (float)(16 + 100.0 * Random.Shared.NextDouble()),
                    RotationSpeed = (float)(90.0 - (180.0 * Random.Shared.NextDouble())),
                }
            );
        }
    }

    private static ImpellerTexture CreateImageTexture(ImpellerContext context)
    {
        var data = new byte[16 * 16 * 4];

        // draw a white crossed box with a gradient in the background
        for (var y = 0; y < 16; y++)
        {
            for (var x = 0; x < 16; x++)
            {
                if (x == y || x == (15 - y) || x == 0 || x == 15 || y == 0 || y == 15)
                {
                    data[(y * 16 + x) * 4 + 0] = 0xFF;  // R
                    data[(y * 16 + x) * 4 + 1] = 0xFF;  // G
                    data[(y * 16 + x) * 4 + 2] = 0xFF;  // B
                    data[(y * 16 + x) * 4 + 3] = 0xFF;  // A .. no effect?
                }
                else
                {
                    data[(y * 16 + x) * 4 + 0] = 0x80;              // R
                    data[(y * 16 + x) * 4 + 1] = (byte)(x * 16);      // G
                    data[(y * 16 + x) * 4 + 2] = (byte)(y * 16);      // B
                    data[(y * 16 + x) * 4 + 3] = 0xFF;  // A
                }
            }
        }

        return ImpellerTexture.CreateWithContents(
            context,
            new ImpellerTextureDescriptor
            {
                pixel_format = ImpellerPixelFormat.kImpellerPixelFormatRGBA8888,
                mip_count = 1,
                size = new ImpellerISize
                {
                    width = 16,
                    height = 16
                }
            },
            data,
            x => { Console.WriteLine("I was called!"); },
            IntPtr.Zero
        );
    }

    protected override void OnImpellerRender(ImpellerDisplayListBuilder displayListBuilder, ImpellerSurface surface)
    {
        using var redPaint = new ImpellerPaint
        {
            Color = new() { red = 1.0f, green = 0.0f, blue = 0.0f, alpha = 1.0f },
            DrawStyle = ImpellerDrawStyle.kImpellerDrawStyleStroke,
            StrokeWidth = 1.0f,
        };

        using var texturePaint = new ImpellerPaint
        {
            Color = new() { red = 0.0f, green = 0.0f, blue = 0.0f, alpha = 1.0f }
        };

        foreach (var sprite in sprites)
        {
            displayListBuilder.ResetTransform();

            displayListBuilder.Translate(sprite.X * surface.Width, sprite.Y * surface.Height);
            displayListBuilder.Rotate(sprite.StartAngle + (float)((sprite.RotationSpeed * clock.Elapsed.TotalSeconds) % 360.0));
            //displayListBuilder.Translate(50,50);

            displayListBuilder.DrawTextureRect(
                impellerTexture!,
                new ImpellerRect()
                {
                    x = 0,
                    y = 0,
                    width = 16,
                    height = 16
                },
                new ImpellerRect()
                {
                    x = -sprite.StartSize / 2,
                    y = -sprite.StartSize / 2,
                    width = sprite.StartSize,
                    height = sprite.StartSize,
                },
                ImpellerTextureSampling.kImpellerTextureSamplingNearestNeighbor,
                texturePaint);
        }

        /*
        displayListBuilder.ResetTransform();
        displayListBuilder.DrawRect(new()
        {
            x = 0,
            y = 0,
            width = surface.Width,
            height = surface.Height,
        }, redPaint);
        */

        displayListBuilder.ResetTransform();

        using var paragraphBackground = new ImpellerPaint
        {
            Color = new() { alpha = 0.8f, red = 0.3f, green = 0.3f, blue = 0.3f },
            DrawStyle = ImpellerDrawStyle.kImpellerDrawStyleFill,
        };

        using var paragraphForeground = new ImpellerPaint
        {
            Color = new() { alpha = 1.0f, red = 0.1f, green = 0.8f, blue = 0.6f },
            DrawStyle = ImpellerDrawStyle.kImpellerDrawStyleStroke,
            StrokeWidth = 4.0f
        };

        float fontSize = surface.Height / 6.0f;

        using var paragraphStyle = new ImpellerParagraphStyle()
        {
            Background = paragraphBackground,
            Foreground = paragraphForeground,
            FontFamily = "ubuntu",
            FontSize = fontSize,
            FontStyle = ImpellerFontStyle.kImpellerFontStyleNormal,
            FontWeight = ImpellerFontWeight.kImpellerFontWeight100,
            Height = 0.0f,
            MaxLines = 4,
            TextAlignment = ImpellerTextAlignment.kImpellerTextAlignmentCenter,
            TextDirection = ImpellerTextDirection.kImpellerTextDirectionLTR,
            Locale = "en-US",
        };

        using var paragraphBuilder = impellerTypographyContext.ParagraphBuilderNew();
        paragraphBuilder.PushStyle(paragraphStyle);
        paragraphBuilder.ImpellerParagraphBuilderAddText($"Hello Impeller\r\nframe {frameCount++}");
        using var paragraph = paragraphBuilder.BuildParagraphNew(surface.Width);
        displayListBuilder.DrawParagraph(
            paragraph, 
            new ImpellerPoint { 
                x = 100.0f * (float)Math.Sin(2*clock.Elapsed.TotalSeconds),
                y = (surface.Height - fontSize)/2.0f + 0.5f * (surface.Height-fontSize) * (float)Math.Sin( clock.Elapsed.TotalSeconds )  });
    }

    protected override void OnImpellerInit()
    {
        impellerTypographyContext.RegisterFont("Ubuntu-Regular.ttf", "ubuntu");
        impellerTexture = CreateImageTexture(impellerContext);
        clock.Start();
    }

    protected override void OnImpellerDeinit()
    {
        impellerTexture.Dispose();
        impellerTexture = null!;
    }
}
