<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="loginTheatre._Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1><%: Title %>log in<br />
                    <asp:Label ID="Label8" runat="server" Text="User"></asp:Label>
                </h1>
            </hgroup>
        </div>
    </section>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h3>We suggest the following:</h3>
    <ol class="round">
        <li class="one">
            <asp:Label ID="Label1" runat="server" Text="name"></asp:Label>
                    <asp:TextBox ID="TextBox1" runat="server" Height="16px"></asp:TextBox>
                    <br />
                    <asp:Label ID="Label2" runat="server" Text="surname"></asp:Label>
                    <asp:TextBox ID="TextBox2" runat="server" Height="16px"></asp:TextBox>
                    <br />
                    <asp:Label ID="Label3" runat="server" Text="address"></asp:Label>
                    <asp:TextBox ID="TextBox3" runat="server" Height="16px"></asp:TextBox>
                    <br />
                    <asp:Label ID="Label4" runat="server" Text="mail"></asp:Label>
                    <asp:TextBox ID="TextBox4" runat="server" Height="16px"></asp:TextBox>
                    <br />
                    <asp:Label ID="Label6" runat="server" Text="pass"></asp:Label>
                    <asp:TextBox ID="TextBox6" runat="server" Height="16px"></asp:TextBox>
                    <br />
                    <asp:Label ID="Label7" runat="server" Text="repeat"></asp:Label>
                    <asp:TextBox ID="TextBox7" runat="server" Height="16px"></asp:TextBox> 
                    <br />
                    <asp:Button ID="Button1" runat="server" Text="Come in" OnClientClick="OnComeIn"></asp:Button>
         </li>
        <li class="one">
        </li>
    </ol>
</asp:Content>
