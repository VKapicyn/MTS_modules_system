﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    static class Portfolio
    {
        public struct p
        {
            public String ticker;
            public Double amount;
            public Char type;
        }

        static public List<p> portfel=new List<p>();
    }
}
