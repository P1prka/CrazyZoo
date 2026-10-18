using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using CrazyZoo.Core;

namespace CrazyZoo.WpfApp;

public class MainViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private readonly Repository<Lion> lions = new();
    private readonly Repository<Zebra> zebras = new();
    private readonly Repository<Giraffe> giraffes = new();
    private readonly Repository<Hippo> hippos = new();
    private readonly Repository<Penguin> penguins = new();
    private readonly Repository<Monkey> monkeys = new();
    private readonly Repository<Fox> foxes = new();
    private readonly Repository<Panda> pandas = new();
    private readonly Repository<Elephant> elephants = new();


    public ObservableCollection<Animal> VisibleAnimals { get; } = new();
    public ObservableCollection<string> LogMessages { get; } = new();

    public string[] AnimalTypes { get; } =
    {
        "Lion", "Zebra", "Giraffe",
        "Hippo", "Penguin", "Monkey",
        "Fox", "Panda", "Elephant"
    };

    public string[] SortOptions { get; } =
    {
        "Default",
        "Energy: high to low",
        "Energy: low to high"
    };

    public RelayCommand AddCommand { get; }
    public RelayCommand RemoveCommand { get; }
    public RelayCommand NormalCommand { get; }
    public RelayCommand CrazyCommand { get; }
    public RelayCommand SwimCommand { get; }
    public RelayCommand PerformCommand { get; }
    public RelayCommand ClearLogCommand { get; }

    private string animalName = "";

    public string AnimalName
    {
        get => animalName;
        set
        {
            animalName = value ?? "";
            OnPropertyChanged(nameof(AnimalName));
            AddCommand.RaiseCanExecuteChanged();
        }
    }

    private string energyText = "50";

    public string EnergyText
    {
        get => energyText;
        set
        {
            energyText = value ?? "";
            OnPropertyChanged(nameof(EnergyText));
            AddCommand.RaiseCanExecuteChanged();
        }
    }

    private string selectedType = "Lion";

    public string SelectedType
    {
        get => selectedType;
        set
        {
            selectedType = value ?? "";
            OnPropertyChanged(nameof(SelectedType));

            RefreshVisibleAnimals();
            AddCommand.RaiseCanExecuteChanged();
        }
    }

    private string searchText = "";

    public string SearchText
    {
        get => searchText;
        set
        {
            searchText = value ?? "";
            OnPropertyChanged(nameof(SearchText));
            RefreshVisibleAnimals();
        }
    }

    private string sortMode = "Default";

    public string SortMode
    {
        get => sortMode;
        set
        {
            sortMode = value ?? "Default";
            OnPropertyChanged(nameof(SortMode));
            RefreshVisibleAnimals();
        }
    }

    private Animal? selectedAnimal;

    public Animal? SelectedAnimal
    {
        get => selectedAnimal;
        set
        {
            if (ReferenceEquals(selectedAnimal, value))
            {
                return;
            }

            selectedAnimal = value;
            OnPropertyChanged(nameof(SelectedAnimal));
            OnPropertyChanged(nameof(SelectedDescription));

            UpdateEnergyDisplay();

            RemoveCommand.RaiseCanExecuteChanged();
            NormalCommand.RaiseCanExecuteChanged();
            CrazyCommand.RaiseCanExecuteChanged();
            SwimCommand.RaiseCanExecuteChanged();
            PerformCommand.RaiseCanExecuteChanged();
        }
    }

    public string SelectedDescription =>
        SelectedAnimal?.ToString() ?? "No animal selected.";

    public int SelectedEnergy => SelectedAnimal?.Energy ?? 0;

    public string SelectedEnergyText => $"{SelectedEnergy} / 100";

    public string StatisticsText
    {
        get
        {
            return SelectedType switch
            {
                "Lion" => GetStatistics(lions),
                "Zebra" => GetStatistics(zebras),
                "Giraffe" => GetStatistics(giraffes),
                "Hippo" => GetStatistics(hippos),
                "Penguin" => GetStatistics(penguins),
                "Monkey" => GetStatistics(monkeys),

                "Fox" => GetStatistics(foxes),
                "Panda" => GetStatistics(pandas),
                "Elephant" => GetStatistics(elephants),

                _ => ""
            };
        }
    }

    public MainViewModel()
    {
        AddCommand = new RelayCommand(AddAnimal, CanAddAnimal);

        RemoveCommand = new RelayCommand(
            RemoveAnimal, () => SelectedAnimal != null);

        NormalCommand = new RelayCommand(
            DoNormalAction, () => SelectedAnimal != null);

        CrazyCommand = new RelayCommand(
            DoCrazyAction, () => SelectedAnimal != null);

        SwimCommand = new RelayCommand(
            DoSwim, () => SelectedAnimal is ISwimmable);

        PerformCommand = new RelayCommand(
            DoPerform, () => SelectedAnimal is IPerformer);

        ClearLogCommand = new RelayCommand(
            () => LogMessages.Clear(),
            () => LogMessages.Count > 0);

        LogMessages.CollectionChanged += (sender, e) =>
            ClearLogCommand.RaiseCanExecuteChanged();

        lions.LogCreated += OnLogCreated;
        zebras.LogCreated += OnLogCreated;
        giraffes.LogCreated += OnLogCreated;
        hippos.LogCreated += OnLogCreated;
        penguins.LogCreated += OnLogCreated;
        monkeys.LogCreated += OnLogCreated;

        foxes.LogCreated += OnLogCreated;
        pandas.LogCreated += OnLogCreated;
        elephants.LogCreated += OnLogCreated;


        RefreshVisibleAnimals();
    }

    private bool CanAddAnimal()
    {
        return AnimalTypes.Contains(SelectedType)
            && !string.IsNullOrWhiteSpace(AnimalName)
            && int.TryParse(EnergyText, out int energy)
            && energy >= 0
            && energy <= 100;
    }

    private void AddAnimal()
    {
        if (!CanAddAnimal())
        {
            return;
        }

        string name = AnimalName.Trim();
        int energy = int.Parse(EnergyText);

        Animal animal = SelectedType switch
        {
            "Lion" => new Lion(name, energy),
            "Zebra" => new Zebra(name, energy),
            "Giraffe" => new Giraffe(name, energy),
            "Hippo" => new Hippo(name, energy),
            "Penguin" => new Penguin(name, energy),
            "Monkey" => new Monkey(name, energy),
            "Fox" => new Fox(name, energy),
            "Panda" => new Panda(name, energy),
            "Elephant" => new Elephant(name, energy),

            _ => throw new InvalidOperationException("Unknown animal type.")
        };

        switch (animal)
        {
            case Lion lion:
                lions.Add(lion);
                break;
            case Zebra zebra:
                zebras.Add(zebra);
                break;
            case Giraffe giraffe:
                giraffes.Add(giraffe);
                break;
            case Hippo hippo:
                hippos.Add(hippo);
                break;
            case Penguin penguin:
                penguins.Add(penguin);
                break;
            case Monkey monkey:
                monkeys.Add(monkey);
                break;
            case Fox fox:
                foxes.Add(fox);
                break;
            case Panda panda:
                pandas.Add(panda);
                break;
            case Elephant elephant:
                elephants.Add(elephant);
                break;
        }

        LogMessages.Add($"Added {animal}.");

        RefreshVisibleAnimals();

        if (VisibleAnimals.Contains(animal))
        {
            SelectedAnimal = animal;
        }

        AnimalName = "";
        EnergyText = "50";
    }

    private void RemoveAnimal()
    {
        if (SelectedAnimal is not Animal animal)
        {
            return;
        }

        switch (animal)
        {
            case Lion lion:
                lions.Remove(lion);
                break;
            case Zebra zebra:
                zebras.Remove(zebra);
                break;
            case Giraffe giraffe:
                giraffes.Remove(giraffe);
                break;
            case Hippo hippo:
                hippos.Remove(hippo);
                break;
            case Penguin penguin:
                penguins.Remove(penguin);
                break;
            case Monkey monkey:
                monkeys.Remove(monkey);
                break;

            case Fox fox:
                foxes.Remove(fox);
                break;

            case Panda panda:
                pandas.Remove(panda);
                break;

            case Elephant elephant:
                elephants.Remove(elephant);
                break;

        }

        LogMessages.Add($"Removed {animal}.");
        RefreshVisibleAnimals();
    }

    private void DoNormalAction()
    {
        if (SelectedAnimal is null) return;

        LogMessages.Add(SelectedAnimal.NormalAction());
        UpdateEnergyDisplay();
    }

    private void DoCrazyAction()
    {
        if (SelectedAnimal is null) return;

        LogMessages.Add(SelectedAnimal.CrazyAction());
        UpdateEnergyDisplay();
    }

    private void DoSwim()
    {
        if (SelectedAnimal is not ISwimmable swimmer) return;

        LogMessages.Add(swimmer.Swim());
        UpdateEnergyDisplay();
    }

    private void DoPerform()
    {
        if (SelectedAnimal is not IPerformer performer) return;

        LogMessages.Add(performer.Perform());
        UpdateEnergyDisplay();
    }

    private void RefreshVisibleAnimals()
    {
        IEnumerable<Animal> animals = SelectedType switch
        {
            "Lion" => GetFilteredAnimals(lions),
            "Zebra" => GetFilteredAnimals(zebras),
            "Giraffe" => GetFilteredAnimals(giraffes),
            "Hippo" => GetFilteredAnimals(hippos),
            "Penguin" => GetFilteredAnimals(penguins),
            "Monkey" => GetFilteredAnimals(monkeys),

            "Fox" => GetFilteredAnimals(foxes),
            "Panda" => GetFilteredAnimals(pandas),
            "Elephant" => GetFilteredAnimals(elephants),

            _ => Array.Empty<Animal>()
        };

        VisibleAnimals.Clear();

        foreach (Animal animal in animals)
        {
            VisibleAnimals.Add(animal);
        }

        SelectedAnimal = null;
        OnPropertyChanged(nameof(StatisticsText));
    }

    private IEnumerable<Animal> GetFilteredAnimals<T>(
        Repository<T> repository) where T : Animal
    {
        IEnumerable<T> animals;

        if (string.IsNullOrWhiteSpace(SearchText))
        {
            animals = repository.GetAll();
        }
        else
        {
            animals = repository.SearchByName(SearchText);
        }

        if (SortMode == "Energy: high to low")
        {
            return animals
                .OrderByDescending(a => a.Energy)
                .ToList();
        }

        if (SortMode == "Energy: low to high")
        {
            return animals
                .OrderBy(a => a.Energy)
                .ToList();
        }

        return animals.ToList();
    }

    private string GetStatistics<T>(
        Repository<T> repository) where T : Animal
    {
        Dictionary<string, int> groups =
            repository.CountByEnergyLevel();

        int low = groups.GetValueOrDefault("Low");
        int medium = groups.GetValueOrDefault("Medium");
        int high = groups.GetValueOrDefault("High");

        int total = repository.GetAll().Count();
        double average = repository.AverageEnergy();

        return $"Total: {total} | Low: {low} | Medium: {medium} | High: {high}"
            + $"\nAverage energy: {average:0.0}";
    }

    private void OnLogCreated(
        object? sender, LogMessageEventArgs e)
    {
        LogMessages.Add(e.Message);
        UpdateEnergyDisplay();
    }

    private void UpdateEnergyDisplay()
    {
        OnPropertyChanged(nameof(SelectedEnergy));
        OnPropertyChanged(nameof(SelectedEnergyText));
        OnPropertyChanged(nameof(StatisticsText));
    }

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(
            this,
            new PropertyChangedEventArgs(propertyName));
    }
}
