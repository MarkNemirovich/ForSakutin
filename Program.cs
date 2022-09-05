using System;

namespace IMJunior
{
	class Program
	{
		private const int MIN_AGE = 18;
		private const int MAX_AGE = 99;
		private class Stats
		{
			private const int LIMIT = 10;
			public int Age;
			public int Strength { get; private set; }
			public int Agility { get; private set; }
			public int Intelligence { get; private set; }
			public int Points { get; private set; }
			public Stats()
			{
				Points = 25;
			}
			public void TryChangeState(string name, int value)
			{
				switch (name)
				{
					case "сила":
						Strength = TryChangeValue(Strength, value);
						break;
					case "ловкость":
						Agility = TryChangeValue(Agility, value);
						break;
					case "интеллект":
						Intelligence = TryChangeValue(Intelligence, value);
						break;
					default:
						break;
				}
			}
			private int TryChangeValue(int stateValue, int add)
			{
				if (add > 0)
				{
					var increaseValue = Math.Min(Math.Min(stateValue + add, LIMIT) - stateValue, Points);
					Points -= increaseValue;
					return stateValue + increaseValue;
				}
				if (add < 0)
				{
					var decreaseValue = stateValue - Math.Max(stateValue + add, 0);
					Points += decreaseValue;
					return stateValue - decreaseValue;
				}
				return stateValue;
			}
		}
		private static void Main(string[] args)
		{
			Introduction();
			Stats stats = new Stats();
			string[] consoleValues = new string[3];

			while (stats.Points > 0)
			{
				Console.Clear();
				consoleValues = FormatValuesForOutput(stats);
				Console.WriteLine($"Поинтов - {stats.Points}");
				Console.WriteLine($"Возраст - {stats.Age}\nСила - [{consoleValues[0]}]\nЛовкость - [{consoleValues[1]}]\nИнтеллект - [{consoleValues[2]}]");
				(string name, int value) changing = ChooseCharacteristic();
				stats.TryChangeState(changing.name, changing.value);
			}

			Console.Clear();
			Console.WriteLine("Вы распределили все очки. Введите возраст персонажа:");
			stats.Age = ChangeAge();
			consoleValues = FormatValuesForOutput(stats);
			Console.WriteLine($"Возраст - {stats.Age}\nСила - [{consoleValues[0]}]\nЛовкость - [{consoleValues[1]}]\nИнтеллект - [{consoleValues[2]}]");
		}

		private static void Introduction()
		{
			string text = "Добро пожаловать в меню выбора создания персонажа!\n" +
							"У вас есть 25 очков, которые вы можете распределить по умениям\n" +
							"Нажмите любую клавишу чтобы продолжить...\n";
			Console.Write(text);
			Console.ReadKey();
		}
		private static int ChangeAge()
		{
			while (true)
			{
				Console.WriteLine("Введите ваш возраст:");
				if (Int32.TryParse(Console.ReadLine(), out int inputAge))
				{
					if (inputAge > MIN_AGE && inputAge < MAX_AGE)
						return inputAge;
					else
						Console.WriteLine($"Только пользователям от {MIN_AGE} до {MAX_AGE} разрешено играть в эту игру.\n");
				}
				else
					Console.WriteLine($"Некорректное значение. Попробуйте ещё раз.\n");
			}
		}
		private static (string, int) ChooseCharacteristic()
		{
			string[] patterns = { "сила", "ловкость", "интеллект" };
			Console.WriteLine("Какую характеристику вы хотите изменить: сила, ловкость, интеллект?");
			string subject = Console.ReadLine().ToLower();
			foreach (string pattern in patterns)
				if (subject == pattern)
					return (pattern, ChooseOperator());
			return (default(string), default(int));
		}
		private static int ChooseOperator()
		{
			Console.WriteLine(@"Что вы хотите сделать? +\-");
			string sign;
			do
			{
				sign = Console.ReadLine();
			} while (sign != "+" && sign != "-");
			return ChooseValue(sign);
		}
		private static int ChooseValue(string operation)
		{
			Console.WriteLine(@"Колличество поинтов которые следует {0}", operation == "+" ? "прибавить" : "отнять");
			byte output = default(byte);
			while (byte.TryParse(Console.ReadLine(), out output) == false)
				;
			return (operation == "+") ? (int)output : -(int)output;
		}
		private static string[] FormatValuesForOutput(Stats source)
		{
			string[] output = new string[3];
			output[0] = "".PadLeft(source.Strength, '#').PadRight(10, '_');
			output[1] = "".PadLeft(source.Agility, '#').PadRight(10, '_');
			output[2] = "".PadLeft(source.Intelligence, '#').PadRight(10, '_');
			return output;
		}
	}
}