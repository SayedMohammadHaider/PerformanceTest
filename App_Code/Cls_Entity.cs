using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Cls_Entity
/// </summary>
public class Cls_Entity
{
    public Cls_Entity()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string EF()
    {
        Book bookToCreate = new Book();
        Author authorToCreate = new Author();
        Stopwatch tellTime = new Stopwatch();
        long insertingTime = 0;
        long updatingTime = 0;
        long deletingTime = 0;
        List<Int32> generatedBookIds = new List<Int32>();
        PerformanceTestEntities entities = new PerformanceTestEntities();
        // let us delete table contents
        try
        {
            entities.Database.ExecuteSqlCommand("TRUNCATE TABLE [Book]");
            entities.Database.ExecuteSqlCommand("TRUNCATE TABLE [Author]");
            //var objCtx = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)thisDB).ObjectContext;
            //objCtx.ExecuteStoreCommand("DELETE FROM Books");
            //objCtx.ExecuteStoreCommand("DELETE FROM Authors");
        }


        catch (Exception e)
        {
            // write exception. Maybe it's the first time we run this and have no tables
            Debug.Write("Error in truncating tables: {0}", e.Message);

        }

        // let us start the watch
        tellTime.Start();

        // INSERTING!
        // we create 1000 authors with name="John Doe nr: " + a GUID
        // and address ="5th Avenue nr: " + a GUID
        // we create a book called "The Cronicles of: " + a GUID and attach it to the author
        // we save the book, so the author is also automatically created

        for (int i = 0; i < 1000; i++)
        {
            using (PerformanceTestEntities entities1 = new PerformanceTestEntities())
            {
                // creating author
                authorToCreate = new Author();
                authorToCreate.Name = "John Doe nr. " + Guid.NewGuid();
                authorToCreate.Address = "5th Avenue nr. " + Guid.NewGuid();

                //creating book and linking it to the author
                bookToCreate = new Book();
                bookToCreate.Title = "The Chronicles of: " + Guid.NewGuid();
                bookToCreate.Author = authorToCreate;

                //saving the book. Automatically, the author is saved
                entities1.Books.Add(bookToCreate);
                entities1.SaveChanges();
                generatedBookIds.Add(Convert.ToInt32(bookToCreate.Id));
            }               
        }

        insertingTime = tellTime.ElapsedMilliseconds; // how did I do with inserting?

        tellTime.Restart(); // restart timer

        // We update the 1000 books by changing their title
        foreach (int bookId in generatedBookIds)
        {
            
            Book bookToUpdate = entities.Books.Find(bookId);
            bookToUpdate.Title = "New chronicles of: " + Guid.NewGuid();

            entities.SaveChanges();

        }

        updatingTime = tellTime.ElapsedMilliseconds; // how did I do with inserting?
        tellTime.Restart(); // restart timer

        // We delete 1000 books, one by one
        foreach (int bookId in generatedBookIds)
        {

            Book bookToDelete = entities.Books.Find(bookId);
            entities.Books.Remove(bookToDelete);

        }

        deletingTime = tellTime.ElapsedMilliseconds; // how did I do with inserting?
        tellTime.Stop(); // stop timer


        //printing the results 

        string returnedMessage = "Results with Entity Framwork 6.1.3: ";
        returnedMessage += "1000 Insert operations in ms.: " + insertingTime.ToString();
            returnedMessage += " 1000 Update operations in ms.: " + updatingTime.ToString();
            returnedMessage += " 1000 Delete operations in ms.: " + deletingTime.ToString();


        return returnedMessage;
    }

    public string getEF()
    {
        Stopwatch tellTime = new Stopwatch();
        long insertingtime = 0;
        List<Book> booklist = new List<Book>();
        tellTime.Start();
        using (PerformanceTestEntities mde = new PerformanceTestEntities())
        {
            booklist = mde.Books.ToList();
        }
        insertingtime = tellTime.ElapsedMilliseconds;
        var avv = booklist;
        return insertingtime.ToString();
    }

    public string getEFStudent() //out long studentTime, out long foreachTime
    {
        Stopwatch tellTime = new Stopwatch();
        //long studentListTime = 0;
        //long studentForeach = 0;
        string studentData = "";
        //tellTime.Start();
        using (PerformanceTestEntities mde = new PerformanceTestEntities())
        {
          var studentList = mde.Students.Where(x=>x.ClassId==1).ToList();
            //tellTime.Stop();
            //studentListTime = tellTime.ElapsedMilliseconds;
            var i = 0;


            //using (System.IO.StreamWriter file =
            //new System.IO.StreamWriter(@"C:\Users\SayedMohammadHaider\Desktop\aaa\abc.txt"))
            //{                
            //    file.WriteLine(JsonConvert.SerializeObject(studentList));
            //}

            tellTime.Start();            
            foreach (var list in studentList)
            {
                i++;
                studentData += "<tr>";
                studentData += "<td>" + list.Id + "</td>";
                studentData += "<td>" + list.Name + "</td>";
                studentData += "<td>" + list.Gender + "</td>";
                studentData += "<td>" + list.Address + "</td>";
                studentData += "<td>" + list.MobileNumber + "</td>";
                studentData += "<td>" + list.EmailId + "</td>";
                studentData += "<td>" + list.DOB + "</td>";
                studentData += "<td>" + list.ClassId + "</td>";
                studentData += "<td>" + list.SectionId + "</td>";
                studentData += "<td>" + list.Description + "</td>";
                //studentData += "<td>" + list.ProfilePicture + "</td>";
                studentData += "</tr>";
            }
            //tellTime.Stop();
            //studentForeach = tellTime.ElapsedMilliseconds;
        }
        //studentTime = studentListTime;
        //foreachTime = studentForeach;
        return studentData.ToString();
    }

}