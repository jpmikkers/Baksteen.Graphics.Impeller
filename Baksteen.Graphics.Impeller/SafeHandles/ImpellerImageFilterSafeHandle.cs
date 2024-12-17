namespace Baksteen.Graphics.Impeller;

using System;
using System.Runtime.InteropServices;

public class ImpellerImageFilterSafeHandle : SafeHandle
{
    private readonly bool isDummy = false;
    public static ImpellerImageFilterSafeHandle NullSafeHandle = new(true);

    // The constructor should set the SafeHandle to an invalid value.
    public ImpellerImageFilterSafeHandle() : base(IntPtr.Zero, true) { }

    private ImpellerImageFilterSafeHandle(bool isDummy) : base(IntPtr.Zero, !isDummy) 
    { 
        this.isDummy = isDummy;
    }

    public override bool IsInvalid => !isDummy && handle == IntPtr.Zero;

    protected override bool ReleaseHandle()
    {
        // Call the appropriate method to decrement the reference count.
        if(!isDummy) ImpellerNative.ImpellerImageFilterRelease(handle);
        return true;
    }
}
