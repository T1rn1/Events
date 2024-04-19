using System;
using System.Data;

internal class Program
{
    class Worker
    {
        public string Surname;
        private string Name;
        public string Post;
        public DateTime Year_of_employment;
        public double Salary;
        public int normal_employment;
        public double normal_Salary;

        public Worker(string surname, string name, string post, string year_of_employment, double salary)
        {
            Surname = surname;
            Name = name;
            Post = post;
            Year_of_employment = DateTime.Parse(year_of_employment);
            Salary = salary;
            normal_Salary = 50000;
            normal_employment = 3650;
        }
    }
    class Company
    {
        private List<Worker> Workers;
        private delegate void AccountHandler(string message);
        private event AccountHandler? Notify;
        public Company(List<Worker> workers)
        {
            Workers = workers;  
            Notify += Print;
        }
        private void Print(string message)
        {
            Console.WriteLine(message);
        }
        public void Check_Employment()
        {
            Console.WriteLine($"Работники, стаж работы которых превышает {Workers[0].normal_employment}:");
            DateTime now = DateTime.Now;
            foreach (Worker worker in Workers)
            {
                if (now.Subtract(worker.Year_of_employment).TotalDays > worker.normal_employment)
                {
                    Notify?.Invoke($"{worker.Surname} - {worker.Post}");
                }
            }
        }
        public void Check_Salary()
        {
            Console.WriteLine($"Работники, зарплата которых превышает {Workers[0].normal_Salary} руб.:");
            foreach (Worker worker in Workers)
            {
                if (worker.Salary > worker.normal_Salary)
                {
                    Notify?.Invoke($"{worker.Surname} - {worker.Post}");
                }
            }
        }
        public void Check_Post(string post)
        {
            Console.WriteLine($"Работники с должностью {post}:");
            foreach (Worker worker in Workers)
            {
                if (worker.Post == post)
                {
                    Notify?.Invoke($"{worker.Surname} - {worker.Post}");
                }
            }
        }
    }
    private static void Main(string[] args)
    {
        List<Worker> workers = new List<Worker>
        {
            new Worker("Иванов", "Иван", "Директор", "2010-01-01", 100000),
            new Worker("Петров", "Петр", "Зам. директора", "2011-01-01", 90000),
            new Worker("Сидоров", "Сидор", "Менеджер", "2002-01-01", 80000),
            new Worker("Алексеев", "Алексей", "Аналитик", "2013-01-01", 70000),
            new Worker("Васильев", "Василий", "Программист", "2014-01-01", 60000),
            new Worker("Николаев", "Николай", "Тестировщик", "2015-01-01", 50000),
            new Worker("Михайлов", "Михаил", "Дизайнер", "2016-01-01", 40000),
            new Worker("Андреев", "Андрей", "Маркетолог", "2007-01-01", 30000),
            new Worker("Александров", "Александр", "Секретарь", "2018-01-01", 20000),
            new Worker("Дмитриев", "Дмитрий", "Охранник", "2019-01-01", 10000)
        };
        Company company = new Company(workers);
        company.Check_Employment();
        Console.WriteLine();
        company.Check_Salary();
        Console.WriteLine();
        company.Check_Post("Программист");
    }
}