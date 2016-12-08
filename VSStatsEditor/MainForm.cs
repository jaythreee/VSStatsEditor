using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace VSStatsEditor
{
    public partial class MainForm : Form
    {
        private string zonesDir = GameData.DefaultZonesDir;
        private UInt16[] curEnemyBodyPartHPs = new UInt16[6];
        string[] zoneData;
        private static int[] zoneEnemiesIndex = new int[50];

        public MainForm()
        {
            InitializeComponent();

            string version = Application.ProductVersion;
            this.Text = String.Format("VS Stat Editor v{0}", version);

            /* Setup equipment lists */
            bladeBox.DataSource = new BindingSource(GameData.Blades, null);
            bladeBox.DisplayMember = "Value";
            bladeBox.ValueMember = "Key";
            bladeMatBox.DataSource = new BindingSource(GameData.Materials, null);
            bladeMatBox.DisplayMember = "Value";
            bladeMatBox.ValueMember = "Key";
            gripBox.DataSource = new BindingSource(GameData.Grips, null);
            gripBox.DisplayMember = "Value";
            gripBox.ValueMember = "Key";
            weapGem1Box.DataSource = new BindingSource(GameData.Gems, null);
            weapGem1Box.DisplayMember = "Value";
            weapGem1Box.ValueMember = "Key";
            weapGem2Box.DataSource = new BindingSource(GameData.Gems, null);
            weapGem2Box.DisplayMember = "Value";
            weapGem2Box.ValueMember = "Key";
            weapGem3Box.DataSource = new BindingSource(GameData.Gems, null);
            weapGem3Box.DisplayMember = "Value";
            weapGem3Box.ValueMember = "Key";
            shieldBox.DataSource = new BindingSource(GameData.Shields, null);
            shieldBox.DisplayMember = "Value";
            shieldBox.ValueMember = "Key";
            shieldMatBox.DataSource = new BindingSource(GameData.Materials, null);
            shieldMatBox.DisplayMember = "Value";
            shieldMatBox.ValueMember = "Key";
            shieldGem1Box.DataSource = new BindingSource(GameData.Gems, null);
            shieldGem1Box.DisplayMember = "Value";
            shieldGem1Box.ValueMember = "Key";
            shieldGem2Box.DataSource = new BindingSource(GameData.Gems, null);
            shieldGem2Box.DisplayMember = "Value";
            shieldGem2Box.ValueMember = "Key";
            shieldGem3Box.DataSource = new BindingSource(GameData.Gems, null);
            shieldGem3Box.DisplayMember = "Value";
            shieldGem3Box.ValueMember = "Key";
            accBox.DataSource = new BindingSource(GameData.Acc, null);
            accBox.DisplayMember = "Value";
            accBox.ValueMember = "Key";
            bp0NameBox.DataSource = new BindingSource(GameData.OtherItems, null);
            bp0NameBox.DisplayMember = "Value";
            bp0NameBox.ValueMember = "Key";
            bp0MatBox.DataSource = new BindingSource(GameData.Materials, null);
            bp0MatBox.DisplayMember = "Value";
            bp0MatBox.ValueMember = "Key";
            bp1NameBox.DataSource = new BindingSource(GameData.OtherItems, null);
            bp1NameBox.DisplayMember = "Value";
            bp1NameBox.ValueMember = "Key";
            bp1MatBox.DataSource = new BindingSource(GameData.Materials, null);
            bp1MatBox.DisplayMember = "Value";
            bp1MatBox.ValueMember = "Key";
            bp2NameBox.DataSource = new BindingSource(GameData.OtherItems, null);
            bp2NameBox.DisplayMember = "Value";
            bp2NameBox.ValueMember = "Key";
            bp2MatBox.DataSource = new BindingSource(GameData.Materials, null);
            bp2MatBox.DisplayMember = "Value";
            bp2MatBox.ValueMember = "Key";
            bp3NameBox.DataSource = new BindingSource(GameData.OtherItems, null);
            bp3NameBox.DisplayMember = "Value";
            bp3NameBox.ValueMember = "Key";
            bp3MatBox.DataSource = new BindingSource(GameData.Materials, null);
            bp3MatBox.DisplayMember = "Value";
            bp3MatBox.ValueMember = "Key";
            bp4NameBox.DataSource = new BindingSource(GameData.OtherItems, null);
            bp4NameBox.DisplayMember = "Value";
            bp4NameBox.ValueMember = "Key";
            bp4MatBox.DataSource = new BindingSource(GameData.Materials, null);
            bp4MatBox.DisplayMember = "Value";
            bp4MatBox.ValueMember = "Key";
            bp5NameBox.DataSource = new BindingSource(GameData.OtherItems, null);
            bp5NameBox.DisplayMember = "Value";
            bp5NameBox.ValueMember = "Key";
            bp5MatBox.DataSource = new BindingSource(GameData.Materials, null);
            bp5MatBox.DisplayMember = "Value";
            bp5MatBox.ValueMember = "Key";
        }

        private void LoadZones()
        {
            /* Repopulate zone box with zones in selected directory */
            zoneBox.Items.Clear();
            zoneBox.ResetText();
            zoneBox.SelectedIndex = -1;
            zoneBox.Refresh();

            foreach (string curZone in Directory.GetFiles(zonesDir, "*.ASM").Select(Path.GetFileNameWithoutExtension))
                zoneBox.Items.Add(curZone);

            if (zoneBox.Items.Count > 0)
                zoneBox.SelectedIndex = 0;
        }

        private void enemyBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            /* Parse out enemy's current data */
            ClearCurrentEnemy();

            /* Basic stats: HP, MP, STR, INT, AGI, Run speed */
            hpBox.Text = UInt32.Parse(GetEnemyData(GameData.HPHeader, zoneEnemiesIndex[enemyBox.SelectedIndex]), System.Globalization.NumberStyles.HexNumber).ToString();
            mpBox.Text = UInt32.Parse(GetEnemyData(GameData.MPHeader, zoneEnemiesIndex[enemyBox.SelectedIndex]), System.Globalization.NumberStyles.HexNumber).ToString();
            strBox.Text = UInt16.Parse(GetEnemyData(GameData.STRHeader, zoneEnemiesIndex[enemyBox.SelectedIndex]), System.Globalization.NumberStyles.HexNumber).ToString();
            intBox.Text = UInt16.Parse(GetEnemyData(GameData.INTHeader, zoneEnemiesIndex[enemyBox.SelectedIndex]), System.Globalization.NumberStyles.HexNumber).ToString();
            agiBox.Text = UInt16.Parse(GetEnemyData(GameData.AGIHeader, zoneEnemiesIndex[enemyBox.SelectedIndex]), System.Globalization.NumberStyles.HexNumber).ToString();
            runSpeedBox.Text = UInt16.Parse(GetEnemyData(GameData.RunSpeedHeader, zoneEnemiesIndex[enemyBox.SelectedIndex]), System.Globalization.NumberStyles.HexNumber).ToString();

            /* Equipment and droprates */
            /* Weapon */
            bladeBox.SelectedValue = GetEnemyData(GameData.WeaponNameHeader, zoneEnemiesIndex[enemyBox.SelectedIndex]);
            gripBox.SelectedValue = GetEnemyData(GameData.WeaponGripNameHeader, zoneEnemiesIndex[enemyBox.SelectedIndex]);
            weapGem1Box.SelectedValue = GetEnemyData(GameData.WeaponGem1Header, zoneEnemiesIndex[enemyBox.SelectedIndex]);
            weapGem2Box.SelectedValue = GetEnemyData(GameData.WeaponGem2Header, zoneEnemiesIndex[enemyBox.SelectedIndex]);
            weapGem3Box.SelectedValue = GetEnemyData(GameData.WeaponGem3Header, zoneEnemiesIndex[enemyBox.SelectedIndex]);
            bladeMatBox.SelectedValue = GetEnemyData(GameData.WeaponMatHeader, zoneEnemiesIndex[enemyBox.SelectedIndex]);
            weapDropRateBox.Text = (UInt16.Parse(GetEnemyData(GameData.WeaponDropChanceHeader, zoneEnemiesIndex[enemyBox.SelectedIndex]), System.Globalization.NumberStyles.HexNumber) / 255.0d * 100).ToString("0.00");

            /* Shield */
            shieldBox.SelectedValue = GetEnemyData(GameData.ShieldNameHeader, zoneEnemiesIndex[enemyBox.SelectedIndex]);
            shieldGem1Box.SelectedValue = GetEnemyData(GameData.ShieldGem1Header, zoneEnemiesIndex[enemyBox.SelectedIndex]);
            shieldGem2Box.SelectedValue = GetEnemyData(GameData.ShieldGem2Header, zoneEnemiesIndex[enemyBox.SelectedIndex]);
            shieldGem3Box.SelectedValue = GetEnemyData(GameData.ShieldGem3Header, zoneEnemiesIndex[enemyBox.SelectedIndex]);
            shieldMatBox.SelectedValue = GetEnemyData(GameData.ShieldMatHeader, zoneEnemiesIndex[enemyBox.SelectedIndex]);
            shieldDropRateBox.Text = (UInt16.Parse(GetEnemyData(GameData.ShieldDropChanceHeader, zoneEnemiesIndex[enemyBox.SelectedIndex]), System.Globalization.NumberStyles.HexNumber) / 255.0d * 100).ToString("0.00");

            /* Accessory */
            accBox.SelectedValue = GetEnemyData(GameData.AccNameHeader, zoneEnemiesIndex[enemyBox.SelectedIndex]);
            accDropRateBox.Text = (UInt16.Parse(GetEnemyData(GameData.AccDropChanceHeader, zoneEnemiesIndex[enemyBox.SelectedIndex]), System.Globalization.NumberStyles.HexNumber) / 255.0d * 100).ToString("0.00");

            /* Bodypart HP / armors */
            curEnemyBodyPartHPs[0] = Convert.ToUInt16(GetEnemyData(GameData.BodyPart0HPHeader, zoneEnemiesIndex[enemyBox.SelectedIndex]), 16);
            bp0NameBox.SelectedValue = GetEnemyData(GameData.BodyPart0NameHeader, zoneEnemiesIndex[enemyBox.SelectedIndex]);
            bp0MatBox.SelectedValue = GetEnemyData(GameData.BodyPart0MatHeader, zoneEnemiesIndex[enemyBox.SelectedIndex]);
            bp0DropBox.Text = (UInt16.Parse(GetEnemyData(GameData.BodyPart0DropChanceHeader, zoneEnemiesIndex[enemyBox.SelectedIndex]), System.Globalization.NumberStyles.HexNumber) / 255.0d * 100).ToString("0.00");

            curEnemyBodyPartHPs[1] = Convert.ToUInt16(GetEnemyData(GameData.BodyPart1HPHeader, zoneEnemiesIndex[enemyBox.SelectedIndex]), 16);
            bp1NameBox.SelectedValue = GetEnemyData(GameData.BodyPart1NameHeader, zoneEnemiesIndex[enemyBox.SelectedIndex]);
            bp1MatBox.SelectedValue = GetEnemyData(GameData.BodyPart1MatHeader, zoneEnemiesIndex[enemyBox.SelectedIndex]);
            bp1DropBox.Text = (UInt16.Parse(GetEnemyData(GameData.BodyPart1DropChanceHeader, zoneEnemiesIndex[enemyBox.SelectedIndex]), System.Globalization.NumberStyles.HexNumber) / 255.0d * 100).ToString("0.00");

            curEnemyBodyPartHPs[2] = Convert.ToUInt16(GetEnemyData(GameData.BodyPart2HPHeader, zoneEnemiesIndex[enemyBox.SelectedIndex]), 16);
            bp2NameBox.SelectedValue = GetEnemyData(GameData.BodyPart2NameHeader, zoneEnemiesIndex[enemyBox.SelectedIndex]);
            bp2MatBox.SelectedValue = GetEnemyData(GameData.BodyPart2MatHeader, zoneEnemiesIndex[enemyBox.SelectedIndex]);
            bp2DropBox.Text = (UInt16.Parse(GetEnemyData(GameData.BodyPart2DropChanceHeader, zoneEnemiesIndex[enemyBox.SelectedIndex]), System.Globalization.NumberStyles.HexNumber) / 255.0d * 100).ToString("0.00");

            curEnemyBodyPartHPs[3] = Convert.ToUInt16(GetEnemyData(GameData.BodyPart3HPHeader, zoneEnemiesIndex[enemyBox.SelectedIndex]), 16);
            bp3NameBox.SelectedValue = GetEnemyData(GameData.BodyPart3NameHeader, zoneEnemiesIndex[enemyBox.SelectedIndex]);
            bp3MatBox.SelectedValue = GetEnemyData(GameData.BodyPart3MatHeader, zoneEnemiesIndex[enemyBox.SelectedIndex]);
            bp3DropBox.Text = (UInt16.Parse(GetEnemyData(GameData.BodyPart3DropChanceHeader, zoneEnemiesIndex[enemyBox.SelectedIndex]), System.Globalization.NumberStyles.HexNumber) / 255.0d * 100).ToString("0.00");

            curEnemyBodyPartHPs[4] = Convert.ToUInt16(GetEnemyData(GameData.BodyPart4HPHeader, zoneEnemiesIndex[enemyBox.SelectedIndex]), 16);
            bp4NameBox.SelectedValue = GetEnemyData(GameData.BodyPart4NameHeader, zoneEnemiesIndex[enemyBox.SelectedIndex]);
            bp4MatBox.SelectedValue = GetEnemyData(GameData.BodyPart4MatHeader, zoneEnemiesIndex[enemyBox.SelectedIndex]);
            bp4DropBox.Text = (UInt16.Parse(GetEnemyData(GameData.BodyPart4DropChanceHeader, zoneEnemiesIndex[enemyBox.SelectedIndex]), System.Globalization.NumberStyles.HexNumber) / 255.0d * 100).ToString("0.00");

            curEnemyBodyPartHPs[5] = Convert.ToUInt16(GetEnemyData(GameData.BodyPart5HPHeader, zoneEnemiesIndex[enemyBox.SelectedIndex]), 16);
            bp5NameBox.SelectedValue = GetEnemyData(GameData.BodyPart5NameHeader, zoneEnemiesIndex[enemyBox.SelectedIndex]);
            bp5MatBox.SelectedValue = GetEnemyData(GameData.BodyPart5MatHeader, zoneEnemiesIndex[enemyBox.SelectedIndex]);
            bp5DropBox.Text = (UInt16.Parse(GetEnemyData(GameData.BodyPart5DropChanceHeader, zoneEnemiesIndex[enemyBox.SelectedIndex]), System.Globalization.NumberStyles.HexNumber) / 255.0d * 100).ToString("0.00");
        }

        private void zoneBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            /* Parse enemies in zone */
            string enemy;
            int currentLine = 0;
            int numEnemies = 0;
            ClearCurrentEnemy();
            enemyBox.Items.Clear();
            enemyBox.ResetText();
            enemyBox.SelectedIndex = -1;
            enemyBox.Refresh();

            zoneData = File.ReadAllLines(String.Format("{0}{1}{2}.ASM", zonesDir, Path.DirectorySeparatorChar, zoneBox.SelectedItem.ToString()));
            while (!zoneData[currentLine].Contains(GameData.EndStatsSection))
            {
                if (zoneData[currentLine].Contains(GameData.EnemyNameHeader))
                {
                    enemy = zoneData[currentLine].Substring(zoneData[currentLine].IndexOf('x') + 1);
                    enemyBox.Items.Add(GameData.TranslateVSText(enemy));
                    zoneEnemiesIndex[numEnemies] = currentLine;
                    numEnemies++;
                }

                currentLine++;
            }

            if (enemyBox.Items.Count > 0)
                enemyBox.SelectedIndex = 0;
        }

        private void saveEnemy_Click(object sender, EventArgs e)
        {
            savingLabel.Visible = true;
            savingLabel.Refresh();
            saveEnemyBtn.Enabled = false;
            compileButton.Enabled = false;

            /* Loop through all ASM files and multipy each enemy's stat value */
            if (multModeCheckBox.Checked)
            {
                string asmPath = String.Format("{0}{1}", zonesDir, Path.DirectorySeparatorChar);
                string[] asmFiles = Directory.GetFiles(asmPath, "*.ASM", SearchOption.TopDirectoryOnly);

                double newVal;
                int currentLine;
                int numEnemies;
                string enemy;
                foreach (string curZonePath in asmFiles)
                {
                    zoneData = File.ReadAllLines(curZonePath);
                    currentLine = 0;
                    numEnemies = 0;
                    while (!zoneData[currentLine].Contains(GameData.EndStatsSection))
                    {
                        if (zoneData[currentLine].Contains(GameData.EnemyNameHeader))
                        {
                            enemy = zoneData[currentLine].Substring(zoneData[currentLine].IndexOf('x') + 1);
                            zoneEnemiesIndex[numEnemies] = currentLine;
                            numEnemies++;
                        }

                        currentLine++;
                    }

                    for (int curEnemy = 0; curEnemy < numEnemies; curEnemy++)
                    {
                        newVal = Math.Truncate(UInt16.Parse(GetEnemyData(GameData.HPHeader, zoneEnemiesIndex[curEnemy]), System.Globalization.NumberStyles.HexNumber) * Double.Parse(hpBox.Text));
                        if (newVal > UInt16.MaxValue)
                            newVal = UInt16.MaxValue;

                        SetEnemyData(GameData.HPHeader, zoneEnemiesIndex[curEnemy], UInt16.Parse(newVal.ToString()).ToString("X4"));

                        newVal = Math.Truncate(UInt16.Parse(GetEnemyData(GameData.MPHeader, zoneEnemiesIndex[curEnemy]), System.Globalization.NumberStyles.HexNumber) * Double.Parse(mpBox.Text));
                        if (newVal > UInt16.MaxValue)
                            newVal = UInt16.MaxValue;

                        SetEnemyData(GameData.MPHeader, zoneEnemiesIndex[curEnemy], UInt16.Parse(newVal.ToString()).ToString("X4"));

                        newVal = Math.Truncate(Byte.Parse(GetEnemyData(GameData.STRHeader, zoneEnemiesIndex[curEnemy]), System.Globalization.NumberStyles.HexNumber) * Double.Parse(strBox.Text));
                        if (newVal > Byte.MaxValue)
                            newVal = Byte.MaxValue;

                        SetEnemyData(GameData.STRHeader, zoneEnemiesIndex[curEnemy], Byte.Parse(newVal.ToString()).ToString("X2"));

                        newVal = Math.Truncate(Byte.Parse(GetEnemyData(GameData.INTHeader, zoneEnemiesIndex[curEnemy]), System.Globalization.NumberStyles.HexNumber) * Double.Parse(intBox.Text));
                        if (newVal > Byte.MaxValue)
                            newVal = Byte.MaxValue;

                        SetEnemyData(GameData.INTHeader, zoneEnemiesIndex[curEnemy], Byte.Parse(newVal.ToString()).ToString("X2"));

                        newVal = Math.Truncate(Byte.Parse(GetEnemyData(GameData.AGIHeader, zoneEnemiesIndex[curEnemy]), System.Globalization.NumberStyles.HexNumber) * Double.Parse(agiBox.Text));
                        if (newVal > Byte.MaxValue)
                            newVal = Byte.MaxValue;

                        SetEnemyData(GameData.AGIHeader, zoneEnemiesIndex[curEnemy], Byte.Parse(newVal.ToString()).ToString("X2"));

                        newVal = Math.Truncate(Byte.Parse(GetEnemyData(GameData.RunSpeedHeader, zoneEnemiesIndex[curEnemy]), System.Globalization.NumberStyles.HexNumber) * Double.Parse(runSpeedBox.Text));
                        if (newVal > Byte.MaxValue)
                            newVal = Byte.MaxValue;
                        SetEnemyData(GameData.RunSpeedHeader, zoneEnemiesIndex[curEnemy], Byte.Parse(newVal.ToString()).ToString("X2"));

                        /* Update body hp values */
                        newVal = Math.Truncate(UInt16.Parse(GetEnemyData(GameData.BodyPart0HPHeader, zoneEnemiesIndex[curEnemy]), System.Globalization.NumberStyles.HexNumber) * Double.Parse(hpBox.Text));
                        if (newVal > UInt16.MaxValue)
                            newVal = UInt16.MaxValue;
                        SetEnemyData(GameData.BodyPart0HPHeader, zoneEnemiesIndex[curEnemy], UInt16.Parse(newVal.ToString()).ToString("X4"));

                        newVal = Math.Truncate(UInt16.Parse(GetEnemyData(GameData.BodyPart1HPHeader, zoneEnemiesIndex[curEnemy]), System.Globalization.NumberStyles.HexNumber) * Double.Parse(hpBox.Text));
                        if (newVal > UInt16.MaxValue)
                            newVal = UInt16.MaxValue;
                        SetEnemyData(GameData.BodyPart1HPHeader, zoneEnemiesIndex[curEnemy], UInt16.Parse(newVal.ToString()).ToString("X4"));

                        newVal = Math.Truncate(UInt16.Parse(GetEnemyData(GameData.BodyPart2HPHeader, zoneEnemiesIndex[curEnemy]), System.Globalization.NumberStyles.HexNumber) * Double.Parse(hpBox.Text));
                        if (newVal > UInt16.MaxValue)
                            newVal = UInt16.MaxValue;
                        SetEnemyData(GameData.BodyPart2HPHeader, zoneEnemiesIndex[curEnemy], UInt16.Parse(newVal.ToString()).ToString("X4"));

                        newVal = Math.Truncate(UInt16.Parse(GetEnemyData(GameData.BodyPart3HPHeader, zoneEnemiesIndex[curEnemy]), System.Globalization.NumberStyles.HexNumber) * Double.Parse(hpBox.Text));
                        if (newVal > UInt16.MaxValue)
                            newVal = UInt16.MaxValue;
                        SetEnemyData(GameData.BodyPart3HPHeader, zoneEnemiesIndex[curEnemy], UInt16.Parse(newVal.ToString()).ToString("X4"));

                        newVal = Math.Truncate(UInt16.Parse(GetEnemyData(GameData.BodyPart4HPHeader, zoneEnemiesIndex[curEnemy]), System.Globalization.NumberStyles.HexNumber) * Double.Parse(hpBox.Text));
                        if (newVal > UInt16.MaxValue)
                            newVal = UInt16.MaxValue;
                        SetEnemyData(GameData.BodyPart4HPHeader, zoneEnemiesIndex[curEnemy], UInt16.Parse(newVal.ToString()).ToString("X4"));

                        newVal = Math.Truncate(UInt16.Parse(GetEnemyData(GameData.BodyPart5HPHeader, zoneEnemiesIndex[curEnemy]), System.Globalization.NumberStyles.HexNumber) * Double.Parse(hpBox.Text));
                        if (newVal > UInt16.MaxValue)
                            newVal = UInt16.MaxValue;
                        SetEnemyData(GameData.BodyPart5HPHeader, zoneEnemiesIndex[curEnemy], UInt16.Parse(newVal.ToString()).ToString("X4"));
                    }

                    File.WriteAllLines(curZonePath, zoneData);
                }
            }
            /* Save single enemy */
            else
            {
                if (enemyBox.SelectedIndex > -1)
                {
                    /* Enemy stats are already validated at this point. Write out stats, equipment, and drop rates */
                    SetEnemyData(GameData.HPHeader, zoneEnemiesIndex[enemyBox.SelectedIndex], UInt16.Parse(hpBox.Text).ToString("X4"));
                    SetEnemyData(GameData.MPHeader, zoneEnemiesIndex[enemyBox.SelectedIndex], UInt16.Parse(mpBox.Text).ToString("X4"));
                    SetEnemyData(GameData.STRHeader, zoneEnemiesIndex[enemyBox.SelectedIndex], Byte.Parse(strBox.Text).ToString("X2"));
                    SetEnemyData(GameData.INTHeader, zoneEnemiesIndex[enemyBox.SelectedIndex], Byte.Parse(intBox.Text).ToString("X2"));
                    SetEnemyData(GameData.AGIHeader, zoneEnemiesIndex[enemyBox.SelectedIndex], Byte.Parse(agiBox.Text).ToString("X2"));
                    SetEnemyData(GameData.RunSpeedHeader, zoneEnemiesIndex[enemyBox.SelectedIndex], Byte.Parse(runSpeedBox.Text).ToString("X2"));

                    SetEnemyData(GameData.WeaponNameHeader, zoneEnemiesIndex[enemyBox.SelectedIndex], bladeBox.SelectedValue.ToString());
                    SetEnemyData(GameData.WeaponMatHeader, zoneEnemiesIndex[enemyBox.SelectedIndex], bladeMatBox.SelectedValue.ToString());
                    SetEnemyData(GameData.WeaponGripNameHeader, zoneEnemiesIndex[enemyBox.SelectedIndex], gripBox.SelectedValue.ToString());
                    SetEnemyData(GameData.WeaponGem1Header, zoneEnemiesIndex[enemyBox.SelectedIndex], weapGem1Box.SelectedValue.ToString());
                    SetEnemyData(GameData.WeaponGem2Header, zoneEnemiesIndex[enemyBox.SelectedIndex], weapGem2Box.SelectedValue.ToString());
                    SetEnemyData(GameData.WeaponGem3Header, zoneEnemiesIndex[enemyBox.SelectedIndex], weapGem3Box.SelectedValue.ToString());
                    SetEnemyData(GameData.WeaponDropChanceHeader, zoneEnemiesIndex[enemyBox.SelectedIndex], ((Byte)Math.Truncate(double.Parse(weapDropRateBox.Text) / 100.0d * 255.0d)).ToString("X2"));

                    SetEnemyData(GameData.ShieldNameHeader, zoneEnemiesIndex[enemyBox.SelectedIndex], shieldBox.SelectedValue.ToString());
                    SetEnemyData(GameData.ShieldMatHeader, zoneEnemiesIndex[enemyBox.SelectedIndex], shieldMatBox.SelectedValue.ToString());
                    SetEnemyData(GameData.ShieldGem1Header, zoneEnemiesIndex[enemyBox.SelectedIndex], shieldGem1Box.SelectedValue.ToString());
                    SetEnemyData(GameData.ShieldGem2Header, zoneEnemiesIndex[enemyBox.SelectedIndex], shieldGem2Box.SelectedValue.ToString());
                    SetEnemyData(GameData.ShieldGem3Header, zoneEnemiesIndex[enemyBox.SelectedIndex], shieldGem3Box.SelectedValue.ToString());
                    SetEnemyData(GameData.ShieldDropChanceHeader, zoneEnemiesIndex[enemyBox.SelectedIndex], ((Byte)Math.Truncate(double.Parse(shieldDropRateBox.Text) / 100.0d * 255.0d)).ToString("X2"));

                    SetEnemyData(GameData.AccNameHeader, zoneEnemiesIndex[enemyBox.SelectedIndex], accBox.SelectedValue.ToString());
                    SetEnemyData(GameData.AccDropChanceHeader, zoneEnemiesIndex[enemyBox.SelectedIndex], ((Byte)Math.Truncate(double.Parse(accDropRateBox.Text) / 100.0d * 255.0d)).ToString("X2"));

                    SetEnemyData(GameData.BodyPart0NameHeader, zoneEnemiesIndex[enemyBox.SelectedIndex], bp0NameBox.SelectedValue.ToString());
                    SetEnemyData(GameData.BodyPart0MatHeader, zoneEnemiesIndex[enemyBox.SelectedIndex], bp0MatBox.SelectedValue.ToString());
                    SetEnemyData(GameData.BodyPart0DropChanceHeader, zoneEnemiesIndex[enemyBox.SelectedIndex], ((Byte)Math.Truncate(double.Parse(bp0DropBox.Text) / 100.0d * 255.0d)).ToString("X2"));

                    SetEnemyData(GameData.BodyPart1NameHeader, zoneEnemiesIndex[enemyBox.SelectedIndex], bp1NameBox.SelectedValue.ToString());
                    SetEnemyData(GameData.BodyPart1MatHeader, zoneEnemiesIndex[enemyBox.SelectedIndex], bp1MatBox.SelectedValue.ToString());
                    SetEnemyData(GameData.BodyPart1DropChanceHeader, zoneEnemiesIndex[enemyBox.SelectedIndex], ((Byte)Math.Truncate(double.Parse(bp1DropBox.Text) / 100.0d * 255.0d)).ToString("X2"));

                    SetEnemyData(GameData.BodyPart2NameHeader, zoneEnemiesIndex[enemyBox.SelectedIndex], bp2NameBox.SelectedValue.ToString());
                    SetEnemyData(GameData.BodyPart2MatHeader, zoneEnemiesIndex[enemyBox.SelectedIndex], bp2MatBox.SelectedValue.ToString());
                    SetEnemyData(GameData.BodyPart2DropChanceHeader, zoneEnemiesIndex[enemyBox.SelectedIndex], ((Byte)Math.Truncate(double.Parse(bp2DropBox.Text) / 100.0d * 255.0d)).ToString("X2"));

                    SetEnemyData(GameData.BodyPart3NameHeader, zoneEnemiesIndex[enemyBox.SelectedIndex], bp3NameBox.SelectedValue.ToString());
                    SetEnemyData(GameData.BodyPart3MatHeader, zoneEnemiesIndex[enemyBox.SelectedIndex], bp3MatBox.SelectedValue.ToString());
                    SetEnemyData(GameData.BodyPart3DropChanceHeader, zoneEnemiesIndex[enemyBox.SelectedIndex], ((Byte)Math.Truncate(double.Parse(bp3DropBox.Text) / 100.0d * 255.0d)).ToString("X2"));

                    SetEnemyData(GameData.BodyPart4NameHeader, zoneEnemiesIndex[enemyBox.SelectedIndex], bp4NameBox.SelectedValue.ToString());
                    SetEnemyData(GameData.BodyPart4MatHeader, zoneEnemiesIndex[enemyBox.SelectedIndex], bp4MatBox.SelectedValue.ToString());
                    SetEnemyData(GameData.BodyPart4DropChanceHeader, zoneEnemiesIndex[enemyBox.SelectedIndex], ((Byte)Math.Truncate(double.Parse(bp4DropBox.Text) / 100.0d * 255.0d)).ToString("X2"));

                    SetEnemyData(GameData.BodyPart5NameHeader, zoneEnemiesIndex[enemyBox.SelectedIndex], bp5NameBox.SelectedValue.ToString());
                    SetEnemyData(GameData.BodyPart5MatHeader, zoneEnemiesIndex[enemyBox.SelectedIndex], bp5MatBox.SelectedValue.ToString());
                    SetEnemyData(GameData.BodyPart5DropChanceHeader, zoneEnemiesIndex[enemyBox.SelectedIndex], ((Byte)Math.Truncate(double.Parse(bp5DropBox.Text) / 100.0d * 255.0d)).ToString("X2"));

                    /* Update body hp values */
                    SetEnemyData(GameData.BodyPart0HPHeader, zoneEnemiesIndex[enemyBox.SelectedIndex], curEnemyBodyPartHPs[0].ToString("X4"));
                    SetEnemyData(GameData.BodyPart1HPHeader, zoneEnemiesIndex[enemyBox.SelectedIndex], curEnemyBodyPartHPs[1].ToString("X4"));
                    SetEnemyData(GameData.BodyPart2HPHeader, zoneEnemiesIndex[enemyBox.SelectedIndex], curEnemyBodyPartHPs[2].ToString("X4"));
                    SetEnemyData(GameData.BodyPart3HPHeader, zoneEnemiesIndex[enemyBox.SelectedIndex], curEnemyBodyPartHPs[3].ToString("X4"));
                    SetEnemyData(GameData.BodyPart4HPHeader, zoneEnemiesIndex[enemyBox.SelectedIndex], curEnemyBodyPartHPs[4].ToString("X4"));
                    SetEnemyData(GameData.BodyPart5HPHeader, zoneEnemiesIndex[enemyBox.SelectedIndex], curEnemyBodyPartHPs[5].ToString("X4"));

                    File.WriteAllLines(String.Format("{0}{1}{2}.ASM", zonesDir, Path.DirectorySeparatorChar, zoneBox.SelectedItem.ToString()), zoneData);
                }
            }
            savingLabel.Visible = false;
            saveEnemyBtn.Enabled = true;
            compileButton.Enabled = true;
        }

        private void loadZonesBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog zoneDialog = new FolderBrowserDialog();
            zoneDialog.ShowDialog();
            zonesDir = zoneDialog.SelectedPath;

            if (zonesDir == String.Empty)
                zonesDir = GameData.DefaultZonesDir;

            LoadZones();
        }

        private void ClearCurrentEnemy()
        {
            foreach (Control c in this.statsGroupBox.Controls)
            {
                if (c is TextBox)
                    ((TextBox)c).Clear();
            }
        }

        private string GetEnemyData(string statName, int enemyIndex)
        {
            /* Jump to stat name */
            while (!zoneData[++enemyIndex].Contains(statName)) ;
            return zoneData[enemyIndex].Substring(zoneData[enemyIndex].IndexOf('x') + 1);
        }

        private void SetEnemyData(string statName, int enemyIndex, string newVal)
        {
            /* Jump to stat name and replace */
            while (!zoneData[++enemyIndex].Contains(statName)) ;
            string currentStatVal = zoneData[enemyIndex].Substring(zoneData[enemyIndex].IndexOf('x') + 1);
            zoneData[enemyIndex] = zoneData[enemyIndex].Replace(currentStatVal, newVal);
        }

        private void Validate2Byte(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text == String.Empty)
                ((TextBox)sender).Text = "0";

            if (!multModeCheckBox.Checked)
            {
                if (UInt32.Parse(((TextBox)sender).Text) > UInt16.MaxValue)
                    ((TextBox)sender).Text = UInt16.MaxValue.ToString();
            }
        }

        private void ValidateHP(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text == String.Empty)
                ((TextBox)sender).Text = "0";

            if (!multModeCheckBox.Checked)
            {
                if (UInt32.Parse(((TextBox)sender).Text) > UInt16.MaxValue)
                    ((TextBox)sender).Text = UInt16.MaxValue.ToString();

                UInt16 val = Convert.ToUInt16(((TextBox)sender).Text);

                /* There seems to be a glitch that causes the enemy to do nothing if the sum of the body parts HP
                 * is less than the enemy's total HP.  The body part HP values do NOT have to add up exactly to the
                 * total HP though.  To make it easier, we'll just use some common multipliers on each body part */

                /* Enemy has only 1 body part */
                if (curEnemyBodyPartHPs[1] == 0)
                {
                    curEnemyBodyPartHPs[0] = val;
                }
                /* Increase each non-zero body part's HP */
                else
                {
                    curEnemyBodyPartHPs[0] = curEnemyBodyPartHPs[0] > 0 ? (UInt16)Math.Round(val * 0.60, 0) : (UInt16)0;
                    curEnemyBodyPartHPs[1] = curEnemyBodyPartHPs[1] > 0 ? (UInt16)Math.Round(val * 0.55, 0) : (UInt16)0;
                    curEnemyBodyPartHPs[2] = curEnemyBodyPartHPs[2] > 0 ? (UInt16)Math.Round(val * 0.75, 0) : (UInt16)0;
                    curEnemyBodyPartHPs[3] = curEnemyBodyPartHPs[3] > 0 ? (UInt16)Math.Round(val * 0.45, 0) : (UInt16)0;
                    curEnemyBodyPartHPs[4] = curEnemyBodyPartHPs[4] > 0 ? (UInt16)Math.Round(val * 0.65, 0) : (UInt16)0;
                    curEnemyBodyPartHPs[5] = curEnemyBodyPartHPs[5] > 0 ? (UInt16)Math.Round(val * 0.50, 0) : (UInt16)0;

                    /* Verify numbers add up */
                    uint total = 0;
                    for (uint i = 0; i < curEnemyBodyPartHPs.Length; i++)
                        total += curEnemyBodyPartHPs[i];

                    if (total < val)
                        curEnemyBodyPartHPs[0] += (UInt16)(val - total);
                }
            }
        }

        private void ValidateByte(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text == String.Empty)
                ((TextBox)sender).Text = "0";

            if (!multModeCheckBox.Checked)
            {
                if (UInt32.Parse(((TextBox)sender).Text) > 255)
                    ((TextBox)sender).Text = Byte.MaxValue.ToString();
            }
        }

        private void ValidateDropChance(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text == String.Empty)
                ((TextBox)sender).Text = "0";

            /* Drop rate is specified as x out of 255 so set nearest percentage */
            double val;
            double.TryParse(((TextBox)sender).Text, out val);
            if (val > 99.6d)
                ((TextBox)sender).Text = "100";
            else
                ((TextBox)sender).Text = (Math.Truncate(val / 100.0d * 255.0d) / 255.0d * 100.0d).ToString("0.00");
        }

        private void ValidateDigitOnly(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsDigit(e.KeyChar) || e.KeyChar == 8);
        }

        private void ValidateDecimal(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsDigit(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == 8);
        }

        private void multModeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            /* Change some defaults, events, and clean up GUI between modes */
            if (multModeCheckBox.Checked)
            {
                xLabel1.Visible = true;
                xLabel2.Visible = true;
                xLabel3.Visible = true;
                xLabel4.Visible = true;
                xLabel5.Visible = true;
                xLabel6.Visible = true;
                eneLabel.Enabled = false;
                enemyBox.Enabled = false;
                equipGroupBox.Enabled = false;
                zoneBox.Enabled = false;

                hpBox.KeyPress -= ValidateDigitOnly;
                mpBox.KeyPress -= ValidateDigitOnly;
                strBox.KeyPress -= ValidateDigitOnly;
                intBox.KeyPress -= ValidateDigitOnly;
                agiBox.KeyPress -= ValidateDigitOnly;
                runSpeedBox.KeyPress -= ValidateDigitOnly;

                hpBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ValidateDecimal);
                mpBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ValidateDecimal);
                strBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ValidateDecimal);
                intBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ValidateDecimal);
                agiBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ValidateDecimal);
                runSpeedBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ValidateDecimal);

                hpBox.Text = "1.0";
                mpBox.Text = "1.0";
                strBox.Text = "1.0";
                intBox.Text = "1.0";
                agiBox.Text = "1.0";
                runSpeedBox.Text = "1.0";
            }
            else
            {
                xLabel1.Visible = false;
                xLabel2.Visible = false;
                xLabel3.Visible = false;
                xLabel4.Visible = false;
                xLabel5.Visible = false;
                xLabel6.Visible = false;
                eneLabel.Enabled = true;
                enemyBox.Enabled = true;
                equipGroupBox.Enabled = true;
                zoneBox.Enabled = true;

                hpBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ValidateDigitOnly);
                mpBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ValidateDigitOnly);
                strBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ValidateDigitOnly);
                intBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ValidateDigitOnly);
                agiBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ValidateDigitOnly);
                runSpeedBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ValidateDigitOnly);

                if (enemyBox.SelectedIndex > -1 && zoneBox.SelectedIndex > -1)
                {
                    enemyBox_SelectedIndexChanged(sender, e);
                }
                else
                {
                    hpBox.Text = "0";
                    mpBox.Text = "0";
                    strBox.Text = "0";
                    intBox.Text = "0";
                    agiBox.Text = "0";
                    runSpeedBox.Text = "0";
                }
            }
        }

        private void compileButton_Click(object sender, EventArgs e)
        {
            saveEnemyBtn.Enabled = false;
            compileButton.Enabled = false;

            /* Send all ASM files through the assembler and output to same directory */
            Process asmToZnd = new Process();
            asmToZnd.StartInfo.FileName = GameData.AsmToZndTool;
            asmToZnd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            if (zonesDir != String.Empty)
            {
                compLabel.Visible = true;
                compLabel.Refresh();
                foreach (string curZone in Directory.GetFiles(zonesDir, "*.ASM").Select(Path.GetFileNameWithoutExtension))
                {
                    asmToZnd.StartInfo.Arguments = String.Format("\"{0}{1}{2}.ASM\" \"{0}{1}{2}.ZND\"", zonesDir, Path.DirectorySeparatorChar, curZone);
                    asmToZnd.Start();
                    asmToZnd.WaitForExit();
                };
                compLabel.Visible = false;
            }

            saveEnemyBtn.Enabled = true;
            compileButton.Enabled = true;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadZones();

            ToolTip multToolTip = new ToolTip();
            string helpStr = "Edits all enemies in the game by multiplying current stats by the values specified above";
            multToolTip.AutoPopDelay = 5000;
            multToolTip.InitialDelay = 100;
            multToolTip.ReshowDelay = 500;
            multToolTip.ShowAlways = true;
            multToolTip.SetToolTip(this.multModeCheckBox, helpStr);
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        private void bladeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bladeBox.SelectedItem.ToString().Contains("---"))
                bladeBox.SelectedIndex++;
        }

        private void bp0NameBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bp0NameBox.SelectedItem.ToString().Contains("---"))
                bp0NameBox.SelectedIndex++;
        }

        private void bp1NameBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bp1NameBox.SelectedItem.ToString().Contains("---"))
                bp1NameBox.SelectedIndex++;
        }

        private void bp2NameBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bp2NameBox.SelectedItem.ToString().Contains("---"))
                bp2NameBox.SelectedIndex++;
        }

        private void bp3NameBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bp3NameBox.SelectedItem.ToString().Contains("---"))
                bp3NameBox.SelectedIndex++;
        }

        private void bp4NameBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bp4NameBox.SelectedItem.ToString().Contains("---"))
                bp4NameBox.SelectedIndex++;
        }

        private void bp5NameBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bp5NameBox.SelectedItem.ToString().Contains("---"))
                bp5NameBox.SelectedIndex++;
        }

        private void gripBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gripBox.SelectedItem.ToString().Contains("---"))
                gripBox.SelectedIndex++;
        }
    }
}
