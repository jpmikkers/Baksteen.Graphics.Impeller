namespace Baksteen.Graphics.Impeller;

using System;
using DotGLFW;
using static Baksteen.Graphics.Impeller.ImpellerNative;

public class ImpellerPaint : IDisposable
{
    private readonly ImpellerPaintSafeHandle _handle;
    private bool disposedValue;

    private ImpellerDrawStyle _drawStyle;
    private ImpellerStrokeCap _strokeCap;
    private ImpellerStrokeJoin _strokeJoin;
    private float _strokeWidth = 1.0f;
    private float _strokeMiter = 1.0f;

    public ImpellerPaintSafeHandle Handle => _handle;

    public ImpellerColor Color
    {
        set => ImpellerPaintSetColor(_handle, value);
    }

    public ImpellerDrawStyle DrawStyle
    {
        get => _drawStyle;
        set
        {
            _drawStyle = value;
            ImpellerPaintSetDrawStyle(_handle, _drawStyle);
        }
    }

    public ImpellerBlendMode BlendMode
    {
        set
        {
            ImpellerPaintSetBlendMode(_handle, value);
        }
    }

    public ImpellerStrokeCap StrokeCap
    {
        get => _strokeCap;
        set
        {
            _strokeCap = value;
            ImpellerPaintSetStrokeCap(_handle, _strokeCap);
        }
    }

    public ImpellerStrokeJoin StrokeJoin
    {
        get => _strokeJoin;
        set
        {
            _strokeJoin = value;
            ImpellerPaintSetStrokeJoin(_handle, _strokeJoin);
        }
    }

    public float StrokeWidth
    {
        get => _strokeWidth;
        set
        {
            _strokeWidth = value;
            ImpellerPaintSetStrokeWidth(_handle, _strokeWidth);
        }
    }

    public float StrokeMiter
    {
        get => _strokeMiter;
        set
        {
            _strokeMiter = value;
            ImpellerPaintSetStrokeMiter(_handle, _strokeMiter);
        }
    }

    public ImpellerPaint()
    {
        _handle = ImpellerNative.ImpellerPaintNew().AssertValid();
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
