using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.OpenGL;
using Avalonia.OpenGL.Controls;
using System.Runtime.InteropServices;
using Baksteen.Graphics.Impeller;
using static Baksteen.Graphics.Impeller.ImpellerNative;
using Avalonia.Threading;
using static Avalonia.OpenGL.GlConsts;
using System.Diagnostics;
using Avalonia;
using Avalonia.VisualTree;

namespace ImpellerDemoAvalonia;

public class ImpellerControl : OpenGlControlBase
{
    ImpellerContext? impellerContext;
    ImpellerSurface? impellerSurface;
    ImpellerTypographyContext? impellerTypographyContext;
    ImpellerTexture? impellerTexture;
    private Stopwatch clock = new();
    private List<Sprite> sprites;

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

        public float X { get; set; }
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
                    X = Random.Shared.Next(0, 800),
                    Y = Random.Shared.Next(0, 600),
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

    protected override void OnOpenGlRender(GlInterface gl, int fb)
    {
        var scale = this.VisualRoot?.RenderScaling ?? 1.0;

        PixelSize pixelSize = PixelSize.FromSizeWithDpi(Bounds.Size, 96.0 * scale);
        int pixelWidth = Math.Max(8, pixelSize.Width);
        int pixelHeight = Math.Max(8, pixelSize.Height);

        gl.Viewport(0, 0, pixelWidth, pixelHeight);
        //gl.
        gl.ClearDepth(1);
        gl.Disable(GL_CULL_FACE);
        gl.Disable(GL_SCISSOR_TEST);
        gl.DepthFunc(GL_LESS);
        gl.DepthMask(1);

        gl.ClearColor(0, 0, 0, 0);
        //gl.Clear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
        gl.Enable(GL_DEPTH_TEST);
        //gl.Enable(GL_SAMPLES); ?? not sure how to enable supersampling
        //gl.Enable(0x809D); // GL_MULTISAMPLE

        //base.OnOpenGlRender(gl, fb);
        //gl.ClearColor(0.0f,1.0f,0.0f,1.0f);
        //gl.ClearColor(0, 0, 0, 0);
        //gl.Clear(GL_COLOR_BUFFER_BIT);
        //gl.Enable(GL_DEPTH_TEST);
        //gl.Clear((uint)(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit));
        //gl.Enable(EnableCap.DepthTest);
        //gl.Flush();

        if (impellerContext == null) return;

        if (impellerSurface == null || impellerSurface.Width != pixelWidth || impellerSurface.Height != pixelHeight)
        {
            impellerSurface?.Dispose();

            impellerSurface = new ImpellerSurface(
                    impellerContext,
                    (ulong)fb,
                    pixelWidth,
                    pixelHeight);
        }

        impellerTexture ??= CreateImageTexture(impellerContext);

        using (var displayListBuilder = new ImpellerDisplayListBuilder())
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

            foreach (Sprite sprite in sprites)
            {
                displayListBuilder.ResetTransform();

                displayListBuilder.Translate(sprite.X, sprite.Y);
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
                        width =  sprite.StartSize,
                        height = sprite.StartSize,
                    },
                    ImpellerTextureSampling.kImpellerTextureSamplingNearestNeighbor,
                    texturePaint);
            }

            displayListBuilder.ResetTransform();
            displayListBuilder.DrawRect(new()
            {
                x = 0,
                y = 0,
                width = impellerSurface.Width,
                height = impellerSurface.Height,
            }, redPaint);

            //displayListBuilder.Translate(8, 8);

            using var displayList = displayListBuilder.CreateDisplayList();
            impellerSurface.DrawDisplayList(displayList);
        }

        RequestNextFrameRendering();
    }

    protected override void OnOpenGlInit(GlInterface gl)
    {
        base.OnOpenGlInit(gl);

        var extensions = gl.ContextInfo.Extensions.ToList();

        impellerContext = new ImpellerContext(x => gl.GetProcAddress(x));
        impellerTypographyContext = new();
        impellerTypographyContext.RegisterFont("Ubuntu-Regular.ttf", "ubuntu");
        clock.Start();
    }

    protected override void OnOpenGlDeinit(GlInterface gl)
    {
        base.OnOpenGlDeinit(gl);

        impellerTexture?.Dispose();
        impellerTexture = null;

        impellerContext?.Dispose();
        impellerContext = null;

        impellerTypographyContext?.Dispose();
        impellerTypographyContext = null;
    }
}
