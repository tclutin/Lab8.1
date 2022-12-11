using System.Reflection;

namespace Lab8._1
{
    class Program
    {
        public static int width = 100;
        public static int height = 30;
        public static int time = 0;

        public static System.Timers.Timer timer;

        public static List<Data> listOfData = new List<Data>();

        static void Main(string[] args)
        {
            ReadFile();
            DrawScreen();
            SetTimer();
            Console.ReadLine();
        }

        public static void SetTimer()
        {
            timer = new System.Timers.Timer();

            timer.Interval = 1000;
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        public static void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            foreach (var item in listOfData)
            {
                if (item.firstTime == time) WriteWord(item);
                if (item.secondTime == time) DeleteWord(item);
            }
            time++;
        }

        public static void WriteWord(Data data)
        {
            ChangeColor(data.color);
            ChangePosition(data.position, data.word.Length);
            Console.WriteLine(data.word);
        }

        public static void DeleteWord(Data data)
        {
            ChangePosition(data.position, data.word.Length);
            for (int i = 0; i < data.word.Length; i++)
            {
                Console.Write(" ");
            }
        }

        public static void ChangePosition(string position, int lengthOfWord)
        {
            switch (position)
            {
                case "Top":
                    Console.SetCursorPosition((width - 2 - lengthOfWord) / 2, 1);
                    break;
                case "Bottom":
                    Console.SetCursorPosition((width - 2 - lengthOfWord) / 2, height - 2);
                    break;
                case "Right":
                    Console.SetCursorPosition(width - 1 - lengthOfWord, height / 2 - 1);
                    break;
                case "Left":
                    Console.SetCursorPosition(1, height / 2 - 1);
                    break;
                default:
                    Console.SetCursorPosition((width - lengthOfWord) / 2, height / 2);
                    break;
            }
        }

        public static void ChangeColor(string color)
        {
            if (color != null) color = color.Replace(" ", "");
            if (color == "Red") Console.ForegroundColor = ConsoleColor.Red;
            if (color == "Green") Console.ForegroundColor = ConsoleColor.Green;
            if (color == "White") Console.ForegroundColor = ConsoleColor.White;
            if (color == "Blue") Console.ForegroundColor = ConsoleColor.Blue;
        }

        public static void ReadFile()
        {
            char[] separators = { '-', '[', ',', ']' };
            foreach (var line in File.ReadLines("C:\\Users\\Lutin\\Desktop\\Lab5\\Lab8.1\\subtitle.txt")) 
            {
                string[] dataFromLine = line.Split(separators);
                ConvertToClass(dataFromLine);
            }

        }

        public static void ConvertToClass(string[] dataFromLine)
        {
            Data data = new Data() { firstTime = int.Parse(dataFromLine[0].Replace(":", "")) % 100 };
            if (dataFromLine.Length == 2)
            {
                data.secondTime = int.Parse(dataFromLine[1].Substring(0, 6).Replace(":", ""));
                data.word = dataFromLine[1].Substring(6);
            } 
            else
            {
                data.secondTime = int.Parse(dataFromLine[1].Replace(":", "")) % 100;
                data.position = dataFromLine[2];
                data.color = dataFromLine[3];
                data.word = dataFromLine[4];
            }
            listOfData.Add(data);
        }

        public static void DrawScreen()
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (i == 0 || i == height - 1) Console.Write("-");
                    else if (j == 0 || j == width - 1) Console.Write("|");
                    else Console.Write(" ");
                }
                Console.WriteLine();
            }
        }
    }
}