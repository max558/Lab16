using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using _1_inJSON;

namespace _2_outJSON
{
    /*
     * Необходимо разработать программу для получения информации о товаре из json-файла. 
     * Десериализовать файл «Products.json» из задачи 1. 
     * Определить название самого дорогого товара.
     */
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = @"..\..\..\1 - inJSON\bin\Debug\Products.json";
            string strJSON = "";
            WorkDirFiles.ReadTxtFiles(fileName, ref strJSON);

            /* В случае оформления файла*.json с пробелами (красивое оформление для печати) и без этого оформления, 
             * переводим в формат построчного исполнения, т.е. строка заключенная в {} на новой строке 
             */

            string[] strJSONMas= { };
            StringJSONMas(strJSON, ref strJSONMas);
            
            //Определяем кол-во продуктов и оформляем их в массив объектов
            int
                nMas = strJSONMas.Length,
                index = 0;

            Product[] productMas = new Product[nMas];
            foreach (var str in strJSONMas)
            {
                if (str.Length > 0)
                {
                    try
                    {
                        productMas[index] = JsonSerializer.Deserialize<Product>(str);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Ошибка! {0}", ex.Message);
                    }
                    index++;
                }
            }

            //Определяем результат
            double priceProduct = 0;
            string nameProduct = "";
            for (int i = 0; i < nMas; i++)
            {
                if(priceProduct< productMas[i].Price)
                {
                    priceProduct = productMas[i].Price;
                    nameProduct = productMas[i].Name;
                }
            }  

            Console.WriteLine("Самый дорогой товар: {0} его цена {1} руб.",nameProduct, priceProduct);
            Console.ReadKey();
        }
        static void StringJSONMas(string JSON,ref string[] strMas)
        {
            string[] strJSONMas = JSON.Split('\r');
            string strJSON = "";
            foreach (var str in strJSONMas)
            {
                str.Trim();
                strJSON += str.Trim();
            }
            strMas = strJSON.Split('\n');
        }
    }
}
