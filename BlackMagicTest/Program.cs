using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;
using Magic;

namespace BlackMagicTest
{
	class Program
	{
		//const uint CLIENT_CONNECTION = 0x011CA260;
		//const uint CURMGR_OFFSET = 0x2864;
		//const int FIRST_OBJECT = 0xAC;

		private const string PATTERN_CLIENT_CONNECTION = "EB 02 33 C0 8B 0D 00 00 00 00 64 8B 15 00 00 00 00 8B 34 8A 8B 0D 00 00 00 00 89 81 00 00 00 00";
		private const string MASK_CLIENT_CONNECTION = "xxxxxx????xxx????xxxxx????xx????";

		static void Main(string[] args)
		{
			uint dwCodeLoc;
			BlackMagic wow = new BlackMagic();
			if (wow.OpenProcessAndThread(SProcess.GetProcessFromProcessName("wow")))
			{
				Console.WriteLine(wow.GetModuleFilePath());
				DateTime dt = DateTime.Now;
				
				//dwCodeLoc = SPattern.FindPattern(wow.ProcessHandle, wow.MainModule, PATTERN_CLIENT_CONNECTION, MASK_CLIENT_CONNECTION, ' ');
				dwCodeLoc = wow.FindPattern(PATTERN_CLIENT_CONNECTION, MASK_CLIENT_CONNECTION);
				Console.WriteLine("Pattern found in {0}ms", DateTime.Now.Subtract(dt).TotalMilliseconds);
				Console.WriteLine("Code loc: 0x{0:X08}", dwCodeLoc);
				Console.WriteLine("CLIENT_CONNECTION: 0x{0:X08}", wow.ReadUInt(dwCodeLoc + 0x16));
				Console.WriteLine("CURMGR_OFFSET: 0x{0:X08}", wow.ReadUInt(dwCodeLoc + 0x1C));
				
			}
			else
			{
				Console.WriteLine("World of Warcraft could not be opened for read/write.");
			}

			Console.ReadLine();
		}
	}
}
