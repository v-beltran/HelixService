<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HelixServiceUI.BinaryHandler.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Binary Handler</title>
    <style>
        .success p,
        .failure p {
            line-height: 5px;
            display: block;
        }

        .success p {
            color: green;
        }

        .failure p {
            color: red;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="main">
            <p>
                <a href="ViewUploads.aspx">View Uploads</a>
            </p>
            <p>
                Upload:                
                <asp:FileUpload ID="fuMultipleFiles" runat="server" AllowMultiple="true" />
            </p>
        </div>
        <div class="success">
            <asp:Literal ID="lSuccess" runat="server"></asp:Literal>
        </div>
        <div class="failure">
            <asp:Literal ID="lFailure" runat="server"></asp:Literal>
        </div>
        <div class="actions">
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
        </div>
    </form>
</body>
</html>
