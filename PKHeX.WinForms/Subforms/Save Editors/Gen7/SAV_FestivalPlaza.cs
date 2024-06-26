using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using PKHeX.Core;
using PKHeX.Drawing.PokeSprite;
using static System.Buffers.Binary.BinaryPrimitives;

namespace PKHeX.WinForms;

public partial class SAV_FestivalPlaza : Form
{
    private readonly SaveFile Origin;
    private readonly SAV7 SAV;

    private int entry;

    public SAV_FestivalPlaza(SAV7 sav)
    {
        InitializeComponent();
        SAV = (SAV7)(Origin = sav).Clone();
        editing = true;
        entry = -1;
        typeMAX = SAV is SAV7USUM ? 0x7F : 0x7C;
        TB_PlazaName.Text = SAV.Festa.FestivalPlazaName;

        if (SAV is SAV7USUM)
        {
            PBs = [ppkx1, ppkx2, ppkx3];
            NUD_Trainers = [NUD_Trainer1, NUD_Trainer2, NUD_Trainer3];
            LoadBattleAgency();
        }
        else
        {
            TC_Editor.TabPages.Remove(Tab_BattleAgency);
        }

        if (Main.Unicode)
        {
            TB_OTName.Font = FontUtil.GetPKXFont();
        }

        var cc = SAV.Festa.FestaCoins;
        var cu = SAV.GetRecord(038);
        NUD_FC_Current.Value = Math.Min(cc, NUD_FC_Current.Maximum);
        NUD_FC_Used.Value = Math.Min(cu, NUD_FC_Used.Maximum);
        L_FC_CollectedV.Text = (cc + cu).ToString();
        string[] res;
        switch (Main.CurrentLanguage)
        {
            case "ja":
                res = [
                    "おじさんの きんのたま だからね！","かがくの ちからって すげー","1 2の …… ポカン！","おーす！ みらいの チャンピオン！","おお！ あんたか！","みんな げんきに なりましたよ！","とっても 幸せそう！","なんでも ないです","いあいぎりで きりますか？","レポートを かきこんでいます",
                    "…… ぼくも もう いかなきゃ！","ボンジュール！","バイビー！","ばか はずれです……","やけどなおしの よういは いいか！","ウー！ ハーッ！","ポケモンは たたかわせるものさ","ヤドランは そっぽを むいた！","マサラは まっしろ はじまりのいろ","10000こうねん はやいんだよ！","おーい！ まてー！ まつんじゃあ！","こんちわ！ ぼく ポケモン……！","っだと こらあ！","ぐ ぐーッ！ そんな ばかなーッ！","みゅう！","タチサレ…… タチサレ……",
                    "カイリュー はかいこうせん","どっちか 遊んでくれないか？","ぬいぐるみ かっておいたわよ","ひとのこと じろじろ みてんなよ","なんのことだか わかんない","みんな ポケモン やってるやん","きょうから 24時間 とっくんだ！","あたいが ホンモノ！","でんげきで いちころ……","スイクンを おいかけて 10ねん","かんどうが よみがえるよ！","われわれ ついに やりましたよー！","ヤドンのシッポを うるなんて……","ショオーッ!!","ギャーアアス!!","だいいっぽを ふみだした！",
                    "いちばん つよくて すごいんだよね","にくらしいほど エレガント！","そうぞうりょくが たりないよ","キミは ビッグウェーブ！","おまえさんには しびれた わい","なに いってんだろ…… てへへ……","ぬいぐるみ なんか かってないよ","ここで ゆっくり して おいき！","はじけろ！ ポケモン トレーナー！","はいが はいに はいった……","…できる！","ぶつかった かいすう 5かい！","たすけて おくれーっ!!","マボロシじま みえんのう……","ひゅああーん！","しゅわーん！",
                    "あつい きもち つたわってくる！","こいつが！ おれの きりふだ！","ひとりじめとか そういうの ダメよ！","ワーオ！ ぶんせきどーり！","ぱるぱるぅ!!!","グギュグバァッ!!!","ばっきん 100まんえん な！","オレ つよくなる……","ながれる 時間は とめられない！","ぜったいに お願いだからね","きみたちから はどうを かんじる！","あたしのポケモンに なにすんのさ！","リングは おれの うみ～♪","オレの おおごえの ひとりごとを","そう コードネームは ハンサム！","……わたしが まけるかも だと!?",
                    "やめたげてよぉ！","ブラボー！ スーパー ブラボー！","ボクは チャンピオンを こえる","オレは いまから いかるぜッ！","ライモンで ポケモン つよいもん","キミ むしポケモン つかいなよ","ストップ！","ひとよんで メダルおやじ！","トレーナーさんも がんばれよ！","おもうぞんぶん きそおーぜ！","プラズマズイ！","ワタクシを とめることは できない！","けいさんずみ ですとも！","ババリバリッシュ！","ンバーニンガガッ！","ヒュラララ！",
                    "お友達に なっちゃお♪","じゃあ みんな またねえ！","このひとたち ムチャクチャです……","トレーナーとは なにか しりたい","スマートに くずれおちるぜ","いのち ばくはつッ!!","いいんじゃない いいんじゃないの！","あれだよ あれ おみごとだよ！","ぜんりょくでいけー！ ってことよ！","おまちなさいな！","つまり グッド ポイント なわけ！","ざんねん ですが さようなら","にくすぎて むしろ 好きよ","この しれもの が！","イクシャア!!","イガレッカ!!",
                    "フェスサークル ランク 100！",
                ];
                break;
            default:
                const string musical8note = "♪";
                const string linedP = "₽"; //currency Ruble
                res = [ //source:UltraMoon
                    /* (SM)Pokémon House */"因为是大叔的金珠呀!","科学的力量,好厉害!","1, 2... 空!","喂- -,未来的冠军!","哦, 你啊!","大家都变得精力充沛了哦!","你的宝可梦看起来很幸福!","没什么.","要用居合斩切断吗?","正在写入记录...",
                     /* (SM)Kanto Tent */"...我也该走了!","甭主!","回见!","糟糕,弄错了...","你准备好灼伤药了吧!","唔- -!哈- -!","宝可梦是为对战而存在的!","呆壳兽把头转向了一旁!","真新是纯白,开始的颜色!","你还差着10000光年呢!","喂- -! 等等- -! 等一下!","你好哟!我是宝可梦...","这是要干嘛!","什,什么! 这不可能!","妙!","走开...走开...",
                    /* (SM)Joht Tent */"快龙,破坏光线!","哪个人来陪我玩玩啊?","我买好布偶了哦!","不要盯着别人看哦","不知道到底是怎么回事.","大家都在玩宝可梦.","今天开始24小时特训!","我是货真价实的!","用电击,一招解决...","我追寻水君10年了.","我被深深的感动了!","我们终于做到了- -!","竟然卖呆呆兽的尾巴...","啸- -!","奇- -亚斯!","迈出了第一步!",
                    /* (SM)Sinnoh Tent */"我感受到了你热切的心情!","这就是! 我的王牌!","独占之类的,那样是不行的哦!","哇哦!正如我所料!","帕路帕路!!!","咕叽咕叭!!!","罚金 " + linedP + "1000000!","我要变强...","时光流逝,永不停歇!","一定,拜托了!","我从你们身上感受到了波导!","想对我的宝可梦干嘛?!","擂台就是我的大海~！ " + musical8note,"我大声的自言自语","没错, 我的代号是帅哥!","...我也会输!?",
                    /* (SM)Unova Tent */"快住手啊!!","棒极了! 超棒!!","我要超越冠军.","我现在要发货了!","我的宝可梦在雷文很强很萌.","你用虫属性宝可梦吧.","站住!","人们叫我奖牌爷爷!","训练家也要加油哦!","来放手一搏吧!","等离子, 离远点!","没什么能阻拦我!","我早就料到了!","叭叭哩叭叭哩咻!","嗯叭尼~咔咔!","嗨啦啦啦!",
                    /* (SM)Kalos Tent */"让我们做朋友吧♪ " + musical8note,"那么我们下次再见啦!","这些人蛮不讲理...","我想知道所谓训练家是什么.","即便溃败也要潇洒倒下.","燃烧吧,我的生命!","不错吧!很不错吧!","那个哦!就是那个!卓越非凡啊!","请等一下!","也就是说,是优点!","很遗憾,再见.","打是亲骂是爱哦.","这个白痴!","伊克夏!!","伊噶莱!!",
                    "圆庆广场的等级是100!",
                ];
                break;
        }
        CLB_Phrases.Items.Clear();
        CLB_Phrases.Items.Add(res[^1], SAV.Festa.GetFestaPhraseUnlocked(106)); //add Lv100 before TentPhrases
        for (int i = 0; i < res.Length - 1; i++)
            CLB_Phrases.Items.Add(res[i], SAV.Festa.GetFestaPhraseUnlocked(i));

        DateTime dt = SAV.Festa.FestaDate ?? new DateTime(2000, 1, 1);
        CAL_FestaStartDate.Value = CAL_FestaStartTime.Value = dt;

        string[] res2 = ["004: 游乐项目开放", "008: 可购买店铺", "010: 可购买衣服", "020: 圆庆广场命名", "030: 追加购买内容", "040: 可更换音乐", "050: 获得主题-豪华", "060: 获得主题-童话", "070: 获得主题-单调", "100: 台词-左栏第一个", "当前等级"];
        CLB_Reward.Items.Clear();
        CLB_Reward.Items.Add(res2[^1], (CheckState)RewardState[SAV.Festa.GetFestPrizeReceived(10)]); //add CurrentRank before const-rewards
        for (int i = 0; i < res2.Length - 1; i++)
            CLB_Reward.Items.Add(res2[i], (CheckState)RewardState[SAV.Festa.GetFestPrizeReceived(i)]);

        for (int i = 0; i < JoinFesta7.FestaFacilityCount; i++)
            f[i] = SAV.Festa.GetFestaFacility(i);

        string[] res3 = ["遇见","离开","带路","失望"];
        CB_FacilityMessage.Items.Clear();
        CB_FacilityMessage.Items.AddRange(res3);
        string[] res5 =
        [
            "精英训练家" + gendersymbols[1],
            "精英训练家" + gendersymbols[0],
            "资深训练家" + gendersymbols[1],
            "资深训练家" + gendersymbols[0],
            "商务人士" + gendersymbols[0],
            "商务人士" + gendersymbols[1],
            "坏男孩",
            "坏女孩",
            "培育家" + gendersymbols[0],
            "培育家" + gendersymbols[1],
            "短裤小子",
            "迷你裙",
        ];
        CB_FacilityNPC.Items.Clear();
        CB_FacilityNPC.Items.AddRange(res5);
        string[] res6 = ["抽签屋", "恐怖屋", "礼品店", "美食摊", "气球摊", "占卜屋", "印染屋", "更换屋"];
        string[][] res7 = [
            ["大梦想", "黄金涌现", "狩猎宝物"],
            ["幽灵洞", "戏法空间", "奇异之光"],
            ["球球屋", "万万百货", "对战商店", "饮料屋", "药店"],
            ["神奇厨房", "对战餐桌", "亲密咖啡", "亲密小吃吧"],
            ["砰砰磅磅", "硬硬邦邦", "走走跑跑"],
            ["关都帐篷", "城都帐篷", "丰缘帐篷", "神奥帐篷", "合众帐篷", "卡洛斯帐篷", "宝可梦帐篷"],
            ["红色", "黄色", "绿色", "蓝色", "橙色", "靛色", "紫色", "粉色"],
            ["代理人对战"],
        ];

        CB_FacilityType.Items.Clear();
        for (int k = 0; k < RES_FacilityLevelType.Length - (SAV is SAV7USUM ? 0 : 1); k++) // Exchange is US/UM only
        {
            var arr = RES_FacilityLevelType[k];
            for (int j = 0; j < arr.Length; j++)
            {
                var x = res6[k];
                var y = res7[k];
                var name = $"{x} {y[j]}";

                var count = arr[j];
                if (count == 4)
                {
                    CB_FacilityType.Items.Add($"{name} 1");
                    CB_FacilityType.Items.Add($"{name} 3");
                    CB_FacilityType.Items.Add($"{name} 5");
                }
                else
                {
                    for (int i = 0; i < count; i++)
                        CB_FacilityType.Items.Add($"{name} {i + 1}");
                }
            }
        }

        string[] types = ["GTS", "Wonder Trade", "Battle Spot", "Festival Plaza", "mission", "lottery shop", "haunted house"];
        string[] lvl = ["+", "++", "+++"];
        CB_LuckyResult.Items.Clear();
        CB_LuckyResult.Items.Add("none");
        foreach (string type in types)
        {
            foreach (string lv in lvl)
                CB_LuckyResult.Items.Add($"{lv} {type}");
        }

        NUD_Rank.Value = SAV.Festa.FestaRank;
        LoadRankLabel(SAV.Festa.FestaRank);
        NUD_Messages = [NUD_MyMessageMeet, NUD_MyMessagePart, NUD_MyMessageMoved, NUD_MyMessageDissapointed];
        for (int i = 0; i < NUD_Messages.Length; i++)
            NUD_Messages[i].Value = SAV.Festa.GetFestaMessage(i);

        LB_FacilityIndex.SelectedIndex = 0;
        CB_FacilityMessage.SelectedIndex = 0;
        editing = false;

        entry = 0;
        LoadFacility();
    }

