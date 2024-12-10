/*
// Copyright 2013 The Flutter Authors. All rights reserved.
// Use of this source code is governed by a BSD-style license that can be
// found in the LICENSE file.

//#ifndef FLUTTER_IMPELLER_TOOLKIT_INTEROP_IMPELLER_H_
//#define FLUTTER_IMPELLER_TOOLKIT_INTEROP_IMPELLER_H_

//#include <stdbool.h>
//#include <stddef.h>
//#include <stdint.h>

///-----------------------------------------------------------------------------
///-----------------------------------------------------------------------------
/// -------  ___                      _ _                _    ____ ___  --------
/// ------- |_ _|_ __ ___  _ __   ___| | | ___ _ __     / \  |  _ \_ _| --------
/// -------  | || '_ ` _ \| '_ \ / _ \ | |/ _ \ '__|   / _ \ | |_) | |  --------
/// -------  | || | | | | | |_) |  __/ | |  __/ |     / ___ \|  __/| |  --------
/// ------- |___|_| |_| |_| .__/ \___|_|_|\___|_|    /_/   \_\_|  |___| --------
/// -------               |_|                                           --------
///-----------------------------------------------------------------------------
///-----------------------------------------------------------------------------
///
/// This file describes a high-level, single-header, dependency-free, 2D
/// graphics API.
///
/// The API fundamentals that include details about the object model, reference
/// counting, and null-safety are described in the README.
///
//#if defined(__cplusplus)
//#define IMPELLER_EXTERN_C extern "C"
//#define IMP_EXTERN_C_BEGIN IMPELLER_EXTERN_C {
//#define IMP_EXTERN_C_END }
//#else  // defined(__cplusplus)
//#define IMPELLER_EXTERN_C
//#define IMP_EXTERN_C_BEGIN
//#define IMP_EXTERN_C_END
//#endif  // defined(__cplusplus)

//#ifdef _WIN32
//#define [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
public static extern _DECORATION __declspec(dllexport)
//#else
//#define [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
public static extern _DECORATION __attribute__((visibility("default")))
//#endif

//#ifndef IMPELLER_NO_EXPORT
//#define [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
public static extern [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
public static extern _DECORATION
//#else  // IMPELLER_NO_EXPORT
//#define [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
public static extern //#endif  // IMPELLER_NO_EXPORT

//#ifdef __clang__
//#define _Nullable
//#define _Nonnull
//#else  // __clang__
//#define 
//#define 
//#endif  // __clang__

//#if defined(__STDC_VERSION__) && (__STDC_VERSION__ >= 202000L)
//#define [[nodiscard]]
//#else  // defined(__STDC_VERSION__) && (__STDC_VERSION__ >= 202000L)
//#define 
//#endif  // defined(__STDC_VERSION__) && (__STDC_VERSION__ >= 202000L)


*/
namespace Baksteen.Graphics.Impeller;

using System;
using System.Runtime.InteropServices;
using static Baksteen.Graphics.Impeller.ImpellerNative;

