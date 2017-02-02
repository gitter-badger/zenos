﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using OS.Runtime;
using static OS.RuntimeImports;

namespace OS
{
    static unsafe class RuntimeImports
    {
        [RuntimeImport("InitializeModules"), MethodImpl(MethodImplOptions.InternalCall)]
        public static extern void InitializeModules(IntPtr modules, int count);
    }

    public static class Program
    {

        public static void Main()
        {
            // fake entry point for now or the optimizer will remove it
            EntryPoint(0);
        }

        static void Wait(int ms)
        {
            for (int i = 0; i < ms; i++)
            {
                WaitOneMilli();
            }
        }

        static void WaitOneMilli()
        {
            var x = 0L;
            for (int i = 0; i < 715827; i++)
            {
                x += i;
            }
        }

        static unsafe void EntryPoint(long heapBase)
        {
            // nice to haves:
            // static ctors
            // string support
            // new objects?
            Memory.Init(heapBase);
            RedHawk.Init();
            Screen.Init();
            Screen.Clear();
            DoInitModules();

            Wait(3000);
            Screen.Clear();
            Screen.ForegroundColor = ConsoleColor.White;
            Screen.BackgroundColor = ConsoleColor.Black;

            var net = ".NET";
            Screen.WriteLine(net);

            Wait(500);

            Screen.WriteLine("Lives");
            Wait(500);

            //TODO this doesnt work: var str = FormatProvider.FormatInt32(123456, null, null);
            Screen.WriteLine(string.Concat("a", net));
            
            var taco = new Taco(25);
            Screen.Write((uint)taco.Meat);
            Screen.WriteLine();

            Wait(1000);

            //Screen.WriteLine(Run());

            while (true) ;
        }

        static void DoInitModules()
        {
            //TODO initialize this on startup with real values AddrOf(__modules_a)
            InitializeModules(new IntPtr(0x0178000), 1);
        }

        static string Run()
        {
            var result = "";
            for (int i = 0; i < 10; i++)
            {
                result += i;
            }

            return result;
        }
    }

    class Taco
    {
        public int Meat { get; private set; }

        public Taco(int meat)
        {
            Meat = meat;
        }
    }

}