    private bool editing;
    private static ReadOnlySpan<byte> RewardState => [ 0, 2, 1 ]; // CheckState.Indeterminate <-> CheckState.Checked
    private readonly int typeMAX;
    private readonly FestaFacility[] f = new FestaFacility[JoinFesta7.FestaFacilityCount];
    private readonly string[] RES_Color = WinFormsTranslator.GetEnumTranslation<FestivalPlazaFacilityColor>(Main.CurrentLanguage);

    private readonly byte[][] RES_FacilityColor = //facility appearance
    [
        [0,1,2,3],//Lottery
        [4,0,5,3],//Haunted
        [1,0,5,3],//Goody
        [6,7,0,3],//Food
        [4,5,8,3],//Bouncy
        [0,1,2,3],//Fortune
        [0,7,8,4,5,1,9,10],//Dye
        [11,1,5,3],//Exchange
    ];

    private readonly byte[][] RES_FacilityLevelType = //3:123 4:135 5:12345
    [
        [5,5,5],
        [5,5,5],
        [3,5,3,3,3],
        [5,4,5,5],
        [5,5,5],
        [4,4,4,4,4,4,4],
        [4,4,4,4,4,4,4,4],
        [3],
    ];

    private int TypeIndexToType(int typeIndex)
    {
        if ((uint)typeIndex > typeMAX + 1)
            return -1;
        return typeIndex switch
        {
            < 0x0F => 0,
            < 0x1E => 1,
            < 0x2F => 2,
            < 0x41 => 3,
            < 0x50 => 4,
            < 0x65 => 5,
            < 0x7D => 6,
            _ => 7,
        };
    }

