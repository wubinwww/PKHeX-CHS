using System;
using System.ComponentModel;
using static System.Buffers.Binary.BinaryPrimitives;

namespace PKHeX.Core;

public sealed class RaidSpawnList8(SAV8SWSH sav, SCBlock block, int legal) : SaveBlock<SAV8SWSH>(sav, block.Data)
{
    public readonly int CountAll = block.Data.Length / RaidSpawnDetail.SIZE;
    public readonly int CountUsed = legal;

    public const int RaidCountLegal_O0 = 100;
    public const int RaidCountLegal_R1 = 90;
    public const int RaidCountLegal_R2 = 86;

    public RaidSpawnDetail GetRaid(int entry) => new(Raw.Slice(entry * RaidSpawnDetail.SIZE, RaidSpawnDetail.SIZE));

    public RaidSpawnDetail[] GetAllRaids()
    {
        var result = new RaidSpawnDetail[CountAll];
        for (int i = 0; i < result.Length; i++)
            result[i] = GetRaid(i);
        return result;
    }

    public void DectivateAllRaids()
    {
        for (int i = 0; i < CountUsed; i++)
        {
            if (i == 16) // Watchtower, special
                continue;
            GetRaid(i).Deactivate();
        }
    }

    public void ActivateAllRaids(bool rare, bool isEvent)
    {
        var rnd = Util.Rand;
        for (int i = 0; i < CountUsed; i++)
        {
            if (i == 16) // Watchtower, special
                continue;
            var star = (byte)rnd.Next(5);
            var rand = (byte)(1 + rnd.Next(100));
            GetRaid(i).Activate(star, rand, rare, isEvent);
        }
    }

    public string[] DumpAll()
    {
        var raids = GetAllRaids();
        var result = new string[CountAll];
        for (int i = 0; i < result.Length; i++)
            result[i] = raids[i].Dump();
        return result;
    }
}

public sealed class RaidSpawnDetail(Memory<byte> raw)
{
    public const int SIZE = 0x18;

    private Span<byte> Data => raw.Span;

    private const string General = nameof(General);
    private const string Derived = nameof(Derived);

    [Category(General), Description("用于获取团战数据表的FNV哈希算法（64位）."), TypeConverter(typeof(TypeConverterU64))]
    public ulong Hash
    {
        get => ReadUInt64LittleEndian(Data);
        set => WriteUInt64LittleEndian(Data, value);
    }

    [Category(General), Description("用于生成团战内容的RNG种子（64位）."), TypeConverter(typeof(TypeConverterU64))]
    public ulong Seed
    {
        get => ReadUInt64LittleEndian(Data[8..]);
        set => WriteUInt64LittleEndian(Data[8..], value);
    }

    [Category(General), Description("团战内容的星数(0-4).")]
    public byte Stars
    {
        get => Data[0x10];
        set => Data[0x10] = value;
    }

    [Category(General), Description("从团战数据表中挑选遭遇精灵的随机值(1-100).")]
    public byte RandRoll
    {
        get => Data[0x11];
        set => Data[0x11] = value;
    }

    [Category(General), Description("巢穴类型.")]
    public RaidType DenType
    {
        get => (RaidType)Data[0x12];
        set
        {
            Data[0x12] = (byte)value;
            if (value == RaidType.活动)
            {
                IsEvent = true;
            }
            else if (value != RaidType.许愿星普通)
            {
                IsEvent = false;
            }
        }
    }

    [Category(General), Description("巢穴旗帜.")]
    public byte Flags
    {
        get => Data[0x13];
        set => Data[0x13] = value;
    }

    [Category(Derived), Description("活动巢穴")]
    public bool IsActive => DenType > 0;

    [Category(Derived), Description("使用稀有的遭遇代替普通遭遇.")]
    public bool IsRare
    {
        get => DenType is RaidType.稀有 or RaidType.许愿星稀有;
        set
        {
            if (value)
            {
                DenType = IsWishingPiece ? RaidType.许愿星稀有 : RaidType.稀有;
            }
            else
            {
                DenType = IsWishingPiece ? RaidType.许愿星普通 : RaidType.普通;
            }
        }
    }

    [Category(Derived), Description("许愿星被用于团队遭遇战.")]
    public bool IsWishingPiece
    {
        get => DenType is RaidType.许愿星普通 or RaidType.许愿星稀有;
        set
        {
            if (value)
            {
                DenType = IsRare ? RaidType.许愿星稀有 : RaidType.许愿星普通;
            }
            else
            {
                DenType = IsRare ? RaidType.稀有 : RaidType.普通;
            }
        }
    }

    [Category(Derived), Description("瓦特已经获取了吗?")]
    public bool WattsHarvested
    {
        get => IsActive && (Flags & 1) == 1;
        set => Flags = (byte)((Flags & ~1) | (value ? 1 : 0));
    }

    [Category(Derived), Description("用于团战遭遇的分布(事件)详细信息.")]
    public bool IsEvent
    {
        get => IsActive && (Flags & 2) == 2;
        set
        {
            Flags = (byte)((Flags & ~2) | (value ? 2 : 0));
            if (value)
            {
                if (DenType != RaidType.许愿星普通 && DenType != RaidType.活动)
                {
                    DenType = RaidType.活动;
                }
            }
            else
            {
                if (DenType == RaidType.活动)
                {
                    DenType = RaidType.普通;
                }
            }
        }
    }

    public void Activate(byte star, byte rand, bool rare = false, bool isEvent = false)
    {
        Stars = star;
        RandRoll = rand;
        IsRare = rare;
        IsEvent = isEvent;
    }

    public void Deactivate()
    {
        DenType = RaidType.无;
        Stars = 0;
        RandRoll = 0;
    }

    public string Dump() => $"{Hash:X16}\t{Seed:X16}\t{Stars}\t{RandRoll:00}\t{DenType}\t{Flags:X2}";

    // The games use a xoroshiro RNG to create the PKM from the stored seed.
}

public enum RaidType : byte
{
    无 = 0,
    普通 = 1,
    稀有 = 2,
    许愿星普通 = 3,
    许愿星稀有 = 4,
    活动 = 5,
    极巨结晶 = 6,
}

public enum MaxRaidOrigin: uint
{
    迦勒尔,
    铠之孤岛,
    王冠雪原
}
