namespace Baksteen.Graphics.Impeller;

using System;
using System.Runtime.InteropServices;

public class ImpellerParagraph : IDisposable
{
    private readonly ImpellerParagraphSafeHandle _handle;
    private bool disposedValue;

    public ImpellerParagraphSafeHandle Handle => _handle;
    public float MaxWidth => GetMaxWidth();
    public float Height => GetHeight();
    public float LongestLineWidth => GetLongestLineWidth();
    public float MinIntrinsicWidth => GetMinIntrinsicWidth();
    public float MaxIntrinsicWidth => GetMaxIntrinsicWidth();
    public float IdeographicBaseline => GetIdeographicBaseline();
    public float AlphabeticBaseline => GetAlphabeticBaseline();
    public uint LineCount => GetLineCount();

    internal ImpellerParagraph(ImpellerParagraphSafeHandle handle)
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
    /// @see        `ImpellerParagraphGetMinIntrinsicWidth`
    ///
    /// @param[in]  paragraph  The paragraph.
    ///
    ///
    /// @return     The width provided to the paragraph builder during the call to
    ///             layout. This is the maximum width any line in the laid out
    ///             paragraph can occupy. But, it is not necessarily the actual
    ///             width of the paragraph after layout.
    ///
    public float GetMaxWidth()
    {
        return ImpellerNative.ImpellerParagraphGetMaxWidth(Handle);
    }

    //------------------------------------------------------------------------------
    /// @param[in]  paragraph  The paragraph.
    ///
    /// @return     The height of the laid out paragraph. This is **not** a tight
    ///             bounding box and some glyphs may not reach the minimum location
    ///             they are allowed to reach.
    ///
    public float GetHeight()
    {
        return ImpellerNative.ImpellerParagraphGetHeight(Handle);
    }

    //------------------------------------------------------------------------------
    /// @param[in]  paragraph  The paragraph.
    ///
    /// @return     The length of the longest line in the paragraph. This is the
    ///             horizontal distance between the left edge of the leftmost glyph
    ///             and the right edge of the rightmost glyph, in the longest line
    ///             in the paragraph.
    ///
    public float GetLongestLineWidth()
    {
        return ImpellerNative.ImpellerParagraphGetLongestLineWidth(Handle);
    }

    //------------------------------------------------------------------------------
    /// @see        `ImpellerParagraphGetMaxWidth`
    ///
    /// @param[in]  paragraph  The paragraph.
    ///
    /// @return     The actual width of the longest line in the paragraph after
    ///             layout. This is expected to be less than or equal to
    ///             `ImpellerParagraphGetMaxWidth`.
    ///
    public float GetMinIntrinsicWidth()
    {
        return ImpellerNative.ImpellerParagraphGetMinIntrinsicWidth(Handle);
    }

    //------------------------------------------------------------------------------
    /// @param[in]  paragraph  The paragraph.
    ///
    /// @return     The width of the paragraph without line breaking.
    ///
    public float GetMaxIntrinsicWidth()
    {
        return ImpellerNative.ImpellerParagraphGetMaxIntrinsicWidth(Handle);
    }

    //------------------------------------------------------------------------------
    /// @param[in]  paragraph  The paragraph.
    ///
    /// @return     The distance from the top of the paragraph to the ideographic
    ///             baseline of the first line when using ideographic fonts
    ///             (Japanese, Korean, etc...).
    ///
    public float GetIdeographicBaseline()
    {
        return ImpellerNative.ImpellerParagraphGetIdeographicBaseline(Handle);
    }

    //------------------------------------------------------------------------------
    /// @param[in]  paragraph  The paragraph.
    ///
    /// @return     The distance from the top of the paragraph to the alphabetic
    ///             baseline of the first line when using alphabetic fonts (A-Z,
    ///             a-z, Greek, etc...).
    ///
    public float GetAlphabeticBaseline()
    {
        return ImpellerNative.ImpellerParagraphGetAlphabeticBaseline(Handle);
    }

    //------------------------------------------------------------------------------
    /// @param[in]  paragraph  The paragraph.
    ///
    /// @return     The number of lines visible in the paragraph after line
    ///             breaking.
    ///
    public uint GetLineCount()
    {
        return ImpellerNative.ImpellerParagraphGetLineCount(Handle);
    }
}
