using System;
using System.Windows.Forms;
using PKHeX.Core;

namespace PKHeX.WinForms;

public partial class SAV_ZygardeCell : Form
{
    private readonly SaveFile Origin;
    private readonly SAV7 SAV;

    public SAV_ZygardeCell(SaveFile sav)
    {
        InitializeComponent();
        WinFormsUtil.TranslateInterface(this, Main.CurrentLanguage);
        SAV = (SAV7)(Origin = sav).Clone();

        // Constants @ 0x1C00
        // Cell Data @ 0x1D8C
        // Use constants 0x18C/2 = 198 thru +95
        ushort[] constants = SAV.GetAllEventWork();
        var cells = constants.AsSpan(celloffset, CellCount);

        int cellCount = constants[cellstotal];
        int cellCollected = constants[cellscollected];

        NUD_Cells.Value = cellCount;
        NUD_Collected.Value = cellCollected;

        var combo = (DataGridViewComboBoxColumn)dgv.Columns[2];
        combo.Items.AddRange(states); // add only the Names
        dgv.Columns[0].ValueType = typeof(int);

        // Populate Grid
        dgv.Rows.Add(CellCount);
        var locations = SAV is SAV7SM ? locationsSM : locationsUSUM;
        for (int i = 0; i < CellCount; i++)
        {
            if (cells[i] > 2)
                throw new IndexOutOfRangeException("Unable to find cell index.");

            dgv.Rows[i].Cells[0].Value = (i + 1);
            dgv.Rows[i].Cells[1].Value = locations[i];
            dgv.Rows[i].Cells[2].Value = states[cells[i]];
        }
    }

    private const int cellstotal = 161;
    private const int cellscollected = 169;
    private const int celloffset = 0xC6;
    private int CellCount => SAV is SAV7USUM ? 100 : 95;
    private static readonly string[] states = { "无", "可获得", "已获得" };

    private void B_Save_Click(object sender, EventArgs e)
    {
        ushort[] constants = SAV.GetAllEventWork();
        for (int i = 0; i < CellCount; i++)
        {
            string str = (string)dgv.Rows[i].Cells[2].Value;
            int val = Array.IndexOf(states, str);
            if (val < 0)
                throw new IndexOutOfRangeException("Unable to find cell index.");

            constants[celloffset + i] = (ushort)val;
        }

        constants[cellstotal] = (ushort)NUD_Cells.Value;
        constants[cellscollected] = (ushort)NUD_Collected.Value;
        if (SAV is SAV7USUM)
            SAV.SetRecord(72, (int)NUD_Collected.Value);

        SAV.SetAllEventWork(constants);
        Origin.CopyChangesFrom(SAV);

        Close();
    }

    private void B_Cancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void B_GiveAll_Click(object sender, EventArgs e)
    {
        int added = 0;
        for (int i = 0; i < dgv.RowCount; i++)
        {
            if (Array.IndexOf(states, (string)dgv.Rows[i].Cells[2].Value) != 2) // Not Collected
                added++;
            dgv.Rows[i].Cells[2].Value = states[2];
        }

        NUD_Collected.Value += added;
        if (SAV is not SAV7USUM)
            NUD_Cells.Value += added;

        System.Media.SystemSounds.Asterisk.Play();
    }

    #region locations -- lazy

