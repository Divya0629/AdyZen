<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddSeries.aspx.cs" Inherits="AdyZen.AddSeries" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add Series</title>
  <link href="~/Content/bootstrap.min.css" rel="stylesheet" />
<script src="~/Scripts/jquery.min.js"></script>
<script src="~/Scripts/bootstrap.bundle.min.js"></script>
</head>
<body class="container-xl">
    <h3>Series Detail</h3>
    <form id="form1" runat="server">
        <div>
            <table style="width:100%;">
                <tr id="idRow" runat="server">
                    <td class="auto-style1">Series ID<br />
                        <asp:TextBox ID="sid" runat="server" readonly="true" cssClass="form-control"></asp:TextBox>
                    </td>
                    <td class="auto-style2">Series API ID<br />
                        <asp:TextBox ID="txtapi_id" runat="server" readonly="true" cssClass="form-control"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">Series Name<br />
                        <asp:TextBox ID="seriesName" runat="server" placeholder="Series Name" required cssClass="form-control"></asp:TextBox>
                    </td>
                    <td class="auto-style2">Series Type<br />
                        <asp:DropDownList ID="dropseriestype" runat="server" placeholder="Select" required CssClass="form-select">
                            <asp:ListItem Selected="True" Hidden>Select</asp:ListItem>
                            <asp:ListItem>Internatio</asp:ListItem>
                            <asp:ListItem>Domestic</asp:ListItem>
                            <asp:ListItem>Women</asp:ListItem>
                            <asp:ListItem>Mens</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style3">Series Status<br />
                        <asp:DropDownList ID="series_status" runat="server" placeholder="Scheduled" required CssClass="form-select">
                            <asp:ListItem>Scheduled</asp:ListItem>
                            <asp:ListItem>Completed</asp:ListItem>
                            <asp:ListItem>Live</asp:ListItem>
                            <asp:ListItem>Abandon</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">Match Status<br />
                        <asp:DropDownList ID="match_status" runat="server" Placeholder="Scheduled" required CssClass="form-select">
                             <asp:ListItem>Scheduled</asp:ListItem>
                            <asp:ListItem>Completed</asp:ListItem>
                            <asp:ListItem>Live</asp:ListItem>
                            <asp:ListItem>Abandoned</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style2">Match Format<br />
                        <asp:DropDownList ID="match_format" runat="server" Placeholder="Select" CssClass="form-select">
                             <asp:ListItem Selected Hidden>Select</asp:ListItem>
                            <asp:ListItem>ODI</asp:ListItem>
                            <asp:ListItem>TEST</asp:ListItem>
                            <asp:ListItem>T20</asp:ListItem>
                            <asp:ListItem>T10</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style3">Series Match Type<br />
                        <asp:DropDownList ID="match_type" runat="server" Placeholder="Select" CssClass="form-select">
                             <asp:ListItem Value="-1" Selected Hidden>Select</asp:ListItem>
                            <asp:ListItem Value="1">ODI</asp:ListItem>
                            <asp:ListItem Value="2">TEST</asp:ListItem>
                            <asp:ListItem Value="3">T20I</asp:ListItem>
                            <asp:ListItem Value="4">LIST A</asp:ListItem>
                            <asp:ListItem Value="5">T20(Domestic)</asp:ListItem>
                            <asp:ListItem Value="6">First Class</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">Gender<br />
                        <asp:DropDownList ID="gender" runat="server" Placeholder="Select" required CssClass="form-select">
                             <asp:ListItem Selected Hidden>Select</asp:ListItem>
                            <asp:ListItem >Mens</asp:ListItem>
                            <asp:ListItem>Womens</asp:ListItem>
                            <asp:ListItem>Others</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style2">Year<br />
                        <asp:TextBox ID="year" runat="server" Placeholder="Year" required cssClass="form-control"></asp:TextBox>
                    </td>
                    <td class="auto-style3">Trophy Type<br />
                        <asp:DropDownList ID="trophy_type" runat="server" Placeholder="Select" required CssClass="form-select">
                            <asp:ListItem Selected Hidden>Select</asp:ListItem>
                            <asp:ListItem>Asia Cup</asp:ListItem>
                            <asp:ListItem>ICC WC T20</asp:ListItem>
                            <asp:ListItem>ICC WC ODI</asp:ListItem>
                            <asp:ListItem>IPL</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">Series Start Date<br />
                        <asp:TextBox ID="start_date" runat="server" type="date" placeholder="" required cssClass="form-control"></asp:TextBox>
                    </td>
                    <td class="auto-style2">Series End Date<br />
                        <asp:TextBox ID="end_date" runat="server" type="date" required cssClass="form-control"></asp:TextBox>
                    </td>
                    <td class="auto-style3">Is Active<br />
                        <asp:DropDownList ID="active_status" runat="server" Placeholder="Yes" required CssClass="form-select">
                            <asp:ListItem>Yes</asp:ListItem>
                            <asp:ListItem>No</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">Description<br />
                        <asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine" cssClass="form-control"></asp:TextBox>
                    </td>
                  
                </tr>
            </table>
        </div>
        <div class="text-center mt-4">
            <asp:Button ID="save_series" runat="server" Text="Save" OnClick="save_series_Click" CssClass="btn btn-success me-4" />
            <asp:Button ID="refresh" runat="server" Text="Refresh" OnClick="refresh_Click" CssClass="btn btn-success me-4"/>
            <asp:Button ID="Cancel" runat="server" Text="Cancel" OnClick="Cancel_Click" CssClass="btn btn-success" type="button"/>
        </div>
        <asp:Label ID="lblmsg" runat="server" CssClass="mt-5 d-block alert alert-success"></asp:Label>
    </form>
</body>
</html>
