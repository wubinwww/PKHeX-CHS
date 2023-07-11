using System;

namespace PKHeX.Core;

[Flags]
public enum EntreeForestArea
{
    无,
    最深处 = 1 << 0,
    第一区 =   1 << 1,
    第二区 =  1 << 2,
    第三区 =   1 << 3,
    第四区 =  1 << 4,
    第五区 =   1 << 5,
    第六区 =   1 << 6,
    第七区 = 1 << 7,
    第八区 =  1 << 8,
    第九区 =   1 << 9,

    左边 =    1 << 10,
    右边 =   1 << 11,
    中央 =  1 << 12,
}
