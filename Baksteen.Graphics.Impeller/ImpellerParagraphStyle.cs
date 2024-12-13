namespace Baksteen.Graphics.Impeller;

using System;
using static Baksteen.Graphics.Impeller.ImpellerNative;
using System.Runtime.InteropServices;

public class ImpellerParagraphStyle : IDisposable
{
    private readonly ImpellerParagraphStyleSafeHandle _handle;
    private bool disposedValue;

    public ImpellerParagraphStyleSafeHandle Handle => _handle;

    public ImpellerParagraphStyle()
    {
        _handle = ImpellerNative.ImpellerParagraphStyleNew().AssertValid();
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

    public ImpellerPaint Foreground
    {
        set => SetForeground(value);
    }

    public ImpellerPaint Background
    {
        set => SetBackground(value);
    }

    public ImpellerFontWeight FontWeight
    {
        set => SetFontWeight(value);
    }

    public ImpellerFontStyle FontStyle
    {
        set => SetFontStyle(value);
    }

    public string FontFamily
    {
        set => SetFontFamily(value);
    }

    public float FontSize
    {
        set => SetFontSize(value);
    }

    public float Height
    {
        set => SetHeight(value);
    }

    public ImpellerTextAlignment TextAlignment
    {
        set => SetTextAlignment(value); 
    }

    public ImpellerTextDirection TextDirection
    {
        set => SetTextDirection(value);
    }

    public uint MaxLines
    {
        set => SetMaxLines(value); 
    }

    public string Locale
    {
        set => SetLocale(value);
    }

    //------------------------------------------------------------------------------
    /// @brief      Set the paint used to render the text glyph contents.
    ///
    /// @param[in]  paragraph_style  The paragraph style.
    /// @param[in]  paint            The paint.
    ///
    public void SetForeground(ImpellerPaint paint)
    {
        ImpellerNative.ImpellerParagraphStyleSetForeground(
            Handle,
            paint.Handle);
    }

    //------------------------------------------------------------------------------
    /// @brief      Set the paint used to render the background of the text glyphs.
    ///
    /// @param[in]  paragraph_style  The paragraph style.
    /// @param[in]  paint            The paint.
    ///
    public void SetBackground(ImpellerPaint paint)
    {
        ImpellerNative.ImpellerParagraphStyleSetBackground(
            Handle,
            paint.Handle);
    }

    //------------------------------------------------------------------------------
    /// @brief      Set the weight of the font to select when rendering glyphs.
    ///
    /// @param[in]  paragraph_style  The paragraph style.
    /// @param[in]  weight           The weight.
    ///
    public void SetFontWeight(ImpellerFontWeight weight)
    {
        ImpellerNative.ImpellerParagraphStyleSetFontWeight(
            Handle,
            weight);
    }

    //------------------------------------------------------------------------------
    /// @brief      Set whether the glyphs should be bolded or italicized.
    ///
    /// @param[in]  paragraph_style  The paragraph style.
    /// @param[in]  style            The style.
    ///
    public void SetFontStyle(ImpellerFontStyle style)
    {
        ImpellerNative.ImpellerParagraphStyleSetFontStyle(
            Handle,
            style);
    }

    //------------------------------------------------------------------------------
    /// @brief      Set the font family.
    ///
    /// @param[in]  paragraph_style  The paragraph style.
    /// @param[in]  family_name      The family name.
    ///
    public void SetFontFamily(string family_name)
    {
        ImpellerNative.ImpellerParagraphStyleSetFontFamily(
            Handle,
            family_name);
    }

    //------------------------------------------------------------------------------
    /// @brief      Set the font size.
    ///
    /// @param[in]  paragraph_style  The paragraph style.
    /// @param[in]  size             The size.
    ///
    public void SetFontSize(float size)
    {
        ImpellerNative.ImpellerParagraphStyleSetFontSize(
            Handle,
            size);
    }

    //------------------------------------------------------------------------------
    /// @brief      The height of the text as a multiple of text size.
    ///
    ///             When height is 0.0, the line height will be determined by the
    ///             font's metrics directly, which may differ from the font size.
    ///             Otherwise the line height of the text will be a multiple of font
    ///             size, and be exactly fontSize * height logical pixels tall.
    ///
    /// @param[in]  paragraph_style  The paragraph style.
    /// @param[in]  height           The height.
    ///
    public void SetHeight(float height)
    {
        ImpellerNative.ImpellerParagraphStyleSetHeight(
            Handle,
            height);
    }

    //------------------------------------------------------------------------------
    /// @brief      Set the alignment of text within the paragraph.
    ///
    /// @param[in]  paragraph_style  The paragraph style.
    /// @param[in]  align            The align.
    ///
    public void SetTextAlignment(ImpellerTextAlignment align)
    {
        ImpellerNative.ImpellerParagraphStyleSetTextAlignment(
            Handle,
            align);    
    }

    //------------------------------------------------------------------------------
    /// @brief      Set the directionality of the text within the paragraph.
    ///
    /// @param[in]  paragraph_style  The paragraph style.
    /// @param[in]  direction        The direction.
    ///
    public void SetTextDirection(ImpellerTextDirection direction)
    {
        ImpellerNative.ImpellerParagraphStyleSetTextDirection(
            Handle,
            direction);
    }

    //------------------------------------------------------------------------------
    /// @brief      Set the maximum line count within the paragraph.
    ///
    /// @param[in]  paragraph_style  The paragraph style.
    /// @param[in]  max_lines        The maximum lines.
    ///
    public void SetMaxLines(uint max_lines)
    {
        ImpellerNative.ImpellerParagraphStyleSetMaxLines(
            Handle,
            max_lines);
    }

    //------------------------------------------------------------------------------
    /// @brief      Set the paragraph locale.
    ///
    /// @param[in]  paragraph_style  The paragraph style.
    /// @param[in]  locale           The locale.
    ///
    public void SetLocale(string locale)
    {
        ImpellerNative.ImpellerParagraphStyleSetLocale(
            Handle,
            locale);
    }
}