    private int GetColorCount(int type)
    {
        var colors = RES_FacilityColor;
        if (type >= 0 && type < colors.Length - (SAV is SAV7USUM ? 0 : 1))
            return colors[type].Length - 1;
        return 3;
    }

    private void LoadFacility()
    {
        editing = true;
        var facility = f[entry];
        CB_FacilityType.SelectedIndex =
            CB_FacilityType.Items.Count > facility.Type
                ? facility.Type
                : -1;
        int type = TypeIndexToType(CB_FacilityType.SelectedIndex);
        NUD_FacilityColor.Maximum = GetColorCount(type);
        NUD_FacilityColor.Value = Math.Min(facility.Color, NUD_FacilityColor.Maximum);
        if (type >= 0) LoadColorLabel(type);
        CB_LuckyResult.Enabled = CB_LuckyResult.Visible = L_LuckyResult.Visible = type == 5;
        NUD_Exchangable.Enabled = NUD_Exchangable.Visible = L_Exchangable.Visible = type == 7;
        switch (type)
        {
            case 5:
                int lucky = (facility.UsedLuckyPlace * 3) + facility.UsedLuckyRank - 3;
                if ((uint)lucky >= CB_LuckyResult.Items.Count)
                    lucky = 0;
                CB_LuckyResult.SelectedIndex = lucky;
                break;
            case 7:
                NUD_Exchangable.Value = facility.ExchangeLeftCount;
                break;
        }
        CB_FacilityNPC.SelectedIndex =
            CB_FacilityNPC.Items.Count > facility.NPC
                ? facility.NPC
                : 0;
        CHK_FacilityIntroduced.Checked = facility.IsIntroduced;
        TB_OTName.Text = facility.OriginalTrainerName;
        LoadOTlabel(facility.Gender);
        if (CB_FacilityMessage.SelectedIndex >= 0)
            LoadFMessage(CB_FacilityMessage.SelectedIndex);

        var obj = f[entry];
        TB_UsedFlags.Text = obj.UsedFlags.ToString("X8");
        TB_UsedStats.Text = obj.UsedRandStat.ToString("X8");
        TB_FacilityID.Text = Util.GetHexStringFromBytes(obj.TrainerFesID);
        editing = false;
    }

