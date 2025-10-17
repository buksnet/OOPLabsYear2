using Lab5;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.InputEncoding = System.Text.Encoding.UTF8;

try
{
    var stateMachine = new Cycle();
    stateMachine.Run();
}
catch (Exception ex)
{
    Console.WriteLine($"Критическая ошибка: {ex.Message}");
    Console.WriteLine("Нажмите любую клавишу для выхода...");
    Console.ReadKey();
}