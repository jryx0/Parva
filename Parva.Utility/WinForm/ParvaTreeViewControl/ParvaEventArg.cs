using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Parva.Utility.WinForm
{
    public class ParvaEventArg : EventArgs
    {
        public string EventName { set; get; }
        public ParvaTreeViewEnum ArgType { set; get; }

        public Object  ArgData { set; get; }
    }
}
