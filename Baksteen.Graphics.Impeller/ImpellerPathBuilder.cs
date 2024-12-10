namespace Baksteen.Graphics.Impeller;

using System;
using static Baksteen.Graphics.Impeller.ImpellerNative;

public class ImpellerPathBuilder : IDisposable
{
    private readonly ImpellerPathBuilderSafeHandle _handle;
    private bool disposedValue;

    public ImpellerPathBuilder()
    {
        _handle = ImpellerNative.ImpellerPathBuilderNew().AssertValid();
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
    /// @brief      Move the cursor to the specified location.
    ///
    /// @param[in]  builder   The builder.
    /// @param[in]  location  The location.
    ///
    public void MoveTo(in ImpellerPoint location)
    {
        ImpellerNative.ImpellerPathBuilderMoveTo(_handle,location);
    }

    //------------------------------------------------------------------------------
    /// @brief      Add a line segment from the current cursor location to the given
    ///             location. The cursor location is updated to be at the endpoint.
    ///
    /// @param[in]  builder   The builder.
    /// @param[in]  location  The location.
    ///
    public void LineTo(in ImpellerPoint location)
    {
        ImpellerNative.ImpellerPathBuilderLineTo(_handle, location);
    }

    //------------------------------------------------------------------------------
    /// @brief      Add a quadratic curve from whose start point is the cursor to
    ///             the specified end point using the a single control point.
    ///
    ///             The new location of the cursor after this call is the end point.
    ///
    /// @param[in]  builder        The builder.
    /// @param[in]  control_point  The control point.
    /// @param[in]  end_point      The end point.
    ///
    public void QuadraticCurveTo(
        in ImpellerPoint control_point,
        in ImpellerPoint end_point)
    {
        ImpellerNative.ImpellerPathBuilderQuadraticCurveTo(_handle,control_point,end_point);
    }

    //------------------------------------------------------------------------------
    /// @brief      Add a cubic curve whose start point is current cursor location
    ///             to the specified end point using the two specified control
    ///             points.
    ///
    ///             The new location of the cursor after this call is the end point
    ///             supplied.
    ///
    /// @param[in]  builder          The builder
    /// @param[in]  control_point_1  The control point 1
    /// @param[in]  control_point_2  The control point 2
    /// @param[in]  end_point        The end point
    ///
    public void CubicCurveTo(
        in ImpellerPoint control_point_1,
        in ImpellerPoint control_point_2,
        in ImpellerPoint end_point)
    {
        ImpellerNative.ImpellerPathBuilderCubicCurveTo(_handle,control_point_1, control_point_2, end_point);
    }

    //------------------------------------------------------------------------------
    /// @brief      Adds a rectangle to the path.
    ///
    /// @param[in]  builder  The builder.
    /// @param[in]  rect     The rectangle.
    ///
    public void AddRect(
        in ImpellerRect rect)
    {
        ImpellerNative.ImpellerPathBuilderAddRect(_handle, rect);
    }

    //------------------------------------------------------------------------------
    /// @brief      Add an arc to the path.
    ///
    /// @param[in]  builder              The builder.
    /// @param[in]  oval_bounds          The oval bounds.
    /// @param[in]  start_angle_degrees  The start angle in degrees.
    /// @param[in]  end_angle_degrees    The end angle in degrees.
    ///
    public void AddArc(
        in ImpellerRect oval_bounds,
        float start_angle_degrees,
        float end_angle_degrees)
    {
        ImpellerNative.ImpellerPathBuilderAddArc(_handle,oval_bounds,start_angle_degrees,end_angle_degrees);
    }

    //------------------------------------------------------------------------------
    /// @brief      Add an oval to the path.
    ///
    /// @param[in]  builder      The builder.
    /// @param[in]  oval_bounds  The oval bounds.
    ///
    public void AddOval(
        in ImpellerRect oval_bounds)
    {
        ImpellerNative.ImpellerPathBuilderAddOval(_handle,oval_bounds);
    }

    //------------------------------------------------------------------------------
    /// @brief      Add a rounded rect with potentially non-uniform radii to the
    ///             path.
    ///
    /// @param[in]  builder         The builder.
    /// @param[in]  rect            The rectangle.
    /// @param[in]  rounding_radii  The rounding radii.
    ///
    public void AddRoundedRect(
        ImpellerPathBuilderSafeHandle builder,
        in ImpellerRect rect,
        in ImpellerRoundingRadii rounding_radii)
    {
        ImpellerNative.ImpellerPathBuilderAddRoundedRect(
            _handle,
            rect,
            rounding_radii);
    }

    //------------------------------------------------------------------------------
    /// @brief      Close the path.
    ///
    /// @param[in]  builder  The builder.
    ///
    public void Close()
    {
        ImpellerNative.ImpellerPathBuilderClose(_handle);    
    }

    //------------------------------------------------------------------------------
    /// @brief      Create a new path by copying the existing built-up path. The
    ///             existing path can continue being added to.
    ///
    /// @param[in]  builder  The builder.
    /// @param[in]  fill     The fill.
    ///
    /// @return     The impeller path.
    ///
    public ImpellerPath CopyPathNew(
        ImpellerFillType fill)
    {
        return new ImpellerPath(ImpellerNative.ImpellerPathBuilderCopyPathNew(_handle, fill).AssertValid());
    }

    //------------------------------------------------------------------------------
    /// @brief      Create a new path using the existing built-up path. The existing
    ///             path builder now contains an empty path.
    ///
    /// @param[in]  builder  The builder.
    /// @param[in]  fill     The fill.
    ///
    /// @return     The impeller path.
    ///
    public ImpellerPath TakePathNew(
        ImpellerFillType fill)
    {
        return new ImpellerPath(ImpellerNative.ImpellerPathBuilderTakePathNew(_handle, fill).AssertValid());
    }
}
