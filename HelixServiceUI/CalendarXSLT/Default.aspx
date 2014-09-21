<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HelixServiceUI.CalendarXSLT.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Calendar</title>
    <script type="text/javascript" src="../js/jquery-1.11.1.min.js"></script>
    <style>
        * {
            font-family: Calibri, Candara, Segoe, "Segoe UI", Optima, Arial, sans-serif;
            font-size: 12px;
        }

        .calendar {
            white-space: normal;
            border-collapse: collapse;
            width: 980px;
        }

            .calendar th,
            .calendar tr,
            .calendar td {
                border: 1px solid yellowgreen;
            }

            .calendar thead {
                text-transform: uppercase;
            }

            .calendar .week th {
                background-color: darkseagreen;
                color: white;
                width: 140px;
                height: 20px;
            }

            .calendar .days td {
                vertical-align: top;
                max-width: 140px;
                height: 140px;
            }

            .calendar .days .day {
                width: 10%;
                display: block;
                text-align: right;
                padding: 5px;
                float: right;
            }

            .calendar .days .today {
                background-color: #E2F8B8;
            }

            .calendar .days .event {
                width: 75%;
                display: block;
                text-align: left;
                word-wrap: break-word;
                padding: 5px;
            }

            .calendar .calendar-header {
                background-color: darkolivegreen;
                color: white;
            }

                .calendar .calendar-header th {
                    border: none;
                }

                    .calendar .calendar-header th:first-child {
                        text-align: left;
                        padding-left: 10px;
                    }

                    .calendar .calendar-header th:last-child {
                        text-align: right;
                        padding-right: 10px;
                    }

                .calendar .calendar-header .month {
                    padding: 3px 0 3px 0;
                    text-align: center;
                    font-size: 24px;
                    text-transform: uppercase;
                }

                .calendar .calendar-header a {
                    color: white;
                    text-decoration: none;
                    font-size: 24px;
                    cursor: pointer;
                }

        #divFilter {
            display: block;
            margin-bottom: 10px;
        }

            #divFilter span, #divFilter input {
                font-size: 14px;
                margin: 0 10px 0 10px;
            }

            #divFilter select, #divFilter input {
                height: 25px;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="divFilter">
            <span>Year: </span>
            <asp:DropDownList ID="ddlYear" runat="server"></asp:DropDownList>
            <span>Month: </span>
            <asp:DropDownList ID="ddlMonth" runat="server"></asp:DropDownList>
            <asp:Button ID="btnSubmit" Text="Submit" runat="server" OnClick="btnSubmit_Click" />
        </div>
        <div id="divCalendar" runat="server"></div>
    </form>
    <script type="text/javascript">
        // Set postback links for previous and next month.
        var prevMonth = $("#prev").attr("data-month");
        var nextMonth = $("#next").attr("data-month");
        $("#prev").attr("href", "javascript:__doPostBack('prev','" + prevMonth + "')");
        $("#next").attr("href", "javascript:__doPostBack('next','" + nextMonth + "')");

        // Add CSS class for today.
        $(".event").each(function () {
            if ($(this).text().indexOf("Today") >= 0) {
                $(this).parent().addClass("today");
                return false;
            }
        });
    </script>
</body>
</html>
