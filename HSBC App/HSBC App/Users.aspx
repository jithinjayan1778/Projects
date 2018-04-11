<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="HSBC_App.Users" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
        <div style="padding:2.5%">
            <asp:Button ID="btn_NewUser" runat="server" Text="Add New User" />
        </div>
    <cc1:toolkitscriptmanager runat="server"></cc1:toolkitscriptmanager>
    <cc1:modalpopupextender ID="ModalPopupExtender1" runat="server" PopupControlID="pnl_addnewuser" TargetControlID="btn_NewUser"  BackgroundCssClass="modalBackground" >
</cc1:modalpopupextender>
 <div id="model-dialog" style="display:none; ">

    <asp:Panel ID="pnl_addnewuser" runat="server" CssClass="modalPopup"     align="center"  EnableViewState="False" >
        <table>
            <div class="head" >
                <caption style="background-color:red;color:white; text-align:center">
                    Add New User</caption>
            </div>
            <tr>
                <td>
                    <asp:Label ID="lbl_username" runat="server" Text="User Name"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_username" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbl_Password" runat="server" Text="Password"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_password" runat="server"></asp:TextBox>
                </td>
            </tr>                        
        </table><br/>
        <asp:Button ID="btn_AddNew" runat="server" Text="Save" OnClick="btn_AddNewuser_Click" />
                <asp:Button ID="btn_close" runat="server" Text="Close" OnClientClick="close" /><br />
        <asp:Label ID="lbl_errormsg" runat="server" Text="" style="color:red"></asp:Label>
        </asp:Panel><br />
     </div>
    <div >
    <asp:GridView ID="GridView1" cssclass="Grid" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="Username" DataSourceID="SqlDataSource1">
        <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="ID" />
            <asp:BoundField DataField="Username" HeaderText="User Name" ReadOnly="True" SortExpression="Username" />
            <asp:BoundField DataField="Password" HeaderText="Password" SortExpression="Password" />
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:constr %>" DeleteCommand="DELETE FROM [Users] WHERE [Username] = @original_Username AND [ID] = @original_ID AND (([Password] = @original_Password) OR ([Password] IS NULL AND @original_Password IS NULL))" InsertCommand="INSERT INTO [Users] ([Username], [Password]) VALUES (@Username, @Password)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT * FROM [Users]" UpdateCommand="UPDATE [Users] SET [ID] = @ID, [Password] = @Password WHERE [Username] = @original_Username AND [ID] = @original_ID AND (([Password] = @original_Password) OR ([Password] IS NULL AND @original_Password IS NULL))">
        <DeleteParameters>
            <asp:Parameter Name="original_Username" Type="String" />
            <asp:Parameter Name="original_ID" Type="Int32" />
            <asp:Parameter Name="original_Password" Type="String" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="Username" Type="String" />
            <asp:Parameter Name="Password" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="ID" Type="Int32" />
            <asp:Parameter Name="Password" Type="String" />
            <asp:Parameter Name="original_Username" Type="String" />
            <asp:Parameter Name="original_ID" Type="Int32" />
            <asp:Parameter Name="original_Password" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>
        </div>
    <style>
        .Grid {
            background-color: #fff; 
            margin: 5px 0 10px 0; 
            border: solid 1px #525252; 
            border-collapse:collapse;
             font-family:Calibri; 
             color: #474747;
             width:35%;
        }

.Grid td {

      padding: 2px;

      border: solid 1px #c1c1c1; }

.Grid th  {

      padding : 4px 2px;

      color: #fff;

      background: #47474c url(Images/grid-header.png) repeat-x top;

      border-left: solid 1px #525252;

      font-size: 0.9em; }

.Grid .alt {

      background: #fcfcfc url(Images/grid-alt.png) repeat-x top; }

.Grid .pgr {background: #363670 url(Images/grid-pgr.png) repeat-x top; }

.Grid .pgr table { margin: 3px 0; }

.Grid .pgr td { border-width: 0; padding: 0 6px; border-left: solid 1px #666; font-weight: bold; color: #fff; line-height: 12px; }  

.Grid .pgr a { color: Gray; text-decoration: none; }

.Grid .pgr a:hover { color: #000; text-decoration: none; }
.modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }
        .modalPopup
        {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 25%;
            height: auto;
        }
    </style>
</asp:Content>
