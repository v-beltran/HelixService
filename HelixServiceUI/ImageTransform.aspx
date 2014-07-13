<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImageTransform.aspx.cs" Inherits="HelixServiceUI.ImageTransform" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        * {
            background-color: black;
            color: white;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Original</h2>
            <p>
                <img src="image.ashx" />
            </p>
        </div>
        <div>
            <h2>Resize: Width 200px</h2>
            <p>
                <img src="image.ashx?width=200" />
            </p>
        </div>
        <div>
            <h2>Resize: Height 200px</h2>
            <p>
                <img src="image.ashx?height=200" />
            </p>
        </div>
        <div>
            <h2>Resize: 200x200</h2>
            <p>
                <img src="image.ashx?width=200&height=200" />
            </p>
        </div>
        <div>
            <h2>Scale: 0.5 ratio</h2>
            <p>
                <img src="image.ashx?scale=0.5" />
            </p>
        </div>
        <div>
            <h2>Scale: 1.5 width and 0.5 height </h2>
            <p>
                <img src="image.ashx?scalew=1.5&scaleh=0.5" />
            </p>
        </div>
        <div>
            <h2>Crop: 3x3 grid</h2>
            <p>
                <img src="image.ashx?x=left&y=top" />
                <img src="image.ashx?x=center&y=top" />
                <img src="image.ashx?x=right&y=top" /><br />
                <img src="image.ashx?x=left&y=center" />
                <img src="image.ashx?x=center&y=center" />
                <img src="image.ashx?x=right&y=center" /><br />
                <img src="image.ashx?x=left&y=bottom" />
                <img src="image.ashx?x=center&y=bottom" />
                <img src="image.ashx?x=right&y=bottom" />
            </p>
        </div>
        <div>
            <h2>Crop: 1x3 grid </h2>
            <p>
                <img src="image.ashx?y=top" /><br />
                <img src="image.ashx?y=center" /><br />
                <img src="image.ashx?y=bottom" /><br />
            </p>
        </div>
        <div>
            <h2>Crop: 3x1 grid </h2>
            <p>
                <img src="image.ashx?x=left" />
                <img src="image.ashx?x=center" />
                <img src="image.ashx?x=right" />
            </p>
        </div>
        <div>
            <h2>Crop: x1 (302), y1 (143), x2(155), y2(224)</h2>
            <p>
                <img src="image.ashx?x1=302&y1=143&x2=155&y2=224" />
            </p>
        </div>
        <div>
            <h2>Mix: Resize 300x300, Scale 2x, Crop Center</h2>
            <p>
                <img src="image.ashx?width=300&height=300&scale=2&x=center&y=center" />
            </p>
        </div>
    </form>
</body>
</html>
