using ADRIFT.Core.Testing;

namespace ADRIFT.TestRunner;

class Program
{
    static async Task<int> Main(string[] args)
    {
        Console.WriteLine("ADRIFT File I/O Test Runner");
        Console.WriteLine("============================\n");

        string testDir = args.Length > 0 ? args[0] : "./test_output";

        Console.WriteLine($"Test directory: {testDir}\n");

        bool success = await SerializationTest.RunFullTest(testDir);

        return success ? 0 : 1;
    }
}
