using System;
using static System.Buffers.Binary.BinaryPrimitives;

namespace PKHeX.Core;

public sealed class FestaBlock5 : SaveBlock<SAV5B2W2>
{
    public FestaBlock5(SAV5B2W2 SAV, int offset) : base(SAV) => Offset = offset;

    public const ushort MaxScore = 9999;
    public const int FunfestFlag = 2438;

    public const int MaxMissionIndex = (int)Funfest5Mission.最初的寻找树果;

    public ushort Hosted
    {
        get => ReadUInt16LittleEndian(Data.AsSpan(Offset + 0xF0));
        set => WriteUInt16LittleEndian(Data.AsSpan(Offset + 0xF0), Math.Min(MaxScore, value));
    }

    public ushort Participated
    {
        get => ReadUInt16LittleEndian(Data.AsSpan(Offset + 0xF2));
        set => WriteUInt16LittleEndian(Data.AsSpan(Offset + 0xF2), Math.Min(MaxScore, value));
    }

    public ushort Completed
    {
        get => ReadUInt16LittleEndian(Data.AsSpan(Offset + 0xF4));
        set => WriteUInt16LittleEndian(Data.AsSpan(Offset + 0xF4), Math.Min(MaxScore, value));
    }

    public ushort TopScores
    {
        get => ReadUInt16LittleEndian(Data.AsSpan(Offset + 0xF6));
        set => WriteUInt16LittleEndian(Data.AsSpan(Offset + 0xF6), Math.Min(MaxScore, value));
    }

    public byte WhiteEXP
    {
        get => Data[Offset + 0xF8];
        set => Data[Offset + 0xF8] = value;
    }

    public byte BlackEXP
    {
        get => Data[Offset + 0xF9];
        set => Data[Offset + 0xF9] = value;
    }

    public byte Participants
    {
        get => Data[Offset + 0xFA];
        set => Data[Offset + 0xFA] = value;
    }

    private static int GetMissionRecordOffset(int mission)
    {
        if ((uint)mission > MaxMissionIndex)
            throw new ArgumentOutOfRangeException(nameof(mission));
        return mission * sizeof(uint);
    }

    public Funfest5Score GetMissionRecord(int mission)
    {
        var raw = ReadUInt32LittleEndian(Data.AsSpan(Offset + GetMissionRecordOffset(mission)));
        return new Funfest5Score(raw);
    }

    public void SetMissionRecord(int mission, Funfest5Score score)
    {
        var value = score.RawValue;
        WriteUInt32LittleEndian(Data.AsSpan(Offset + GetMissionRecordOffset(mission)), value);
    }

    public bool IsFunfestMissionsUnlocked
    {
        get => SAV.GetEventFlag(FunfestFlag);
        set => SAV.SetEventFlag(FunfestFlag, value);
    }

    public bool IsFunfestMissionUnlocked(int mission)
    {
        if ((uint) mission > MaxMissionIndex)
            throw new ArgumentOutOfRangeException(nameof(mission));

        if (mission == 0)
            return !IsFunfestMissionsUnlocked;

        var req = FunfestMissionUnlockFlagRequired[mission];
        foreach (var f in req)
        {
            if (!SAV.GetEventFlag(f))
                return false;
        }
        return true;
    }

    public void UnlockFunfestMission(int mission)
    {
        if ((uint)mission > MaxMissionIndex)
            throw new ArgumentOutOfRangeException(nameof(mission));

        IsFunfestMissionsUnlocked = true;
        var req = FunfestMissionUnlockFlagRequired[mission];
        foreach (var f in req)
            SAV.SetEventFlag(f, true);
    }

    public void UnlockAllFunfestMissions()
    {
        for (int i = 0; i < MaxMissionIndex; i++)
            UnlockFunfestMission(i);
    }

