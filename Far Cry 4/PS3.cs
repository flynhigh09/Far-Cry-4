using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PS3Lib.NET;

namespace Far_Cry_4
{
    public static class Ps3
	{
		static uint[] ProcessIDs;
		static uint ProcessID;
		public static void Connect()
		{
            PS3Lib.NET.PS3TMAPI.InitTargetComms();
            PS3Lib.NET.PS3TMAPI.Connect(0, null);
		}
		public static void TurnOFF()
		{
            PS3Lib.NET.PS3TMAPI.PowerOff(0, true);
		}

        public static string GetStatus()
        {
            PS3Lib.NET.PS3TMAPI.ConnectStatus state = new PS3Lib.NET.PS3TMAPI.ConnectStatus();
            string use = "";
            PS3Lib.NET.PS3TMAPI.GetConnectStatus(0, out state, out use);
            return state.ToString();
        }
        public static string GetIP()
        {
            PS3Lib.NET.PS3TMAPI.TCPIPConnectProperties ip = new PS3Lib.NET.PS3TMAPI.TCPIPConnectProperties();
            PS3Lib.NET.PS3TMAPI.GetConnectionInfo(0, out ip);
            return ip.IPAddress.ToString();
        }
        public static string GetGame()
        {
            PS3TMAPI.ProcessInfo infos = new PS3TMAPI.ProcessInfo();
            PS3TMAPI.GetProcessInfo(0, ProcessID, out infos);
            string[] str = infos.Hdr.ELFPath.Split('/');
            string ID = str[3];
            try
            {
                System.Net.WebClient seeker = new System.Net.WebClient();
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                string content = seeker.DownloadString("https://a0.ww.np.dl.playstation.net/tpl/np/" + ID + "/" + ID + "-ver.xml").Replace("<TITLE>", ";");
                string name = content.Split(';')[1].Replace("</TITLE>", ";");
                return name.Split(';')[0];
            }
            catch
            {
                return ID;
            }
        }

		public static void Pause()
		{
            PS3Lib.NET.PS3TMAPI.GetProcessList(0, out ProcessIDs);
			ulong uProcess = ProcessIDs[0];
			ProcessID = Convert.ToUInt32(uProcess);
            PS3Lib.NET.PS3TMAPI.ProcessAttach(0, PS3Lib.NET.PS3TMAPI.UnitType.PPU, ProcessID);
		}
		public static void Continue()
		{
			PS3Lib.NET.PS3TMAPI.GetProcessList(0, out ProcessIDs);
			ulong uProcess = ProcessIDs[0];
			ProcessID = Convert.ToUInt32(uProcess);
            PS3Lib.NET.PS3TMAPI.ProcessAttach(0, PS3Lib.NET.PS3TMAPI.UnitType.PPU, ProcessID);
            PS3Lib.NET.PS3TMAPI.ProcessContinue(0, ProcessID);
		}
		public static void Attach()
		{
            PS3Lib.NET.PS3TMAPI.GetProcessList(0, out ProcessIDs);
			ulong uProcess = ProcessIDs[0];
			ProcessID = Convert.ToUInt32(uProcess);
            PS3Lib.NET.PS3TMAPI.ProcessAttach(0, PS3Lib.NET.PS3TMAPI.UnitType.PPU, ProcessID);
            PS3Lib.NET.PS3TMAPI.ProcessContinue(0, ProcessID);
		}

