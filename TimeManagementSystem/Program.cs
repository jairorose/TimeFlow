// Projecten kunenn aanmaken
// Een project heeft: een naam

// Taak aan project kunnen hangen
// Een taak heeft: een begintijd, eindtijd, beschrijving, 

// CRUD: create, read, update, delete

// 1. Project aanmaken
// 2. Projecten bekijken
// 3. Timer starten
// 4. Timer stoppen
// 5. Tijd handmatig toevoegen
// 6. Week overzicht bekijken

// Initalizing data
List<string> projects = new List<string> {"MMA", "Lezen", "School"}; // List of existing projects
List<string[]> timeEntries = new List<string[]>();

do
{
    Console.WriteLine("** Time Management System **");
    Console.WriteLine("Menu: Kies een optie");
    Console.WriteLine("1. Project aanmaken");
    Console.WriteLine("2. Projecten bekijken");
    Console.WriteLine("3. Timer starten");
    Console.WriteLine("4. Timer stoppen");
    Console.WriteLine("5. Tijd handmatig toevoegen");
    Console.WriteLine("6. Week overzicht bekijken");

    string readInput = Console.ReadLine();

    switch (readInput)
    {
        case "1":
            projects = CreateProject(projects);
            break;
        case "2":
            ShowProjects(projects);
            break;
        case "3":
            //
            break;
        case "4":
            //
            break;
        case "5":
            CreateTimeEntry(timeEntries);
            break;
        case "6":
            ShowTimeEntries(timeEntries);
            break;
        default:
            Console.WriteLine("Optie niet herkent");
            break; 
    }

    Console.WriteLine("Druk op Enter om verder te gaan...");
    readInput = Console.ReadLine();
} while (true);

static List<string> CreateProject(List<string> projects)
{
    Console.WriteLine("Maak een project aan. Wat is de naam van het project?");
    string readInput = Console.ReadLine();

    string projectName = readInput;
    Console.WriteLine($"Project '{projectName}' is aangemaakt!");
    Console.WriteLine(); // White space
    projects.Add(projectName);
    
    return projects;
}

static void ShowProjects(List<string> projects)
{
    // Read
    Console.WriteLine("Projecten:");

    foreach (string project in projects)
    {
        Console.WriteLine(project);
    }
}

static List<string[]> CreateTimeEntry(List<string[]> timeEntries)
{
    // Time entry aanmaken
    Console.WriteLine("Voeg een taak toe, begin met de beschrijving:");
    string readInput = Console.ReadLine();

    string timeEntryDescription = readInput;

    Console.WriteLine("Geef een begin tijd op: 23:59 23-07-2026");
    readInput = Console.ReadLine();

    string timeEntryStartTime = readInput;

    Console.WriteLine("Geef een eind tijd op: 23:59 23-07-2026");
    readInput = Console.ReadLine();

    string timeEntryEndTime = readInput;

    Console.WriteLine("Geef een gekoppelde project aan");
    readInput = Console.ReadLine();

    string projectName = readInput;

    Console.WriteLine($"Taak is aangemaakt: {timeEntryDescription}, van {timeEntryStartTime} tot {timeEntryEndTime} (Project: {projectName})");
    string[] timeEntry = {timeEntryDescription, timeEntryStartTime, timeEntryEndTime, projectName};

    timeEntries.Add(timeEntry);

    return timeEntries;
}

static void ShowTimeEntries(List<string[]> timeEntries)
{
    Console.WriteLine("Dit zijn alle time entries:");

    foreach (string[] timeEntry in timeEntries)
    {
        string timeEntryDescription = timeEntry[0];
        string timeEntryStartTime = timeEntry[1];
        string timeEntryEndTime = timeEntry[2];
        string projectName = timeEntry[3];

        Console.WriteLine($"{timeEntryDescription}, vanaf {timeEntryStartTime} tot {timeEntryEndTime} (Project: {projectName})");
    }
}