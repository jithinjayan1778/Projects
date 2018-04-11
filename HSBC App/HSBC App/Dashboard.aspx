<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="HSBC_App.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


<button class="btn btn-primary" type="button" style="background-color:#365ff4b8">
 <a href="CustomerDetails.aspx" style="color:white">Customers <span class="badge" style="color:#f44336">
     <asp:Label ID="lbl_noofcustomers" runat="server" Text=""></asp:Label></span></a>
</button>
    <button class="btn btn-primary" type="button" style="background-color:#be0cca">
 <a href="MailSentDetails.aspx" style="color:white">Mails Sent Today <span class="badge" style="color:#f44336">
     <asp:Label ID="lbl_NoOfMailssenttoday" runat="server" Text=""></asp:Label></span></a>
</button>
    <button class="btn btn-primary" type="button" style="background-color:#ef2424">
 <a href="MailSentDetails.aspx" style="color:white">Mails Sent For this Month <span class="badge" style="color:#f44336">
     <asp:Label ID="lbl_mailssentthismonth" runat="server" Text=""></asp:Label></span></a>
</button>
    <style>
        button.btn.btn-primary{
            margin-top: 20px;
    width: 25%;
    height: 100px;
        }
    </style>
</asp:Content>