public static class ImpellerNative
{
    const string ImpellerDLLName = @"impeller_sdk\lib\impeller.dll";

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void ImpellerCallback(IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr ImpellerProcAddressCallback(string procName, IntPtr userData);


    //------------------------------------------------------------------------------
    /// @brief      Pack a version in a uint.
    ///
    /// @param[in]  variant  The version variant.
    /// @param[in]  major    The major version.
    /// @param[in]  minor    The minor version.
    /// @param[in]  patch    The patch version.
    ///
    /// @return     The packed version number.
    ///
    //#define IMPELLER_MAKE_VERSION(variant, major, minor, patch)           ((((uint)(variant)) << 29U) | (((uint)(major)) << 22U) |     (((uint)(minor)) << 12U) | ((uint)(patch)))

    //#define IMPELLER_VERSION_VARIANT 1
    //#define IMPELLER_VERSION_MAJOR 1
    //#define IMPELLER_VERSION_MINOR 2
    //#define IMPELLER_VERSION_PATCH 0

    //------------------------------------------------------------------------------
    /// The current Impeller API version.
    ///
    /// This version must be passed to APIs that create top-level objects like
    /// graphics contexts. Construction of the context may fail if the API version
    /// expected by the caller is not supported by the library.
    ///
    /// The version currently supported by the library is returned by a call to
    /// `ImpellerGetVersion`
    ///
    /// Since there are no API stability guarantees today, passing a version that is
    /// different to the one returned by `ImpellerGetVersion` will always fail.
    ///
    /// @see `ImpellerGetVersion`
    ///
    //#define IMPELLER_VERSION                                                     IMPELLER_MAKE_VERSION(IMPELLER_VERSION_VARIANT, IMPELLER_VERSION_MAJOR,                          IMPELLER_VERSION_MINOR, IMPELLER_VERSION_PATCH)

    //------------------------------------------------------------------------------
    /// @param[in]  version  The packed version.
    ///
    /// @return     The version variant.
    ///
    //#define IMPELLER_VERSION_GET_VARIANT(version) ((uint)(version) >> 29U)

    //------------------------------------------------------------------------------
    /// @param[in]  version  The packed version.
    ///
    /// @return     The major version.
    ///
    //#define IMPELLER_VERSION_GET_MAJOR(version)    (((uint)(version) >> 22U) & 0x7FU)

    //------------------------------------------------------------------------------
    /// @param[in]  version  The packed version.
    ///
    /// @return     The minor version.
    ///
    //#define IMPELLER_VERSION_GET_MINOR(version)    (((uint)(version) >> 12U) & 0x3FFU)

    //------------------------------------------------------------------------------
    /// @param[in]  version  The packed version.
    ///
    /// @return     The patch version.
    ///
    //#define IMPELLER_VERSION_GET_PATCH(version) ((uint)(version) & 0xFFFU)

    //------------------------------------------------------------------------------
    // Handles
    //------------------------------------------------------------------------------

    //#define IMPELLER_INTERNAL_HANDLE_NAME(handle) handle##_
    //#define IMPELLER_DEFINE_HANDLE(handle)    typedef struct IMPELLER_INTERNAL_HANDLE_NAME(handle) * handle;

    //------------------------------------------------------------------------------
    /// An Impeller graphics context. Contexts are platform and client-rendering-API
    /// specific.
    ///
    /// Contexts are thread-safe objects that are expensive to create. Most
    /// applications will only ever create a single context during their lifetimes.
    /// Once setup, Impeller is ready to render frames as performantly as possible.
    ///
    /// During setup, context create the underlying graphics pipelines, allocators,
    /// worker threads, etc...
    ///
    /// The general guidance is to create as few contexts as possible (typically
    /// just one) and share them as much as possible.
    ///
    //IMPELLER_DEFINE_HANDLE(ImpellerContext);

    //------------------------------------------------------------------------------
    /// Display lists represent encoded rendering intent. These objects are
    /// immutable, reusable, thread-safe, and context-agnostic.
    ///
    /// While it is perfectly fine to create new display lists per frame, there may
    /// be opportunities for optimization when display lists are reused multiple
    /// times.
    ///
    //IMPELLER_DEFINE_HANDLE(ImpellerDisplayList);

    //------------------------------------------------------------------------------
    /// Display list builders allow for the incremental creation of display lists.
    ///
    /// Display list builders are context-agnostic.
    ///
    //IMPELLER_DEFINE_HANDLE(ImpellerDisplayListBuilder);

    //------------------------------------------------------------------------------
    /// Paints control the behavior of draw calls encoded in a display list.
    ///
    /// Like display lists, paints are context-agnostic.
    ///
    //IMPELLER_DEFINE_HANDLE(ImpellerPaint);

    //------------------------------------------------------------------------------
    /// Color filters are functions that take two colors and mix them to produce a
    /// single color. This color is then merged with the destination during
    /// blending.
    ///
    //IMPELLER_DEFINE_HANDLE(ImpellerColorFilter);

    //------------------------------------------------------------------------------
    /// Color sources are functions that generate colors for each texture element
    /// covered by a draw call. The colors for each element can be generated using a
    /// mathematical function (to produce gradients for example) or sampled from a
    /// texture.
    ///
    //IMPELLER_DEFINE_HANDLE(ImpellerColorSource);

    //------------------------------------------------------------------------------
    /// Image filters are functions that are applied regions of a texture to produce
    /// a single color. Contrast this with color filters that operate independently
    /// on a per-pixel basis. The generated color is then merged with the
    /// destination during blending.
    ///
    //IMPELLER_DEFINE_HANDLE(ImpellerImageFilter);

    //------------------------------------------------------------------------------
    /// Mask filters are functions that are applied over a shape after it has been
    /// drawn but before it has been blended into the final image.
    ///
    //IMPELLER_DEFINE_HANDLE(ImpellerMaskFilter);

    //------------------------------------------------------------------------------
    /// Typography contexts allow for the layout and rendering of text.
    ///
    /// These are typically expensive to create and applications will only ever need
    /// to create a single one of these during their lifetimes.
    ///
    /// Unlike graphics context, typograhy contexts are not thread-safe. These must
    /// be created, used, and collected on a single thread.
    ///
    //IMPELLER_DEFINE_HANDLE(ImpellerTypographyContext);

    //------------------------------------------------------------------------------
    /// An immutable, fully laid out paragraph.
    ///
    //IMPELLER_DEFINE_HANDLE(ImpellerParagraph);

    //------------------------------------------------------------------------------
    /// Paragraph builders allow for the creation of fully laid out paragraphs
    /// (which themselves are immutable).
    ///
    /// To build a paragraph, users push/pop paragraph styles onto a stack then add
    /// UTF-8 encoded text. The properties on the top of paragraph style stack when
    /// the text is added are used to layout and shape that subset of the paragraph.
    ///
    /// @see      `ImpellerParagraphStyle`
    ///
    //IMPELLER_DEFINE_HANDLE(ImpellerParagraphBuilder);

    //------------------------------------------------------------------------------
    /// Specified when building a paragraph, paragraph styles are managed in a stack
    /// with specify text properties to apply to text that is added to the paragraph
    /// builder.
    ///
    //IMPELLER_DEFINE_HANDLE(ImpellerParagraphStyle);

    //------------------------------------------------------------------------------
    /// Represents a two-dimensional path that is immutable and graphics context
    /// agnostic.
    ///
    /// Paths in Impeller consist of linear, cubic Bézier curve, and quadratic
    /// Bézier curve segments. All other shapes are approximations using these
    /// building blocks.
    ///
    /// Paths are created using path builder that allow for the configuration of the
    /// path segments, how they are filled, and/or stroked.
    ///
    //IMPELLER_DEFINE_HANDLE(ImpellerPath);

    //------------------------------------------------------------------------------
    /// Path builders allow for the incremental building up of paths.
    ///
    //IMPELLER_DEFINE_HANDLE(ImpellerPathBuilder);

    //------------------------------------------------------------------------------
    /// A surface represents a render target for Impeller to direct the rendering
    /// intent specified the form of display lists to.
    ///
    /// Render targets are how Impeller API users perform Window System Integration
    /// (WSI). Users wrap swapchain images as surfaces and draw display lists onto
    /// these surfaces to present content.
    ///
    /// Creating surfaces is typically platform and client-rendering-API specific.
    ///
    //IMPELLER_DEFINE_HANDLE(ImpellerSurface);

    //------------------------------------------------------------------------------
    /// A reference to a texture whose data is resident on the GPU. These can be
    /// referenced in draw calls and paints.
    ///
    /// Creating textures is extremely expensive. Creating a single one can
    /// typically comfortably blow the frame budget of an application. Textures
    /// should be created on background threads.
    ///
    /// @warning    While textures themselves are thread safe, some context types
    ///             (like OpenGL) may need extra configuration to be able to operate
    ///             from multiple threads.
    ///
    //IMPELLER_DEFINE_HANDLE(ImpellerTexture);

    //------------------------------------------------------------------------------
    // Signatures
    //------------------------------------------------------------------------------

    //------------------------------------------------------------------------------
    /// A callback invoked by Impeller that passes a user supplied baton back to the
    /// user. Impeller does not interpret the baton in any way. The way the baton is
    /// specified and the thread on which the callback is invoked depends on how the
    /// user supplies the callback to Impeller.
    ///
    //typedef void (*ImpellerCallback)(IntPtr user_data);


    //------------------------------------------------------------------------------
    /// A callback used by Impeller to allow the user to resolve function pointers.
    /// A user supplied baton that is uninterpreted by Impeller is passed back to
    /// the user in the callback. How the baton is specified to Impeller and the
    /// thread on which the callback is invoked depends on how the callback is
    /// specified to Impeller.
    ///
    //typedef IntPtr (*ImpellerProcAddressCallback)(
    //    string proc_name,
    //    IntPtr user_data);

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
    // Non-opaque structs
    // -----------------------------------------------------------------------------
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
    public struct ImpellerSize
    {
        public float width;
        public float height;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ImpellerISize
    {
        public long width;
        public long height;
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
        public unsafe fixed float m[16];
    }

    //------------------------------------------------------------------------------
    /// A 4x5 matrix using row-major storage used for transforming color values.
    ///
    /// To transform color values, a 5x5 matrix is constructed with the 5th row
    /// being identity. Then the following transformation is performed:
    ///
    /// ```
    /// | R' |   | m[0]  m[1]  m[2]  m[3]  m[4]  |   | R |
    /// | G' |   | m[5]  m[6]  m[7]  m[8]  m[9]  |   | G |
    /// | B' | = | m[10] m[11] m[12] m[13] m[14] | * | B |
    /// | A' |   | m[15] m[16] m[17] m[18] m[19] |   | A |
    /// | 1  |   | 0     0     0     0     1     |   | 1 |
    /// ```
    ///
    /// The translation column (m[4], m[9], m[14], m[19]) must be specified in
    /// non-normalized 8-bit unsigned integer space (0 to 255). Values outside this
    /// range will produce undefined results.
    ///
    /// The identity transformation is thus:
    ///
    /// ```
    /// 1, 0, 0, 0, 0,
    /// 0, 1, 0, 0, 0,
    /// 0, 0, 1, 0, 0,
    /// 0, 0, 0, 1, 0,
    /// ```
    ///
    /// Some examples:
    ///
    /// To invert all colors:
    ///
    /// ```
    /// -1,  0,  0, 0, 255,
    ///  0, -1,  0, 0, 255,
    ///  0,  0, -1, 0, 255,
    ///  0,  0,  0, 1,   0,
    /// ```
    ///
    /// To apply a sepia filter:
    ///
    /// ```
    /// 0.393, 0.769, 0.189, 0, 0,
    /// 0.349, 0.686, 0.168, 0, 0,
    /// 0.272, 0.534, 0.131, 0, 0,
    /// 0,     0,     0,     1, 0,
    /// ```
    ///
    /// To apply a grayscale conversion filter:
    ///
    /// ```
    ///  0.2126, 0.7152, 0.0722, 0, 0,
    ///  0.2126, 0.7152, 0.0722, 0, 0,
    ///  0.2126, 0.7152, 0.0722, 0, 0,
    ///  0,      0,      0,      1, 0,
    /// ```
    ///
    /// @see      ImpellerColorFilter
    ///
    [StructLayout(LayoutKind.Sequential)]
    public struct ImpellerColorMatrix
    {
        public unsafe fixed float m[20];
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ImpellerRoundingRadii
    {
        public ImpellerPoint top_left;
        public ImpellerPoint bottom_left;
        public ImpellerPoint top_right;
        public ImpellerPoint bottom_right;
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
    public struct ImpellerTextureDescriptor
    {
        public ImpellerPixelFormat pixel_format;
        public ImpellerISize size;
        public uint mip_count;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ImpellerMapping
    {
        public byte[] data;
        public ulong length;
        ImpellerCallback on_release;
    }

    //------------------------------------------------------------------------------
    // Version
    //------------------------------------------------------------------------------

    //------------------------------------------------------------------------------
    /// @brief      Get the version of Impeller standalone API. This is the API that
    ///             will be accepted for validity checks when provided to the
    ///             context creation methods.
    ///
    ///             The current version of the API  is denoted by the
    ///             `IMPELLER_VERSION` macro. This version must be passed to APIs
    ///             that create top-level objects like graphics contexts.
    ///             Construction of the context may fail if the API version expected
    ///             by the caller is not supported by the library.
    ///
    ///             Since there are no API stability guarantees today, passing a
    ///             version that is different to the one returned by
    ///             `ImpellerGetVersion` will always fail.
    ///
    /// @see        `ImpellerContextCreateOpenGLESNew`
    ///
    /// @return     The version of the standalone API.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern uint ImpellerGetVersion();

    //------------------------------------------------------------------------------
    // Context
    //------------------------------------------------------------------------------

    //------------------------------------------------------------------------------
    /// @brief      Create an OpenGL(ES) Impeller context.
    ///
    /// @warning    Unlike other context types, the OpenGL ES context can only be
    ///             created, used, and collected on the calling thread. This
    ///             restriction may be lifted in the future once reactor workers are
    ///             exposed in the API. No other context types have threading
    ///             restrictions. Till reactor workers can be used, using the
    ///             context on a background thread will cause a stall of OpenGL
    ///             operations.
    ///
    /// @param[in]  version      The version of the Impeller
    ///                          standalone API. See `ImpellerGetVersion`. If the
    ///                          specified here is not compatible with the version
    ///                          of the library, context creation will fail and NULL
    ///                          context returned from this call.
    /// @param[in]  gl_proc_address_callback
    ///                          The gl proc address callback. For instance,
    ///                          `eglGetProcAddress`.
    /// @param[in]  gl_proc_address_callback_user_data
    ///                          The gl proc address callback user data baton. This
    ///                          pointer is not interpreted by Impeller and will be
    ///                          returned as user data in the proc address callback.
    ///                          user data.
    ///
    /// @return     The context or NULL if one cannot be created.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ImpellerContextSafeHandle
ImpellerContextCreateOpenGLESNew(
    uint version,
    ImpellerProcAddressCallback gl_proc_address_callback,
    IntPtr gl_proc_address_callback_user_data);

    //------------------------------------------------------------------------------
    /// @brief      Retain a strong reference to the object. The object can be NULL
    /// in which case this method is a no-op.
    ///
    /// @param[in]  context  The context.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerContextRetain(IntPtr context);

    //------------------------------------------------------------------------------
    /// @brief      Release a previously retained reference to the object. The
    /// object can be NULL in which case this method is a no-op.
    ///
    /// @param[in]  context  The context.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerContextRelease(IntPtr context);

    //------------------------------------------------------------------------------
    // Surface
    //------------------------------------------------------------------------------

    //------------------------------------------------------------------------------
    /// @brief      Create a new surface by wrapping an existing framebuffer object.
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
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ImpellerSurfaceSafeHandle
ImpellerSurfaceCreateWrappedFBONew(ImpellerContextSafeHandle context,
                                   ulong fbo,
                                     ImpellerPixelFormat format,
                                     in ImpellerISize size);

    //------------------------------------------------------------------------------
    /// @brief      Retain a strong reference to the object. The object can be NULL
    /// in which case this method is a no-op.
    ///
    /// @param[in]  surface  The surface.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerSurfaceRetain(IntPtr surface);

    //------------------------------------------------------------------------------
    /// @brief      Release a previously retained reference to the object. The
    ///             object can be NULL in which case this method is a no-op.
    ///
    /// @param[in]  surface  The surface.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerSurfaceRelease(IntPtr surface);

    //------------------------------------------------------------------------------
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
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool ImpellerSurfaceDrawDisplayList(
    ImpellerSurfaceSafeHandle surface,
    ImpellerDisplayListSafeHandle display_list);

    //------------------------------------------------------------------------------
    // Path
    //------------------------------------------------------------------------------

    //------------------------------------------------------------------------------
    /// @brief      Retain a strong reference to the object. The object can be NULL
    ///             in which case this method is a no-op.
    ///
    /// @param[in]  path  The path.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerPathRetain(IntPtr path);

    //------------------------------------------------------------------------------
    /// @brief      Release a previously retained reference to the object. The
    ///             object can be NULL in which case this method is a no-op.
    ///
    /// @param[in]  path  The path.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerPathRelease(IntPtr path);
    //------------------------------------------------------------------------------
    // Path Builder
    //------------------------------------------------------------------------------

    //------------------------------------------------------------------------------
    /// @brief      Create a new path builder. Paths themselves are immutable.
    ///             A builder builds these immutable paths.
    ///
    /// @return     The path builder.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ImpellerPathBuilderSafeHandle
    ImpellerPathBuilderNew();

    //------------------------------------------------------------------------------
    /// @brief      Retain a strong reference to the object. The object can be NULL
    ///             in which case this method is a no-op.
    ///
    /// @param[in]  builder  The builder.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerPathBuilderRetain(IntPtr builder);

    //------------------------------------------------------------------------------
    /// @brief      Release a previously retained reference to the object. The
    ///             object can be NULL in which case this method is a no-op.
    ///
    /// @param[in]  builder  The builder.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerPathBuilderRelease(IntPtr builder);
    //------------------------------------------------------------------------------
    /// @brief      Move the cursor to the specified location.
    ///
    /// @param[in]  builder   The builder.
    /// @param[in]  location  The location.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerPathBuilderMoveTo(ImpellerPathBuilderSafeHandle builder,
                                   in ImpellerPoint location);

    //------------------------------------------------------------------------------
    /// @brief      Add a line segment from the current cursor location to the given
    ///             location. The cursor location is updated to be at the endpoint.
    ///
    /// @param[in]  builder   The builder.
    /// @param[in]  location  The location.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerPathBuilderLineTo(ImpellerPathBuilderSafeHandle builder,
                                   in ImpellerPoint location);

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
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerPathBuilderQuadraticCurveTo(
        ImpellerPathBuilderSafeHandle builder,
        in ImpellerPoint control_point,
        in ImpellerPoint end_point);

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
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerPathBuilderCubicCurveTo(
        ImpellerPathBuilderSafeHandle builder,
        in ImpellerPoint control_point_1,
        in ImpellerPoint control_point_2,
        in ImpellerPoint end_point);

    //------------------------------------------------------------------------------
    /// @brief      Adds a rectangle to the path.
    ///
    /// @param[in]  builder  The builder.
    /// @param[in]  rect     The rectangle.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerPathBuilderAddRect(ImpellerPathBuilderSafeHandle builder,
                                    in ImpellerRect rect);

    //------------------------------------------------------------------------------
    /// @brief      Add an arc to the path.
    ///
    /// @param[in]  builder              The builder.
    /// @param[in]  oval_bounds          The oval bounds.
    /// @param[in]  start_angle_degrees  The start angle in degrees.
    /// @param[in]  end_angle_degrees    The end angle in degrees.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerPathBuilderAddArc(ImpellerPathBuilderSafeHandle builder,
                                   in ImpellerRect oval_bounds,
                                   float start_angle_degrees,
                                   float end_angle_degrees);

    //------------------------------------------------------------------------------
    /// @brief      Add an oval to the path.
    ///
    /// @param[in]  builder      The builder.
    /// @param[in]  oval_bounds  The oval bounds.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerPathBuilderAddOval(
        ImpellerPathBuilderSafeHandle builder,
        in ImpellerRect oval_bounds);

