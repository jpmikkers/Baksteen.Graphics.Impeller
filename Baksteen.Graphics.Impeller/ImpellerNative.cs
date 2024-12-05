namespace Baksteen.Graphics.Impeller;

using System;
using System.Runtime.InteropServices;

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
    public static extern IntPtr ImpellerSurfaceCreateWrappedFBONew(ImpellerContextSafeHandle context,
                                     UInt64 fbo,
                                     ImpellerPixelFormat format,
                                     [In] ref ImpellerISize size);

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
    public static extern IntPtr ImpellerDisplayListBuilderNew(IntPtr cullRect);  // should be [In] ref const ImpellerRect* IMPELLER_NULLABLE cull_rect);

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
    public static extern IntPtr ImpellerDisplayListBuilderCreateDisplayListNew(IntPtr builder);

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
    public static extern bool ImpellerSurfaceDrawDisplayList(IntPtr surface, IntPtr displayList);

    /// @brief      Create a new paint with default values.
    ///
    /// @return     The impeller paint.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr ImpellerPaintNew();

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
    public static extern void ImpellerPaintSetColor(IntPtr paint, in ImpellerColor color);

    /// @brief      Fills the current clip with the specified paint.
    ///
    /// @param[in]  builder  The builder.
    /// @param[in]  paint    The paint.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerDisplayListBuilderDrawPaint(IntPtr builder, IntPtr paint);

    /// @brief      Draws a rectangle.
    ///
    /// @param[in]  builder  The builder.
    /// @param[in]  rect     The rectangle.
    /// @param[in]  paint    The paint.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerDisplayListBuilderDrawRect(
      IntPtr builder,
      in ImpellerRect rect,
      IntPtr paint);

    /// @brief      Draws an oval.
    ///
    /// @param[in]  builder      The builder.
    /// @param[in]  oval_bounds  The oval bounds.
    /// @param[in]  paint        The paint.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerDisplayListBuilderDrawOval(
      IntPtr builder,
      in ImpellerRect rect,
      IntPtr paint);

    /// @brief      Draws a rounded rect.
    ///
    /// @param[in]  builder  The builder.
    /// @param[in]  rect     The rectangle.
    /// @param[in]  radii    The radii.
    /// @param[in]  paint    The paint.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerDisplayListBuilderDrawRoundedRect(
      IntPtr builder,
      in ImpellerRect rect,
      in ImpellerRoundingRadii radii,
      IntPtr paint);

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
