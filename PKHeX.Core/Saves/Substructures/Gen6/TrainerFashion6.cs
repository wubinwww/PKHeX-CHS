using System;
using static System.Buffers.Binary.BinaryPrimitives;

namespace PKHeX.Core;

public abstract class TrainerFashion6(ReadOnlySpan<byte> span)
{
    public const int SIZE = 16;

    protected uint data0 = ReadUInt32LittleEndian(span);
    protected uint data1 = ReadUInt32LittleEndian(span[04..]);
    protected uint data2 = ReadUInt32LittleEndian(span[08..]);
    protected uint data3 = ReadUInt32LittleEndian(span[12..]);

    public static TrainerFashion6 GetFashion(ReadOnlySpan<byte> data, byte gender)
    {
        if (gender == 0) // m
            return new Fashion6Male(data);
        return new Fashion6Female(data);
    }

    public void Write(Span<byte> data)
    {
        WriteUInt32LittleEndian(data, data0);
        WriteUInt32LittleEndian(data[04..], data1);
        WriteUInt32LittleEndian(data[08..], data2);
        WriteUInt32LittleEndian(data[12..], data3);
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
        ��ɫ,
        ��ɫ,
        ��ɫ,
        ��ɫ,
        ��ɫ,
    }

    public enum F6ContactLens
    {
        ��ɫ,
        ����ɫ,
        ��,
        ��ɫ,
        ��ɫ,
    }

    public enum F6Skin
    {
        ��ɫ,
        ����ɫ,
        �غ�ɫ,
        ��ɫ,
    }
}

public sealed class Fashion6Male(ReadOnlySpan<byte> data) : TrainerFashion6(data)
{
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
        �����п�_��ɫ,
        �����п�_��ɫ,
        �����п�_��ɫ,
        �����п�_��ɫ,
        �����п�_����ɫ,
        �����п�_��ɫ,
        ���޼п�_��ɫ,
        ���޼п�_��ɫ,
        ���޼п�_����ɫ,
        ˯��_����,
        ��ɫ���Ƴ���_��ɫ,
        ��ɫ���Ƴ���_��ɫ,
        ��ɫ���Ƴ���_����ɫ,
        ��ɫ���Ƴ���_ǳ��ɫ,
        ���ӳ���_��ɫ,
        ���ӳ���_��ɫ,
        ��������_��ɫ,
        ��������_��ɫ,
        ��ñ��_���ɫ,
        ��ñ��_����ɫ,
        ��ñ��_��ɫ,
        V��T��_��ɫ,
        V��T��_��ɫ,
        V��T��_ǳ��ɫ,
        V��T��_����ɫ,
        ����־��T��_��ɫ,
        ����־��T��_��ɫ,
        ����־��T��_��ɫ,
        ����־��T��_��ɫ,
        ����־��T��_��ɫ,
        ����T��_��ɫ,
        ����T��_��ɫ,
        ����T��_��ɫ,
        ����T��_��ɫ,
        ͬ��T��_��ɫ,
    }

    public enum F6Socks
    {
        ��,
        ������_��ɫ,
        ������_��ɫ,
        ������_��ɫ,
        ������_��ɫ,
    }

    public enum F6Shoes
    {
        _0,
        ���,
        _2,
        ��ѥ_��ɫ,
        ��ѥ_��ɫ,
        ��ѥ_��ɫ,
        _6,
        ����Ь_��ɫ,
        ����Ь_��ɫ,
        �˶�Ь_��ɫ,
        �˶�Ь_��ɫ,
        �˶�Ь_��ɫ,
        �˶�Ь_��ɫ,
        �˶�Ь_��ɫ,
    }

    public enum F6Hat
    {
        ��,
        ����־��̫��ñ_����, // hacked
        ����־��̫��ñ_��ɫ,
        ����־��̫��ñ_��ɫ,
        ����־��̫��ñ_��ɫ,
        ����־��̫��ñ_��ɫ,
        ������ñ_����, // hacked
        ������ñ_��ɫ,
        ������ñ_��ɫ,
        ������ñ_��ɫ,
        ������ñ_��ɫ,
        ����ñ_����, // hacked
        ����ñ_��ɫ,
        ����ñ_��ɫ,
        ����ñ_���ɫ,
        ����ñ_��ɫ,
        ��ñ_����, // hacked
        ��ñ_��ɫ,
        ��ñ_��ɫ,
        ��ñ_��ɫ,
        ��ñ_��ɫ,
        �Բ���ñ_���ɫ,
        �Բ���ñ_����ɫ,
        ������ñ,
    }

    public enum F6HairStyle
    {
        _0,
        �з�,
        ���̷�,
        �̷�,
        ���̷�,
    }

    public enum F6Bottoms
    {
        _0,
        б���޲���_��ɫ,
        б���޲���_��ɫ,
        ���ӿ���_��ɫ,
        ���ӿ���_��ɫ,
        ����ţ�п�_��ɫ,
        �ƶ�ţ�п�_����ɫ,
        ����ţ�п�_��ɫ,
        ˯��_����,
        �̹�װ��_��ɫ,
        �̹�װ��_���ɫ,
        �̹�װ��_��ɫ,
        ������Ƥ��_��ɫ,
        ������Ƥ��_��ɫ,
        ������Ƥ��_��ɫ,
        ������Ƥ��_��ɫ,
        �Բʿ�_��ɫ,
        �Բʿ�_��ɫ,
    }

    public enum F6Bag
    {
        ��,
        ˫ɫ����_��ɫ,
        ˫ɫ����_��ɫ,
        ˫ɫ����_���ɫ,
        ˫ɫ����_����ɫ,
        ˫ɫ����_��ɫ,
        б���_��ɫ,
        б���_��ɫ,
    }

    public enum F6Accessory
    {
        ��,
        ��ɫŦ�ۻ���_��ɫ,
        ��ɫŦ�ۻ���_ǳ��ɫ,
        ��ɫŦ�ۻ���_��ɫ,
        ��ɫŦ�ۻ���_��ɫ,
        ��ɫŦ�ۻ���_�����ɫ,
        ����̫����_��ɫ,
        ����̫����_��ɫ,
        ����̫����_��ɫ,
        ����̫����_��ɫ,
        ����_��ɫ,
        ����_��ɫ,
        ����_��ɫ,
    }

    public enum F6HairStyleFront
    {
        _0,
        Ĭ��,
    }
}