    //------------------------------------------------------------------------------
    /// @brief      Add a rounded rect with potentially non-uniform radii to the
    ///             path.
    ///
    /// @param[in]  builder         The builder.
    /// @param[in]  rect            The rectangle.
    /// @param[in]  rounding_radii  The rounding radii.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerPathBuilderAddRoundedRect(
        ImpellerPathBuilderSafeHandle builder,
        in ImpellerRect rect,
        in ImpellerRoundingRadii rounding_radii);

    //------------------------------------------------------------------------------
    /// @brief      Close the path.
    ///
    /// @param[in]  builder  The builder.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerPathBuilderClose(ImpellerPathBuilderSafeHandle builder);

    //------------------------------------------------------------------------------
    /// @brief      Create a new path by copying the existing built-up path. The
    ///             existing path can continue being added to.
    ///
    /// @param[in]  builder  The builder.
    /// @param[in]  fill     The fill.
    ///
    /// @return     The impeller path.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ImpellerPathSafeHandle
    ImpellerPathBuilderCopyPathNew(ImpellerPathBuilderSafeHandle builder,
                                   ImpellerFillType fill);

    //------------------------------------------------------------------------------
    /// @brief      Create a new path using the existing built-up path. The existing
    ///             path builder now contains an empty path.
    ///
    /// @param[in]  builder  The builder.
    /// @param[in]  fill     The fill.
    ///
    /// @return     The impeller path.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ImpellerPathSafeHandle
    ImpellerPathBuilderTakePathNew(ImpellerPathBuilderSafeHandle builder,
                                   ImpellerFillType fill);

    //------------------------------------------------------------------------------
    // Paint
    //------------------------------------------------------------------------------

    //------------------------------------------------------------------------------
    /// @brief      Create a new paint with default values.
    ///
    /// @return     The impeller paint.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ImpellerPaintSafeHandle
