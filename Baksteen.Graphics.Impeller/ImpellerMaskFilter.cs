namespace Baksteen.Graphics.Impeller;

using System;
using static Baksteen.Graphics.Impeller.ImpellerNative;

public class ImpellerMaskFilter : IDisposable
{
    private readonly ImpellerMaskFilterSafeHandle _handle;
    private bool disposedValue;

    public ImpellerMaskFilterSafeHandle Handle => _handle;

    internal ImpellerMaskFilter(ImpellerMaskFilterSafeHandle handle)
    {
        _handle = handle;
    }

    public static ImpellerMaskFilter CreateBlur(ImpellerBlurStyle style, float sigma)
    {
        return new ImpellerMaskFilter(ImpellerNative.ImpellerMaskFilterCreateBlurNew(style,sigma).AssertValid());
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
