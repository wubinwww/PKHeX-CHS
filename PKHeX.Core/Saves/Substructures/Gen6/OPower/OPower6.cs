using System;
using static PKHeX.Core.OPower6Type;

namespace PKHeX.Core;

public sealed class OPower6 : SaveBlock<SAV6>
{
    private static readonly OPowerFlagSet[] Mapping =
    {
        // Skip unused byte
        new(5, 孵蛋之力) {Offset = 1},
        new(5, 优惠之力) {Offset = 6},
        new(5, 零花钱之力) {Offset = 11},
        new(5, 经验之力) {Offset = 16},
        new(5, 捕获之力) {Offset = 21},

        new(3, 相遇之力) {Offset = 26},
        new(3, 隐身之力) {Offset = 29},
        new(3, HP回复之力) {Offset = 32},
        new(3, PP回复之力) {Offset = 35},

        new(1, 完全回复之力) {Offset = 38},

        new(5, 亲密之力) {Offset = 39},

        new(3, 攻击之力) {Offset = 44},
        new(3, 防御之力) {Offset = 47},
        new(3, 特攻之力) {Offset = 50},
        new(3, 特防之力) {Offset = 53},
        new(3, 速度之力) {Offset = 56},
        new(3, 要害之力) {Offset = 59},
        new(3, 命中之力) {Offset = 62},
    };

    public OPower6(SAV6XY sav, int offset) : base(sav) => Offset = offset;
    public OPower6(SAV6AO sav, int offset) : base(sav) => Offset = offset;

    private static OPowerFlagSet Get(OPower6Type type) => Array.Find(Mapping, t => t.Identifier == type) ?? throw new ArgumentOutOfRangeException(nameof(type));
    public static int GetOPowerCount(OPower6Type type) => Get(type).BaseCount;
    public int GetOPowerLevel(OPower6Type type) => Get(type).GetOPowerLevel(Data.AsSpan(Offset));

    public static bool GetHasOPowerS(OPower6Type type) => Get(type).HasOPowerS;
    public static bool GetHasOPowerMAX(OPower6Type type) => Get(type).HasOPowerMAX;
    public bool GetOPowerS(OPower6Type type) => Get(type).GetOPowerS(Data.AsSpan(Offset));
    public bool GetOPowerMAX(OPower6Type type) => Get(type).GetOPowerMAX(Data.AsSpan(Offset));

    public void SetOPowerLevel(OPower6Type type, int lvl) => Get(type).SetOPowerLevel(Data.AsSpan(Offset), lvl);
    public void SetOPowerS(OPower6Type type, bool value) => Get(type).SetOPowerS(Data.AsSpan(Offset), value);
    public void SetOPowerMAX(OPower6Type type, bool value) => Get(type).SetOPowerMAX(Data.AsSpan(Offset), value);

    public bool MasterFlag
    {
        get => Data[Offset] == 1;
        set => Data[Offset] = (byte) (value ? OPowerFlagState.Unlocked : OPowerFlagState.Locked);
    }

    public void UnlockAll() => ToggleFlags(allEvents: true);
    public void UnlockRegular() => ToggleFlags();
    public void ClearAll() => ToggleFlags(clearOnly: true);

    private void ToggleFlags(bool allEvents = false, bool clearOnly = false)
    {
        var span = Data.AsSpan(Offset);
        foreach (var m in Mapping)
        {
            // Clear before applying new value
            m.SetOPowerLevel(span, 0);
            m.SetOPowerS(span, false);
            m.SetOPowerMAX(span, false);

            if (clearOnly)
                continue;

            int lvl = allEvents ? m.BaseCount : (m.BaseCount != 1 ? 3 : 0); // Full_Recovery is ORAS/event only @ 1 level
            m.SetOPowerLevel(span, lvl);
            if (!allEvents)
                continue;

            m.SetOPowerS(span, true);
            m.SetOPowerMAX(span, true);
        }
    }

    public byte[] Write() => Data;
}
