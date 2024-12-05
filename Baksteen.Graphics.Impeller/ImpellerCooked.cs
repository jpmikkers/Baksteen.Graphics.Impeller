namespace Baksteen.Graphics.Impeller;

using System;

public static class ImpellerCooked
{
  public static Version GetVersion()
  {
    var rawVersion = ImpellerNative.ImpellerGetVersion();
    uint versionVariant = rawVersion >> 29;
    uint versionMajor = (rawVersion >> 22) & 0x7FU;
    uint versionMinor = (rawVersion >> 12) & 0x3FFU;
    uint versionPatch = rawVersion & 0xFFFU;

    return new Version((int)versionMajor, (int)versionMinor, (int)versionVariant, (int)versionPatch);
  }
}