    private void Save()
    {
        SAV.Festa.SetFestaPhraseUnlocked(106, CLB_Phrases.GetItemChecked(0));
        for (int i = 1; i < CLB_Phrases.Items.Count; i++)
            SAV.Festa.SetFestaPhraseUnlocked(i - 1, CLB_Phrases.GetItemChecked(i));

        SAV.SetRecord(038, (int)NUD_FC_Used.Value);
        SAV.Festa.FestaCoins = (int)NUD_FC_Current.Value;
        SAV.Festa.FestaDate = new DateTime(CAL_FestaStartDate.Value.Year, CAL_FestaStartDate.Value.Month, CAL_FestaStartDate.Value.Day, CAL_FestaStartTime.Value.Hour, CAL_FestaStartTime.Value.Minute, CAL_FestaStartTime.Value.Second);

        SAV.Festa.SetFestaPrizeReceived(10, RewardState[(int)CLB_Reward.GetItemCheckState(0)]);
        for (int i = 1; i < CLB_Reward.Items.Count; i++)
            SAV.Festa.SetFestaPrizeReceived(i - 1, RewardState[(int)CLB_Reward.GetItemCheckState(i)]);

        SaveFacility();
        if (SAV is SAV7USUM)
            SaveBattleAgency();
    }

    private void LoadBattleAgency()
    {
        p[0] = SAV.GetStoredSlot(SAV.Data.AsSpan(0x6C200));
        p[1] = SAV.GetPartySlot(SAV.Data.AsSpan(0x6C2E8));
        p[2] = SAV.GetPartySlot(SAV.Data.AsSpan(0x6C420));
        LoadPictureBox();
        B_ImportParty.Visible = SAV.HasParty;
        CHK_Choosed.Checked = SAV.GetFlag(0x6C55E, 1);
        CHK_TrainerInvited.Checked = IsTrainerInvited();
        ushort valus = ReadUInt16LittleEndian(SAV.Data.AsSpan(0x6C55C));
        int grade = (valus >> 6) & 0x3F;
        NUD_Grade.Value = grade;
        int max = (Math.Min(49, grade) / 10 * 3) + 2;
        int defeated = valus >> 12;
        NUD_Defeated.Value = defeated > max ? max : defeated;
        NUD_Defeated.Maximum = max;
        NUD_DefeatMon.Value = ReadUInt16LittleEndian(SAV.Data.AsSpan(0x6C558));
        for (int i = 0; i < NUD_Trainers.Length; i++)
        {
            int j = GetSavData16(0x6C56C + (0x14 * i));
            var m = (int)NUD_Trainers[i].Maximum;
            NUD_Trainers[i].Value = (uint)j > m ? m : j;
        }
        B_AgentGlass.Enabled = (SAV.Fashion.Data[0xD0] & 1) == 0;
    }

