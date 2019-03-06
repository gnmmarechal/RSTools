using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS_Tools
{
    class Config
    {

        public Config(Display.POINT[] ChatScanner, Display.POINT[] HealthScanner, Display.POINT[] PrayerScanner)
        {
            this.ChatScanner = ChatScanner;
            this.HealthScanner = HealthScanner;
            this.PrayerScanner = PrayerScanner;
        }

        public Config(String fileName)
        {
            List<String> fileContents = new List<String>( File.ReadAllLines(fileName));
            Display.POINT tempVar, tempVar2;
            for (int i = 0; i < fileContents.Count; i++)
            {
                String[] tempSplit = fileContents.ElementAt(i).Split(' ');
                tempVar.X = Convert.ToInt32(tempSplit[0]);
                tempVar.Y = Convert.ToInt32(tempSplit[1]);
                tempVar2.X = Convert.ToInt32(tempSplit[2]);
                tempVar2.Y = Convert.ToInt32(tempSplit[3]);
                Display.POINT[] arr = { tempVar, tempVar2 };

                switch(i)
                {
                    case 0:
                        ChatScanner = arr;
                        break;
                    case 1:
                        HealthScanner = arr;
                        break;
                    case 2:
                        PrayerScanner = arr;
                        break;
                }
            }
        }

        public Display.POINT[] ChatScanner
        {
            get; set;
        }

        public Display.POINT[] HealthScanner
        {
            get; set;
        }

        public Display.POINT[] PrayerScanner
        {
            get; set;
        }

        override public string ToString()
        {
            return ChatScanner[0].X + " " + ChatScanner[0].Y + " " + ChatScanner[1].X + " " + ChatScanner[1].Y + "\n" +
                HealthScanner[0].X + " " + HealthScanner[0].Y + " " + HealthScanner[1].X + " " + HealthScanner[1].Y + "\n" +
                PrayerScanner[0].X + " " + PrayerScanner[0].Y + " " + PrayerScanner[1].X + " " + PrayerScanner[1].Y + "";
        }

    }
}
