using System;
using System.Windows.Forms;

using PKHeX.Core;
using PKHeX.Drawing.PokeSprite;
using PKHeX.WinForms.Controls;

namespace PKHeX.WinForms;

public partial class StatusBrowser : Form
{
    public bool WasChosen { get; private set; }
    public StatusCondition Choice { get; private set; }

    private readonly int StatusHeight;
    private const int StatusCount = 7;
    private int StatusWidth => StatusHeight;
    private int StatusBrowserWidth => StatusWidth * 2;

    public StatusBrowser()
    {
        InitializeComponent();
        StatusHeight = Drawing.PokeSprite.Properties.Resources.sicksleep.Height;
        NUD_Sleep = new NumericUpDown
        {
            Minimum = 1,
            Maximum = 7,
            Value = 1,
            Width = 40,
            TextAlign = HorizontalAlignment.Center,
            Margin = Padding.Empty,
            Padding = Padding.Empty,
        };

        Add(GetImage(StatusCondition.无, "无"));
        Add(GetImage(StatusCondition.睡眠1, "睡眠"), false);
        Add(NUD_Sleep);
        Add(GetImage(StatusCondition.中毒, "中毒"));
        Add(GetImage(StatusCondition.灼伤, "灼伤"));
        Add(GetImage(StatusCondition.麻痹, "麻痹"));
        Add(GetImage(StatusCondition.冰冻, "冰冻"));
        Add(GetImage(StatusCondition.剧毒, "剧毒"));

        Height = StatusCount * StatusHeight;
        Width = StatusBrowserWidth;
    }

    private readonly NumericUpDown NUD_Sleep;

    private void Add(Control c, bool flowBreak = true)
    {
        flp.Controls.Add(c);
        if (flowBreak)
            flp.SetFlowBreak(c, true);
    }

    public void LoadList(PKM pk)
    {
        var condition = (StatusCondition)pk.Status_Condition;
        NUD_Sleep.Value = Math.Max(1, (int)condition & 7);
        Text = condition.ToString();
    }

    private SelectablePictureBox GetImage(StatusCondition value, string name)
    {
        var img = value == 0
            ? Drawing.PokeSprite.Properties.Resources.sickfaint
            : value.GetStatusSprite();
        var pb = new SelectablePictureBox
        {
            Image = img,
            Name = name,
            AccessibleDescription = name,
            AccessibleName = name,
            AccessibleRole = AccessibleRole.Graphic,
            Margin = Padding.Empty,
            Padding = Padding.Empty,
            Width = StatusWidth,
            Height = StatusHeight,
        };

        pb.MouseEnter += (_, _) => Text = name;
        pb.Click += (_, _) =>
        {
            if (value is StatusCondition.睡眠1)
                value = (StatusCondition)NUD_Sleep.Value;
            SelectValue(value);
        };
        pb.KeyDown += (_, e) =>
        {
            if (e.KeyCode == Keys.Enter)
                SelectValue(value);
        };
        return pb;
    }

    private void SelectValue(StatusCondition value)
    {
        Choice = value;
        WasChosen = true;
        Close();
    }
}
