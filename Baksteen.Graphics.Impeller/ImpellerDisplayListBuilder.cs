﻿namespace Baksteen.Graphics.Impeller;

using System;
using static Baksteen.Graphics.Impeller.ImpellerNative;
using System.Runtime.InteropServices;

public class ImpellerDisplayListBuilder : IDisposable
{
    private readonly ImpellerDisplayListBuilderSafeHandle _handle;
    private bool disposedValue;

    public ImpellerDisplayListBuilderSafeHandle Handle => _handle;

    public ImpellerDisplayListBuilder()
    {
        _handle = ImpellerNative.ImpellerDisplayListBuilderNew(
            new ImpellerRect { x = 0, y = 0, width = 800, height = 600 }                // TODO FIX ME
            ).AssertValid();  // todo cullrect
    }

    public void DrawPaint(ImpellerPaint paint)
    {
        ImpellerNative.ImpellerDisplayListBuilderDrawPaint(
            Handle,
            paint.Handle);
    }

    public void DrawRect(ImpellerNative.ImpellerRect rect,ImpellerPaint paint)
    {
        ImpellerNative.ImpellerDisplayListBuilderDrawRect(
          Handle,
          rect,
          paint.Handle);
    }

    public void DrawRoundedRect(ImpellerNative.ImpellerRect rect, ImpellerNative.ImpellerRoundingRadii radii, ImpellerPaint paint)
    {
        ImpellerNative.ImpellerDisplayListBuilderDrawRoundedRect(
            Handle,
            rect,
            radii,
            paint.Handle
        );
    }

    public void DrawOval(ImpellerNative.ImpellerRect rect, ImpellerPaint paint)
    {
        ImpellerNative.ImpellerDisplayListBuilderDrawOval(
          Handle,
          rect,
          paint.Handle);
    }

    public void DrawLine(ImpellerNative.ImpellerPoint from, ImpellerNative.ImpellerPoint to, ImpellerPaint paint)
    {
        ImpellerNative.ImpellerDisplayListBuilderDrawLine(
            Handle,
            from,
            to,
            paint.Handle);
    }

