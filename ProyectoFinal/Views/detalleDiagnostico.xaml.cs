using System.Collections.ObjectModel;
using System.Net;
using Newtonsoft.Json;
using ProyectoFinal.Models;

namespace ProyectoFinal.Views;

public partial class detalleDiagnostico : ContentPage
{
    
    

    

    private async void btnCompartir_Clicked(object sender, EventArgs e)
    {
        try
        {
            WebClient cliente = new WebClient();
            var parametros = new System.Collections.Specialized.NameValueCollection();
            parametros.Add("nombre", txtNombre.Text);
            parametros.Add("resultado", txtResultado.Text);
            parametros.Add("fecha", txtFecha.Text);
            cliente.UploadValues("http://192.168.1.11/proyecto/insertarDiagnostico.php", "insertarDiagnostico", parametros);

        }
        catch (Exception ex)
        {
            DisplayAlert("Alerta", ex.Message, "Cerrar");
        }
    }

    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Eliminar", "�Est�s seguro de eliminar este diagn�stico?", "S�", "No");
        if (confirm)
        {
            // Aqu� deber�as eliminar de la base de datos (cuando est� lista)
            await DisplayAlert("Eliminado", "Diagn�stico eliminado correctamente", "OK");
            await Navigation.PopAsync();
        }
    }
}