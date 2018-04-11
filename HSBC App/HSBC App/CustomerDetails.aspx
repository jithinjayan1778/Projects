<%@ Page Title="Customer Details" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CustomerDetails.aspx.cs" Inherits="HSBC_App.Customer_Details" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="buttons">
           <table style="background-color:rgba(168, 174, 186, 1)">
               <tr>
                   <td><asp:button runat="server" text="Add New " id="btn_AddNew"  /></td>
                   <td><asp:FileUpload ID="FileUpload1" runat="server" /></td>
                   <td><asp:Button ID="btn_uploadexcel" runat="server" Text="Upload Excel" onclick="uploadexcel_click" /></td>
               </tr>
           </table>                        
    </div>

    <cc1:ToolkitScriptManager runat="server"></cc1:ToolkitScriptManager>
    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="pnl_addnewcust" TargetControlID="btn_AddNew"  BackgroundCssClass="modalBackground" >
</cc1:ModalPopupExtender>
 <div id="model-dialog" style="display:none; ">

    <asp:Panel ID="pnl_addnewcust" runat="server" CssClass="modalPopup"     align="center"  EnableViewState="False" >
        <table>
            <div class="head" >
                <caption style="background-color:red;color:white; text-align:center">
                    Add New Customer</caption>
            </div>
            <tr>
                <td>
                    <asp:Label ID="lbl_masked" runat="server" Text="Masked"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_masked" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbl_Customer_Name" runat="server" Text="Customer Name"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_Name" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbl_EmailID" runat="server" Text="Customer Email ID"></asp:Label>

                </td>
                <td>
                    <asp:TextBox ID="txt_Email_ID" runat="server"></asp:TextBox><br />
                    <asp:Label ID="lbl_emaildescription" runat="server" Text="For multiple email seperate by ',' xxx@xxx.com,yyy@yyy.com"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbl_ccemail" runat="server" Text="CC"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_ccemail" runat="server" ></asp:TextBox><br />
                    <asp:Label ID="lbl_ccemaildescription" runat="server" Text="For multiple email seperate by ',' xxx@xxx.com,yyy@yyy.com"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbl_pdf_password" runat="server" Text="PDF Password"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_pdf_password" runat="server" type="password"></asp:TextBox>
                </td>
            </tr>
        </table><br/>
        <asp:Button ID="btn_AddNewCust" runat="server" Text="Save" OnClick="btn_AddNewCustClick" />
                <asp:Button ID="btn_close" runat="server" Text="Close" OnClientClick="close" /><br />
        <asp:Label ID="lbl_errormsg" runat="server" Text="" style="color:red"></asp:Label>
        </asp:Panel><br />
     </div>
            
    

    <div class="gridview">
    <asp:GridView ID="GridView1" runat="server" cssclass="Grid" AllowPaging="True" PageSize="20" AutoGenerateColumns="False" DataKeyNames="Masked" DataSourceID="SqlDataSource1" Width="70%" style="margin-top:30px" >
        <Columns>
            <asp:BoundField DataField="Masked" HeaderText="Masked" ReadOnly="True" SortExpression="Masked" />
            <asp:BoundField DataField="De_Masked" HeaderText="Name" SortExpression="De_Masked" />
            <asp:BoundField DataField="Email" HeaderText="Email ID" SortExpression="Email" />
            <asp:BoundField DataField="CCEmail" HeaderText="CC" SortExpression="CCEmail" />
            <asp:TemplateField ShowHeader="False">
            <ItemTemplate>
                <asp:LinkButton ID="btn_edit" runat="server" CausesValidation="False" 
                    CommandName="Select" Text="Edit" OnClick="btn_edit_Click"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
            <asp:TemplateField ShowHeader="False">
            <ItemTemplate>
                <asp:LinkButton ID="btn_delete" runat="server" CausesValidation="False" 
                    CommandName="Select" Text="Delete" OnClick="btn_delete_Click"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        </Columns>
    </asp:GridView>
        <div class="Excelbutton">
            <asp:Button ID="btn_download" runat="server" Text="Download Customer Details" OnClick="btn_download_Click" />
        </div>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:constr %>" DeleteCommand="DELETE FROM [Master] WHERE [Masked] = @original_Masked AND (([De_Masked] = @original_De_Masked) OR ([De_Masked] IS NULL AND @original_De_Masked IS NULL)) AND (([Email] = @original_Email) OR ([Email] IS NULL AND @original_Email IS NULL)) AND (([PDFPassword] = @original_PDFPassword) OR ([PDFPassword] IS NULL AND @original_PDFPassword IS NULL))" InsertCommand="INSERT INTO [Master] ([Masked], [De_Masked], [Email], [PDFPassword]) VALUES (@Masked, @De_Masked, @Email, @PDFPassword)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT * FROM [Master]" UpdateCommand="UPDATE [Master] SET [De_Masked] = @De_Masked, [Email] = @Email, [PDFPassword] = @PDFPassword WHERE [Masked] = @original_Masked AND (([De_Masked] = @original_De_Masked) OR ([De_Masked] IS NULL AND @original_De_Masked IS NULL)) AND (([Email] = @original_Email) OR ([Email] IS NULL AND @original_Email IS NULL)) AND (([PDFPassword] = @original_PDFPassword) OR ([PDFPassword] IS NULL AND @original_PDFPassword IS NULL))">
        <DeleteParameters>
            <asp:Parameter Name="original_Masked" Type="String" />
            <asp:Parameter Name="original_De_Masked" Type="String" />
            <asp:Parameter Name="original_Email" Type="String" />
            <asp:Parameter Name="original_PDFPassword" Type="String" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="Masked" Type="String" />
            <asp:Parameter Name="De_Masked" Type="String" />
            <asp:Parameter Name="Email" Type="String" />
            <asp:Parameter Name="PDFPassword" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="De_Masked" Type="String" />
            <asp:Parameter Name="Email" Type="String" />
            <asp:Parameter Name="PDFPassword" Type="String" />
            <asp:Parameter Name="original_Masked" Type="String" />
            <asp:Parameter Name="original_De_Masked" Type="String" />
            <asp:Parameter Name="original_Email" Type="String" />
            <asp:Parameter Name="original_PDFPassword" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>
        <br />
        <asp:GridView ID="GridView2" runat="server" style="display:none">
        </asp:GridView>

        <asp:Button ID="Button2" runat="server" Text="Button" style="display:none"></asp:Button>
        
