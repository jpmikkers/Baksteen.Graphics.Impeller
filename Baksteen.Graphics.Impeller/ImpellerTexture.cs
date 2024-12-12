namespace Baksteen.Graphics.Impeller;

using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using static Baksteen.Graphics.Impeller.ImpellerNative;

public class ImpellerTexture : IDisposable
{
    private readonly ImpellerTextureSafeHandle _handle;
    private bool disposedValue;

    public ImpellerTextureSafeHandle Handle => _handle;

    internal ImpellerTexture(ImpellerTextureSafeHandle handle)
    {
        _handle = handle;
    }

    public static ImpellerTexture CreateWithContents(
        ImpellerContext context,
        ImpellerTextureDescriptor descriptor,
        byte[] pixelData,
        Action<nint> callback,
        IntPtr contents_on_release_user_data)
    {
        var unmanagedPointer = Marshal.AllocHGlobal(pixelData.Length);
        Marshal.Copy(pixelData, 0, unmanagedPointer, pixelData.Length);

        try
        {
            return new ImpellerTexture(
                ImpellerTextureCreateWithContentsNew(
                    context.Handle,
                    descriptor,
                    new ImpellerMapping
                    {
                        data = unmanagedPointer,
                        length = (ulong)pixelData.Length,
                        on_release = x => callback(x)
                    },
                    contents_on_release_user_data).AssertValid());
        }
        finally
        {
            Marshal.FreeHGlobal(unmanagedPointer);
        }
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
