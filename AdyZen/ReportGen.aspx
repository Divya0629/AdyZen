<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportGen.aspx.cs" Inherits="AdyZen.ReportGen" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Report Generation Based on Year</title>
    <link href="~/Content/bootstrap.min.css" rel="stylesheet" />
    <script src="~/Scripts/jquery.min.js"></script>
    <script src="~/Scripts/bootstrap.bundle.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-multiselect/0.9.15/css/bootstrap-multiselect.css" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-multiselect/0.9.15/js/bootstrap-multiselect.min.js"></script>
</head>
<body class="container-xl">
    <form id="form1" runat="server">

        <asp:Label ID="lblYear" runat="server" Text="Select Year(s):" AssociatedControlID="ddl_report" CssClass="form-label"></asp:Label>

        <asp:DropDownList ID="ddl_report" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_report_SelectedIndexChanged" CssClass="form-select">
        </asp:DropDownList>

        <asp:ListBox ID="lstSelectedValues" runat="server" SelectionMode="Multiple"></asp:ListBox>
        <asp:Button ID="btnGenerateReport" Text="Generate Report" runat="server" OnClick="btnGenerateReport_Click" CssClass="me-4 btn btn-success" />
        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CssClass="btn btn-success" OnClick="btnCancel_Click" />


        <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="True" CssClass="mt-5 table">
            
        </asp:GridView>
    </form>

</body>
</html>
