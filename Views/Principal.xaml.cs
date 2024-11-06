using kvargasS5B.Models;
using System.Xml.Linq;

namespace kvargasS5B.Views;

public partial class Principal : ContentPage
{
    string tipo = "N";
	public Principal()
	{
		InitializeComponent();
        btnAdd.Text = "Agregar Persona";

    }

    private void btnAdd_Clicked(object sender, EventArgs e)
    {
        if (tipo.Equals("N"))
        {
            App.personRepo.AddNewPerson(txtName.Text);
            lblStatus.Text = App.personRepo.StatusMessage;
        }

        if (tipo.Equals("E"))
        {
            App.personRepo.EditPerson(Convert.ToInt32(txtId.Text), txtName.Text);
            lblStatus.Text = App.personRepo.StatusMessage;
        }

        List<Persona> people = App.personRepo.GetAllPeople();
        listaPersonas.ItemsSource = people;
    }

    private void btnMostrar_Clicked(object sender, EventArgs e)
    {
        List<Persona> people = App.personRepo.GetAllPeople();
        listaPersonas.ItemsSource = people;
    }

    private void btnNuevo_Clicked(object sender, EventArgs e)
    {
        tipo = "N";
        btnAdd.Text = "Agregar Persona";
        btnAdd.BackgroundColor = Colors.Blue;
        txtName.Text = "";
        txtId.Text = "";
    }

    private void listaPersonas_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedPerson = e.CurrentSelection.FirstOrDefault() as Persona;
        if (selectedPerson != null)
        {
            ShowEditDeleteOptions(selectedPerson);
        }

        listaPersonas.SelectedItem = null;
    }
    private async void ShowEditDeleteOptions(Persona selectedPerson)
    {
        string action = await DisplayActionSheet("Opciones", "Cancelar", null, "Editar", "Eliminar");

        if (action == "Editar")
        {
            tipo = "E";
            btnAdd.Text = "Editar Persona";
            btnAdd.BackgroundColor = Colors.Aqua;
            txtName.Text = selectedPerson.Name;
            txtId.Text = Convert.ToString(selectedPerson.Id);
        }
        else if (action == "Eliminar")
        {
            App.personRepo.DeletePerson(Convert.ToInt32(selectedPerson.Id));
            lblStatus.Text = App.personRepo.StatusMessage;
            List<Persona> people = App.personRepo.GetAllPeople();
            listaPersonas.ItemsSource = people;
        }
    }
}