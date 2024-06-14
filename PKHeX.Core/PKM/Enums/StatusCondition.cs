using System;

namespace PKHeX.Core;

[Flags]
public enum StatusCondition
{
    无 = 0,
#pragma warning disable RCS1191 // Declare enum value as combination of names
    // Sleep (if present) indicates the number of turns remaining
    睡眠1 = 1,
    睡眠2 = 2,
    睡眠3 = 3,
    睡眠4 = 4,
    睡眠5 = 5,
    睡眠6 = 6,
    睡眠7 = 7,
#pragma warning restore RCS1191 // Declare enum value as combination of names
    中毒 = 1 << 3,
    灼伤 = 1 << 4,
    冰冻 = 1 << 5,
    麻痹 = 1 << 6,
    剧毒 = 1 << 7,
}

public static class StatusConditionUtil
{
    public static StatusType GetStatusType(this PKM pk)
    {
        var value = (StatusCondition)pk.Status_Condition;
        return GetStatusType(value);
    }

    public static StatusType GetStatusType(this StatusCondition value)
    {
        if (value == StatusCondition.无)
            return StatusType.无;
        if (value <= StatusCondition.睡眠7)
            return (StatusType)value;

        if ((value & StatusCondition.麻痹) != 0)
            return StatusType.麻痹;
        if ((value & StatusCondition.灼伤) != 0)
            return StatusType.灼伤;
        if ((value & (StatusCondition.中毒 | StatusCondition.剧毒)) != 0)
            return StatusType.中毒;
        if ((value & StatusCondition.冰冻) != 0)
            return StatusType.冰冻;

        return StatusType.无;
    }
}

public enum StatusType
{
    无 = 0,
    睡眠 = 1,
    中毒 = 2,
    灼伤 = 3,
    冰冻 = 4,
    麻痹 = 5,
}