    private static readonly string[] locationsSM =
    {
        "葱郁洞窟 - 考验之地",
        "战争遗迹 - 室外",
        "1号道路 (昼)",
        "3号道路",
        "3号道路 (昼)",
        "卡拉蔚湾",
        "好奥乐墓园",
        "2号道路",
        "1号道路 - 训练师学校 (夜)",
        "好奥乐市 - 商业区",
        "1号道路 - 郊区",
        "好奥乐市 - 商业区 (夜)",
        "1号道路",
        "利利小镇 (夜)",
        "4号道路",
        "欧哈纳牧场 (夜)",
        "欧哈纳牧场 (昼)",
        "维拉火山公园 - 山顶",
        "树荫丛林 - 洞穴内",
        "7号道路",
        "阿卡拉岛郊外",
        "皇家大道 (昼)",
        "皇家大道 (夜)",
        "可霓可市 (夜)",
        "慷待市 (夜)",
        "8号道路",
        "8号道路 (昼)",
        "5号道路",
        "豪诺豪诺海滩 (昼)",
        "慷待市",
        "地鼠隧道",
        "豪诺豪诺海滩",
        "马利埃庭园",
        "马利埃静市 - 居民中心 (夜)",
        "马利埃静市 (昼)",
        "马利埃静市 - 市郊海角 (昼)",
        "11号道路 (夜)",
        "12号道路 (昼)",
        "12号道路",
        "乌拉乌拉海滩 (夜)",
        "火特力山",
        "13号道路",
        "哈伊纳沙漠",
        "丰收遗迹 - 室外",
        "14号道路",
        "14号道路 (夜)",
        "卡璞村",
        "15号道路",
        "以太之家 (昼)",
        "乌拉乌拉花园 - 木板道",
        "16号道路 (昼)",
        "乌拉乌拉花园 - 草丛",
        "17号道路 - 围墙",
        "17号道路 - 暗礁",
        "魄镇 (夜)",
        "10号道路 (昼)",
        "辉克拉尼天文台 (夜)",
        "拉纳基拉山 - 山腰",
        "拉纳基拉山 - 高山腰",
        "乌拉乌拉海滩 (昼)",
        "13号道路 (夜)",
        "魄镇 (昼)",
        "海洋居民之村 - 吼吼鲸船",
        "海洋居民之村 - 空地",
        "波尼原野 (昼)",
        "波尼原野 (夜)",
        "波尼原野",
        "波尼古道 - 井附近 (昼)",
        "波尼古道 (夜)",
        "波尼鼓浪岩岸 (昼)",
        "彼岸遗迹",
        "波尼树林 - 山脚",
        "波尼树林 - 灌木胖",
        "波尼旷野 (昼)",
        "波尼旷野 (夜)",
        "波尼旷野",
        "波尼花园",
        "波尼海岸 (夜)",
        "波尼海岸",
        "波尼险路 - 桥上",
        "波尼险路 - 资深训练家胖",
        "终结洞穴 - 1层 (昼)",
        "终结洞穴 - 负1层 (夜)",
        "波尼大峡谷 - 3层",
        "波尼大峡谷 - 2层",
        "波尼大峡谷 - 顶层",
        "波尼大峡谷 - 洞内",
        "波尼古道 - 砖墙 (昼)",
        "波尼鼓浪岩岸 (夜)",
        "终结洞穴 - 负1层",
        "以太基金会 负2层 - 右边走廊",
        "以太基金会 1层 - 屋外右方",
        "以太基金会 1层 - 屋外 (昼)",
        "以太基金会 1层 - 入口 (夜)",
        "以太基金会 1层 - 主建筑",
    };