<cc1:ModalPopupExtender ID="ModalPopupExtender3" runat="server" PopupControlID="pnl_custEdit" TargetControlID="Button2"  BackgroundCssClass="modalBackground"  Enabled="True" >
</cc1:ModalPopupExtender>
        <div id="model-dialog" style="display:none">
            <asp:Panel ID="pnl_custEdit" runat="server" CssClass="modalPopup"     align="center"  EnableViewState="False" >
        <table>
            <div class="head" >
                <caption style="background-color:red;color:white; text-align:center">
                    Edit Customer Details</caption>
            </div>
            <tr>
                <td>
                    <asp:Label ID="lbl_Editmasked" runat="server" Text="Masked"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_Editmasked" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbl_editcustname" runat="server" Text="Customer Name"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_editcustname" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbl_editcustemail" runat="server" Text="Customer Email ID"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_editcustemail" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbl_editcustccemail" runat="server" Text="CC"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_editcustccemail" runat="server" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbl_editPDFpass" runat="server" Text="PDF Password"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_editpdfpass" runat="server" ></asp:TextBox>
                </td>
            </tr>
        </table><br/>
        <asp:Button ID="btn_updatecustdetails" runat="server" Text="Update" OnClick="btn_updateCustClick" />
                <asp:Button ID="Button3" runat="server" Text="Close" OnClientClick="close" /><br />
        <asp:Label ID="Label5" runat="server" Text="" style="color:red"></asp:Label>
        </asp:Panel>

        </div>
        </div>
    <style>
    td
    {
            padding: 9px 20px 12px 10px;
    }
    .Grid {background-color: #fff; margin: 5px 0 10px 0; border: solid 1px #525252; border-collapse:collapse; font-family:Calibri; color: #474747;}

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
.buttons{
    padding:4.5%;
    margin: -12px 0px -87px 0px;
}










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
            width: 42%;
            height: auto;
        }
        .myGridClass td {
    
    color: black !important;
}
.myGridClass th {
    
    background-color: black !important;
}
.ui-dialog.ui-widget.ui-widget-content.ui-corner-all.ui-draggable.ui-resizable
{
    
    min-width: 80% !important;
   
    left: 10% !important;
    }
    .ui-widget-header {
     border: none !important;
     background: none !important;
    color: #e52c2b !important;
    font-weight: bold;
}
</style>
    <script>
    $(window).load(function () {
        $('.nav li a').click(function (e) {

            $('.nav li.active').removeClass('active');
            
               
                
            
        });
    });
        </script>
</asp:Content>