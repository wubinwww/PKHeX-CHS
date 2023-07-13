using System;
using static System.Buffers.Binary.BinaryPrimitives;

namespace PKHeX.Core;

public abstract class TrainerFashion6
{
    protected uint data0;
    protected uint data1;
    protected uint data2;
    protected uint data3;

    protected TrainerFashion6(ReadOnlySpan<byte> data, int offset) : this(data[offset..]) { }

    private TrainerFashion6(ReadOnlySpan<byte> span)
    {
        data0 = ReadUInt32LittleEndian(span);
        data1 = ReadUInt32LittleEndian(span[04..]);
        data2 = ReadUInt32LittleEndian(span[08..]);
        data3 = ReadUInt32LittleEndian(span[12..]);
    }

    public static TrainerFashion6 GetFashion(byte[] data, int offset, int gender)
    {
        if (gender == 0) // m
            return new Fashion6Male(data, offset);
        return new Fashion6Female(data, offset);
    }

    public void Write(byte[] data, int offset)
    {
        var span = data.AsSpan(offset);
        WriteUInt32LittleEndian(span, data0);
        WriteUInt32LittleEndian(span[04..], data1);
        WriteUInt32LittleEndian(span[08..], data2);
        WriteUInt32LittleEndian(span[12..], data3);
    }

    protected static uint GetBits(uint value, int startPos, int bits)
    {
        uint mask = ((1u << bits) - 1) << startPos;
        return (value & mask) >> startPos;
    }

    protected static uint SetBits(uint value, int startPos, int bits, uint bitValue)
    {
        uint mask = ((1u << bits) - 1) << startPos;
        bitValue &= mask >> startPos;
        return (value & ~mask) | (bitValue << startPos);
    }

    public enum F6HairColor
    {
        黑色,
        褐色,
        橡色,
        橙色,
        金色,
    }

    public enum F6ContactLens
    {
        褐色,
        淡褐色,
        无,
        绿色,
        蓝色,
    }

    public enum F6Skin
    {
        白色,
        亮白色,
        棕褐色,
        黑色,
    }
}

public sealed class Fashion6Male : TrainerFashion6
{
    public Fashion6Male(byte[] data, int offset)
        : base(data, offset) { }

    public uint Version  { get => GetBits(data0,  0, 3); set => data0 = SetBits(data0,  0, 3, value); }
    public uint Model    { get => GetBits(data0,  3, 3); set => data0 = SetBits(data0,  3, 3, value); }
    public F6Skin Skin   { get => (F6Skin)GetBits(data0,  6, 2); set => data0 = SetBits(data0,  6, 2, (uint)value); }
    public F6HairColor HairColor { get => (F6HairColor)GetBits(data0,  8, 3); set => data0 = SetBits(data0,  8, 3, (uint)value); }
    public F6Hat Hat     { get => (F6Hat)GetBits(data0, 11, 5); set => data0 = SetBits(data0, 11, 5, (uint)value); }
    public F6HairStyleFront Front    { get => (F6HairStyleFront)GetBits(data0, 16, 3); set => data0 = SetBits(data0, 16, 3, (uint)value); }
    public F6HairStyle Hair { get => (F6HairStyle)GetBits(data0, 19, 4); set => data0 = SetBits(data0, 19, 4, (uint)value); }
    public uint Face     { get => GetBits(data0, 23, 3); set => data0 = SetBits(data0, 23, 3, value); }
    public uint Arms     { get => GetBits(data0, 26, 2); set => data0 = SetBits(data0, 26, 2, value); }
    public uint Unknown0 { get => GetBits(data0, 28, 2); set => data0 = SetBits(data0, 28, 2, value); }
    public uint Unused0  { get => GetBits(data0, 30, 2); set => data0 = SetBits(data0, 30, 2, value); }

