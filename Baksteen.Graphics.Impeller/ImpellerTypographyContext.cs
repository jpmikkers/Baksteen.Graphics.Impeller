namespace Baksteen.Graphics.Impeller;

using System;
using static Baksteen.Graphics.Impeller.ImpellerNative;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;

public class ImpellerTypographyContext : IDisposable
{
    private readonly ImpellerTypographyContextSafeHandle _handle;
    private bool disposedValue;

    public ImpellerTypographyContextSafeHandle Handle => _handle;

    public ImpellerTypographyContext()
    {
        _handle = ImpellerNative.ImpellerTypographyContextNew().AssertValid();
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

    private static void DummyCallback(IntPtr userdata)
    {
    }

    //------------------------------------------------------------------------------
    /// @brief      Register a custom font.
    ///
    ///             The following font formats are supported:
    ///             * OpenType font collections (.ttc extension)
    ///             * TrueType fonts: (.ttf extension)
    ///             * OpenType fonts: (.otf extension)
    ///
    /// @warning    Web Open Font Formats (.woff and .woff2 extensions) are **not**
    ///             supported.
    ///
    ///             The font data is specified as a mapping. It is possible for the
    ///             release callback of the mapping to not be called even past the
    ///             destruction of the typography context. Care must be taken to not
    ///             collect the mapping till the release callback is invoked by
    ///             Impeller.
    ///
    ///             The family alias name can be NULL. In such cases, the font
    ///             family specified in paragraph styles must match the family that
    ///             is specified in the font data.
    ///
    ///             If the family name alias is not NULL, that family name must be
    ///             used in the paragraph style to reference glyphs from this font
    ///             instead of the one encoded in the font itself.
    ///
    ///             Multiple fonts (with glyphs for different styles) can be
    ///             specified with the same family.
    ///
    /// @see        `ImpellerParagraphStyleSetFontFamily`
    ///
    /// @param[in]  context                        The context.
    /// @param[in]  contents                       The contents.
    /// @param[in]  contents_on_release_user_data  The user data baton to be passed
    ///                                            to the contents release callback.
    /// @param[in]  family_name_alias              The family name alias or NULL if
    ///                                            the one specified in the font
    ///                                            data is to be used.
    ///
    /// @return     If the font could be successfully registered.
    ///
    public void RegisterFont(
        byte[] fontData,
        string family_name_alias)
    {
        var unmanagedPointer = Marshal.AllocHGlobal(fontData.Length);
        Marshal.Copy(fontData, 0, unmanagedPointer, fontData.Length);

        try
        {
            if (!ImpellerNative.ImpellerTypographyContextRegisterFont(
                Handle,
                new ImpellerMapping
                {
                    data = unmanagedPointer,
                    length = (ulong)fontData.Length,
                    on_release = DummyCallback,     // TODO: it seems the callback is never called, impeller bug?
                },
                IntPtr.Zero,    // contents_on_release_user_data,
                family_name_alias))
            {
                throw new Exception($"could not load font with alias {family_name_alias}");
            }
        }
        finally
        {
            // TODO: only free font when context disappears. Impeller keeps referring to the passed pointer!
            //Marshal.FreeHGlobal(unmanagedPointer);
        }
    }

    public void RegisterFont(Stream fontStream, string family_name_alias)
    {
        using var ms = new MemoryStream();
        fontStream.CopyTo(ms);
        RegisterFont(ms.ToArray(), family_name_alias);
    }

    public void RegisterFont(string fontPath, string family_name_alias)
    {
        using var fontStream = File.OpenRead(fontPath);
        RegisterFont(fontStream, family_name_alias);
    }

    //------------------------------------------------------------------------------
    /// @brief      Create a new paragraph builder.
    ///
    /// @param[in]  context  The context.
    ///
    /// @return     The paragraph builder.
    ///
    public ImpellerParagraphBuilder ParagraphBuilderNew()
    {
        return new ImpellerParagraphBuilder(ImpellerNative.ImpellerParagraphBuilderNew(Handle).AssertValid());
    }
}
