using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksListByElliot1
{
    class App
    {
        private List<string> tasks = new List<string>();
        private List<bool> isActioned = new List<bool>();

        private int selectedTask = 0;

        const int pageLength = 25;

        public App()
        {
            Console.OutputEncoding = Encoding.Unicode;

            ReadListFromFile();

           
        }


        public void Run()
        {
           

            bool quit;

            do
            {
                RemoveFirstActionedItems();
                PrintTaskList();
              
                var key = RunInputCycle();
                quit = HandleUserInput(key);

            } while (!quit);

            WriteListToFile();

            Console.WriteLine();
        }

        private void RemoveFirstActionedItems()
        {


            while (isActioned[0])
            {

                tasks.RemoveAt(0);
                isActioned.RemoveAt(0);
                selectedTask -= 1;

            }

            if (selectedTask < 0)
            {
                selectedTask = 0;
            }
        }
          

        private ConsoleKey RunInputCycle()
        {
            ConsoleKey key;

            PrintUsageOptions();
            key = GetInputFromUser();

            return key;

        }

        private bool HandleUserInput(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.A:
                    InputTaskToList();
                    break;
                case ConsoleKey.D:
                    DeleteCurrentlySelectedTask();
                    break;
                case ConsoleKey.N:
                    SelectNextPage();
                    break;
                case ConsoleKey.DownArrow:
                    SelectNextUnactionedTask();
                    break;
                case ConsoleKey.Enter:
                    WorkOnSelectTask();
                    break;
                case ConsoleKey.Q:
                    return true;
            }
            return false;



        }

        private void SelectNextPage()
        {
            var page = GetPage();
            selectedTask = FirstElementInPage(page + 1) - 1;

            
            SelectNextUnactionedTask();
        }

        private void WorkOnSelectTask()
        {
            bool valid = false;

            do
            {
                Console.Clear();
                Console.WriteLine($"Working on: {tasks[selectedTask]}");
                Console.WriteLine("r: re-enter | c: completed | q: cancel");
                Console.Write("Input: ");

                var key = GetInputFromUser();

                switch (key)
                {
                    case ConsoleKey.R:
                        AddTaskToList(tasks[selectedTask]);
                        DeleteCurrentlySelectedTask();
                        valid = true;
                        break;

                    case ConsoleKey.C:
                        DeleteCurrentlySelectedTask();
                        valid = true;
                        break;

                    case ConsoleKey.Q:
                        valid = true;
                        break;

                }
            } while (!valid);

        }

        private void DeleteCurrentlySelectedTask()
        {
            isActioned[selectedTask] = true;
            SelectNextUnactionedTask();
        }

        private void SelectNextUnactionedTask()
        {

            bool overflowed = false;

            do
            {
                selectedTask += 1;

                if (selectedTask >= isActioned.Count)
                {
                    selectedTask = 0;
                    overflowed = true;
                }

            } while (!overflowed && isActioned[selectedTask]);

        }

        private ConsoleKey GetInputFromUser()
        {
            return Console.ReadKey().Key;
        }

        private void PrintUsageOptions()
        {
            Console.WriteLine("a: add | d: delete | n: next page | \u2193: select | enter: action | q: quit");
            Console.Write("Input: ");
        }

        private void InputTaskToList()
        {
            Console.Clear();
            Console.WriteLine("Add a new task (empty to cancel): ");

            var input = Console.ReadLine();

            
        }

        private void AddTaskToList(string input)
        {
            if (!string.IsNullOrWhiteSpace(input))

                tasks.Add(input);
            isActioned.Add(false);
        }

        private void PrintTaskList()
        {
            Console.Clear();
            int page = GetPage();
            int startingPoint = FirstElementInPage(page);

            int endingPoint = FirstElementInPage(page + 1);

            for (int i = startingPoint; (i < endingPoint) && (i < tasks.Count); ++i)
            {
                if (isActioned[i])
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                }

                else if (i == selectedTask)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                }

                Console.WriteLine(tasks[i]);

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.BackgroundColor = ConsoleColor.Black;
            }

            Console.WriteLine();

        }

        private static int v(int page)
        {
            return FirstElementInPage(page + 1);
        }

        private static int FirstElementInPage(int page)
        {
            return page * pageLength;
        }

        private int GetPage()
        {
            return selectedTask / pageLength;
        }

        private void ReadListFromFile()
        {
            

            try
            {
                using (StreamReader sr = new StreamReader(@"C:\Users\ignacio\source\repos\TasksListByElliot1\TasksListByElliot1\bin\Debug\TaskList.txt"))
                {
                    while (!sr.EndOfStream)
                    {
                        var input = sr.ReadLine();
                        var splits = input.Split(new char[] { '\x1e' });

                        if(splits.Length == 2)
                        {
                            tasks.Add(splits[0]);
                            isActioned.Add(bool.Parse(splits[1]));
                        }


                       
                    }
                    

                } 
            }

            catch (FileNotFoundException)
            {
                {; }
            }

          
        }

        private void WriteListToFile()
        {
            using (StreamWriter sw = new StreamWriter(@"C:\Users\ignacio\source\repos\TasksListByElliot1\TasksListByElliot1\bin\Debug\TaskList.txt"))
            {
                for (int i = 0; i < tasks.Count; ++i)
                {
                    sw.WriteLine($"{tasks[i]}\x1e{isActioned[i]}");
                }
            }
        }
       
    }

    
}







