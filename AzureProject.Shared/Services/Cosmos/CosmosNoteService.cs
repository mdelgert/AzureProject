namespace AzureProject.Shared.Services.Cosmos;

public static class CosmosNoteService
{
    private static readonly CosmosContext Context = new();

    public static Task<string> GetNotes()
    {
        var notes = Context.Note.ToList();
        var response = JsonConvert.SerializeObject(notes, Formatting.Indented);
        return Task.FromResult(response);
    }
    
    public static async Task Demo()
    {
        //await Context.Database.EnsureDeletedAsync();
        await Context.Database.EnsureCreatedAsync();
        
        await Create();
        //await Read();
        //await Update();
        //await Delete();
    }

    private static async Task Create()
    {
        
        //await Context.Database.EnsureDeletedAsync();
        //await Context.Database.EnsureCreatedAsync();
        
        for (var i = 1; i <= 10; i++)
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
        foreach (var note in notes)
        {
            Console.WriteLine($"Deleting note {note.Id}.");
            Context.Note.Remove(note);
        }
        await Context.SaveChangesAsync();
        Console.WriteLine("Delete success!");
    }
}