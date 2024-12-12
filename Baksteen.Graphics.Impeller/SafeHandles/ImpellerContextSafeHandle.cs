namespace Baksteen.Graphics.Impeller;

using System;
using System.Runtime.InteropServices;

public class ImpellerContextSafeHandle : SafeHandle
{
    // The constructor should set the SafeHandle to an invalid value.
    public ImpellerContextSafeHandle() : base(IntPtr.Zero, true) { }

    public override bool IsInvalid => handle == IntPtr.Zero;

    protected override bool ReleaseHandle()
    {
        // Call the appropriate method to decrement the reference count.
        ImpellerNative.ImpellerContextRelease(handle);
        return true;
    }
}
