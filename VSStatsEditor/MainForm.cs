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
        private string[] zoneEnemies = new String[50];
        private UInt16[] curEnemyBodyPartHPs = new UInt16[6];

        public MainForm()
        {
            InitializeComponent();
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
            StreamReader enemyReader = File.OpenText(String.Format("{0}{1}{2}.ASM", zonesDir, Path.DirectorySeparatorChar, zoneBox.SelectedItem.ToString()));

            string line;
            while ((line = enemyReader.ReadLine()) != null)
            {
                if (line.Contains(zoneEnemies[enemyBox.SelectedIndex]))
                {
                    /* Basic stats: HP, MP, STR, INT, AGI, Run speed */
                    hpBox.Text = UInt32.Parse(GetEnemyData(GameData.HPHeader, enemyReader), System.Globalization.NumberStyles.HexNumber).ToString();
                    mpBox.Text = UInt32.Parse(GetEnemyData(GameData.MPHeader, enemyReader), System.Globalization.NumberStyles.HexNumber).ToString();
                    strBox.Text = UInt16.Parse(GetEnemyData(GameData.STRHeader, enemyReader), System.Globalization.NumberStyles.HexNumber).ToString();
                    intBox.Text = UInt16.Parse(GetEnemyData(GameData.INTHeader, enemyReader), System.Globalization.NumberStyles.HexNumber).ToString();
                    agiBox.Text = UInt16.Parse(GetEnemyData(GameData.AGIHeader, enemyReader), System.Globalization.NumberStyles.HexNumber).ToString();
                    runSpeedBox.Text = UInt16.Parse(GetEnemyData(GameData.RunSpeedHeader, enemyReader), System.Globalization.NumberStyles.HexNumber).ToString();

                    /* Equipment and droprates */
                    /* Weapon */
                    bladeBox.Items.Add(GameData.ItemNames[GetEnemyData(GameData.WeaponNameHeader, enemyReader)]);
                    bladeBox.SelectedIndex = 0;
                    gripBox.Items.Add(GameData.ItemNames[GetEnemyData(GameData.WeaponGripNameHeader, enemyReader)]);
                    gripBox.SelectedIndex = 0;
                    weapGem1Box.Items.Add(GameData.ItemNames[GetEnemyData(GameData.WeaponGem1Header, enemyReader)]);
                    weapGem1Box.SelectedIndex = 0;
                    weapGem2Box.Items.Add(GameData.ItemNames[GetEnemyData(GameData.WeaponGem2Header, enemyReader)]);
                    weapGem2Box.SelectedIndex = 0;
                    weapGem3Box.Items.Add(GameData.ItemNames[GetEnemyData(GameData.WeaponGem3Header, enemyReader)]);
                    weapGem3Box.SelectedIndex = 0;
                    bladeMatBox.Items.Add(GameData.Materials[GetEnemyData(GameData.WeaponMatHeader, enemyReader)]);
                    bladeMatBox.SelectedIndex = 0;
                    weapDropRateBox.Text = (UInt16.Parse(GetEnemyData(GameData.WeaponDropChanceHeader, enemyReader), System.Globalization.NumberStyles.HexNumber) / 255.0d * 100).ToString("0.00");

                    /* Shield */
                    shieldBox.Items.Add(GameData.ItemNames[GetEnemyData(GameData.ShieldNameHeader, enemyReader)]);
                    shieldBox.SelectedIndex = 0;
                    weapGem1Box.Items.Add(GameData.ItemNames[GetEnemyData(GameData.ShieldGem1Header, enemyReader)]);
                    weapGem1Box.SelectedIndex = 0;
                    weapGem2Box.Items.Add(GameData.ItemNames[GetEnemyData(GameData.ShieldGem2Header, enemyReader)]);
                    weapGem2Box.SelectedIndex = 0;
                    weapGem3Box.Items.Add(GameData.ItemNames[GetEnemyData(GameData.ShieldGem3Header, enemyReader)]);
                    weapGem3Box.SelectedIndex = 0;
                    shieldMatBox.Items.Add(GameData.Materials[GetEnemyData(GameData.ShieldMatHeader, enemyReader)]);
                    shieldMatBox.SelectedIndex = 0;
                    shieldDropRateBox.Text = (UInt16.Parse(GetEnemyData(GameData.ShieldDropChanceHeader, enemyReader), System.Globalization.NumberStyles.HexNumber) / 255.0d * 100).ToString("0.00");

                    /* Accessory */
                    accBox.Items.Add(GameData.ItemNames[GetEnemyData(GameData.AccNameHeader, enemyReader)]);
                    accBox.SelectedIndex = 0;
                    accDropRateBox.Text = (UInt16.Parse(GetEnemyData(GameData.AccDropChanceHeader, enemyReader), System.Globalization.NumberStyles.HexNumber) / 255.0d * 100).ToString("0.00");

                    /* Bodypart HP / armors */
                    curEnemyBodyPartHPs[0] = Convert.ToUInt16(GetEnemyData(GameData.BodyPart0HPHeader, enemyReader), 16);
                    bp0NameBox.Items.Add(GameData.ItemNames[GetEnemyData(GameData.BodyPart0NameHeader, enemyReader)]);
                    bp0NameBox.SelectedIndex = 0;
                    bp0MatBox.Items.Add(GameData.Materials[GetEnemyData(GameData.BodyPart0MatHeader, enemyReader)]);
                    bp0MatBox.SelectedIndex = 0;
                    bp0DropBox.Text = (UInt16.Parse(GetEnemyData(GameData.BodyPart0DropChanceHeader, enemyReader), System.Globalization.NumberStyles.HexNumber) / 255.0d * 100).ToString("0.00");

                    curEnemyBodyPartHPs[1] = Convert.ToUInt16(GetEnemyData(GameData.BodyPart1HPHeader, enemyReader), 16);
                    bp1NameBox.Items.Add(GameData.ItemNames[GetEnemyData(GameData.BodyPart1NameHeader, enemyReader)]);
                    bp1NameBox.SelectedIndex = 0;
                    bp1MatBox.Items.Add(GameData.Materials[GetEnemyData(GameData.BodyPart1MatHeader, enemyReader)]);
                    bp1MatBox.SelectedIndex = 0;
                    bp1DropBox.Text = (UInt16.Parse(GetEnemyData(GameData.BodyPart1DropChanceHeader, enemyReader), System.Globalization.NumberStyles.HexNumber) / 255.0d * 100).ToString("0.00");

                    curEnemyBodyPartHPs[2] = Convert.ToUInt16(GetEnemyData(GameData.BodyPart2HPHeader, enemyReader), 16);
                    bp2NameBox.Items.Add(GameData.ItemNames[GetEnemyData(GameData.BodyPart2NameHeader, enemyReader)]);
                    bp2NameBox.SelectedIndex = 0;
                    bp2MatBox.Items.Add(GameData.Materials[GetEnemyData(GameData.BodyPart2MatHeader, enemyReader)]);
                    bp2MatBox.SelectedIndex = 0;
                    bp2DropBox.Text = (UInt16.Parse(GetEnemyData(GameData.BodyPart2DropChanceHeader, enemyReader), System.Globalization.NumberStyles.HexNumber) / 255.0d * 100).ToString("0.00");

                    curEnemyBodyPartHPs[3] = Convert.ToUInt16(GetEnemyData(GameData.BodyPart3HPHeader, enemyReader), 16);
                    bp3NameBox.Items.Add(GameData.ItemNames[GetEnemyData(GameData.BodyPart3NameHeader, enemyReader)]);
                    bp3NameBox.SelectedIndex = 0;
                    bp3MatBox.Items.Add(GameData.Materials[GetEnemyData(GameData.BodyPart3MatHeader, enemyReader)]);
                    bp3MatBox.SelectedIndex = 0;
                    bp3DropBox.Text = (UInt16.Parse(GetEnemyData(GameData.BodyPart3DropChanceHeader, enemyReader), System.Globalization.NumberStyles.HexNumber) / 255.0d * 100).ToString("0.00");

                    curEnemyBodyPartHPs[4] = Convert.ToUInt16(GetEnemyData(GameData.BodyPart4HPHeader, enemyReader), 16);
                    bp4NameBox.Items.Add(GameData.ItemNames[GetEnemyData(GameData.BodyPart4NameHeader, enemyReader)]);
                    bp4NameBox.SelectedIndex = 0;
                    bp4MatBox.Items.Add(GameData.Materials[GetEnemyData(GameData.BodyPart4MatHeader, enemyReader)]);
                    bp4MatBox.SelectedIndex = 0;
                    bp4DropBox.Text = (UInt16.Parse(GetEnemyData(GameData.BodyPart4DropChanceHeader, enemyReader), System.Globalization.NumberStyles.HexNumber) / 255.0d * 100).ToString("0.00");

                    curEnemyBodyPartHPs[5] = Convert.ToUInt16(GetEnemyData(GameData.BodyPart5HPHeader, enemyReader), 16);
                    bp5NameBox.Items.Add(GameData.ItemNames[GetEnemyData(GameData.BodyPart5NameHeader, enemyReader)]);
                    bp5NameBox.SelectedIndex = 0;
                    bp5MatBox.Items.Add(GameData.Materials[GetEnemyData(GameData.BodyPart5MatHeader, enemyReader)]);
                    bp5MatBox.SelectedIndex = 0;
                    bp5DropBox.Text = (UInt16.Parse(GetEnemyData(GameData.BodyPart5DropChanceHeader, enemyReader), System.Globalization.NumberStyles.HexNumber) / 255.0d * 100).ToString("0.00");

                    break;
                }
            }

            enemyReader.Close();
        }

        private void zoneBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            /* Parse enemies in zone */
            string enemy;
            uint numEnemies = 0;
            ClearCurrentEnemy();
            enemyBox.Items.Clear();
            enemyBox.ResetText();
            enemyBox.SelectedIndex = -1;
            enemyBox.Refresh();
            StreamReader enemyReader = File.OpenText(String.Format("{0}{1}{2}.ASM", zonesDir, Path.DirectorySeparatorChar, zoneBox.SelectedItem.ToString()));

            while ((enemy = GetEnemyData(GameData.EnemyNameHeader, enemyReader)) != null)
            {
                enemyBox.Items.Add(GameData.TranslateVSText(enemy));
                zoneEnemies[numEnemies] = enemy;
                numEnemies++;
            }

            if (enemyBox.Items.Count > 0)
                enemyBox.SelectedIndex = 0;

            enemyReader.Close();
        }

        private void saveEnemy_Click(object sender, EventArgs e)
        {
            savingLabel.Visible = true;
            savingLabel.Refresh();

            /* Loop through all ASM files and multipy each enemy's stat value */
            if (multModeCheckBox.Checked)
            {
                string asmPath = String.Format("{0}{1}", zonesDir, Path.DirectorySeparatorChar);
                string[] asmFiles = Directory.GetFiles(asmPath, "*.ASM", SearchOption.TopDirectoryOnly);

                foreach (string curZonePath in asmFiles)
                {
                    string[] currentZone = File.ReadAllLines(curZonePath);
                    StreamReader enemyReader = File.OpenText(curZonePath);
                    string curEnemy;
                    double newVal;

                    while ((curEnemy = GetEnemyData(GameData.EnemyNameHeader, enemyReader)) != null)
                    {
                        newVal = Math.Truncate(UInt16.Parse(GetEnemyData(GameData.HPHeader, enemyReader), System.Globalization.NumberStyles.HexNumber) * Double.Parse(hpBox.Text));
                        if (newVal > UInt16.MaxValue)
                            newVal = UInt16.MaxValue;

                        SetEnemyData(currentZone, curEnemy, GameData.HPHeader, UInt16.Parse(newVal.ToString()).ToString("X4"));

                        newVal = Math.Truncate(UInt16.Parse(GetEnemyData(GameData.MPHeader, enemyReader), System.Globalization.NumberStyles.HexNumber) * Double.Parse(mpBox.Text));
                        if (newVal > UInt16.MaxValue)
                            newVal = UInt16.MaxValue;

                        SetEnemyData(currentZone, curEnemy, GameData.MPHeader, UInt16.Parse(newVal.ToString()).ToString("X4"));

                        newVal = Math.Truncate(Byte.Parse(GetEnemyData(GameData.STRHeader, enemyReader), System.Globalization.NumberStyles.HexNumber) * Double.Parse(strBox.Text));
                        if (newVal > Byte.MaxValue)
                            newVal = Byte.MaxValue;

                        SetEnemyData(currentZone, curEnemy, GameData.STRHeader, Byte.Parse(newVal.ToString()).ToString("X2"));

                        newVal = Math.Truncate(Byte.Parse(GetEnemyData(GameData.INTHeader, enemyReader), System.Globalization.NumberStyles.HexNumber) * Double.Parse(intBox.Text));
                        if (newVal > Byte.MaxValue)
                            newVal = Byte.MaxValue;

                        SetEnemyData(currentZone, curEnemy, GameData.INTHeader, Byte.Parse(newVal.ToString()).ToString("X2"));

                        newVal = Math.Truncate(Byte.Parse(GetEnemyData(GameData.AGIHeader, enemyReader), System.Globalization.NumberStyles.HexNumber) * Double.Parse(agiBox.Text));
                        if (newVal > Byte.MaxValue)
                            newVal = Byte.MaxValue;

                        SetEnemyData(currentZone, curEnemy, GameData.AGIHeader, Byte.Parse(newVal.ToString()).ToString("X2"));

                        newVal = Math.Truncate(Byte.Parse(GetEnemyData(GameData.RunSpeedHeader, enemyReader), System.Globalization.NumberStyles.HexNumber) * Double.Parse(runSpeedBox.Text));
                        if (newVal > Byte.MaxValue)
                            newVal = Byte.MaxValue;
                        SetEnemyData(currentZone, curEnemy, GameData.RunSpeedHeader, Byte.Parse(newVal.ToString()).ToString("X2"));

                        /* Update body hp values */
                        newVal = Math.Truncate(UInt16.Parse(GetEnemyData(GameData.BodyPart0HPHeader, enemyReader), System.Globalization.NumberStyles.HexNumber) * Double.Parse(hpBox.Text));
                        if (newVal > UInt16.MaxValue)
                            newVal = UInt16.MaxValue;
                        SetEnemyData(currentZone, curEnemy, GameData.BodyPart0HPHeader, UInt16.Parse(newVal.ToString()).ToString("X4"));

                        newVal = Math.Truncate(UInt16.Parse(GetEnemyData(GameData.BodyPart1HPHeader, enemyReader), System.Globalization.NumberStyles.HexNumber) * Double.Parse(hpBox.Text));
                        if (newVal > UInt16.MaxValue)
                            newVal = UInt16.MaxValue;
                        SetEnemyData(currentZone, curEnemy, GameData.BodyPart1HPHeader, UInt16.Parse(newVal.ToString()).ToString("X4"));

                        newVal = Math.Truncate(UInt16.Parse(GetEnemyData(GameData.BodyPart2HPHeader, enemyReader), System.Globalization.NumberStyles.HexNumber) * Double.Parse(hpBox.Text));
                        if (newVal > UInt16.MaxValue)
                            newVal = UInt16.MaxValue;
                        SetEnemyData(currentZone, curEnemy, GameData.BodyPart2HPHeader, UInt16.Parse(newVal.ToString()).ToString("X4"));

                        newVal = Math.Truncate(UInt16.Parse(GetEnemyData(GameData.BodyPart3HPHeader, enemyReader), System.Globalization.NumberStyles.HexNumber) * Double.Parse(hpBox.Text));
                        if (newVal > UInt16.MaxValue)
                            newVal = UInt16.MaxValue;
                        SetEnemyData(currentZone, curEnemy, GameData.BodyPart3HPHeader, UInt16.Parse(newVal.ToString()).ToString("X4"));

                        newVal = Math.Truncate(UInt16.Parse(GetEnemyData(GameData.BodyPart4HPHeader, enemyReader), System.Globalization.NumberStyles.HexNumber) * Double.Parse(hpBox.Text));
                        if (newVal > UInt16.MaxValue)
                            newVal = UInt16.MaxValue;
                        SetEnemyData(currentZone, curEnemy, GameData.BodyPart4HPHeader, UInt16.Parse(newVal.ToString()).ToString("X4"));

                        newVal = Math.Truncate(UInt16.Parse(GetEnemyData(GameData.BodyPart5HPHeader, enemyReader), System.Globalization.NumberStyles.HexNumber) * Double.Parse(hpBox.Text));
                        if (newVal > UInt16.MaxValue)
                            newVal = UInt16.MaxValue;
                        SetEnemyData(currentZone, curEnemy, GameData.BodyPart5HPHeader, UInt16.Parse(newVal.ToString()).ToString("X4"));
                    }

                    enemyReader.Close();
                    File.WriteAllLines(curZonePath, currentZone);
                }
            }

            /* Save single enemy */
            else
            {
                if (enemyBox.SelectedIndex > -1)
                {
                    /* Enemy stats are already validated at this point */
                    /* Read in current ASM file */
                    string asmPath = String.Format("{0}{1}{2}.ASM", zonesDir, Path.DirectorySeparatorChar, zoneBox.SelectedItem.ToString());
                    string[] currentZone = File.ReadAllLines(asmPath);

                    /* Write stats and drop rates for now */
                    SetEnemyData(currentZone, zoneEnemies[enemyBox.SelectedIndex], GameData.HPHeader, UInt16.Parse(hpBox.Text).ToString("X4"));
                    SetEnemyData(currentZone, zoneEnemies[enemyBox.SelectedIndex], GameData.MPHeader, UInt16.Parse(mpBox.Text).ToString("X4"));
                    SetEnemyData(currentZone, zoneEnemies[enemyBox.SelectedIndex], GameData.STRHeader, Byte.Parse(strBox.Text).ToString("X2"));
                    SetEnemyData(currentZone, zoneEnemies[enemyBox.SelectedIndex], GameData.INTHeader, Byte.Parse(intBox.Text).ToString("X2"));
                    SetEnemyData(currentZone, zoneEnemies[enemyBox.SelectedIndex], GameData.AGIHeader, Byte.Parse(agiBox.Text).ToString("X2"));
                    SetEnemyData(currentZone, zoneEnemies[enemyBox.SelectedIndex], GameData.RunSpeedHeader, Byte.Parse(runSpeedBox.Text).ToString("X2"));

                    SetEnemyData(currentZone, zoneEnemies[enemyBox.SelectedIndex], GameData.WeaponDropChanceHeader, ((Byte)Math.Truncate(double.Parse(weapDropRateBox.Text) / 100.0d * 255.0d)).ToString("X2"));
                    SetEnemyData(currentZone, zoneEnemies[enemyBox.SelectedIndex], GameData.ShieldDropChanceHeader, ((Byte)Math.Truncate(double.Parse(shieldDropRateBox.Text) / 100.0d * 255.0d)).ToString("X2"));
                    SetEnemyData(currentZone, zoneEnemies[enemyBox.SelectedIndex], GameData.AccDropChanceHeader, ((Byte)Math.Truncate(double.Parse(accDropRateBox.Text) / 100.0d * 255.0d)).ToString("X2"));
                    SetEnemyData(currentZone, zoneEnemies[enemyBox.SelectedIndex], GameData.BodyPart0DropChanceHeader, ((Byte)Math.Truncate(double.Parse(bp0DropBox.Text) / 100.0d * 255.0d)).ToString("X2"));
                    SetEnemyData(currentZone, zoneEnemies[enemyBox.SelectedIndex], GameData.BodyPart1DropChanceHeader, ((Byte)Math.Truncate(double.Parse(bp1DropBox.Text) / 100.0d * 255.0d)).ToString("X2"));
                    SetEnemyData(currentZone, zoneEnemies[enemyBox.SelectedIndex], GameData.BodyPart2DropChanceHeader, ((Byte)Math.Truncate(double.Parse(bp2DropBox.Text) / 100.0d * 255.0d)).ToString("X2"));
                    SetEnemyData(currentZone, zoneEnemies[enemyBox.SelectedIndex], GameData.BodyPart3DropChanceHeader, ((Byte)Math.Truncate(double.Parse(bp3DropBox.Text) / 100.0d * 255.0d)).ToString("X2"));
                    SetEnemyData(currentZone, zoneEnemies[enemyBox.SelectedIndex], GameData.BodyPart4DropChanceHeader, ((Byte)Math.Truncate(double.Parse(bp4DropBox.Text) / 100.0d * 255.0d)).ToString("X2"));
                    SetEnemyData(currentZone, zoneEnemies[enemyBox.SelectedIndex], GameData.BodyPart5DropChanceHeader, ((Byte)Math.Truncate(double.Parse(bp5DropBox.Text) / 100.0d * 255.0d)).ToString("X2"));

                    /* Update body hp values */
                    SetEnemyData(currentZone, zoneEnemies[enemyBox.SelectedIndex], GameData.BodyPart0HPHeader, curEnemyBodyPartHPs[0].ToString("X4"));
                    SetEnemyData(currentZone, zoneEnemies[enemyBox.SelectedIndex], GameData.BodyPart1HPHeader, curEnemyBodyPartHPs[1].ToString("X4"));
                    SetEnemyData(currentZone, zoneEnemies[enemyBox.SelectedIndex], GameData.BodyPart2HPHeader, curEnemyBodyPartHPs[2].ToString("X4"));
                    SetEnemyData(currentZone, zoneEnemies[enemyBox.SelectedIndex], GameData.BodyPart3HPHeader, curEnemyBodyPartHPs[3].ToString("X4"));
                    SetEnemyData(currentZone, zoneEnemies[enemyBox.SelectedIndex], GameData.BodyPart4HPHeader, curEnemyBodyPartHPs[4].ToString("X4"));
                    SetEnemyData(currentZone, zoneEnemies[enemyBox.SelectedIndex], GameData.BodyPart5HPHeader, curEnemyBodyPartHPs[5].ToString("X4"));

                    File.WriteAllLines(asmPath, currentZone);
                }
            }
            savingLabel.Visible = false;
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
            foreach (Control c in this.equipGroupBox.Controls)
            {
                if (c is ComboBox)
                {
                    ((ComboBox)c).Items.Clear();
                    ((ComboBox)c).ResetText();
                    ((ComboBox)c).SelectedIndex = -1;
                    ((ComboBox)c).Refresh();
                }
            }

            foreach (Control c in this.statsGroupBox.Controls)
            {
                if (c is TextBox)
                    ((TextBox)c).Clear();
            }
        }

        private string GetEnemyData(string statName, StreamReader reader)
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (line.Contains(statName))
                    return line.Substring(line.IndexOf('x') + 1);
            }
            return null;
        }

        private void SetEnemyData(string[] currentZone, string enemyName, string statName, string newVal)
        {
            /* Find enemy to edit */
            var enemyIndex = Array.FindIndex(currentZone, row => row.Contains(enemyName));

            /* Jump to enemy's stat to edit */
            while (!currentZone[++enemyIndex].Contains(statName)) ;

            /* Repalce data */
            currentZone[enemyIndex] = currentZone[enemyIndex].Replace(currentZone[enemyIndex].Substring(currentZone[enemyIndex].IndexOf('x') + 1), newVal);
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
    }
}