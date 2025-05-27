namespace ProyectoFinal.Views;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using ProyectoFinal.Models;
using System.Threading.Tasks;

public partial class captura : ContentPage
{
    //analizar con IA
    public async Task<string> EnviarImagenAsync(string rutaImagen)
    { 
            try
            {
                var httpClient = new HttpClient();
                var url = "http://192.168.1.11:5000/analizar-imagen"; // Ajusta a tu IP

                using var form = new MultipartFormDataContent();
                using var imagenStream = File.OpenRead(rutaImagen);
                using var fileContent = new StreamContent(imagenStream);

                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");

                form.Add(fileContent, "imagen", Path.GetFileName(rutaImagen));

                var response = await httpClient.PostAsync(url, form);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return json;
                }
                else
                {
                    return $"Error en la solicitud: {response.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                return $"Excepción: {ex.Message}";
            }
    }
    //FIN DEL ANALISIS
    string ImagenBase64 = "";
    private FileResult fotoTomada;
    private const string UrlPost = "http://192.168.1.11/proyecto/insertarDiagnostico.php";
    private readonly HttpClient cliente = new HttpClient();
    public async Task EnviarDiagnosticoAsync()
    {
        var nuevoDiagnostico = new Diagnostico
        {
            NombrePaciente = "Juan Pérez",
            Resultado = "Negativo",
            Fecha = DateTime.Now.ToString("yyyy-MM-dd"),
            Imagen = ImagenBase64
        };

        var json = JsonConvert.SerializeObject(nuevoDiagnostico);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            var response = await cliente.PostAsync(UrlPost, content);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Éxito", "Diagnóstico enviado correctamente.", "OK");
            }
            else
            {
                await DisplayAlert("Error", "Hubo un problema al enviar el diagnóstico.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
    public captura()
	{
		InitializeComponent();
	}
    private async void OnUsarCamaraClicked(object sender, EventArgs e)
    {
        try
        {
            fotoTomada = await MediaPicker.CapturePhotoAsync();

            if (fotoTomada != null)
            {
                using Stream stream = await fotoTomada.OpenReadAsync();
                var imageStream = await fotoTomada.OpenReadAsync();
                imgPreview.Source = ImageSource.FromStream(() => imageStream);

                imageStream.Position = 0; // Reiniciar posición
                using var memoryStream = new MemoryStream();
                await stream.CopyToAsync(memoryStream);
                byte[] imageBytes = memoryStream.ToArray();
                string base64Image = Convert.ToBase64String(imageBytes);

                // Guardamos la imagen base64 en una variable para luego enviarla
                ImagenBase64 = base64Image;

            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudo tomar la foto: {ex.Message}", "OK");
        }
    }
    private async void OnContinuarClicked(object sender, EventArgs e)
    {
        try
        {  
            if (fotoTomada != null)
            {
                var imageStream = await fotoTomada.OpenReadAsync();
                //////// analisis
                string rutaTemporal = Path.Combine(FileSystem.CacheDirectory, fotoTomada.FileName);

                using (var stream = await fotoTomada.OpenReadAsync())
                using (var nuevoStream = File.OpenWrite(rutaTemporal))
                    await stream.CopyToAsync(nuevoStream);

                // Enviar la imagen a la API
                var resultado = await EnviarImagenAsync(rutaTemporal);
                await DisplayAlert("Resultado del diagnóstico", resultado, "OK");
                string variable = resultado;
                /////
                await Navigation.PushAsync(new Views.Resultado(fotoTomada, variable));
               
                
            }
            else
            {
                await DisplayAlert("Aviso", "Primero debes tomar una foto antes de continuar.", "OK");
            }
        }
        catch (Exception ex)
        {

            await DisplayAlert("Error", $"Hubo un problema: {ex.Message}", "OK");
        }
        
    }
 
    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Views.historial());
    }
}