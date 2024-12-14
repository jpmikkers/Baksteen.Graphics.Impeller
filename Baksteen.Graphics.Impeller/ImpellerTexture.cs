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
        // pin the array, possibly prevents an additional duplicate of the texture data
        var gch = GCHandle.Alloc(pixelData,GCHandleType.Pinned);

        // NOTE: unlike with ImpellerTypographyContextRegisterFont, it seems the on_release callback here _is_ actually
        // called, and immediately during/after the CreateWithContentsNew call

        try
        {
            return new ImpellerTexture(
                ImpellerTextureCreateWithContentsNew(
                    context.Handle,
                    descriptor,
                    new ImpellerMapping
                    {
                        data = gch.AddrOfPinnedObject(),
                        length = (ulong)pixelData.Length,
                        on_release = x => callback(x)
                    },
                    contents_on_release_user_data).AssertValid());
        }
        finally
        {
            gch.Free();
        }
    }

    //public static ImpellerTexture CreateWithContents(
    //    ImpellerContext context,
    //    ImpellerTextureDescriptor descriptor,
    //    byte[] pixelData,
    //    Action<nint> callback,
    //    IntPtr contents_on_release_user_data)
    //{
    //    using var memory = new HGlobalSafeHandle(Marshal.AllocHGlobal(pixelData.Length), true).AssertValid();
    //    Marshal.Copy(pixelData, 0, memory.DangerousGetHandle(), pixelData.Length);
    //    // NOTE: unlike with ImpellerTypographyContextRegisterFont, it seems the on_release callback here _is_ actually
    //    // called, and immediately during/after the CreateWithContentsNew call
    //    return new ImpellerTexture(
    //        ImpellerTextureCreateWithContentsNew(
    //            context.Handle,
    //            descriptor,
    //            new ImpellerMapping
    //            {
    //                data = memory.DangerousGetHandle(),
    //                length = (ulong)pixelData.Length,
    //                on_release = x => callback(x)
    //            },
    //            contents_on_release_user_data).AssertValid());
    //}

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
