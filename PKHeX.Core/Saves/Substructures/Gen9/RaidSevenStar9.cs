using System;
using System.ComponentModel;
using static System.Buffers.Binary.BinaryPrimitives;
// ReSharper disable UnusedMember.Global

namespace PKHeX.Core;

public sealed class RaidSevenStar9 : SaveBlock<SAV9SV>
{
    public readonly int CountAll;

    public RaidSevenStar9(SAV9SV sav, SCBlock block) : base(sav, block.Data)
    {
        CountAll = block.Data.Length / SevenStarRaidDetail.SIZE;
    }

    public SevenStarRaidDetail GetRaid(int entry) => new(Data, 0x00 + (entry * SevenStarRaidDetail.SIZE));

    public SevenStarRaidDetail[] GetAllRaids()
    {
        var result = new SevenStarRaidDetail[CountAll];
        for (int i = 0; i < result.Length; i++)
            result[i] = GetRaid(i);
        return result;
    }
}

public sealed class SevenStarRaidDetail
{
    public const int SIZE = 0x08;

    private readonly byte[] Data;
    private readonly int Offset;

    public SevenStarRaidDetail(byte[] data, int ofs)
    {
        Data = data;
        Offset = ofs;
    }

    private const string General = nameof(General);

    [Category(General), Description("用于此7星团体战的标识符。与这次突袭的首次发布日期相符.")]
    public uint Identifier
    {
        get => ReadUInt32LittleEndian(Data.AsSpan(Offset + 0x00));
        set => WriteUInt32LittleEndian(Data.AsSpan(Offset + 0x00), value);
    }

    [Category(General), Description("指示玩家是否已捕获此太晶团体战Boss.")]
    public bool Captured
    {
        get => Data[Offset + 0x04] == 1;
        set => Data[Offset + 0x04] = (byte)(value ? 1 : 0);
    }

    [Category(General), Description("指示此太晶团体战Boss是否至少被玩家击败过一次.")]
    public bool Defeated
    {
        get => Data[Offset + 0x05] == 1;
        set => Data[Offset + 0x05] = (byte)(value ? 1 : 0);
    }

    // 0x06 - 0x07 padding
}
