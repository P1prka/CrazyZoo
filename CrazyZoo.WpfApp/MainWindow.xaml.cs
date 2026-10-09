using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using CrazyZoo.Core;

namespace CrazyZoo.WpfApp;

public partial class MainWindow : Window
{
    private readonly ObservableCollection<Animal> animals = new();

    public MainWindow()
    {
        InitializeComponent();

        AnimalListBox.ItemsSource = animals;

        SwimButton.IsEnabled = false;
        PerformButton.IsEnabled = false;
    }

    private void AddAnimal_Click(object sender, RoutedEventArgs e)
    {
        string name = NameTextBox.Text.Trim();

        if (!int.TryParse(EnergyTextBox.Text, out int energy))
        {
            MessageBox.Show("Energy must be a number.");
            return;
        }

        if (AnimalTypeComboBox.SelectedItem is not ComboBoxItem selectedItem)
        {
            MessageBox.Show("Select an animal type.");
            return;
        }

        string animalType =
            selectedItem.Content.ToString()!;

        try
        {
            Animal animal;

            if (animalType == "Lion")
            {
                animal = new Lion(name, energy);
            }
            else if (animalType == "Zebra")
            {
                animal = new Zebra(name, energy);
            }
            else if (animalType == "Giraffe")
            {
                animal = new Giraffe(name, energy);
            }
            else if (animalType == "Hippo")
            {
                animal = new Hippo(name, energy);
            }
            else
            {
                animal = new Penguin(name, energy);
            }

            animals.Add(animal);

            LogListBox.Items.Add(
                $"Added {animal}.");

            NameTextBox.Clear();
            EnergyTextBox.Text = "50";
            AnimalTypeComboBox.SelectedIndex = -1;
        }
        catch (ArgumentException ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void AnimalListBox_SelectionChanged(
        object sender,
        SelectionChangedEventArgs e)
    {
        UpdateSelectedAnimal();
    }

    private void NormalAction_Click(
        object sender,
        RoutedEventArgs e)
    {
        if (AnimalListBox.SelectedItem is not Animal animal)
        {
            MessageBox.Show("Select an animal.");
            return;
        }

        string message = animal.NormalAction();

        LogListBox.Items.Add(message);

        UpdateSelectedAnimal();
    }

    private void CrazyAction_Click(
        object sender,
        RoutedEventArgs e)
    {
        if (AnimalListBox.SelectedItem is not Animal animal)
        {
            MessageBox.Show("Select an animal.");
            return;
        }

        string message = animal.CrazyAction();

        LogListBox.Items.Add(message);

        UpdateSelectedAnimal();
    }

    private void Swim_Click(
        object sender,
        RoutedEventArgs e)
    {
        if (AnimalListBox.SelectedItem is ISwimmable swimmer)
        {
            string message = swimmer.Swim();

            LogListBox.Items.Add(message);

            UpdateSelectedAnimal();
        }
    }

    private void Perform_Click(
        object sender,
        RoutedEventArgs e)
    {
        if (AnimalListBox.SelectedItem is IPerformer performer)
        {
            string message = performer.Perform();

            LogListBox.Items.Add(message);

            UpdateSelectedAnimal();
        }
    }

    private void Remove_Click(
        object sender,
        RoutedEventArgs e)
    {
        if (AnimalListBox.SelectedItem is not Animal animal)
        {
            MessageBox.Show("Select an animal.");
            return;
        }

        animals.Remove(animal);

        LogListBox.Items.Add(
            $"Removed {animal}.");

        SelectedAnimalTextBlock.Text =
            "No animal selected.";

        EnergyTextBlock.Text = "0 / 100";
        EnergyProgressBar.Value = 0;

        SwimButton.IsEnabled = false;
        PerformButton.IsEnabled = false;
    }

    private void UpdateSelectedAnimal()
    {
        if (AnimalListBox.SelectedItem is not Animal animal)
        {
            SelectedAnimalTextBlock.Text =
                "No animal selected.";

            EnergyTextBlock.Text =
                "0 / 100";

            EnergyProgressBar.Value = 0;

            SwimButton.IsEnabled = false;
            PerformButton.IsEnabled = false;

            return;
        }

        SelectedAnimalTextBlock.Text =
            $"{animal.GetType().Name}: {animal.Name}";

        EnergyTextBlock.Text =
            $"{animal.Energy} / 100";

        EnergyProgressBar.Value =
            animal.Energy;

        SwimButton.IsEnabled =
            animal is ISwimmable;

        PerformButton.IsEnabled =
            animal is IPerformer;
    }

    private void ClearLog_Click(object sender, RoutedEventArgs e)
    {
        LogListBox.Items.Clear();
    }
}


