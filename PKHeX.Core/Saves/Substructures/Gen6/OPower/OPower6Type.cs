namespace PKHeX.Core;

public enum OPower6Index : byte
{
    激活 = 0,

    孵蛋之力1 = 1,
    孵蛋之力2 = 2,
    孵蛋之力3 = 3,
    孵蛋之力S = 4,
    孵蛋之力MAX = 5,

    优惠之力1 = 6,
    优惠之力2 = 7,
    优惠之力3 = 8,
    优惠之力S = 9,
    优惠之力MAX = 10,

    零花钱之力1 = 11,
    零花钱之力2 = 12,
    零花钱之力3 = 13,
    零花钱之力S = 14,
    零花钱之力MAX = 15,

    经验之力1 = 16,
    经验之力2 = 17,
    经验之力3 = 18,
    经验之力S = 19,
    经验之力MAX = 20,

    捕获之力1 = 21,
    捕获之力2 = 22,
    捕获之力3 = 23,
    捕获之力S = 24,
    捕获之力MAX = 25,

    相遇之力1 = 26,
    相遇之力2 = 27,
    相遇之力3 = 28,

    隐身之力1 = 29,
    隐身之力2 = 30,
    隐身之力3 = 31,

    HP回复之力1 = 32,
    HP回复之力2 = 33,
    HP回复之力3 = 34,

    PP回复之力1 = 35,
    PP回复之力2 = 36,
    PP回复之力3 = 37,

    完全回复之力 = 38,

    亲密之力1 = 39,
    亲密之力2 = 40,
    亲密之力3 = 41,
    亲密之力S = 42,
    亲密之力MAX = 43,

    攻击之力1 = 44,
    攻击之力2 = 45,
    攻击之力3 = 46,

    防御之力1 = 47,
    防御之力2 = 48,
    防御之力3 = 49,

    特攻之力1 = 50,
    特攻之力2 = 51,
    特攻之力3 = 52,

    特防之力1 = 53,
    特防之力2 = 54,
    特防之力3 = 55,

    速度之力1 = 56,
    速度之力2 = 57,
    速度之力3 = 58,

    要害之力1 = 59,
    要害之力2 = 60,
    要害之力3 = 61,

    命中之力1 = 62,
    命中之力2 = 63,
    命中之力3 = 64,

    计数 = 65,
}

public static class OPowerTypeExtensions
{
    public static OPower6FieldType GetFieldType(this OPower6Index index) => index switch
    {
        0 => OPower6FieldType.Count, // Invalid
        <= OPower6Index.孵蛋之力MAX => OPower6FieldType.孵蛋之力,
        <= OPower6Index.优惠之力MAX => OPower6FieldType.优惠之力,
        <= OPower6Index.零花钱之力MAX => OPower6FieldType.零花钱之力,
        <= OPower6Index.经验之力MAX => OPower6FieldType.经验之力,
        <= OPower6Index.捕获之力MAX => OPower6FieldType.捕获之力,
        <= OPower6Index.相遇之力3 => OPower6FieldType.相遇之力,
        <= OPower6Index.隐身之力3 => OPower6FieldType.隐身之力,
        <= OPower6Index.HP回复之力3 => OPower6FieldType.HP回复之力,
        <= OPower6Index.PP回复之力3 => OPower6FieldType.PP回复之力,
           OPower6Index.完全回复之力 => OPower6FieldType.Count, // Invalid
        <= OPower6Index.亲密之力MAX => OPower6FieldType.亲密之力,
        _ => OPower6FieldType.Count, // Invalid
    };

    public static OPower6BattleType GetBattleType(this OPower6Index index) => index switch
    {
        >= OPower6Index.攻击之力1 and <= OPower6Index.命中之力3 => (OPower6BattleType)((index - OPower6Index.攻击之力1) / 3),
        _ => OPower6BattleType.Count, // Invalid
    };
}