    private void LoadPictureBox()
    {
        for (int i = 0; i < 3; i++)
            PBs[i].Image = p[i].Sprite(SAV, -1, -1, flagIllegal: true);
    }

    private readonly NumericUpDown[] NUD_Trainers = new NumericUpDown[3];
    private ushort GetSavData16(int offset) => ReadUInt16LittleEndian(SAV.Data.AsSpan(offset));
    private const ushort InvitedValue = 0x7DFF;
    private readonly PKM[] p = new PKM[3];
    private readonly PictureBox[] PBs = new PictureBox[3];
    private bool IsTrainerInvited() => (GetSavData16(0x6C3EE) & InvitedValue) == InvitedValue && (GetSavData16(0x6C526) & InvitedValue) == InvitedValue;

    private void SaveBattleAgency()
    {
        SAV.SetFlag(0x6C55E, 1, CHK_Choosed.Checked);
        if (IsTrainerInvited() != CHK_TrainerInvited.Checked)
        {
            WriteUInt16LittleEndian(SAV.Data.AsSpan(0x6C3EE), (ushort)(CHK_TrainerInvited.Checked ? GetSavData16(0x6C3EE) | InvitedValue : 0));
            WriteUInt16LittleEndian(SAV.Data.AsSpan(0x6C526), (ushort)(CHK_TrainerInvited.Checked ? GetSavData16(0x6C526) | InvitedValue : 0));
        }
        SAV.SetData(p[0].EncryptedBoxData, 0x6C200);
        SAV.SetData(p[1].EncryptedPartyData, 0x6C2E8);
        SAV.SetData(p[2].EncryptedPartyData, 0x6C420);

        var gradeDefeated = ((((int)NUD_Defeated.Value & 0xF) << 12) | (((int)NUD_Grade.Value & 0x3F) << 6) | (SAV.Data[0x6C55C] & 0x3F));
        WriteUInt16LittleEndian(SAV.Data.AsSpan(0x6C558), (ushort)NUD_DefeatMon.Value);
        WriteUInt16LittleEndian(SAV.Data.AsSpan(0x6C55C), (ushort)gradeDefeated);
        for (int i = 0; i < NUD_Trainers.Length; i++)
            WriteUInt16LittleEndian(SAV.Data.AsSpan(0x6C56C + (0x14 * i)), (ushort)NUD_Trainers[i].Value);
        SAV.Festa.FestivalPlazaName = TB_PlazaName.Text;
    }

