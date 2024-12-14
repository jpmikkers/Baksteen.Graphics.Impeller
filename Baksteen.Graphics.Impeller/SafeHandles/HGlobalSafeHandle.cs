namespace Baksteen.Graphics.Impeller;

using System;
using System.Runtime.InteropServices;

public sealed class HGlobalSafeHandle : SafeHandle
{
    public HGlobalSafeHandle() : base(IntPtr.Zero, true) { }

    public HGlobalSafeHandle(IntPtr handle, bool ownsHandle) : base(handle, ownsHandle)
    {
    }

    public override bool IsInvalid => handle == IntPtr.Zero;

    protected override bool ReleaseHandle()
    {
        Marshal.FreeHGlobal(handle);
        return true;
    }
}