public sealed class Fashion6Female(ReadOnlySpan<byte> data) : TrainerFashion6(data)
{
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
        ��,
        ��Ҷ�ߵ�������_ǳ��ɫ,
        ��Ҷ�ߵ�������_����ɫ,
        ��Ҷ�ߵ�������_��ɫ,
        ��Ҷ�ߵ�������_��ɫ,
        ���Ʊ���_��ɫ,
        ���Ʊ���_��ɫ,
        ���Ʊ���_��ɫ,
        �����������_����ɫ,
        �����������_��ɫ,
        ���������_��ɫ,
        ���������_��ɫ,
        ˯��_����,
        С�ͷ�ѩ����_��ɫ,
        С�ͷ�ѩ����_��ɫ,
        С�ͷ�ѩ����_�����ɫ,
        С�ͷ�ѩ����_��ɫ,
        �����Ŀ���Ů����_��ɫ,
        �����Ŀ���Ů����_��ɫ,
        �����Ŀ���Ů����_��ɫ,
        �����Ŀ���Ů����_��ɫ,
        �����Ŀ���Ů����_��ɫ,
        �����Ŀ���Ů����_��ɫ,
        ������֯��_�����ɫ,
        ������֯��_��ɫ,
        ���������֯��_��ɫ,
        ���������֯��_��ɫ,
        ���������֯��_��ɫ,
        ����������֯��_��ɫ,
        ������ĳ���_��ɫ,
        ������ĳ���_��ɫ,
        ������ĳ���_��ɫ,
        ���α�־T��_����ɫ,
        ���α�־T��_��ɫ,
        ���α�־T��_��ɫ,
        ���α�־T��_��ɫ,
    }

    public enum F6Socks
    {
        ��,
        ��ϥ��_��ɫ,
        ��ϥ��_��ɫ,
        ��ϥ��_��ɫ,
        ��ϥ��_��ɫ,
        ��ϥ��_��ɫ,
        ��ϥ��_��ɫ,
        ��ϥ��_��ɫ,
        ������,
        ��ϥ��_��ɫ,
        ��ϥ��_��ɫe,
        ��ϥ��_��ɫ,
        ��ϥ��_��ɫ,
        ��ϥ��_��ɫ,
        ��ϥ��_��ɫ,
        ��ϥ��_��ɫ,
        �����ƹ�ϥ��_��ɫ,
        �����ƹ�ϥ��_��ɫ,
        �����ƹ�ϥ��_ǳ��ɫ,
        ��˹�ϥ��_��ɫ,
        �Բʹ�ϥ��_��ɫ,
        ��׿�_��ɫ,
        ����_��ɫ,
        ����_��ɫ,
        ����_ǳ��ɫ,
        ����_����ɫ,
        ����_��ɫ,
        ����_��ɫ,
    }

    public enum F6Shoes
    {
        ��,
        ��ʿ��ѥ_��ɫ,
        ��ʿ��ѥ_��ɫ,
        ��ʿ��ѥ_��ɫ,
        ��ʿ��ѥ_��ɫ,
        ��ʿ��ѥ_��ɫ,
        ��ʿ��ѥ_��ɫ,
        ��֯��ѥ_��ɫ,
        ��֯��ѥ_��ɫ,
        ��������ѥ_��ɫ,
        ���,
        _11,
        �߰��˶�Ь_��ɫ,
        �߰��˶�Ь_��ɫ,
        �߰��˶�Ь_��ɫ,
        �߰��˶�Ь_��ɫ,
        С�д�ǳ��Ь_��ɫ,
        С�д�ǳ��Ь_��ɫ,
        ��ʿЬ_��ɫ,
        ��ʿЬ_��ɫ,
        ��ʿЬ_����ɫ,
        Ƥ���¸�Ь_��ɫ,
        Ƥ���¸�Ь_��ɫ,
        Ƥ���¸�Ь_��ɫ,
        Ƥ���¸�Ь_��ɫ,
        Ƥ���¸�Ь_��ɫ,
        Ƥ���¸�Ь_����ɫ,
    }

    public enum F6Hat
    {
        ��,
        ����ñ_����, // hacked
        ����ñ_��ɫ,
        ����ñ_��ɫ,
        ����ñ_��ɫ,
        ����ñ_����ɫ,
        ����ñ_��ɫ,
        ����ñ_��ɫ,
        ����ñ_ǳ��ɫ,
        ����ñ_��ɫ,
        ����ñ_����ɫ,
        ������ñ_��ɫ,
        ������ñ_��ɫ,
        ��ɫñ��_����, // hacked
        ��ɫñ��_����ɫ,
        ��ɫñ��_��ɫ,
        ��ɫñ��_��ɫ,
        ����־��̫��ñ_��ɫ,
        ����־��̫��ñ_��ɫ,
        Ѽ��ñ_����, // hacked
        Ѽ��ñ_��ɫ,
        Ѽ��ñ_��ɫ,
        Ѽ��ñ_��ɫ,
        ����Ѽ��ñ_��ɫ,
        ����Ѽ��ñ_��ɫ,
        ������ñ_����, // hacked
        ������ñ_��ɫ,
        ������ñ_��ɫ,
        ������ñ_��ɫ,
        ������ñ_��ɫ,
        ������ñ_��ɫ,
        ������ñ_��ɫ,
    }

    public enum F6HairStyle
    {
        ��,
        ����ͷ,
        ����,
        �з�,
        ����β,
        �̷�,
        ˫��β,
    }

    public enum F6HairStyleFront
    {
        ������,
        ����,
        б����,
    }

    public enum F6Dress
    {
        ��,
        Ů����������ȹ_��ɫ,
        ˫�ſ���������ȹ_����ɫ,
        ��������ȹ_��ɫ,
        ��������ȹ_��ɫ,
        ����ȹ��,
        ��˿��Ҷ������ȹ_ǳ��ɫ,
        �޿���������ȹ_��ɫ,
        �����ȹ_��ɫ,
        ����Ůʿ��װ_��ɫ,
        ����Ůʿ��װ_��ɫ,
    }

    public enum F6Bottom
    {
        ��,
        ţ������ȹ_��ɫ,
        ţ������ȹ_���ɫ,
        ţ������ȹ_��ɫ,
        ��Ҷ������ȹ_��ɫ,
        ��Ҷ������ȹ_��ɫ,
        ����ţ�п�_����ɫ,
        ����ţ�п�_��ɫ,
        ����ţ�п�_���ɫ,
        ����ţ�п�_��ɫ,
        ����ţ�п�_��ɫ,
        ����ţ�п�_��ɫ,
        ����ţ�п�_��ɫ,
        �ƶ�����ţ�п�_��ɫ,
        ˫ɫţ�п�_��ɫ,
        ˫ɫţ�п�_��ɫ,
        ˫ɫţ�п�_��ɫ,
        ˫ɫţ�п�_�����ɫ,
        ���߿���_��ɫ,
        ���߿���_��ɫ,
        ���߿���_��ɫ,
        ˯��_����,
        ����ȹ_��ɫ,
        ����ȹ_��ɫ,
        ����ȹ_��ɫ,
        ����ȹ_��ɫ,
        ��������ȹ_��ɫ,
        ��������ȹ_����ɫ,
        ��������ȹ_��ɫ,
        ���̰���ȹ_��ɫ,
        ���̰���ȹ_��ɫ,
        ����Ҷȹ_��ɫ,
        ����Ҷȹ_��ɫ,
        ����Ҷȹ_��ɫ1,
        ����Ҷȹ_��ɫ,
        ����Ҷȹ_��ɫ2,
        ţ�ж̿�_��ɫ,
        ţ�ж̿�_����ɫ,
        ţ�ж̿�_��ɫ,
        ţ�ж̿�_��ɫ,
        ţ�ж̿�_��ɫ,
        ţ�ж̿�_��ɫ,
        ţ�ж̿�_��ɫ,
        �ƶ�����ţ�ж̿�_��ɫ,
        ������֯��_���ɫ,
        ������֯��_��ɫ,
    }

    public enum F6Bag
    {
        ��,
        Ůʿ���_��ɫ,
        Ůʿ���_��ɫ,
        Ůʿ���_��ɫ,
        Ůʿ���_��ɫ,
        Ůʿ���_��ɫ,
        ��Ƥ���ư�_��ɫ,
        ��Ƥ���ư�_��ɫ,
        �д������_ǳ��ɫ,
        �д������_����ɫ,
        Ƥ��װ�ΰ�_��ɫ,
        Ƥ��װ�ΰ�_��ɫ,
        Ƥ��װ�ΰ�_��ɫ,
        Ƥ��װ�ΰ�_��ɫ,
        Ƥ��װ�ΰ�_��ɫ,
        ���հ���_��ɫ,
        ���հ���_��ɫ,
    }

    public enum F6Accessory
    {
        ��,
        ��ɫŦ�ۻ���_��ɫ,
        ��ɫŦ�ۻ���_��ɫ,
        ��ɫŦ�ۻ���_��ɫ,
        ��ɫŦ�ۻ���_��ɫ,
        ��ɫŦ�ۻ���_�����ɫ,
        �������_��ɫ,
        �������_ǳ��ɫ,
        �������_��ɫ,
        �������_����ɫ,
        ����̫����_��ɫ,
        ����̫����_��ɫ,
        ����̫����_��ɫ,
        ����̫����_��ɫ,
        ��������_��ɫ,
        ��������_��ɫ,
        ��������_��ɫ,
        ñ���öд�_��ɫ,
        ñ���öд�_��ɫ,
        ñ���öд�_��ɫ,
        ñ���öд�_��ɫ,
        ñ���öд�_ǳ��ɫ,
        ����_��ɫ,
        ����_��ɫ,
        ����_��ɫ,
    }
}
