using System.Text;
using EFCore0004;

var context = new MyDbContext();

var listaContinue = new List<string>(){ "Sim","Não" };
var continuar     = true;

do 
{

var firstList = new List<string>()
{
    "Usuario",
    "Grupo"
};

if (SelectOption(firstList, "Você deseja cadastrar User ou Group?", true) == 1)
{
    Console.WriteLine("Digite o nome do usuário:");
    var userName = Console.ReadLine();

    while (string.IsNullOrEmpty(userName))
    {
        Console.WriteLine("Digite o nome do usuário:");
        userName = Console.ReadLine();
    }

    Console.WriteLine("Digite a Senha:");
    var password = Console.ReadLine();

    while (string.IsNullOrEmpty(password)){
        
    Console.WriteLine("Digite a Senha:");
        password = Console.ReadLine();
    }

    var lista = context.Groups.ToList().Select(g => g.Name).ToList();
    var groupIndex = SelectOption(lista, "Escolha um Grupo", false);
    var opcaoEscolhida = context.Groups.Find(groupIndex);

    context.Users.Add(new User()
    { 
        Name     = userName, 
        Password = password, 
        Group    = opcaoEscolhida
    });
    
    context.SaveChanges();

    Console.WriteLine();
    Console.WriteLine("Usuário Cadastrado com sucesso!");
    Console.WriteLine();
    
   continuar = ContinueOption(listaContinue);

}
else
{
    Console.WriteLine("Digite o nome do grupo:");
    var groupName = Console.ReadLine();
    
    context.Groups.Add(new Group(){ Name = groupName});
    context.SaveChanges();

    Console.WriteLine("Grupo Cadastrado com sucesso!");

    continuar = ContinueOption(listaContinue);

}

Console.WriteLine("RESUMO DA OPERAÇÃO:");
Console.WriteLine("-------------------");
Console.WriteLine();

Console.WriteLine("USERS:");
foreach (var u in context.Users)
{
    Console.WriteLine("ID: {0} - Name: {1} - Group: {2}", u.Id, u.Name, u.Group.Name);
}

Console.WriteLine();

Console.WriteLine("GROUPS:");
foreach (var g in context.Groups)
{
    Console.WriteLine("ID: {0} - Name: {1}", g.Id, g.Name);
}

Console.WriteLine();

}
while(continuar);

static bool ContinueOption(List<string> listaContinue) 
{
     var continueIndex = SelectOption(listaContinue, "Deseja Continuar?", false);
    return continueIndex == 1 ? true : false;
}

static int SelectOption(List<string>? options, string titulo, bool clearScreen)
{
    if (clearScreen)
        Console.Clear();

    Console.OutputEncoding = Encoding.UTF8;
    Console.CursorVisible = false;
    Console.ForegroundColor = ConsoleColor.Cyan;

    Console.WriteLine(titulo);
    Console.ResetColor();
    Console.WriteLine("\nUse ⬆️  and ⬇️  to navigate and press \u001b[32mEnter/Return\u001b[0m to select:");

    (int leftGroup, int topGroup) = Console.GetCursorPosition();
    var optionGroup = 1;
    var decoratorGroup = "✅ \u001b[32m";
    ConsoleKeyInfo keyGroup;
    bool isSelectedGroup = false;

    while (!isSelectedGroup)
    {
        Console.SetCursorPosition(leftGroup, topGroup);

        var aux = 1;
        foreach (var g in options)
        {
            Console.WriteLine($"{(optionGroup == aux ? decoratorGroup : "   ")}{g}\u001b[0m");
            aux++;
        }

        keyGroup = Console.ReadKey(false);

        switch (keyGroup.Key)
        {
            case ConsoleKey.UpArrow:
                optionGroup = optionGroup == 1 ? options.Count() : optionGroup - 1;
                break;

            case ConsoleKey.DownArrow:
                optionGroup = optionGroup == options.Count() ? 1 : optionGroup + 1;
                break;

            case ConsoleKey.Enter:
                isSelectedGroup = true;
                break;
        }
    }

    return optionGroup;
}