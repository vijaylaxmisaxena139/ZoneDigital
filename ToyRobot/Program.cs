// See https://aka.ms/new-console-template for more information
using ToyRobot;
try
{
    #region Declaration
    string inputChar = String.Empty;
    #endregion

    #region Initialization
    // Here we are asking user for the input type File/Manual 
    Console.WriteLine("Please enter F and press enter to provide file Or enter M and press enter for manual inputs.");
    inputChar = Console.ReadLine();
    #endregion

    #region Calling BAL for data processing
    //Process the user input (file or manual)
    if (!String.IsNullOrEmpty(inputChar) && inputChar.ToLower() == "f")
        new Robot().ProcessFileInput();
    else if (!String.IsNullOrEmpty(inputChar) && inputChar.ToLower() == "m")
        new Robot().ProcessManualInput();
    else
        Console.WriteLine("Invalid input."); 
    #endregion

    Console.WriteLine("End of program, Goodbye!");
}
catch {
    //Exception handling
    Console.WriteLine("Invalid input.");
    Console.WriteLine("End of program, Goodbye!");
}
Console.ReadKey();

