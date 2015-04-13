<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewUploads.aspx.cs" Inherits="HelixServiceUI.BinaryHandler.ViewUploads" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Uploads</title>
    <style>
        table {
            width: 920px;
            table-layout: fixed;
            overflow-wrap: break-word;
        }

            table,
            table tbody tr th,
            table tbody tr td {
                border: 0;
            }

                table tbody tr th:first-child,
                table tbody tr td:first-child {
                    width: 400px;
                }

                table tbody tr th,
                table tbody tr td {
                    text-align: left;
                    padding: 5px;
                }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox> <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" /> <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" />
        </div>
        <div>
            <asp:GridView ID="gvUploads" runat="server" AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true" PageSize="25" DataKeyNames="ID" OnRowCreated="gvUploads_RowCreated"
                OnRowDeleting="gvUploads_RowDeleting" OnRowDataBound="gvUploads_RowDataBound" OnPageIndexChanging="gvUploads_PageIndexChanging" OnSorting="gvUploads_Sorting">
                <Columns>
                    <asp:HyperLinkField DataNavigateUrlFields="ID" DataNavigateUrlFormatString="binary.ashx?id={0}" HeaderText="File Name" DataTextField="Name" SortExpression="Name" />
                    <asp:BoundField DataField="MimeType" HeaderText="Mime Type" SortExpression="MimeType" />
                    <asp:BoundField DataField="Size" HeaderText="Size" SortExpression="Size" />
                    <asp:ButtonField CommandName="Delete" ButtonType="Button" Text="Delete" HeaderText="Actions" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
