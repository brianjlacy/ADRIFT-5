using ADRIFT.Core.Models;
using ADRIFT.Core.IO;

namespace ADRIFT.Core.Testing;

/// <summary>
/// Simple test class for validating serialization round-trip
/// </summary>
public static class SerializationTest
{
    public static async Task<bool> RunFullTest(string testDirectory)
    {
        Console.WriteLine("=== ADRIFT Serialization Test ===\n");

        try
        {
            // Create test directory if it doesn't exist
            if (!Directory.Exists(testDirectory))
            {
                Directory.CreateDirectory(testDirectory);
            }

            // Step 1: Generate sample adventure
            Console.WriteLine("1. Generating sample adventure...");
            var originalAdventure = SampleDataGenerator.CreateSampleAdventure();
            Console.WriteLine($"   Created: {originalAdventure.Title}");
            Console.WriteLine($"   - Locations: {originalAdventure.Locations.Count}");
            Console.WriteLine($"   - Objects: {originalAdventure.Objects.Count}");
            Console.WriteLine($"   - Characters: {originalAdventure.Characters.Count}");
            Console.WriteLine($"   - Tasks: {originalAdventure.Tasks.Count}");
            Console.WriteLine($"   - Events: {originalAdventure.Events.Count}");
            Console.WriteLine($"   - Variables: {originalAdventure.Variables.Count}");
            Console.WriteLine($"   - Groups: {originalAdventure.Groups.Count}");
            Console.WriteLine($"   - Synonyms: {originalAdventure.Synonyms.Count}");
            Console.WriteLine($"   - Hints: {originalAdventure.Hints.Count}");
            Console.WriteLine();

            // Step 2: Save as XML
            Console.WriteLine("2. Saving as XML...");
            string xmlPath = Path.Combine(testDirectory, "test_adventure.xml");
            await AdventureFileIO.SaveAdventureAsync(originalAdventure, xmlPath, compress: false);
            var xmlSize = new FileInfo(xmlPath).Length;
            Console.WriteLine($"   Saved to: {xmlPath}");
            Console.WriteLine($"   File size: {xmlSize:N0} bytes");
            Console.WriteLine();

            // Step 3: Save as compressed TAF
            Console.WriteLine("3. Saving as compressed TAF...");
            string tafPath = Path.Combine(testDirectory, "test_adventure.taf");
            await AdventureFileIO.SaveAdventureAsync(originalAdventure, tafPath, compress: true);
            var tafSize = new FileInfo(tafPath).Length;
            Console.WriteLine($"   Saved to: {tafPath}");
            Console.WriteLine($"   File size: {tafSize:N0} bytes");
            Console.WriteLine($"   Compression ratio: {(double)tafSize / xmlSize:P1}");
            Console.WriteLine();

            // Step 4: Load from XML
            Console.WriteLine("4. Loading from XML...");
            var loadedFromXml = await AdventureFileIO.LoadAdventureAsync(xmlPath);
            Console.WriteLine($"   Loaded: {loadedFromXml.Title}");
            Console.WriteLine();

            // Step 5: Load from TAF
            Console.WriteLine("5. Loading from TAF...");
            var loadedFromTaf = await AdventureFileIO.LoadAdventureAsync(tafPath);
            Console.WriteLine($"   Loaded: {loadedFromTaf.Title}");
            Console.WriteLine();

            // Step 6: Verify data integrity
            Console.WriteLine("6. Verifying data integrity...");
            var errors = new List<string>();

            VerifyAdventureMetadata(originalAdventure, loadedFromXml, "XML", errors);
            VerifyAdventureMetadata(originalAdventure, loadedFromTaf, "TAF", errors);

            VerifyLocations(originalAdventure, loadedFromXml, "XML", errors);
            VerifyLocations(originalAdventure, loadedFromTaf, "TAF", errors);

            VerifyObjects(originalAdventure, loadedFromXml, "XML", errors);
            VerifyObjects(originalAdventure, loadedFromTaf, "TAF", errors);

            VerifyCharacters(originalAdventure, loadedFromXml, "XML", errors);
            VerifyCharacters(originalAdventure, loadedFromTaf, "TAF", errors);

            VerifyTasks(originalAdventure, loadedFromXml, "XML", errors);
            VerifyTasks(originalAdventure, loadedFromTaf, "TAF", errors);

            VerifyEvents(originalAdventure, loadedFromXml, "XML", errors);
            VerifyEvents(originalAdventure, loadedFromTaf, "TAF", errors);

            VerifyVariables(originalAdventure, loadedFromXml, "XML", errors);
            VerifyVariables(originalAdventure, loadedFromTaf, "TAF", errors);

            if (errors.Count == 0)
            {
                Console.WriteLine("   ✓ All data verified successfully!");
                Console.WriteLine();
                Console.WriteLine("=== TEST PASSED ===");
                return true;
            }
            else
            {
                Console.WriteLine($"   ✗ Found {errors.Count} errors:");
                foreach (var error in errors)
                {
                    Console.WriteLine($"     - {error}");
                }
                Console.WriteLine();
                Console.WriteLine("=== TEST FAILED ===");
                return false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n✗ Test failed with exception: {ex.Message}");
            Console.WriteLine(ex.StackTrace);
            Console.WriteLine("\n=== TEST FAILED ===");
            return false;
        }
    }

    private static void VerifyAdventureMetadata(Adventure original, Adventure loaded, string format, List<string> errors)
    {
        if (original.Title != loaded.Title)
            errors.Add($"{format}: Title mismatch");
        if (original.Author != loaded.Author)
            errors.Add($"{format}: Author mismatch");
        if (original.Version != loaded.Version)
            errors.Add($"{format}: Version mismatch");
    }

    private static void VerifyLocations(Adventure original, Adventure loaded, string format, List<string> errors)
    {
        if (original.Locations.Count != loaded.Locations.Count)
        {
            errors.Add($"{format}: Location count mismatch ({original.Locations.Count} vs {loaded.Locations.Count})");
            return;
        }

        foreach (var kvp in original.Locations)
        {
            if (!loaded.Locations.ContainsKey(kvp.Key))
            {
                errors.Add($"{format}: Missing location {kvp.Key}");
                continue;
            }

            var orig = kvp.Value;
            var load = loaded.Locations[kvp.Key];

            if (orig.ShortDescription != load.ShortDescription)
                errors.Add($"{format}: Location {kvp.Key} ShortDescription mismatch");
            if (orig.Directions.Count != load.Directions.Count)
                errors.Add($"{format}: Location {kvp.Key} Direction count mismatch");
        }
    }

    private static void VerifyObjects(Adventure original, Adventure loaded, string format, List<string> errors)
    {
        if (original.Objects.Count != loaded.Objects.Count)
        {
            errors.Add($"{format}: Object count mismatch ({original.Objects.Count} vs {loaded.Objects.Count})");
            return;
        }

        foreach (var kvp in original.Objects)
        {
            if (!loaded.Objects.ContainsKey(kvp.Key))
            {
                errors.Add($"{format}: Missing object {kvp.Key}");
                continue;
            }

            var orig = kvp.Value;
            var load = loaded.Objects[kvp.Key];

            if (orig.Name != load.Name)
                errors.Add($"{format}: Object {kvp.Key} Name mismatch");
            if (orig.Aliases.Count != load.Aliases.Count)
                errors.Add($"{format}: Object {kvp.Key} Aliases count mismatch");
            if (orig.LocationType != load.LocationType)
                errors.Add($"{format}: Object {kvp.Key} LocationType mismatch");
            if (orig.Size != load.Size)
                errors.Add($"{format}: Object {kvp.Key} Size mismatch");
            if (orig.IsContainer != load.IsContainer)
                errors.Add($"{format}: Object {kvp.Key} IsContainer mismatch");
        }
    }

    private static void VerifyCharacters(Adventure original, Adventure loaded, string format, List<string> errors)
    {
        if (original.Characters.Count != loaded.Characters.Count)
        {
            errors.Add($"{format}: Character count mismatch");
            return;
        }

        foreach (var kvp in original.Characters)
        {
            if (!loaded.Characters.ContainsKey(kvp.Key))
            {
                errors.Add($"{format}: Missing character {kvp.Key}");
                continue;
            }

            var orig = kvp.Value;
            var load = loaded.Characters[kvp.Key];

            if (orig.Name != load.Name)
                errors.Add($"{format}: Character {kvp.Key} Name mismatch");
            if (orig.Topics.Count != load.Topics.Count)
                errors.Add($"{format}: Character {kvp.Key} Topics count mismatch");
            if (orig.WalkSteps.Count != load.WalkSteps.Count)
                errors.Add($"{format}: Character {kvp.Key} WalkSteps count mismatch");
        }
    }

    private static void VerifyTasks(Adventure original, Adventure loaded, string format, List<string> errors)
    {
        if (original.Tasks.Count != loaded.Tasks.Count)
        {
            errors.Add($"{format}: Task count mismatch");
            return;
        }

        foreach (var kvp in original.Tasks)
        {
            if (!loaded.Tasks.ContainsKey(kvp.Key))
            {
                errors.Add($"{format}: Missing task {kvp.Key}");
                continue;
            }

            var orig = kvp.Value;
            var load = loaded.Tasks[kvp.Key];

            if (orig.Name != load.Name)
                errors.Add($"{format}: Task {kvp.Key} Name mismatch");
            if (orig.Commands.Count != load.Commands.Count)
                errors.Add($"{format}: Task {kvp.Key} Commands count mismatch");
        }
    }

    private static void VerifyEvents(Adventure original, Adventure loaded, string format, List<string> errors)
    {
        if (original.Events.Count != loaded.Events.Count)
        {
            errors.Add($"{format}: Event count mismatch");
            return;
        }

        foreach (var kvp in original.Events)
        {
            if (!loaded.Events.ContainsKey(kvp.Key))
            {
                errors.Add($"{format}: Missing event {kvp.Key}");
                continue;
            }

            var orig = kvp.Value;
            var load = loaded.Events[kvp.Key];

            if (orig.Name != load.Name)
                errors.Add($"{format}: Event {kvp.Key} Name mismatch");
            if (orig.Type != load.Type)
                errors.Add($"{format}: Event {kvp.Key} Type mismatch");
        }
    }

    private static void VerifyVariables(Adventure original, Adventure loaded, string format, List<string> errors)
    {
        if (original.Variables.Count != loaded.Variables.Count)
        {
            errors.Add($"{format}: Variable count mismatch");
            return;
        }

        foreach (var kvp in original.Variables)
        {
            if (!loaded.Variables.ContainsKey(kvp.Key))
            {
                errors.Add($"{format}: Missing variable {kvp.Key}");
                continue;
            }

            var orig = kvp.Value;
            var load = loaded.Variables[kvp.Key];

            if (orig.Name != load.Name)
                errors.Add($"{format}: Variable {kvp.Key} Name mismatch");
            if (orig.Type != load.Type)
                errors.Add($"{format}: Variable {kvp.Key} Type mismatch");
        }
    }
}