    private static readonly string[] locationsUSUM =
    {
        "好奥乐市 (商业区) - 美发沙龙 (屋外)",
        "好奥乐市 (商业区) - 马拉萨达连锁店 (屋外)",
        "好奥乐市 (商业区) - 伊利马家 (2楼)",
        "马利埃静市 - 图书馆 (1楼)",
        "好奥乐市 (港口区) - 码头",
        "2号道路 - 东南方的房子",
        "好奥乐市 (商业区) - 伊利马家 (屋外)",
        "好奥乐市 (商业区) - 市政府",
        "慷待市 - 听涛酒店 (3楼)",
        "2号道路 - 树果园的房子",
        "2号道路 - 树果园的房子 (屋外)",
        "皇家大道 - 东北方",
        "好奥乐市 (商业区) - 宝可梦中心 (屋外)",
        "皇家大道 - 南方",
        "辉克拉尼天文台 - 房间",
        "辉克拉尼天文台 - 前台",
        "好奥乐市 (商业区) - 市政府 (屋外)",
        "可霓可市 - 丽姿的宝石店 (2楼)",
        "慷待市 - 冲浪板店 (屋外)",
        "魄镇 - 西南方",
        "豪诺豪诺度假地大厅 - 西南方 水池",
        "好奥乐市 (商业区) - 警察局 西北方",
        "好奥乐市 (港口区) - 乘船处 (屋外)",
        "2号道路 - 东南方的房子 (屋外)",
        "2号道路 - 宝可梦中心 (屋外)",
        "慷待市 - 西方",
        "慷待市 - 听涛酒店 西方 (屋外)",
        "慷待市 - 听涛酒店 (屋外)",
        "慷待市 - 空间研究所 东方 (屋外)",
        "慷待市 - 空间研究所 南方 (屋外)",
        "慷待市 - Game Freak工作室",
        "辉克拉尼天文台 - 死胡同",
        "慷待市 - Game Freak工作室大楼 (3楼)",
        "慷待市 - 空间研究所",
        "慷待市 - 听涛酒店 (1楼)",
        "皇家巨蛋 - 2楼",
        "欧哈纳镇 - 西方",
        "欧哈纳镇 - 卡奇家 (1楼)",
        "欧哈纳镇 - 卡奇家 (2楼)",
        "欧哈纳牧场 - 西北方",
        "欧哈纳牧场 - 东南方",
        "豪诺豪诺海滩",
        "豪诺豪诺度假地 - 南方",
        "豪诺豪诺度假地 - 北方",
        "可霓可市 灯塔 (通过地鼠洞穴)",
        "皇家巨蛋 - 1楼",
        "8号道路 - 以太基地 (屋外)",
        "8号道路 - 化石复原所 (屋外)",
        "可霓可市 - 西方",
        "可霓可市 - 餐馆 (1楼)",
        "利利小镇 - 西南方",
        "好奥乐市 (商业区) - 伊利马家 泳池",
        "维拉火山公园 - 石堆后面",
        "5号道路 - 宝可梦中心南方",
        "豪诺豪诺海滩 - 沙丘娃背后",
        "马利埃静市 (市郊海角) - 回收工厂 (屋外)",
        "马利埃静市 - 乘船处 (屋外)",
        "马利埃静市 - 时装店 (屋外)",
        "马利埃静市 - 美发沙龙 (屋外)",
        "16道路 - 以太基地 (屋外)",
        "火特力山 - 发电厂 (屋外)",
        "马利埃静市 - 图书馆 (2F)",
        "马利埃庭园 - 东北方",
        "马利埃静市 - 居民中心",
        "辉克拉尼天文台 - 屋外",
        "辉克拉尼山",
        "火特力山 - 发电厂",
        "13道路",
        "14道路 - 超值超市旧址",
        "14道路 - 南方",
        "15道路 - 小岛 冲浪板店 (屋外)",
        "17道路 - 警察局 (屋外)",
        "17道路 - 警察局",
        "魄镇 - 宝可梦中心 (屋外)",
        "椰蛋树岛 - 下方石头",
        "魄镇 - 可疑宅邸 东方 (屋外)",
        "魄镇 - 宝可梦中心",
        "魄镇 - 可疑宅邸 (1楼)",
        "13道路 - 汽车旅馆 (屋外)",
        "魄镇 - 可疑宅邸 2F (屋外)",
        "17道路 - 魄镇 南方",
        "乌拉乌拉花园",
        "魄镇 - 可疑宅邸 西方 石堆 (屋外) 1",
        "魄镇 - 可疑宅邸 西方 石堆 (屋外) 2",
        "魄镇 - 可疑宅邸 西方 石堆 (屋外) 3",
        "海洋居民之村 - 东南方 鲶鱼王船 (茉莉家) (屋外)",
        "海洋居民之村 - 西南方 猎斑鱼船",
        "海洋居民之村 - 西南方 猎斑鱼船 (屋外)",
        "海洋居民之村 - 东南方 鲶鱼王船 (茉莉家)",
        "海洋居民之村 - 西方 吼鲸王船 (餐馆)",
        "海洋居民之村 - 东方 大钢蛇船",
        "波尼原野 - 东南方",
        "波尼古道 - 哈普乌家 (厨房)",
        "海洋居民之村 - 东北方",
        "波尼古道 - 哈普乌家 (睡房)",
        "波尼古道 - 西南方",
        "波尼古道 - 哈普乌家 (庭院)",
        "波尼古道 - 哈普乌家 (屋外 背后)",
        "波尼古道 - 东北方",
        "对战树 - 入口",
    };

    #endregion
}
