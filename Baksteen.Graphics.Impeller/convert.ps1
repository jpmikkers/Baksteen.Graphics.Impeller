$src = Get-Content -Path .\impeller_sdk\include\impeller.h -Raw

$src = "/*`n" + $src

while($true)
{
    $nxt = $src -replace '(?m)^#define (.*?)\\\n','#define $1 '
    if($nxt -ceq $src){ break; }
    $src = $nxt
}

$src = $src -replace "(?m)^#define IMPELLER_EXTERN_C_BEGIN","#define IMP_EXTERN_C_BEGIN"
$src = $src -replace "(?m)^#define IMPELLER_EXTERN_C_END.*?","#define IMP_EXTERN_C_END"
$src = $src -replace "(?m)^#","//#"
$src = $src -replace "(?m)IMPELLER_EXPORT\s?","[DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]`npublic static extern "
$src = $src -replace "(?m)^IMPELLER_DEFINE_HANDLE","//IMPELLER_DEFINE_HANDLE"
#$src = $src -replace "(?m)^","    "
$src = $src -replace "uint8_t","byte"
$src = $src -replace "uint16_t","ushort"
$src = $src -replace "uint32_t","uint"
$src = $src -replace "uint64_t","ulong"
$src = $src -replace "int8_t","sbyte"
$src = $src -replace "int16_t","short"
$src = $src -replace "int32_t","int"
$src = $src -replace "int64_t","long"
$src = $src -replace "(?ms)^[\s]+float[\s]+([a-zA-Z0-9_]+);", '    public float $1;'
$src = $src -replace "(?ms)^[\s]+long[\s]+([a-zA-Z0-9_]+);", '    public long $1;'
$src = $src -replace "(?ms)^[\s]+ImpellerPoint[\s]+([a-zA-Z0-9_]+);", '    public ImpellerPoint $1;'
$src = $src -replace "(?ms)^[\s]+ImpellerColorSpace[\s]+([a-zA-Z0-9_]+);", '    public ImpellerColorSpace $1;'
$src = $src -replace "(?ms)^[\s]+ImpellerPixelFormat[\s]+([a-zA-Z0-9_]+);", '    public ImpellerPixelFormat $1;'
$src = $src -replace "(?ms)^[\s]+ImpellerISize[\s]+([a-zA-Z0-9_]+);", '    public ImpellerISize $1;'
$src = $src -replace "(?ms)^[\s]+uint[\s]+([a-zA-Z0-9_]+);", '    public uint $1;'
$src = $src -replace "(?ms)^[\s]+ulong[\s]+([a-zA-Z0-9_]+);", '    public ulong $1;'
#$src = $src -replace "(?ms)^[\s]+ImpellerCallback[\s]+([a-zA-Z0-9_]+);", '    public ImpellerCallback $1;'
#$src = $src -replace '(?ms)^\s*byte\[\]\s+([a-zA-Z0-9_]+);', '    public byte[] $1;'

$src = $src -replace "IMPELLER_EXTERN_C_BEGIN",@"

*/
namespace Baksteen.Graphics.Impeller;

using System;
using System.Runtime.InteropServices;
using static Baksteen.Graphics.Impeller.ImpellerNative;

