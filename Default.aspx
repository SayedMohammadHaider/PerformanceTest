<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
  <%--      <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var row = "";
            $.ajax({                
                type: 'GET',
                async: false,
                cache: false,
                url: 'https://jsonplaceholder.typicode.com/photos',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (response) {
                    $.each(response, function (index, value) {
                        row += '<tr><td>' + value.albumId + '</td><td>' + value.id + '</td><td>' + value.title + '</td><td>' + value.url + '</td><td><img src=' + value.thumbnailUrl + '/></td></tr>';
                    })
                }
            });
            $("#Tbody1").html(row);
        })
    </script>--%>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <p runat="server" id="txtADOPerformance"></p>
        <p runat="server" id="txtEntityPerformance"></p>
       <div>
           <table border="1">
               <tbody runat="server" id="divStudent">

               </tbody>
           </table>

           <h3>This is new changes</h3>
           <h3></h3>
          <%-- <table border="1">
               <tbody id="Tbody1">

               </tbody>
           </table>--%>
       </div>
    </div>
    </form>
</body>
</html>
