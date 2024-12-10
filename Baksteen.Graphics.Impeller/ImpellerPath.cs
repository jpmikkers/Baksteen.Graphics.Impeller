namespace Baksteen.Graphics.Impeller;

using System;

public class ImpellerPath : IDisposable
{
    private readonly ImpellerPathSafeHandle _handle;
    private bool disposedValue;

    public ImpellerPathSafeHandle Handle => _handle;

    internal ImpellerPath(ImpellerPathSafeHandle handle)
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