    private void NUD_FC_ValueChanged(object sender, EventArgs e)
    {
        if (editing) return;
        L_FC_CollectedV.Text = (NUD_FC_Current.Value + NUD_FC_Used.Value).ToString(CultureInfo.InvariantCulture);
    }

    private void B_Cancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void B_Save_Click(object sender, EventArgs e)
    {
        Save();
        Origin.CopyChangesFrom(SAV);
        Close();
    }

    private void B_AllReceiveReward_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < CLB_Reward.Items.Count; i++)
            CLB_Reward.SetItemCheckState(i, CheckState.Checked);
    }

    private void B_AllReadyReward_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < CLB_Reward.Items.Count; i++)
            CLB_Reward.SetItemCheckState(i, CheckState.Indeterminate);
    }

    private void B_AllPhrases_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < CLB_Phrases.Items.Count; i++)
            CLB_Phrases.SetItemChecked(i, true);
    }

    private void TB_OTName_MouseDown(object sender, MouseEventArgs e)
    {
        TextBox tb = sender as TextBox ?? TB_OTName;
        // Special Character Form
        if (ModifierKeys != Keys.Control)
            return;

        var d = new TrashEditor(tb, SAV, SAV.Generation);
        d.ShowDialog();
        tb.Text = d.FinalString;
    }

    private readonly string[] gendersymbols = ["♂", "♀"];

    private void LoadOTlabel(int b)
    {
        Label_OTGender.Text = gendersymbols[b & 1];
        Label_OTGender.ForeColor = b == 1 ? Color.Red : Color.Blue;
    }

    private void Label_OTGender_Click(object sender, EventArgs e)
    {
        if (entry < 0)
            return;
        var b = f[entry].Gender;
        b ^= 1;
        f[entry].Gender = b;
        LoadOTlabel(b);
    }

    private void LoadFMessage(int fmIndex) => NUD_FacilityMessage.Value = f[entry].GetMessage(fmIndex);

    private void CB_FacilityMessage_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (editing)
            return;

        int fmIndex = CB_FacilityMessage.SelectedIndex;
        if (fmIndex < 0)
            return;
        if (entry < 0)
            return;

        editing = true;
        LoadFMessage(fmIndex);
        editing = false;
    }

    private void NUD_FacilityMessage_ValueChanged(object sender, EventArgs e)
    {
        if (editing)
            return;

        int fmIndex = CB_FacilityMessage.SelectedIndex;
        if (fmIndex < 0)
            return;
        if (entry < 0)
            return;

        f[entry].SetMessage(fmIndex, (ushort)NUD_FacilityMessage.Value);
    }

    private void HexTextBox_TextChanged(object sender, EventArgs e)
    {
        if (editing)
            return;
        if (entry < 0)
            return;

        string t = Util.GetOnlyHex(((TextBox)sender).Text);
        if (string.IsNullOrWhiteSpace(t))
            t = "0";
        int maxlen = sender == TB_FacilityID ? 12 << 1 : 4 << 1;
        if (t.Length > maxlen)
        {
            t = t[..maxlen];
            editing = true;
            ((TextBox)sender).Text = t;
            editing = false;
            System.Media.SystemSounds.Asterisk.Play();
        }
        if (sender == TB_UsedFlags)
        {
            f[entry].UsedFlags = Convert.ToUInt32(t, 16);
        }
        else if (sender == TB_UsedStats)
        {
            f[entry].UsedRandStat = Convert.ToUInt32(t, 16);
        }
        else if (sender == TB_FacilityID)
        {
            var updated = Util.GetBytesFromHexString(t.PadLeft(24, '0'));
            updated.CopyTo(f[entry].TrainerFesID);
        }
    }

    private void LoadColorLabel(int type) => L_FacilityColorV.Text = RES_Color[RES_FacilityColor[type][(int)NUD_FacilityColor.Value]];

    private void NUD_FacilityColor_ValueChanged(object sender, EventArgs e)
    {
        if (editing)
            return;
        if (entry < 0)
            return;
        f[entry].Color = (byte)NUD_FacilityColor.Value;
        int type = TypeIndexToType(CB_FacilityType.SelectedIndex);
        if (type < 0)
            return;

        editing = true;
        LoadColorLabel(type);
        editing = false;
    }

    private void CB_FacilityType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (editing)
            return;
        if (entry < 0)
            return;

        int typeIndex = CB_FacilityType.SelectedIndex;
        if (typeIndex < 0)
            return;

        var facility = f[entry];
        facility.Type = typeIndex;
        // reset color
        int type = TypeIndexToType(typeIndex);
        int colorCount = GetColorCount(type);
        editing = true;
        if (colorCount < NUD_FacilityColor.Value)
        {
            NUD_FacilityColor.Value = colorCount;
            facility.Color = colorCount;
        }
        NUD_FacilityColor.Maximum = colorCount;
        LoadColorLabel(type);
        // reset forms
        CB_LuckyResult.Enabled = CB_LuckyResult.Visible = L_LuckyResult.Visible = type == 5;
        NUD_Exchangable.Enabled = NUD_Exchangable.Visible = L_Exchangable.Visible = type == 7;
        switch (type)
        {
            case 5:
                int lucky = (facility.UsedLuckyPlace * 3) + facility.UsedLuckyRank - 3;
                if (lucky < 0 || lucky >= CB_LuckyResult.Items.Count) lucky = 0;
                CB_LuckyResult.SelectedIndex = lucky;
                break;
            case 7:
                NUD_Exchangable.Value = facility.ExchangeLeftCount;
                break;
        }
        editing = false;
    }

    private void SaveFacility()
    {
        if (entry < 0)
            return;
        var facility = f[entry];
        if (CB_FacilityType.SelectedIndex >= 0)
            facility.Type = CB_FacilityType.SelectedIndex;
        facility.Color = (byte)NUD_FacilityColor.Value;
        facility.OriginalTrainerName = TB_OTName.Text;
        if (CB_FacilityNPC.SelectedIndex >= 0)
            facility.NPC = CB_FacilityNPC.SelectedIndex;
        facility.IsIntroduced = CHK_FacilityIntroduced.Checked;
        int type = TypeIndexToType(facility.Type);
        facility.ExchangeLeftCount = type == 7 ? (byte)NUD_Exchangable.Value : 0;
        int lucky = CB_LuckyResult.SelectedIndex - 1;
        bool writeLucky = type == 5 && lucky >= 0;
        facility.UsedLuckyRank = writeLucky ? (lucky % 3) + 1 : 0;
        facility.UsedLuckyPlace = writeLucky ? (lucky / 3) + 1 : 0;
    }

    private void LoadRankLabel(int rank) => L_RankFC.Text = GetRankText(rank);

    private static string GetRankText(int rank)
    {
        if (rank < 1) return string.Empty;
        if (rank == 1) return "0 - 5";
        if (rank == 2) return "6 - 15";
        if (rank == 3) return "16 - 30";
        if (rank <= 10)
        {
            int i = ((rank - 1) * (rank - 2) * 5) + 1;
            return $"{i} - {i + ((rank - 1) * 10) - 1}";
        }
        if (rank <= 20)
        {
            int i = (rank * 100) - 649;
            return $"{i} - {i + 99}";
        }
        if (rank <= 70)
        {
            int j = (rank - 1) / 10;
            int i = (rank * ((j * 30) + 60)) - ((j * j * 150) + (j * 180) + 109); // 30 * (rank - 5 * j + 4) * (j + 2) - 349;
            return $"{i} - {i + (j * 30) + 59}";
        }
        if (rank <= 100)
        {
            int i = (rank * 270) - 8719;
            return $"{i} - {i + 269}";
        }
        if (rank <= 998)
        {
            int i = (rank * 300) - 11749;
            return $"{i} - {i + 299}";
        }
        if (rank == 999)
            return "287951 - ";
        return string.Empty;
    }

    private void NUD_Rank_ValueChanged(object sender, EventArgs e)
    {
        if (editing) return;
        int rank = (int)NUD_Rank.Value;
        SAV.Festa.FestaRank = (ushort)rank;
        LoadRankLabel(rank);
    }

    private readonly NumericUpDown[] NUD_Messages;

    private void NUD_MyMessage_ValueChanged(object sender, EventArgs e)
    {
        if (editing)
            return;

        int mmIndex = Array.IndexOf(NUD_Messages, (NumericUpDown)sender);
        if (mmIndex < 0)
            return;

        SAV.Festa.SetFestaMessage(mmIndex, (ushort)((NumericUpDown)sender).Value);
    }

    private void CHK_FacilityIntroduced_CheckedChanged(object sender, EventArgs e)
    {
        if (editing)
            return;
        if (entry < 0)
            return;

        f[entry].IsIntroduced = CHK_FacilityIntroduced.Checked;
    }

    private void TB_OTName_TextChanged(object sender, EventArgs e)
    {
        if (editing)
            return;
        if (entry < 0)
            return;

        f[entry].OriginalTrainerName = TB_OTName.Text;
    }

    private void LB_FacilityIndex_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (editing)
            return;

        SaveFacility();
        entry = LB_FacilityIndex.SelectedIndex;
        if (entry < 0)
            return;
        LoadFacility();
    }

    private void LB_FacilityIndex_DrawItem(object? sender, DrawItemEventArgs? e)
    {
        if (e is null || sender is not ListBox lb)
            return;
        e.DrawBackground();

        var font = e.Font ?? Font;
        e.Graphics.DrawString(lb.Items[e.Index].ToString(), font, new SolidBrush(e.ForeColor), new RectangleF(e.Bounds.X, e.Bounds.Y + ((e.Bounds.Height - 12) >> 1), e.Bounds.Width, 12));
        e.DrawFocusRectangle();
    }

    private void B_DelVisitor_Click(object sender, EventArgs e)
    {
        if (entry < 0)
            return;
        var facility = f[entry];
        // there is an unknown value when not introduced...no reproducibility, just mistake?
        if (facility.IsIntroduced)
            facility.ClearTrainerFesID();
        facility.IsIntroduced = false;
        facility.OriginalTrainerName = string.Empty;
        facility.Gender = 0;
        for (int i = 0; i < 4; i++)
            facility.SetMessage(i, 0);
        LoadFacility();
    }

    private string GetSpeciesNameFromPKM(PKM pk) => SpeciesName.GetSpeciesName(pk.Species, SAV.Language);

    private void B_ImportParty_Click(object sender, EventArgs e)
    {
        if (!SAV.HasParty)
            return;
        var party = SAV.PartyData;
        string msg = string.Empty;
        for (int i = 0; i < 3; i++)
        {
            if (i < party.Count)
                msg += $"{Environment.NewLine}{GetSpeciesNameFromPKM(p[i])} -> {GetSpeciesNameFromPKM(party[i])}";
            else
                msg += $"{Environment.NewLine}未更换: {GetSpeciesNameFromPKM(p[i])}";
        }
        if (DialogResult.Yes != WinFormsUtil.Prompt(MessageBoxButtons.YesNo, "保存宝可梦?", msg))
            return;

        for (int i = 0, min = Math.Min(3, party.Count); i < min; i++)
            p[i] = party[i];
        LoadPictureBox();
    }

    private void MnuSave_Click(object sender, EventArgs e)
    {
        var pb = WinFormsUtil.GetUnderlyingControl<PictureBox>(sender);
        int i = Array.IndexOf(PBs, pb);
        if (i < 0)
            return;
        WinFormsUtil.SavePKMDialog(p[i]);
    }

    private void NUD_Grade_ValueChanged(object sender, EventArgs e)
    {
        if (editing)
            return;
        int max = (Math.Min(49, (int)NUD_Grade.Value) / 10 * 3) + 2;
        editing = true;
        if (NUD_Defeated.Value > max)
            NUD_Defeated.Value = max;
        NUD_Defeated.Maximum = max;
        editing = false;
    }

    private void NUD_Exchangable_ValueChanged(object sender, EventArgs e)
    {
        if (editing)
            return;
        if (entry < 0)
            return;
        f[entry].ExchangeLeftCount = (byte)NUD_Exchangable.Value;
    }

    private void CB_LuckyResult_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (editing)
            return;
        if (entry < 0)
            return;
        int lucky = CB_LuckyResult.SelectedIndex;
        if (lucky-- < 0)
            return;
        // both 0 if "none"
        f[entry].UsedLuckyRank = lucky < 0 ? 0 : (lucky % 3) + 1;
        f[entry].UsedLuckyPlace = lucky < 0 ? 0 : (lucky / 3) + 1;
    }

    private void B_AgentGlass_Click(object sender, EventArgs e)
    {
        if (NUD_Grade.Value < 30 && DialogResult.Yes != WinFormsUtil.Prompt(MessageBoxButtons.YesNo, "代理人墨镜是30级的奖励.", "继续?"))
            return;
        SAV.Fashion.GiveAgentSunglasses();
        B_AgentGlass.Enabled = false;
        System.Media.SystemSounds.Asterisk.Play();
    }
}
