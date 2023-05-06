using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWorker
{
    internal class Node
    {
        /// <summary>
        /// Путь к файлу
        /// </summary>
        private string _path;

        /// <summary>
        /// Размер файла
        /// </summary>
        private int _size;

        /// <summary>
        /// Дата изменения
        /// </summary>
        private DateTime _date;

        /// <summary>
        /// Путь к файлу
        /// </summary>
        public string Path { get => _path;/* set => _path = value; */}

        /// <summary>
        /// Размер файла в Мб
        /// </summary>
        public int Size { get => _size; }

        /// <summary>
        /// Дата изменения
        /// </summary>
        public DateTime Date { get => _date; }

        ///// <summary>
        ///// Пустой конструктор
        ///// </summary>
        //public Node() { }

        /// <summary>
        /// Конструктор с параметрами
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <param name="size">Размер файла в Мб</param>
        /// <param name="date">Дата</param>
        public Node(string path, int size, DateTime date)
        {
            _path = path;
            _size = size;
            _date = date;
        }
    }
}
