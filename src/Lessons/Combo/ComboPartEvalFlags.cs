using System;

namespace WallyMapSpinzor2;

[Flags]
public enum ComboPartEvalFlags
{
    HIDE = 1,
    IGNORE = 2,
    NODATA = 4,
    NOFLAG = 8,
    SUB = 16,
}