using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.IO;
using System.Text.Json.Serialization;

namespace _1_inJSON
{
    /*
     * Необходимо разработать программу для записи информации о товаре в текстовый файл в формате json. 
     * 
     * Разработать класс для моделирования объекта «Товар». 
     * Предусмотреть члены класса «Код товара» (целое число), «Название товара» (строка), «Цена товара» (вещественное число). 
     * Создать массив из 5-ти товаров, значения должны вводиться пользователем с клавиатуры. 
     * Сериализовать массив в json-строку, сохранить ее программно в файл «Products.json».
     */
    class Program
    {
        static void Main(string[] args)
        {
            string filesName = "Products.json";
            int n = 5;
            Console.WriteLine("Введите {0} товаров:", n);
            Product[] productMas = new Product[n];
            for (int i = 0; i < n; i++)
            {
                Product product = new Product();
                Console.Write("Название {0} товара: ", i + 1);
                product.Name = Console.ReadLine();
                Console.Write("Код {0} товара: ", i + 1);
                try
                {
                    product.Code = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка! {0}", ex.Message);
                }
                Console.Write("Цену {0} товара: ", i + 1);
                try
                {
                    product.Price = Convert.ToDouble(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка! {0}", ex.Message);
                }
                productMas[i] = product;
            }

            JsonSerializerOptions option = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };
            string strJSON = "";
            for (int i = 0; i < n; i++)
            {
                strJSON = strJSON + JsonSerializer.Serialize(productMas[i], option);
                strJSON = strJSON + "\n";
            }

            WorkDirFiles.WriteTxtFiles(filesName, strJSON);
            Console.WriteLine(strJSON);
            Console.ReadKey();
        }

    }
    public class Product
    {
        
        int code;        
        double price;
        [JsonPropertyName("Название товара")]
        public string Name { get; set; }
        [JsonPropertyName("Код товара")]
        public int Code
        {
            get
            {
                return code;
            }
            set
            {
                if (value >= 0)
                {
                    code = value;
                }
            }
        }
        [JsonPropertyName("Цена товара")]
        public double Price
        {
            get
            {
                return price;
            }
            set
            {
                if (value >= 0)
                {
                    price = value;
                }
            }
        }
    }
    public static class WorkDirFiles
    {
        public static void CreateDir(string path)
        {
            if ((!Directory.Exists(path)) && (path.Length > 0))
            {
                Directory.CreateDirectory(path);
            }
        }
        public static void WriteTxtFiles(string filePath, params string[] data)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    foreach (var str in data)
                    {
                        sw.WriteLine(str);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! {0}", ex.Message);
            }
        }
        public static void ReadTxtFiles(string filePath, ref string[] data)
        {
            Array.Clear(data, 0, data.Length);
            try
            {

                using (StreamReader sr = new StreamReader(filePath))
                {
                    data = sr.ReadToEnd().Split('\n');
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! {0}", ex.Message);
            }
        }
        public static void ReadTxtFiles(string filePath, ref string data)
        {
            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    data = sr.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! {0}", ex.Message);
            }
        }
    }
}
