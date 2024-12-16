namespace Baksteen.Graphics.Impeller;

using System;

public class ImpellerSurface : IDisposable
{
    private readonly ImpellerSurfaceSafeHandle _handle;
    private bool disposedValue;

    public ImpellerSurfaceSafeHandle Handle => _handle;

    public int Width{get;private set;}
    public int Height{get;private set;}

    public ImpellerSurface(ImpellerContext context, ulong fbo, int width, int height)
    {
        Width = width;
        Height = height;

        _handle = ImpellerNative.ImpellerSurfaceCreateWrappedFBONew(
            context.Handle,
            fbo,
            ImpellerNative.ImpellerPixelFormat.kImpellerPixelFormatRGBA8888,
            new ImpellerNative.ImpellerISize { width =  Width, height = Height }
        ).AssertValid();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                _handle.Dispose();
            }
            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            disposedValue = true;
        }
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    public void DrawDisplayList(ImpellerDisplayList displayList)
    {
        ImpellerNative.ImpellerSurfaceDrawDisplayList(_handle, displayList.Handle);
    }
}
