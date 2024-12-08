namespace Baksteen.Graphics.Impeller;

using System;

public class ImpellerSurface : IDisposable
{
    private readonly ImpellerSurfaceSafeHandle _handle;
    private bool disposedValue;

    public ImpellerSurfaceSafeHandle Handle => _handle;

    public ImpellerSurface(ImpellerContext context, ulong fbo, ImpellerNative.ImpellerPixelFormat pixelFormat, ImpellerNative.ImpellerISize size)
    {
        _handle = ImpellerNative.ImpellerSurfaceCreateWrappedFBONew(
            context.Handle,
            fbo,
            pixelFormat,
            size
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

    public void DrawDisplayList(ImpellerDisplayListSafeHandle displayList)
    {
        ImpellerNative.ImpellerSurfaceDrawDisplayList(_handle, displayList);
    }
}
