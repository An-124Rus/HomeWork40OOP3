using System;
using System.Collections.Generic;

namespace HomeWork40OOP3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string AddPlayerCommand = "1";
            const string BanPlayerCommand = "2";
            const string UnbanPlayerCommand = "3";
            const string DeletePlayerCommand = "4";
            const string ShowAllPlayersInfoCommand = "5";
            const string ExitCommand = "6";

            bool isWorking = true;

            Database database = new Database();

            while (isWorking)
            {
                Console.Clear();
                Console.WriteLine($"Для добавления игрока нажмите              - {AddPlayerCommand}");
                Console.WriteLine($"Забанить игрока по номеру нажмите          - {BanPlayerCommand}");
                Console.WriteLine($"Разбанить игрока по номеру нажмите         - {UnbanPlayerCommand}");
                Console.WriteLine($"Удалить игрока по номеру нажмите           - {DeletePlayerCommand}");
                Console.WriteLine($"Показать информацию о всех игроках нажмите - {ShowAllPlayersInfoCommand}");
                Console.WriteLine($"Для выхода нажмите                         - {ExitCommand}\n");
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case AddPlayerCommand:
                        database.AddPlayer();
                        break;

                    case BanPlayerCommand:
                        database.BanPlayer();
                        break;

                    case UnbanPlayerCommand:
                        database.UnbanPlayer();
                        break;

                    case DeletePlayerCommand:
                        database.DeletePlayer();
                        break;

                    case ShowAllPlayersInfoCommand:
                        database.ShowAllPlayersInfo();
                        break;

                    case ExitCommand:
                        isWorking = false;
                        break;
                }
            }
        }
    }

    class Player
    {
        public Player(int number, string nik, int level, bool isBanned = false)
        {
            Number = number;
            Nik = nik;
            Level = level;
            IsBanned = isBanned;
        }

        public int Number { get; private set; }
        public string Nik { get; private set; }
        public int Level { get; private set; }
        public bool IsBanned { get; private set; }

        public void ShowInfo()
        {
            string StatusBan = "Забанен";
            string StatusUnban = "Не забанен";

            Console.Write($"Номер: {Number}, ник: {Nik}, уровень: {Level}, ");

            if (IsBanned)
                Console.WriteLine($"статус: {StatusBan}.");
            else
                Console.WriteLine($"статус: {StatusUnban}.");
        }

        public void Ban()
        {
            IsBanned = true;
        }

        public void Unban()
        {
            IsBanned = false;
        }
    }

    class Database
    {
        private List<Player> _players = new List<Player>();
        private int number = 0;

        public void AddPlayer()
        {
            Console.Clear();

            number++;

            Console.Write("Введите ник: ");
            string nik = Console.ReadLine();

            int level = ParseNumber("Введите уровень: ");

            _players.Add(new Player(number, nik, level));
        }

        public void ShowAllPlayersInfo()
        {
            Console.Clear();

            for (int i = 0; i < _players.Count; i++)
                _players[i].ShowInfo();

            Console.ReadKey();
        }

        public void BanPlayer()
        {
            if (TryGetPlayer(out Player player))
            {
                player.Ban();
            }
            else
            {
                Console.WriteLine("Такого игрока нет. Попробуйте снова.");
                Console.ReadKey();
            }
        }

        public void UnbanPlayer()
        {
            if (TryGetPlayer(out Player player))
            {
                player.Unban();
            }
            else
            {
                Console.WriteLine("Такого игрока нет. Попробуйте снова.");
                Console.ReadKey();
            }
        }

        public void DeletePlayer()
        {
            if (TryGetPlayer(out Player player))
            {
                _players.Remove(player);
            }
            else
            {
                Console.WriteLine("Такого игрока нет. Попробуйте снова.");
                Console.ReadKey();
            }
        }

        private bool TryGetPlayer(out Player player)
        {
            Console.Clear();

            int number = ParseNumber("Введите никальный номер игрока: ");

            for (int i = 0; i < _players.Count; i++)
            {
                if (_players[i].Number == number)
                {
                    player = _players[i];
                    return true;
                }
            }

            player = null;
            return false;
        }

        private int ParseNumber(string mesage)
        {
            int number;

            Console.Write(mesage);
            string userInput = Console.ReadLine();

            while (int.TryParse(userInput, out number) == false)
            {
                Console.Write("Введено не чиcло. Введите только целое число: ");
                userInput = Console.ReadLine();
            }

            return number;
        }
    }
}