    public F6Top Top      { get => (F6Top)GetBits(data1,  0, 6); set => data1 = SetBits(data1,  0, 6, (uint)value); }
    public F6Bottoms Legs { get => (F6Bottoms)GetBits(data1,  6, 5); set => data1 = SetBits(data1,  6, 5, (uint)value); }
    public F6Socks Socks  { get => (F6Socks)GetBits(data1, 11, 3); set => data1 = SetBits(data1, 11, 3, (uint)value); }
    public F6Shoes Shoes  { get => (F6Shoes)GetBits(data1, 14, 5); set => data1 = SetBits(data1, 14, 5, (uint)value); }
    public F6Bag Bag      { get => (F6Bag)GetBits(data1, 19, 4); set => data1 = SetBits(data1, 19, 4, (uint)value); }
    public F6Accessory AHat { get => (F6Accessory)GetBits(data1, 23, 4); set => data1 = SetBits(data1, 23, 4, (uint)value); }
    public uint Unknown1  { get => GetBits(data1, 27, 2); set => data1 = SetBits(data1, 27, 2, value); }
    public uint Unused1   { get => GetBits(data1, 29, 3); set => data1 = SetBits(data1, 29, 3, value); }

    public bool Contacts      { get => GetBits(data2,  0, 1) == 1; set => data2 = SetBits(data2,  0, 1, value ? 1u : 0); }
    public uint FacialHair    { get => GetBits(data2,  1, 3);      set => data2 = SetBits(data2,  1, 3, value); }
    public F6ContactLens ColorContacts { get => (F6ContactLens)GetBits(data2,  4, 3);  set => data2 = SetBits(data2,  4, 3, (uint)value); }
    public uint FacialColor   { get => GetBits(data2,  7, 3);      set => data2 = SetBits(data2,  7, 3, value); }
    public uint PaintLeft     { get => GetBits(data2, 10, 4);      set => data2 = SetBits(data2, 10, 4, value); }
    public uint PaintRight    { get => GetBits(data2, 14, 4);      set => data2 = SetBits(data2, 14, 4, value); }
    public uint PaintLeftC    { get => GetBits(data2, 18, 3);      set => data2 = SetBits(data2, 18, 3, value); }
    public uint PaintRightC   { get => GetBits(data2, 21, 3);      set => data2 = SetBits(data2, 21, 3, value); }
    public bool Freckles      { get => GetBits(data2, 24, 2) == 1; set => data2 = SetBits(data2, 24, 2, value ? 1u : 0); }
    public uint ColorFreckles { get => GetBits(data2, 26, 3);      set => data2 = SetBits(data2, 26, 3, value); }
    public uint Unused2       { get => GetBits(data2, 29, 3);      set => data2 = SetBits(data2, 29, 3, value); }

    public enum F6Top
    {
        _0,
        开衫夹克_蓝色,
        开衫夹克_红色,
        开衫夹克_绿色,
        开衫夹克_黑色,
        开衫夹克_藏青色,
        开衫夹克_橙色,
        羽绒夹克_黑色,
        羽绒夹克_红色,
        羽绒夹克_海蓝色,
        睡衣_上衣,
        彩色条纹衬衫_紫色,
        彩色条纹衬衫_红色,
        彩色条纹衬衫_海蓝色,
        彩色条纹衬衫_浅粉色,
        格子衬衫_红色,
        格子衬衫_灰色,
        拉链衬衫_黑色,
        拉链衬衫_白色,
        连帽衫_橄榄色,
        连帽衫_海蓝色,
        连帽衫_黄色,
        V领T恤_黑色,
        V领T恤_白色,
        V领T恤_浅粉色,
        V领T恤_海蓝色,
        带标志的T恤_白色,
        带标志的T恤_橙色,
        带标志的T恤_绿色,
        带标志的T恤_蓝色,
        带标志的T恤_黄色,
        艺术T恤_黑色,
        艺术T恤_红色,
        艺术T恤_紫色,
        王者T恤_红色,
        同款T恤_黑色,
    }

    public enum F6Socks
    {
        无,
        过踝袜_黑色,
        过踝袜_红色,
        过踝袜_绿色,
        过踝袜_紫色,
    }

