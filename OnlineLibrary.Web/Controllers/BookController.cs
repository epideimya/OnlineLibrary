using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.Data.EF.Data;
using OnlineLibrary.Data.EF.EF;
using OnlineLibrary.Web.Models;

namespace OnlineLibrary.Web.Controllers
{
    public class BookController : Controller
    {
        [HttpGet]  //обращение клента к экшену только через GET запрос
        public IActionResult AddBook()//Обработка запроса для получения страницы AddBook с сервера
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddBook(AddBookViewModel addBookViewModel)//Принимает данные от клиента(то, что я введу)
        {
            Book dataBaseBook = new Book
            {
                Name = addBookViewModel.Name,
                Author = addBookViewModel.Author,
                Body = addBookViewModel.Body,
                ReleaseYear = addBookViewModel.ReleaseYear,
                ShortDescription = addBookViewModel.ShortDescription
            };
            if (addBookViewModel.BookImage != null)
            {
                //начинаем обрабатывать нашу картинку для того что бы сохнарить её имя в базу
                var splittedFileName = addBookViewModel.BookImage.FileName.Split('.');
                var fileType = splittedFileName.Last();
                var fileName = Guid.NewGuid();
                dataBaseBook.CoverImageName = $"{fileName}.{fileType}";

                var filePath = $"wwwroot/ClientImages/BookImages/{dataBaseBook.CoverImageName}";//путь к картинке в файловой системе с его именем в БД

                using (var stream = System.IO.File.Create(filePath))
                {
                    addBookViewModel.BookImage.CopyTo(stream);
                }
            }
            else
                dataBaseBook.CoverImageName = "not_found";

            using (var myContext = new DesignTimeDbContextFactory().CreateDbContext())
            {
                myContext.Books.Add(dataBaseBook);
                myContext.SaveChanges();
            }

            return new LocalRedirectResult($"~/Home/Index/");
        }

        [HttpGet, Route("book/{bookId:int}")]
        public IActionResult ReadBook(int bookId)
        {
            var myContext = new DesignTimeDbContextFactory().CreateDbContext();
            Book books = myContext.Books.Find(bookId);
            return View(books);
        }

        [HttpGet, Route("Book/Edit/{bookId:int}")]
        public IActionResult EditBook(int bookId)
        {
            var myContext = new DesignTimeDbContextFactory().CreateDbContext();
            Book dbBook = myContext.Books.Find(bookId);

            if (dbBook == null)
            {
                return NotFound();
            }

            EditBookViewModel editBookViewModel = new EditBookViewModel
            {
                Name = dbBook.Name,
                Author = dbBook.Author,
                ReleaseYear = dbBook.ReleaseYear,
                ShortDescription = dbBook.ShortDescription,
                Body = dbBook.Body,
                Id = dbBook.Id,
            };

            return View(editBookViewModel);
            //return new LocalRedirectResult($"~/Home/Index/");  
        }

        [HttpPost, Route("Book/Edit/{bookId:int}")]
        public IActionResult EditBook(int bookId, EditBookViewModel editBookViewModel)
        {
            var myContext = new DesignTimeDbContextFactory().CreateDbContext();

            Book dbBook = myContext.Books.Find(bookId);

            dbBook.Name = editBookViewModel.Name;
            dbBook.Author = editBookViewModel.Author;
            dbBook.ReleaseYear = editBookViewModel.ReleaseYear;
            dbBook.ShortDescription = editBookViewModel.ShortDescription;
            dbBook.Body = editBookViewModel.Body;

            var oldImageName = dbBook.CoverImageName;

            if (editBookViewModel.BookImage != null)
            {

                var splittedFileName = editBookViewModel.BookImage.FileName.Split('.');
                var fileType = splittedFileName.Last();
                var fileName = Guid.NewGuid();

                dbBook.CoverImageName = $"{fileName}.{fileType}";

                var filePath = $"wwwroot/ClientImages/BookImages/{dbBook.CoverImageName}";

                using (var stream = System.IO.File.Create(filePath))
                {
                    editBookViewModel.BookImage.CopyTo(stream);
                }
            }

            myContext.SaveChanges();

            if (oldImageName != dbBook.CoverImageName)
            {
                System.IO.File.Delete($"wwwroot/ClientImages/BookImages/{oldImageName}");
            }

            return new LocalRedirectResult($"~/book/{bookId}");
        }

        [HttpGet, Route("Book/Delete/{bookId:int}")]
        public IActionResult DeleteBook(int bookId)
        {
            var myContext = new DesignTimeDbContextFactory().CreateDbContext();
            Book dbBook = myContext.Books.Find(bookId);
            myContext.Books.Remove(dbBook);
            myContext.SaveChanges();
            return new LocalRedirectResult($"~/Home/Index/");
        }


    }


}
