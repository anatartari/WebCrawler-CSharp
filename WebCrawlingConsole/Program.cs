using System;

using HtmlAgilityPack;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using System.Net;
using System.Collections.Generic;
using WebCrawlingConsole.Models;
using WebCrawlingConsole.Data;

namespace WebCrawlingConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ola, estamos fazendo a busca dos dados...\n ");

            var books = new List<Book>();
            try
            {
                books = startCrawSearch(books);
                Console.WriteLine("\nDados coletados com sucesso!\n");
                Console.WriteLine("\nSalvando na base de dados...\n");
                saveInDataBase(books);
                Console.WriteLine("\nOperacoes finalizadas com sucesso\n");

            }
            catch (Exception e)
            {
                Console.WriteLine("Houve algum erro na busca de dados\n");
            }


            
        }

        private static void saveInDataBase(List<Book> books)
        {
            using (var context = new DataContext())
            {
                foreach(var book in books)
                {
                    context.Books.Add(book);
                    context.SaveChanges();
                }
               
            }
        }

        private static List<Book> startCrawSearch(List<Book> books)
        {
            var url = "https://www.saraiva.com.br/livros/informatica";
            var client = new WebClient();
            string pagina = client.DownloadString(url);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(pagina);


            var divs = htmlDocument.DocumentNode.Descendants("div")
               .Where(node => node.GetAttributeValue("class", "").Equals("_lazy product _prdv g _prd-out-of-stock"))
               .ToList();

            foreach(var div in divs)
            {
                string name = fixedNameBook(div.Descendants("h3").FirstOrDefault().InnerText);
                string img = div.Descendants("img").FirstOrDefault().ChildAttributes("src").FirstOrDefault().Value;

                var autList = div.Descendants("div").Where(node => node.GetAttributeValue("class", "")
                .Equals("product-field product_field_1364 product-field-type_8")).ToList();

                var aut = new List<Author>();

                foreach (var a in autList)
                {
                    var grao = a.Descendants("ul").FirstOrDefault().InnerText;
                    string[] autores = grao.Split("\r\n");
                  
                    for(int i =0; i < autores.Length; i++)
                    {
                        if(autores[i] != "")
                        {
                            Author at = new Author { Name = autores[i] };

                            aut.Add(at);
                        }
                    }
                }

                var b = new Book
                {
                    Name = name,
                    ImageUrl = img,
                    Autor = aut
                };
               
                books.Add(b);
            }

            return books;
        }

        private static string fixedNameBook(string name)
        {
            char[] aux = name.ToCharArray(0, name.Length);
            char[] titulo = new char[name.Length];
            int i = 0;

            foreach (char c in aux)
            {
                    if ((Convert.ToInt32(c) >= 32 && Convert.ToInt32(c) <= 126)
                        ||
                        (Convert.ToInt32(c) >= 128 && Convert.ToInt32(c) <= 254))
                    {
                        if(Convert.ToInt32(c) == 32 && i > 0)
                        {
                            titulo[i] = c;
                            i++;
                        }
                        else if(Convert.ToInt32(c) != 32)
                        {
                            titulo[i] = c;
                            i++;
                        }
                       
                    }
            }
            string result = new string(titulo);

            return result;
        }
    }
}
