﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project1_frames
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            using (var game = new Game())
            {
                bool status = game.InitGL();
                if( status == false )
                {
                    return;
                }
                game.Run(60.0);
            }
        }
    }
}
