<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HelixServiceUI.FormValidationCSharp.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Input Validation</title>
    <style>
        * {
            font-family: Calibri, Candara, Segoe, "Segoe UI", Optima, Arial, sans-serif;
            font-size: 14px;
        }

        #wrapper {
            width: 720px;
        }

        .input-container {
            width: 100%;
            margin: 1% 5% 5% 0;
        }

            .input-container label,
            .input-container input,
            .input-container select {
                display: inline-block;
            }

            .input-container label {
                width: 30%;
                text-align: left;
            }

            .input-container input {
                width: 40%;
                margin: 0 0 0 1%;
            }

        .error-container {
            width: 72%;
            display: block;
            background-color: #cc3c3c;
            color: white;
            margin: 1% 5% 5% 0;
        }

        .success-container {
            width: 72%;
            display: block;
            background-color: darkgreen;
            color: white;
            margin: 1% 5% 5% 0;
        }

            .error-container label,
            .success-container label {
                width: 100%;
                line-height: 30px;
                text-align: center;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="wrapper">
            <asp:Panel ID="pnlForm" runat="server" CssClass="form-container" DefaultButton="btnSubmit">
                <div class="input-container" id="inputPassword" runat="server">
                    <asp:Label ID="lblPassword" runat="server" AssociatedControlID="txtPassword" Text="Password"></asp:Label>
                    <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
                    <p>A password must be at least 8 characters while containing at least one uppercase character and one digit.</p>
                </div>
                <div class="input-container" id="inputZipCode" runat="server">
                    <asp:Label ID="lblZipCode" runat="server" AssociatedControlID="txtZipCode" Text="ZIP Code"></asp:Label>
                    <asp:TextBox ID="txtZipCode" runat="server"></asp:TextBox>
                    <p>A US ZIP Code must be 5 digits.</p>
                </div>
                <div class="input-container" id="inputTelephoneNo" runat="server">
                    <asp:Label ID="lblTelephoneNo" runat="server" AssociatedControlID="txtTelephoneNo" Text="Telephone Number"></asp:Label>
                    <asp:TextBox ID="txtTelephoneNo" runat="server"></asp:TextBox>
                    <p>A US telephone number must be 10 digits.</p>
                </div>
                <div class="input-container" id="inputEmail" runat="server">
                    <asp:Label ID="lblEmail" runat="server" AssociatedControlID="txtEmail" Text="E-mail Address"></asp:Label>
                    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                    <p>A valid e-mail address follows general restriction rules as outlined by <a href="http://tools.ietf.org/html/rfc5322#section-3.4">RFC 5322</a>.</p>
                </div>
                <div class="input-container" id="inputDOB" runat="server">
                    <asp:Label ID="lblDOB" runat="server" AssociatedControlID="txtDOB" Text="Date of Birth (mm/dd/yyyy)"></asp:Label>
                    <asp:TextBox ID="txtDOB" runat="server"></asp:TextBox>
                    <p>A valid DOB is in the range of 1900 to the current year.</p>
                </div>
                <div class="input-container" id="inputDate" runat="server">
                    <asp:Label ID="lblDate" runat="server" AssociatedControlID="txtDate" Text="Date of Anything (mm/dd/yyyy)"></asp:Label>
                    <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                    <p>A valid date must be anything in mm/dd/yyyy format.</p>
                </div>
                <div class="input-container" id="inputTimeStandard" runat="server">
                    <asp:Label ID="lblTimeStandard" runat="server" AssociatedControlID="txtTimeStandard" Text="What time is it now?"></asp:Label>
                    <asp:TextBox ID="txtTimeStandard" runat="server"></asp:TextBox>
                    <p>Time must be in 12-hour am/pm format.</p>
                </div>
                <div class="input-container" id="inputTimeMilitary" runat="server">
                    <asp:Label ID="lblTimeMilitary" runat="server" AssociatedControlID="txtTimeMilitary" Text="What time is it now?"></asp:Label>
                    <asp:TextBox ID="txtTimeMilitary" runat="server"></asp:TextBox>
                    <p>Time must be in 24-hour format.</p>
                </div>
                <div class="input-container" id="inputNumber" runat="server">
                    <asp:Label ID="lblNumber" runat="server" AssociatedControlID="txtNumber" Text="What is your favorite number?"></asp:Label>
                    <asp:TextBox ID="txtNumber" runat="server" MaxLength="9"></asp:TextBox>
                    <p>A valid number is an integer.</p>
                </div>
                <div class="input-container" id="inputCurrency" runat="server">
                    <asp:Label ID="lblCurrency" runat="server" AssociatedControlID="txtCurrency" Text="What is your yearly salary?"></asp:Label>
                    <asp:TextBox ID="txtCurrency" runat="server"></asp:TextBox>
                    <p>A valid decimal in US currency format.</p>
                </div>
                <div class="input-container" id="inputWebsite" runat="server">
                    <asp:Label ID="lblWebsite" runat="server" AssociatedControlID="txtWebsite" Text="What is the URL of your favorite website?"></asp:Label>
                    <asp:TextBox ID="txtWebsite" runat="server"></asp:TextBox>
                    <p>A valid URL must start with http://, https://, ftp://, or www.</p>
                </div>
                <div class="submit-container">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                    <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" />
                </div>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
