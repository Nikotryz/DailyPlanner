namespace DailyPlanner;

public static class Command
{
    /* ФУНКЦИИ С ПОЛЬЗОВАТЕЛЯМИ */

    //Функция авторизации
    public static void SignIn()
    {
        Console.ForegroundColor = ConsoleColor.White; 
        
        Console.Write("ВВЕДИТЕ ЛОГИН: ");
        string login = Console.ReadLine();
        Console.Write("ВВЕДИТЕ ПАРОЛЬ: ");
        string password = Console.ReadLine();
        Console.Clear();

        User.id = DatabaseRequests.SignInQuery(login, password);
        
        if (User.id is not null) Console.WriteLine($"Авторизация прошла успешно. Ваш ID: {User.id}");
        else
        {
            Console.ForegroundColor = ConsoleColor.Red; 
            Console.WriteLine("Неправильно введён логин и/или пароль");
        }
    }
    
    //Функция регистрации
    public static void SignUp()
    {
        string login = null;
        string password = null;
        bool log = false;
        bool pas = false;
        
        while (log is false)
        {
            Console.ForegroundColor = ConsoleColor.White; 
            Console.Write("ПРИДУМАЙТЕ ЛОГИН (как минимум 3 символа): ");
            login = Console.ReadLine();
            Console.Clear();
            if (login.Length >= 3) log = true;
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("В логине меньше 3-х символов");
            }
        }
        if (DatabaseRequests.CheckLoginQuery(login) is false)
        {
            Console.ForegroundColor = ConsoleColor.Red; 
            Console.WriteLine("Такой логин уже существует");
        }
        else
        {
            while (pas is false)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("ПРИДУМАЙТЕ ПАРОЛЬ (как минимум 8 символов): ");
                password = Console.ReadLine();
                Console.Clear();
                if (password.Length >= 8) pas = true;
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red; 
                    Console.WriteLine("В пароле меньше 8-и символов");
                }
            }
            DatabaseRequests.SignUpQuery(login, password);
            User.id = DatabaseRequests.SignInQuery(login, password);
            Console.WriteLine("Вы успешно зарегистрировались");
        }
    }
    
    /* ФУНКЦИИ С ЗАДАЧАМИ */
    
    //Функция добавления новой задачи
    public static void AddTask()
    {
        try
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("ВВЕДИТЕ НАЗВАНИЕ ЗАДАЧИ: ");
            string name = Console.ReadLine();
            Console.Write("ВВЕДИТЕ ОПИСАНИЕ ЗАДАЧИ (можете оставить поле пустым): ");
            string description = Console.ReadLine();
            Console.Write("ВВЕДИТЕ ДЕДЛАЙН ЗАДАЧИ (YYYY.MM.DD): ");
            string deadline = Console.ReadLine();
            
            DatabaseRequests.AddTaskQuery(name, description, DateTime.Parse(deadline));
            
            Console.WriteLine("\nЗАДАЧА УСПЕШНО ДОБАВЛЕНА");
        }
        catch (FormatException)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("ОШИБКА. НЕПРАВИЛЬНЫЙ ФОРМАТ ВВОДА");
        }
    }
    
    //Функция удаления задачи
    public static void DeleteTask()
    {
        try
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("ВВЕДИТЕ ID ЗАДАЧИ: ");
            int id = int.Parse(Console.ReadLine());
            
            DatabaseRequests.DeleteTaskQuery(id);
            
            Console.WriteLine("\nЗАДАЧА УСПЕШНО УДАЛЕНА");
        }
        catch (FormatException)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("ОШИБКА. НЕПРАВИЛЬНЫЙ ФОРМАТ ВВОДА");
        }
    }
    
    //Функция изменения задачи
    public static void EditTask()
    {
        try
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("ВВЕДИТЕ ID ЗАДАЧИ: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("ВЫБЕРИТЕ ДЕЙСТВИЕ:\n[1] — Изменить название задачи\n[2] — Изменить описание задачи\n[3] — Изменить дедлайн задачи\n\nВВЕДИТЕ ДЕЙСТВИЕ: ");
            int action = int.Parse(Console.ReadLine());

            switch (action)
            {
                case 1:
                    Console.Write("ВВЕДИТЕ НОВОЕ НАЗВАНИЕ: ");
                    string name = Console.ReadLine();
                    DatabaseRequests.EditTaskNameQuery(id, name);
                    Console.WriteLine("\nНАЗВАНИЕ ОБНОВЛЕНО");
                    break;
                case 2:
                    Console.Write("ВВЕДИТЕ НОВОЕ ОПИСАНИЕ: ");
                    string description = Console.ReadLine();
                    DatabaseRequests.EditTaskDescriptionQuery(id, description);
                    Console.WriteLine("\nОПИСАНИЕ ОБНОВЛЕНО");
                    break;
                case 3:
                    Console.Write("ВВЕДИТЕ НОВЫЙ ДЕДЛАЙН (YYYY.MM.DD): ");
                    DateTime deadline = DateTime.Parse(Console.ReadLine());
                    DatabaseRequests.EditTaskDeadlineQuery(id, deadline);
                    Console.WriteLine("\nДЕДЛАЙН ОБНОВЛЕН");
                    break;
            }
        }
        catch (FormatException)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("ОШИБКА. НЕПРАВИЛЬНЫЙ ФОРМАТ ВВОДА");
        }
    }

    //Функция просмотра всех задач
    public static void GetAllTasks()
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("СПИСОК ВСЕХ ЗАДАЧ:");
        DatabaseRequests.GetAllTasksQuery();
    }
    
    //Функция просмотра прошедших задач
    public static void GetPastTasks()
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("СПИСОК ПРОШЕДШИХ ЗАДАЧ:");
        DatabaseRequests.GetPastTasksQuery();
    }
    
    //Функция просмотра предстоящих задач
    public static void GetUpcomingTasks()
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("СПИСОК ПРЕДСТОЯЩИХ ЗАДАЧ:");
        DatabaseRequests.GetUpcomingTasksQuery();
    }
    
    //Функция просмотра задач на сегодня
    public static void GetTodayTasks()
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("СПИСОК ЗАДАЧ НА СЕГОДНЯ:");
        DatabaseRequests.GetTodayTasksQuery();
    }
    
    //Функция просмотра задач на завтра
    public static void GetTomorrowTasks()
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("СПИСОК ЗАДАЧ НА ЗАВТРА:");
        DatabaseRequests.GetTomorrowTasksQuery();
    }
    
    //Функция просмотра задач на неделю
    public static void GetWeekTasks()
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("СПИСОК ЗАДАЧ НА НЕДЕЛЮ:");
        DatabaseRequests.GetWeekTasksQuery();
    }
}