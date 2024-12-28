namespace Baksteen.Graphics.Impeller;

using System;

public class ImpellerContext : IDisposable
{
    private readonly ImpellerContextSafeHandle _handle;
    private readonly Func<string, nint> _linker;
    private bool disposedValue;

    public ImpellerContextSafeHandle Handle => _handle;

    public IntPtr MyProcAddressCallback(string procName, IntPtr userData)
    {
        //Console.WriteLine($"GetProcAddress for {procName}");
        return _linker(procName);   // Glfw.GetProcAddress(procName);
    }

    public ImpellerContext(Func<string,IntPtr> linker)
    {
        this._linker = linker;
        ImpellerNative.ImpellerProcAddressCallback procAddressCallback = MyProcAddressCallback;

        _handle = ImpellerNative.ImpellerContextCreateOpenGLESNew(
            ImpellerNative.ImpellerGetVersion(),
            procAddressCallback,
            IntPtr.Zero // userData
        ).AssertValid();
    }

    public static Version GetVersion()
    {
        var rawVersion = ImpellerNative.ImpellerGetVersion();
        var versionVariant = rawVersion >> 29;
        var versionMajor = (rawVersion >> 22) & 0x7FU;
        var versionMinor = (rawVersion >> 12) & 0x3FFU;
        var versionPatch = rawVersion & 0xFFFU;
        return new Version((int)versionMajor, (int)versionMinor, (int)versionVariant, (int)versionPatch);
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
