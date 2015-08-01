<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Reportes._Default" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrappers">
             <asp:DropDownList ID="DropDownList1" runat="server" Height="33px" Width="25%" DataSourceID="SqlDataSource2" DataTextField="persona" DataValueField="uid">
            </asp:DropDownList>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="1954%" Width="98%" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" BackColor="#66CCFF">
                <LocalReport ReportPath="ReporteCobranza.rdlc" DisplayName="Reporte">
                    <DataSources>
                        <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="cobranzas" />
                    </DataSources>
                </LocalReport>
            </rsweb:ReportViewer>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DeleteMethod="Delete" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="Reportes.CobranzaTableAdapters.balanceTableAdapter" UpdateMethod="Update">
                <DeleteParameters>
                    <asp:Parameter Name="Original_id_solicitante" Type="Int32" />
                    <asp:Parameter Name="Original_factura" Type="Int32" />
                    <asp:Parameter Name="Original_num_documento" Type="Int32" />
                    <asp:Parameter Name="Original_fecha_captura" Type="DateTime" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="id_solicitante" Type="Int32" />
                    <asp:Parameter Name="factura" Type="Int32" />
                    <asp:Parameter Name="num_documento" Type="Int32" />
                    <asp:Parameter Name="descripcion" Type="String" />
                    <asp:Parameter Name="cantidad" Type="Decimal" />
                    <asp:Parameter Name="fecha_captura" Type="DateTime" />
                    <asp:Parameter Name="fecha_vencimiento" Type="DateTime" />
                    <asp:Parameter Name="fecha_pago" Type="DateTime" />
                    <asp:Parameter Name="capturista" Type="Int32" />
                    <asp:Parameter Name="borrado" Type="Boolean" />
                    <asp:Parameter Name="cubierto" Type="Boolean" />
                </InsertParameters>
                <SelectParameters>
                    <asp:ControlParameter ControlID="DropDownList1" Name="solicitante" PropertyName="SelectedValue" Type="Int32" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:Parameter Name="descripcion" Type="String" />
                    <asp:Parameter Name="cantidad" Type="Decimal" />
                    <asp:Parameter Name="fecha_vencimiento" Type="DateTime" />
                    <asp:Parameter Name="fecha_pago" Type="DateTime" />
                    <asp:Parameter Name="capturista" Type="Int32" />
                    <asp:Parameter Name="borrado" Type="Boolean" />
                    <asp:Parameter Name="cubierto" Type="Boolean" />
                    <asp:Parameter Name="Original_id_solicitante" Type="Int32" />
                    <asp:Parameter Name="Original_factura" Type="Int32" />
                    <asp:Parameter Name="Original_num_documento" Type="Int32" />
                    <asp:Parameter Name="Original_fecha_captura" Type="DateTime" />
                </UpdateParameters>
            </asp:ObjectDataSource>
           <a href='<%=Page.ResolveClientUrl("Reportes.aspx ")%>'>Detalles</a> 
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:caja_popular2ConnectionString %>" SelectCommand="SELECT id_solicitante, factura, num_documento, descripcion, cantidad, fecha_captura, fecha_vencimiento, fecha_pago, capturista, borrado, cubierto FROM balance WHERE (borrado &lt;&gt; 1) AND (id_solicitante = @solicitante)">
                <SelectParameters>
                    <asp:ControlParameter ControlID="DropDownList1" Name="solicitante" PropertyName="SelectedValue" DefaultValue="1" />
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:caja_popular2ConnectionString %>" SelectCommand="SELECT s.uid, p.paterno + ' ' + p.materno + ', ' + p.nombre + ' / ' + s.num_solicitante AS persona FROM solicitante AS s INNER JOIN persona AS p ON s.id_persona = p.uid ORDER BY p.paterno, p.materno, p.nombre" OnSelecting="SqlDataSource2_Selecting"></asp:SqlDataSource>
        </div>
    </section>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
   
</asp:Content>
<asp:Content ID="Content1" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        .content-wrappers {
            height: 39px;
        }
    </style>
</asp:Content>

