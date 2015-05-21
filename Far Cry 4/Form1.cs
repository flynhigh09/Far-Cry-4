using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PS3Lib;
using System.Globalization;
using System.Threading;
using System.IO;
using System.Reflection;

namespace Far_Cry_4
{   
    public partial class Form1 : Form
    {
        private static PS3API PS3 = new PS3API(SelectAPI.TargetManager);
        public bool Connected = false;
        public bool isCONNECTED = false;
        public bool isATTACHED = false;
        private int Api = 0;
        private static int NumberOffsets = 0;
        private static uint ZeroOffset;
        private bool Checkinout;
        private static uint XpMultiy = 0x010A2A54;

        private uint Customaddr = 0;
        private uint PlayerOffset = 0;

        public static uint gstart = 0u;
        public static uint Ammo = 0u;
        public static uint HaveBullets = 0u;
        public static uint infitems = 0u;
        public static uint Health = 0u;
        private static uint XP = 0u;
        private static uint Moneydecrease = 0u;
        private static uint moneygain = 0u;

        public static class Globals
        {
            public static string TITLE;
            public static string SERVER = @"http://flynhigh09.x10host.com/";

            public static string TEMPFOLDER = System.IO.Path.Combine(System.IO.Path.GetTempPath().ToString(), Guid.NewGuid().ToString()).ToString();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                Checkinout = true;
                Opacity = 0;
                ScreenTimer.Enabled = true;
            }
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (e.Cancel == true)
                return;
            if (Opacity > 0)
            {
                Checkinout = false;
                ScreenTimer.Enabled = true;
                e.Cancel = true;
            }
        }
        private void ScreenTimer_Tick(object sender, EventArgs e)
        {
            if (Checkinout == false)
            {
                Opacity -= (ScreenTimer.Interval / 750.0);
                if (Opacity > 0)
                    ScreenTimer.Enabled = true;
                else
                {
                    ScreenTimer.Enabled = false;
                    Close();
                }
            }
            else
            {
                Opacity += (ScreenTimer.Interval / 750.0);
                ScreenTimer.Enabled = (Opacity < 1.0);
                Checkinout = (Opacity < 1.0);
            }
        }

        private void AllButtons_MouseEnter(object sender, EventArgs e)
        {
            var currentButton = sender as Button;
            var name = currentButton.Name;
            currentButton.ForeColor = Color.Blue;
           // currentButton.BackgroundImage = Properties.Resources.ea_nhl15;
        }
        private void AllButtons_MouseLeave(object sender, EventArgs e)
        {
            var currentButton = sender as Button;
            var name = currentButton.Name;
            currentButton.ForeColor = Color.Maroon;
           // currentButton.BackgroundImage = Properties.Resources.nhl15;
        }

