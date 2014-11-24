<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HelixServiceUI.InputValidationJS.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Input Validation</title>
    <script type="text/javascript" src="../js/jquery-1.11.1.min.js"></script>
    <script type="text/javascript" src="validator.js"></script>
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
    <form id="form1" runat="server" onsubmit="return IsValid()">
        <div id="wrapper">
            <div class="form-container">
                <div class="input-container" id="inputPassword">
                    <label>Password</label>
                    <input id="txtPassword" type="text" name="password" />
                    <p>A password must be at least 8 characters while containing at least one uppercase character and one digit.</p>
                </div>
                <div class="input-container" id="inputZipCode">
                    <label>ZIP Code</label>
                    <input id="txtZipCode" type="text" name="zip" />
                    <p>A US ZIP Code must be 5 digits.</p>
                </div>
                <div class="input-container" id="inputTelephoneNo">
                    <label>Telephone Number</label>
                    <input id="txtTelephoneNo" type="text" name="telephoneno" />
                    <p>A US telephone number must be 10 digits.</p>
                </div>
                <div class="input-container" id="inputEmail">
                    <label>E-mail Address</label>
                    <input id="txtEmail" type="text" name="email" />
                    <p>A valid e-mail address follows general restriction rules as outlined by <a href="http://tools.ietf.org/html/rfc5322#section-3.4">RFC 5322</a>.</p>
                </div>
                <div class="input-container" id="inputDOB">
                    <label>Date of Birth (mm/dd/yyyy)</label>
                    <input id="txtDOB" type="text" name="dob" />
                    <p>A valid DOB is in the range of 1900 to the current year.</p>
                </div>
                <div class="input-container" id="inputDate">
                    <label>Date of Anything (mm/dd/yyyy)</label>
                    <input id="txtDate" type="text" name="date" />
                    <p>A valid date must be anything in mm/dd/yyyy format.</p>
                </div>
                <div class="input-container" id="inputTimeStandard" runat="server">
                    <label>What time is it now?</label>
                    <input id="txtTimeStandard" type="text" name="timestandard" />
                    <p>Time must be in 12-hour am/pm format.</p>
                </div>
                <div class="input-container" id="inputTimeMilitary">
                    <label>What time is it now?</label>
                    <input id="txtTimeMilitary" type="text" name="timemilitary" />
                    <p>Time must be in 24-hour format.</p>
                </div>
                <div class="input-container" id="inputNumber">
                    <label>What is your favorite number?</label>
                    <input id="txtNumber" type="text" name="number" />
                    <p>A valid number is an integer.</p>
                </div>
                <div class="input-container" id="inputCurrency">
                    <label>What is your hourly wage?</label>
                    <input id="txtCurrency" type="text" name="currency" />
                    <p>A valid decimal in US currency format.</p>
                </div>
                <div class="input-container" id="inputWebsite">
                    <label>What is the URL of your favorite website?</label>
                    <input id="txtWebsite" type="text" name="website" />
                    <p>A valid URL must start with http://, https://, ftp://, or www.</p>
                </div>
                <div class="submit-container">
                    <input id="btnSubmit" type="submit" name="Submit" value="Submit" onclick="return IsValid()" />
                    <input id="btnClear" type="button" name="Clear" value="Clear" onclick="ClearForm()" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
