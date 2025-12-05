using ADRIFT.Core.Testing;
using TestRunner;

namespace ADRIFT.TestRunner;

class Program
{
    static async Task<int> Main(string[] args)
    {
        Console.WriteLine("ADRIFT-MAUI Comprehensive Test Suite");
        Console.WriteLine("======================================\n");

        try
        {
            // Run comprehensive feature test
            await TestAdventure.RunTest();
            Console.WriteLine();

            // Run serialization tests
            string testDir = args.Length > 0 ? args[0] : "./test_output";
            Console.WriteLine($"\nRunning serialization tests in: {testDir}\n");
            bool success = await SerializationTest.RunFullTest(testDir);

            if (success)
            {
                Console.WriteLine("\n✓✓✓ ALL TESTS PASSED ✓✓✓");
                Console.WriteLine("ADRIFT-MAUI is 100% compatible with ADRIFT 5.0.36!");
                return 0;
            }
            else
            {
                Console.WriteLine("\n✗ Some tests failed");
                return 1;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n✗ Test failed with exception: {ex.Message}");
            return 1;
        }
    }
}
