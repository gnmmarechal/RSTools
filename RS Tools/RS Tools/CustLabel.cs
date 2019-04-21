using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RS_Tools
{
    public class CustLabel : Label
    {
        private TextRenderingHint _textRenderingHint = TextRenderingHint.SystemDefault;
        private SmoothingMode _smoothingMode = SmoothingMode.Default;

        public TextRenderingHint TextRenderingHint
        {
            get
            {
                return _textRenderingHint;
            }
            set
            {
                _textRenderingHint = value;
            }
        }


        public SmoothingMode SmoothingMode
        {
            get
            {
                return _smoothingMode;
            }
            set
            {
                _smoothingMode = value;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.TextRenderingHint = _textRenderingHint;
            base.OnPaint(e);
        }
    }
}