    public enum F6Shoes
    {
        _0,
        赤脚,
        _2,
        短靴_黑色,
        短靴_红色,
        短靴_褐色,
        _6,
        休闲鞋_褐色,
        休闲鞋_黑色,
        运动鞋_黑色,
        运动鞋_白色,
        运动鞋_红色,
        运动鞋_蓝色,
        运动鞋_黄色,
    }

    public enum F6Hat
    {
        无,
        带标志的太阳帽_故障, // hacked
        带标志的太阳帽_黑色,
        带标志的太阳帽_蓝色,
        带标志的太阳帽_红色,
        带标志的太阳帽_绿色,
        中折礼帽_故障, // hacked
        中折礼帽_红色,
        中折礼帽_灰色,
        中折礼帽_黑色,
        格子礼帽_黑色,
        狩猎帽_故障, // hacked
        狩猎帽_红色,
        狩猎帽_黑色,
        狩猎帽_橄榄色,
        狩猎帽_米色,
        绒帽_故障, // hacked
        绒帽_白色,
        绒帽_黑色,
        绒帽_橙色,
        绒帽_紫色,
        迷彩绒帽_橄榄色,
        迷彩绒帽_海蓝色,
        竹子礼帽,
    }

    public enum F6HairStyle
    {
        _0,
        中发,
        中烫发,
        短发,
        超短发,
    }

    public enum F6Bottoms
    {
        _0,
        斜纹棉布裤_黑色,
        斜纹棉布裤_米色,
        格子裤子_红色,
        格子裤子_灰色,
        卷边牛仔裤_蓝色,
        破洞牛仔裤_海蓝色,
        紧身牛仔裤_黑色,
        睡衣_裤子,
        短工装裤_黑色,
        短工装裤_橄榄色,
        短工装裤_紫色,
        光面漆皮裤_蓝色,
        光面漆皮裤_褐色,
        光面漆皮裤_米色,
        光面漆皮裤_红色,
        迷彩裤_绿色,
        迷彩裤_灰色,
    }

    public enum F6Bag
    {
        None,
        双色包包_黑色,
        双色包包_红色,
        双色包包_橄榄色,
        双色包包_海蓝色,
        双色包包_橙色,
        斜挎包_黑色,
        斜挎包_褐色,
    }

    public enum F6Accessory
    {
        无,
        彩色纽扣徽章_灰色,
        彩色纽扣徽章_浅粉色,
        彩色纽扣徽章_紫色,
        彩色纽扣徽章_黄色,
        彩色纽扣徽章_酸橙绿色,
        宽边太阳镜_黑色,
        宽边太阳镜_黄色,
        宽边太阳镜_红色,
        宽边太阳镜_白色,
        羽饰_黑色,
        羽饰_红色,
        羽饰_绿色,
    }

    public enum F6HairStyleFront
    {
        _0,
        默认,
    }
}

public sealed class Fashion6Female : TrainerFashion6
{
    public Fashion6Female(byte[] data, int offset)
        : base(data, offset) { }

    public uint Version  { get => GetBits(data0,  0, 3); set => data0 = SetBits(data0,  0, 3, value); }
    public uint Model    { get => GetBits(data0,  3, 3); set => data0 = SetBits(data0,  3, 3, value); }
    public F6Skin Skin   { get => (F6Skin)GetBits(data0,  6, 2); set => data0 = SetBits(data0,  6, 2, (uint)value); }
    public F6HairColor HairColor{ get => (F6HairColor)GetBits(data0,  8, 3); set => data0 = SetBits(data0,  8, 3, (uint)value); }
    public F6Hat Hat     { get => (F6Hat)GetBits(data0, 11, 6); set => data0 = SetBits(data0, 11, 6, (uint)value); }
    public F6HairStyleFront Front { get => (F6HairStyleFront)GetBits(data0, 17, 3); set => data0 = SetBits(data0, 17, 3, (uint)value); }
    public F6HairStyle Hair { get => (F6HairStyle)GetBits(data0, 20, 4); set => data0 = SetBits(data0, 20, 4, (uint)value); }
    public uint Face     { get => GetBits(data0, 24, 3); set => data0 = SetBits(data0, 24, 3, value); }
    public uint Arms     { get => GetBits(data0, 27, 2); set => data0 = SetBits(data0, 27, 2, value); }
    public uint Unknown0 { get => GetBits(data0, 29, 2); set => data0 = SetBits(data0, 29, 2, value); }
    public uint Unused0  { get => GetBits(data0, 31, 1); set => data0 = SetBits(data0, 31, 1, value); }

