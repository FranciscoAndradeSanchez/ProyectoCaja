<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reportes.aspx.cs" Inherits="Reportes.Reportes" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource2" DataTextField="persona" DataValueField="uid">
    </asp:DropDownList>
    <br />
    <br />
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" BackColor="#0033CC" BorderColor="#FFFF99" Font-Names="Verdana" Font-Size="8pt" Height="80%" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="98%">
        <LocalReport ReportPath="Report1.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="ObjectDataSource2" Name="cobrancita" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
    <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" DeleteMethod="Delete" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="Reportes.CobranzaTableAdapters.balanceTableAdapter" UpdateMethod="Update">
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
            <asp:ControlParameter ControlID="DropDownList1" DefaultValue="1" Name="solicitante" PropertyName="SelectedValue" Type="Int32" />
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
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName="Reportes.CobranzaTableAdapters.balanceTableAdapter"/>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:caja_popular2ConnectionString %>" SelectCommand="SELECT id_solicitante, factura, num_documento, descripcion, cantidad, fecha_captura, fecha_vencimiento, fecha_pago, capturista, borrado, cubierto FROM balance WHERE (borrado &lt;&gt; 1) AND (id_solicitante = @solicitante)">
        <SelectParameters>
            <asp:ControlParameter ControlID="DropDownList1" DefaultValue="1" Name="solicitante" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:caja_popular2ConnectionString %>" SelectCommand="SELECT s.uid, p.paterno + ' ' + p.materno + ', ' + p.nombre + ' / ' + s.num_solicitante AS persona FROM solicitante AS s INNER JOIN persona AS p ON s.id_persona = p.uid ORDER BY p.paterno, p.materno, p.nombre"></asp:SqlDataSource>
</asp:Content>
