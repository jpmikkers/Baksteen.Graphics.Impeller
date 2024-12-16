using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Runtime.InteropServices;

namespace Baksteen.Graphics.Impeller;
public static class SafeHandleExtension
{
    public static T AssertValid<T>(this T sh) where T : SafeHandle
    {
        if (sh.IsInvalid)
        {
            throw new InvalidOperationException($"invalid handle '{typeof(T).Name}'");
        }
        return sh;
    }
}