    public F6Top Top     { get => (F6Top)GetBits(data1,  0, 6); set => data1 = SetBits(data1,  0, 6, (uint)value); }
    public F6Bottom Legs { get => (F6Bottom)GetBits(data1,  6, 7); set => data1 = SetBits(data1,  6, 7, (uint)value); }
    public F6Dress Dress { get => (F6Dress)GetBits(data1, 13, 4); set => data1 = SetBits(data1, 13, 4, (uint)value); }
    public F6Socks Socks { get => (F6Socks)GetBits(data1, 17, 5); set => data1 = SetBits(data1, 17, 5, (uint)value); }
    public F6Shoes Shoes { get => (F6Shoes)GetBits(data1, 22, 6); set => data1 = SetBits(data1, 22, 6, (uint)value); }
    public uint Unknown1 { get => GetBits(data1, 28, 2); set => data1 = SetBits(data1, 28, 2, value); }
    public uint Unused1  { get => GetBits(data1, 30, 2); set => data1 = SetBits(data1, 30, 2, value); }

    public F6Bag Bag          { get => (F6Bag)GetBits(data2,  0, 5); set => data2 = SetBits(data2,  0, 5, (uint)value); }
    public F6Accessory AHat   { get => (F6Accessory)GetBits(data2,  5, 5); set => data2 = SetBits(data2,  5, 5, (uint)value); }
    public bool Contacts      { get => GetBits(data2, 10, 1) == 1; set => data2 = SetBits(data2, 10, 1, value ? 1u : 0); }
    public uint MascaraType   { get => GetBits(data2, 11, 2); set => data2 = SetBits(data2, 11, 2, value); }
    public bool Eyeliner      { get => GetBits(data2, 13, 2) == 1; set => data2 = SetBits(data2, 13, 2, value ? 1u : 0); }
    public bool Cheek         { get => GetBits(data2, 15, 2) == 1; set => data2 = SetBits(data2, 15, 2, value ? 1u : 0); }
    public bool Lips          { get => GetBits(data2, 17, 2) == 1; set => data2 = SetBits(data2, 17, 2, value ? 1u : 0); }
    public F6ContactLens ColorContacts { get => (F6ContactLens)GetBits(data2, 19, 3); set => data2 = SetBits(data2, 19, 3, (uint)value); }
    public bool Mascara       { get => GetBits(data2, 22, 3) == 1; set => data2 = SetBits(data2, 22, 3, value ? 1u : 0); }
    public uint ColorEyeliner { get => GetBits(data2, 25, 3);      set => data2 = SetBits(data2, 25, 3, value); }
    public uint ColorCheek    { get => GetBits(data2, 28, 3);      set => data2 = SetBits(data2, 28, 3, value); }
    public uint Unused2       { get => GetBits(data2, 31, 1);      set => data2 = SetBits(data2, 31, 1, value); }

    public uint ColorLips     { get => GetBits(data3, 0, 2); set => data3 = SetBits(data3, 0, 2, value); }
    public uint ColorFreckles { get => GetBits(data3, 2, 3); set => data3 = SetBits(data3, 2, 3, value); }
    public bool Freckles      { get => GetBits(data3, 5, 3) == 1; set => data3 = SetBits(data3, 5, 3, value ? 1u : 0); }
    public uint Unused3       { get => GetBits(data3, 8, 24); set => data3 = SetBits(data3, 8, 24, value); }

