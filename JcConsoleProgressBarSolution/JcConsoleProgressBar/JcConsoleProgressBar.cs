using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JcConsoleProgressBarLib
{
    public class JcConsoleProgressBar
    {
        public JcConsoleProgressBar() 
        {
            
        }

        public JcConsoleProgressBar(int consoleTop, int consoleLeft)
        {
            ConsoleTop = consoleTop;
            ConsoleLeft = consoleLeft;
        }

        public JcConsoleProgressBar(string header, int valueDecimal = 2, double progress = 0.0d, double totalProgress = 100.0d, int consoleTop = 0, int consoleLeft = 0)
        {
            Header = header;
            ValueDecimal = valueDecimal;
            Progress = progress;
            TotalProgress = totalProgress;
            ConsoleTop = consoleTop;
            ConsoleLeft = consoleLeft;
        }

        public string Header { get; set; }

        public int ValueDecimal { get; set; } = 2;

        public double Progress { get; set; } = 0.0d;

        public double TotalProgress { get; set; } = 100.0d;  

        internal string ProgressMsg { get; private set; }

        public int ConsoleTop { get; set; }

        public int ConsoleLeft { get; set; }

        public char ProgressCharacter { get; set; } = '=';

        public int ProgressLength { get; set; } = 50;

        public ConsoleColor Foreground { get; set; } = ConsoleColor.White;

        public ConsoleColor Background { get; set; } = ConsoleColor.Black;

        public void Update()
        {
            var progressValue = Progress / TotalProgress;

            int value = (int)(ProgressLength * progressValue);

            var progressStr = new string(ProgressCharacter, value) + new string(' ', ProgressLength - value);

            var percentStr = Math.Round(100.0d * progressValue, ValueDecimal) + "%" + new string(' ', ValueDecimal);

            ProgressMsg = Header + '[' + progressStr + ']' + ' ' + percentStr;
        }

        public void Update(double progress)
        {
            Progress = progress;
            Update();
        }

        public void Update(double progress, double totalProgress)
        {
            Progress= progress;
            TotalProgress= totalProgress;

            this.Update();
        }

        public string ConsoleWrite()
        {

            var foregroundTemp = Console.ForegroundColor;

            var backgroundTemp = Console.BackgroundColor;
            try
            {
                Console.ForegroundColor = this.Foreground;

                Console.BackgroundColor = this.Background;

                Console.CursorVisible = false;

                Console.CursorTop = ConsoleTop;

                Console.CursorLeft = ConsoleLeft;

                Console.Write(ProgressMsg);

                return ProgressMsg;
            }
            finally 
            {
                Console.ForegroundColor = foregroundTemp;

                Console.BackgroundColor = backgroundTemp;
            }
            
        }
    }
}
