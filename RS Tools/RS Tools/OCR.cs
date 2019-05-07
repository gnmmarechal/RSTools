using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS_Tools
{
    public abstract class OCRPlugin
    {
        public OCRPlugin()
        {

        }

        abstract public String PerformOCR(Bitmap bmp);
    }
}
