namespace ProyectoFinal.Views;

public partial class redes : ContentPage
{
	public redes()
	{
		InitializeComponent();
	}
    private async void OnFacebookClicked(object sender, EventArgs e)
    {
        var url = "https://www.facebook.com/profile.php?id=61576350674645"; 
        await OpenUrl(url);
    }
    
    private async void OnInstagramClicked(object sender, EventArgs e)
    {
        var url = "https://www.instagram.com/skincheck3/"; 
        await OpenUrl(url);
    }

    private async Task OpenUrl(string url)
    {
        try
        {
            if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                await Launcher.OpenAsync(url); 
            }
            else
            {
                await DisplayAlert("Error", "La URL proporcionada no es válida", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Hubo un problema al abrir el enlace: {ex.Message}", "OK");
        }
    }
}