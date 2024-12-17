namespace Baksteen.Graphics.Impeller;

using System;
using static Baksteen.Graphics.Impeller.ImpellerNative;
using System.Runtime.InteropServices;

public class ImpellerImageFilter : IDisposable
{
    private readonly ImpellerImageFilterSafeHandle _handle;
    private bool disposedValue;

    public ImpellerImageFilterSafeHandle Handle => _handle;

    internal ImpellerImageFilter(ImpellerImageFilterSafeHandle handle)
    {
        _handle = handle;
    }

    //------------------------------------------------------------------------------
    /// @brief      Creates an image filter that applies a Gaussian blur.
    ///
    ///             The Gaussian blur applied may be an approximation for
    ///             performance.
    ///
    ///
    /// @param[in]  x_sigma    The x sigma.
    /// @param[in]  y_sigma    The y sigma.
    /// @param[in]  tile_mode  The tile mode.
    ///
    /// @return     The image filter.
    ///
    public static ImpellerImageFilter CreateBlur(float x_sigma, float y_sigma, ImpellerTileMode tile_mode)
    {
        return new ImpellerImageFilter(ImpellerNative.ImpellerImageFilterCreateBlurNew(x_sigma,y_sigma,tile_mode).AssertValid());
    }

    //------------------------------------------------------------------------------
    /// @brief      Creates an image filter that enhances the per-channel pixel
    ///             values to the maximum value in a circle around the pixel.
    ///
    /// @param[in]  x_radius  The x radius.
    /// @param[in]  y_radius  The y radius.
    ///
    /// @return     The image filter.
    ///
    public static ImpellerImageFilter CreateDilate(float x_radius, float y_radius)
    {
        return new ImpellerImageFilter(ImpellerNative.ImpellerImageFilterCreateDilateNew(x_radius, y_radius).AssertValid());
    }

    //------------------------------------------------------------------------------
    /// @brief      Creates an image filter that dampens the per-channel pixel
    ///             values to the minimum value in a circle around the pixel.
    ///
    /// @param[in]  x_radius  The x radius.
    /// @param[in]  y_radius  The y radius.
    ///
    /// @return     The image filter.
    ///
    public static ImpellerImageFilter CreateErode(float x_radius, float y_radius)
    {
        return new ImpellerImageFilter(ImpellerNative.ImpellerImageFilterCreateErodeNew(x_radius, y_radius).AssertValid());
    }

    //------------------------------------------------------------------------------
    /// @brief      Creates an image filter that applies a transformation matrix to
    ///             the underlying image.
    ///
    /// @param[in]  matrix    The transformation matrix.
    /// @param[in]  sampling  The image sampling mode.
    ///
    /// @return     The image filter.
    ///
    public static ImpellerImageFilter CreateMatrix(in ImpellerMatrix matrix, ImpellerTextureSampling sampling)
    {
        return new ImpellerImageFilter(ImpellerNative.ImpellerImageFilterCreateMatrixNew(matrix,sampling).AssertValid());
    }

    //------------------------------------------------------------------------------
    /// @brief      Creates a composed filter that when applied is identical to
    ///             subsequently applying the inner and then the outer filters.
    ///
    ///             ```
    ///             destination = outer_filter(inner_filter(source))
    ///             ```
    ///
    /// @param[in]  outer  The outer image filter.
    /// @param[in]  inner  The inner image filter.
    ///
    /// @return     The combined image filter.
    ///
    public static ImpellerImageFilter CreateCompose(ImpellerImageFilter outer, ImpellerImageFilter inner)
    {
        return new ImpellerImageFilter(ImpellerNative.ImpellerImageFilterCreateComposeNew(outer.Handle,inner.Handle).AssertValid());
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
