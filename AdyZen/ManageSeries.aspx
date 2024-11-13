<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageSeries.aspx.cs" Inherits="AdyZen.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manage Series</title>
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.11.5/css/jquery.dataTables.css" />
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.datatables.net/1.11.5/js/jquery.dataTables.min.js"></script>
    <link href="~/Content/bootstrap.min.css" rel="stylesheet" />
    <script src="~/Scripts/jquery.min.js"></script>
    <script src="~/Scripts/bootstrap.bundle.min.js"></script>
</head>
<body class="container-xl">
    <h3>Manage Series</h3>

    <form id="form1" runat="server">
        <table style="width: 100%;">
            <tr>
                <td class="auto-style1">Series API ID<br />
                    <asp:TextBox ID="series_id" type="int" runat="server" placeholder="SeriesAPI_ ID" CssClass="form-control"></asp:TextBox>
                    <br />
                </td>
                <td class="auto-style2">Series Name<br />
                    <asp:TextBox ID="series_Name" type="string" runat="server" placeholder="Series Name" CssClass="form-control"></asp:TextBox>
                    <br />
                </td>
                <td>Series Type<br />
                    <asp:DropDownList ID="series_type" runat="server" CssClass="form-select">
                        <asp:ListItem>Select</asp:ListItem>
                        <asp:ListItem>Internatio</asp:ListItem>
                        <asp:ListItem>Domestic</asp:ListItem>
                        <asp:ListItem>Women</asp:ListItem>
                        <asp:ListItem>Mens</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                </td>
            </tr>
            <tr>
                <td class="auto-style1">Series Start From Date<br />
                    <asp:TextBox ID="start_date" runat="server" type="date" CssClass="form-control"></asp:TextBox>
                </td>
                <td class="auto-style2">Series End To Date<br />
                    <asp:TextBox ID="end_date" runat="server" type="date" CssClass="form-control"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div id="" class="text-center mt-3">
            <asp:Button ID="Add" runat="server" Text="Add Series" OnClick="Add_Click" CssClass="btn btn-success me-2" />
            <asp:Button ID="search" runat="server" Text="Search" OnClick="search_Click" CssClass="btn btn-success me-2" />
            <asp:Button ID="Refresh" runat="server" Text="Refresh" OnClick="Refresh_Click" CssClass="btn btn-success me-2" />
            <asp:Button ID="btnReport" runat="server" Text="Report" OnClick="Rep_Click" CssClass="btn btn-success me-2" />

        </div>

        <div class="mt-4">
            <table id="dataTable">
                <thead>
                    <tr>
                        <th>Delete Series</th>
                        <th>Series ID</th>
                        <th>Series API_ID</th>
                        <th>Series Name</th>
                        <th>Series Type</th>
                        <th>Match Type</th>
                        <th>Gender</th>
                        <th>Series Year</th>
                        <th>Trophy Type</th>
                        <th>Series Start Date</th>
                        <th>Series End Date</th>

                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptSeries" runat="server" OnItemCommand="Delete_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="BtnDelete" runat="server" CommandName="DeleteItem"
                                        CommandArgument='<%# Eval("SeriesID") %>'
                                        Text="Delete"
                                        OnClientClick="return confirm('Are you sure you want to delete this item?');"></asp:LinkButton>
                                </td>
                                <td><%# Eval("SeriesID") %></td>
                                <td><%# Eval("SeriesAPI_ID") %></td>
                                <td>
                                    <a href="<%# GetEncryptedUrl("E", (int)Eval("SeriesId"), (int)Eval("SeriesAPI_ID")) %>">
                                        <%# Eval("SeriesName") %>
                                    </a>
                                </td>


                                <td><%# Eval("SeriesType") %></td>
                                <td><%# GetMatchText(Convert.ToInt32(Eval("SeriesMatchType"))) %></td>
                                <td><%# GetGenderText(Convert.ToInt32(Eval("Gender"))) %></td>
                                <td><%# Eval("SeriesYear") %></td>
                                <td><%# GetTrophyText(Convert.ToInt32(Eval("TrophyType"))) %></td>
                                <td><%# DataBinder.Eval(Container.DataItem, "SeriesStartDate", "{0:yyyy-MM-dd}") %></td>
                                <td><%# DataBinder.Eval(Container.DataItem, "SeriesEndDate", "{0:yyyy-MM-dd}") %></td>

                            </tr>


                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
    </form>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#dataTable').DataTable();
        });
    </script>

</body>
</html>
