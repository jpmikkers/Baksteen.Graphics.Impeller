namespace Baksteen.Graphics.Impeller;

using System;
using System.IO;
using static Baksteen.Graphics.Impeller.ImpellerNative;
using System.Runtime.InteropServices;

public class ImpellerColorSource : IDisposable
{
    private readonly ImpellerColorSourceSafeHandle _handle;
    private bool disposedValue;

    public ImpellerColorSourceSafeHandle Handle => _handle;
    
    private ImpellerColorSource(ImpellerColorSourceSafeHandle handle)
    {
        _handle = handle;
    }

    public static ImpellerColorSource CreateLinearGradient(
        in ImpellerPoint start_point,
        in ImpellerPoint end_point,
        uint stop_count,
        in ImpellerColor[] colors,
        in float[] stops,
        ImpellerTileMode tile_mode,
        in ImpellerMatrix transformation)
    {
        if (stop_count > colors.Length || stop_count > stops.Length)
        {
            throw new ArgumentException("gradient stop count to large");
        }

        // note: it seems that Impeller creates clones of the 'colors' and 'stops' arrays, so there's
        // no need to keep references to those arrays alive after the ...New() call

        return new ImpellerColorSource(
            ImpellerColorSourceCreateLinearGradientNew(
                start_point,
                end_point,
                stop_count,
                colors,
                stops,
                tile_mode,
                transformation).AssertValid());
    }

    public static ImpellerColorSource CreateRadialGradient(
        in ImpellerPoint center,
        float radius,
        uint stop_count,
        in ImpellerColor[] colors,
        in float[] stops,
        ImpellerTileMode tile_mode,
        in ImpellerMatrix transformation)
    {
        if (stop_count > colors.Length || stop_count > stops.Length)
        {
            throw new ArgumentException("gradient stop count to large");
        }

        return new ImpellerColorSource(
            ImpellerColorSourceCreateRadialGradientNew(
                center,
                radius,
                stop_count,
                colors,
                stops,
                tile_mode,
                transformation).AssertValid());
    }

    //------------------------------------------------------------------------------
    /// @brief      Create a color source that forms a conical gradient.
    ///
    /// @param[in]  start_center    The start center.
    /// @param[in]  start_radius    The start radius.
    /// @param[in]  end_center      The end center.
    /// @param[in]  end_radius      The end radius.
    /// @param[in]  stop_count      The stop count.
    /// @param[in]  colors          The colors.
    /// @param[in]  stops           The stops.
    /// @param[in]  tile_mode       The tile mode.
    /// @param[in]  transformation  The transformation.
    ///
    /// @return     The color source.
    ///
    public static ImpellerColorSource CreateConicalGradient(
        in ImpellerPoint start_center,
        float start_radius,
        in ImpellerPoint end_center,
        float end_radius,
        uint stop_count,
        in ImpellerColor[] colors,
        in float[] stops,
        ImpellerTileMode tile_mode,
        in ImpellerMatrix transformation)
    {
        if (stop_count > colors.Length || stop_count > stops.Length)
        {
            throw new ArgumentException("gradient stop count to large");
        }

        return new ImpellerColorSource(
            ImpellerColorSourceCreateConicalGradientNew(
                start_center,
                start_radius,
                end_center,
                end_radius,
                stop_count,
                colors,
                stops,
                tile_mode,
                transformation).AssertValid());
    }

    //------------------------------------------------------------------------------
    /// @brief      Create a color source that forms a sweep gradient.
    ///
    /// @param[in]  center          The center.
    /// @param[in]  start           The start.
    /// @param[in]  end             The end.
    /// @param[in]  stop_count      The stop count.
    /// @param[in]  colors          The colors.
    /// @param[in]  stops           The stops.
    /// @param[in]  tile_mode       The tile mode.
    /// @param[in]  transformation  The transformation.
    ///
    /// @return     The color source.
    ///
    public static ImpellerColorSource CreateSweepGradient(
        in ImpellerPoint center,
        float start,
        float end,
        uint stop_count,
        in ImpellerColor[] colors,
        in float[] stops,
        ImpellerTileMode tile_mode,
        in ImpellerMatrix transformation)
    {
        if (stop_count > colors.Length || stop_count > stops.Length)
        {
            throw new ArgumentException("gradient stop count to large");
        }

        return new ImpellerColorSource(
            ImpellerColorSourceCreateSweepGradientNew(
                center,
                start,
                end,
                stop_count,
                colors,
                stops,
                tile_mode,
                transformation).AssertValid());
    }

    //------------------------------------------------------------------------------
    /// @brief      Create a color source that samples from an image.
    ///
    /// @param[in]  image                 The image.
    /// @param[in]  horizontal_tile_mode  The horizontal tile mode.
    /// @param[in]  vertical_tile_mode    The vertical tile mode.
    /// @param[in]  sampling              The sampling.
    /// @param[in]  transformation        The transformation.
    ///
    /// @return     The color source.
    ///
    public static ImpellerColorSource CreateImage(
        ImpellerTextureSafeHandle image,
        ImpellerTileMode horizontal_tile_mode,
        ImpellerTileMode vertical_tile_mode,
        ImpellerTextureSampling sampling,
        in ImpellerMatrix transformation)
    {
        return new ImpellerColorSource(
            ImpellerColorSourceCreateImageNew(
            image,
            horizontal_tile_mode,
            vertical_tile_mode,
            sampling,
            transformation).AssertValid());
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
}