        public void EnableMods(bool active)
        {
            ModBox.Enabled = active;
        }
        public void NotConnected()
        {
            Connection.ForeColor = Color.OrangeRed;
            Connection.Text = ("Not Connected");
        }
        public void IsConnected()
        {
            Connection.ForeColor = Color.Chartreuse;
            Connection.Text = ("Connected");
        }
        public void NotAttached()
        {
            Attached.ForeColor = Color.OrangeRed;
            Attached.Text = ("Not Attached");
        }
        public void IsAttached()
        {
           Attached.ForeColor = Color.DodgerBlue;
           Attached.Text = ("Attached");
        }

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (var button in this.Controls.OfType<Button>())
            {
                button.MouseEnter += AllButtons_MouseEnter;
                button.MouseLeave += AllButtons_MouseLeave;
            }
            string VERSION = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            Globals.TITLE = this.Text; this.Text = this.Text + " - v" + VERSION;      
        }
        private void Form1_FormClosing(object sender, EventArgs e)
        {
            AddressTimer.Stop();
        }
        private void Connect_Click(object sender, EventArgs e)
        {
            try
            {
                if (Api == 1)
                {
                    Ps3.Connect(); Ps3.Attach();
                    PS3.ConnectTarget(); PS3.AttachProcess();
                    Game.Text = Ps3.GetGame(); EnableMods(true); IsConnected(); IsAttached();
                }
                else if (Api == 2)
                {
                    PS3.ConnectTarget(IPBox.Text);
                    PS3.AttachProcess(); EnableMods(true); IsConnected(); IsAttached();
                }
            }
            catch
            {
                NotConnected(); NotAttached();
            }
        }
        private void IPBox_TextChanged(object sender, EventArgs e)
        {
            if (IPBox.TextLength != 0)
            {
                Connect.Enabled = true;
            }
            else
            {
                Connect.Enabled = false;
            }
        }
        private void DEX_CheckedChanged(object sender, EventArgs e)
        {
           if (DEX.Checked)
            {
                PS3.ChangeAPI(SelectAPI.TargetManager);
                Api = 1;
                Connect.Enabled = true;
                Connect.Visible = true;
                IPBox.Enabled = false;
                IPBox.Visible = false;
            }
        }
        private void CEX_CheckedChanged(object sender, EventArgs e)
        {
            if (CEX.Checked)
            {
                PS3.ChangeAPI(SelectAPI.ControlConsole);
                Api = 2;
                IPBox.Enabled = true;
                IPBox.Visible = true;
            }
        }
        private void SaveAll_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Ini Files|*.ini|All Files|*.*";
                saveFileDialog.ShowDialog();
                if ((saveFileDialog.FileName) == "")
                {
                    MessageBox.Show("Closed");
                }
                if (!File.Exists((saveFileDialog.FileName)))
                {
                    using (File.Create((saveFileDialog.FileName))) ;
                }
                IniParser iniParser = new IniParser((saveFileDialog.FileName));
                iniParser.AddSetting("flynhigh09" + " " + Update.Value, "Health", Health.ToString("X"));
                iniParser.AddSetting("flynhigh09" + " " + Update.Value, "Ammo", Ammo.ToString("X"));
                iniParser.AddSetting("flynhigh09" + " " + Update.Value, "HaveBullets", HaveBullets.ToString("X"));
                iniParser.AddSetting("flynhigh09" + " " + Update.Value, "infitems", infitems.ToString("X"));
                iniParser.AddSetting("flynhigh09" + " " + Update.Value, "XP", XP.ToString("X"));
                iniParser.AddSetting("flynhigh09" + " " + Update.Value, "Moneygain", moneygain.ToString("X"));
                iniParser.AddSetting("flynhigh09" + " " + Update.Value, "Moneydecrease", Moneydecrease.ToString("X"));
                iniParser.SaveSettings();
                MessageBox.Show("You have Succesfully Saved the Addresses for TU");
            }
            catch
            {
                MessageBox.Show("Save Aborted!");
            }
        }
        private void LoadAll_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Ini Files|*.ini|All Files|*.*";
                openFileDialog.ShowDialog();
                IniParser iniParser = new IniParser(openFileDialog.FileName);
                string fileName = openFileDialog.FileName;
                Health = uint.Parse(iniParser.GetSetting("flynhigh09" + " " + Update.Value, "Health"), NumberStyles.HexNumber);
                Ammo = uint.Parse(iniParser.GetSetting("flynhigh09" + " " + Update.Value, "Ammo"), NumberStyles.HexNumber);
                HaveBullets = uint.Parse(iniParser.GetSetting("flynhigh09" + " " + Update.Value, "HaveBullets"), NumberStyles.HexNumber);
                infitems = uint.Parse(iniParser.GetSetting("flynhigh09" + " " + Update.Value, "infitems"), NumberStyles.HexNumber);
                XP = uint.Parse(iniParser.GetSetting("flynhigh09" + " " + Update.Value, "XP"), NumberStyles.HexNumber);
                moneygain = uint.Parse(iniParser.GetSetting("flynhigh09" + " " + Update.Value, "moneygain"), NumberStyles.HexNumber);
                Moneydecrease = uint.Parse(iniParser.GetSetting("flynhigh09" + " " + Update.Value, "Moneydecrease"), NumberStyles.HexNumber);
                AddressTimer.Start();
                SaveAll.Enabled = true;
            }
            catch
            {
                MessageBox.Show("Load Aborted!");
            }
        }
        private void InfHealth_CheckedChanged(object sender, EventArgs e)
        {
            if (InfHealth.Checked)
            {
                byte[] bytes_Found_branch_address = StringBAToBA("480000044E8000203F847AE1"); // Search For Bytes
                Health = Search(bytes_Found_branch_address, 0x00400000, 0x01000000, 4);
                byte[] InfAmmo = StringBAToBA("D04300104E8000203F847AE1");
                PS3.SetMemory(Health, InfAmmo);

                InfHealth.Text = "Inf Health Set";
            }
            if (!InfHealth.Checked)
            {
                byte[] bytes_Found_branch_address = StringBAToBA("D04300104E8000203F847AE1"); // Search For Bytes
                Health = Search(bytes_Found_branch_address, 0x00400000, 0x01000000, 4);

                byte[] InfAmmo = StringBAToBA("480000044E8000203F847AE1");
                PS3.SetMemory(Form1.Health, InfAmmo);

                InfHealth.Text = "Inf Health Off";
            }
        }
        private void InfAmmoNoReload_CheckedChanged(object sender, EventArgs e)
        {
            if (InfAmmoNoReload.Checked)
            {
                byte[] bytes_Found_branch_address = StringBAToBA("7C9F28107C0428004182002C80C30050"); // Search For Bytes
                Ammo = Search(bytes_Found_branch_address, 0x00700000, 0x01000000, 4);

                byte[] infammo = StringBAToBA("7CA42B787C0428004182002C80C30050");
                PS3.SetMemory(Ammo, infammo);

                InfAmmoNoReload.Text = "Inf Ammo Noreload Set";
            }
            if (!InfAmmoNoReload.Checked)
            {
                byte[] bytes_Found_branch_address = StringBAToBA("7CA42B787C0428004182002C80C30050"); // Search For Bytes
                Ammo = Search(bytes_Found_branch_address, 0x00700000, 0x01000000, 4);

                byte[] infammo = StringBAToBA("7C9F28107C0428004182002C80C30050");
                PS3.SetMemory(Ammo, infammo);

                InfAmmoNoReload.Text = "Inf Ammo Noreload Off";
            }
        }
        private void HaveBulletsonreload_CheckedChanged(object sender, EventArgs e)
        {

            if (!HaveBulletsonreload.Checked)
            {
                byte[] bytes_Found_branch_address = StringBAToBA("7C832810909D000038800001989E0021"); // Search For Bytes
                HaveBullets = Search(bytes_Found_branch_address, 0x00600000, 0x01000000, 4);

                byte[] InfAmmo = StringBAToBA("388003E7909D000038800001989E0021");
                PS3.SetMemory(HaveBullets, InfAmmo);

                HaveBulletsonreload.Text = "Have Bullets Noreload Set";
            }
            if (!HaveBulletsonreload.Checked)
            {
                byte[] bytes_Found_branch_address = StringBAToBA("388003E7909D000038800001989E0021"); // Search For Bytes
                HaveBullets = Search(bytes_Found_branch_address, 0x00600000, 0x01000000, 4);

                byte[] InfAmmo = StringBAToBA("7C832810909D000038800001989E0021");
                PS3.SetMemory(HaveBullets, InfAmmo);

                HaveBulletsonreload.Text = "Have Bullets Noreload Off";
            }
        }
        private void InfItemsmax_CheckedChanged(object sender, EventArgs e)
        {
            if (InfItemsmax.Checked)
            {
                byte[] bytes_Found_branch_address = StringBAToBA("7C9F201038A0000138C000007C8407B4"); // Search For Bytes
                infitems = Search(bytes_Found_branch_address, 0x00600000, 0x1000000, 4);

                byte[] Infitems = StringBAToBA("6000000038A0000138C000007C8407B4");
                PS3.SetMemory(infitems, Infitems);

                InfItemsmax.Text = "Inf Items Set";
            }
            if (!InfItemsmax.Checked)
            {
                byte[] bytes_Found_branch_address = StringBAToBA("6000000038A0000138C000007C8407B4"); // Search For Bytes
                infitems = Search(bytes_Found_branch_address, 0x00600000, 0x1000000, 4);

                byte[] Infitems = StringBAToBA("7C9F201038A0000138C000007C8407B4");
                PS3.SetMemory(infitems, Infitems);

                InfItemsmax.Text = "Inf Items Off";
            }
        }
        private void MoneyNeverDecreases_CheckedChanged(object sender, EventArgs e)
        {
            if (MoneyNeverDecreases.Checked)
            {
                byte[] bytes_Found_branch_address = StringBAToBA("7FFF18102C1F000040800008"); // Search For Bytes
                Moneydecrease = Search(bytes_Found_branch_address, 0x00600000, 0x01000000, 4);

                byte[] Moneyd = StringBAToBA("607F00002C1F000040800008");
                PS3.SetMemory(Moneydecrease, Moneyd);

                MoneyNeverDecreases.Text = "Money Wont decrease Set";
            }
            if (!MoneyNeverDecreases.Checked)
            {
                byte[] bytes_Found_branch_address = StringBAToBA("607F00002C1F000040800008"); // Search For Bytes
                Moneydecrease = Search(bytes_Found_branch_address, 0x00600000, 0x01000000, 4);

                byte[] Moneyd = StringBAToBA("7FFF18102C1F000040800008");
                PS3.SetMemory(Moneydecrease, Moneyd);

                MoneyNeverDecreases.Text = "Money Wont decrease Off";
            }
        }
        private void MaxMoneyOnGain_CheckedChanged(object sender, EventArgs e)
        {
            if (MaxMoneyOnGain.Checked)
            {
                byte[] bytes_Found_branch_address = StringBAToBA("4080000838C0000063830000"); // Search For Bytes
                moneygain = Search(bytes_Found_branch_address, 0x00600000, 0x01000000, 4);

                byte[] moneyg = StringBAToBA("3CC03B9A60C6C9FF63830000");
                PS3.SetMemory(moneygain, moneyg);

                MaxMoneyOnGain.Text = "Max Money Set";
            }
            if (!MaxMoneyOnGain.Checked)
            {
                byte[] bytes_Found_branch_address = StringBAToBA("3CC03B9A60C6C9FF63830000"); // Search For Bytes
                moneygain = Search(bytes_Found_branch_address, 0x00600000, 0x01000000, 4);

                byte[] moneyg = StringBAToBA("4080000838C0000063830000");
                PS3.SetMemory(moneygain, moneyg);

                MaxMoneyOnGain.Text = "Max Money Off";
            }
        }
        private void XPMultiplier_Click(object sender, EventArgs e)
        {
            byte[] bytes_Found_branch_address = StringBAToBA("609E0000887F00402C03000041820150"); // Search For Bytes
            XP = Search(bytes_Found_branch_address, 0x01000000, 0x01000000, 4);

            byte[] InfAmmo = StringBAToBA("1FC4");
            PS3.SetMemory(XP, InfAmmo);

            PS3.SetMemory(XP + 0x03, setxp());

            XPMultiplier.Text = "XPMultiplier Set";
        }     
        private byte[] setxp()
        {
            UInt32.Parse(XPBox.Text, NumberStyles.HexNumber);

            List<Byte> bytes = new List<Byte>();
            foreach (var splittedValue in XPBox.Text.Split(' '))
            {
               bytes.Add((byte)UInt32.Parse(splittedValue, NumberStyles.HexNumber));
            }
            return bytes.ToArray();             
        }

        #region Set All
        public void Getall()
        {
            NewThread(new Action(this.Bulletsreload));
            NewThread(new Action(this.Maxammo));
            NewThread(new Action(this.Items));
            NewThread(new Action(this.getHealth));
            NewThread(new Action(this.MoneyGain));
            NewThread(new Action(this.Moneydeacrese));
            NewThread(new Action(this.getXP));
        }
        private void SetAll_Click(object sender, EventArgs e)
        {
            Ammo = 0;
            HaveBullets = 0;
            infitems = 0;
            Health = 0;
            XP = 0;
            Moneydecrease = 0;
            moneygain = 0;
            gstart = 0U;
            AddressTimer.Start();
        }
        private void AddressTimer_Tick(object sender, EventArgs e)
        {
            if ((int)gstart == 0)
            {
                NewThread(new Action(Getall));
                ++gstart;
            }
            if ((int)Ammo > 65536U)
            {
                MoneyNeverDecreases.Enabled = true;
            }
            if ((int)HaveBullets > 65536U)
            {
                HaveBulletsonreload.Enabled = true;
            }
            if ((int)infitems > 65536U)
            {
                InfItemsmax.Enabled = true;
            }
            if ((int)Health > 65536U)
            {
                InfHealth.Enabled = true;
            }
            if ((int)moneygain > 65536U)
            {
                MaxMoneyOnGain.Enabled = true;
            }
            if ((int)Moneydecrease > 65536U)
            {
                MoneyNeverDecreases.Enabled = true;
            }
            if ((int)XP > 65536U)
            {
                XPMultiplier.Enabled = true;
            }
            if (Ammo <= 0U || HaveBullets <= 0U || (infitems <= 0U || Health <= 0U) || (moneygain <= 0U || Moneydecrease <= 0U || XP <= 0U))
            return;
        this.SaveAll.Enabled = true;
        this.SetAll.Text = "Set All Done";
        this.AddressTimer.Stop();
        }
        public void Bulletsreload()
        {
            byte[] bytes_Found_branch_address = StringBAToBA("7C832810909D000038800001989E0021"); // Search For Bytes
            HaveBullets = Search(bytes_Found_branch_address, 0x00690000, 0x00400000, 4);

            byte[] InfAmmo = StringBAToBA("388003E7909D000038800001989E0021");
            PS3.SetMemory(HaveBullets, InfAmmo);
        }
        public void Maxammo()
        {
            byte[] bytes_Found_branch_address = StringBAToBA("7C9F28107C0428004182002C80C30050"); // Search For Bytes
            Ammo = Search(bytes_Found_branch_address, 0x00700000, 0x00400000, 4);

            byte[] infammo = StringBAToBA("7CA42B787C0428004182002C80C30050");
            PS3.SetMemory(Ammo, infammo);
        }
        public void Items()
        {
            byte[] bytes_Found_branch_address = StringBAToBA("7C9F201038A0000138C000007C8407B4"); // Search For Bytes
            infitems = Search(bytes_Found_branch_address, 0x00690000, 0x00400000, 4);

            byte[] Infitems = StringBAToBA("6000000038A0000138C000007C8407B4");
            PS3.SetMemory(infitems, Infitems);
        }
        public void getHealth()
        {
            byte[] bytes_Found_branch_address = StringBAToBA("480000044E8000203F847AE1"); // Search For Bytes
            Health = Search(bytes_Found_branch_address, 0x00400000, 0x00400000, 4);

            byte[] InfAmmo = StringBAToBA("D04300104E8000203F847AE1");
            PS3.SetMemory(Health, InfAmmo);
        }
        public void MoneyGain()
        {
            byte[] bytes_Found_branch_address = StringBAToBA("4080000838C0000063830000"); // Search For Bytes
            moneygain = Search(bytes_Found_branch_address, 0x006A0000, 0x00400000, 4);

            byte[] InfAmmo = StringBAToBA("3CC03B9A60C6C9FF63830000");
            PS3.SetMemory(moneygain, InfAmmo);
        }
        public void Moneydeacrese()
        {
            byte[] bytes_Found_branch_address = StringBAToBA("7FFF18102C1F000040800008"); // Search For Bytes
            Moneydecrease = Search(bytes_Found_branch_address, 0x006A0000, 0x00400000, 4);

            byte[] InfAmmo = StringBAToBA("607F00002C1F000040800008");
            PS3.SetMemory(Moneydecrease, InfAmmo);
        }
        public void getXP()
        {
            byte[] bytes_Found_branch_address = StringBAToBA("609E0000887F00402C03000041820150"); // Search For Bytes
            XP = Search(bytes_Found_branch_address, 0x01000000, 0x01000000, 4);

            byte[] Infxp = StringBAToBA("1FC4");
            PS3.SetMemory(XP, Infxp);

            PS3.SetMemory(XP + 0x02, setxp());
        }
        #endregion

        #region ><>< Helpers ><><
        public void NewThread(Action task)
        {
            new Thread((() => task()))
            {
                Name = task.Method.Name
            }

            .Start();
        }
        public static string ByteAToString(byte[] ba)
        {
            string hex = BitConverter.ToString(ba);
            return hex.Replace("-", "");
        }
        private string StringFix(string h)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i <= h.Length - 2; i += 2)
            {
             sb.Append(Convert.ToString(Convert.ToChar(Int32.Parse(h.Substring(i, 2), System.Globalization.NumberStyles.HexNumber))));
            }
            sb.Replace((char)0x00, ' ');
            return sb.ToString();
        }
        public static byte[] StringBAToBA(string str)
        {
            if (str == null || (str.Length % 2) == 1)
                return new byte[0];
            byte[] ret = new byte[str.Length / 2];
            for (int x = 0; x < str.Length; x += 2)
                ret[x / 2] = byte.Parse(sMid(str, x, 2), System.Globalization.NumberStyles.HexNumber);
            return ret;
        }
        public static string sMid(string text, int index, int length)
        {
            if (length < 0)
                throw new ArgumentOutOfRangeException("length", length, "length must be > 0");
            else if (length == 0 || text.Length == 0)
                return "";
            else if (text.Length < (length + index))
                return text;
            else
                return text.Substring(index, length);
        }
        public static uint ContainsSequence(byte[] toSearch, byte[] toFind, uint StartOffset, int bytes)
        {
            for (int i = 0; (i + toFind.Length) < toSearch.Length; i += bytes)
            {
                bool flag = true;
                for (int j = 0; j < toFind.Length; j++)
                {
                    if (toSearch[i + j] != toFind[j])
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    NumberOffsets++;
                    int num3 = ((int)StartOffset) + i;
                    return (uint)num3;
                }
            }
            return 0;
        }
        public static uint Search(byte[] Search, uint Start, int Length, int bytes)
        {
            byte[] ReadBytes = PS3.Extension.ReadBytes(Start, Length);
            uint num = ContainsSequence(ReadBytes, Search, Start, bytes);
            if (num.Equals(ZeroOffset))
            {
                return 0;
            }
            else
            {
                int counter = 0;
                foreach (int value in Search)
                    if (value == 1) ++counter;
                uint num2 = num + ((uint)counter);
                return num2;
            }
        }
        public uint SpecifiedSearch(byte[] ToSearchIn, byte[] FindWhat, uint UsedOffsetToGetBytesFrom, int OffsetNumber)
        {
            int num1 = 0;
            uint num2 = 0U;
            int num3 = 0;
        label_14:
            while (num3 + FindWhat.Length < ToSearchIn.Length)
            {
                bool flag = true;
                int num4 = 0;
                int num5 = FindWhat.Length - 1;
                for (int index = num4; index <= num5; ++index)
                {
                    if ((int)ToSearchIn[num3 + index] != (int)FindWhat[index])
                    {
                        if (!false)
                        {
                            num3 += 4;
                            goto label_14;
                        }
                        else
                        {
                            ++num1;
                            num2 = UsedOffsetToGetBytesFrom + (uint)num3;
                            if (num1.Equals(OffsetNumber))
                                return num2;
                            num3 += 4;
                            goto label_14;
                        }
                    }
                }
                if (!flag)
                {
                    num3 += 4;
                }
                else
                {
                    ++num1;
                    num2 = UsedOffsetToGetBytesFrom + (uint)num3;
                    num3 += 4;
                    if (num1.Equals(OffsetNumber))
                        return num2;
                }
            }
            return num2;
        }
        public static byte[] strToArray(string hex)
        {
            string str = hex.Replace("0x", "").Replace(" ", "");
            if (str.Length % 2 == 1)
                throw new Exception("Binary cannot have an odd number of digits");
            byte[] numArray = new byte[str.Length / 2];
            string[] strArray = hex.Split(new char[1]
            {
                ' '
            });
            int index = 0;
            foreach (string s in strArray)
            {
                numArray[index] = (byte)int.Parse(s, NumberStyles.HexNumber);
                ++index;
            }
            return numArray;
        }
        public uint CombineHl(uint startOffset, uint High, uint Low, bool take1 = false)
        {
            uint num1 = 2U + startOffset + High;
            uint num2 = 2U + startOffset + Low;
            byte[] numArray1 = new byte[2];
            byte[] numArray2 = new byte[2];
            if (Api == 1)
                Ps3.ReadBytes(num1, numArray1);
            Ps3.ReadBytes(num2, numArray2);
            if (Api == 2)
                PS3.CCAPI.GetMemory(num1, numArray1);
            PS3.CCAPI.GetMemory(num2, numArray2);
            string str1 = numArray1[0].ToString("X");
            if (take1)
                --numArray1[1];
            string str2 = numArray1[1].ToString("X");
            string str3 = numArray2[0].ToString("X");
            string str4 = numArray2[1].ToString("X");
            if (str1.Length == 1)
                str1 = "0" + numArray1[0].ToString("X");
            if (str2.Length == 1)
                str2 = "0" + numArray1[1].ToString("X");
            if (str3.Length == 1)
                str3 = "0" + numArray2[0].ToString("X");
            if (str4.Length == 1)
                str4 = "0" + numArray2[1].ToString("X");
            return (uint)int.Parse(str1 + str2 + str3 + str4, NumberStyles.HexNumber);
        }
        public static uint FindAddress(string UniqueBytes, uint startOffset = 1694498816U, uint difference = 0U, uint maxoffset = 2415919104U, uint skip = 4U, uint size = 65536U)
        {
            byte[] numArray1 = strToArray(UniqueBytes);
            int length1 = UniqueBytes.Replace(" ", "").Length / 2;
            byte[] numArray2 = new byte[length1];
            uint num1 = 0U;
            while (num1 < maxoffset - startOffset)
            {
                byte[] length2 = new byte[(int)(IntPtr)size];
                Ps3.ReadBytes(startOffset + num1, length2);
                uint num2 = 0U;
                while (num2 < size - 4U)
                {
                    int num3 = length1;
                    for (int index = 0; index < length1; ++index)
                    {
                        if ((int)length2[(int)(IntPtr)(checked((long)(ulong)unchecked((long)num2 + (long)index)))] !=(int)numArray1[index])
                        {
                            --num3;
                            break;
                        }
                    }
                    if (num3 == length1)
                        return startOffset + num1 + num2 + difference;
                    num2 += skip;
                }
                num1 += size;
            }
            return 69U;
        }
        public static uint FindAddressCcapi(string UniqueBytes, uint startOffset = 1694498816U, uint difference = 0U, uint maxoffset = 2415919104U, uint skip = 4U, uint size = 65536U)
        {
            byte[] numArray1 = strToArray(UniqueBytes);
            int length1 = UniqueBytes.Replace(" ", "").Length / 2;
            byte[] numArray2 = new byte[length1];
            uint num1 = 0U;
            while (num1 < maxoffset - startOffset)
            {
                byte[] length2 = new byte[(int)(IntPtr)size];
                PS3.CCAPI.GetMemory(startOffset + num1, length2);
                uint num2 = 0U;
                while (num2 < size - 4U)
                {
                    int num3 = length1;
                    for (int index = 0; index < length1; ++index)
                    {
                        if (
                            (int)
                                length2[(int)(IntPtr)(checked((long)(ulong)unchecked((long)num2 + (long)index)))] !=
                            (int)numArray1[index])
                        {
                            --num3;
                            break;
                        }
                    }
                    if (num3 == length1)
                        return startOffset + num1 + num2 + difference;
                    num2 += skip;
                }
                num1 += size;
            }
            return 69U;
        }
        private static int ScanBytes(byte[] bytesToScan, byte[] BytePattern, uint startOffset = 0U, uint MaxSearchLength = 2415919103U, uint skip = 4U)
        {
            int num = -1;
            if ((int)MaxSearchLength == int.MaxValue)
                MaxSearchLength = (uint)bytesToScan.Length;
            for (int index1 = (int)startOffset;
                (long)index1 < (long)(MaxSearchLength - startOffset) - (long)BytePattern.Length - 1L;
                ++index1)
            {
                if ((int)bytesToScan[index1] == (int)BytePattern[0])
                {
                    int index2 = 1;
                    while (index2 < BytePattern.Length &&
                           (int)bytesToScan[index1 + index2] == (int)BytePattern[index2])
                    {
                        ++index2;
                        if ((int)bytesToScan[index1 + index2] == (int)BytePattern[index2] &&
                            index2 == BytePattern.Length - 1)
                            return (int)((long)startOffset + (long)index1);
                    }
                }
            }
            return num;
        }
        #endregion
    }
}
