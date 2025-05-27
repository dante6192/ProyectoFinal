using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.IO;

namespace ProyectoFinal.Views;

public partial class Resultado : ContentPage
{
    
    string ImagenBase64 = "";

    private FileResult fotoTomada;
    private const string UrlPost = "http://192.168.1.11/proyecto/insertarDiagnostico.php";
    private readonly HttpClient cliente = new HttpClient();
    private FileResult _foto;

    public Resultado(FileResult foto, string variable)
    {
        InitializeComponent();
        txtResultado.Text = variable;
        txtFecha.Text = DateTime.Now.ToString("yyyy/MM/dd");
        _foto = foto;
        MostrarImagen();
    }
    private async void MostrarImagen()
    {
        try
        {
            if (_foto != null)
            {
                var stream = await _foto.OpenReadAsync();
                imgResultado.Source = ImageSource.FromStream(() => stream);
            }
        }
        catch (Exception ex)
        {

            await DisplayAlert("Error", $"No se pudo mostrar la imagen: {ex.Message}", "Ok");
        }
        
    }

    private async void OnVolverCapturaClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void OnGuardarResultadoClicked(object sender, EventArgs e)
    {
        try
        {
            string imagenBase64 = await ConvertirImagenABase64Async(_foto);
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtResultado.Text) ||
            string.IsNullOrWhiteSpace(txtFecha.Text) || string.IsNullOrWhiteSpace(imagenBase64))
            {
                await DisplayAlert("Error", "Faltan datos por llenar.", "OK");
                return;
            }
            var parametros = new Dictionary<string, string>
        {
            { "nombre", txtNombre.Text },
            { "resultado", txtResultado.Text },
            { "fecha", txtFecha.Text },
            { "imagen", imagenBase64 }
        };

            var content = new FormUrlEncodedContent(parametros);
            var response = await cliente.PostAsync("http://192.168.1.11/proyecto/insertarDiagnostico.php", content);
            var respuesta = await response.Content.ReadAsStringAsync();

            await DisplayAlert("Respuesta del servidor", respuesta, "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Alerta", ex.Message, "Cerrar");
        }
    }
    private async Task<string> ConvertirImagenABase64Async(FileResult foto)
    {
        if (foto != null)
        {
            using var stream = await foto.OpenReadAsync();
            using var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            byte[] bytes = memoryStream.ToArray();
            return Convert.ToBase64String(bytes);
        }
        return null;
    }
}