namespace AzureProject.Shared.Services.Cosmos;

public static class CosmosNoteService
{
    private static readonly CosmosContext Context = new();

    public static async Task Demo()
    {
        //await Context.Database.EnsureDeletedAsync();
        //await Create();
        //await Read();
        //await Update();
        await Delete();
    }

    private static async Task Create()
    {
        await Context.Database.EnsureCreatedAsync();
        
        for (var i = 1; i <= 1000; i++)
        {
            var note = new NoteModel {Title = $"Test{i}", Message = $"Message{i}"};
            Console.WriteLine($"Creating note {i}.");
            Context.Note.Add(note);
        }

        await Context.SaveChangesAsync();
        Console.WriteLine("Notes saved.");
    }

    private static Task Read()
    {
        var notes = Context.Note.ToList();
        foreach (var note in notes) Console.WriteLine($"{note.Title} {note.Message}");
        return Task.CompletedTask;
    }

    private static async Task Update()
    {
        var notes = Context.Note.ToList();
        foreach (var note in notes)
        {
            note.Title += "-updated";
            note.Message += "-updated";
            Context.Note.Update(note);
            Console.WriteLine($"Updating note {note.Id}.");
        }
        await Context.SaveChangesAsync();
        Console.WriteLine("Update success!");
    }

    private static async Task Delete()
    {
        var notes = Context.Note.ToList();
        foreach (var note in notes) Context.Note.Remove(note);
        await Context.SaveChangesAsync();
        Console.WriteLine("Delete success!");
    }
}