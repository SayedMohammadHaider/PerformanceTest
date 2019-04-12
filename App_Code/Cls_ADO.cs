using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Configuration;

/// <summary>
/// Summary description for Cls_ADO
/// </summary>
public class Cls_ADO
{
    public Cls_ADO()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public class Book1
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Author1 Author { get; set; }

    }

    public class Author1
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public ICollection<Book1> Books { get; set; }
    }

    public string ADOCLS()
    {

        string insertAuthorSQL = "INSERT INTO Author (Name, Address) VALUES (@name, @address)";
        string insertBookSQL = "INSERT INTO Book(Title, Author_Id) VALUES (@Title, @Author_Id)";
        string updateBookSQL = "UPDATE Book Set Title=@Title where Id=@Id";
        string deleteBookSQL = "DELETE Book where Id=@Id";

        Book1 bookToCreate = new Book1();
        Author1 authorToCreate = new Author1();
        Stopwatch tellTime = new Stopwatch();

        // SQL Objects we will use
        SqlConnection connAntiEF = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
        SqlCommand cmdAntiEF = new SqlCommand();

        // Open Connection
        connAntiEF.Open();

        long insertingTime = 0;
        long updatingTime = 0;
        long deletingTime = 0;
        List<int> generatedBookIds = new List<int>();

        // let us delete table contents
        try
        {
            cmdAntiEF = new SqlCommand("DELETE FROM Book", connAntiEF);
            cmdAntiEF.ExecuteNonQuery();
            cmdAntiEF = new SqlCommand("DELETE FROM Author", connAntiEF);
            cmdAntiEF.ExecuteNonQuery();
        }


        catch (Exception e)
        {
            // write exception. 
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

            // creating author
            authorToCreate = new Author1();
            authorToCreate.Name = "John Doe nr. " + Guid.NewGuid();
            authorToCreate.Address = "5th Avenue nr. " + Guid.NewGuid();

            //creating book and linking it to the author
            bookToCreate = new Book1();
            bookToCreate.Title = "The Chronicles of: " + Guid.NewGuid();
            bookToCreate.Author = authorToCreate;

            // INSERT book with SQL and get its Id


            SqlParameter parmName = new SqlParameter("Name", authorToCreate.Name);
            SqlParameter parmAddress = new SqlParameter("Address", authorToCreate.Address);
            cmdAntiEF.CommandText = insertAuthorSQL;
            cmdAntiEF.Parameters.Add(parmName);
            cmdAntiEF.Parameters.Add(parmAddress);
            cmdAntiEF.ExecuteNonQuery();

            cmdAntiEF.Parameters.Clear();
            cmdAntiEF.CommandText = "SELECT @@IDENTITY";

            int insertedAuthorID = Convert.ToInt32(cmdAntiEF.ExecuteScalar());

            // INSERT book with SQL and get its Id


            parmName = new SqlParameter("title", bookToCreate.Title);
            parmAddress = new SqlParameter("author_id", insertedAuthorID);

            cmdAntiEF.CommandText = insertBookSQL;
            cmdAntiEF.Parameters.Add(parmName);
            cmdAntiEF.Parameters.Add(parmAddress);
            cmdAntiEF.ExecuteNonQuery();

            // we neeed the book's Id to iterate through the Id's later
            cmdAntiEF.CommandText = "SELECT @@IDENTITY";
            int insertedBookID = Convert.ToInt32(cmdAntiEF.ExecuteScalar());
            generatedBookIds.Add(insertedBookID);


            parmName = null;
            parmAddress = null;
            cmdAntiEF.Parameters.Clear();

        }


        insertingTime = tellTime.ElapsedMilliseconds; // how did I do with inserting?

        tellTime.Restart(); // restart timer

        // We update 1000 books by changing their title
        cmdAntiEF.CommandText = updateBookSQL;
        foreach (int bookId in generatedBookIds)
        {

            //parameters are loaded with the book's new data
            SqlParameter parmTitle = new SqlParameter("Title", "New chronicles of: " + Guid.NewGuid());
            SqlParameter parmId = new SqlParameter("Id", bookId);
            cmdAntiEF.Parameters.Add(parmTitle);
            cmdAntiEF.Parameters.Add(parmId);

            cmdAntiEF.ExecuteNonQuery();
            parmTitle = null;
            cmdAntiEF.Parameters.Clear();

        }

        updatingTime = tellTime.ElapsedMilliseconds; // how did I do with inserting?
        tellTime.Restart(); // restart timer

        // We delete 1000 books one by one
        cmdAntiEF.CommandText = deleteBookSQL;
        foreach (int bookId in generatedBookIds)
        {
            SqlParameter parmId = new SqlParameter("Id", bookId);
            cmdAntiEF.Parameters.Add(parmId);
            cmdAntiEF.ExecuteNonQuery();
            parmId = null;
            cmdAntiEF.Parameters.Clear();
        }

        

        deletingTime = tellTime.ElapsedMilliseconds; // how did I do with inserting?
        tellTime.Stop(); // stop timer

        try
        {
            cmdAntiEF = new SqlCommand("DELETE FROM Book", connAntiEF);
            cmdAntiEF.ExecuteNonQuery();
            cmdAntiEF = new SqlCommand("DELETE FROM Author", connAntiEF);
            cmdAntiEF.ExecuteNonQuery();
        }


        catch (Exception e)
        {
            // write exception. 
            Debug.Write("Error in truncating tables: {0}", e.Message);

        }
        connAntiEF.Close();
        // printing the results
        string returnedMessage = "Results with SQL Connection: ";
        returnedMessage += "1000 Insert operations in ms.: " + insertingTime.ToString();
            returnedMessage += " 1000 Update operations in ms.: " + updatingTime.ToString();
            returnedMessage += " 1000 Delete operations in ms.: " + deletingTime.ToString();


            return returnedMessage;

    }

}