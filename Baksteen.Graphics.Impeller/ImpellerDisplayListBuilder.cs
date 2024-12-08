namespace Baksteen.Graphics.Impeller;

using System;

public class ImpellerDisplayListBuilder : IDisposable
{
    private readonly ImpellerDisplayListBuilderSafeHandle _handle;
    private bool disposedValue;

    public ImpellerDisplayListBuilderSafeHandle Handle => _handle;

    public ImpellerDisplayListBuilder()
    {
        _handle = ImpellerNative.ImpellerDisplayListBuilderNew(IntPtr.Zero).AssertValid();  // todo cullrect
    }

    public void DrawPaint(ImpellerPaint paint)
    {
        ImpellerNative.ImpellerDisplayListBuilderDrawPaint(
            Handle,
            paint.Handle);
    }

    public void DrawRect(ImpellerNative.ImpellerRect rect,ImpellerPaint paint)
    {
        ImpellerNative.ImpellerDisplayListBuilderDrawRect(
          Handle,
          rect,
          paint.Handle);
    }

    public void DrawRoundedRect(ImpellerNative.ImpellerRect rect, ImpellerNative.ImpellerRoundingRadii radii, ImpellerPaint paint)
    {
        ImpellerNative.ImpellerDisplayListBuilderDrawRoundedRect(
            Handle,
            rect,
            radii,
            paint.Handle
        );
    }

    public void DrawOval(ImpellerNative.ImpellerRect rect, ImpellerPaint paint)
    {
        ImpellerNative.ImpellerDisplayListBuilderDrawOval(
          Handle,
          rect,
          paint.Handle);
    }

    public void DrawLine(ImpellerNative.ImpellerPoint from, ImpellerNative.ImpellerPoint to, ImpellerPaint paint)
    {
        ImpellerNative.ImpellerDisplayListBuilderDrawLine(
            Handle,
            from,
            to,
            paint.Handle);
    }

    public ImpellerDisplayList CreateDisplayListNew()
    {
        return new ImpellerDisplayList(ImpellerNative.ImpellerDisplayListBuilderCreateDisplayListNew(Handle).AssertValid());
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
}