    private readonly int[][] FunfestMissionUnlockFlagRequired =
    {
        Array.Empty<int>(),   // 00
        Array.Empty<int>(),   // 01
        new[] { 2444 },       // 02
        Array.Empty<int>(),   // 03
        new[] { 2445 },       // 04
        Array.Empty<int>(),   // 05
        new[] { 2462 },       // 06
        new[] { 2452, 2476 }, // 07
        new[] { 2476, 2548 }, // 08
        new[] { 2447 }, new[] { 2447 }, // 09
        new[] { 2453 }, new[] { 2453 }, // 10
        new[] { 2504 },       // 11
        new[] { 2457, 2507 }, // 12
        new[] { 2458, 2478 }, // 13
        new[] { 2456, 2508 }, // 14
        new[] { 2448 }, new[] { 2448 }, // 15
        new[] { 2549 },       // 16
        new[] { 2449 },       // 17
        new[] { 2479, 2513 }, // 18
        new[] { 2479, 2550 }, // 19
        new[] { 2481 },       // 20
        new[] { 2459 },       // 21
        new[] { 2454 },       // 22
        new[] { 2551 },       // 23
        new[] { 2400 },       // 24
        new[] { 2400 },       // 25
        new[] { 2400 }, new[] { 2400 }, // 26
        new[] { 2400 }, new[] { 2400 }, // 27
        new[] { 2400 },       // 28
        new[] { 2400, 2460 }, // 29
        new[] { 2400 },       // 30
        new[] { 2400, 2461 }, new[] { 2400, 2461 }, // 31
        new[] { 2437 },       // 32
        new[] { 2450 },       // 33
        new[] { 2451 },       // 34
        new[] { 2455 },       // 35
        new[] { 0105 },       // 36
        new[] { 2400 },       // 37
        new[] { 2557 },       // 38
    };

    public static int GetExpNeededForLevelUp(int lv)
    {
        return lv > 8 ? 50 : (lv * 5) + 5;
    }

    public static int GetTotalEntreeExp(int lv)
    {
        if (lv < 9)
            return lv * (lv + 1) * 5 / 2;
        return ((lv - 9) * 50) + 225;
    }
}

public enum Funfest5Mission
{
    最初的寻找树果 = 0,
    收集树果 = 1,
    寻找丢失的东西 = 2,
    寻找迷路的少年 = 3,
    享受购物 = 4,
    寻找差不多娃娃 = 5,
    搜索宝可梦 = 6,
    与格斗家修行 = 7,
    多人对打 = 8,
    寻找电飞鼠 = 9,
    吊桥上飘落的羽毛 = 10,
    挖掘宝藏 = 11,
    蘑菇的捉迷藏 = 12,
    多个财宝 = 13,
    树果大丰收 = 14,
    敲响钟 = 15,
    多次响起的吊钟 = 16,
    成为精英之路 = 17,
    撞击购物 = 18,
    记忆力训练 = 19,
    直到记忆的极限 = 20,
    寻找摇动的草丛 = 21,
    寻找碎片 = 22,
    那个道具我要了 = 23,
    目标是稻秸富翁 = 24,
    寻找隐藏洞穴 = 25,
    钓鱼大会 = 26,
    肥料收藏家 = 27,
    闪耀的心在何处 = 28,
    石头剪子布大会 = 29,
    和蛋一起散步 = 30,
    寻找大钢蛇 = 31,
    心跳的道具交换 = 32,
    开心的道具交换 = 33,
    一攫千金 = 34,
    宝藏狩猎 = 35,
    寻找神秘的矿石 = 36,
    寻找闪亮的矿石 = 37,
    被遗忘的失物 = 38,
    找不到的失物 = 39,
    理想的价格是 = 40,
    真实的价格是 = 41,
    喧闹的隐藏洞穴 = 42,
    静谧的隐藏洞穴 = 43,
    寻找树果大冒险 = 44,
}

public record struct Funfest5Score(uint RawValue)
{
    public Funfest5Score(int total, int score, int level, bool isNew) : this(0)
    {
        Total = total;
        Score = score;
        Level = level;
        IsNew = isNew;
    }

    // Structure - 32bits
    // u32 bestTotal:14
    // u32 bestScore:14
    // u32 level:3
    // u32 isNew:1

    public int Total
    {
        readonly get => (int)(RawValue & 0x3FFFu);
        set => RawValue = (RawValue & ~0x3FFFu) | ((uint)value & 0x3FFFu);
    }

    public int Score
    {
        readonly get => (int)((RawValue >> 14) & 0x3FFFu);
        set => RawValue = (RawValue & 0xF0003FFFu) | (((uint)value & 0x3FFFu) << 14);
    }

    public int Level
    {
        readonly get => (int)((RawValue >> 28) & 0x7u);
        set => RawValue = (RawValue & 0x8FFFFFFFu) | (((uint)value & 0x7u) << 28);
    }

    public bool IsNew
    {
        readonly get => RawValue >> 31 == 1;
        set => RawValue = (RawValue & 0x7FFFFFFFu) | ((value ? 1u : 0) << 31);
    }
}
