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
        private List<string> taskList;

        public App()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            taskList = ReadListFromFile();
        }


        public void Run()
        {
            taskList = ReadListFromFile();

            bool quit;

            do
            {
                PrintTaskList();
                var key = RunInputCycle();
                quit = HandleUserInput(key);

            } while (!quit);

            WriteListToFile();

            Console.WriteLine();
        }

        private ConsoleKey RunInputCycle()
        {
            ConsoleKey key;

            PrintUsageOptions();
            key = GetInputFromUsers();

            return key;

        }

        private bool HandleUserInput(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.A:
                    InputTaskListToList();
                    break;

                case ConsoleKey.B:

                    break;

                case ConsoleKey.N:

                    break;

                case ConsoleKey.DownArrow:

                    break;

                case ConsoleKey.Enter:

                    break;

                case ConsoleKey.Q:
                    return true;

            }
            return false;
        }

        private ConsoleKey GetInputFromUsers()
        {
            return Console.ReadKey().Key;
        }

        private void PrintUsageOptions()
        {
            Console.WriteLine("a: add | d: delete | n: next page | \u2193: select | enter: actions | q: quit");

        }

        private void InputTaskListToList()
        {
            Console.Clear();
            Console.WriteLine("Add a new task: ");
            var input = Console.ReadLine();
            taskList.Add(input);
        }

        private void PrintTaskList()
        {
            foreach(var t in taskList)
            {
                Console.WriteLine(t);
            }

            Console.WriteLine();

        }

        private List<string> ReadListFromFile()
        {
            var taskList = new List<string>();

            try
            {
                using (StreamReader sr = new StreamReader(@"C:\Users\ignacio\source\repos\TasksListByElliot1\TasksListByElliot1\bin\Debug\TaskList.txt"))
                {
                    var input = sr.ReadLine();
                    taskList.Add(input);

                } 
            }

            catch (FileNotFoundException)
            {
                {; }
            }

            return taskList;
        }

        private void WriteListToFile()
        {
            using (StreamWriter sw = new StreamWriter(@"C:\Users\ignacio\source\repos\TasksListByElliot1\TasksListByElliot1\bin\Debug\TaskList.txt"))
            {
                foreach (var t in taskList)
                {
                    Console.WriteLine(t);
                }

            }
        }
       
    }

    
}