		public static void SetMemory(uint Address, byte[] Bytes, uint thread = 0)
		{
            PS3Lib.NET.PS3TMAPI.ProcessSetMemory(0, PS3Lib.NET.PS3TMAPI.UnitType.PPU, ProcessID, thread, Address, Bytes);
		}
		public static byte[] GetMemory(uint Address, int length, uint thread = 0)
		{
            byte[] mem = new byte[length];
            PS3Lib.NET.PS3TMAPI.ProcessGetMemory(0, PS3Lib.NET.PS3TMAPI.UnitType.PPU, ProcessID, thread, Address, ref mem);
            return mem;
		}
        public static void ReadBytes(uint address, byte[] mem)
        {
            int memory = BitConverter.ToInt32(mem, 0);
            GetMemory(address, memory);
        }
        public static double ReadDouble(uint address)
        {
            byte[] memory = GetMemory(address, 8);
            Array.Reverse(memory, 0, 8);
            return BitConverter.ToDouble(memory, 0);
        }
        public static double[] ReadDouble(uint address, int length)
        {
            byte[] memory = GetMemory(address, length * 8);
            Reverse(memory);
            double[] numArray = new double[length];
            for (int i = 0; i < length; i++)
            {
                numArray[i] = BitConverter.ToSingle(memory, ((length - 1) - i) * 8);
            }
            return numArray;
        }
        public static float ReadFloat(uint address)
        {
            byte[] buff = GetMemory(address, 4, 0);
            Array.Reverse(buff);
            float val = BitConverter.ToSingle(buff, 0);
            return val;
        }
        public static int ReadInt(uint address)
        {
            byte[] buff = GetMemory(address, 4, 0);
            Array.Reverse(buff);
            int val = BitConverter.ToInt32(buff, 0);
            return val;
        }
        public static uint ReadUInt(uint address)
        {
            byte[] buff = GetMemory(address, 4, 0);
            Array.Reverse(buff);
            uint val = BitConverter.ToUInt32(buff, 0);
            return val;
        }
        public static short ReadShort(uint address, bool dvar = false)
        {
            byte[] buff = GetMemory(address, 2, 0);
            if (!dvar) { Array.Reverse(buff); }
            short val = BitConverter.ToInt16(buff, 0);
            return val;
        }
        public static byte ReadByte(uint address)
        {
            byte[] buff = GetMemory(address, 1, 0);
            return buff[0];
        }
        public static string ReadString(uint address)
        {
            int length = 0;
            for (int i = 0; i < 5000; i++)
            {
                byte buffer = GetMemory(address + (uint)i, 1)[0];
                if (buffer == (byte)0x00) { length = i; break; }
            }
            byte[] buff = GetMemory(address, length, 0);
            return Encoding.ASCII.GetString(buff);
        }
        public static byte[] Reverse(byte[] buff)
		{
			Array.Reverse(buff);
			return buff;
		}
        public static void WriteBool(uint address, bool input)
        {
            byte[] Bytes = new byte[1] { input ? (byte)1 : (byte)0 };
            SetMemory(address, Bytes);
        }
        public static void WriteByte(uint address, byte input)
        {
            SetMemory(address, new byte[1] { input });
        }
        public static void WriteBytes(uint address, byte[] input)
        {
            SetMemory(address, input);
        }
        public static bool WriteBytesToggle(uint Offset, byte[] On, byte[] Off)
        {
            bool flag = ReadByte(Offset) == (int)On[0];
            WriteBytes(Offset, !flag ? On : Off);
            return flag;
        }
        public static void WriteDouble(uint address, double input)
        {
            byte[] Bytes = new byte[8];
            BitConverter.GetBytes(input).CopyTo(Bytes, 0);
            Array.Reverse(Bytes, 0, 8);
            SetMemory(address, Bytes);
        }
        public static void WriteDouble(uint address, double[] input)
        {
            int length = input.Length;
            byte[] Bytes = new byte[length * 8];
            for (int index = 0; index < length; ++index)
               Reverse(BitConverter.GetBytes(input[index])).CopyTo(Bytes, index * 8);
            SetMemory(address, Bytes);
        }
		public static void WriteFloat(uint address, float val)
		{
            SetMemory(address, Reverse(BitConverter.GetBytes(val)), 0);
		}
		public static void WriteInt(uint address, int val)
		{
            SetMemory(address, Reverse(BitConverter.GetBytes(val)), 0);
		}
		public static void WriteShort(uint address, int val, bool dvar = false)
		{
			byte[] data = BitConverter.GetBytes(val);
			if (!dvar)
                SetMemory(address, new byte[] { data[0], data[1] }, 0);
			else
                SetMemory(address, new byte[] { data[1], data[0] }, 0);
		}
		public static void WriteUInt(uint address, uint val)
		{
            SetMemory(address, Reverse(BitConverter.GetBytes(val)), 0);
		}
		public static void WriteString(uint address, string txt)
		{
            SetMemory(address, Encoding.ASCII.GetBytes(txt + "\0"), 0);
		}
	}    
}