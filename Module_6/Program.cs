using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Module_6
{
    internal class Program
    {
        private static string _filePathName = "employeesDB.csv";    //Путь и имя файла
        private static int _linesCount = 0;
        private static String[] _noteMetaData;

        private static void Main()
        {
            Console.SetWindowSize(250, 50);

            NoteMetaDataInit();
            LinesNumberInFile();

            Console.WriteLine("Справочник \"Сотрудники\"\n");
            Console.WriteLine("Нажмите \"1\", чтобы вывести данные по текущим сотрудникам на экран\n" +
                "Нажмите \"2\", чтобы добавить новую запись");

            char key = Console.ReadKey(true).KeyChar;

            if (char.ToLower(key) == '1')
            {
                ReadData();
            }
            else if (char.ToLower(key) == '2')
            {
                WriteData();
            }

            Console.ReadLine();
        }

        /// <summary>
        /// Инициализация массива метаданных, необходимых для создания записи по одному сотруднику. По необходимости можно добавлять новые данные
        /// </summary>
        private static void NoteMetaDataInit()
        {
            _noteMetaData = new string[]
            {
                "Ф.И.О. сотрудника: ",
                "Возраст сотрудника: ",
                "Рост сотрудника: ",
                "Дата рождения сотрудника: ",
                "Место рождения сотрудника: ",
            };
        }

        /// <summary>
        /// Считает количество строк в документе и записывает результат в переменную _linesCount
        /// </summary>
        private static void LinesNumberInFile()
        {
            if (File.Exists(_filePathName))     //Проверка на наличие файла
            {
                using (var streamReader = new StreamReader(_filePathName))
                {
                    while (!streamReader.EndOfStream)
                    {
                        if (streamReader.ReadLine() != null) _linesCount++;
                    }
                    Console.WriteLine(_linesCount);
                }

            }
        }

        /// <summary>
        /// Чтение файла и вывод на экран данных из файла
        /// </summary>
        private static void ReadData()
        {
            using (StreamReader streamReader = new StreamReader(_filePathName))
            {
                string line;

                while ((line = streamReader.ReadLine()) != null)
                {
                    string[] noteData = line.Split('#');
                    Console.Write("\n");
                    foreach (var item in noteData)
                    {
                        Console.Write($"\t{item,24}");
                    }

                }
            }
        }

        /// <summary>
        /// Создание новой записи в файл
        /// </summary>
        private static void WriteData()
        {
            using (StreamWriter streamWriter = new StreamWriter(_filePathName, true))
            {
                char key = 'д';

                do
                {
                    _linesCount++;
                    string note = string.Empty;
                    note += _linesCount.ToString();     //Номер строки

                    string writingDate = DateTime.Now.ToString();
                    note += $"#{writingDate}";     //Время и дата записи

                    for (int index = 0; index < _noteMetaData.Length; index++)
                    {
                        Console.Write(_noteMetaData[index]);
                        note += $"#{Console.ReadLine()}";
                    }

                    streamWriter.WriteLine(note);
                    Console.Write("Продожить н/д\n"); key = Console.ReadKey(true).KeyChar;

                } while (char.ToLower(key) == 'д');
            }
        }

    }
}
