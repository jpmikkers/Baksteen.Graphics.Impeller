namespace Baksteen.Graphics.Impeller;

using DotGLFW;
using System;
using System.Runtime.InteropServices;

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

        using var surface = new ImpellerSurface(
            context,
            0L,
            ImpellerNative.ImpellerPixelFormat.kImpellerPixelFormatRGBA8888,
            new ImpellerNative.ImpellerISize
            {
                width = fbWidth,
                height = fbHeight
            }
        );

        ImpellerDisplayList displayList;

        using (var builder = new ImpellerDisplayListBuilder())
        using (var paint = new ImpellerPaint())
        {
            //paint.BlendMode = ImpellerNative.ImpellerBlendMode.kImpellerBlendModeDestinationOut;

            builder.Translate(100.5f, 0.5f);
            builder.Rotate(10.0f);

            paint.Color = new()
            {
                alpha = 1.0f,
                blue = 0.0f,
                green = 1.0f,
                red = 1.0f
            };

            builder.DrawPaint( paint );

            paint.Color = new()
            {
                alpha = 1.0f,
                blue = 0.0f,
                green = 0.0f,
                red = 1.0f
            };

            paint.DrawStyle = ImpellerNative.ImpellerDrawStyle.kImpellerDrawStyleStrokeAndFill;
            //paint.StrokeCap = ImpellerNative.ImpellerStrokeCap.kImpellerStrokeCapButt;
            paint.StrokeWidth = 10.5f;
            //paint.StrokeMiter = 10.0f;

            builder.DrawRect(
              new ImpellerNative.ImpellerRect
              {
                  x = 10,
                  y = 10,
                  width = 100,
                  height = 100
              },
              paint);

            // draw rounded rectangle
            paint.Color = new() {
                alpha = 1.0f,
                blue = 1.0f,
                green = 0.0f,
                red = 0.0f
            };

            builder.DrawRoundedRect(
                new ImpellerNative.ImpellerRect
                {
                    x = 150,
                    y = 10,
                    width = 100,
                    height = 100
                },
                new ImpellerNative.ImpellerRoundingRadii
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
                new ImpellerNative.ImpellerRect
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
            paint.StrokeCap = ImpellerNative.ImpellerStrokeCap.kImpellerStrokeCapButt;

            builder.DrawLine(
                new ImpellerNative.ImpellerPoint
                {
                    x=10,
                    y=200
                },
                new ImpellerNative.ImpellerPoint
                {
                    x=200,
                    y=300
                },
                paint);

            displayList = builder.CreateDisplayList();
        }

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
