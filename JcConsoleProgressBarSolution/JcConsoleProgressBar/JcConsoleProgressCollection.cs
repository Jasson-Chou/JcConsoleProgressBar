using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JcConsoleProgressBarLib
{
    public  class JcConsoleProgressCollection : List<JcConsoleProgressBar>
    {
        public void ConsoleWriteAll()
        {
            foreach (JcConsoleProgressBar pb in this) 
            {
                pb.ConsoleWrite();
            }
        }

        public void ConsoleWriteRange(int start, int Count)
        {
            var enums =this.GetRange(start, Count);
            foreach(var pb in this) 
            {
                pb.ConsoleWrite();
            }
        }
    }
}