public static class ImpellerNative
{
    const string ImpellerDLLName = @`"impeller_sdk\lib\impeller.dll`";

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void ImpellerCallback(IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr ImpellerProcAddressCallback(string procName, IntPtr userData);

"@
$src = $src -replace "IMPELLER_EXTERN_C_END","}"
$src = $src -replace "IMPELLER_NULLABLE ",""
$src = $src -replace "IMPELLER_NULLABLE ",""
$src = $src -replace "IMPELLER_NULLABLE",""
$src = $src -replace "IMPELLER_NONNULL ",""
$src = $src -replace "IMPELLER_NONNULL",""
$src = $src -replace "IMPELLER_NODISCARD ",""
$src = $src -replace "IMPELLER_NODISCARD",""

$src = $src -replace "ImpellerDisplayListBuilder ","ImpellerDisplayListBuilderSafeHandle "

$src = $src -replace "ImpellerDisplayList ","ImpellerDisplayListSafeHandle "

$src = $src -replace "ImpellerPaint ","ImpellerPaintSafeHandle "

$src = $src -replace "ImpellerContext ","ImpellerContextSafeHandle "

$src = $src -replace "ImpellerSurface ","ImpellerSurfaceSafeHandle "

$src = $src -replace "ImpellerPath ","ImpellerPathSafeHandle "

$src = $src -replace "ImpellerPathBuilder ","ImpellerPathBuilderSafeHandle "

$src = $src -replace "ImpellerTexture ","ImpellerTextureSafeHandle "

$src = $src -replace "ImpellerColorSource ","ImpellerColorSourceSafeHandle "

$src = $src -replace "ImpellerColorFilter ","ImpellerColorFilterSafeHandle "

$src = $src -replace "ImpellerMaskFilter ","ImpellerMaskFilterSafeHandle "

$src = $src -replace "ImpellerImageFilter ","ImpellerImageFilterSafeHandle "

$src = $src -replace "ImpellerTypographyContext ","ImpellerTypographyContextSafeHandle "

$src = $src -replace "ImpellerParagraphStyle ","ImpellerParagraphStyleSafeHandle "

$src = $src -replace "ImpellerParagraphBuilder ","ImpellerParagraphBuilderSafeHandle "

$src = $src -replace "ImpellerParagraph ","ImpellerParagraphSafeHandle "

$src = $src -replace '(?m)typedef enum ([a-zA-Z0-9_]+) \{([\s\S.]*?)\}([\sa-zA-Z0-9_]+;)', 'public enum $1 {$2}'
$src = $src -replace '(?m)typedef struct ([a-zA-Z0-9_]+) \{([\s\S.]*?)\}([\sa-zA-Z0-9_]+;)', ("[StructLayout(LayoutKind.Sequential)]`n"+'    public struct $1 {$2}')

# move enum opening curly bracket to new line
$src = $src -replace '(?m)public enum ([a-zA-Z0-9_]+) \{',('public enum $1' + "`n{")

# move struct opening curly bracket to new line
$src = $src -replace '(?m)public struct ([a-zA-Z0-9_]+) \{',('public struct $1' + "`n{")

$src = $src -replace '(?m)const ImpellerISize\*\s+([a-zA-Z0-9_]+)', 'in ImpellerISize $1'
$src = $src -replace '(?m)const ImpellerRect\*\s+([a-zA-Z0-9_]+)', 'in ImpellerRect $1'
$src = $src -replace '(?m)const ImpellerPoint\*\s+([a-zA-Z0-9_]+)', 'in ImpellerPoint $1'
$src = $src -replace '(?m)const ImpellerRoundingRadii\*\s+([a-zA-Z0-9_]+)', 'in ImpellerRoundingRadii $1'
$src = $src -replace '(?m)const ImpellerTextureDescriptor\*\s+([a-zA-Z0-9_]+)', 'in ImpellerTextureDescriptor $1'
$src = $src -replace '(?m)const ImpellerMapping\*\s+([a-zA-Z0-9_]+)', 'in ImpellerMapping $1'
$src = $src -replace '(?m)const ImpellerColor\*\s+([a-zA-Z0-9_]+)', 'in ImpellerColor $1'
$src = $src -replace '(?m)const ImpellerMatrix\*\s+([a-zA-Z0-9_]+)', 'in ImpellerMatrix $1'
$src = $src -replace '(?m)const ImpellerColorMatrix\*\s+([a-zA-Z0-9_]+)', 'in ImpellerColorMatrix $1'

$src = $src -replace '(?m)ImpellerMatrix\*\s+([a-zA-Z0-9_]+)', 'out ImpellerMatrix $1'

$src = $src -replace '(?m)const char\*\s+([a-zA-Z0-9_]+)', 'string $1'

$src = $src -replace '(?m)const byte\*\s+([a-zA-Z0-9_]+)', 'byte[] $1'

$src = $src -replace '(?m)const float\*\s+([a-zA-Z0-9_]+)', 'float[] $1'

$src = $src -replace 'void\* ', 'IntPtr '

$src = $src -replace 'float m\[16\]', 'public unsafe fixed float m[16]'

$src = $src -replace 'float m\[20\]', 'public unsafe fixed float m[20]'

$src = $src -replace '(?ms)Impeller(.*?)Retain\(\s*?Impeller(.*?)SafeHandle', 'Impeller$1Retain(IntPtr'

$src = $src -replace '(?ms)Impeller(.*?)Release\(\s*?Impeller(.*?)SafeHandle', 'Impeller$1Release(IntPtr'

Set-Content -Path .\rawconverted.txt -Value $src
