using System.Collections.ObjectModel;
using ProyectoFinal.Models;
using System.Net.Http;
using Microsoft.Maui.Controls;
using Newtonsoft.Json;

namespace ProyectoFinal.Views;

public partial class historial : ContentPage
{
    private const string Url = "http://192.168.1.11/proyecto/listarDiagnosticos.php";
    private readonly HttpClient cliente = new HttpClient();

    private async Task CargarDiagnosticos()
    {
        try
        {
            var contenido = await cliente.GetStringAsync(Url);
            var lista = JsonConvert.DeserializeObject<List<Diagnostico>>(contenido);

            // Convertir la ruta relativa de imagen a URL absoluta
            foreach (var d in lista)
            {
                d.Imagen = "http://192.168.1.11/proyecto/" + d.Imagen;
            }

            listaDiagnostico.ItemsSource = new ObservableCollection<Diagnostico>(lista);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudo obtener los datos: {ex.Message}", "OK");
        }
    }
    public historial()
    {
        InitializeComponent();
        _ = CargarDiagnosticos(); // Llamar sin await directamente en el constructor
    }

    private async void Eliminar_Clicked(object sender, EventArgs e)
    {
        var boton = sender as Button;
        var id = boton?.CommandParameter?.ToString();

        if (string.IsNullOrWhiteSpace(id))
        {
            await DisplayAlert("Error", "ID inválido", "OK");
            return;
        }

        bool confirmacion = await DisplayAlert("Confirmar", "¿Deseas eliminar este diagnóstico?", "Sí", "No");

        if (confirmacion)
        {
            try
            {
                var url = $"http://192.168.1.11/proyecto/eliminarDiagnostico.php?id={id}";

                // Solo para depuración
                Console.WriteLine($"URL generada: {url}");
                await DisplayAlert("Depuración", $"URL: {url}", "OK");

                HttpClient client = new HttpClient();
                var response = await client.GetAsync(url);
                var contenido = await response.Content.ReadAsStringAsync();

                //await DisplayAlert("Respuesta del servidor", contenido, "OK");

                if (response.IsSuccessStatusCode && contenido.Contains("success"))
                {
                    await DisplayAlert("Éxito", "Diagnóstico eliminado correctamente.", "OK");
                    await CargarDiagnosticos(); // Recargar la lista
                }
                else
                {
                    await DisplayAlert("Error", "No se pudo eliminar el diagnóstico.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Excepción: {ex.Message}", "OK");
            }
        }
    }
    
}