    public enum F6Top
    {
        无,
        荷叶边吊带背心_浅粉色,
        荷叶边吊带背心_海蓝色,
        荷叶边吊带背心_黄色,
        荷叶边吊带背心_黑色,
        条纹背心_黑色,
        条纹背心_蓝色,
        条纹背心_粉色,
        迷你吊带背心_海蓝色,
        迷你吊带背心_橙色,
        无袖编制衫_黑色,
        无袖编制衫_白色,
        睡衣_上衣,
        小型风雪大衣_红色,
        小型风雪大衣_粉色,
        小型风雪大衣_酸橙绿色,
        小型风雪大衣_蓝色,
        带领结的宽松女上衣_红色,
        带领结的宽松女上衣_灰色,
        带领结的宽松女上衣_褐色,
        带领结的宽松女上衣_粉色,
        带领结的宽松女上衣_黄色,
        带领结的宽松女上衣_紫色,
        民族针织衫_酸橙绿色,
        民族针织衫_橙色,
        带披肩的针织衫_黄色,
        带披肩的针织衫_粉色,
        带披肩的针织衫_紫色,
        华丽披肩针织衫_粉色,
        带领带的衬衫_灰色,
        带领带的衬衫_绿色,
        带领带的衬衫_蓝色,
        球形标志T恤_海蓝色,
        球形标志T恤_紫色,
        球形标志T恤_黄色,
        球形标志T恤_绿色,
    }

    public enum F6Socks
    {
        无,
        及膝袜_黑色,
        及膝袜_白色,
        及膝袜_红色,
        及膝袜_蓝色,
        及膝袜_绿色,
        及膝袜_粉色,
        及膝袜_黄色,
        无袜子,
        过膝袜_黑色,
        过膝袜_白色e,
        过膝袜_绿色,
        过膝袜_灰色,
        过膝袜_粉色,
        过膝袜_红色,
        过膝袜_褐色,
        单条纹过膝袜_白色,
        宽条纹过膝袜_黑色,
        宽条纹过膝袜_浅粉色,
        朋克过膝袜_黑色,
        迷彩过膝袜_褐色,
        打底裤_黑色,
        裤袜_黑色,
        裤袜_橙色,
        裤袜_浅粉色,
        裤袜_藏青色,
        裤袜_粉色,
        裤袜_紫色,
    }

    public enum F6Shoes
    {
        无,
        骑士马靴_粉色,
        骑士马靴_黑色,
        骑士马靴_白色,
        骑士马靴_灰色,
        骑士马靴_褐色,
        骑士马靴_米色,
        编织长靴_褐色,
        编织长靴_黑色,
        侧拉链长靴_黑色,
        赤脚,
        _11,
        高帮运动鞋_黑色,
        高帮运动鞋_粉色,
        高帮运动鞋_黄色,
        高帮运动鞋_紫色,
        小缎带浅口鞋_黑色,
        小缎带浅口鞋_褐色,
        绅士鞋_白色,
        绅士鞋_褐色,
        绅士鞋_藏青色,
        皮带坡跟鞋_黑色,
        皮带坡跟鞋_红色,
        皮带坡跟鞋_紫色,
        皮带坡跟鞋_白色,
        皮带坡跟鞋_粉色,
        皮带坡跟鞋_海蓝色,
    }

    public enum F6Hat
    {
        无,
        妇人帽_故障, // hacked
        妇人帽_黑色,
        妇人帽_白色,
        妇人帽_灰色,
        妇人帽_藏青色,
        妇人帽_褐色,
        妇人帽_粉色,
        妇人帽_浅粉色,
        妇人帽_米色,
        妇人帽_海蓝色,
        船工草帽_红色,
        船工草帽_蓝色,
        彩色帽子_故障, // hacked
        彩色帽子_海蓝色,
        彩色帽子_黄色,
        彩色帽子_绿色,
        带标志的太阳帽_粉色,
        带标志的太阳帽_黑色,
        鸭舌帽_故障, // hacked
        鸭舌帽_蓝色,
        鸭舌帽_白色,
        鸭舌帽_米色,
        民族鸭舌帽_褐色,
        民族鸭舌帽_紫色,
        中折礼帽_故障, // hacked
        中折礼帽_白色,
        中折礼帽_褐色,
        中折礼帽_红色,
        中折礼帽_黄色,
        中折礼帽_绿色,
        中折礼帽_紫色,
    }

