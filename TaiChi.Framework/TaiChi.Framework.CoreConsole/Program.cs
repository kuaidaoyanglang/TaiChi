using System;
using System.Text.Json;
using System.Text.Unicode;

namespace TaiChi.Framework.CoreConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var user = new { 
                Id=11,
                Name="HelloDavy",
                Age=18
            };

            Console.WriteLine(user);
            var options = new JsonSerializerOptions();
            options.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(UnicodeRanges.All);
            var json = JsonSerializer.Serialize(user,options);
            Console.WriteLine(json);

            Console.WriteLine("**************************************");
            {
                SharpSix six = new SharpSix();
                People people = new People()
                {
                    Id = 505,
                    Name = "马尔凯蒂"
                };
                six.Show(people);
            }

            Console.WriteLine("**************************************");
            {
                SharpSeven seven = new SharpSeven();
                seven.Show();
            }

        }
    }
}
