namespace Baksteen.Graphics.Impeller;

using System;

public class ImpellerDisplayList : IDisposable
{
    private readonly ImpellerDisplayListSafeHandle _handle;
    private bool disposedValue;

    public ImpellerDisplayListSafeHandle Handle => _handle;

    internal ImpellerDisplayList(ImpellerDisplayListSafeHandle handle)
    {
        _handle = handle;
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
