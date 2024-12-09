namespace Baksteen.Graphics.Impeller;

using System;
using System.Runtime.InteropServices;
using static Baksteen.Graphics.Impeller.ImpellerNative;

public static class ImpellerNative
{
    const string ImpellerDLLName = @"impeller_sdk\lib\impeller.dll";

    //[LibraryImport(ImpellerDLLName)]
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern UInt32 ImpellerGetVersion();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr ImpellerProcAddressCallback(string procName, IntPtr userData);

    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ImpellerContextSafeHandle ImpellerContextCreateOpenGLESNew(
        uint version,
        ImpellerProcAddressCallback glProcAddressCallback,
        IntPtr glProcAddressCallbackUserData);

    /// Retain a strong reference to the object. The object can be NULL
    /// in which case this method is a no-op.
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerContextRetain(IntPtr context);

    /// Release a previously retained reference to the object. The
    /// object can be NULL in which case this method is a no-op.
    ///
    /// @param[in]  context  The context
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerContextRelease(IntPtr context);

    /// Create a new surface by wrapping an existing framebuffer object.
    /// The framebuffer must be complete as determined by
    /// `glCheckFramebufferStatus`. The framebuffer is still owned by
    /// the caller and it must be collected once the surface is
    /// collected.
    ///
    /// @param[in]  context  The context.
    /// @param[in]  fbo      The framebuffer object handle.
    /// @param[in]  format   The format of the framebuffer.
    /// @param[in]  size     The size of the framebuffer is texels.
    ///
    /// @return     The surface if once can be created, NULL otherwise.
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ImpellerSurfaceSafeHandle ImpellerSurfaceCreateWrappedFBONew(ImpellerContextSafeHandle context,
                                     UInt64 fbo,
                                     ImpellerPixelFormat format,
                                     in ImpellerISize size);

    /// Retain a strong reference to the object. The object can be NULL
    /// in which case this method is a no-op.
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerSurfaceRetain(IntPtr surface);

    /// @brief      Release a previously retained reference to the object. The
    ///             object can be NULL in which case this method is a no-op.
    ///
    /// @param[in]  surface  The surface.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerSurfaceRelease(IntPtr surface);

    //------------------------------------------------------------------------------
    /// @brief      Create a new display list builder.
    ///
    ///             An optional cull rectangle may be specified. Impeller is allowed
    ///             to treat the contents outside this rectangle as being undefined.
    ///             This may aid performance optimizations.
    ///
    /// @param[in]  cull_rect  The cull rectangle or NULL.
    ///
    /// @return     The display list builder.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ImpellerDisplayListBuilderSafeHandle ImpellerDisplayListBuilderNew(IntPtr cullRect);  // should be [In] ref const ImpellerRect* IMPELLER_NULLABLE cull_rect);

    //------------------------------------------------------------------------------
    /// @brief      Retain a strong reference to the object. The object can be NULL
    ///             in which case this method is a no-op.
    ///
    /// @param[in]  builder  The display list builder.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerDisplayListBuilderRetain(IntPtr builder);

    //------------------------------------------------------------------------------
    /// @brief      Release a previously retained reference to the object. The
    ///             object can be NULL in which case this method is a no-op.
    ///
    /// @param[in]  builder  The display list builder.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerDisplayListBuilderRelease(IntPtr builder);

    /// @brief      Create a new display list using the rendering intent already
    ///             encoded in the builder. The builder is reset after this call.
    ///
    /// @param[in]  builder  The builder.
    ///
    /// @return     The display list.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ImpellerDisplayListSafeHandle ImpellerDisplayListBuilderCreateDisplayListNew(ImpellerDisplayListBuilderSafeHandle builder);

    //------------------------------------------------------------------------------
    /// @brief      Retain a strong reference to the object. The object can be NULL
    ///             in which case this method is a no-op.
    ///
    /// @param[in]  display_list  The display list.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerDisplayListRetain(IntPtr displayList);

    /// @brief      Release a previously retained reference to the object. The
    ///             object can be NULL in which case this method is a no-op.
    ///
    /// @param[in]  display_list  The display list.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerDisplayListRelease(IntPtr displayList);

    /// @brief      Draw a display list onto the surface. The same display list can
    ///             be drawn multiple times to different surfaces.
    ///
    /// @warning    In the OpenGL backend, Impeller will not make an effort to
    ///             preserve the OpenGL state that is current in the context.
    ///             Embedders that perform additional OpenGL operations in the
    ///             context should expect the reset state after control transitions
    ///             back to them. Key state to watch out for would be the viewports,
    ///             stencil rects, test toggles, resource (texture, framebuffer,
    ///             buffer) bindings, etc...
    ///
    /// @param[in]  surface       The surface to draw the display list to.
    /// @param[in]  display_list  The display list to draw onto the surface.
    ///
    /// @return     If the display list could be drawn onto the surface.
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool ImpellerSurfaceDrawDisplayList(ImpellerSurfaceSafeHandle surface, ImpellerDisplayListSafeHandle displayList);

