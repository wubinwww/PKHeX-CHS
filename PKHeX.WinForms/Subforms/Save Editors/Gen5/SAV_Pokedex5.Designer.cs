namespace PKHeX.WinForms
{
    partial class SAV_Pokedex5
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            B_Cancel = new System.Windows.Forms.Button();
            LB_Species = new System.Windows.Forms.ListBox();
            CHK_P1 = new System.Windows.Forms.CheckBox();
            CHK_P2 = new System.Windows.Forms.CheckBox();
            CHK_P3 = new System.Windows.Forms.CheckBox();
            CHK_P4 = new System.Windows.Forms.CheckBox();
            CHK_P5 = new System.Windows.Forms.CheckBox();
            CHK_P6 = new System.Windows.Forms.CheckBox();
            CHK_P7 = new System.Windows.Forms.CheckBox();
            CHK_P8 = new System.Windows.Forms.CheckBox();
            CHK_P9 = new System.Windows.Forms.CheckBox();
            CHK_L7 = new System.Windows.Forms.CheckBox();
            CHK_L6 = new System.Windows.Forms.CheckBox();
            CHK_L5 = new System.Windows.Forms.CheckBox();
            CHK_L4 = new System.Windows.Forms.CheckBox();
            CHK_L3 = new System.Windows.Forms.CheckBox();
            CHK_L2 = new System.Windows.Forms.CheckBox();
            CHK_L1 = new System.Windows.Forms.CheckBox();
            L_goto = new System.Windows.Forms.Label();
            CB_Species = new System.Windows.Forms.ComboBox();
            B_GiveAll = new System.Windows.Forms.Button();
            B_Save = new System.Windows.Forms.Button();
            B_Modify = new System.Windows.Forms.Button();
            GB_Language = new System.Windows.Forms.GroupBox();
            GB_Encountered = new System.Windows.Forms.GroupBox();
            GB_Owned = new System.Windows.Forms.GroupBox();
            GB_Displayed = new System.Windows.Forms.GroupBox();
            modifyMenu = new System.Windows.Forms.ContextMenuStrip(components);
            mnuSeenNone = new System.Windows.Forms.ToolStripMenuItem();
            mnuSeenAll = new System.Windows.Forms.ToolStripMenuItem();
            mnuCaughtNone = new System.Windows.Forms.ToolStripMenuItem();
            mnuCaughtAll = new System.Windows.Forms.ToolStripMenuItem();
            mnuComplete = new System.Windows.Forms.ToolStripMenuItem();
            B_ModifyForms = new System.Windows.Forms.Button();
            L_FormDisplayed = new System.Windows.Forms.Label();
            CLB_FormDisplayed = new System.Windows.Forms.CheckedListBox();
            L_FormsSeen = new System.Windows.Forms.Label();
            CLB_FormsSeen = new System.Windows.Forms.CheckedListBox();
            modifyMenuForms = new System.Windows.Forms.ContextMenuStrip(components);
            mnuFormNone = new System.Windows.Forms.ToolStripMenuItem();
            mnuForm1 = new System.Windows.Forms.ToolStripMenuItem();
            mnuFormAll = new System.Windows.Forms.ToolStripMenuItem();
            CHK_NationalDexUnlocked = new System.Windows.Forms.CheckBox();
            TB_PID = new System.Windows.Forms.TextBox();
            L_Spinda = new System.Windows.Forms.Label();
            CHK_NationalDexActive = new System.Windows.Forms.CheckBox();
            GB_Language.SuspendLayout();
            GB_Encountered.SuspendLayout();
            GB_Owned.SuspendLayout();
            GB_Displayed.SuspendLayout();
            modifyMenu.SuspendLayout();
            modifyMenuForms.SuspendLayout();
            SuspendLayout();
            // 
            // B_Cancel
            // 
            B_Cancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            B_Cancel.Location = new System.Drawing.Point(538, 250);
            B_Cancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            B_Cancel.Name = "B_Cancel";
            B_Cancel.Size = new System.Drawing.Size(93, 27);
            B_Cancel.TabIndex = 0;
            B_Cancel.Text = "Cancel";
            B_Cancel.UseVisualStyleBackColor = true;
            B_Cancel.Click += B_Cancel_Click;
            // 
            // LB_Species
            // 
            LB_Species.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            LB_Species.FormattingEnabled = true;
            LB_Species.Location = new System.Drawing.Point(14, 46);
            LB_Species.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            LB_Species.Name = "LB_Species";
            LB_Species.Size = new System.Drawing.Size(151, 225);
            LB_Species.TabIndex = 2;
            LB_Species.SelectedIndexChanged += ChangeLBSpecies;
            // 
            // CHK_P1
            // 
            CHK_P1.AutoSize = true;
            CHK_P1.Location = new System.Drawing.Point(7, 16);
            CHK_P1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            CHK_P1.Name = "CHK_P1";
            CHK_P1.Size = new System.Drawing.Size(68, 21);
            CHK_P1.TabIndex = 3;
            CHK_P1.Text = "Owned";
            CHK_P1.UseVisualStyleBackColor = true;
            // 
            // CHK_P2
            // 
            CHK_P2.AutoSize = true;
            CHK_P2.Location = new System.Drawing.Point(7, 17);
            CHK_P2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            CHK_P2.Name = "CHK_P2";
            CHK_P2.Size = new System.Drawing.Size(56, 21);
            CHK_P2.TabIndex = 4;
            CHK_P2.Text = "Male";
            CHK_P2.UseVisualStyleBackColor = true;
            CHK_P2.Click += ChangeEncountered;
            // 
            // CHK_P3
            // 
            CHK_P3.AutoSize = true;
            CHK_P3.Location = new System.Drawing.Point(7, 33);
            CHK_P3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            CHK_P3.Name = "CHK_P3";
            CHK_P3.Size = new System.Drawing.Size(68, 21);
            CHK_P3.TabIndex = 5;
            CHK_P3.Text = "Female";
            CHK_P3.UseVisualStyleBackColor = true;
            CHK_P3.Click += ChangeEncountered;
            // 
            // CHK_P4
            // 
            CHK_P4.AutoSize = true;
            CHK_P4.Location = new System.Drawing.Point(7, 50);
            CHK_P4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            CHK_P4.Name = "CHK_P4";
            CHK_P4.Size = new System.Drawing.Size(90, 21);
            CHK_P4.TabIndex = 6;
            CHK_P4.Text = "Shiny Male";
            CHK_P4.UseVisualStyleBackColor = true;
            CHK_P4.Click += ChangeEncountered;
            // 
            // CHK_P5
            // 
            CHK_P5.AutoSize = true;
            CHK_P5.Location = new System.Drawing.Point(7, 66);
            CHK_P5.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            CHK_P5.Name = "CHK_P5";
            CHK_P5.Size = new System.Drawing.Size(102, 21);
            CHK_P5.TabIndex = 7;
            CHK_P5.Text = "Shiny Female";
            CHK_P5.UseVisualStyleBackColor = true;
            CHK_P5.Click += ChangeEncountered;
            // 
            // CHK_P6
            // 
            CHK_P6.AutoSize = true;
            CHK_P6.Location = new System.Drawing.Point(6, 16);
            CHK_P6.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            CHK_P6.Name = "CHK_P6";
            CHK_P6.Size = new System.Drawing.Size(56, 21);
            CHK_P6.TabIndex = 8;
            CHK_P6.Text = "Male";
            CHK_P6.UseVisualStyleBackColor = true;
            CHK_P6.Click += ChangeDisplayed;
            // 
            // CHK_P7
            // 
            CHK_P7.AutoSize = true;
            CHK_P7.Location = new System.Drawing.Point(6, 32);
            CHK_P7.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            CHK_P7.Name = "CHK_P7";
            CHK_P7.Size = new System.Drawing.Size(68, 21);
            CHK_P7.TabIndex = 9;
            CHK_P7.Text = "Female";
            CHK_P7.UseVisualStyleBackColor = true;
            CHK_P7.Click += ChangeDisplayed;
            // 
            // CHK_P8
            // 
            CHK_P8.AutoSize = true;
            CHK_P8.Location = new System.Drawing.Point(6, 48);
            CHK_P8.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            CHK_P8.Name = "CHK_P8";
            CHK_P8.Size = new System.Drawing.Size(90, 21);
            CHK_P8.TabIndex = 10;
            CHK_P8.Text = "Shiny Male";
            CHK_P8.UseVisualStyleBackColor = true;
            CHK_P8.Click += ChangeDisplayed;
            // 
            // CHK_P9
            // 
            CHK_P9.AutoSize = true;
            CHK_P9.Location = new System.Drawing.Point(6, 65);
            CHK_P9.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            CHK_P9.Name = "CHK_P9";
            CHK_P9.Size = new System.Drawing.Size(102, 21);
            CHK_P9.TabIndex = 11;
            CHK_P9.Text = "Shiny Female";
            CHK_P9.UseVisualStyleBackColor = true;
            CHK_P9.Click += ChangeDisplayed;
            // 
            // CHK_L7
            // 
            CHK_L7.AutoSize = true;
            CHK_L7.Location = new System.Drawing.Point(21, 144);
            CHK_L7.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            CHK_L7.Name = "CHK_L7";
            CHK_L7.Size = new System.Drawing.Size(69, 21);
            CHK_L7.TabIndex = 19;
            CHK_L7.Text = "Korean";
            CHK_L7.UseVisualStyleBackColor = true;
            // 
            // CHK_L6
            // 
            CHK_L6.AutoSize = true;
            CHK_L6.Location = new System.Drawing.Point(21, 125);
            CHK_L6.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            CHK_L6.Name = "CHK_L6";
            CHK_L6.Size = new System.Drawing.Size(72, 21);
            CHK_L6.TabIndex = 18;
            CHK_L6.Text = "Spanish";
            CHK_L6.UseVisualStyleBackColor = true;
            // 
            // CHK_L5
            // 
            CHK_L5.AutoSize = true;
            CHK_L5.Location = new System.Drawing.Point(21, 105);
            CHK_L5.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            CHK_L5.Name = "CHK_L5";
            CHK_L5.Size = new System.Drawing.Size(73, 21);
            CHK_L5.TabIndex = 17;
            CHK_L5.Text = "German";
            CHK_L5.UseVisualStyleBackColor = true;
            // 
            // CHK_L4
            // 
            CHK_L4.AutoSize = true;
            CHK_L4.Location = new System.Drawing.Point(21, 85);
            CHK_L4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            CHK_L4.Name = "CHK_L4";
            CHK_L4.Size = new System.Drawing.Size(61, 21);
            CHK_L4.TabIndex = 16;
            CHK_L4.Text = "Italian";
            CHK_L4.UseVisualStyleBackColor = true;
            // 
            // CHK_L3
            // 
            CHK_L3.AutoSize = true;
            CHK_L3.Location = new System.Drawing.Point(21, 66);
            CHK_L3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            CHK_L3.Name = "CHK_L3";
            CHK_L3.Size = new System.Drawing.Size(65, 21);
            CHK_L3.TabIndex = 15;
            CHK_L3.Text = "French";
            CHK_L3.UseVisualStyleBackColor = true;
            // 
            // CHK_L2
            // 
            CHK_L2.AutoSize = true;
            CHK_L2.Location = new System.Drawing.Point(21, 46);
            CHK_L2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            CHK_L2.Name = "CHK_L2";
            CHK_L2.Size = new System.Drawing.Size(68, 21);
            CHK_L2.TabIndex = 14;
            CHK_L2.Text = "English";
            CHK_L2.UseVisualStyleBackColor = true;
            // 
            // CHK_L1
            // 
            CHK_L1.AutoSize = true;
            CHK_L1.Location = new System.Drawing.Point(21, 27);
            CHK_L1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            CHK_L1.Name = "CHK_L1";
            CHK_L1.Size = new System.Drawing.Size(81, 21);
            CHK_L1.TabIndex = 13;
            CHK_L1.Text = "Japanese";
            CHK_L1.UseVisualStyleBackColor = true;
            // 
            // L_goto
            // 
            L_goto.AutoSize = true;
            L_goto.Location = new System.Drawing.Point(14, 18);
            L_goto.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            L_goto.Name = "L_goto";
            L_goto.Size = new System.Drawing.Size(39, 17);
            L_goto.TabIndex = 20;
            L_goto.Text = "goto:";
            // 
            // CB_Species
            // 
            CB_Species.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            CB_Species.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            CB_Species.DropDownWidth = 95;
            CB_Species.FormattingEnabled = true;
            CB_Species.Items.AddRange(new object[] { "0" });
            CB_Species.Location = new System.Drawing.Point(58, 15);
            CB_Species.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            CB_Species.Name = "CB_Species";
            CB_Species.Size = new System.Drawing.Size(107, 25);
            CB_Species.TabIndex = 21;
            CB_Species.SelectedIndexChanged += ChangeCBSpecies;
            CB_Species.SelectedValueChanged += ChangeCBSpecies;
            // 
            // B_GiveAll
            // 
            B_GiveAll.Location = new System.Drawing.Point(174, 13);
            B_GiveAll.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            B_GiveAll.Name = "B_GiveAll";
            B_GiveAll.Size = new System.Drawing.Size(70, 27);
            B_GiveAll.TabIndex = 23;
            B_GiveAll.Text = "Check All";
            B_GiveAll.UseVisualStyleBackColor = true;
            B_GiveAll.Click += B_GiveAll_Click;
            // 
            // B_Save
            // 
            B_Save.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            B_Save.Location = new System.Drawing.Point(638, 250);
            B_Save.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            B_Save.Name = "B_Save";
            B_Save.Size = new System.Drawing.Size(93, 27);
            B_Save.TabIndex = 24;
            B_Save.Text = "Save";
            B_Save.UseVisualStyleBackColor = true;
            B_Save.Click += B_Save_Click;
            // 
            // B_Modify
            // 
            B_Modify.Location = new System.Drawing.Point(370, 13);
            B_Modify.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            B_Modify.Name = "B_Modify";
            B_Modify.Size = new System.Drawing.Size(70, 27);
            B_Modify.TabIndex = 25;
            B_Modify.Text = "Modify...";
            B_Modify.UseVisualStyleBackColor = true;
            B_Modify.Click += B_Modify_Click;
            // 
            // GB_Language
            // 
            GB_Language.Controls.Add(CHK_L7);
            GB_Language.Controls.Add(CHK_L6);
            GB_Language.Controls.Add(CHK_L5);
            GB_Language.Controls.Add(CHK_L4);
            GB_Language.Controls.Add(CHK_L3);
            GB_Language.Controls.Add(CHK_L2);
            GB_Language.Controls.Add(CHK_L1);
            GB_Language.Location = new System.Drawing.Point(314, 46);
            GB_Language.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            GB_Language.Name = "GB_Language";
            GB_Language.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            GB_Language.Size = new System.Drawing.Size(126, 177);
            GB_Language.TabIndex = 26;
            GB_Language.TabStop = false;
            GB_Language.Text = "Languages";
            // 
            // GB_Encountered
            // 
            GB_Encountered.Controls.Add(CHK_P5);
            GB_Encountered.Controls.Add(CHK_P4);
            GB_Encountered.Controls.Add(CHK_P3);
            GB_Encountered.Controls.Add(CHK_P2);
            GB_Encountered.Location = new System.Drawing.Point(173, 46);
            GB_Encountered.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            GB_Encountered.Name = "GB_Encountered";
            GB_Encountered.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            GB_Encountered.Size = new System.Drawing.Size(134, 90);
            GB_Encountered.TabIndex = 27;
            GB_Encountered.TabStop = false;
            GB_Encountered.Text = "Seen";
            // 
            // GB_Owned
            // 
            GB_Owned.Controls.Add(CHK_P1);
            GB_Owned.Location = new System.Drawing.Point(173, 136);
            GB_Owned.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            GB_Owned.Name = "GB_Owned";
            GB_Owned.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            GB_Owned.Size = new System.Drawing.Size(134, 40);
            GB_Owned.TabIndex = 28;
            GB_Owned.TabStop = false;
            GB_Owned.Text = "Owned";
            // 
            // GB_Displayed
            // 
            GB_Displayed.Controls.Add(CHK_P9);
            GB_Displayed.Controls.Add(CHK_P8);
            GB_Displayed.Controls.Add(CHK_P7);
            GB_Displayed.Controls.Add(CHK_P6);
            GB_Displayed.Location = new System.Drawing.Point(174, 183);
            GB_Displayed.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            GB_Displayed.Name = "GB_Displayed";
            GB_Displayed.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            GB_Displayed.Size = new System.Drawing.Size(134, 89);
            GB_Displayed.TabIndex = 31;
            GB_Displayed.TabStop = false;
            GB_Displayed.Text = "Displayed";
            // 
            // modifyMenu
            // 
            modifyMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { mnuSeenNone, mnuSeenAll, mnuCaughtNone, mnuCaughtAll, mnuComplete });
            modifyMenu.Name = "modifyMenu";
            modifyMenu.Size = new System.Drawing.Size(159, 114);
            // 
            // mnuSeenNone
            // 
            mnuSeenNone.Name = "mnuSeenNone";
            mnuSeenNone.Size = new System.Drawing.Size(158, 22);
            mnuSeenNone.Text = "Seen none";
            mnuSeenNone.Click += ModifyAll;
            // 
            // mnuSeenAll
            // 
            mnuSeenAll.Name = "mnuSeenAll";
            mnuSeenAll.Size = new System.Drawing.Size(158, 22);
            mnuSeenAll.Text = "Seen all";
            mnuSeenAll.Click += ModifyAll;
            // 
            // mnuCaughtNone
            // 
            mnuCaughtNone.Name = "mnuCaughtNone";
            mnuCaughtNone.Size = new System.Drawing.Size(158, 22);
            mnuCaughtNone.Text = "Caught none";
            mnuCaughtNone.Click += ModifyAll;
            // 
            // mnuCaughtAll
            // 
            mnuCaughtAll.Name = "mnuCaughtAll";
            mnuCaughtAll.Size = new System.Drawing.Size(158, 22);
            mnuCaughtAll.Text = "Caught all";
            mnuCaughtAll.Click += ModifyAll;
            // 
            // mnuComplete
            // 
            mnuComplete.Name = "mnuComplete";
            mnuComplete.Size = new System.Drawing.Size(158, 22);
            mnuComplete.Text = "Complete Dex";
            mnuComplete.Click += ModifyAll;
            // 
            // B_ModifyForms
            // 
            B_ModifyForms.Location = new System.Drawing.Point(662, 13);
            B_ModifyForms.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            B_ModifyForms.Name = "B_ModifyForms";
            B_ModifyForms.Size = new System.Drawing.Size(70, 27);
            B_ModifyForms.TabIndex = 43;
            B_ModifyForms.Text = "Modify...";
            B_ModifyForms.UseVisualStyleBackColor = true;
            B_ModifyForms.Click += B_ModifyForms_Click;
            // 
            // L_FormDisplayed
            // 
            L_FormDisplayed.Location = new System.Drawing.Point(589, 46);
            L_FormDisplayed.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            L_FormDisplayed.Name = "L_FormDisplayed";
            L_FormDisplayed.Size = new System.Drawing.Size(121, 23);
            L_FormDisplayed.TabIndex = 42;
            L_FormDisplayed.Text = "Displayed Form:";
            L_FormDisplayed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CLB_FormDisplayed
            // 
            CLB_FormDisplayed.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            CLB_FormDisplayed.FormattingEnabled = true;
            CLB_FormDisplayed.Location = new System.Drawing.Point(593, 70);
            CLB_FormDisplayed.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            CLB_FormDisplayed.Name = "CLB_FormDisplayed";
            CLB_FormDisplayed.Size = new System.Drawing.Size(138, 144);
            CLB_FormDisplayed.TabIndex = 41;
            CLB_FormDisplayed.ItemCheck += UpdateDisplayedForm;
            // 
            // L_FormsSeen
            // 
            L_FormsSeen.Location = new System.Drawing.Point(443, 46);
            L_FormsSeen.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            L_FormsSeen.Name = "L_FormsSeen";
            L_FormsSeen.Size = new System.Drawing.Size(121, 23);
            L_FormsSeen.TabIndex = 40;
            L_FormsSeen.Text = "Seen Forms:";
            L_FormsSeen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CLB_FormsSeen
            // 
            CLB_FormsSeen.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            CLB_FormsSeen.FormattingEnabled = true;
            CLB_FormsSeen.Location = new System.Drawing.Point(447, 70);
            CLB_FormsSeen.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            CLB_FormsSeen.Name = "CLB_FormsSeen";
            CLB_FormsSeen.Size = new System.Drawing.Size(138, 144);
            CLB_FormsSeen.TabIndex = 39;
            // 
            // modifyMenuForms
            // 
            modifyMenuForms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { mnuFormNone, mnuForm1, mnuFormAll });
            modifyMenuForms.Name = "modifyMenu";
            modifyMenuForms.Size = new System.Drawing.Size(138, 70);
            // 
            // mnuFormNone
            // 
            mnuFormNone.Name = "mnuFormNone";
            mnuFormNone.Size = new System.Drawing.Size(137, 22);
            mnuFormNone.Text = "Seen none";
            mnuFormNone.Click += ModifyAllForms;
            // 
            // mnuForm1
            // 
            mnuForm1.Name = "mnuForm1";
            mnuForm1.Size = new System.Drawing.Size(137, 22);
            mnuForm1.Text = "Seen one";
            mnuForm1.Click += ModifyAllForms;
            // 
            // mnuFormAll
            // 
            mnuFormAll.Name = "mnuFormAll";
            mnuFormAll.Size = new System.Drawing.Size(137, 22);
            mnuFormAll.Text = "Seen all";
            mnuFormAll.Click += ModifyAllForms;
            // 
            // CHK_NationalDexUnlocked
            // 
            CHK_NationalDexUnlocked.AutoSize = true;
            CHK_NationalDexUnlocked.Location = new System.Drawing.Point(335, 229);
            CHK_NationalDexUnlocked.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            CHK_NationalDexUnlocked.Name = "CHK_NationalDexUnlocked";
            CHK_NationalDexUnlocked.Size = new System.Drawing.Size(173, 21);
            CHK_NationalDexUnlocked.TabIndex = 20;
            CHK_NationalDexUnlocked.Text = "National Mode Unlocked";
            CHK_NationalDexUnlocked.UseVisualStyleBackColor = true;
            // 
            // TB_PID
            // 
            TB_PID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            TB_PID.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            TB_PID.Location = new System.Drawing.Point(664, 224);
            TB_PID.MaxLength = 8;
            TB_PID.Name = "TB_PID";
            TB_PID.Size = new System.Drawing.Size(68, 20);
            TB_PID.TabIndex = 44;
            TB_PID.Text = "00000000";
            TB_PID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // L_Spinda
            // 
            L_Spinda.Location = new System.Drawing.Point(589, 220);
            L_Spinda.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            L_Spinda.Name = "L_Spinda";
            L_Spinda.Size = new System.Drawing.Size(79, 24);
            L_Spinda.TabIndex = 45;
            L_Spinda.Text = "Spinda:";
            L_Spinda.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CHK_NationalDexActive
            // 
            CHK_NationalDexActive.AutoSize = true;
            CHK_NationalDexActive.Location = new System.Drawing.Point(335, 248);
            CHK_NationalDexActive.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            CHK_NationalDexActive.Name = "CHK_NationalDexActive";
            CHK_NationalDexActive.Size = new System.Drawing.Size(153, 21);
            CHK_NationalDexActive.TabIndex = 46;
            CHK_NationalDexActive.Text = "National Mode Active";
            CHK_NationalDexActive.UseVisualStyleBackColor = true;
            // 
            // SAV_Pokedex5
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            ClientSize = new System.Drawing.Size(740, 285);
            Controls.Add(CHK_NationalDexActive);
            Controls.Add(L_Spinda);
            Controls.Add(TB_PID);
            Controls.Add(CHK_NationalDexUnlocked);
            Controls.Add(B_ModifyForms);
            Controls.Add(L_FormDisplayed);
            Controls.Add(CLB_FormDisplayed);
            Controls.Add(L_FormsSeen);
            Controls.Add(CLB_FormsSeen);
            Controls.Add(GB_Displayed);
            Controls.Add(GB_Owned);
            Controls.Add(GB_Encountered);
            Controls.Add(GB_Language);
            Controls.Add(B_Modify);
            Controls.Add(B_Save);
            Controls.Add(B_GiveAll);
            Controls.Add(CB_Species);
            Controls.Add(L_goto);
            Controls.Add(LB_Species);
            Controls.Add(B_Cancel);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Icon = Properties.Resources.Icon;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SAV_Pokedex5";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Pokédex Editor";
            GB_Language.ResumeLayout(false);
            GB_Language.PerformLayout();
            GB_Encountered.ResumeLayout(false);
            GB_Encountered.PerformLayout();
            GB_Owned.ResumeLayout(false);
            GB_Owned.PerformLayout();
            GB_Displayed.ResumeLayout(false);
            GB_Displayed.PerformLayout();
            modifyMenu.ResumeLayout(false);
            modifyMenuForms.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button B_Cancel;
        private System.Windows.Forms.ListBox LB_Species;
        private System.Windows.Forms.CheckBox CHK_P1;
        private System.Windows.Forms.CheckBox CHK_P2;
        private System.Windows.Forms.CheckBox CHK_P3;
        private System.Windows.Forms.CheckBox CHK_P4;
        private System.Windows.Forms.CheckBox CHK_P5;
        private System.Windows.Forms.CheckBox CHK_P6;
        private System.Windows.Forms.CheckBox CHK_P7;
        private System.Windows.Forms.CheckBox CHK_P8;
        private System.Windows.Forms.CheckBox CHK_P9;
        private System.Windows.Forms.CheckBox CHK_L7;
        private System.Windows.Forms.CheckBox CHK_L6;
        private System.Windows.Forms.CheckBox CHK_L5;
        private System.Windows.Forms.CheckBox CHK_L4;
        private System.Windows.Forms.CheckBox CHK_L3;
        private System.Windows.Forms.CheckBox CHK_L2;
        private System.Windows.Forms.CheckBox CHK_L1;
        private System.Windows.Forms.Label L_goto;
        private System.Windows.Forms.ComboBox CB_Species;
        private System.Windows.Forms.Button B_GiveAll;
        private System.Windows.Forms.Button B_Save;
        private System.Windows.Forms.Button B_Modify;
        private System.Windows.Forms.GroupBox GB_Language;
        private System.Windows.Forms.GroupBox GB_Encountered;
        private System.Windows.Forms.GroupBox GB_Owned;
        private System.Windows.Forms.GroupBox GB_Displayed;
        private System.Windows.Forms.ContextMenuStrip modifyMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuSeenNone;
        private System.Windows.Forms.ToolStripMenuItem mnuSeenAll;
        private System.Windows.Forms.ToolStripMenuItem mnuCaughtNone;
        private System.Windows.Forms.ToolStripMenuItem mnuCaughtAll;
        private System.Windows.Forms.ToolStripMenuItem mnuComplete;
        private System.Windows.Forms.CheckedListBox CLB_FormsSeen;
        private System.Windows.Forms.Label L_FormsSeen;
        private System.Windows.Forms.CheckedListBox CLB_FormDisplayed;
        private System.Windows.Forms.Label L_FormDisplayed;
        private System.Windows.Forms.Button B_ModifyForms;
        private System.Windows.Forms.ContextMenuStrip modifyMenuForms;
        private System.Windows.Forms.ToolStripMenuItem mnuFormNone;
        private System.Windows.Forms.ToolStripMenuItem mnuForm1;
        private System.Windows.Forms.ToolStripMenuItem mnuFormAll;
        private System.Windows.Forms.CheckBox CHK_NationalDexUnlocked;
        private System.Windows.Forms.TextBox TB_PID;
        private System.Windows.Forms.Label L_Spinda;
        private System.Windows.Forms.CheckBox CHK_NationalDexActive;
    }
}
