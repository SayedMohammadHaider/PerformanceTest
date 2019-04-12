using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Cls_ADO adoCRUD = new Cls_ADO();
        //var ADOperformanceString = adoCRUD.ADOCLS();

        //Cls_Entity entityCRUD = new Cls_Entity();
        //var entityPerformanceString = entityCRUD.getEF();

        //txtADOPerformance.InnerText = ADOperformanceString;
        //txtEntityPerformance.InnerText = entityPerformanceString;

        Cls_Entity entityStudent = new Cls_Entity();
        long studentTime = 0;
        long foreachTime = 0;
        var entityStudentData = entityStudent.getEFStudent(); //out studentTime, out foreachTime
        //txtADOPerformance.InnerText = Convert.ToString(studentTime);
        //txtEntityPerformance.InnerText = Convert.ToString(foreachTime);
        divStudent.InnerHtml = entityStudentData;


        //using (StreamReader r = new StreamReader("C://Users//SayedMohammadHaider//Desktop//PerformanceTest//fetchData.json"))
        //{
        //    string json = r.ReadToEnd();
        //    dynamic items = JsonConvert.DeserializeObject(json);
        //    string studentData = "";
        //    foreach(var aa in items)
        //    {
        //        studentData += "<tr><td>"+ aa["Id"] + "</td><td>" + aa["Name"] + "</td><td>" + aa["Gender"] + "</td><td>" + aa["Address"] + "</td><td>" + aa["MobileNumber"] + "</td><td>" + aa["EmailId"] + "</td><td>" + aa["DOB"] + "</td><td>" + aa["ClassId"] + "</td><td>" + aa["SectionId"] + "</td><td>" + aa["Description"] + "</td><td>" + aa["ProfilePicture"] + "</td></tr>";
        //    }
        //    divStudent.InnerHtml = studentData;   
        //}

        //txtEntityPerformance.InnerText = entityStudentData;

    }
}