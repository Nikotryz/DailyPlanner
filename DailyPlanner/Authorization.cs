namespace DailyPlanner;

public static class Authorization
{
    public static void SignUpIn()
    {
        while (User.id is null)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Войти или зарегестрироваться?\n[1] — Войти\n[2] — Регистрация\n\n> ");
            string action = Console.ReadLine();
            Console.Clear();
            switch (action)
            {
                case "1": Command.SignIn(); break;
                case "2": Command.SignUp(); break;
                default: Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Неправильный формат ввода\n"); break;
            }
        }
    }
}
public static class User
{
    public static string id { get; set; }
}