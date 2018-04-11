<%@ Page Title="Configuration" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Configuration.aspx.cs" Inherits="HSBC_App.Configuration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="configuration" style="padding-top:7.5%;margin-left:24%">
        <table>
            <caption style="text-align:center;background-color:#47474c;color:white">
                SMTP Details 
            </caption>
            <tr>
                <td>
                    <asp:Label ID="lbl_SMTPServer" runat="server" Text="SMTP Server "></asp:Label>
                </td>
                <td class="textbox">
                    <asp:TextBox ID="txt_SMTPserver" runat="server" Width="350px" Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbl_SMTPPort" runat="server" Text="SMTP Port"></asp:Label>
                </td>
                <td class="textbox">
                    <asp:TextBox ID="txt_SMTPPort" runat="server" Width="350px" Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbl_FTPURLSource" runat="server" Text="FTP Source URL"></asp:Label>
                </td>
                <td class="textbox">
                    <asp:TextBox ID="txt_FTPURLSource" runat="server" Width="350px" Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbl_FTPURLDestination" runat="server" Text="FTP Destination URL"></asp:Label>
                </td>
                <td class="textbox">
                    <asp:TextBox ID="txt_FTPURLDestination" runat="server" Width="350px" Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbl_EmailID" runat="server" Text="Email ID"></asp:Label>
                </td>
                <td class="textbox">
                    <asp:TextBox ID="txt_EmailID" runat="server" Width="350px" Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbl_password" runat="server" Text="Password"></asp:Label>
                </td>
                <td class="textbox">
                    <asp:TextBox ID="txt_password" runat="server" Width="350px" Enabled="False"></asp:TextBox>
                </td>
            </tr>
         </table>
        <div class="buttons" style="margin-left:202px" >
        <asp:Button ID="btn_Edit" runat="server" Text="Edit" OnClick="btn_Edit_Click" style="width:10%;"/>
        <asp:Button ID="btn_update" runat="server" Text="Update" OnClick="btn_update_Click" style="width:10%;"/>
            </div>
    </div>
    <style>
        td
        {
            padding: 6px;
            
        }
        table
        {
            width:69%;
        }
        
    </style>
    </asp:Content>
