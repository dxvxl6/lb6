using System;
using System.Threading;
namespace lb6
{
    class AnimalThread
    {
        private Thread thread;
        private string name;
        private int priority;
        private int distanceCovered = 0;
        private static int finishLine = 100; 

        public AnimalThread(string name, ThreadPriority priority)
        {
            this.name = name;
            this.priority = (int)priority;
            thread = new Thread(run);
            thread.Priority = priority;
        }

        public void Start()
        {
            thread.Start();
        }
        public void Join()
        {
            thread.Join();
        }

        public void run()
        {
            Random random = new Random();
            while (distanceCovered < finishLine)
            {
                int distance = random.Next(1, 11);
                distanceCovered += distance;
                Console.WriteLine($"{name} пробежал{(distance > 1 ? " " : "а ")}{distance} метров. Всего: {distanceCovered} метров.");
                ManagePriorities();
                Thread.Sleep(500);
            }
            Console.WriteLine($"{name} достиг финиша!");
        }

        private void ManagePriorities()
        {        
            if (distanceCovered < finishLine / 2 || (distanceCovered < finishLine && distanceCovered % 20 == 0))
            {
                if (thread.Priority < ThreadPriority.Highest)
                {
                    thread.Priority++;
                    Console.WriteLine($"{name} увеличил уровень приоритета до {thread.Priority}");
                }
            }
        }
    }

    class RabbitAndTurtle
    {
        static void Main(string[] args)
        {
            AnimalThread rabbit = new AnimalThread("Кролик", ThreadPriority.Normal);
            AnimalThread turtle = new AnimalThread("Черепаха", ThreadPriority.BelowNormal);
            rabbit.Start();
            turtle.Start();
            rabbit.Join();
            turtle.Join();
            Console.WriteLine("Игра окончена!");
        }
    }
}
