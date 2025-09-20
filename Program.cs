using LapTrinhNangCao1;

var svc = new StudentService();
while (true)
{
    Console.WriteLine("=== Student Manager ===");
    Console.WriteLine("1. Input students");
    Console.WriteLine("2. Search by id/name");
    Console.WriteLine("3. Edit or delete by Id");
    Console.WriteLine("4. Statistics (Name | Course | Total)");
    Console.WriteLine("5. Show all");
    Console.WriteLine("0. Exit");
    Console.Write("Your choice: ");
    string c = Console.ReadLine() ?? "";
    Console.WriteLine();

    switch (c)
    {
        case "1": svc.InputMany(); break;
        case "2": svc.Search(); break;
        case "3": svc.EditOrDelete(); break;
        case "4": svc.Report(); break;
        case "5": svc.ShowAll(); break;
        case "0": return;
        default: Console.WriteLine("Invalid choice.\n"); break;
    }
}
