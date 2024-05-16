using System;
using Npgsql;

namespace DailyPlanner;

public static class DatabaseRequests
{
    /* ЗАПРОСЫ С ПОЛЬЗОВАТЕЛЯМИ */

    //Запрос на авторизацию
    public static string SignInQuery(string login, string password)
    {
        var querySql = $"SELECT user_id FROM users WHERE login = '{login}' and password = '{password}'";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
        if (cmd.ExecuteScalar() is not null) return cmd.ExecuteScalar().ToString();
        return null;
    }
    
    //Проверка логина на идентичность
    public static bool CheckLoginQuery(string login)
    {
        var querySql = $"SELECT user_id FROM users WHERE login = '{login}'";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
        if (cmd.ExecuteScalar() is not null) return false;
        return true;
    }
    
    //Запрос на регистрацию
    public static void SignUpQuery(string login, string password)
    {
        var querySql = $"INSERT INTO users (login, password) VALUES ('{login}','{password}')";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
        cmd.ExecuteNonQuery();
    }
    
    
    /* ЗАПРОСЫ С ЗАДАЧАМИ */
    
    //Метод добавления новой задачи в БД
    public static void AddTaskQuery(string name, string description, DateTime deadline)
    {
        var querySql1 = $"INSERT INTO tasks (name, description, deadline) VALUES ('{name}', '{description}', '{deadline}')";
        using var cmd1 = new NpgsqlCommand(querySql1, DatabaseService.GetSqlConnection());
        cmd1.ExecuteNonQuery();
        
        var querySql2 = $"INSERT INTO user_tasks (user_id, task_id) " +
                             $"SELECT {int.Parse(User.id)}, max(task_id) from tasks";
        using var cmd2 = new NpgsqlCommand(querySql2, DatabaseService.GetSqlConnection());
        cmd2.ExecuteNonQuery();
    }
    
    //Метод удаления задачи из БД
    public static void DeleteTaskQuery(int id)
    {
        var querySql = $"DELETE FROM tasks WHERE task_id = (SELECT task_id FROM user_tasks JOIN tasks USING (task_id) WHERE tasks.task_id = {id} and user_id = {int.Parse(User.id)})";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
        cmd.ExecuteNonQuery();
    }
    
    //Метод изменения названия задачи в БД
    public static void EditTaskNameQuery(int id, string name)
    {
        var querySql = $"UPDATE tasks SET name = '{name}' WHERE task_id = (SELECT task_id FROM user_tasks JOIN tasks USING (task_id) WHERE tasks.task_id = {id} and user_id = {int.Parse(User.id)})";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
        cmd.ExecuteNonQuery();
    }
    
    //Метод изменения описания задачи в БД
    public static void EditTaskDescriptionQuery(int id, string description)
    {
        var querySql = $"UPDATE tasks SET description = '{description}' WHERE task_id = (SELECT task_id FROM user_tasks JOIN tasks USING (task_id) WHERE tasks.task_id = {id} and user_id = {int.Parse(User.id)})";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
        cmd.ExecuteNonQuery();
    }
    
    //Метод изменения дедлайна задачи в БД
    public static void EditTaskDeadlineQuery(int id, DateTime date)
    {
        var querySql = $"UPDATE tasks SET deadline = '{date}' WHERE task_id = (SELECT task_id FROM user_tasks JOIN tasks USING (task_id) WHERE tasks.task_id = {id} and user_id = {int.Parse(User.id)})";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
        cmd.ExecuteNonQuery();
    }
    
    //Метод получения всех задач из БД
    public static void GetAllTasksQuery()
    {
        var querySql = $"SELECT task_id, name, description, deadline FROM user_tasks " +
                            $"JOIN tasks USING (task_id) " +
                            $"WHERE user_id = {int.Parse(User.id)}";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine($"| ID: {reader[0]} | Название: {reader[1]} | Описание: {reader[2]} | Дедлайн: {reader[3]} |");
        }
    }
    
    //Метод получения прошедших задач из БД
    public static void GetPastTasksQuery()
    {
        var querySql = $"SELECT task_id, name, description, deadline FROM user_tasks " +
                            $"JOIN tasks USING (task_id) " +
                            $"WHERE user_id = {int.Parse(User.id)} AND deadline < '{DateTime.Now}'";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine($"| ID: {reader[0]} | Название: {reader[1]} | Описание: {reader[2]} | Дедлайн: {reader[3]} |");
        }
    }
    
    //Метод получения предстоящих задач из БД
    public static void GetUpcomingTasksQuery()
    {
        var querySql = $"SELECT task_id, name, description, deadline FROM user_tasks " +
                            $"JOIN tasks USING (task_id) " +
                            $"WHERE user_id = {int.Parse(User.id)} AND deadline > '{DateTime.Now}'";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine($"| ID: {reader[0]} | Название: {reader[1]} | Описание: {reader[2]} | Дедлайн: {reader[3]} |");
        }
    }
    
    //Метод получения задач на сегодня из БД
    public static void GetTodayTasksQuery()
    {
        var querySql = $"SELECT task_id, name, description, deadline FROM user_tasks " +
                            $"JOIN tasks USING (task_id) " +
                            $"WHERE user_id = {int.Parse(User.id)} AND deadline = '{DateTime.Today}'";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine($"| ID: {reader[0]} | Название: {reader[1]} | Описание: {reader[2]} | Дедлайн: {reader[3]} |");
        }
    }
    
    //Метод получения задач на завтра из БД
    public static void GetTomorrowTasksQuery()
    {
        var querySql = $"SELECT task_id, name, description, deadline FROM user_tasks " +
                            $"JOIN tasks USING (task_id) " +
                            $"WHERE user_id = {int.Parse(User.id)} AND deadline = '{DateTime.Today.AddDays(1)}'";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine($"| ID: {reader[0]} | Название: {reader[1]} | Описание: {reader[2]} | Дедлайн: {reader[3]} |");
        }
    }
    
    //Метод получения задач на неделю из БД
    public static void GetWeekTasksQuery()
    {
        var querySql = $"SELECT task_id, name, description, deadline FROM user_tasks " +
                            $"JOIN tasks USING (task_id) " +
                            $"WHERE user_id = {int.Parse(User.id)} AND deadline >= '{DateTime.Today}' AND deadline <= '{DateTime.Today.AddDays(7)}'";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine($"| ID: {reader[0]} | Название: {reader[1]} | Описание: {reader[2]} | Дедлайн: {reader[3]} |");
        }
    }
}