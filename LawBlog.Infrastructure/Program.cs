using System;
using MongoDB.Driver;
using LawBlog.Infrastructure.Data;
using LawBlog.Infrastructure.Models;
using System.Collections.Generic;
using LawBlog.Infrastructure.Interfaces;

namespace LawBlog.Infrastructure
{
    class Program
    {
        static void Main(string[] args)
        {
            IAutorData dbContext = new AutorData();


            foreach (var item in dbContext.FindAll())
            {
                Console.WriteLine($"Nome: {item.nome}");
            }

            Console.ReadKey();
        }
    }
}
