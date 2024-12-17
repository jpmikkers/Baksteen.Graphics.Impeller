using System;
using Avalonia;
using Avalonia.OpenGL;
using Avalonia.OpenGL.Controls;
using Avalonia.Threading;
using Baksteen.Graphics.Impeller;
using static Avalonia.OpenGL.GlConsts;

namespace ImpellerDemoAvalonia;

public abstract class ImpellerControlBase : OpenGlControlBase
{
    protected ImpellerContext impellerContext = default!;
    protected ImpellerTypographyContext impellerTypographyContext = default!;
    private ImpellerSurface? impellerSurface;

    protected abstract void OnImpellerInit();
    protected abstract void OnImpellerDeinit();
    protected abstract void OnImpellerRender(ImpellerDisplayListBuilder builder,ImpellerSurface surface);

    public ImpellerControlBase() : base()
    {
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

        using (var displayListBuilder = new ImpellerDisplayListBuilder())
        {
            OnImpellerRender(displayListBuilder,impellerSurface);
            using var displayList = displayListBuilder.CreateDisplayList();
            impellerSurface.DrawDisplayList(displayList);
        }

        // doing Post iso directly calling RequestNextFrameRendering seems to improve the long running stability
        Dispatcher.UIThread.Post(RequestNextFrameRendering, DispatcherPriority.Background);
    }

    protected override void OnOpenGlInit(GlInterface gl)
    {
        base.OnOpenGlInit(gl);
        impellerContext = new ImpellerContext(x => gl.GetProcAddress(x));
        impellerTypographyContext = new();
        OnImpellerInit();
    }

    protected override void OnOpenGlDeinit(GlInterface gl)
    {
        OnImpellerDeinit();
        impellerTypographyContext.Dispose();
        impellerTypographyContext = null!;
        impellerContext.Dispose();
        impellerContext = null!;
        base.OnOpenGlDeinit(gl);
    }
}