    public ImpellerDisplayList CreateDisplayList()
    {
        return new ImpellerDisplayList(ImpellerNative.ImpellerDisplayListBuilderCreateDisplayListNew(Handle).AssertValid());
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

    public void Save()
    {
        ImpellerNative.ImpellerDisplayListBuilderSave(Handle);
    }

    // TODO
    //------------------------------------------------------------------------------
    /// @brief      Stashes the current transformation and clip state onto a save
    ///             stack and creates and creates an offscreen layer onto which
    ///             subsequent rendering intent will be directed to.
    ///
    ///             On the balancing call to restore, the supplied paints filters
    ///             and blend modes will be used to composite the offscreen contents
    ///             back onto the display display list.
    ///
    /// @param[in]  builder   The builder.
    /// @param[in]  bounds    The bounds.
    /// @param[in]  paint     The paint.
    /// @param[in]  backdrop  The backdrop.
    ///
    //[DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    //public static extern void ImpellerDisplayListBuilderSaveLayer(
    //ImpellerDisplayListBuilderSafeHandle builder,
    //in ImpellerRect bounds,
    //ImpellerPaintSafeHandle paint,
    //ImpellerImageFilter IMPELLER_NULLABLE backdrop);

    //------------------------------------------------------------------------------
    /// @brief      Pops the last entry pushed onto the save stack using a call to
    ///             `ImpellerDisplayListBuilderSave` or
    ///             `ImpellerDisplayListBuilderSaveLayer`.
    ///
    /// @param[in]  builder  The builder.
    ///
    public void Restore()
    {
        ImpellerNative.ImpellerDisplayListBuilderRestore(Handle);
    }

    //------------------------------------------------------------------------------
    /// @brief      Apply a scale to the transformation matrix currently on top of
    ///             the save stack.
    ///
    /// @param[in]  builder  The builder.
    /// @param[in]  x_scale  The x scale.
    /// @param[in]  y_scale  The y scale.
    ///
    public void Scale(float x_scale, float y_scale)
    {
        ImpellerNative.ImpellerDisplayListBuilderScale(
            Handle,
            x_scale,
            y_scale);
    }

    //------------------------------------------------------------------------------
    /// @brief      Apply a clockwise rotation to the transformation matrix
    ///             currently on top of the save stack.
    ///
    /// @param[in]  builder        The builder.
    /// @param[in]  angle_degrees  The angle in degrees.
    ///

    public void Rotate(float angle_degrees)
    {
        ImpellerNative.ImpellerDisplayListBuilderRotate(
            Handle,
            angle_degrees);
    }

    //------------------------------------------------------------------------------
    /// @brief      Apply a translation to the transformation matrix currently on
    ///             top of the save stack.
    ///
    /// @param[in]  builder        The builder.
    /// @param[in]  x_translation  The x translation.
    /// @param[in]  y_translation  The y translation.
    ///
    public void Translate(
        float x_translation,
        float y_translation)
    {    
        ImpellerNative.ImpellerDisplayListBuilderTranslate(
            Handle,
            x_translation,
            y_translation);
    }

    //------------------------------------------------------------------------------
    /// @brief      Appends the the provided transformation to the transformation
    ///             already on the save stack.
    ///
    /// @param[in]  builder    The builder.
    /// @param[in]  transform  The transform to append.
    ///
    public void Transform(in ImpellerMatrix transform)
    {
        ImpellerNative.ImpellerDisplayListBuilderTransform(
            Handle,
            transform);
    }

    //------------------------------------------------------------------------------
    /// @brief      Clear the transformation on top of the save stack and replace it
    ///             with a new value.
    ///
    /// @param[in]  builder    The builder.
    /// @param[in]  transform  The new transform.
    ///
    public void SetTransform(
        in ImpellerMatrix transform)
    {
        ImpellerNative.ImpellerDisplayListBuilderSetTransform(
            Handle,
            transform);
    }

    //------------------------------------------------------------------------------
    /// @brief      Get the transformation currently built up on the top of the
    ///             transformation stack.
    ///
    /// @param[in]  builder        The builder.
    /// @param[out] out_transform  The transform.
    ///
    public ImpellerMatrix GetTransform()
    {
        ImpellerNative.ImpellerDisplayListBuilderGetTransform(
            Handle,
            out var result);
        return result;
    }

    //------------------------------------------------------------------------------
    /// @brief      Reset the transformation on top of the transformation stack to
    ///             identity.
    ///
    /// @param[in]  builder  The builder.
    ///
    public void ResetTransform()
    {
        ImpellerNative.ImpellerDisplayListBuilderResetTransform(Handle);
    }

    //------------------------------------------------------------------------------
    /// @brief      Get the current size of the save stack.
    ///
    /// @param[in]  builder  The builder.
    ///
    /// @return     The save stack size.
    ///
    public UInt32 GetSaveCount()
    {
        return ImpellerNative.ImpellerDisplayListBuilderGetSaveCount(Handle);
    }

    //------------------------------------------------------------------------------
    /// @brief      Effectively calls ImpellerDisplayListBuilderRestore till the
    ///             size of the save stack becomes a specified count.
    ///
    /// @param[in]  builder  The builder.
    /// @param[in]  count    The count.
    ///
    public void RestoreToCount(UInt32 count)
    {
        ImpellerNative.ImpellerDisplayListBuilderRestoreToCount(Handle, count);
    }

    //------------------------------------------------------------------------------
    /// @brief      Draws the specified shape.
    ///
    /// @param[in]  builder  The builder.
    /// @param[in]  path     The path.
    /// @param[in]  paint    The paint.
    ///
    public void DrawPath(
        ImpellerPath path,
        ImpellerPaint paint)
    {
        ImpellerNative.ImpellerDisplayListBuilderDrawPath(
            Handle,
            path.Handle,
            paint.Handle);
    }
}
