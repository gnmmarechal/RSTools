﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS_Tools
{
    public interface RSToolsPlugin : RSToolsPluginBase
    {
        void Run(in Bitmap gameImage);
    }
}