    /// @brief      Create a new paint with default values.
    ///
    /// @return     The impeller paint.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ImpellerPaintSafeHandle ImpellerPaintNew();

    /// @brief      Retain a strong reference to the object. The object can be NULL
    ///             in which case this method is a no-op.
    ///
    /// @param[in]  paint  The paint.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerPaintRetain(IntPtr paint);

    /// @brief      Release a previously retained reference to the object. The
    ///             object can be NULL in which case this method is a no-op.
    ///
    /// @param[in]  paint  The paint.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerPaintRelease(IntPtr paint);

    /// @brief      Set the paint color.
    ///
    /// @param[in]  paint  The paint.
    /// @param[in]  color  The color.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerPaintSetColor(
        ImpellerPaintSafeHandle paint,
        in ImpellerColor color);

    /// @brief      Set the paint draw style. The style controls if the closed
    ///             shapes are filled and/or stroked.
    ///
    /// @param[in]  paint  The paint.
    /// @param[in]  style  The style.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerPaintSetDrawStyle(ImpellerPaintSafeHandle paint,
                               ImpellerDrawStyle style);

    /// @brief      Set the paint blend mode. The blend mode controls how the new
    ///             paints contents are mixed with the values already drawn using
    ///             previous draw calls.
    ///
    /// @param[in]  paint  The paint.
    /// @param[in]  mode   The mode.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerPaintSetBlendMode(ImpellerPaintSafeHandle paint,
                               ImpellerBlendMode mode);

    /// @brief      Sets how strokes rendered using this paint are capped.
    ///
    /// @param[in]  paint  The paint.
    /// @param[in]  cap    The stroke cap style.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerPaintSetStrokeCap(ImpellerPaintSafeHandle paint,
                               ImpellerStrokeCap cap);

    /// @brief      Sets how strokes rendered using this paint are joined.
    ///
    /// @param[in]  paint  The paint.
    /// @param[in]  join   The join.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerPaintSetStrokeJoin(ImpellerPaintSafeHandle paint,
                                ImpellerStrokeJoin join);

    /// @brief      Set the width of the strokes rendered using this paint.
    ///
    /// @param[in]  paint  The paint.
    /// @param[in]  width  The width.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerPaintSetStrokeWidth(ImpellerPaintSafeHandle paint,
                                 float width);

    /// @brief      Set the miter limit of the strokes rendered using this paint.
    ///
    /// @param[in]  paint  The paint.
    /// @param[in]  miter  The miter limit.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerPaintSetStrokeMiter(ImpellerPaintSafeHandle paint,
                                 float miter);

    /// @brief      Fills the current clip with the specified paint.
    ///
    /// @param[in]  builder  The builder.
    /// @param[in]  paint    The paint.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerDisplayListBuilderDrawPaint(
        ImpellerDisplayListBuilderSafeHandle builder,
        ImpellerPaintSafeHandle paint);

    /// @brief      Draws a rectangle.
    ///
    /// @param[in]  builder  The builder.
    /// @param[in]  rect     The rectangle.
    /// @param[in]  paint    The paint.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerDisplayListBuilderDrawRect(
      ImpellerDisplayListBuilderSafeHandle builder,
      in ImpellerRect rect,
      ImpellerPaintSafeHandle paint);

    /// @brief      Draws an oval.
    ///
    /// @param[in]  builder      The builder.
    /// @param[in]  oval_bounds  The oval bounds.
    /// @param[in]  paint        The paint.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerDisplayListBuilderDrawOval(
      ImpellerDisplayListBuilderSafeHandle builder,
      in ImpellerRect rect,
      ImpellerPaintSafeHandle paint);

    /// @brief      Draws a rounded rect.
    ///
    /// @param[in]  builder  The builder.
    /// @param[in]  rect     The rectangle.
    /// @param[in]  radii    The radii.
    /// @param[in]  paint    The paint.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerDisplayListBuilderDrawRoundedRect(
      ImpellerDisplayListBuilderSafeHandle builder,
      in ImpellerRect rect,
      in ImpellerRoundingRadii radii,
      ImpellerPaintSafeHandle paint);

    /// @brief      Draws a line segment.
    ///
    /// @param[in]  builder  The builder.
    /// @param[in]  from     The starting point of the line.
    /// @param[in]  to       The end point of the line.
    /// @param[in]  paint    The paint.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerDisplayListBuilderDrawLine(
        ImpellerDisplayListBuilderSafeHandle builder,
        in ImpellerPoint from,
        in ImpellerPoint to,
        ImpellerPaintSafeHandle paint);

    //------------------------------------------------------------------------------
    /// @brief      Stashes the current transformation and clip state onto a save
    ///             stack.
    ///
    /// @param[in]  builder  The builder.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerDisplayListBuilderSave(
        ImpellerDisplayListBuilderSafeHandle builder);

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
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerDisplayListBuilderRestore(
        ImpellerDisplayListBuilderSafeHandle builder);

    //------------------------------------------------------------------------------
    /// @brief      Apply a scale to the transformation matrix currently on top of
    ///             the save stack.
    ///
    /// @param[in]  builder  The builder.
    /// @param[in]  x_scale  The x scale.
    /// @param[in]  y_scale  The y scale.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerDisplayListBuilderScale(
        ImpellerDisplayListBuilderSafeHandle builder,
        float x_scale,
        float y_scale);

    //------------------------------------------------------------------------------
    /// @brief      Apply a clockwise rotation to the transformation matrix
    ///             currently on top of the save stack.
    ///
    /// @param[in]  builder        The builder.
    /// @param[in]  angle_degrees  The angle in degrees.
    ///

    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerDisplayListBuilderRotate(
        ImpellerDisplayListBuilderSafeHandle builder,
        float angle_degrees);

    //------------------------------------------------------------------------------
    /// @brief      Apply a translation to the transformation matrix currently on
    ///             top of the save stack.
    ///
    /// @param[in]  builder        The builder.
    /// @param[in]  x_translation  The x translation.
    /// @param[in]  y_translation  The y translation.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerDisplayListBuilderTranslate(
        ImpellerDisplayListBuilderSafeHandle builder,
        float x_translation,
        float y_translation);

    //------------------------------------------------------------------------------
    /// @brief      Appends the the provided transformation to the transformation
    ///             already on the save stack.
    ///
    /// @param[in]  builder    The builder.
    /// @param[in]  transform  The transform to append.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerDisplayListBuilderTransform(
        ImpellerDisplayListBuilderSafeHandle builder,
        in ImpellerMatrix transform);

    //------------------------------------------------------------------------------
    /// @brief      Clear the transformation on top of the save stack and replace it
    ///             with a new value.
    ///
    /// @param[in]  builder    The builder.
    /// @param[in]  transform  The new transform.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerDisplayListBuilderSetTransform(
        ImpellerDisplayListBuilderSafeHandle builder,
        in ImpellerMatrix transform);

    //------------------------------------------------------------------------------
    /// @brief      Get the transformation currently built up on the top of the
    ///             transformation stack.
    ///
    /// @param[in]  builder        The builder.
    /// @param[out] out_transform  The transform.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerDisplayListBuilderGetTransform(
        ImpellerDisplayListBuilderSafeHandle builder,
        out ImpellerMatrix out_transform);

    //------------------------------------------------------------------------------
    /// @brief      Reset the transformation on top of the transformation stack to
    ///             identity.
    ///
    /// @param[in]  builder  The builder.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerDisplayListBuilderResetTransform(
        ImpellerDisplayListBuilderSafeHandle builder);

    //------------------------------------------------------------------------------
    /// @brief      Get the current size of the save stack.
    ///
    /// @param[in]  builder  The builder.
    ///
    /// @return     The save stack size.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern UInt32 ImpellerDisplayListBuilderGetSaveCount(
        ImpellerDisplayListBuilderSafeHandle builder);

    //------------------------------------------------------------------------------
    /// @brief      Effectively calls ImpellerDisplayListBuilderRestore till the
    ///             size of the save stack becomes a specified count.
    ///
    /// @param[in]  builder  The builder.
    /// @param[in]  count    The count.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerDisplayListBuilderRestoreToCount(
        ImpellerDisplayListBuilderSafeHandle builder,
        UInt32 count);

    //------------------------------------------------------------------------------
    // Enumerations
    // -----------------------------------------------------------------------------
    public enum ImpellerFillType
    {
        kImpellerFillTypeNonZero,
        kImpellerFillTypeOdd,
    }

    public enum ImpellerClipOperation
    {
        kImpellerClipOperationDifference,
        kImpellerClipOperationIntersect,
    }

    public enum ImpellerBlendMode
    {
        kImpellerBlendModeClear,
        kImpellerBlendModeSource,
        kImpellerBlendModeDestination,
        kImpellerBlendModeSourceOver,
        kImpellerBlendModeDestinationOver,
        kImpellerBlendModeSourceIn,
        kImpellerBlendModeDestinationIn,
        kImpellerBlendModeSourceOut,
        kImpellerBlendModeDestinationOut,
        kImpellerBlendModeSourceATop,
        kImpellerBlendModeDestinationATop,
        kImpellerBlendModeXor,
        kImpellerBlendModePlus,
        kImpellerBlendModeModulate,
        kImpellerBlendModeScreen,
        kImpellerBlendModeOverlay,
        kImpellerBlendModeDarken,
        kImpellerBlendModeLighten,
        kImpellerBlendModeColorDodge,
        kImpellerBlendModeColorBurn,
        kImpellerBlendModeHardLight,
        kImpellerBlendModeSoftLight,
        kImpellerBlendModeDifference,
        kImpellerBlendModeExclusion,
        kImpellerBlendModeMultiply,
        kImpellerBlendModeHue,
        kImpellerBlendModeSaturation,
        kImpellerBlendModeColor,
        kImpellerBlendModeLuminosity,
    }

    public enum ImpellerDrawStyle
    {
        kImpellerDrawStyleFill,
        kImpellerDrawStyleStroke,
        kImpellerDrawStyleStrokeAndFill,
    }

    public enum ImpellerStrokeCap
    {
        kImpellerStrokeCapButt,
        kImpellerStrokeCapRound,
        kImpellerStrokeCapSquare,
    }

    public enum ImpellerStrokeJoin
    {
        kImpellerStrokeJoinMiter,
        kImpellerStrokeJoinRound,
        kImpellerStrokeJoinBevel,
    }

    public enum ImpellerPixelFormat
    {
        kImpellerPixelFormatRGBA8888,
    }

    public enum ImpellerTextureSampling
    {
        kImpellerTextureSamplingNearestNeighbor,
        kImpellerTextureSamplingLinear,
    }

    public enum ImpellerTileMode
    {
        kImpellerTileModeClamp,
        kImpellerTileModeRepeat,
        kImpellerTileModeMirror,
        kImpellerTileModeDecal,
    }

    public enum ImpellerBlurStyle
    {
        kImpellerBlurStyleNormal,
        kImpellerBlurStyleSolid,
        kImpellerBlurStyleOuter,
        kImpellerBlurStyleInner,
    }

    public enum ImpellerColorSpace
    {
        kImpellerColorSpaceSRGB,
        kImpellerColorSpaceExtendedSRGB,
        kImpellerColorSpaceDisplayP3,
    }

    public enum ImpellerFontWeight
    {
        kImpellerFontWeight100,  // Thin
        kImpellerFontWeight200,  // Extra-Light
        kImpellerFontWeight300,  // Light
        kImpellerFontWeight400,  // Normal/Regular
        kImpellerFontWeight500,  // Medium
        kImpellerFontWeight600,  // Semi-bold
        kImpellerFontWeight700,  // Bold
        kImpellerFontWeight800,  // Extra-Bold
        kImpellerFontWeight900,  // Black
    }

    public enum ImpellerFontStyle
    {
        kImpellerFontStyleNormal,
        kImpellerFontStyleItalic,
    }

    public enum ImpellerTextAlignment
    {
        kImpellerTextAlignmentLeft,
        kImpellerTextAlignmentRight,
        kImpellerTextAlignmentCenter,
        kImpellerTextAlignmentJustify,
        kImpellerTextAlignmentStart,
        kImpellerTextAlignmentEnd,
    }

    public enum ImpellerTextDirection
    {
        kImpellerTextDirectionRTL,
        kImpellerTextDirectionLTR,
    }

    //------------------------------------------------------------------------------
    /// A 4x4 transformation matrix using column-major storage.
    ///
    /// ```
    /// | m[0] m[4] m[8]  m[12] |
    /// | m[1] m[5] m[9]  m[13] |
    /// | m[2] m[6] m[10] m[14] |
    /// | m[3] m[7] m[11] m[15] |
    /// ```
    ///
    [StructLayout(LayoutKind.Sequential)]
    public struct ImpellerMatrix
    {
        public unsafe fixed float m[10];
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ImpellerISize
    {
        public Int64 Width;
        public Int64 Height;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ImpellerColor
    {
        public float red;
        public float green;
        public float blue;
        public float alpha;
        public ImpellerColorSpace color_space;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ImpellerRect
    {
        public float x;
        public float y;
        public float width;
        public float height;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ImpellerPoint
    {
        public float x;
        public float y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ImpellerRoundingRadii
    {
        public ImpellerPoint top_left;
        public ImpellerPoint bottom_left;
        public ImpellerPoint top_right;
        public ImpellerPoint bottom_right;
    }
}
