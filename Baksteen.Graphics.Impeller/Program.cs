namespace Baksteen.Graphics.Impeller;

using DotGLFW;
using System;
using System.Runtime.InteropServices;

internal class Program
{
    public static IntPtr MyProcAddressCallback(string procName, IntPtr userData)
    {
        //Console.WriteLine($"GetProcAddress for {procName}");
        return Glfw.GetProcAddress(procName);
    }

    private const int GL_COLOR_BUFFER_BIT = 0x00004000;

    private delegate void glClearColorHandler(float r, float g, float b, float a);
    private delegate void glClearHandler(int mask);
    private static glClearColorHandler glClearColor;
    private static glClearHandler glClear;

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

        glClearColor = Marshal.GetDelegateForFunctionPointer<glClearColorHandler>(
          Glfw.GetProcAddress("glClearColor"));
        glClear = Marshal.GetDelegateForFunctionPointer<glClearHandler>(
          Glfw.GetProcAddress("glClear"));

        Glfw.SetWindowIcon(window, [CreateIcon()]);

        Glfw.SetKeyCallback(window, (window, key, scancode, action, mods) =>
        {
            Console.WriteLine($"Key: {key}, Scancode: {scancode}, Action: {action}, Mods: {mods}");
        });

        GC.Collect();


        Console.WriteLine($"Running impeller version : {ImpellerCooked.GetVersion()}");

        ImpellerNative.ImpellerProcAddressCallback procAddressCallback = MyProcAddressCallback;
        IntPtr userData = IntPtr.Zero;

        IntPtr context = ImpellerNative.ImpellerContextCreateOpenGLESNew(ImpellerNative.ImpellerGetVersion(), procAddressCallback, userData);

        if (context == IntPtr.Zero)
        {
            throw new Exception("Failed to create Impeller context.");
        }

        ImpellerNative.ImpellerISize iSize = new()
        {
            Width = fbWidth,
            Height = fbHeight
        };

        IntPtr surface = ImpellerNative.ImpellerSurfaceCreateWrappedFBONew(context, 0, ImpellerNative.ImpellerPixelFormat.kImpellerPixelFormatRGBA8888, ref iSize);

        if (surface == IntPtr.Zero)
        {
            throw new Exception("Failed to create Impeller surface.");
        }

        IntPtr builder = ImpellerNative.ImpellerDisplayListBuilderNew(IntPtr.Zero);

        if (builder == IntPtr.Zero)
        {
            throw new Exception("Failed to create display list builder.");
        }

        IntPtr paint = ImpellerNative.ImpellerPaintNew();

        if (paint == IntPtr.Zero)
        {
            throw new Exception("Failed to create paint object.");
        }

        // clear screen
        ImpellerNative.ImpellerPaintSetColor(paint, color: new ImpellerNative.ImpellerColor { alpha = 1.0f, blue = 0.0f, green = 1.0f, red = 1.0f });
        ImpellerNative.ImpellerDisplayListBuilderDrawPaint(
          builder,
          paint);

        // draw rectangle
        ImpellerNative.ImpellerPaintSetColor(paint, color: new ImpellerNative.ImpellerColor { alpha = 1.0f, blue = 0.0f, green = 0.0f, red = 1.0f });
        ImpellerNative.ImpellerDisplayListBuilderDrawRect(
          builder,
          new ImpellerNative.ImpellerRect { x = 10, y = 10, width = 100, height = 100 },
          paint);

        // draw rounded rectangle
        ImpellerNative.ImpellerPaintSetColor(paint, color: new ImpellerNative.ImpellerColor { alpha = 1.0f, blue = 1.0f, green = 0.0f, red = 0.0f });
        ImpellerNative.ImpellerDisplayListBuilderDrawRoundedRect(
          builder,
          new ImpellerNative.ImpellerRect
          {
              x = 150,
              y = 10,
              width = 100,
              height = 100
          },
          new ImpellerNative.ImpellerRoundingRadii
          {
              top_left = new() { x = 20, y = 10 },
              bottom_left = new() { x = 20, y = 10 },
              top_right = new() { x = 20, y = 10 },
              bottom_right = new() { x = 20, y = 10 },
          },
          paint);

        // draw oval
        ImpellerNative.ImpellerPaintSetColor(paint, color: new ImpellerNative.ImpellerColor { alpha = 1.0f, blue = 0.0f, green = 1.0f, red = 0.0f });
        ImpellerNative.ImpellerDisplayListBuilderDrawOval(
          builder,
          new ImpellerNative.ImpellerRect { x = 290, y = 10, width = 100, height = 100 },
          paint);

        IntPtr displayList = ImpellerNative.ImpellerDisplayListBuilderCreateDisplayListNew(builder);

        if (builder == IntPtr.Zero)
        {
            throw new Exception("Failed to create display list.");
        }

        ImpellerNative.ImpellerPaintRelease(paint);
        ImpellerNative.ImpellerDisplayListBuilderRelease(builder);

        while (!Glfw.WindowShouldClose(window))
        {
            Glfw.PollEvents();

            ImpellerNative.ImpellerSurfaceDrawDisplayList(surface, displayList);

            Glfw.SwapBuffers(window);

            //double currentTime = Glfw.GetTime();
            //SetHueShiftedColor(currentTime * delta * 200);

            // Clear the buffer to the set color
            //glClear(GL_COLOR_BUFFER_BIT);
        }

        ImpellerNative.ImpellerDisplayListRelease(displayList);
        ImpellerNative.ImpellerSurfaceRelease(surface);
        ImpellerNative.ImpellerContextRelease(context);
    }

    private static void SetHueShiftedColor(double time)
    {
        // Set the clear color to a shifted hue
        float r = (float)(Math.Sin(time) / 2 + 0.5);
        float g = (float)(Math.Sin(time + 2) / 2 + 0.5);
        float b = (float)(Math.Sin(time + 4) / 2 + 0.5);
        float a = 1.0f;
        glClearColor(r, g, b, a);
    }

    private static Image CreateIcon()
    {
        var image = new Image
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
        return image;
    }
}