    public enum F6HairStyle
    {
        无,
        波浪头,
        长发,
        中发,
        单马尾,
        短发,
        双马尾,
    }

    public enum F6HairStyleFront
    {
        无刘海,
        刘海,
        斜刘海,
    }

    public enum F6Dress
    {
        无,
        女生外套连衣裙_粉色,
        双排扣外套连衣裙_藏青色,
        风衣连衣裙_米色,
        风衣连衣裙_黑色,
        甜美裙子,
        蕾丝荷叶边连衣裙_浅粉色,
        无扣上衣连衣裙_粉色,
        迷你黑裙_黑色,
        高腰女士套装_黑色,
        高腰女士套装_白色,
    }

    public enum F6Bottom
    {
        无,
        牛仔迷你裙_蓝色,
        牛仔迷你裙_橄榄色,
        牛仔迷你裙_黑色,
        荷叶边迷你裙_橙色,
        荷叶边迷你裙_红色,
        紧身牛仔裤_海蓝色,
        紧身牛仔裤_白色,
        紧身牛仔裤_橄榄色,
        紧身牛仔裤_蓝色,
        紧身牛仔裤_米色,
        紧身牛仔裤_黑色,
        紧身牛仔裤_红色,
        破洞紧身牛仔裤_蓝色,
        双色牛仔裤_蓝色,
        双色牛仔裤_黄色,
        双色牛仔裤_红色,
        双色牛仔裤_酸橙绿色,
        宽线裤子_灰色,
        宽线裤子_绿色,
        宽线裤子_蓝色,
        睡衣_裤子,
        百褶裙_黑色,
        百褶裙_白色,
        百褶裙_红色,
        百褶裙_蓝色,
        甜美百褶裙_粉色,
        甜美百褶裙_海蓝色,
        甜美百褶裙_黄色,
        棋盘百褶裙_灰色,
        棋盘百褶裙_红色,
        多层荷叶裙_粉色,
        多层荷叶裙_黄色,
        多层荷叶裙_白色1,
        多层荷叶裙_紫色,
        多层荷叶裙_白色2,
        牛仔短裤_灰色,
        牛仔短裤_海蓝色,
        牛仔短裤_白色,
        牛仔短裤_褐色,
        牛仔短裤_黑色,
        牛仔短裤_粉色,
        牛仔短裤_橙色,
        破洞紧身牛仔短裤_蓝色,
        交叉针织裤_橄榄色,
        交叉针织裤_褐色,
    }

    public enum F6Bag
    {
        无,
        女士提包_粉色,
        女士提包_红色,
        女士提包_橙色,
        女士提包_黄色,
        女士提包_白色,
        漆皮条纹包_红色,
        漆皮条纹包_蓝色,
        缎带主题包_浅粉色,
        缎带主题包_海蓝色,
        皮带装饰包_黑色,
        皮带装饰包_褐色,
        皮带装饰包_米色,
        皮带装饰包_紫色,
        皮带装饰包_白色,
        流苏包包_紫色,
        流苏包包_绿色,
    }

    public enum F6Accessory
    {
        无,
        彩色纽扣徽章_灰色,
        彩色纽扣徽章_粉色,
        彩色纽扣徽章_紫色,
        彩色纽扣徽章_黄色,
        彩色纽扣徽章_酸橙绿色,
        花朵徽章_粉色,
        花朵徽章_浅粉色,
        花朵徽章_黄色,
        花朵徽章_海蓝色,
        宽边太阳镜_白色,
        宽边太阳镜_红色,
        宽边太阳镜_蓝色,
        宽边太阳镜_黄色,
        金属工章_金色,
        金属工章_银色,
        金属工章_黑色,
        帽子用缎带_黑色,
        帽子用缎带_红色,
        帽子用缎带_白色,
        帽子用缎带_蓝色,
        帽子用缎带_浅粉色,
        羽饰_黑色,
        羽饰_红色,
        羽饰_绿色,
    }
}
