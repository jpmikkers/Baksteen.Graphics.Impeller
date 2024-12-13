namespace Baksteen.Graphics.Impeller;

using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Unicode;
using static Baksteen.Graphics.Impeller.ImpellerNative;

public class ImpellerParagraphBuilder : IDisposable
{
    private readonly ImpellerParagraphBuilderSafeHandle _handle;
    private bool disposedValue;

    public ImpellerParagraphBuilderSafeHandle Handle => _handle;

    internal ImpellerParagraphBuilder(ImpellerParagraphBuilderSafeHandle handle)
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


    //------------------------------------------------------------------------------
    /// @brief      Push a new paragraph style onto the paragraph style stack
    ///             managed by the paragraph builder.
    ///
    ///             Not all paragraph styles can be combined. For instance, it does
    ///             not make sense to mix text alignment for different text runs
    ///             within a paragraph. In such cases, the preference of the the
    ///             first paragraph style on the style stack will take hold.
    ///
    ///             If text is pushed onto the paragraph builder without a style
    ///             previously pushed onto the stack, a default paragraph text style
    ///             will be used. This may not always be desirable because some
    ///             style element cannot be overridden. It is recommended that a
    ///             default paragraph style always be pushed onto the stack before
    ///             the addition of any text.
    ///
    /// @param[in]  paragraph_builder  The paragraph builder.
    /// @param[in]  style              The style.
    ///
    public void PushStyle(ImpellerParagraphStyle style)
    {
        ImpellerNative.ImpellerParagraphBuilderPushStyle(
            Handle,
            style.Handle);
    }

    //------------------------------------------------------------------------------
    /// @brief      Pop a previously pushed paragraph style from the paragraph style
    ///             stack.
    ///
    /// @param[in]  paragraph_builder  The paragraph builder.
    ///
    public void PopStyle()
    {
        ImpellerNative.ImpellerParagraphBuilderPopStyle(Handle);
    }

    //------------------------------------------------------------------------------
    /// @brief      Add UTF-8 encoded text to the paragraph. The text will be styled
    ///             according to the paragraph style already on top of the paragraph
    ///             style stack.
    ///
    /// @param[in]  paragraph_builder  The paragraph builder.
    /// @param[in]  data               The data.
    /// @param[in]  length             The length.
    ///
    public void ImpellerParagraphBuilderAddText(string text)
    {
        var textBytes = Encoding.UTF8.GetBytes(text);
        ImpellerNative.ImpellerParagraphBuilderAddText(
            Handle,
            textBytes,
            (uint)textBytes.Length);
    }

    //------------------------------------------------------------------------------
    /// @brief      Layout and build a new paragraph using the specified width. The
    ///             resulting paragraph is immutable. The paragraph builder must be
    ///             discarded and a new one created to build more paragraphs.
    ///
    /// @param[in]  paragraph_builder  The paragraph builder.
    /// @param[in]  width              The paragraph width.
    ///
    /// @return     The paragraph if one can be created, NULL otherwise.
    ///
    public ImpellerParagraph BuildParagraphNew(float width)
    {
        return new ImpellerParagraph(ImpellerNative.ImpellerParagraphBuilderBuildParagraphNew(Handle, width).AssertValid());
    }
}
