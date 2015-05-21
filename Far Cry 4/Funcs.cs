using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PS3Lib;
using System.Windows.Forms;
using System.Drawing;
using System.Net.Mail;
using System.Net;

namespace NameSpace//Change to your program's namespace
    
{
    public static class Funcs
    {
        private static PS3API PS3 = new PS3API();
        #region Scanner Thanks to Tustin and BadChoices!!
        public static int ScanBytes(byte[] bytesToScan, byte[] BytePattern, uint startOffset = 0, uint MaxSearchLength = 0x7fffffff)
        {
            {
                int result = -1;
                if (MaxSearchLength == 2147483647u)
                {
                    MaxSearchLength = (uint)bytesToScan.Length;
                }
                int num = (int)startOffset;
                while ((long)num < (long)((ulong)(MaxSearchLength - startOffset) - (ulong)((long)BytePattern.Length) - 1uL))
                {
                    if (bytesToScan[num] == BytePattern[0])
                    {
                        int num2 = 1;
                        while (num2 < BytePattern.Length && bytesToScan[num + num2] == BytePattern[num2])
                        {
                            num2++;
                            if (bytesToScan[num + num2] == BytePattern[num2] && num2 == BytePattern.Length - 1)
                            {
                                return (int)((ulong)startOffset + (ulong)((long)num));
                            }
                        }
                    }
                    num++;
                }
                return result;
            }
        }//Used to search for dynamic bytes. Thanks to Tustin!
        public static uint scan(byte[] ByteArray, uint range1, int range2, uint returnvalue, byte distance)
        {
            uint num = 0;
            byte[] bytePattern = ByteArray;
            for (uint i = range1; i < range2; i += 0x5000)
            {
                int num3 = ScanBytes(PS3.GetBytes(i, 0x5000), bytePattern, 0, 0x7fffffff);
                if (num3 != -1)
                {
                    num = ((uint)num3) + i;
                    returnvalue = (num - distance); // change - or + depending on location

                    return returnvalue;
                }
            }
            return num;
        } //requires user input in this Function.
        #endregion
        #region Toggleshit (Thanks to Shark!!!
        public static Boolean Toggle(UInt32 Address, Byte Val, Boolean State)
        {
            Byte Value = PS3.Extension.ReadByte(Address);
            PS3.Extension.WriteByte(Address, (Byte)(State ? Value - Val : Value + Val));
            return PS3.Extension.ReadByte(Address) > Value;
        } //part 1 of Toggle Switch
        public static void MakeToggle(CheckBox btn, UInt32 Address, Byte Val)
        {
            btn.ForeColor = Toggle(Address, Val, btn.ForeColor == Color.Green) ? Color.Green : Color.Red;
        } //part 2 of Toggle switch
        public static void ToggleSwitch(CheckBox CheckBox, Byte bytes, ComboBox cBox)
        {
            UInt32[] address = new UInt32[] { /*Offsets Goe Here*/ };
            MakeToggle(CheckBox, address[cBox.SelectedIndex], bytes);
        } //Toggles + adds bytes on top of existing bytes

        #endregion
        #region OffsetPrint Function (Thanks to Krank!!!)
        public static uint NumberOffsets;
        public static uint ZeroOffset;
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
        private static ulong Search(byte[] Search, uint Start, int Length, int bytes)
        {
            byte[] ReadBytes =

            PS3.Extension.ReadBytes(Start, Length);
            uint num = ContainsSequence

            (ReadBytes, Search, Start, bytes);
            if (num.Equals(ZeroOffset))
            {
                return 0;
                //not found
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
        public static void OffsetPrint(byte[] ByteArray, uint Range1, int Range2, Byte Length, Button Label)
        {
            byte[] bytes = ByteArray; //Just random Bytes as an example you use yours
            ulong Found = Search(bytes, Range1, Range2, 4); //bytes, Uint start , int Length, what bytes type to search 

            if (Found == ZeroOffset)
            {
                Label.Text = "NOT FOUND";
            }
            else
            {
                Label.Text = "Found at : " + string.Format("0x{0:X}", Found); // ("0x{0:X}", Found) defines the result and return it into text label // using that ulong Found = Search(bytes, 0x32500000, 0x200000, 4);
            }
        }
        #endregion
        public static void JumpFunc(uint address, byte[] bytes, int times, byte jump)
        {
            int i = 0;
            uint startoffset = address;
            byte[] num = bytes;
            while (i < times)
            {
                startoffset = startoffset + jump;
                PS3.Extension.WriteBytes(startoffset, num);
                PS3.SetMemory(address, num);
                i++;
            }
        }//Used if you need to write multiple offsets spaced apart.  Thanks to Tustin!
        public static void SIndexRead(UINt32[] Address, ComboBox CBox, byte[] bytes) 
        {
            PS3.Extension.WriteBytes(Address[CBox.SelectedIndex], bytes);
        }//For reading offset on selected index
        public static void SIndexWrite(uint address, ComboBox CBox)
        {
            var bytes = new byte[] 
            {
                /* Bytes Here */
            };
            PS3.Extension.WriteByte(address, bytes[CBox.SelectedIndex]);
        }//used to write byte on selected index of combobox ((input required))
        public static void TrackBarWrite(TrackBar trackbar, UInt32[] address, Label label, ComboBox CBox)
        {
            trackbar.Maximum = 20;
            PS3.Extension.WriteFloat(address[CBox.SelectedIndex], trackbar.Value);
            label.Text = "Current Value: " + trackbar.Value;
        } //used to write physics trackbar attached to combobox.((address uint in main form))
        public static void TrackBarCheck(TrackBar trackbar, UInt32[] address, Label label, ComboBox CBox)
        {
            trackbar.Value = (Int32)PS3.Extension.ReadFloat(address[CBox.SelectedIndex]);
            label.Text = "Current Value: " + trackbar.Value;
        }//Used to check physics trackbar attached to combobox((address uint in main form))
        public static void ConnectDEX(Label label1, Label label2, Label label3)
        {
            PS3.ChangeAPI(SelectAPI.TargetManager);
            bool state = PS3.ConnectTarget();
            label1.Text = state ? "Connected" : "Can't connect";
            label1.ForeColor = state ? Color.Blue : Color.Red;
            state = PS3.AttachProcess();
            label2.Text = state ? "Attached" : "Can't Attach";
            label2.ForeColor = state ? Color.Blue : Color.Red;
            label3.Text = "DEX";
        }//Connect/Attach to console (TMAPI)
        public static void ConnectCEX(Label label1, Label label2, Label label3)
        {
            PS3.ChangeAPI(SelectAPI.ControlConsole);
            bool state = PS3.ConnectTarget();
            label1.Text = state ? "Connected" : "Can't connect";
            label1.ForeColor = state ? Color.Blue : Color.Red;
            state = PS3.AttachProcess();
            label2.Text = state ? "Attached" : "Can't Attach";
            label2.ForeColor = state ? Color.Blue : Color.Red;
            label3.Text = PS3.CCAPI.GetFirmwareVersion();
        }//Connect/Attach to console (CCAPI)
        public static void num(UInt32 Address, NumericUpDown NumPad)
        {
            byte[] Value = BitConverter.GetBytes((Int16)NumPad.Value);
            Array.Reverse(Value);
            PS3.Extension.WriteBytes(Address, Value);
        } //NumericUpDown writing more than 255
}
}