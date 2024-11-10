<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportGen.aspx.cs" Inherits="AdyZen.ReportGen" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Report Generation Based on Year</title>
     <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />
    <script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-multiselect/0.9.15/css/bootstrap-multiselect.css" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-multiselect/0.9.15/js/bootstrap-multiselect.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
    &nbsp;
        <br />
        <br />
        <asp:Label ID="lblYear" runat="server" Text="Select Year(s):" AssociatedControlID="ddl_report"></asp:Label>
        <br />
        <br />
        <asp:DropDownList ID="ddl_report" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_report_SelectedIndexChanged" SelectionMode="Multiple">
        </asp:DropDownList>
        <br />
        <br />
   
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:ListBox ID="lstSelectedValues" runat="server" SelectionMode="Multiple"></asp:ListBox>
        <asp:Button ID="btnGenerateReport" Text="Generate Report" runat="server" OnClick="btnGenerateReport_Click" />
        <br />
        <br />
        <br />
        <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="True">
        </asp:GridView>
    </form>
         <script type="text/javascript">
             $(document).ready(function () {
                 $('#<%= ddl_report %>').multiselect({
                includeSelectAllOption: true,
                nonSelectedText: 'Select',
                enableFiltering: true,
                buttonWidth: '50%'
            });
        });
         </script>
</body>
</html>
