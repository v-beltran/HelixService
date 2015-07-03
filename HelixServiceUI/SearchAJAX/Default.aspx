<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HelixServiceUI.SearchAJAX.Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>United States of America - Search</title>
    <script type="text/javascript" src="../js/jquery-1.11.1.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            // Do not show results, since there was nothing searched for yet.
            $("#divSearchResults").hide();

            // Enter-key press will initiate the AJAX request using the 'Search' button click event.
            $("#txtSearch").keypress(function (event) {
                if (event.which === 13) {
                    $('#btnSearch').trigger('click');
                    return false;
                }
            });

            // Button click event to use AJAX request.
            $("#btnSearch").click(function () {
                GetStates();
            });

            // This function will get search results from an AJAX request.
            function GetStates() {
                // Get user's search term(s).
                var searchTerm = $("#txtSearch").val();

                $.ajax({
                    type: 'GET',
                    url: 'Search.ashx',
                    data: { "Search": searchTerm },
                    dataType: 'json',
                    success: function (data) {
                        // We got results successfully, show results.
                        $("#divSearchResults").show();

                        // Remove previous results.
                        $("#tblSearchResults tbody .tblSearchRow").remove();

                        // Indicate how many results were returned.
                        $("#pSearchCount").html("Found: " + data.length);

                        // Populate table rows with results.
                        for (var i = 0; i < data.length; i++) {
                            $("#tblSearchResults tbody").append("<tr class='tblSearchRow'></td>");
                            $("#tblSearchResults tbody tr:last").append("<td>" + data[i].AnsiCode + "</td>");
                            $("#tblSearchResults tbody tr:last").append("<td>" + data[i].Name + "</td>");
                            $("#tblSearchResults tbody tr:last").append("<td>" + data[i].Capital + "</td>");
                            $("#tblSearchResults tbody tr:last").append("<td>" + data[i].LargestCity + "</td>");
                            $("#tblSearchResults tbody tr:last").append("<td>" + data[i].LargestMetro + "</td>");
                        }
                    },
                    error: function (xhr, status, error) {
                        alert("Something bad happened.");
                    }
                });
            }
        });
    </script>
    <style>
        tr, th, td {
            text-align: left;
            padding: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <p>Search for a state in the United States to find more information about it.</p>
            <input id="txtSearch" type="text" />
            <input id="btnSearch" type="button" value="Search" />
        </div>
        <div id="divSearchResults">
            <p id="pSearchCount"></p>
            <table id="tblSearchResults">
                <tbody>
                    <tr id="tblSearchHeader">
                        <th>State Code
                        </th>
                        <th>State Name
                        </th>
                        <th>State Capital
                        </th>
                        <th>State Largest City
                        </th>
                        <th>State Largest Metro
                        </th>
                    </tr>
                </tbody>
            </table>
        </div>
    </form>
</body>
</html>
