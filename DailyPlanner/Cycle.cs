namespace DailyPlanner;

public static class Cycle
{
    public static void Loop()
    {
        bool flag = true;

        while (flag == true)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\nКОМАНДЫ ПРОСМОТРА:" +
                              "\n[1] — Просмотреть все задачи" +
                              "\n[2] — Просмотреть прошедшие задачи" +
                              "\n[3] — Просмотреть предстоящие задачи" +
                              "\n[4] — Просмотреть задачи на сегодня" +
                              "\n[5] — Просмотреть задачи на завтра" +
                              "\n[6] — Просмотреть задачи на неделю" +
                              "\n\nКОМАНДЫ УПРАВЛЕНИЯ:" +
                              "\n[7] — Добавить новую задачу" +
                              "\n[8] — Отредактировать задачу" +
                              "\n[9] — Удалить задачу" +
                              "\n\n[stop] — ЗАКРЫТЬ ПРОГРАММУ");
            Console.Write("\n> ");
            string action = Console.ReadLine();

            Console.ResetColor();
            Console.Clear();

            switch (action)
            {
                case "1": Command.GetAllTasks(); break;
                case "2": Command.GetPastTasks(); break;
                case "3": Command.GetUpcomingTasks(); break;
                case "4": Command.GetTodayTasks(); break;
                case "5": Command.GetTomorrowTasks(); break;
                case "6": Command.GetWeekTasks(); break;
                case "7": Command.AddTask(); break;
                case "8": Command.EditTask(); break;
                case "9": Command.DeleteTask(); break;
                case "stop": flag = false; break;
            }
        }
    }
}