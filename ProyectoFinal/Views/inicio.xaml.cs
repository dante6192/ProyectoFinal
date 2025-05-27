using System.Threading.Tasks;

namespace ProyectoFinal.Views;

public partial class inicio : ContentPage
{
	public inicio()
	{
		InitializeComponent();
	}
    private async void OnEmpezarClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Views.captura());
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Views.redes());
    }
}