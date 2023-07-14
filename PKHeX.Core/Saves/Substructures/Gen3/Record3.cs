using System;
using System.Collections.Generic;
using static System.Buffers.Binary.BinaryPrimitives;

namespace PKHeX.Core;

public sealed class Record3
{
    private readonly SAV3 SAV;

    public uint GetRecord(int record) => ReadUInt32LittleEndian(SAV.Large.AsSpan(GetRecordOffset(record))) ^ SAV.SecurityKey;
    public void SetRecord(int record, uint value) => WriteUInt32LittleEndian(SAV.Large.AsSpan(GetRecordOffset(record)), value ^ SAV.SecurityKey);

    private int GetRecordOffset(int record)
    {
        var baseOffset = GetOffset(SAV.Version);
        var offset = baseOffset + (4 * record);
        return offset;
    }

    public Record3(SAV3 sav) => SAV = sav;

    public static int GetOffset(GameVersion ver) => ver switch
    {
        GameVersion.RS or GameVersion.R or GameVersion.S => 0x1540,
        GameVersion.E => 0x159C,
        GameVersion.FRLG or GameVersion.FR or GameVersion.LG => 0x1200,
        _ => throw new ArgumentException(nameof(ver)),
    };

    private static Type GetEnumType(GameVersion ver) => ver switch
    {
        GameVersion.RS or GameVersion.R or GameVersion.S => typeof(RecID3RuSa),
        GameVersion.FRLG or GameVersion.FR or GameVersion.LG => typeof(RecID3FRLG),
        GameVersion.E => typeof(RecID3Emerald),
        _ => throw new ArgumentException(nameof(ver)),
    };

    public static int[] GetEnumValues(GameVersion ver) => (int[])Enum.GetValues(GetEnumType(ver));
    public static string[] GetEnumNames(GameVersion ver) => Enum.GetNames(GetEnumType(ver));

    public static IList<ComboItem> GetItems(SAV3 sav)
    {
        var ver = sav.Version;
        var names = GetEnumNames(ver);
        var values = GetEnumValues(ver);

        var result = new ComboItem[values.Length];
        for (int i = 0; i < result.Length; i++)
        {
            var replaced = names[i].Replace('_', ' ');
            var titled = Util.ToTitleCase(replaced);
            result[i] = new ComboItem(titled, values[i]);
        }
        return result;
    }
}

/// <summary>
/// Record IDs for <see cref="GameVersion.R"/> and <see cref="GameVersion.S"/>
/// </summary>
/// <remarks>
/// https://github.com/pret/pokeruby/blob/f839afb24aa2c7b70e9c28a5c069aacc46993099/include/constants/game_stat.h
/// </remarks>
public enum RecID3RuSa
{
    已保存的游戏 = 0,
    首次游玩时间 = 1,
    改变流行语 = 2,
    种植的树果 = 3,
    已获得自行车 = 4,
    步数 = 5,
    被采访次数 = 6,
    总计对战 = 7,
    野生对战 = 8,
    训练师对战 = 9,
    已登入名人堂= 10,
    宝可梦捕捉 = 11,
    钓鱼捕获 = 12,
    孵蛋次数 = 13,
    宝可梦进化 = 14,
    使用宝可梦中心 = 15,
    在家休息 = 16,
    进入狩猎地带 = 17,
    使用居合劈 = 18,
    使用碎岩 = 19,
    改变秘密基地 = 20,
    宝可梦交换 = 21,
    未知_22 = 22,
    通讯对战胜利 = 23,
    通讯对战失败 = 24,
    通讯对战平局 = 25,
    使用跃起 = 26,
    使用挣扎 = 27,
    老虎机累积奖金 = 28,
    老虎机连续赢奖 = 29,
    进入对战塔 = 30,
    未知_31 = 31,
    对战塔最高连胜 = 32,
    做宝可方块 = 33,
    和朋友一起做宝可方块 = 34,
    参加通讯华丽大赛 = 35,
    参加华丽大赛 = 36,
    赢得华丽大赛 = 37,
    商店购物 = 38,
    使用探宝器 = 39,
    遇到下雨 = 40,
    已检查宝可梦图鉴 = 41,
    已收到奖章 = 42,
    壁架遭遇 = 43,
    观看电视 = 44,
    检查时钟 = 45,
    宝可梦抽奖获奖 = 46,
    已使用寄放屋 = 47,
    乘坐缆车 = 48,
    进入温泉 = 49,
    // NUM_GAME_STATS = 50
}

