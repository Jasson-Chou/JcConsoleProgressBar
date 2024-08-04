using JcConsoleProgressBarLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using System.Timers;

namespace ConsoleDemo
{
    internal class Program
    {

        static JcConsoleProgressCollection jcProgresses = new JcConsoleProgressCollection();

        static bool isComplete = false;

        static void Main(string[] args)
        {
            const int DownloadCount = 5;

            // 30 fps
            Timer timer= new Timer(1 / 30.0d);

            Task[] tasks= new Task[DownloadCount];
            
            int CursorTop = Console.CursorTop;

            foreach(var index in Enumerable.Range(0, DownloadCount))
            {
                var cursorTopIndex = CursorTop + index;
                var progress = new JcConsoleProgressBar($"File[{index}]")
                {
                    ConsoleTop= cursorTopIndex,
                    ConsoleLeft = 0,
                };
                jcProgresses.Add(progress);
                tasks[index] = SimDownloadProgress(progress);
            }

            timer.AutoReset = false; 
            timer.Elapsed += Timer_Elapsed;
            timer.Start();


            Task.WaitAll(tasks);

            isComplete = true;

            jcProgresses.ConsoleWriteAll();

            Console.CursorTop = CursorTop + DownloadCount;

            Console.CursorLeft = 0;

            Console.WriteLine("Download Complete!");

            Console.WriteLine();

            Console.ReadLine();

        }

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var timer = sender as Timer;
            
            jcProgresses.ConsoleWriteAll();

            if(!isComplete)
                timer.Start();
        }

        static async Task SimDownloadProgress(JcConsoleProgressBar progressBar)
        {
            var downloadByteSize = (new Random(DateTime.Now.Ticks.GetHashCode())).Next(5, 4096);

            progressBar.TotalProgress = downloadByteSize;

            for(int downloadPorgress = 0; downloadPorgress< downloadByteSize; downloadPorgress++) 
            {
                // download delay
                await Task.Delay(10);
                
                progressBar.Update(downloadPorgress + 1);
            }

        }
    }
}