ImpellerPaintNew();

    //------------------------------------------------------------------------------
    /// @brief      Retain a strong reference to the object. The object can be NULL
    ///             in which case this method is a no-op.
    ///
    /// @param[in]  paint  The paint.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerPaintRetain(IntPtr paint);

    //------------------------------------------------------------------------------
    /// @brief      Release a previously retained reference to the object. The
    ///             object can be NULL in which case this method is a no-op.
    ///
    /// @param[in]  paint  The paint.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerPaintRelease(IntPtr paint);

    //------------------------------------------------------------------------------
    /// @brief      Set the paint color.
    ///
    /// @param[in]  paint  The paint.
    /// @param[in]  color  The color.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerPaintSetColor(ImpellerPaintSafeHandle paint,
        in ImpellerColor color);

    //------------------------------------------------------------------------------
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

    //------------------------------------------------------------------------------
    /// @brief      Set the paint draw style. The style controls if the closed
    ///             shapes are filled and/or stroked.
    ///
    /// @param[in]  paint  The paint.
    /// @param[in]  style  The style.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerPaintSetDrawStyle(ImpellerPaintSafeHandle paint,
                               ImpellerDrawStyle style);

    //------------------------------------------------------------------------------
    /// @brief      Sets how strokes rendered using this paint are capped.
    ///
    /// @param[in]  paint  The paint.
    /// @param[in]  cap    The stroke cap style.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerPaintSetStrokeCap(ImpellerPaintSafeHandle paint,
                               ImpellerStrokeCap cap);

    //------------------------------------------------------------------------------
    /// @brief      Sets how strokes rendered using this paint are joined.
    ///
    /// @param[in]  paint  The paint.
    /// @param[in]  join   The join.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerPaintSetStrokeJoin(ImpellerPaintSafeHandle paint,
                                ImpellerStrokeJoin join);

    //------------------------------------------------------------------------------
    /// @brief      Set the width of the strokes rendered using this paint.
    ///
    /// @param[in]  paint  The paint.
    /// @param[in]  width  The width.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerPaintSetStrokeWidth(ImpellerPaintSafeHandle paint,
                                 float width);

    //------------------------------------------------------------------------------
    /// @brief      Set the miter limit of the strokes rendered using this paint.
    ///
    /// @param[in]  paint  The paint.
    /// @param[in]  miter  The miter limit.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerPaintSetStrokeMiter(ImpellerPaintSafeHandle paint,
                                 float miter);

    //------------------------------------------------------------------------------
    /// @brief      Set the color filter of the paint.
    ///
    ///             Color filters are functions that take two colors and mix them to
    ///             produce a single color. This color is then usually merged with
    ///             the destination during blending.
    ///
    /// @param[in]  paint         The paint.
    /// @param[in]  color_filter  The color filter.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerPaintSetColorFilter(
        ImpellerPaintSafeHandle paint,
        ImpellerColorFilterSafeHandle color_filter);

    //------------------------------------------------------------------------------
    /// @brief      Set the color source of the paint.
    ///
    ///             Color sources are functions that generate colors for each
    ///             texture element covered by a draw call.
    ///
    /// @param[in]  paint         The paint.
    /// @param[in]  color_source  The color source.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerPaintSetColorSource(
        ImpellerPaintSafeHandle paint,
        ImpellerColorSourceSafeHandle color_source);

    //------------------------------------------------------------------------------
    /// @brief      Set the image filter of a paint.
    ///
    ///             Image filters are functions that are applied to regions of a
    ///             texture to produce a single color.
    ///
    /// @param[in]  paint         The paint.
    /// @param[in]  image_filter  The image filter.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerPaintSetImageFilter(
        ImpellerPaintSafeHandle paint,
        ImpellerImageFilterSafeHandle image_filter);

    //------------------------------------------------------------------------------
    /// @brief      Set the mask filter of a paint.
    ///
    /// @param[in]  paint        The paint.
    /// @param[in]  mask_filter  The mask filter.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerPaintSetMaskFilter(
        ImpellerPaintSafeHandle paint,
        ImpellerMaskFilterSafeHandle mask_filter);

    //------------------------------------------------------------------------------
    // Texture
    //------------------------------------------------------------------------------

    //------------------------------------------------------------------------------
    /// @brief      Create a texture with decompressed bytes.
    ///
    ///             Impeller will do its best to perform the transfer of this data
    ///             to GPU memory with a minimal number of copies. Towards this
    ///             end, it may need to send this data to a different thread for
    ///             preparation and transfer. To facilitate this transfer, it is
    ///             recommended that the content mapping have a release callback
    ///             attach to it. When there is a release callback, Impeller assumes
    ///             that collection of the data can be deferred till texture upload
    ///             is done and can happen on a background thread. When there is no
    ///             release callback, Impeller may try to perform an eager copy of
    ///             the data if it needs to perform data preparation and transfer on
    ///             a background thread.
    ///
    ///             Whether an extra data copy actually occurs will always depend on
    ///             the rendering backend in use. But it is best practice to provide
    ///             a release callback and be resilient to the data being released
    ///             in a deferred manner on a background thread.
    ///
    /// @warning    Do **not** supply compressed image data directly (PNG, JPEG,
    ///             etc...). This function only works with tightly packed
    ///             decompressed data.
    ///
    /// @param[in]  context                        The context.
    /// @param[in]  descriptor                     The texture descriptor.
    /// @param[in]  contents                       The contents.
    /// @param[in]  contents_on_release_user_data  The baton passes to the contents
    ///                                            release callback if one exists.
    ///
    /// @return     The texture if one can be created using the provided data, NULL
    ///             otherwise.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ImpellerTextureSafeHandle
    ImpellerTextureCreateWithContentsNew(
        ImpellerContextSafeHandle context,
        in ImpellerTextureDescriptor descriptor,
        in ImpellerMapping contents,
        IntPtr contents_on_release_user_data);

    //------------------------------------------------------------------------------
    /// @brief      Create a texture with an externally created OpenGL texture
    ///             handle.
    ///
    ///             Ownership of the handle is transferred over to Impeller after a
    ///             successful call to this method. Impeller is responsible for
    ///             calling glDeleteTextures on this handle. Do **not** collect this
    ///             handle yourself as this will lead to a double-free.
    ///
    ///             The handle must be created in the same context as the one used
    ///             by Impeller. If a different context is used, that context must
    ///             be in the same sharegroup as Impellers OpenGL context and all
    ///             synchronization of texture contents must already be complete.
    ///
    ///             If the context is not an OpenGL context, this call will always
    ///             fail.
    ///
    /// @param[in]  context     The context
    /// @param[in]  descriptor  The descriptor
    /// @param[in]  handle      The handle
    ///
    /// @return     The texture if one could be created by adopting the supplied
    ///             texture handle, NULL otherwise.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ImpellerTextureSafeHandle
    ImpellerTextureCreateWithOpenGLTextureHandleNew(
        ImpellerContextSafeHandle context,
        in ImpellerTextureDescriptor descriptor,
        ulong handle  // transfer-in ownership
    );

    //------------------------------------------------------------------------------
    /// @brief      Retain a strong reference to the object. The object can be NULL
    ///             in which case this method is a no-op.
    ///
    /// @param[in]  texture  The texture.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerTextureRetain(IntPtr texture);

    //------------------------------------------------------------------------------
    /// @brief      Release a previously retained reference to the object. The
    ///             object can be NULL in which case this method is a no-op.
    ///
    /// @param[in]  texture  The texture.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerTextureRelease(IntPtr texture);

    //------------------------------------------------------------------------------
    /// @brief      Get the OpenGL handle associated with this texture. If this is
    ///             not an OpenGL texture, this method will always return 0.
    ///
    ///             OpenGL handles are lazily created, this method will return
    ///             GL_NONE is no OpenGL handle is available. To ensure that this
    ///             call eagerly creates an OpenGL texture, call this on a thread
    ///             where Impeller knows there is an OpenGL context available.
    ///
    /// @param[in]  texture  The texture.
    ///
    /// @return     The OpenGL handle if one is available, GL_NONE otherwise.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ulong ImpellerTextureGetOpenGLHandle(
        ImpellerTextureSafeHandle texture);

    //------------------------------------------------------------------------------
    // Color Sources
    //------------------------------------------------------------------------------

    //------------------------------------------------------------------------------
    /// @brief      Retain a strong reference to the object. The object can be NULL
    ///             in which case this method is a no-op.
    ///
    /// @param[in]  color_source  The color source.
    ///

    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerColorSourceRetain(IntPtr color_source);

    //------------------------------------------------------------------------------
    /// @brief      Release a previously retained reference to the object. The
    ///             object can be NULL in which case this method is a no-op.
    ///
    /// @param[in]  color_source  The color source.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerColorSourceRelease(IntPtr color_source);

    //------------------------------------------------------------------------------
    /// @brief      Create a color source that forms a linear gradient.
    ///
    /// @param[in]  start_point     The start point.
    /// @param[in]  end_point       The end point.
    /// @param[in]  stop_count      The stop count.
    /// @param[in]  colors          The colors.
    /// @param[in]  stops           The stops.
    /// @param[in]  tile_mode       The tile mode.
    /// @param[in]  transformation  The transformation.
    ///
    /// @return     The color source.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ImpellerColorSourceSafeHandle
    ImpellerColorSourceCreateLinearGradientNew(
        in ImpellerPoint start_point,
        in ImpellerPoint end_point,
        uint stop_count,
        in ImpellerColor colors,
        float[] stops,
        ImpellerTileMode tile_mode,
        in ImpellerMatrix transformation);

    //------------------------------------------------------------------------------
    /// @brief      Create a color source that forms a radial gradient.
    ///
    /// @param[in]  center          The center.
    /// @param[in]  radius          The radius.
    /// @param[in]  stop_count      The stop count.
    /// @param[in]  colors          The colors.
    /// @param[in]  stops           The stops.
    /// @param[in]  tile_mode       The tile mode.
    /// @param[in]  transformation  The transformation.
    ///
    /// @return     The color source.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ImpellerColorSourceSafeHandle
    ImpellerColorSourceCreateRadialGradientNew(
        in ImpellerPoint center,
        float radius,
        uint stop_count,
        in ImpellerColor colors,
        float[] stops,
        ImpellerTileMode tile_mode,
        in ImpellerMatrix transformation);

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
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ImpellerColorSourceSafeHandle
    ImpellerColorSourceCreateConicalGradientNew(
        in ImpellerPoint start_center,
        float start_radius,
        in ImpellerPoint end_center,
        float end_radius,
        uint stop_count,
        in ImpellerColor colors,
        float[] stops,
        ImpellerTileMode tile_mode,
        in ImpellerMatrix transformation);

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
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ImpellerColorSourceSafeHandle
    ImpellerColorSourceCreateSweepGradientNew(
        in ImpellerPoint center,
        float start,
        float end,
        uint stop_count,
        in ImpellerColor colors,
        float[] stops,
        ImpellerTileMode tile_mode,
        in ImpellerMatrix transformation);

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
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ImpellerColorSourceSafeHandle
    ImpellerColorSourceCreateImageNew(
        ImpellerTextureSafeHandle image,
        ImpellerTileMode horizontal_tile_mode,
        ImpellerTileMode vertical_tile_mode,
        ImpellerTextureSampling sampling,
        in ImpellerMatrix transformation);

    //------------------------------------------------------------------------------
    // Color Filters
    //------------------------------------------------------------------------------

    //------------------------------------------------------------------------------
    /// @brief      Retain a strong reference to the object. The object can be NULL
    ///             in which case this method is a no-op.
    ///
    /// @param[in]  color_filter  The color filter.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerColorFilterRetain(IntPtr color_filter);

    //------------------------------------------------------------------------------
    /// @brief      Release a previously retained reference to the object. The
    ///             object can be NULL in which case this method is a no-op.
    ///
    /// @param[in]  color_filter  The color filter.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerColorFilterRelease(IntPtr color_filter);

    //------------------------------------------------------------------------------
    /// @brief      Create a color filter that performs blending of pixel values
    ///             independently.
    ///
    /// @param[in]  color       The color.
    /// @param[in]  blend_mode  The blend mode.
    ///
    /// @return     The color filter.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ImpellerColorFilterSafeHandle
    ImpellerColorFilterCreateBlendNew(in ImpellerColor color,
                                      ImpellerBlendMode blend_mode);

    //------------------------------------------------------------------------------
    /// @brief      Create a color filter that transforms pixel color values
    ///             independently.
    ///
    /// @param[in]  color_matrix  The color matrix.
    ///
    /// @return     The color filter.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ImpellerColorFilterSafeHandle
    ImpellerColorFilterCreateColorMatrixNew(
        in ImpellerColorMatrix color_matrix);

    //------------------------------------------------------------------------------
    // Mask Filters
    //------------------------------------------------------------------------------

    //------------------------------------------------------------------------------
    /// @brief      Retain a strong reference to the object. The object can be NULL
    ///             in which case this method is a no-op.
    ///
    /// @param[in]  mask_filter  The mask filter.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerMaskFilterRetain(IntPtr mask_filter);

    //------------------------------------------------------------------------------
    /// @brief      Release a previously retained reference to the object. The
    ///             object can be NULL in which case this method is a no-op.
    ///
    /// @param[in]  mask_filter  The mask filter.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerMaskFilterRelease(IntPtr mask_filter);

    //------------------------------------------------------------------------------
    /// @brief      Create a mask filter that blurs contents in the masked shape.
    ///
    /// @param[in]  style  The style.
    /// @param[in]  sigma  The sigma.
    ///
    /// @return     The mask filter.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ImpellerMaskFilterSafeHandle
    ImpellerMaskFilterCreateBlurNew(ImpellerBlurStyle style, float sigma);

    //------------------------------------------------------------------------------
    // Image Filters
    //------------------------------------------------------------------------------

    //------------------------------------------------------------------------------
    /// @brief      Retain a strong reference to the object. The object can be NULL
    ///             in which case this method is a no-op.
    ///
    /// @param[in]  image_filter  The image filter.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerImageFilterRetain(IntPtr image_filter);

    //------------------------------------------------------------------------------
    /// @brief      Release a previously retained reference to the object. The
    ///             object can be NULL in which case this method is a no-op.
    ///
    /// @param[in]  image_filter  The image filter.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerImageFilterRelease(IntPtr image_filter);

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
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ImpellerImageFilterSafeHandle
    ImpellerImageFilterCreateBlurNew(float x_sigma,
                                     float y_sigma,
                                     ImpellerTileMode tile_mode);

    //------------------------------------------------------------------------------
    /// @brief      Creates an image filter that enhances the per-channel pixel
    ///             values to the maximum value in a circle around the pixel.
    ///
    /// @param[in]  x_radius  The x radius.
    /// @param[in]  y_radius  The y radius.
    ///
    /// @return     The image filter.
    ///

    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ImpellerImageFilterSafeHandle
    ImpellerImageFilterCreateDilateNew(float x_radius, float y_radius);

    //------------------------------------------------------------------------------
    /// @brief      Creates an image filter that dampens the per-channel pixel
    ///             values to the minimum value in a circle around the pixel.
    ///
    /// @param[in]  x_radius  The x radius.
    /// @param[in]  y_radius  The y radius.
    ///
    /// @return     The image filter.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ImpellerImageFilterSafeHandle
    ImpellerImageFilterCreateErodeNew(float x_radius, float y_radius);

    //------------------------------------------------------------------------------
    /// @brief      Creates an image filter that applies a transformation matrix to
    ///             the underlying image.
    ///
    /// @param[in]  matrix    The transformation matrix.
    /// @param[in]  sampling  The image sampling mode.
    ///
    /// @return     The image filter.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ImpellerImageFilterSafeHandle
    ImpellerImageFilterCreateMatrixNew(
        in ImpellerMatrix matrix,
        ImpellerTextureSampling sampling);

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
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ImpellerImageFilterSafeHandle
    ImpellerImageFilterCreateComposeNew(ImpellerImageFilterSafeHandle outer,
                                        ImpellerImageFilterSafeHandle inner);

    //------------------------------------------------------------------------------
    // Display List
    //------------------------------------------------------------------------------

    //------------------------------------------------------------------------------
    /// @brief      Retain a strong reference to the object. The object can be NULL
    ///             in which case this method is a no-op.
    ///
    /// @param[in]  display_list  The display list.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerDisplayListRetain(IntPtr display_list);

    //------------------------------------------------------------------------------
    /// @brief      Release a previously retained reference to the object. The
    ///             object can be NULL in which case this method is a no-op.
    ///
    /// @param[in]  display_list  The display list.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerDisplayListRelease(IntPtr display_list);

    //------------------------------------------------------------------------------
    // Display List Builder
    //------------------------------------------------------------------------------

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
    public static extern ImpellerDisplayListBuilderSafeHandle
    ImpellerDisplayListBuilderNew(in ImpellerRect cull_rect);

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

    //------------------------------------------------------------------------------
    /// @brief      Create a new display list using the rendering intent already
    ///             encoded in the builder. The builder is reset after this call.
    ///
    /// @param[in]  builder  The builder.
    ///
    /// @return     The display list.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ImpellerDisplayListSafeHandle
    ImpellerDisplayListBuilderCreateDisplayListNew(
        ImpellerDisplayListBuilderSafeHandle builder);

    //------------------------------------------------------------------------------
    // Display List Builder: Managing the transformation stack.
    //------------------------------------------------------------------------------

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
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerDisplayListBuilderSaveLayer(
        ImpellerDisplayListBuilderSafeHandle builder,
        in ImpellerRect bounds,
        ImpellerPaintSafeHandle paint,
        ImpellerImageFilterSafeHandle backdrop);

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
    public static extern uint ImpellerDisplayListBuilderGetSaveCount(
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
    uint count);

    //------------------------------------------------------------------------------
    // Display List Builder: Clipping
    //------------------------------------------------------------------------------

    //------------------------------------------------------------------------------
    /// @brief      Reduces the clip region to the intersection of the current clip
    ///             and the given rectangle taking into account the clip operation.
    ///
    /// @param[in]  builder  The builder.
    /// @param[in]  rect     The rectangle.
    /// @param[in]  op       The operation.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerDisplayListBuilderClipRect(
        ImpellerDisplayListBuilderSafeHandle builder,
        in ImpellerRect rect,
        ImpellerClipOperation op);

    //------------------------------------------------------------------------------
    /// @brief      Reduces the clip region to the intersection of the current clip
    ///             and the given oval taking into account the clip operation.
    ///
    /// @param[in]  builder      The builder.
    /// @param[in]  oval_bounds  The oval bounds.
    /// @param[in]  op           The operation.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerDisplayListBuilderClipOval(
        ImpellerDisplayListBuilderSafeHandle builder,
        in ImpellerRect oval_bounds,
        ImpellerClipOperation op);

    //------------------------------------------------------------------------------
    /// @brief      Reduces the clip region to the intersection of the current clip
    ///             and the given rounded rectangle taking into account the clip
    ///             operation.
    ///
    /// @param[in]  builder  The builder.
    /// @param[in]  rect     The rectangle.
    /// @param[in]  radii    The radii.
    /// @param[in]  op       The operation.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerDisplayListBuilderClipRoundedRect(
        ImpellerDisplayListBuilderSafeHandle builder,
        in ImpellerRect rect,
        in ImpellerRoundingRadii radii,
        ImpellerClipOperation op);

    //------------------------------------------------------------------------------
    /// @brief      Reduces the clip region to the intersection of the current clip
    ///             and the given path taking into account the clip operation.
    ///
    /// @param[in]  builder  The builder.
    /// @param[in]  path     The path.
    /// @param[in]  op       The operation.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerDisplayListBuilderClipPath(
        ImpellerDisplayListBuilderSafeHandle builder,
        ImpellerPathSafeHandle path,
        ImpellerClipOperation op);

    //------------------------------------------------------------------------------
    // Display List Builder: Drawing Shapes
    //------------------------------------------------------------------------------

    //------------------------------------------------------------------------------
    /// @brief      Fills the current clip with the specified paint.
    ///
    /// @param[in]  builder  The builder.
    /// @param[in]  paint    The paint.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerDisplayListBuilderDrawPaint(
        ImpellerDisplayListBuilderSafeHandle builder,
        ImpellerPaintSafeHandle paint);

    //------------------------------------------------------------------------------
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
    /// @brief      Draws a dash line segment.
    ///
    /// @param[in]  builder     The builder.
    /// @param[in]  from        The starting point of the line.
    /// @param[in]  to          The end point of the line.
    /// @param[in]  on_length   On length.
    /// @param[in]  off_length  Off length.
    /// @param[in]  paint       The paint.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerDisplayListBuilderDrawDashedLine(
        ImpellerDisplayListBuilderSafeHandle builder,
        in ImpellerPoint from,
        in ImpellerPoint to,
        float on_length,
        float off_length,
        ImpellerPaintSafeHandle paint);

    //------------------------------------------------------------------------------
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

    //------------------------------------------------------------------------------
    /// @brief      Draws an oval.
    ///
    /// @param[in]  builder      The builder.
    /// @param[in]  oval_bounds  The oval bounds.
    /// @param[in]  paint        The paint.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerDisplayListBuilderDrawOval(
        ImpellerDisplayListBuilderSafeHandle builder,
        in ImpellerRect oval_bounds,
        ImpellerPaintSafeHandle paint);

    //------------------------------------------------------------------------------
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

    //------------------------------------------------------------------------------
    /// @brief      Draws a shape that is the different between the specified
    ///             rectangles (each with configurable corner radii).
    ///
    /// @param[in]  builder      The builder.
    /// @param[in]  outer_rect   The outer rectangle.
    /// @param[in]  outer_radii  The outer radii.
    /// @param[in]  inner_rect   The inner rectangle.
    /// @param[in]  inner_radii  The inner radii.
    /// @param[in]  paint        The paint.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerDisplayListBuilderDrawRoundedRectDifference(
        ImpellerDisplayListBuilderSafeHandle builder,
        in ImpellerRect outer_rect,
        in ImpellerRoundingRadii outer_radii,
        in ImpellerRect inner_rect,
        in ImpellerRoundingRadii inner_radii,
        ImpellerPaintSafeHandle paint);

    //------------------------------------------------------------------------------
    /// @brief      Draws the specified shape.
    ///
    /// @param[in]  builder  The builder.
    /// @param[in]  path     The path.
    /// @param[in]  paint    The paint.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerDisplayListBuilderDrawPath(
        ImpellerDisplayListBuilderSafeHandle builder,
        ImpellerPathSafeHandle path,
        ImpellerPaintSafeHandle paint);

    //------------------------------------------------------------------------------
    /// @brief      Flattens the contents of another display list into the one
    ///             currently being built.
    ///
    /// @param[in]  builder       The builder.
    /// @param[in]  display_list  The display list.
    /// @param[in]  opacity       The opacity.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerDisplayListBuilderDrawDisplayList(
        ImpellerDisplayListBuilderSafeHandle builder,
        ImpellerDisplayListSafeHandle display_list,
        float opacity);

    //------------------------------------------------------------------------------
    /// @brief      Draw a paragraph at the specified point.
    ///
    /// @param[in]  builder    The builder.
    /// @param[in]  paragraph  The paragraph.
    /// @param[in]  point      The point.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerDisplayListBuilderDrawParagraph(
        ImpellerDisplayListBuilderSafeHandle builder,
        ImpellerParagraphSafeHandle paragraph,
        in ImpellerPoint point);

    //------------------------------------------------------------------------------
    // Display List Builder: Drawing Textures
    //------------------------------------------------------------------------------

    //------------------------------------------------------------------------------
    /// @brief      Draw a texture at the specified point.
    ///
    /// @param[in]  builder   The builder.
    /// @param[in]  texture   The texture.
    /// @param[in]  point     The point.
    /// @param[in]  sampling  The sampling.
    /// @param[in]  paint     The paint.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerDisplayListBuilderDrawTexture(
        ImpellerDisplayListBuilderSafeHandle builder,
        ImpellerTextureSafeHandle texture,
        in ImpellerPoint point,
        ImpellerTextureSampling sampling,
        ImpellerPaintSafeHandle paint);

    //------------------------------------------------------------------------------
    /// @brief      Draw a portion of texture at the specified location.
    ///
    /// @param[in]  builder   The builder.
    /// @param[in]  texture   The texture.
    /// @param[in]  src_rect  The source rectangle.
    /// @param[in]  dst_rect  The destination rectangle.
    /// @param[in]  sampling  The sampling.
    /// @param[in]  paint     The paint.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerDisplayListBuilderDrawTextureRect(
        ImpellerDisplayListBuilderSafeHandle builder,
        ImpellerTextureSafeHandle texture,
        in ImpellerRect src_rect,
        in ImpellerRect dst_rect,
        ImpellerTextureSampling sampling,
        ImpellerPaintSafeHandle paint);

    //------------------------------------------------------------------------------
    // Typography Context
    //------------------------------------------------------------------------------

    //------------------------------------------------------------------------------
    /// @brief      Create a new typography contents.
    ///
    /// @return     The typography context.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ImpellerTypographyContextSafeHandle
    ImpellerTypographyContextNew();

    //------------------------------------------------------------------------------
    /// @brief      Retain a strong reference to the object. The object can be NULL
    ///             in which case this method is a no-op.
    ///
    /// @param[in]  context  The typography context.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerTypographyContextRetain(IntPtr context);

    //------------------------------------------------------------------------------
    /// @brief      Release a previously retained reference to the object. The
    ///             object can be NULL in which case this method is a no-op.
    ///
    /// @param[in]  context  The typography context.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerTypographyContextRelease(IntPtr context);

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
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern bool ImpellerTypographyContextRegisterFont(
        ImpellerTypographyContextSafeHandle context,
        in ImpellerMapping contents,
        IntPtr contents_on_release_user_data,
        string family_name_alias);

    //------------------------------------------------------------------------------
    // Paragraph Style
    //------------------------------------------------------------------------------

    //------------------------------------------------------------------------------
    /// @brief      Create a new paragraph style.
    ///
    /// @return     The paragraph style.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ImpellerParagraphStyleSafeHandle
    ImpellerParagraphStyleNew();

    //------------------------------------------------------------------------------
    /// @brief      Retain a strong reference to the object. The object can be NULL
    ///             in which case this method is a no-op.
    ///
    /// @param[in]  paragraph_style  The paragraph style.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerParagraphStyleRetain(IntPtr paragraph_style);

    //------------------------------------------------------------------------------
    /// @brief      Release a previously retained reference to the object. The
    ///             object can be NULL in which case this method is a no-op.
    ///
    /// @param[in]  paragraph_style  The paragraph style.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerParagraphStyleRelease(IntPtr paragraph_style);

    //------------------------------------------------------------------------------
    /// @brief      Set the paint used to render the text glyph contents.
    ///
    /// @param[in]  paragraph_style  The paragraph style.
    /// @param[in]  paint            The paint.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerParagraphStyleSetForeground(
        ImpellerParagraphStyleSafeHandle paragraph_style,
        ImpellerPaintSafeHandle paint);

    //------------------------------------------------------------------------------
    /// @brief      Set the paint used to render the background of the text glyphs.
    ///
    /// @param[in]  paragraph_style  The paragraph style.
    /// @param[in]  paint            The paint.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerParagraphStyleSetBackground(
        ImpellerParagraphStyleSafeHandle paragraph_style,
        ImpellerPaintSafeHandle paint);

    //------------------------------------------------------------------------------
    /// @brief      Set the weight of the font to select when rendering glyphs.
    ///
    /// @param[in]  paragraph_style  The paragraph style.
    /// @param[in]  weight           The weight.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerParagraphStyleSetFontWeight(
        ImpellerParagraphStyleSafeHandle paragraph_style,
        ImpellerFontWeight weight);

    //------------------------------------------------------------------------------
    /// @brief      Set whether the glyphs should be bolded or italicized.
    ///
    /// @param[in]  paragraph_style  The paragraph style.
    /// @param[in]  style            The style.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerParagraphStyleSetFontStyle(
        ImpellerParagraphStyleSafeHandle paragraph_style,
        ImpellerFontStyle style);

    //------------------------------------------------------------------------------
    /// @brief      Set the font family.
    ///
    /// @param[in]  paragraph_style  The paragraph style.
    /// @param[in]  family_name      The family name.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerParagraphStyleSetFontFamily(
        ImpellerParagraphStyleSafeHandle paragraph_style,
        string family_name);

    //------------------------------------------------------------------------------
    /// @brief      Set the font size.
    ///
    /// @param[in]  paragraph_style  The paragraph style.
    /// @param[in]  size             The size.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerParagraphStyleSetFontSize(
        ImpellerParagraphStyleSafeHandle paragraph_style,
        float size);

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
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerParagraphStyleSetHeight(
        ImpellerParagraphStyleSafeHandle paragraph_style,
        float height);

    //------------------------------------------------------------------------------
    /// @brief      Set the alignment of text within the paragraph.
    ///
    /// @param[in]  paragraph_style  The paragraph style.
    /// @param[in]  align            The align.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerParagraphStyleSetTextAlignment(
        ImpellerParagraphStyleSafeHandle paragraph_style,
        ImpellerTextAlignment align);

    //------------------------------------------------------------------------------
    /// @brief      Set the directionality of the text within the paragraph.
    ///
    /// @param[in]  paragraph_style  The paragraph style.
    /// @param[in]  direction        The direction.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerParagraphStyleSetTextDirection(
        ImpellerParagraphStyleSafeHandle paragraph_style,
        ImpellerTextDirection direction);

    //------------------------------------------------------------------------------
    /// @brief      Set the maximum line count within the paragraph.
    ///
    /// @param[in]  paragraph_style  The paragraph style.
    /// @param[in]  max_lines        The maximum lines.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerParagraphStyleSetMaxLines(
        ImpellerParagraphStyleSafeHandle paragraph_style,
        uint max_lines);

    //------------------------------------------------------------------------------
    /// @brief      Set the paragraph locale.
    ///
    /// @param[in]  paragraph_style  The paragraph style.
    /// @param[in]  locale           The locale.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerParagraphStyleSetLocale(
        ImpellerParagraphStyleSafeHandle paragraph_style,
        string locale);

    //------------------------------------------------------------------------------
    // Paragraph Builder
    //------------------------------------------------------------------------------

    //------------------------------------------------------------------------------
    /// @brief      Create a new paragraph builder.
    ///
    /// @param[in]  context  The context.
    ///
    /// @return     The paragraph builder.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ImpellerParagraphBuilderSafeHandle
    ImpellerParagraphBuilderNew(ImpellerTypographyContextSafeHandle context);

    //------------------------------------------------------------------------------
    /// @brief      Retain a strong reference to the object. The object can be NULL
    ///             in which case this method is a no-op.
    ///
    /// @param[in]  paragraph_builder  The paragraph builder.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerParagraphBuilderRetain(IntPtr paragraph_builder);

    //------------------------------------------------------------------------------
    /// @brief      Release a previously retained reference to the object. The
    ///             object can be NULL in which case this method is a no-op.
    ///
    /// @param[in]  paragraph_builder  The paragraph_builder.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerParagraphBuilderRelease(IntPtr paragraph_builder);

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
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerParagraphBuilderPushStyle(
        ImpellerParagraphBuilderSafeHandle paragraph_builder,
        ImpellerParagraphStyleSafeHandle style);

    //------------------------------------------------------------------------------
    /// @brief      Pop a previously pushed paragraph style from the paragraph style
    ///             stack.
    ///
    /// @param[in]  paragraph_builder  The paragraph builder.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerParagraphBuilderPopStyle(
        ImpellerParagraphBuilderSafeHandle paragraph_builder);

    //------------------------------------------------------------------------------
    /// @brief      Add UTF-8 encoded text to the paragraph. The text will be styled
    ///             according to the paragraph style already on top of the paragraph
    ///             style stack.
    ///
    /// @param[in]  paragraph_builder  The paragraph builder.
    /// @param[in]  data               The data.
    /// @param[in]  length             The length.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerParagraphBuilderAddText(
        ImpellerParagraphBuilderSafeHandle paragraph_builder,
        byte[] data,
        uint length);

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
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ImpellerParagraphSafeHandle
    ImpellerParagraphBuilderBuildParagraphNew(
        ImpellerParagraphBuilderSafeHandle paragraph_builder,
        float width);

    //------------------------------------------------------------------------------
    // Paragraph
    //------------------------------------------------------------------------------

    //------------------------------------------------------------------------------
    /// @brief      Retain a strong reference to the object. The object can be NULL
    ///             in which case this method is a no-op.
    ///
    /// @param[in]  paragraph  The paragraph.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerParagraphRetain(IntPtr paragraph);

    //------------------------------------------------------------------------------
    /// @brief      Release a previously retained reference to the object. The
    ///             object can be NULL in which case this method is a no-op.
    ///
    /// @param[in]  paragraph  The paragraph.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void ImpellerParagraphRelease(IntPtr paragraph);

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
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern float ImpellerParagraphGetMaxWidth(
        ImpellerParagraphSafeHandle paragraph);

    //------------------------------------------------------------------------------
    /// @param[in]  paragraph  The paragraph.
    ///
    /// @return     The height of the laid out paragraph. This is **not** a tight
    ///             bounding box and some glyphs may not reach the minimum location
    ///             they are allowed to reach.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern float ImpellerParagraphGetHeight(ImpellerParagraphSafeHandle paragraph);

    //------------------------------------------------------------------------------
    /// @param[in]  paragraph  The paragraph.
    ///
    /// @return     The length of the longest line in the paragraph. This is the
    ///             horizontal distance between the left edge of the leftmost glyph
    ///             and the right edge of the rightmost glyph, in the longest line
    ///             in the paragraph.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern float ImpellerParagraphGetLongestLineWidth(
        ImpellerParagraphSafeHandle paragraph);

    //------------------------------------------------------------------------------
    /// @see        `ImpellerParagraphGetMaxWidth`
    ///
    /// @param[in]  paragraph  The paragraph.
    ///
    /// @return     The actual width of the longest line in the paragraph after
    ///             layout. This is expected to be less than or equal to
    ///             `ImpellerParagraphGetMaxWidth`.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern float ImpellerParagraphGetMinIntrinsicWidth(
        ImpellerParagraphSafeHandle paragraph);

    //------------------------------------------------------------------------------
    /// @param[in]  paragraph  The paragraph.
    ///
    /// @return     The width of the paragraph without line breaking.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern float ImpellerParagraphGetMaxIntrinsicWidth(
        ImpellerParagraphSafeHandle paragraph);

    //------------------------------------------------------------------------------
    /// @param[in]  paragraph  The paragraph.
    ///
    /// @return     The distance from the top of the paragraph to the ideographic
    ///             baseline of the first line when using ideographic fonts
    ///             (Japanese, Korean, etc...).
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern float ImpellerParagraphGetIdeographicBaseline(
        ImpellerParagraphSafeHandle paragraph);

    //------------------------------------------------------------------------------
    /// @param[in]  paragraph  The paragraph.
    ///
    /// @return     The distance from the top of the paragraph to the alphabetic
    ///             baseline of the first line when using alphabetic fonts (A-Z,
    ///             a-z, Greek, etc...).
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern float ImpellerParagraphGetAlphabeticBaseline(
        ImpellerParagraphSafeHandle paragraph);

    //------------------------------------------------------------------------------
    /// @param[in]  paragraph  The paragraph.
    ///
    /// @return     The number of lines visible in the paragraph after line
    ///             breaking.
    ///
    [DllImport(ImpellerDLLName, CallingConvention = CallingConvention.Cdecl)]
    public static extern uint ImpellerParagraphGetLineCount(
        ImpellerParagraphSafeHandle paragraph);

}

//#endif  // FLUTTER_IMPELLER_TOOLKIT_INTEROP_IMPELLER_H_