/// <summary>
/// Record IDs for <see cref="GameVersion.E"/>
/// </summary>
/// <remarks>
/// https://github.com/pret/pokeemerald/blob/3a40f5203baafb29f94dda8abdce6489d81635ae/include/constants/game_stat.h
/// </remarks>
public enum RecID3Emerald
{
    已保存的游戏 = 0,
    首次游玩时间 = 1,
    改变流行语 = 2,
    种植的树果 = 3,
    已获得自行车 = 4,
    步数 = 5,
    被采访 = 6,
    总计对战 = 7,
    野生对战 = 8,
    训练师对战 = 9,
    已登入名人堂 = 10,
    宝可梦捕捉 = 11,
    钓鱼捕获 = 12,
    孵蛋 = 13,
    宝可梦进化 = 14,
    使用宝可梦中心 = 15,
    在家休息 = 16,
    进入狩猎地带 = 17,
    使用居合劈 = 18,
    使用碎岩 = 19,
    改变秘密基地 = 20,
    宝可梦交换 = 21,
    未知_22 = 22,
    通讯对战胜利 = 23,
    通讯对战失败 = 24,
    通讯对战平局 = 25,
    使用跃起 = 26,
    使用挣扎 = 27,
    老虎机累积奖金 = 28,
    老虎机连续赢奖 = 29,
    进入对战塔 = 30,
    未知_31 = 31,
    对战塔最高连胜 = 32,
    做宝可方块 = 33,
    和朋友一起做宝可方块 = 34,
    参加通讯华丽大赛 = 35,
    参加华丽大赛 = 36,
    赢得华丽大赛 = 37,
    商店购物 = 38,
    使用探宝器 = 39,
    遇到下雨 = 40,
    已检查宝可梦图鉴 = 41,
    已收到奖章 = 42,
    跳下壁架 = 43,
    观看电视 = 44,
    检查时钟 = 45,
    宝可梦抽奖获奖 = 46,
    已使用寄放屋 = 47,
    乘坐缆车 = 48,
    进入温泉 = 49,
    与朋友通讯 = 50,
    与朋友一起吃树果 = 51,

    // NUM_USED_GAME_STATS = 52,
    // NUM_GAME_STATS = 64
}

/// <summary>
/// Record IDs for <see cref="GameVersion.FR"/> and <see cref="GameVersion.LG"/>
/// </summary>
/// <remarks>
/// https://github.com/pret/pokefirered/blob/8367b0015fbf99070cc5a5244d8213420419d2c8/include/constants/game_stat.h
/// </remarks>
public enum RecID3FRLG
{
    已保存的游戏 = 0,
    首次游玩时间 = 1,
    改变流行语 = 2,
    种植的树果 = 3,
    已获得自行车 = 4,
    步数 = 5,
    被采访 = 6,
    总计对战 = 7,
    野生对战 = 8,
    训练师对战 = 9,
    已登入名人堂 = 10,
    宝可梦捕捉 = 11,
    钓鱼捕获 = 12,
    孵蛋次数 = 13,
    宝可梦进化 = 14,
    使用宝可梦中心 = 15,
    在家休息 = 16,
    进入狩猎地带 = 17,
    使用居合劈 = 18,
    使用碎岩 = 19,
    改变秘密基地 = 20,
    宝可梦交换 = 21,
    未知_22 = 22,
    通讯对战胜利 = 23,
    通讯对战失败 = 24,
    通讯对战平局 = 25,
    使用跃起 = 26,
    使用挣扎 = 27,
    老虎机累积奖金 = 28,
    老虎机连续赢奖 = 29,
    进入对战塔 = 30,
    未知_31 = 31,
    对战塔最高连胜 = 32,
    做宝可方块 = 33,
    和朋友一起做宝可方块 = 34,
    参加通讯华丽大赛 = 35,
    参加华丽大赛 = 36,
    赢得华丽大赛 = 37,
    商店购物 = 38,
    使用探宝器 = 39,
    遇到下雨 = 40,
    已检查宝可梦图鉴 = 41,
    已收到奖章 = 42,
    壁架遭遇 = 43,
    观看电视 = 44,
    检查时钟 = 45,
    宝可梦抽奖获奖 = 46,
    已使用寄放屋 = 47,
    乘坐缆车 = 48,
    进入温泉 = 49,
    与朋友通讯 = 50,
    与朋友一起吃树果 = 51,

    // NUM_GAME_STATS = 64,
}
