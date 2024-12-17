namespace ImpellerDemoDotGLFW;

using DotGLFW;
using System;
using System.IO;
using System.Runtime.InteropServices;
using Baksteen.Graphics.Impeller;
using static Baksteen.Graphics.Impeller.ImpellerNative;
using static System.Runtime.InteropServices.JavaScript.JSType;

internal class Program
{
    private static Image CreateIcon()
    {
        return new Image
        {
            Width = 2,
            Height = 2,
            Pixels = [
                0, 0, 0, 255,
                255, 0, 0, 255,
                0, 255, 0, 255,
                0, 0, 255, 255
            ]
        };
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

    static void Main(string[] args)
    {
        Glfw.Init();

        var versionString = Glfw.GetVersionString();
        Console.WriteLine($"GLFW Version: {versionString}, Vulkan supported: {Glfw.VulkanSupported()}");

        Glfw.WindowHint(WindowHint.ClientAPI, ClientAPI.OpenGLAPI);
        Glfw.WindowHint(WindowHint.ContextVersionMajor, 3);
        Glfw.WindowHint(WindowHint.ContextVersionMinor, 3);
        Glfw.WindowHint(WindowHint.OpenGLProfile, OpenGLProfile.CoreProfile);
        Glfw.WindowHint(WindowHint.DoubleBuffer, true);
        Glfw.WindowHint(WindowHint.Decorated, true);
        Glfw.WindowHint(WindowHint.OpenGLForwardCompat, true);
        Glfw.WindowHint(WindowHint.Resizable, true);
        Glfw.WindowHint(WindowHint.Visible, false);
        Glfw.WindowHint(WindowHint.Samples, 4);     // enable 4x MSAA

        var WIDTH = 800;
        var HEIGHT = 600;
        var TITLE = "DotGLFW Impeller Example";

        // Create window
        var window = Glfw.CreateWindow(WIDTH, HEIGHT, TITLE, null, null);
        var primary = Glfw.GetPrimaryMonitor();

        Glfw.GetMonitorWorkarea(primary, out var wx, out var wy, out var ww, out var wh);
        Glfw.SetWindowPos(window, wx + ww / 2 - WIDTH / 2, wy + wh / 2 - HEIGHT / 2);
        Glfw.ShowWindow(window);

        Glfw.GetFramebufferSize(window, out var fbWidth, out var fbHeight);
        Console.WriteLine($"the framebuffer is {fbWidth} x {fbHeight}");

        Glfw.MakeContextCurrent(window);

        // Enable VSYNC
        Glfw.SwapInterval(1);

        var videoMode = Glfw.GetVideoMode(primary);
        int refreshRate = videoMode.RefreshRate;
        double delta = 1.0 / refreshRate;

        Glfw.SetWindowIcon(window, [CreateIcon()]);

        Glfw.SetKeyCallback(window, (window, key, scancode, action, mods) =>
        {
            Console.WriteLine($"Key: {key}, Scancode: {scancode}, Action: {action}, Mods: {mods}");
        });

        GC.Collect();

        Console.WriteLine($"Running impeller version : {ImpellerCooked.GetVersion()}");

        using var context = new ImpellerContext(procName => Glfw.GetProcAddress(procName));
        
        using var typographyContext = new ImpellerTypographyContext();
        typographyContext.RegisterFont("Ubuntu-Regular.ttf", "ubuntu");

        using var surface = new ImpellerSurface(
            context,
            0L,
            fbWidth,
            fbHeight
        );

        ImpellerDisplayList displayList;

        var mat = new ImpellerMatrix();

        unsafe
        {
            var m = mat.m;

            m[0] = 1; m[4] = 0; m[8] = 0; m[12] = 0;
            m[1] = 0; m[5] = 1; m[9] = 0; m[13] = 0;
            m[2] = 0; m[6] = 0; m[10] = 1; m[14] = 0;
            m[3] = 0; m[7] = 0; m[11] = 0; m[15] = 1;
        }

        var lingradcolors = new[]
        {
            new ImpellerColor(){ alpha = 1, red = 0, green = 0, blue = 0 },
            new ImpellerColor(){ alpha = 1, red = 1, green = 1, blue = 1 }
        };

        using var linearGradientColorSource = ImpellerColorSource.CreateLinearGradient(
            new()
            {
                x = 50,
                y = 0,
            },
            new()
            {
                x = 50,
                y = 50,
            },
            2,
            lingradcolors,
            new float[]
            {
                0,
                1
            },
            ImpellerTileMode.kImpellerTileModeMirror,
            mat);

        using var radialGradientColorSource = ImpellerColorSource.CreateRadialGradient(
            new()
            {
                x = 160,
                y = 250,
            },
            20,
            2,
            new[]
            {
                new ImpellerColor(){ alpha = 1, red = 1, green = 0, blue = 0 },
                new ImpellerColor(){ alpha = 1, red = 0, green = 0.5f, blue = 1 }
            },
            new float[]
            {
                0,
                1
            },
            ImpellerTileMode.kImpellerTileModeMirror,
            mat
        );

        using (var builder = new ImpellerDisplayListBuilder())
        using (var paint = new ImpellerPaint())
        {
            //paint.BlendMode = ImpellerBlendMode.kImpellerBlendModeDestinationOut;

            //builder.Translate(100.5f, 0.5f);
            //builder.Rotate(10.0f);

            paint.Color = new()
            {
                alpha = 1.0f,
                blue = 0.0f,
                green = 0.0f,
                red = 0.0f
            };

            builder.DrawPaint(paint);

            paint.Color = new()
            {
                alpha = 1.0f,
                blue = 0.0f,
                green = 0.0f,
                red = 1.0f
            };

            paint.DrawStyle = ImpellerDrawStyle.kImpellerDrawStyleStrokeAndFill;
            //paint.StrokeCap = ImpellerStrokeCap.kImpellerStrokeCapButt;
            paint.StrokeWidth = 10.5f;
            //paint.StrokeMiter = 10.0f;

            builder.DrawRect(
              new ImpellerRect
              {
                  x = 10,
                  y = 10,
                  width = 100,
                  height = 100
              },
              paint);

            // draw rounded rectangle
            paint.Color = new()
            {
                alpha = 1.0f,
                blue = 1.0f,
                green = 0.0f,
                red = 0.0f
            };

            builder.DrawRoundedRect(
                new ImpellerRect
                {
                    x = 150,
                    y = 10,
                    width = 100,
                    height = 100
                },
                new ImpellerRoundingRadii
                {
                    top_left = new() { x = 20, y = 20 },
                    bottom_left = new() { x = 20, y = 20 },
                    top_right = new() { x = 20, y = 20 },
                    bottom_right = new() { x = 20, y = 20 },
                },
                paint
            );

            // draw oval
            paint.Color = new()
            {
                alpha = 1.0f,
                blue = 0.0f,
                green = 1.0f,
                red = 0.0f
            };

            builder.DrawOval(
                new ImpellerRect
                {
                    x = 290,
                    y = 10,
                    width = 100,
                    height = 100
                },
                paint);

            // draw line
            paint.Color = new()
            {
                alpha = 1.0f,
                blue = 1.0f,
                green = 1.0f,
                red = 0.0f
            };

            paint.StrokeWidth = 20;
            paint.StrokeCap = ImpellerStrokeCap.kImpellerStrokeCapButt;

            builder.DrawLine(
                new ImpellerPoint
                {
                    x = 10,
                    y = 150
                },
                new ImpellerPoint
                {
                    x = 200,
                    y = 250
                },
                paint);

            builder.DrawDashedLine(
                new ImpellerPoint
                {
                    x = 110,
                    y = 150
                },
                new ImpellerPoint
                {
                    x = 300,
                    y = 250
                },
                10.0f,
                10.0f,
                paint);

            using var texture = CreateImageTexture(context);
            using var texturepaint = new ImpellerPaint();

            builder.DrawTextureRect(
                texture,
                new()
                {
                    x = 0,
                    y = 0,
                    width = 16,
                    height = 16
                },
                new()
                {
                    x = 450,
                    y = 10,
                    width = 100,
                    height = 100,
                },
                ImpellerTextureSampling.kImpellerTextureSamplingNearestNeighbor,
                texturepaint);

            using (var pathBuilder = new ImpellerPathBuilder())
            {
                pathBuilder.MoveTo(new ImpellerPoint { x = 70, y = 270 });
                pathBuilder.LineTo(new ImpellerPoint { x = 230, y = 270 });
                pathBuilder.LineTo(new ImpellerPoint { x = 100, y = 390 });
                pathBuilder.LineTo(new ImpellerPoint { x = 150, y = 200 });
                pathBuilder.LineTo(new ImpellerPoint { x = 200, y = 390 });
                pathBuilder.Close();

                paint.ColorSource = linearGradientColorSource;
                paint.Color = new()
                {
                    alpha = 1.0f,
                    blue = 0.5f,
                    green = 0.2f,
                    red = 0.7f
                };

                using var pathodd = pathBuilder.CopyPathNew(ImpellerFillType.kImpellerFillTypeOdd);
                builder.DrawPath(pathodd, paint);

                builder.Translate(200, 0);

                paint.ColorSource = radialGradientColorSource;

                using var pathnzr = pathBuilder.CopyPathNew(ImpellerFillType.kImpellerFillTypeNonZero);
                builder.DrawPath(pathnzr, paint);

                builder.Translate(200, 0);
                using var maskFilter = ImpellerMaskFilter.CreateBlur(ImpellerBlurStyle.kImpellerBlurStyleNormal, 4.0f);
                paint.MaskFilter = maskFilter;
                builder.DrawPath(pathnzr, paint);
            }

            builder.ResetTransform();
            using var paragraphStyle = new ImpellerParagraphStyle()
            {
                Background = new ImpellerPaint
                {
                    Color = new() { alpha = 1.0f, red = 0.3f, green = 0.3f, blue = 0.3f },
                    DrawStyle = ImpellerDrawStyle.kImpellerDrawStyleFill,
                },

                Foreground = new ImpellerPaint
                {
                    Color = new() { alpha = 1.0f, red = 1.0f, green = 1.0f, blue = 1.0f },
                    DrawStyle = ImpellerDrawStyle.kImpellerDrawStyleStrokeAndFill,
                },

                FontFamily = "ubuntu",
                FontSize = 80.0f,
                FontStyle = ImpellerFontStyle.kImpellerFontStyleNormal,
                FontWeight = ImpellerFontWeight.kImpellerFontWeight100,
                Height = 0.0f,
                MaxLines = 2,
                TextAlignment = ImpellerTextAlignment.kImpellerTextAlignmentCenter,
                TextDirection = ImpellerTextDirection.kImpellerTextDirectionLTR,
                Locale = "en-US",
            };

            using var paragraphBuilder = typographyContext.ParagraphBuilderNew();
            paragraphBuilder.PushStyle(paragraphStyle);
            paragraphBuilder.ImpellerParagraphBuilderAddText("Hello ");
            paragraphStyle.FontSize = 80.0f;
            paragraphStyle.Foreground = new ImpellerPaint
            {
                Color = new()
                {
                    alpha = 1.0f,
                    red = 1.0f,
                    green = 0.0f,
                    blue = 1.0f
                },
                DrawStyle = ImpellerDrawStyle.kImpellerDrawStyleStroke,
                StrokeWidth = 3.0f
            };
            paragraphStyle.Background = new ImpellerPaint
            {
                Color = new()
                {
                    alpha = 1.0f,
                    red = 0.0f,
                    green = 0.0f,
                    blue = 0.5f
                },
                DrawStyle = ImpellerDrawStyle.kImpellerDrawStyleStrokeAndFill,
                StrokeWidth = 3.0f
            };
            paragraphBuilder.PushStyle(paragraphStyle);
            paragraphBuilder.ImpellerParagraphBuilderAddText("Impeller!");
            using var paragraph = paragraphBuilder.BuildParagraphNew(800.0f);
            builder.DrawParagraph(paragraph,new ImpellerPoint { x = 0.0f, y = 450.0f });

            displayList = builder.CreateDisplayList();
        }

        Console.WriteLine("here!");

        using (displayList)
        {
            while (!Glfw.WindowShouldClose(window))
            {
                Glfw.PollEvents();
                surface.DrawDisplayList(displayList);

                Glfw.SwapBuffers(window);

                //double currentTime = Glfw.GetTime();
            }
        }
    }

}
